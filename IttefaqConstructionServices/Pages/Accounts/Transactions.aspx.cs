using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Accounts
{
    public partial class Transactions : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        DateTime date = DateTime.Now;
        Decimal amount = 0;
        Decimal zero = 0;
        string description = string.Empty;
        string chequeNumber = string.Empty;
        Single quantity = 0.0f;
        string UoM = string.Empty;
        Single rate = 0.0f;
        List<AccountStructure> listTransaction = new List<AccountStructure>();
        Decimal totalDebits = 0;
        Decimal totalCredits = 0;
        string companyName = string.Empty;
        string companyAddress = string.Empty;
        string companyContacts = string.Empty;
        string companyWebsite = string.Empty;
        string companyEmail = string.Empty;
        Dictionary<int, string> accountNamesDictionary = new Dictionary<int, string>();
        Dictionary<int, string> siteNameDictionary = new Dictionary<int, string>();
        Decimal totalDebitsForFooter = 0;
        Decimal totalCreditsForFooter = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect(@"~\Pages\Membership\Login.aspx");
            }
            if (p.Authenticated(Session["UserName"].ToString(), "accounts, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadBanks();
                    loadCapitalAccounts();
                    loadExpensesAccounts();
                    loadIncomeAccounts();
                    loadIndividualAccounts();
                    loadInvestorAccounts();
                    loadPrimaryAccounts();
                    loadLoanAccounts();
                    loadSuppliersAccounts();
                    loadCashAndBankAccounts();
                    loadAccountsGrid();
                    loadTransactionGrid();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                    DateTime date = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
                    txtDate.Text = txtDateJournal.Text = p.fixDateWithMonthNames(date.Day, date.Month, date.Year);
                    txtDate.Enabled = txtDateJournal.Enabled = false;
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }                

        }

        private void loadTransactionGrid()
        {
            string userName = Session["UserName"].ToString();
            string query = string.Format("SELECT tblGenAccounts.accountName AS Account, tblGenAccountsTmpTransactions.debitAmount AS Debit, tblGenAccountsTmpTransactions.creditAmount AS Credit FROM tblGenAccountsTmpTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTmpTransactions.accountID = tblGenAccounts.id WHERE tblGenAccountsTmpTransactions.operator = '{0}'", userName);

            DataTable dt = p.GetDataTable(query);
            gridTransaction.DataSource = dt;
            gridTransaction.DataBind();
        }

        private void loadAccountsGrid()
        {
            string query = "SELECT tblGenAccounts.id AS id, tblGenAccounts.accountName AS accountName, tblGenAccounts_1.accountName AS mainHead FROM tblGenAccounts INNER JOIN tblGenAccounts AS tblGenAccounts_1 ON tblGenAccounts.parentAccountID = tblGenAccounts_1.id WHERE (tblGenAccounts.tertiaryAccountPrefix <> '000') AND (tblGenAccounts.isActive = 'true') AND (tblGenAccounts.remarks = 'General') ORDER BY tblGenAccounts.accountName";
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                gridAccounts.DataSource = dt;
                gridAccounts.DataBind();
                gridAccounts.UseAccessibleHeader = true;
                gridAccounts.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void loadSuppliersAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Suppliers") + " ORDER BY accountName ";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSupplier.DataSource = ddItems;
            cmbSupplier.DataTextField = "DisplayName";
            cmbSupplier.DataValueField = "Value";
            cmbSupplier.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Suppliers')";
            //int accountID = p.getAccountID(query);

            //if (accountID != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbSupplier.DataSource = ddItems;
            //    cmbSupplier.DataTextField = "DisplayName";
            //    cmbSupplier.DataValueField = "Value";
            //    cmbSupplier.DataBind();
            //}
        }

        private void loadCashAndBankAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Cash") + " ORDER BY accountName ";
            //List<string> accounts = new List<string>();
            //accounts.Add("Cash");
            //accounts.Add("Bank");

            //string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts(accounts) + " ORDER BY accountName ";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            string ddQuery2 = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Banks") + " ORDER BY accountName ";
            List<DDList> ddItems2 = new List<DDList>();
            ddItems2 = p.getDDListWithoutSelectOne(ddQuery2);

            for (int i = 0; i < ddItems2.Count; i++)
            {
                ddItems.Add(ddItems2[i]);
            }

            cmbCashAndBank.DataSource = ddItems;
            cmbCashAndBank.DataTextField = "DisplayName";
            cmbCashAndBank.DataValueField = "Value";
            cmbCashAndBank.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Cash in Hand')";
            //int accountID1 = p.getAccountID(query);

            //query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Cash at Bank')";
            //int accountID2 = p.getAccountID(query);

            //if (accountID1 != 0 && accountID2 != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0} OR parentAccountID = {1}) AND (isActive = '{2}') ORDER BY accountName", accountID1, accountID2, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbCashAndBank.DataSource = ddItems;
            //    cmbCashAndBank.DataTextField = "DisplayName";
            //    cmbCashAndBank.DataValueField = "Value";
            //    cmbCashAndBank.DataBind();
            //}
        }

        private void loadLoanAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Loans") + " ORDER BY accountName ";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbLoans.DataSource = ddItems;
            cmbLoans.DataTextField = "DisplayName";
            cmbLoans.DataValueField = "Value";
            cmbLoans.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Loans to Staff')";
            //int accountID = p.getAccountID(query);

            //if (accountID != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbLoans.DataSource = ddItems;
            //    cmbLoans.DataTextField = "DisplayName";
            //    cmbLoans.DataValueField = "Value";
            //    cmbLoans.DataBind();
            //}
        }

        private void loadPrimaryAccounts()
        {
            string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 0) AND (isActive = '{0}') AND (accountName NOT LIKE '%Site%') ORDER BY accountName", true);

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbPrimary.DataSource = ddItems;
            cmbPrimary.DataTextField = "DisplayName";
            cmbPrimary.DataValueField = "Value";
            cmbPrimary.DataBind();

            cmbPrimaryJournal.DataSource = ddItems;
            cmbPrimaryJournal.DataTextField = "DisplayName";
            cmbPrimaryJournal.DataValueField = "Value";
            cmbPrimaryJournal.DataBind();
        }

        private void loadInvestorAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Investments") + " ORDER BY accountName ";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbInvestor.DataSource = ddItems;
            cmbInvestor.DataTextField = "DisplayName";
            cmbInvestor.DataValueField = "Value";
            cmbInvestor.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Investments')";
            //int accountID = p.getAccountID(query);

            //if (accountID != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbInvestor.DataSource = ddItems;
            //    cmbInvestor.DataTextField = "DisplayName";
            //    cmbInvestor.DataValueField = "Value";
            //    cmbInvestor.DataBind();
            //}
        }

        private void loadIndividualAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Personal Ledgers") + " ORDER BY accountName ";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbIndividual.DataSource = ddItems;
            cmbIndividual.DataTextField = "DisplayName";
            cmbIndividual.DataValueField = "Value";
            cmbIndividual.DataBind();
            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Cash in Hand')";
            //int accountID = p.getAccountID(query);

            //if (accountID != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbIndividual.DataSource = ddItems;
            //    cmbIndividual.DataTextField = "DisplayName";
            //    cmbIndividual.DataValueField = "Value";
            //    cmbIndividual.DataBind();
            //}
        }

        private void loadIncomeAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Income") + " ORDER BY accountName ";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbIncome.DataSource = ddItems;
            cmbIncome.DataTextField = "DisplayName";
            cmbIncome.DataValueField = "Value";
            cmbIncome.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName LIKE '%Income:%')";
            //List<int> accountIDs = p.getIntList(query);

            //if (accountIDs.Count > 0)
            //{
            //    string whereClause = p.getWhereClauseForIntegers(accountIDs, "parentAccountID");
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE ({0}) AND (isActive = '{1}') ORDER BY accountName", whereClause, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbIncome.DataSource = ddItems;
            //    cmbIncome.DataTextField = "DisplayName";
            //    cmbIncome.DataValueField = "Value";
            //    cmbIncome.DataBind();
            //}
        }

        private void loadExpensesAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Expense") + " ORDER BY accountName ";
            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbExpenses.DataSource = ddItems;
            cmbExpenses.DataTextField = "DisplayName";
            cmbExpenses.DataValueField = "Value";
            cmbExpenses.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName LIKE '%Expenses:%')";
            //List<int> accountIDs = p.getIntList(query);

            //if (accountIDs.Count > 0)
            //{
            //    string whereClause = p.getWhereClauseForIntegers(accountIDs, "parentAccountID");
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE ({0}) AND (isActive = '{1}') ORDER BY accountName", whereClause, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbExpenses.DataSource = ddItems;
            //    cmbExpenses.DataTextField = "DisplayName";
            //    cmbExpenses.DataValueField = "Value";
            //    cmbExpenses.DataBind();
            //}
        }

        private void loadCapitalAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Capital") + " ORDER BY accountName ";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbCapital.DataSource = ddItems;
            cmbCapital.DataTextField = "DisplayName";
            cmbCapital.DataValueField = "Value";
            cmbCapital.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Owners Capital')";
            //int accountID1 = p.getAccountID(query);
            //query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Owners Drawings')";
            //int accountID2 = p.getAccountID(query);

            //if (accountID1 != 0 && accountID2 != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0} OR parentAccountID = {1}) AND (isActive = '{2}') ORDER BY accountName", accountID1, accountID2, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbCapital.DataSource = ddItems;
            //    cmbCapital.DataTextField = "DisplayName";
            //    cmbCapital.DataValueField = "Value";
            //    cmbCapital.DataBind();
            //}
        }

        private void loadBanks()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Banks") + " ORDER BY accountName "; 

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbBank.DataSource = ddItems;
            cmbBank.DataTextField = "DisplayName";
            cmbBank.DataValueField = "Value";
            cmbBank.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Cash at Bank')";
            //int accountID = p.getAccountID(query);

            //if (accountID != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbBank.DataSource = ddItems;
            //    cmbBank.DataTextField = "DisplayName";
            //    cmbBank.DataValueField = "Value";
            //    cmbBank.DataBind();
            //}
        }

        protected void btnPayToCapital_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlCapital.Visible = pnlPayToCapital.Visible = true;
        }

        protected void btnGiveAwayLoan_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlLoans.Visible = pnlReceiveLoan.Visible = true;
        }

        protected void btnReceiveBackLoan_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlLoans.Visible = pnlPayLoan.Visible = true;
        }

        protected void btnReceiveFromCapital_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlCapital.Visible = pnlReceiveFromCapital.Visible = true;
        }

        protected void btnPayToInvestor_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlInvestor.Visible = pnlPayToInvestor.Visible = true;
        }

        protected void btnReceiveFromIvestors_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlInvestor.Visible = pnlReceiveFromInvestor.Visible = true;
        }

        protected void btnPayToIndividual_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlIndividual.Visible = pnlPayToIndividual.Visible = true;
        }

        protected void btnReceiveFromIndividual_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlIndividual.Visible = pnlReceiveFromIndividual.Visible = true;
        }

        protected void btnDepositInBank_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlBank.Visible = pnlDepositInBank.Visible = true;
        }

        protected void btnWithdrawalFromBank_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlBank.Visible = pnlWithdrawalFromBank.Visible = true;
        }

        protected void btnPayToSuppliers_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlSuppliers.Visible = pnlPayToSupplier.Visible = true;
        }

        protected void btnReceiveFromSupplier_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlSuppliers.Visible = pnlReceiveFromSupplier.Visible = true;
        }

        protected void btnPayExpenses_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlPayExpenses.Visible = true;
        }

        protected void btnReceiveIncome_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlDate.Visible = pnlCashAndBanks.Visible = pnlReceiveIncome.Visible = true;
        }

        protected void btnJournalEntry_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlJournalEntry.Visible = pnlJEAccountHierarcy.Visible = btnJournalEntry1.Visible = true;

            try
            {
                deleteTempTransactions();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void btnJournalEntryIDs_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePanels();
            pnlJournalEntry.Visible = pnlAccountIDsGrid.Visible = pnlJEAccountID.Visible = btnJournalEntryIDs1.Visible = true;
            loadAccountsGrid();

            try
            {
                deleteTempTransactions();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void cmbPrimary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0)
            {
                populateSecondaryAccountsList();
            }
        }

        protected void cmbSecondary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0 && cmbSecondary.SelectedIndex > 0)
            {
                populateTertiaryAccountsList();
            }
        }

        protected void cmbPrimaryJournal_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimaryJournal.SelectedIndex > 0)
            {
                populateSecondaryJournalAccountsList();
            }
        }

        private void populateSecondaryJournalAccountsList()
        {
            int parentAccountID = int.Parse(cmbPrimaryJournal.SelectedValue);

            string querySecondaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

            List<DDList> secondaryAccounts = new List<DDList>();
            secondaryAccounts = p.getDDList(querySecondaryAccounts);

            cmbSecondaryJournal.Items.Clear();
            cmbSecondaryJournal.DataSource = secondaryAccounts;
            cmbSecondaryJournal.DataTextField = "DisplayName";
            cmbSecondaryJournal.DataValueField = "Value";
            cmbSecondaryJournal.DataBind();
        }

        protected void cmbSecondaryJournal_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimaryJournal.SelectedIndex > 0 && cmbSecondaryJournal.SelectedIndex > 0)
            {
                populateTertiaryJournalAccountsList();
            }
        }

        private void populateTertiaryJournalAccountsList()
        {
            int parentAccountID = int.Parse(cmbSecondaryJournal.SelectedValue);

            string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

            List<DDList> tertiaryAccounts = new List<DDList>();
            tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

            cmbTertiaryJournal.Items.Clear();
            cmbTertiaryJournal.DataSource = tertiaryAccounts;
            cmbTertiaryJournal.DataTextField = "DisplayName";
            cmbTertiaryJournal.DataValueField = "Value";
            cmbTertiaryJournal.DataBind();
        }

        private void populateSecondaryAccountsList()
        {
            int parentAccountID = int.Parse(cmbPrimary.SelectedValue);

            string querySecondaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

            List<DDList> secondaryAccounts = new List<DDList>();
            secondaryAccounts = p.getDDList(querySecondaryAccounts);

            cmbSecondary.Items.Clear();
            cmbSecondary.DataSource = secondaryAccounts;
            cmbSecondary.DataTextField = "DisplayName";
            cmbSecondary.DataValueField = "Value";
            cmbSecondary.DataBind();
        }

        private void populateTertiaryAccountsList()
        {
            int parentAccountID = int.Parse(cmbSecondary.SelectedValue);

            string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

            List<DDList> tertiaryAccounts = new List<DDList>();
            tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

            cmbTertiary.Items.Clear();
            cmbTertiary.DataSource = tertiaryAccounts;
            cmbTertiary.DataTextField = "DisplayName";
            cmbTertiary.DataValueField = "Value";
            cmbTertiary.DataBind();
        }

        protected void btnPayToCapital1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbCapital.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbCapital.SelectedValue);
                    int creditAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"].ToString());
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"].ToString());
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnReceiveFromCapital1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbCapital.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int creditAccountID = int.Parse(cmbCapital.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnPayToInvestor1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbInvestor.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbInvestor.SelectedValue);
                    int creditAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity.ToString() + UoM + rate.ToString() + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity.ToString() + UoM + rate.ToString() + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnReceiveFromIvestors1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbInvestor.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int creditAccountID = int.Parse(cmbInvestor.SelectedValue);
                    int debitAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnPayToIndividual1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbIndividual.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbIndividual.SelectedValue);
                    int creditAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnReceiveFromIndividual1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbIndividual.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int creditAccountID = int.Parse(cmbIndividual.SelectedValue);
                    int debitAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnDepositInBank1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbBank.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbBank.SelectedValue);
                    int creditAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnWithdrawalFromBank1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbBank.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int creditAccountID = int.Parse(cmbBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnPayToSuppliers1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSupplier.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbSupplier.SelectedValue);
                    int creditAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnReceiveFromSupplier1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSupplier.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int creditAccountID = int.Parse(cmbSupplier.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnPayExpenses1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbExpenses.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbExpenses.SelectedValue);
                    int creditAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnGiveAwayLoan1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbLoans.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbLoans.SelectedValue);
                    int creditAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnReceiveBackLoan1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbLoans.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int creditAccountID = int.Parse(cmbLoans.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnReceiveIncome1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbIncome.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    populateGlobals();
                    int debitAccountID = int.Parse(cmbCashAndBank.SelectedValue);
                    int creditAccountID = int.Parse(cmbIncome.SelectedValue);
                    int transID = p.getTransactionID("tblGenAccountsTransactions");
                    string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + amount + description + "HeatFoodWithElectricity";
                    string debitAccountChecksum = p.EncryptStringToHex(debitString);
                    string creditAccountChecksum = p.EncryptStringToHex(creditString);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, amount, zero, description, debitAccountChecksum, Session["UserName"]);
                    string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', '{11}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, amount, description, creditAccountChecksum, Session["UserName"]);
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Transaction recorded successfully");
                    clearControls();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnJournalEntry1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimaryJournal.SelectedIndex > 0 && cmbSecondaryJournal.SelectedIndex > 0 && cmbTertiaryJournal.SelectedIndex > 0)
            {
                try
                {
                    populateGlobalsJournal();
                    int accountID = int.Parse(cmbTertiaryJournal.SelectedValue);
                    DateTime dateJournal = Validator.ValidateDate(txtDateJournal.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionJournal.Text);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtDebitJournal.Text, out debitAmountJournal);
                    Decimal.TryParse(txtCreditJournal.Text, out creditAmountJournal);

                    if (debitAmountJournal > 0 && creditAmountJournal > 0)
                    {
                        showWarningMessage("Entry seems wrong, check your amounts and try again.");
                    }
                    else
                    {
                        string chequeNumberJournal = p.FixString(txtChequeJournal.Text);

                        string startTransQuery = "Begin Transaction ";
                        string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, userName);
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        showSuccessMessage("Entry recorded successfully");

                        //clearControlsJournal();
                        loadTransactionGrid();
                        pnlTransactionGrid.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnJournalEntryIDs1_Click(object sender, EventArgs e)
        {
            clearLabels();
            int accountID = 0; 
            int.TryParse(txtAccountID.Text, out accountID);

            string query = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (ID = {0}) AND (isActive = 'true') AND (remarks = 'General')  ORDER BY accountName", accountID);

            if (p.ifRecordsExist(query))
            {
                try
                {
                    populateGlobalsJournal();
                    DateTime dateJournal = Validator.ValidateDate(txtDateJournal.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionJournal.Text);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtDebitJournal.Text, out debitAmountJournal);
                    Decimal.TryParse(txtCreditJournal.Text, out creditAmountJournal);

                    if (debitAmountJournal > 0 && creditAmountJournal > 0)
                    {
                        showWarningMessage("Entry seems wrong, check your amounts and try again.");
                    }
                    else
                    {
                        string chequeNumberJournal = p.FixString(txtChequeJournal.Text);

                        string startTransQuery = "Begin Transaction ";
                        string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, userName);
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        showSuccessMessage("Entry recorded successfully");

                        //clearControlsJournal();
                        loadTransactionGrid();
                        pnlTransactionGrid.Visible = true;
                        loadAccountsGrid();
                        txtAccountID.Text = "";
                        txtAccountID.Focus();
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                    loadAccountsGrid();
                }
            }
            else
            {
                showWarningMessage("Not a valid account number of General category.");
                loadAccountsGrid();
            }
        }

        protected void btnJournalEntry2_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                string userName = Session["UserName"].ToString();
                populateListOfAccounts(userName);
                populateParticulars();
                populateAccountNamesDictionary();
                populateSiteNamesDictionary();

                string startTransQuery = "Begin Transaction ";
                string midQuery = string.Empty;
                int transID = p.getTransactionID("tblGenAccountsTransactions");

                for (int i = 0; i < listTransaction.Count; i++)
                {
                    int siteID = 0;
                    int.TryParse(listTransaction[i].siteID.ToString(), out siteID);

                    int accountID = listTransaction[i].accountID;
                    string accountName = string.Empty;
                    accountNamesDictionary.TryGetValue(accountID, out accountName);

                    string siteName = string.Empty;
                    siteNameDictionary.TryGetValue(siteID, out siteName);

                    int voucherID = p.getVoucherID("tblGenAccountsTransactions");
                    string voucher = p.getVoucher(listTransaction[i], voucherID, companyName, companyAddress, companyContacts, companyWebsite, companyEmail, accountName, siteName, transID);
                    string debitString = transID.ToString() + accountID.ToString() + listTransaction[i].chequeNumber + listTransaction[i].quantity + listTransaction[i].UoM + listTransaction[i].rate + listTransaction[i].debit + listTransaction[i].credit + listTransaction[i].description + "HeatFoodWithElectricity";

                    string debitAccountChecksum = p.EncryptStringToHex(debitString);

                    string debitTransQuery = string.Format("   INSERT INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, voucherID, taxRate, isInHand, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', {11}, {12}, '{13}', '{14}')", transID, listTransaction[i].accountID, listTransaction[i].date, listTransaction[i].chequeNumber, listTransaction[i].quantity, listTransaction[i].UoM, listTransaction[i].rate, listTransaction[i].debit, listTransaction[i].credit, listTransaction[i].description, debitAccountChecksum, voucherID, listTransaction[i].taxRate, listTransaction[i].isInHand, userName);
                    string voucherQuery = string.Format("  INSERT INTO tblGenVouchers(voucherID, voucherText) VALUES ({0},'{1}')", voucherID, voucher);
                    midQuery += debitTransQuery;
                    midQuery += voucherQuery;
                }

                string endTransQuery = " Commit Transaction";
                string transQuery = startTransQuery + midQuery + endTransQuery;
                p.ExecuteQuery(transQuery);

                showSuccessMessage("Transaction recorded successfully");
                clearControls();
                deleteTempTransactions();
                loadTransactionGrid();
                loadAccountsGrid();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
                loadAccountsGrid();
            }
        }

        protected void btnJournalEntry3_Click(object sender, EventArgs e)
        {
            clearLabels();
            clearControls();

            try
            {
                deleteTempTransactions();

                loadTransactionGrid();
                pnlTransactionGrid.Visible = false;
                showSuccessMessage("Incomplete Transaction deleted successfully.");
                loadAccountsGrid();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
                loadAccountsGrid();
            }
        }

        private void populateListOfAccounts(string userName)
        {
            listTransaction.Clear();
            totalCredits = totalDebits = 0;

            string query = string.Format("SELECT accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description FROM tblGenAccountsTmpTransactions WHERE operator = '{0}'", userName);

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count ; i++)
                {
                    DataRow dr = dt.Rows[i];

                    AccountStructure AS = new AccountStructure();
                    AS.accountID = int.Parse(dr[0].ToString());
                    AS.date = DateTime.Parse(dr[1].ToString());
                    AS.chequeNumber = dr[2].ToString();
                    AS.quantity = Single.Parse(dr[3].ToString());
                    AS.UoM = dr[4].ToString();
                    AS.rate = Single.Parse(dr[5].ToString());
                    AS.debit = Decimal.Parse(dr[6].ToString());
                    AS.credit = Decimal.Parse(dr[7].ToString());
                    AS.description = dr[8].ToString();
                    listTransaction.Add(AS);

                    totalDebits += Decimal.Parse(dr[6].ToString());
                    totalCredits += Decimal.Parse(dr[7].ToString());
                }
            }

            if (totalCredits != totalDebits)
            {
                loadTransactionGrid();
                throw new Exception("Debits and credits are not equal. Please try again.");
            }
        }

        private void deleteTempTransactions()
        {
            string userName = Session["UserName"].ToString();

            string query = string.Format("DELETE FROM tblGenAccountsTmpTransactions WHERE operator = '{0}'", userName);
            p.ExecuteQuery(query);
        }

        private void populateGlobals()
        {
            date = Validator.ValidateDate(txtDate.Text, "Date");
            description = p.FixString(txtDescription.Text);
            Decimal.TryParse(txtAmount.Text, out amount);
            chequeNumber = p.FixString(txtCheequeNumber.Text);

            splitString(description);

            if (amount == 0)
            {
                throw new Exception("Amount must be entered for transaction.");
            }
        }

        private void splitString(string s)
        {
            try
            {
                if (description.Contains("@"))
                {
                    string[] strings = description.Split(' ');

                    quantity = float.Parse(strings[0]);
                    UoM = strings[1].ToLower();
                    rate = float.Parse(strings[3]);
                }
            }
            catch (Exception )
            {
                throw new Exception("Description does not seem to be in proper format. Please ensure proper spacing and format and try again.");
            }
        }

        private void populateGlobalsJournal()
        {
            splitString(txtDescriptionJournal.Text);
        }

        private void hidePanels()
        {
            pnlAccountHierarchy.Visible =
                pnlJEAccountHierarcy.Visible = 
                pnlJEAccountID.Visible = 
                pnlAccountIDsGrid.Visible =
                pnlTransactionGrid.Visible =
                btnJournalEntry1.Visible =
                btnJournalEntryIDs1.Visible =
                pnlCashAndBanks.Visible =
                pnlLoans.Visible =
                pnlReceiveLoan.Visible = 
                pnlPayLoan.Visible =
                pnlBank.Visible =
                pnlCapital.Visible =
                pnlDate.Visible =
                pnlDepositInBank.Visible =
                pnlIndividual.Visible =
                pnlInvestor.Visible =
                pnlPayExpenses.Visible =
                pnlPayToCapital.Visible =
                pnlPayToIndividual.Visible =
                pnlPayToInvestor.Visible =
                pnlPayToSupplier.Visible =
                pnlReceiveFromCapital.Visible =
                pnlReceiveFromIndividual.Visible =
                pnlReceiveFromInvestor.Visible =
                pnlReceiveIncome.Visible =
                pnlSuppliers.Visible =
                pnlReceiveFromSupplier.Visible =
                pnlJournalEntry.Visible = 
                pnlWithdrawalFromBank.Visible = false;
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
            txtDate.Enabled = txtDateJournal.Enabled = false;
        }

        private void clearControls()
        {
            txtAmount.Text = txtDescription.Text = txtCheequeNumber.Text =  "";
        }

        private void clearControlsJournal()
        {
            txtChequeJournal.Text = txtCreditJournal.Text = txtDebitJournal.Text = txtDescriptionJournal.Text = "";
        }

        private void populateSiteNamesDictionary()
        {
            siteNameDictionary.Clear();

            siteNameDictionary = p.getIntStringDictionary("SELECT id, siteName FROM tblGenSites");
        }

        private void populateAccountNamesDictionary()
        {
            accountNamesDictionary.Clear();

            accountNamesDictionary = p.getIntStringDictionary("SELECT id, accountName FROM tblGenAccounts");
        }

        private void populateParticulars()
        {
            companyEmail = companyAddress = companyContacts = companyWebsite = companyName = string.Empty;

            string query = "SELECT companyName, companyAddress, companyContacts, companyWebsite, companyEmail FROM tblGenActivationInfo";

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                companyName = dr[0].ToString();
                companyAddress = dr[1].ToString();
                companyContacts = dr[2].ToString();
                companyWebsite = dr[3].ToString();
                companyEmail = dr[4].ToString();
            }
        }

        protected void gridTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDb = (Label)e.Row.FindControl("lblDebit");
                Label lblCr = (Label)e.Row.FindControl("lblCredit");

                if (lblDb != null)
                {
                    totalDebitsForFooter += Decimal.Parse(lblDb.Text);
                }

                if (lblCr != null)
                {
                    totalCreditsForFooter += Decimal.Parse(lblCr.Text);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = totalDebitsForFooter.ToString("0,0.00");
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[2].Text = totalCreditsForFooter.ToString("0,0.00");
                e.Row.Cells[2].Font.Bold = true;
            }
        }
    }
}