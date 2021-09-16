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
    public partial class GeneralTransactions : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        IttefaqConstructionServices.Logic.Accounts a = new IttefaqConstructionServices.Logic.Accounts();
        DateTime date = DateTime.Now;
        //Single amount = 0.0f;
        //Single zero = 0.0f;tb
        string description = string.Empty;
        string chequeNumber = string.Empty;
        Single quantity = 0.0f;
        string UoM = string.Empty;
        Single rate = 0.0f;
        List<AccountStructure> listTransaction = new List<AccountStructure>();
        Decimal totalDebits = 0;
        Decimal totalCredits = 0;
        Decimal totalChequesInHand = 0;
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
                    hidePanels();
                    loadSecondaryAccounts();
                    loadSecondarySiteAccounts();
                    loadSecondarySiteAccountsForChequesInHand();
                    loadTertiaryAccountsForChequesInHand();
                    loadAccountsGrid();
                    loadSuppliers();
                    loadCashAndBankAccounts();
                    loadSites();
                    loadDebitTaxes();
                    loadCreditTaxes();
                    gridChequesInHandDataBind();
                    loadTransactionGrid();
                    deleteTempTransactions();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                    DateTime date = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
                    txtDateCredit.Text = txtDateDebit.Text = txtChequesInHandDate.Text = txtChequesInHandChequeDate.Text = p.fixDateWithMonthNames(date.Day, date.Month, date.Year);
                    //txtDateCredit.Text = txtDateDebit.Text = txtChequesInHandDate.Text = txtChequesInHandChequeDate.Text = p.fixDateWithMonthNames(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                    txtDateCredit.Enabled = txtDateDebit.Enabled = false;
                }
            }
            else
            {
                Response.Redirect(@"~\Default.aspx");
            }                
        }

        //otherFunctionality
        private void deleteTempTransactions()
        {
            string userName = Session["UserName"].ToString();

            string query = string.Format("DELETE FROM tblGenAccountsTmpTransactions WHERE operator = '{0}'", userName);
            p.ExecuteQuery(query);
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

        private void hidePanels()
        {
            pnlAccountIDsGrid.Visible =
                pnlDebitSide.Visible =
                pnlCreditSide.Visible =
                pnlChequesInHandIssue.Visible =
                pnlChequesInHandReceivedCreditSide.Visible =
                pnlChequesInHandReceiveDebitSide.Visible =
                pnlTaxesCredit.Visible =
                pnlTaxesCreditInputs.Visible =
                pnlTaxesDebit.Visible =
                pnlTaxesDebitInputs.Visible =
                pnlCashAndBankCredit.Visible =
                pnlCashAndBankDebit.Visible =
                pnlDateCredit.Visible =
                pnlDateDebit.Visible =
                pnlGeneralAccountsCredit.Visible =
                pnlGeneralAccountsDebit.Visible =
                pnlSiteAccountsCredit.Visible =
                pnlSiteAccountsDebit.Visible =
                pnlSiteCredit.Visible =
                pnlSiteDebit.Visible =
                pnlGeneralAccountsIDDebit.Visible = 
                pnlGeneralAccountsIDCredit.Visible = 
                pnlTransactionGrid.Visible = false;
        }

        private void hideButtons()
        {
            btnGeneralCredit.Visible = btnTaxesDebit.Visible = btnTaxesCredit.Visible = btnGeneralDebit.Visible = btnSiteCredit.Visible = btnSiteDebit.Visible = btnCashBookDebit.Visible = btnCashBookCredit.Visible = btnAccountIDDebit.Visible = btnAccountIDCredit.Visible = false;
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
            txtDateCredit.Enabled = txtDateDebit.Enabled = false;
        }

        private void clearControls()
        {
            txtChequesInHandChequeAmount.Text = txtChequesInHandChequeNumber.Text = txtChequesInHandDescription.Text = txtChequesInHandNameOfBank.Text = txtChequesInHandTaxRate.Text = "";
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

        private void populateGlobalsJournal(string description)
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

        protected void cmbChequesInHandSiteSecondaryAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbChequesInHandSiteSecondaryAccounts.SelectedIndex > 0)
            {
                int parentAccountID = int.Parse(cmbChequesInHandSiteSecondaryAccounts.SelectedValue);

                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbChequesInHandSitesTertiaryAccounts.Items.Clear();
                cmbChequesInHandSitesTertiaryAccounts.DataSource = tertiaryAccounts;
                cmbChequesInHandSitesTertiaryAccounts.DataTextField = "DisplayName";
                cmbChequesInHandSitesTertiaryAccounts.DataValueField = "Value";
                cmbChequesInHandSitesTertiaryAccounts.DataBind();
            }
        }

        protected void gridChequesInHand_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmt");

                if (lblAmount != null)
                {
                    totalChequesInHand += Decimal.Parse(lblAmount.Text);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total";
                e.Row.Cells[4].Font.Bold = true;

                e.Row.Cells[5].Text = totalChequesInHand.ToString("0,0.00");
                e.Row.Cells[5].Font.Bold = true;
            }
        }

        protected void cmbSecondaryAccountsDebit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecondaryAccountsDebit.SelectedIndex > 0)
            {
                int parentAccountID = int.Parse(cmbSecondaryAccountsDebit.SelectedValue);

                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbTertiaryAccountsDebit.Items.Clear();
                cmbTertiaryAccountsDebit.DataSource = tertiaryAccounts;
                cmbTertiaryAccountsDebit.DataTextField = "DisplayName";
                cmbTertiaryAccountsDebit.DataValueField = "Value";
                cmbTertiaryAccountsDebit.DataBind();
            }
        }

        protected void cmbSecondarySiteAccountsDebit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecondarySiteAccountsDebit.SelectedIndex > 0)
            {
                int parentAccountID = int.Parse(cmbSecondarySiteAccountsDebit.SelectedValue);

                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbTertiarySiteAccountsDebit.Items.Clear();
                cmbTertiarySiteAccountsDebit.DataSource = tertiaryAccounts;
                cmbTertiarySiteAccountsDebit.DataTextField = "DisplayName";
                cmbTertiarySiteAccountsDebit.DataValueField = "Value";
                cmbTertiarySiteAccountsDebit.DataBind();
            }
        }

        protected void cmbSecondaryAccountsCredit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecondaryAccountsCredit.SelectedIndex > 0)
            {
                int parentAccountID = int.Parse(cmbSecondaryAccountsCredit.SelectedValue);

                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbTertiaryAccountsCredit.Items.Clear();
                cmbTertiaryAccountsCredit.DataSource = tertiaryAccounts;
                cmbTertiaryAccountsCredit.DataTextField = "DisplayName";
                cmbTertiaryAccountsCredit.DataValueField = "Value";
                cmbTertiaryAccountsCredit.DataBind();
            }
        }

        protected void cmbSecondarySiteAccountsCredit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecondarySiteAccountsCredit.SelectedIndex > 0)
            {
                int parentAccountID = int.Parse(cmbSecondarySiteAccountsCredit.SelectedValue);

                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbTertiarySiteAccountsCredit.Items.Clear();
                cmbTertiarySiteAccountsCredit.DataSource = tertiaryAccounts;
                cmbTertiarySiteAccountsCredit.DataTextField = "DisplayName";
                cmbTertiarySiteAccountsCredit.DataValueField = "Value";
                cmbTertiarySiteAccountsCredit.DataBind();
            }
        }

        private void finalizeTransaction()
        {
            try
            {
                populateListOfAccounts();
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

                    string debitTransQuery = string.Format("   INSERT INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, voucherID, taxRate, isInHand, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', {11}, {12}, '{13}', '{14}')", transID, listTransaction[i].accountID, listTransaction[i].date, listTransaction[i].chequeNumber, listTransaction[i].quantity, listTransaction[i].UoM, listTransaction[i].rate, listTransaction[i].debit, listTransaction[i].credit, listTransaction[i].description, debitAccountChecksum, voucherID, listTransaction[i].taxRate, listTransaction[i].isInHand, Session["UserName"].ToString());
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
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
                loadAccountsGrid();
            }
        }

        protected void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            pnlSitesChequeInHand.Visible = chkSwap.Checked;
            pnlGeneralChequesInHand.Visible = !chkSwap.Checked;

            if (chkSwap.Checked)
            {
                cmbChequesInHandTertiaryAccounts.SelectedIndex = 0;
            }
            else
            {
                cmbChequesInHandSiteSecondaryAccounts.SelectedIndex = 0;
            }
        }

        //dataBindings
        private void loadTransactionGrid()
        {
            string userName = Session["UserName"].ToString();
            string query = string.Format("SELECT tblGenAccounts.accountName AS Account, tblGenAccountsTmpTransactions.debitAmount AS Debit, tblGenAccountsTmpTransactions.creditAmount AS Credit FROM tblGenAccountsTmpTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTmpTransactions.accountID = tblGenAccounts.id WHERE tblGenAccountsTmpTransactions.operator = '{0}'", userName);

            DataTable dt = p.GetDataTable(query);
            gridTransaction.DataSource = dt;
            gridTransaction.DataBind();
        }

        private void loadSuppliers()
        {
        }

        private void gridChequesInHandDataBind()
        {
            try
            {
                string query = "SELECT tblGenAccountsTransactions.isInHand AS Status, tblGenAccountsTransactions.chequeDate AS Date, tblGenAccounts.accountName AS [Party Name], tblGenAccountsTransactions.bankName AS Bank, tblGenAccountsTransactions.debitAmount AS Amount, tblGenAccountsTransactions.id FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.supplierID = tblGenAccounts.id WHERE (tblGenAccountsTransactions.isInHand = 1)";
                DataTable dt = p.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    gridChequesInHand.DataSource = dt;
                    gridChequesInHand.DataBind();

                    gridChequesInHand.UseAccessibleHeader = true;
                    gridChequesInHand.HeaderRow.TableSection = TableRowSection.TableHeader;

                    pnlChequesInHandIssue.Visible = true;
                }
                else
                {
                    pnlChequesInHandIssue.Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void loadTertiaryAccountsForChequesInHand()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Suppliers") + " ORDER BY accountName ";
            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbChequesInHandTertiaryAccounts.DataSource = ddItems;
            cmbChequesInHandTertiaryAccounts.DataTextField = "DisplayName";
            cmbChequesInHandTertiaryAccounts.DataValueField = "Value";
            cmbChequesInHandTertiaryAccounts.DataBind();

            cmbChequesInHandIssuedTo.DataSource = ddItems;
            cmbChequesInHandIssuedTo.DataTextField = "DisplayName";
            cmbChequesInHandIssuedTo.DataValueField = "Value";
            cmbChequesInHandIssuedTo.DataBind();

            cmbChequesInHandSuppliers.DataSource = ddItems;
            cmbChequesInHandSuppliers.DataTextField = "DisplayName";
            cmbChequesInHandSuppliers.DataValueField = "Value";
            cmbChequesInHandSuppliers.DataBind();
            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Suppliers')";
            //int accountID1 = p.getAccountID(query);

            //if (accountID1 != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID1, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbChequesInHandTertiaryAccounts.DataSource = ddItems;
            //    cmbChequesInHandTertiaryAccounts.DataTextField = "DisplayName";
            //    cmbChequesInHandTertiaryAccounts.DataValueField = "Value";
            //    cmbChequesInHandTertiaryAccounts.DataBind();

            //    cmbChequesInHandIssuedTo.DataSource = ddItems;
            //    cmbChequesInHandIssuedTo.DataTextField = "DisplayName";
            //    cmbChequesInHandIssuedTo.DataValueField = "Value";
            //    cmbChequesInHandIssuedTo.DataBind();

            //    cmbChequesInHandSuppliers.DataSource = ddItems;
            //    cmbChequesInHandSuppliers.DataTextField = "DisplayName";
            //    cmbChequesInHandSuppliers.DataValueField = "Value";
            //    cmbChequesInHandSuppliers.DataBind();
            //}
        }

        private void loadSecondarySiteAccountsForChequesInHand()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 5) ORDER BY accountName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbChequesInHandSiteSecondaryAccounts.Items.Clear();
            cmbChequesInHandSiteSecondaryAccounts.DataSource = ddItems;
            cmbChequesInHandSiteSecondaryAccounts.DataTextField = "DisplayName";
            cmbChequesInHandSiteSecondaryAccounts.DataValueField = "Value";
            cmbChequesInHandSiteSecondaryAccounts.DataBind();
        }

        private void loadCreditTaxes()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Taxes Payable')";
            int accountID1 = p.getAccountID(query);

            if (accountID1 != 0)
            {
                string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID1, true);

                List<DDList> ddItems = new List<DDList>();
                ddItems = p.getDDList(ddQuery);

                cmbTaxCredit.DataSource = ddItems;
                cmbTaxCredit.DataTextField = "DisplayName";
                cmbTaxCredit.DataValueField = "Value";
                cmbTaxCredit.DataBind();
            }
        }

        private void loadDebitTaxes()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Taxes Receivable')";
            int accountID1 = p.getAccountID(query);

            if (accountID1 != 0)
            {
                string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID1, true);

                List<DDList> ddItems = new List<DDList>();
                ddItems = p.getDDList(ddQuery);

                cmbTaxDebit.DataSource = ddItems;
                cmbTaxDebit.DataTextField = "DisplayName";
                cmbTaxDebit.DataValueField = "Value";
                cmbTaxDebit.DataBind();

                cmbChequesInHandTaxDebit.DataSource = ddItems;
                cmbChequesInHandTaxDebit.DataTextField = "DisplayName";
                cmbChequesInHandTaxDebit.DataValueField = "Value";
                cmbChequesInHandTaxDebit.DataBind();
            }
        }

        private void loadSites()
        {
            string ddQuery = "SELECT siteName, id FROM tblGenSites order by siteName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSiteDebit.Items.Clear();
            cmbSiteDebit.DataSource = ddItems;
            cmbSiteDebit.DataTextField = "DisplayName";
            cmbSiteDebit.DataValueField = "Value";
            cmbSiteDebit.DataBind();

            cmbSiteCredit.Items.Clear();
            cmbSiteCredit.DataSource = ddItems;
            cmbSiteCredit.DataTextField = "DisplayName";
            cmbSiteCredit.DataValueField = "Value";
            cmbSiteCredit.DataBind();

            cmbChequesInHandSites.Items.Clear();
            cmbChequesInHandSites.DataSource = ddItems;
            cmbChequesInHandSites.DataTextField = "DisplayName";
            cmbChequesInHandSites.DataValueField = "Value";
            cmbChequesInHandSites.DataBind();
        }

        private void loadSecondarySiteAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 5) OR (parentAccountID = 7) ORDER BY accountName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSecondarySiteAccountsDebit.Items.Clear();
            cmbSecondarySiteAccountsDebit.DataSource = ddItems;
            cmbSecondarySiteAccountsDebit.DataTextField = "DisplayName";
            cmbSecondarySiteAccountsDebit.DataValueField = "Value";
            cmbSecondarySiteAccountsDebit.DataBind();

            cmbSecondarySiteAccountsCredit.Items.Clear();
            cmbSecondarySiteAccountsCredit.DataSource = ddItems;
            cmbSecondarySiteAccountsCredit.DataTextField = "DisplayName";
            cmbSecondarySiteAccountsCredit.DataValueField = "Value";
            cmbSecondarySiteAccountsCredit.DataBind();
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

            cmbCashAndBankCredit.DataSource = ddItems;
            cmbCashAndBankCredit.DataTextField = "DisplayName";
            cmbCashAndBankCredit.DataValueField = "Value";
            cmbCashAndBankCredit.DataBind();

            cmbCashAndBankDebit.DataSource = ddItems;
            cmbCashAndBankDebit.DataTextField = "DisplayName";
            cmbCashAndBankDebit.DataValueField = "Value";
            cmbCashAndBankDebit.DataBind();

            //string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Cash in Hand')";
            //int accountID1 = p.getAccountID(query);

            //query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Cash at Bank')";
            //int accountID2 = p.getAccountID(query);

            //if (accountID1 != 0 && accountID2 != 0)
            //{
            //    string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0} OR parentAccountID = {1}) AND (isActive = '{2}') ORDER BY accountName", accountID1, accountID2, true);

            //    List<DDList> ddItems = new List<DDList>();
            //    ddItems = p.getDDList(ddQuery);

            //    cmbCashAndBankCredit.DataSource = ddItems;
            //    cmbCashAndBankCredit.DataTextField = "DisplayName";
            //    cmbCashAndBankCredit.DataValueField = "Value";
            //    cmbCashAndBankCredit.DataBind();

            //    cmbCashAndBankDebit.DataSource = ddItems;
            //    cmbCashAndBankDebit.DataTextField = "DisplayName";
            //    cmbCashAndBankDebit.DataValueField = "Value";
            //    cmbCashAndBankDebit.DataBind();
            //}
        }

        private void loadSecondaryAccounts()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 1) OR (parentAccountID = 2) OR (parentAccountID = 3) OR (parentAccountID = 4) OR (parentAccountID = 6) ORDER BY accountName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSecondaryAccountsDebit.Items.Clear();
            cmbSecondaryAccountsDebit.DataSource = ddItems;
            cmbSecondaryAccountsDebit.DataTextField = "DisplayName";
            cmbSecondaryAccountsDebit.DataValueField = "Value";
            cmbSecondaryAccountsDebit.DataBind();

            cmbSecondaryAccountsCredit.Items.Clear();
            cmbSecondaryAccountsCredit.DataSource = ddItems;
            cmbSecondaryAccountsCredit.DataTextField = "DisplayName";
            cmbSecondaryAccountsCredit.DataValueField = "Value";
            cmbSecondaryAccountsCredit.DataBind();
        }

        private void populateListOfAccounts()
        {
            listTransaction.Clear();
            totalCredits = totalDebits = 0;

            string query = "SELECT accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, taxRate, isInHand, supplierID, bankName, chequeDate FROM tblGenAccountsTmpTransactions";

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
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
                    AS.taxRate = Single.Parse(dr[9].ToString());
                    AS.isInHand = bool.Parse(dr[10].ToString());
                    AS.supplierID = int.Parse(dr[11].ToString());
                    AS.bankName = dr[12].ToString();
                    AS.chequeDate = DateTime.Parse(dr[13].ToString());
                    listTransaction.Add(AS);

                    totalDebits += Decimal.Parse(dr[6].ToString());
                    totalCredits += Decimal.Parse(dr[7].ToString());
                }
            }

            if (totalCredits != totalDebits)
            {
                throw new Exception("Debits and credits are not equal. Please try again.");
            }
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

        //buttonsFunctionality
        protected void btnReceiveInCashBookGeneral_Click(object sender, EventArgs e)
        {
            hideButtons();
            hidePanels();
            pnlDebitSide.Visible = pnlCreditSide.Visible = true;
            pnlCashAndBankDebit.Visible = pnlTaxesDebit.Visible = pnlTaxesDebitInputs.Visible = btnTaxesDebit.Visible = pnlDateDebit.Visible = pnlGeneralAccountsCredit.Visible = pnlDateCredit.Visible = btnCashBookDebit.Visible = btnGeneralCredit.Visible = true;
            lblHeading.Text = "Receive in Cash Book:";
        }

        protected void btnPayFromCashBookGeneral_Click(object sender, EventArgs e)
        {
            hideButtons();
            hidePanels();
            pnlDebitSide.Visible = pnlCreditSide.Visible = true;
            lblHeading.Text = "Pay from Cash Book:";
            pnlCashAndBankCredit.Visible = pnlTaxesCredit.Visible = pnlTaxesCreditInputs.Visible = btnTaxesCredit.Visible = pnlDateDebit.Visible = pnlGeneralAccountsDebit.Visible = pnlDateCredit.Visible = btnGeneralDebit.Visible = btnCashBookCredit.Visible = true;
        }

        protected void btnReceiveInCashBookSite_Click(object sender, EventArgs e)
        {
            hideButtons();
            hidePanels();
            lblHeading.Text = "Receive in Cash Book from Sites:";
            pnlDebitSide.Visible = pnlCreditSide.Visible = true;
            pnlCashAndBankDebit.Visible = pnlTaxesDebit.Visible = pnlTaxesDebitInputs.Visible = btnTaxesDebit.Visible = pnlDateDebit.Visible = pnlSiteCredit.Visible = pnlSiteAccountsCredit.Visible = pnlDateCredit.Visible = btnCashBookDebit.Visible = btnSiteCredit.Visible = true;
        }

        protected void btnPayFromCashBookSite_Click(object sender, EventArgs e)
        {
            hideButtons();
            hidePanels();
            lblHeading.Text = "Pay from Cash Book to Sites:";
            pnlDebitSide.Visible = pnlCreditSide.Visible = true;
            pnlCashAndBankCredit.Visible = pnlTaxesCredit.Visible = pnlTaxesCreditInputs.Visible = btnTaxesCredit.Visible = pnlDateDebit.Visible = pnlSiteDebit.Visible = pnlSiteAccountsDebit.Visible = pnlDateCredit.Visible = btnCashBookCredit.Visible = btnSiteDebit.Visible = true;
        }

        protected void btnJournalEntry_Click(object sender, EventArgs e)
        {
            hideButtons();
            hidePanels();
            pnlGeneralAccountsDebit.Visible = 
                pnlDateDebit.Visible = 
                pnlGeneralAccountsCredit.Visible = 
                pnlDateCredit.Visible = 
                btnGeneralDebit.Visible = 
                btnGeneralCredit.Visible = 
                pnlTaxesDebit.Visible = 
                pnlTaxesDebitInputs.Visible = 
                pnlTaxesCredit.Visible = 
                pnlTaxesCreditInputs.Visible = 
                btnTaxesDebit.Visible = 
                btnTaxesCredit.Visible = true;
            pnlDebitSide.Visible = pnlCreditSide.Visible = true;
            lblHeading.Text = "Journal Entry using Menus:";
        }

        protected void btnJournalEntryIDs_Click(object sender, EventArgs e)
        {
            hideButtons();
            hidePanels();
            pnlAccountIDsGrid.Visible = 
                pnlGeneralAccountsIDDebit.Visible = 
                pnlDateDebit.Visible = 
                pnlGeneralAccountsIDCredit.Visible = 
                pnlDateCredit.Visible = 
                btnAccountIDDebit.Visible = 
                btnAccountIDCredit.Visible =
                pnlTaxesDebit.Visible =
                pnlTaxesDebitInputs.Visible =
                pnlTaxesCredit.Visible =
                pnlTaxesCreditInputs.Visible =
                btnTaxesDebit.Visible =
                btnTaxesCredit.Visible = true;
            pnlDebitSide.Visible = pnlCreditSide.Visible = true;
            lblHeading.Text = "Journal Entries using IDs:";
        }

        protected void btnGeneralDebit_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbSecondaryAccountsDebit.SelectedIndex > 0 && cmbTertiaryAccountsDebit.SelectedIndex > 0)
            {
                try
                {
                    int accountID = 0;
                    int.TryParse(cmbTertiaryAccountsDebit.SelectedValue.ToString(), out accountID);
                    DateTime dateJournal = Validator.ValidateDate(txtDateDebit.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionDebit.Text);
                    populateGlobalsJournal(descriptionJournal);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtAmountDebit.Text, out debitAmountJournal);

                    string chequeNumberJournal = p.FixString(txtChequeNumberDebit.Text);

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, userName);

                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                    loadAccountsGrid();
                }
            }

            else
            {
                showWarningMessage("Please select proper values and try again.");
                loadAccountsGrid();
            }
        }

        protected void btnGeneralCredit_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbSecondaryAccountsCredit.SelectedIndex > 0 && cmbTertiaryAccountsCredit.SelectedIndex > 0)
            {
                try
                {
                    int accountID = 0;
                    int.TryParse(cmbTertiaryAccountsCredit.SelectedValue.ToString(), out accountID);
                    DateTime dateJournal = Validator.ValidateDate(txtDateCredit.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionCredit.Text);
                    populateGlobalsJournal(descriptionJournal);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtAmountCredit.Text, out creditAmountJournal);

                    string chequeNumberJournal = p.FixString(txtChequeNumberCredit.Text);

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}','{10}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, userName);

                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                    loadAccountsGrid();
                }
            }

            else
            {
                showWarningMessage("Please select proper values and try again.");
                loadAccountsGrid();
            }
        }

        protected void btnSiteDebit_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbSiteDebit.SelectedIndex > 0 && cmbSecondarySiteAccountsDebit.SelectedIndex > 0 && cmbTertiarySiteAccountsDebit.SelectedIndex > 0)
            {
                try
                {
                    int siteID = 0;
                    int.TryParse(cmbSiteDebit.SelectedValue.ToString(), out siteID);
                    int accountID = 0;
                    int.TryParse(cmbTertiarySiteAccountsDebit.SelectedValue.ToString(), out accountID);
                    DateTime dateJournal = Validator.ValidateDate(txtDateDebit.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionDebit.Text);
                    populateGlobalsJournal(descriptionJournal);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtAmountDebit.Text, out debitAmountJournal);
                    string chequeNumberJournal = p.FixString(txtChequeNumberDebit.Text);

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10},'{11}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, siteID, userName);
                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                    loadAccountsGrid();
                }
            }

            else
            {
                showWarningMessage("Please select proper values and try again.");
                loadAccountsGrid();
            }
        }

        protected void btnSiteCredit_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbSiteCredit.SelectedIndex > 0 && cmbSecondarySiteAccountsCredit.SelectedIndex > 0 && cmbTertiarySiteAccountsCredit.SelectedIndex > 0)
            {
                try
                {
                    int siteID = 0;
                    int.TryParse(cmbSiteCredit.SelectedValue.ToString(), out siteID);
                    int accountID = 0;
                    int.TryParse(cmbTertiarySiteAccountsCredit.SelectedValue.ToString(), out accountID);
                    DateTime dateJournal = Validator.ValidateDate(txtDateCredit.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionCredit.Text);
                    populateGlobalsJournal(descriptionJournal);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtAmountCredit.Text, out creditAmountJournal);
                    string chequeNumberJournal = p.FixString(txtChequeNumberCredit.Text);

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, siteID, userName);
                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                    loadAccountsGrid();
                }
            }

            else
            {
                showWarningMessage("Please select proper values and try again.");
                loadAccountsGrid();
            }
        }

        protected void btnCashBookDebit_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbCashAndBankDebit.SelectedIndex > 0)
            {
                try
                {
                    int accountID = 0;
                    int.TryParse(cmbCashAndBankDebit.SelectedValue.ToString(), out accountID);
                    DateTime dateJournal = Validator.ValidateDate(txtDateDebit.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionDebit.Text);
                    populateGlobalsJournal(descriptionJournal);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtAmountDebit.Text, out debitAmountJournal);
                    string chequeNumberJournal = p.FixString(txtChequeNumberDebit.Text);

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, userName);
                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                    loadAccountsGrid();
                }
            }

            else
            {
                showWarningMessage("Please select proper values and try again.");
                loadAccountsGrid();
            }
        }

        protected void btnCashBookCredit_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbCashAndBankCredit.SelectedIndex > 0)
            {
                try
                {
                    int accountID = 0;
                    int.TryParse(cmbCashAndBankCredit.SelectedValue.ToString(), out accountID);
                    DateTime dateJournal = Validator.ValidateDate(txtDateCredit.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionCredit.Text);
                    populateGlobalsJournal(descriptionJournal);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtAmountCredit.Text, out creditAmountJournal);
                    string chequeNumberJournal = p.FixString(txtChequeNumberCredit.Text);

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, userName);
                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                    loadAccountsGrid();
                }
            }

            else
            {
                showWarningMessage("Please select proper values and try again.");
                loadAccountsGrid();
            }
        }

        protected void btnAccountIDDebit_Click(object sender, EventArgs e)
        {
            clearLabels();
            int accountID = 0;
            int.TryParse(txtAccountIDDebit.Text, out accountID);

            string query = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (ID = {0}) AND (isActive = 'true') AND (remarks = 'General')  ORDER BY accountName", accountID);

            if (p.ifRecordsExist(query))
            {
                try
                {
                    DateTime dateJournal = Validator.ValidateDate(txtDateDebit.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionDebit.Text);
                    populateGlobalsJournal(descriptionJournal);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtAmountDebit.Text, out debitAmountJournal);

                    string chequeNumberJournal = p.FixString(txtChequeNumberDebit.Text);

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}','{10}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, userName);

                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
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

        protected void btnAccountIDCredit_Click(object sender, EventArgs e)
        {
            clearLabels();
            int accountID = 0;
            int.TryParse(txtAccountIDCredit.Text, out accountID);

            string query = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (ID = {0}) AND (isActive = 'true') AND (remarks = 'General')  ORDER BY accountName", accountID);

            if (p.ifRecordsExist(query))
            {
                try
                {
                    DateTime dateJournal = Validator.ValidateDate(txtDateCredit.Text, "Date");
                    string descriptionJournal = p.FixString(txtDescriptionCredit.Text);
                    populateGlobalsJournal(descriptionJournal);
                    Decimal debitAmountJournal = 0;
                    Decimal creditAmountJournal = 0;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    Decimal.TryParse(txtAmountCredit.Text, out creditAmountJournal);

                    string chequeNumberJournal = p.FixString(txtChequeNumberCredit.Text);

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}','{10}')", accountID, dateJournal, chequeNumberJournal, quantity, UoM, rate, debitAmountJournal, creditAmountJournal, descriptionJournal, accountChecksum, userName);

                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
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
                a.finalizeTransaction(Session["UserName"].ToString());
                showSuccessMessage("Transaction recorded successfully");
                clearControls();
                loadTransactionGrid();
                loadAccountsGrid();
            }
            catch (Exception ex)
            {
                loadTransactionGrid();
                showWarningMessage(ex.Message);
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

        protected void btnTaxesDebit_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbTaxDebit.SelectedIndex > 0)
            {
                try
                {
                    int accountID = int.Parse(cmbTaxDebit.SelectedValue);
                    string txRate = p.FixString(txtTaxesRateDebit.Text);
                    Single taxRate = 0f;
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    txRate = txRate.Replace('%', ' ').Trim();
                    Single.TryParse(txRate, out taxRate);
                    Decimal.TryParse(txtTaxesDebitAmount.Text, out debitAmount);
                    string description = p.FixString(txtTaxesDebitDescription.Text);
                    DateTime date = DateTime.Parse(txtDateDebit.Text);
                    string chequeNumberJournal = string.Empty;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, taxRate, isInHand, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}',{10},'{11}','{12}')", accountID, date, chequeNumberJournal, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, taxRate, false, userName);
                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("Please select a propert tax account and try again.");
            }
        }

        protected void btnTaxesCredit_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbTaxCredit.SelectedIndex > 0)
            {
                try
                {
                    int accountID = int.Parse(cmbTaxCredit.SelectedValue);
                    string txRate = p.FixString(txtTaxesRateCredit.Text);
                    Single taxRate = 0f;
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    txRate = txRate.Replace('%', ' ').Trim();
                    Single.TryParse(txRate, out taxRate);
                    Decimal.TryParse(txtTaxesCreditAmount.Text, out creditAmount);
                    string description = p.FixString(txtTaxesCreditDescription.Text);
                    DateTime date = DateTime.Parse(txtDateCredit.Text);
                    string chequeNumberJournal = string.Empty;
                    string accountChecksum = string.Empty;
                    string userName = Session["UserName"].ToString();

                    string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, taxRate, isInHand, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}',{10},'{11}','{12}')", accountID, date, chequeNumberJournal, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, taxRate, false, userName);
                    p.ExecuteTransactionQuery(debitTransQuery);

                    showSuccessMessage("Entry recorded successfully");

                    loadTransactionGrid();
                    pnlTransactionGrid.Visible = true;
                    loadAccountsGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("Please select a propert tax account and try again.");
            }

        }

        protected void btnChequesInHand_Click(object sender, EventArgs e)
        {
            hideButtons();
            hidePanels();
            pnlChequesInHandReceiveDebitSide.Visible = pnlChequesInHandReceivedCreditSide.Visible = pnlChequesInHandIssue.Visible = true;
            gridChequesInHandDataBind();
            totalChequesInHand = 0;
            lblHeading.Text = "Cheques Manager:";
        }

        protected void btnReceiveCheques_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {                
                if (cmbChequesInHandSites.SelectedIndex > 0 && cmbChequesInHandTertiaryAccounts.SelectedIndex > 0)
                {
                    showWarningMessage("Select either site accounts or general accounts, please don't select both of them together.");
                }

                else if (cmbChequesInHandSites.SelectedIndex > 0 && cmbChequesInHandSiteSecondaryAccounts.SelectedIndex > 0 && cmbChequesInHandSitesTertiaryAccounts.SelectedIndex > 0)
                {
                    if (cmbChequesInHandSuppliers.SelectedIndex > 0)
                    {
                        int siteID = int.Parse(cmbChequesInHandSites.SelectedValue);
                        int chequesInHandID = p.getIntValue("SELECT id FROM tblGenAccounts WHERE (accountName = 'Cheques in Hand')");
                        int accountID = int.Parse(cmbChequesInHandSitesTertiaryAccounts.SelectedValue);
                        DateTime date = DateTime.Parse(txtChequesInHandDate.Text);
                        DateTime chequeDate = DateTime.Parse(txtChequesInHandChequeDate.Text);
                        int partyID = int.Parse(cmbChequesInHandSuppliers.SelectedValue);
                        Decimal amount = 0;
                        Single taxRate = 0f;
                        Decimal fullAmount = 0;
                        Decimal.TryParse(txtChequesInHandChequeAmount.Text, out amount);
                        Single.TryParse(txtChequesInHandTaxRate.Text.Replace('%', ' ').Trim(), out taxRate);

                        if (taxRate == 0.0f)
                        {
                            fullAmount = amount;
                        }
                        else if (taxRate > 0f)
                        {
                            fullAmount = (Decimal) ((float)amount*100 / (100 - taxRate));
                        }
                        else
                        {
                            throw new Exception("Tax rate can't be negative.");
                        }

                        if (amount == fullAmount)
                        {
                            string description = p.FixString(txtChequesInHandDescription.Text);
                            string bankName = p.FixString(txtChequesInHandNameOfBank.Text);
                            string chequeNumber = p.FixString(txtChequesInHandChequeNumber.Text);
                            int zero = 0;
                            string userName = Session["UserName"].ToString();

                            string deleteTransaction = " DELETE FROM tblGenAccountsTmpTransactions";
                            string debitTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions (siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator, remarks, taxRate, isInHand, supplierID, bankName, chequeDate) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}')", zero, chequesInHandID, date, chequeNumber, zero, string.Empty, zero, amount, zero, description, string.Empty, userName, string.Empty, taxRate, true, partyID, bankName, chequeDate);
                            string creditTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions(siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator, chequeDate) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}', '{12}')", siteID, accountID, date, chequeNumber, zero, string.Empty, zero, zero, amount, string.Empty, string.Empty, userName, new DateTime(2000, 1, 1));
                            string query = deleteTransaction + debitTransaction + creditTransaction;

                            p.ExecuteTransactionQuery(query);

                            a.finalizeTransaction(Session["UserName"].ToString());
                            showSuccessMessage("Entry recorded successfully.");
                            gridChequesInHandDataBind();
                        }
                        else
                        {
                            if (cmbChequesInHandTaxDebit.SelectedIndex > 0)
                            {
                                int taxAccountID = int.Parse(cmbChequesInHandTaxDebit.SelectedValue);
                                Decimal taxAmount = fullAmount - amount;

                                string description = p.FixString(txtChequesInHandDescription.Text);
                                string bankName = p.FixString(txtChequesInHandNameOfBank.Text);
                                string chequeNumber = p.FixString(txtChequesInHandChequeNumber.Text);
                                int zero = 0;
                                string userName = Session["UserName"].ToString();

                                string deleteTransaction = " DELETE FROM tblGenAccountsTmpTransactions";
                                string debitChequesInHandTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions (siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator, remarks, taxRate, isInHand, supplierID, bankName, chequeDate) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}')", zero, chequesInHandID, date, chequeNumber, zero, string.Empty, zero, amount, zero, description, string.Empty, userName, string.Empty, taxRate, true, partyID, bankName, chequeDate);
                                string debitTaxAccountTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions(siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator, chequeDate) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}', '{12}')", zero, accountID, date, chequeNumber, zero, string.Empty, zero, taxAmount, zero, string.Empty, string.Empty, userName, new DateTime(2000, 1, 1));
                                string creditTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions(siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator, chequeDate) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}', '{12}')", siteID, accountID, date, chequeNumber, zero, string.Empty, zero, zero, fullAmount, string.Empty, string.Empty, userName, new DateTime(2000, 1, 1));
                                string query = deleteTransaction + debitChequesInHandTransaction + debitTaxAccountTransaction + creditTransaction;

                                p.ExecuteTransactionQuery(query);

                                a.finalizeTransaction(Session["UserName"].ToString());
                                showSuccessMessage("Entry recorded successfully.");
                                gridChequesInHandDataBind();

                            }
                            else
                            {
                                throw new Exception("Please select a tax account.");
                            }
                        }

                    }
                    else
                    {
                        showWarningMessage("Please select proper supplier.");
                    }

                }
                
                else if (cmbChequesInHandTertiaryAccounts.SelectedIndex > 0)
                {
                    if (cmbChequesInHandSuppliers.SelectedIndex > 0)
                    {
                        int siteID = 0;
                        int chequesInHandID = p.getIntValue("SELECT id FROM tblGenAccounts WHERE (accountName = 'Cheques in Hand')");
                        int accountID = int.Parse(cmbChequesInHandTertiaryAccounts.SelectedValue);
                        DateTime date = DateTime.Parse(txtChequesInHandDate.Text);
                        DateTime chequeDate = DateTime.Parse(txtChequesInHandChequeDate.Text);
                        int partyID = int.Parse(cmbChequesInHandSuppliers.SelectedValue);
                        Decimal amount = 0;
                        Single taxRate = 0f;
                        Decimal fullAmount = 0;
                        Decimal.TryParse(txtChequesInHandChequeAmount.Text, out amount);
                        Single.TryParse(txtChequesInHandTaxRate.Text.Replace('%', ' ').Trim(), out taxRate);

                        if (taxRate == 0.0f)
                        {
                            fullAmount = amount;
                        }
                        else if (taxRate > 0f)
                        {
                            fullAmount = (Decimal) ((float)amount * 100 / (100 - taxRate));
                        }
                        else
                        {
                            throw new Exception("Tax rate can't be negative.");
                        }

                        if (fullAmount == amount)
                        {
                            string description = p.FixString(txtChequesInHandDescription.Text);
                            string bankName = p.FixString(txtChequesInHandNameOfBank.Text);
                            string chequeNumber = p.FixString(txtChequesInHandChequeNumber.Text);
                            int zero = 0;
                            string userName = Session["UserName"].ToString();

                            string debitTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions(siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator, remarks, taxRate, isInHand, supplierID, bankName, chequeDate) 
                                                                                                            VALUES ({0},    {1},    '{2}','{3}',        {4},    '{5}',  {6},    {7},        {8},        '{9}',          '{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}')",zero, chequesInHandID, date, chequeNumber, zero, string.Empty, zero, amount, zero, description, string.Empty, userName, string.Empty, taxRate, true, partyID, bankName, chequeDate);
                            string creditTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions(siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}')", siteID, accountID, date, chequeNumber, zero, string.Empty, zero, zero, amount, description, string.Empty, userName);
                            string query = debitTransaction + creditTransaction;

                            p.ExecuteTransactionQuery(query);

                            a.finalizeTransaction(Session["UserName"].ToString());
                            showSuccessMessage("Entry recorded successfully.");
                            gridChequesInHandDataBind();
                        }
                        else
                        {
                            if (cmbChequesInHandTaxDebit.SelectedIndex > 0)
                            {
                                int taxAccountID = int.Parse(cmbChequesInHandTaxDebit.SelectedValue);
                                Decimal taxAmount = fullAmount - amount;

                                string description = p.FixString(txtChequesInHandDescription.Text);
                                string bankName = p.FixString(txtChequesInHandNameOfBank.Text);
                                string chequeNumber = p.FixString(txtChequesInHandChequeNumber.Text);
                                int zero = 0;
                                string userName = Session["UserName"].ToString();

                                string debitChequesInHandTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions(siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator, remarks, taxRate, isInHand, supplierID, bankName, chequeDate) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}')", zero, chequesInHandID, date, chequeNumber, zero, string.Empty, zero, amount, zero, description, string.Empty, userName, string.Empty, taxRate, true, partyID, bankName, chequeDate);
                                string debitTaxesTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions(siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}')", siteID, taxAccountID, date, chequeNumber, zero, string.Empty, zero, taxAmount, zero, description, string.Empty, userName);
                                string creditTransaction = string.Format(@" INSERT INTO tblGenAccountsTmpTransactions(siteID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}','{11}')", siteID, accountID, date, chequeNumber, zero, string.Empty, zero, zero, fullAmount, description, string.Empty, userName);
                                string query = debitChequesInHandTransaction + debitTaxesTransaction + creditTransaction;

                                p.ExecuteTransactionQuery(query);

                                a.finalizeTransaction(Session["UserName"].ToString());
                                showSuccessMessage("Entry recorded successfully.");
                                gridChequesInHandDataBind();
                            }
                            else
                            {
                                throw new Exception("Please select a tax account.");
                            }
                        }

                    }
                    else
                    {
                        showWarningMessage("Please select proper supplier.");
                    }
                }

                else
                {
                    showWarningMessage("Please select proper dropdowns to show from where the cheque was received.");
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void btnChequesInHandIssue_Click(object sender, EventArgs e)
        {
            clearLabels();
            string listOfCheques = string.Empty;
            string whereClause = string.Empty;
            Decimal amount = 0;

            if (cmbChequesInHandIssuedTo.SelectedIndex > 0)
            {
                try
                {
                    for (int i = 0; i < gridChequesInHand.Rows.Count; i++)
                    {
                        CheckBox chkSt = (CheckBox)gridChequesInHand.Rows[i].FindControl("chkInHand");

                        if (!chkSt.Checked)
                        {
                            Label lblID = (Label)gridChequesInHand.Rows[i].FindControl("lbliD");
                            Label lblAmount = (Label)gridChequesInHand.Rows[i].FindControl("lblAmt");
                            Decimal chequeAmount = 0;
                            int tranID = 0;

                            Decimal.TryParse(lblAmount.Text, out chequeAmount);
                            int.TryParse(lblID.Text, out tranID);

                            amount += chequeAmount;

                            if (whereClause.Equals(string.Empty))
                            {
                                whereClause += "id = " + tranID.ToString() + " ";
                            }
                            else
                            {
                                whereClause += " OR id = " + tranID.ToString() + " ";
                            }

                            listOfCheques += tranID.ToString() + " " + " Amount: " + chequeAmount.ToString() + " . ";
                        }
                    }

                    int debitAccountID = int.Parse(cmbChequesInHandIssuedTo.SelectedValue);
                    int creditAccountID = p.getIntValue("SELECT id FROM tblGenAccounts WHERE (accountName = 'Cheques in Hand')");
                    DateTime dateJournal = Validator.ValidateDate(txtChequesInHandDate.Text, "Date");
                    string descriptionJournal = p.FixString(listOfCheques);
                    Decimal debitAmountJournal = amount;
                    Decimal creditAmountJournal = amount;
                    string accountChecksum = string.Empty;
                    Decimal zero = 0;

                    if (!whereClause.Equals(string.Empty))
                    {

                        string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}')", debitAccountID, dateJournal, string.Empty, quantity, UoM, rate, debitAmountJournal, zero, descriptionJournal, accountChecksum);
                        string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}')", creditAccountID, dateJournal, string.Empty, quantity, UoM, rate, zero, creditAmountJournal, descriptionJournal, accountChecksum);
                        string updateQuery = string.Format("  UPDATE tblGenAccountsTransactions SET isInHand = '{0}', chequeIssuedTo = {1}  WHERE {2}", false, int.Parse(cmbChequesInHandIssuedTo.SelectedValue), whereClause);

                        string query = debitTransQuery + creditTransQuery + updateQuery;

                        p.ExecuteTransactionQuery(query);

                        a.finalizeTransaction(Session["UserName"].ToString());

                        showSuccessMessage("Transaction recorded successfully.");
                        gridChequesInHandDataBind();
                    }

                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }

            }
            else
            {
                showWarningMessage("Please select a supplier.");
                gridChequesInHandDataBind();
            }
        }
    }
}