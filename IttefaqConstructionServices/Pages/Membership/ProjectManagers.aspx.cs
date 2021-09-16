using IttefaqConstructionServices.Logic;
using IttefaqConstructionServices.Pages.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Membership
{
    public partial class ProjectManagers : System.Web.UI.Page
    {
        int pmID = 0;
        Single quantity = 0.0f;
        string UoM = string.Empty;
        Single rate = 0.0f;
        Utilities p = new Utilities();
        OtherReports ot = new OtherReports();
        DateTime date;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect(@"~\Pages\Membership\Login.aspx");
            }
            //if (p.Authenticated(Session["UserName"].ToString(), "accounts, viewer", Session["userRoles"].ToString()))
            if (Session["userRoles"].ToString().Contains("pm") || Session["UserName"].ToString() == "tasawarahmed")
            {
                if (Session["UserName"].ToString() == "tasawarahmed")
                {
                    pmID = 151;
                }
                else
                {
                    pmID = int.Parse(Session["PM_ID"].ToString());
                }

                if (!IsPostBack)
                {
                    loadSites();
                    loadExpenseAccounts();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                    DateTime date = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
                    txtDate.Text = p.fixDateWithMonthNames(date.Day, date.Month, date.Year);
                    //txtDateCredit.Text = txtDateDebit.Text = txtChequesInHandDate.Text = txtChequesInHandChequeDate.Text = p.fixDateWithMonthNames(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                    txtDate.Enabled = false;
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadExpenseAccounts()
        {
            List<int> PMParents = p.getIntList("SELECT id FROM tblGenAccounts WHERE (parentAccountID = 7)");

            string query = string.Empty;
            string whereClause = "Where ";

            for (int i = 0; i < PMParents.Count; i++)
            {
                query = "Select accountName, id from viewActiveAccounts ";
                int ID = PMParents[i];
                whereClause += " (parentAccountID = " + ID.ToString() + ") ";

                if ((i + 1) < PMParents.Count)
                {
                    whereClause += " OR";
                }
            }

            if (query != string.Empty && !whereClause.Equals("Where "))
            {
                query = query + whereClause + " ORDER BY accountName";
                List<DDList> PMs = new List<DDList>();
                PMs = p.getDDList(query);

                cmbExpenses.DataSource = PMs;
                cmbExpenses.DataTextField = "DisplayName";
                cmbExpenses.DataValueField = "Value";
                cmbExpenses.DataBind();
            }
        }

        private void loadSites()
        {
            string query = string.Format("SELECT tblGenSites.siteName, tblUsersSitesAssociation.siteID  FROM tblUsersSitesAssociation INNER JOIN tblGenSites ON tblUsersSitesAssociation.siteID = tblGenSites.id WHERE (tblGenSites.isactive = 'true') AND (tblUsersSitesAssociation.PMID = {0})", pmID);
            List<DDList> sites = new List<DDList>();
            sites = p.getDDList(query);

            cmbSites.DataSource = sites;
            cmbSites.DataTextField = "DisplayName";
            cmbSites.DataValueField = "Value";
            cmbSites.DataBind();

        }

        private void showWarningMessage(string s)
        {
            lblWarning.Visible = true;
            lblWarning.Text = p.getWarningMessage(s);
        }

        private void showSuccessMessage(string s)
        {
            lblSuccess.Visible = true;
            lblSuccess.Text = p.getSuccessMessage(s);
        }

        private void clearLabels()
        {
            lblSuccess.Visible = lblWarning.Visible = false;
        }

        protected void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                clearLabels();
                if (cmbExpenses.SelectedIndex > 0 && cmbSites.SelectedIndex > 0)
                {
                    int siteID = int.Parse(cmbSites.SelectedValue);
                    int debitAccountID = int.Parse(cmbExpenses.SelectedValue);
                    int creditAccountID = pmID;
                    Decimal amount = Decimal.Parse(txtAmount.Text);
                    string description = p.FixString(txtDescription.Text);
                    if (description.Contains("@"))
                    {
                        string[] strings = description.Split(' ');

                        quantity = float.Parse(strings[0]);
                        UoM = strings[1].ToLower();
                        rate = float.Parse(strings[3]);
                    }

                    date = DateTime.Parse(txtDate.Text);

                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    int zero = 0;
                    string debitString = transID.ToString() + debitAccountID.ToString() + zero + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + zero + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator, siteID) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}', {12})", transID, debitAccountID, date, zero, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"], siteID);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, zero, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    loadTransactionGrid();
                }
                else
                {
                    throw new Exception("Please select Expense account and/or site account first.");
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void loadTransactionGrid()
        {
            string query = string.Format("SELECT tblGenAccountsTransactions.siteID, tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.operator, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id WHERE (tblGenAccountsTransactions.date = '{0}') AND (tblGenAccountsTransactions.operator = '{1}') ORDER BY tblGenAccountsTransactions.transactionID", date, Session["UserName"]);
            List<Account> transactions = ot.getListOfTransactions("Other", query);
            gridTransaction.DataSource = transactions;
            gridTransaction.DataBind();
        }
    }
}