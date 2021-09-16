using ClosedXML.Excel;
using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Sites
{
    public partial class SiteTransactions : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        OleDbConnection OConnection = new OleDbConnection();
        List<AccountStructure> listTransaction = new List<AccountStructure>();
        List<ExcelData> data = new List<ExcelData>();
        Dictionary<int, string> accountNamesDictionary = new Dictionary<int, string>();
        Dictionary<int, string> siteNameDictionary = new Dictionary<int, string>();
        Dictionary<int, bool> generalAccounts = new Dictionary<int, bool>();
        Dictionary<int, bool> siteAccounts = new Dictionary<int, bool>();
        Dictionary<int, bool> activeSites = new Dictionary<int, bool>();
        Decimal totalDebits = 0;
        Decimal totalCredits = 0;
        Single quantity = 0.0f;
        string UoM = string.Empty;
        Single rate = 0.0f;
        string companyName = string.Empty;
        string companyAddress = string.Empty;
        string companyContacts = string.Empty;
        string companyWebsite = string.Empty; 
        string companyEmail = string.Empty;
        Decimal totalDebitsForFooter = 0;
        Decimal totalCreditsForFooter = 0;
        Decimal miscLiabAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect(@"~\Pages\Membership\Login.aspx");
            }
            if (p.Authenticated(Session["UserName"].ToString(), "sites, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadSites();
                    loadAccountsGrid();
                    loadPrimaryGenAccounts();
                    loadPrimarySiteAccounts();
                    deleteTempTransactions();
                    loadSuppliersAccounts();
                    loadCashAndBankAccounts();
                    loadSiteInputs();
                    loadSiteOutputs();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                    DateTime date = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
                    txtDate.Text = p.fixDateWithMonthNames(date.Day, date.Month, date.Year);
                    txtDate.Enabled = false;
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadAccountsGrid()
        {
            string query = "SELECT tblGenAccounts.id AS id, tblGenAccounts.accountName AS accountName, tblGenAccounts_1.accountName AS mainHead FROM tblGenAccounts INNER JOIN tblGenAccounts AS tblGenAccounts_1 ON tblGenAccounts.parentAccountID = tblGenAccounts_1.id WHERE (tblGenAccounts.tertiaryAccountPrefix <> '000') AND (tblGenAccounts.isActive = 'true') ORDER BY tblGenAccounts.accountName";
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                gridAccounts.DataSource = dt;
                gridAccounts.DataBind();
                gridAccounts.UseAccessibleHeader = true;
                gridAccounts.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void loadSiteOutputs()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Site Outputs')";
            int accountID = p.getAccountID(query);

            if (accountID != 0)
            {
                string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

                List<DDList> ddItems = new List<DDList>();
                ddItems = p.getDDList(ddQuery);

                cmbSecondaryOutput.DataSource = ddItems;
                cmbSecondaryOutput.DataTextField = "DisplayName";
                cmbSecondaryOutput.DataValueField = "Value";
                cmbSecondaryOutput.DataBind();
            }
        }

        private void loadSiteInputs()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Site Inputs')";
            int accountID = p.getAccountID(query);

            if (accountID != 0)
            {
                string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

                List<DDList> ddItems = new List<DDList>();
                ddItems = p.getDDList(ddQuery);

                cmbSecondaryInput.DataSource = ddItems;
                cmbSecondaryInput.DataTextField = "DisplayName";
                cmbSecondaryInput.DataValueField = "Value";
                cmbSecondaryInput.DataBind();
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
        }

        private void loadPrimarySiteAccounts()
        {
            string queryPrimaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 0) AND (isActive = '{0}') AND (accountName LIKE '%Site%') ORDER BY accountName", true);

            List<DDList> primaryAccounts = new List<DDList>();
            primaryAccounts = p.getDDList(queryPrimaryAccounts);

            cmbSitePrimary.Items.Clear();
            cmbSitePrimary.DataSource = primaryAccounts;
            cmbSitePrimary.DataTextField = "DisplayName";
            cmbSitePrimary.DataValueField = "Value";
            cmbSitePrimary.DataBind();

            cmbSitePrimary2.Items.Clear();
            cmbSitePrimary2.DataSource = primaryAccounts;
            cmbSitePrimary2.DataTextField = "DisplayName";
            cmbSitePrimary2.DataValueField = "Value";
            cmbSitePrimary2.DataBind();
        }

        private void loadPrimaryGenAccounts()
        {
            string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 0) AND (isActive = '{0}') ORDER BY accountName", true);

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbPrimary.Items.Clear();
            cmbPrimary.DataSource = ddItems;
            cmbPrimary.DataTextField = "DisplayName";
            cmbPrimary.DataValueField = "Value";
            cmbPrimary.DataBind();
        }

        private void loadSites()
        {
            string ddQuery = "SELECT siteName, id FROM tblGenSites order by siteName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbActiveSites.Items.Clear();
            cmbActiveSites.DataSource = ddItems;
            cmbActiveSites.DataTextField = "DisplayName";
            cmbActiveSites.DataValueField = "Value";
            cmbActiveSites.DataBind();

            cmbActiveSites2.Items.Clear();
            cmbActiveSites2.DataSource = ddItems;
            cmbActiveSites2.DataTextField = "DisplayName";
            cmbActiveSites2.DataValueField = "Value";
            cmbActiveSites2.DataBind();

            cmbActiveSites3.Items.Clear();
            cmbActiveSites3.DataSource = ddItems;
            cmbActiveSites3.DataTextField = "DisplayName";
            cmbActiveSites3.DataValueField = "Value";
            cmbActiveSites3.DataBind();

            cmbActiveSites4.Items.Clear();
            cmbActiveSites4.DataSource = ddItems;
            cmbActiveSites4.DataTextField = "DisplayName";
            cmbActiveSites4.DataValueField = "Value";
            cmbActiveSites4.DataBind();
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
            txtDate.Enabled = false;
        }

        private void clearControls()
        {
            txtGenAmount.Text = 
                txtGenChequeNumber.Text = 
                txtGenDescription.Text = 
                txtSite2Amount.Text = 
                txtSite2Description.Text = 
                txtSiteAmount.Text = 
                txtSiteDescription.Text = ""; 
        }

        private void hidePanels()
        {
            pnlGenAccountHierarchy.Visible =
                pnlFileUpload.Visible = 
                pnlExcelImport.Visible = 
                pnlAccountIDsGrid.Visible = 
                pnlSiteDebitOK.Visible = 
                pnlSiteCreditOK.Visible = 
                pnlSiteDebit.Visible =
                pnlSiteCredit.Visible =
                pnlSiteInputs.Visible =
                pnlSiteOutputs.Visible =
                pnlSuppliers.Visible =
                pnlCashAndBanks.Visible =
                pnlGenDate.Visible =
                pnlGenOK.Visible =
                pnlSite.Visible =
                pnlSiteAccountHirarchy.Visible =
                pnlSiteDate.Visible =
                pnlSiteOK.Visible =
                pnlSite2.Visible =
                pnlSite2AccountHierarcy.Visible =
                pnlSite2Date.Visible =
                pnlSite2OK.Visible =
                pnlMiscLiabilities.Visible =
                pnlDate.Visible = 
                pnlTransaction.Visible = false;
        }

        private void hideButtons()
        {
            btnGenCredit.Visible = 
                btnImportLiabilityFromExcel.Visible =
                btnMiscLiabilitiesCreate.Visible =
                btnSuppliers.Visible = 
                btnCashAndBankCredit.Visible = 
                btnCashAndBankDebit.Visible =
                btnSiteInputs.Visible =
                btnSiteOutputs.Visible = 
                btnGenDebit.Visible = 
                btnSiteCredit.Visible = 
                btnSiteDebit.Visible = 
                btnSiteCredit2.Visible = 
                btnSiteDebit2.Visible = false;
        }

        protected void btnTransferToSite_Click(object sender, EventArgs e)
        {
            hidePanels();
            hideButtons();
            clearLabels();
            pnlDate.Visible = pnlSite.Visible = pnlSiteAccountHirarchy.Visible = pnlSiteDate.Visible = pnlSiteOK.Visible = pnlGenAccountHierarchy.Visible = pnlGenDate.Visible = pnlGenOK.Visible = pnlTransaction.Visible = true;
            btnSiteDebit.Visible = btnGenCredit.Visible = true;
            lblHeading.Text = "Site Outputs in other accounts.";
        }

        protected void btnTransferFromSite_Click(object sender, EventArgs e)
        {
            hidePanels();
            hideButtons();
            clearLabels();
            pnlDate.Visible = pnlSite.Visible = pnlSiteAccountHirarchy.Visible = pnlSiteDate.Visible = pnlSiteOK.Visible = pnlGenAccountHierarchy.Visible = pnlGenDate.Visible = pnlGenOK.Visible = pnlTransaction.Visible = true;
            btnSiteCredit.Visible = btnGenDebit.Visible = true;
            lblHeading.Text = "Site gives its inputs to other sites.";
        }

        protected void btnInterSiteTransfer_Click(object sender, EventArgs e)
        {
            hidePanels();
            hideButtons();
            clearLabels();
            pnlDate.Visible = pnlSite.Visible = pnlSiteAccountHirarchy.Visible = pnlSiteDate.Visible = pnlSiteOK.Visible = pnlSite2.Visible = pnlSite2AccountHierarcy.Visible = pnlSite2Date.Visible = pnlSite2OK.Visible = pnlTransaction.Visible = true;
            btnSiteDebit.Visible = btnSiteCredit2.Visible = true;
            //btnSiteCredit.Visible = btnSiteDebit2.Visible = true;
            lblHeading.Text = "Site to Site or Within Site transfer";
        }

        protected void btnTransferWithinSite_Click(object sender, EventArgs e)
        {
            hidePanels();
            hideButtons();
            clearLabels();
            pnlDate.Visible = pnlSite.Visible = pnlSiteAccountHirarchy.Visible = pnlSiteDate.Visible = pnlSiteOK.Visible = pnlSite2AccountHierarcy.Visible = pnlSite2Date.Visible = pnlSite2OK.Visible = pnlTransaction.Visible = true;
            btnSiteDebit.Visible = btnSiteCredit2.Visible = true;
            //btnSiteCredit.Visible = btnSiteDebit2.Visible = true;
        }

        protected void cmbSitePrimary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSitePrimary.SelectedIndex > 0)
            {                
                populateSiteSecondaryAccountsList("site1");
            }
        }

        protected void cmbSitePrimary2_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSitePrimary2.SelectedIndex > 0)
            {
                populateSiteSecondaryAccountsList("site2");
            }
        }

        private void populateSiteSecondaryAccountsList(string site)
        {
            if (site.Equals("site1"))
            {
                int parentAccountID = int.Parse(cmbSitePrimary.SelectedValue);

                string querySecondaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> secondaryAccounts = new List<DDList>();
                secondaryAccounts = p.getDDList(querySecondaryAccounts);

                cmbSiteSecondary.Items.Clear();
                cmbSiteSecondary.DataSource = secondaryAccounts;
                cmbSiteSecondary.DataTextField = "DisplayName";
                cmbSiteSecondary.DataValueField = "Value";
                cmbSiteSecondary.DataBind();
            }

            else
            {
                int parentAccountID = int.Parse(cmbSitePrimary2.SelectedValue);

                string querySecondaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> secondaryAccounts = new List<DDList>();
                secondaryAccounts = p.getDDList(querySecondaryAccounts);

                cmbSiteSecondary2.Items.Clear();
                cmbSiteSecondary2.DataSource = secondaryAccounts;
                cmbSiteSecondary2.DataTextField = "DisplayName";
                cmbSiteSecondary2.DataValueField = "Value";
                cmbSiteSecondary2.DataBind();
            }
        }

        protected void cmbSiteSecondary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSitePrimary.SelectedIndex > 0 && cmbSiteSecondary.SelectedIndex > 0)
            {
                populateSiteTertiaryAccountsList("site1");
            }
        }

        protected void cmbSiteSecondary2_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSitePrimary2.SelectedIndex > 0 && cmbSiteSecondary2.SelectedIndex > 0)
            {
                populateSiteTertiaryAccountsList("site2");
            }
        }

        private void populateSiteTertiaryAccountsList(string site)
        {
            if (site.Equals("site1"))
            {
                int parentAccountID = int.Parse(cmbSiteSecondary.SelectedValue);

                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbSiteTertiary.Items.Clear();
                cmbSiteTertiary.DataSource = tertiaryAccounts;
                cmbSiteTertiary.DataTextField = "DisplayName";
                cmbSiteTertiary.DataValueField = "Value";
                cmbSiteTertiary.DataBind();
            }
            else
            {
                int parentAccountID = int.Parse(cmbSiteSecondary2.SelectedValue);

                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbSiteTertiary2.Items.Clear();
                cmbSiteTertiary2.DataSource = tertiaryAccounts;
                cmbSiteTertiary2.DataTextField = "DisplayName";
                cmbSiteTertiary2.DataValueField = "Value";
                cmbSiteTertiary2.DataBind();
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

        protected void btnSiteDebit_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites.SelectedIndex > 0 && cmbSitePrimary.SelectedIndex > 0 && cmbSiteSecondary.SelectedIndex > 0 && cmbSiteTertiary.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbActiveSites.SelectedValue);
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbSiteTertiary.SelectedValue);
                    string description = p.FixString(txtSiteDescription.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSiteAmount.Text, out debitAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Entry recorded successfully");
                    //clearControls();
                    loadTransactionGrid();
                    pnlTransaction.Visible = true;
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnSiteCredit_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites.SelectedIndex > 0 && cmbSitePrimary.SelectedIndex > 0 && cmbSiteSecondary.SelectedIndex > 0 && cmbSiteTertiary.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbActiveSites.SelectedValue);
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbSiteTertiary.SelectedValue);
                    string description = p.FixString(txtSiteDescription.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSiteAmount.Text, out creditAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    if (creditAmount > 0)
                    {
                        quantity = quantity * -1;
                    }

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Entry recorded successfully");
                    //clearControls();
                    loadTransactionGrid();
                    pnlTransaction.Visible = true;
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnSiteDebit2_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites2.SelectedIndex > 0 && cmbSitePrimary2.SelectedIndex > 0 && cmbSiteSecondary2.SelectedIndex > 0 && cmbSiteTertiary2.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbActiveSites2.SelectedValue);
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbSiteTertiary2.SelectedValue);
                    string description = p.FixString(txtSite2Description.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSite2Amount.Text, out debitAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Entry recorded successfully");
                    //clearControls();
                    loadTransactionGrid();
                    pnlTransaction.Visible = true;
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnSiteCredit2_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites2.SelectedIndex > 0 && cmbSitePrimary2.SelectedIndex > 0 && cmbSiteSecondary2.SelectedIndex > 0 && cmbSiteTertiary2.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbActiveSites2.SelectedValue);
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbSiteTertiary2.SelectedValue);
                    string description = p.FixString(txtSite2Description.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSite2Amount.Text, out creditAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    if (creditAmount > 0)
                    {
                        quantity = quantity * -1;
                    }

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Entry recorded successfully");
                    //clearControls();
                    loadTransactionGrid();
                    pnlTransaction.Visible = true;
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnGenDebit_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0 && cmbSecondary.SelectedIndex > 0 && cmbTertiary.SelectedIndex > 0)
            {
                try
                {
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbTertiary.SelectedValue);
                    string description = p.FixString(txtGenDescription.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtGenAmount.Text, out debitAmount);
                    string chequeNumber = p.FixString(txtGenChequeNumber.Text);

                    splitString(description);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, Session["UserName"].ToString());
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Entry recorded successfully");
                    //clearControls();
                    loadTransactionGrid();
                    pnlTransaction.Visible = true;
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnGenCredit_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0 && cmbSecondary.SelectedIndex > 0 && cmbTertiary.SelectedIndex > 0)
            {
                try
                {
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbTertiary.SelectedValue);
                    string description = p.FixString(txtGenDescription.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtGenAmount.Text, out creditAmount);
                    string chequeNumber = p.FixString(txtGenChequeNumber.Text);

                    splitString(description);

                    string startTransQuery = "Begin Transaction ";
                    string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, Session["UserName"].ToString());
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);

                    showSuccessMessage("Entry recorded successfully");
                    //clearControls();
                    loadTransactionGrid();
                    pnlTransaction.Visible = true;
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        private void deleteTempTransactions()
        {
            string query = string.Format("DELETE FROM tblGenAccountsTmpTransactions WHERE operator = '{0}'", Session["UserName"].ToString());
            p.ExecuteQuery(query);
            loadAccountsGrid();
        }

        protected void btnTransactionOK_Click(object sender, EventArgs e)
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
                DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
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

                    string debitTransQuery = string.Format("   INSERT INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, voucherID, operator) VALUES ({0}, {1}, '{2}', '{3}', {4}, '{5}', {6}, {7}, {8}, '{9}', '{10}', {11}, {12}, '{13}')", transID, listTransaction[i].accountID, date, listTransaction[i].chequeNumber, listTransaction[i].quantity, listTransaction[i].UoM, listTransaction[i].rate, listTransaction[i].debit, listTransaction[i].credit, listTransaction[i].description, debitAccountChecksum, siteID, voucherID, Session["UserName"].ToString());
                    string voucherQuery = string.Format("  INSERT INTO tblGenVouchers(voucherID, voucherText) VALUES ({0},'{1}')", voucherID, voucher);
                    midQuery += debitTransQuery;
                    midQuery += voucherQuery;
                }

                string endTransQuery = " Commit Transaction";

                string transQuery = startTransQuery + midQuery + endTransQuery;

                p.ExecuteQuery(transQuery);

                showSuccessMessage("Transaction recorded successfully,");
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

        private void loadTransactionGrid()
        {
            string userName = Session["UserName"].ToString();
            string query = string.Format("SELECT tblGenAccounts.accountName AS Account, tblGenAccountsTmpTransactions.debitAmount AS Debit, tblGenAccountsTmpTransactions.creditAmount AS Credit FROM tblGenAccountsTmpTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTmpTransactions.accountID = tblGenAccounts.id WHERE tblGenAccountsTmpTransactions.operator = '{0}'", userName);

            DataTable dt = p.GetDataTable(query);
            gridTransaction.DataSource = dt;
            gridTransaction.DataBind();
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

        protected void btnCancelTransaction_Click(object sender, EventArgs e)
        {
            clearLabels();
            clearControls();

            try
            {
                deleteTempTransactions();
                loadTransactionGrid();
                showSuccessMessage("Incomplete Transaction deleted successfully.");
                pnlTransaction.Visible = false;
                loadAccountsGrid();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void populateListOfAccounts(string userName)
        {
            listTransaction.Clear();
            totalCredits = 0;
            totalDebits = 0;

            string query = string.Format("SELECT accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, siteID FROM tblGenAccountsTmpTransactions WHERE operator = '{0}'", userName);

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int siteID = 0;
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
                    int.TryParse(dr[9].ToString(), out siteID);
                    AS.siteID = siteID;
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

        private void splitString(string description)
        {
            try
            {
                if (description.Contains("@"))
                {
                    string[] strings = description.Split(' ');

                    quantity = float.Parse(strings[0]);
                    UoM = strings[1].ToLower(); ;
                    rate = float.Parse(strings[3]);
                }
            }
            catch (Exception)
            {
                throw new Exception("Description does not seem to be in proper format. Please ensure proper spacing and format and try again.");
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

        protected void btnInputsCashAndBank_Click(object sender, EventArgs e)
        {
            clearControls();
            hidePanels();
            hideButtons();
            pnlSite.Visible = pnlSiteInputs.Visible = pnlSiteOK.Visible = pnlSite2OK.Visible = pnlDate.Visible = pnlTransaction.Visible = pnlCashAndBanks.Visible = pnlSiteDate.Visible = pnlSite2Date.Visible = true;
            btnSiteInputs.Visible = btnCashAndBankCredit.Visible = true;
            lblHeading.Text = "Site inputs through cash book";
        }

        protected void btnInputsSuppliers_Click(object sender, EventArgs e)
        {
            clearControls();
            hidePanels();
            hideButtons();
            pnlSite.Visible = pnlSiteInputs.Visible = pnlSiteOK.Visible = pnlDate.Visible = pnlTransaction.Visible = pnlSite2OK.Visible = pnlSuppliers.Visible = pnlSiteDate.Visible = pnlSite2Date.Visible = true;
            btnSiteInputs.Visible = btnSuppliers.Visible = true;
            lblHeading.Text = "Site inputs through suppliers";
        }

        protected void btnReceiptsCashAndBank_Click(object sender, EventArgs e)
        {
            clearControls();
            hidePanels();
            hideButtons();
            pnlSite.Visible = pnlSiteOutputs.Visible = pnlSiteOK.Visible = pnlDate.Visible = pnlTransaction.Visible = pnlSite2OK.Visible = pnlCashAndBanks.Visible = pnlSiteDate.Visible = pnlSite2Date.Visible = true;
            btnSiteOutputs.Visible = btnCashAndBankDebit.Visible = true;
            lblHeading.Text = "Site Outputs in form of cash or cheque.";
        }

        protected void cmbSecondaryInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecondaryInput.SelectedIndex > 0)
            {
                int accountID = int.Parse(cmbSecondaryInput.SelectedValue);
                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbTertiaryInput.Items.Clear();
                cmbTertiaryInput.DataSource = tertiaryAccounts;
                cmbTertiaryInput.DataTextField = "DisplayName";
                cmbTertiaryInput.DataValueField = "Value";
                cmbTertiaryInput.DataBind();
            }
        }

        protected void cmbSecondaryOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecondaryOutput.SelectedIndex > 0)
            {
                int accountID = int.Parse(cmbSecondaryOutput.SelectedValue);
                string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

                List<DDList> tertiaryAccounts = new List<DDList>();
                tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

                cmbTertiaryOutput.Items.Clear();
                cmbTertiaryOutput.DataSource = tertiaryAccounts;
                cmbTertiaryOutput.DataTextField = "DisplayName";
                cmbTertiaryOutput.DataValueField = "Value";
                cmbTertiaryOutput.DataBind();
            }
        }

        protected void btnSiteInputs_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites.SelectedIndex > 0 && cmbSecondaryInput.SelectedIndex > 0 && cmbTertiaryInput.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbActiveSites.SelectedValue);
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbTertiaryInput.SelectedValue);
                    string description = p.FixString(txtSiteDescription.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSiteAmount.Text, out debitAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    if (debitAmount != 0)
                    {
                        string startTransQuery = "Begin Transaction ";
                        string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        showSuccessMessage("Entry recorded successfully");
                        //clearControls();
                        loadTransactionGrid();
                        pnlTransaction.Visible = true;
                    }
                    else
                    {
                        showWarningMessage("There must be some amount to enter the transaction.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnSiteOutputs_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites.SelectedIndex > 0 && cmbSecondaryOutput.SelectedIndex > 0 && cmbTertiaryOutput.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbActiveSites.SelectedValue);
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbTertiaryOutput.SelectedValue);
                    string description = p.FixString(txtSiteDescription.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSiteAmount.Text, out creditAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    if (creditAmount != 0)
                    {
                        string startTransQuery = "Begin Transaction ";
                        string creditTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + creditTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        showSuccessMessage("Entry recorded successfully");
                        //clearControls();
                        loadTransactionGrid();
                        pnlTransaction.Visible = true;
                    }
                    else
                    {
                        showWarningMessage("There must be some amount to enter the transaction.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnSuppliers_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites.SelectedIndex > 0 && cmbSupplier.SelectedIndex > 0)
            {
                try
                {
                    int siteID = 0;
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbSupplier.SelectedValue);
                    string description = p.FixString(txtSite2Description.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSite2Amount.Text, out creditAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    if (creditAmount != 0)
                    {
                        string startTransQuery = "Begin Transaction ";
                        string creditTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + creditTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        showSuccessMessage("Entry recorded successfully");
                        //clearControls();
                        loadTransactionGrid();
                        pnlTransaction.Visible = true;
                    }
                    else
                    {
                        showWarningMessage("There must be some amount to enter the transaction.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnCashAndBankDebit_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    int siteID = 0;
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbCashAndBank.SelectedValue);
                    string description = p.FixString(txtSite2Description.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSite2Amount.Text, out debitAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    if (debitAmount != 0)
                    {
                        string startTransQuery = "Begin Transaction ";
                        string creditTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + creditTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        showSuccessMessage("Entry recorded successfully");
                        //clearControls();
                        loadTransactionGrid();
                        pnlTransaction.Visible = true;
                    }
                    else
                    {
                        showWarningMessage("There must be some amount to enter the transaction.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnCashAndBankCredit_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbActiveSites.SelectedIndex > 0 && cmbCashAndBank.SelectedIndex > 0)
            {
                try
                {
                    int siteID = 0;
                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                    int accountID = int.Parse(cmbCashAndBank.SelectedValue);
                    string description = p.FixString(txtSite2Description.Text);
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    string accountChecksum = string.Empty;

                    Decimal.TryParse(txtSite2Amount.Text, out creditAmount);
                    string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                    splitString(description);

                    if (creditAmount != 0)
                    {
                        string startTransQuery = "Begin Transaction ";
                        string creditTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + creditTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        showSuccessMessage("Entry recorded successfully");
                        //clearControls();
                        loadTransactionGrid();
                        pnlTransaction.Visible = true;
                    }
                    else
                    {
                        showWarningMessage("There must be some amount to enter the transaction.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnSiteTransactionsIDs_Click(object sender, EventArgs e)
        {
            hideButtons();
            hidePanels();
            pnlSiteDebit.Visible = pnlSiteCredit.Visible = pnlSiteDate.Visible = pnlSite2Date.Visible = pnlAccountIDsGrid.Visible = pnlSiteDebitOK.Visible = pnlSiteCreditOK.Visible = true;
            loadAccountsGrid();
            lblHeading.Text = "Site transactions through account IDs.";
        }

        protected void btnSiteDebitOK_Click(object sender, EventArgs e)
        {
            clearLabels();

            int accountID = 0;
            int.TryParse(txtSiteDebitAccountID.Text, out accountID);

            string simpleAccountQuery = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (ID = {0}) AND (isActive = 'true') AND (remarks = 'General')  ORDER BY accountName", accountID);
            string siteAccountQuery = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (ID = {0}) AND (isActive = 'true') AND (remarks = 'Site')  ORDER BY accountName", accountID);

            if (cmbActiveSites3.SelectedIndex > 0)
            {
                if (p.ifRecordsExist(siteAccountQuery))
                {
                    try
                    {
                        int siteID = int.Parse(cmbActiveSites3.SelectedValue);
                        DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                        string description = p.FixString(txtSiteDescription.Text);
                        Decimal debitAmount = 0;
                        Decimal creditAmount = 0;
                        quantity = 0.0f;
                        rate = 0.0f;
                        UoM = string.Empty;
                        string accountChecksum = string.Empty;

                        Decimal.TryParse(txtSiteAmount.Text, out debitAmount);
                        string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                        splitString(description);

                        if (debitAmount != 0)
                        {
                            string startTransQuery = "Begin Transaction ";
                            string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                            string endTransQuery = " Commit Transaction";

                            string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                            p.ExecuteQuery(transQuery);

                            showSuccessMessage("Entry recorded successfully");
                            clearControls();
                            loadTransactionGrid();
                            pnlTransaction.Visible = true;
                            loadAccountsGrid();
                        }
                        else
                        {
                            showWarningMessage("There must be some amount to enter the transaction.");
                            loadAccountsGrid();
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
                    showWarningMessage("The account ID: " + accountID.ToString() + " is not a valid site account.");
                    loadAccountsGrid();
                }
            }
            else
            {
                if (p.ifRecordsExist(simpleAccountQuery))
                {
                    try
                    {
                        int siteID = 0;
                        DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                        string description = p.FixString(txtSiteDescription.Text);
                        Decimal debitAmount = 0;
                        Decimal creditAmount = 0;
                        quantity = 0.0f;
                        rate = 0.0f;
                        UoM = string.Empty;
                        string accountChecksum = string.Empty;

                        Decimal.TryParse(txtSiteAmount.Text, out debitAmount);
                        string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                        splitString(description);

                        if (debitAmount != 0)
                        {
                            string startTransQuery = "Begin Transaction ";
                            string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                            string endTransQuery = " Commit Transaction";

                            string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                            p.ExecuteQuery(transQuery);

                            showSuccessMessage("Entry recorded successfully");
                            //clearControls();
                            loadTransactionGrid();
                            pnlTransaction.Visible = true;
                            loadAccountsGrid();
                        }
                        else
                        {
                            showWarningMessage("There must be some amount to enter the transaction.");
                            loadAccountsGrid();
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
                    showWarningMessage("The account ID: " + accountID.ToString() + " is not a valid general account.");
                    loadAccountsGrid();
                    pnlTransaction.Visible = true;
	            }
            }
        }

        protected void btnSiteCreditOK_Click(object sender, EventArgs e)
        {
            clearLabels();

            int accountID = 0;
            int.TryParse(txtSiteCreditID.Text, out accountID);

            string simpleAccountQuery = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (ID = {0}) AND (isActive = 'true') AND (remarks = 'General')  ORDER BY accountName", accountID);
            string siteAccountQuery = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (ID = {0}) AND (isActive = 'true') AND (remarks = 'Site')  ORDER BY accountName", accountID);

            if (cmbActiveSites4.SelectedIndex > 0)
            {
                if (p.ifRecordsExist(siteAccountQuery))
                {
                    try
                    {
                        int siteID = int.Parse(cmbActiveSites4.SelectedValue);
                        DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                        string description = p.FixString(txtSite2Description.Text);
                        Decimal debitAmount = 0;
                        Decimal creditAmount = 0;
                        quantity = 0.0f;
                        rate = 0.0f;
                        UoM = string.Empty;
                        string accountChecksum = string.Empty;

                        Decimal.TryParse(txtSite2Amount.Text, out creditAmount);
                        string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                        splitString(description);

                        if (creditAmount != 0)
                        {
                            string startTransQuery = "Begin Transaction ";
                            string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                            string endTransQuery = " Commit Transaction";

                            string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                            p.ExecuteQuery(transQuery);

                            showSuccessMessage("Entry recorded successfully");
                            clearControls();
                            loadTransactionGrid();
                            pnlTransaction.Visible = true;
                            loadAccountsGrid();
                        }
                        else
                        {
                            showWarningMessage("There must be some amount to enter the transaction.");
                            loadAccountsGrid();
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
                    showWarningMessage("The account ID: " + accountID.ToString() + " is not a valid site account.");
                    loadAccountsGrid();
                }
            }
            else
            {
                if (p.ifRecordsExist(simpleAccountQuery))
                {
                    try
                    {
                        int siteID = 0;
                        DateTime date = Validator.ValidateDate(txtDate.Text, "Date");
                        string description = p.FixString(txtSite2Description.Text);
                        Decimal debitAmount = 0;
                        Decimal creditAmount = 0;
                        quantity = 0.0f;
                        rate = 0.0f;
                        UoM = string.Empty;
                        string accountChecksum = string.Empty;

                        Decimal.TryParse(txtSite2Amount.Text, out creditAmount);
                        string chequeNumber = string.Empty; //p.FixString(txtChequeJournal.Text);

                        splitString(description);

                        if (creditAmount != 0)
                        {
                            string startTransQuery = "Begin Transaction ";
                            string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                            string endTransQuery = " Commit Transaction";

                            string transQuery = startTransQuery + debitTransQuery + endTransQuery;

                            p.ExecuteQuery(transQuery);

                            showSuccessMessage("Entry recorded successfully");
                            //clearControls();
                            loadTransactionGrid();
                            pnlTransaction.Visible = true;
                            loadAccountsGrid();
                        }
                        else
                        {
                            showWarningMessage("There must be some amount to enter the transaction.");
                            loadAccountsGrid();
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
                    showWarningMessage("The account ID: " + accountID.ToString() + " is not a valid general account.");
                    loadAccountsGrid();
                }
            }
        }

        protected void btnMiscLiabilities_Click(object sender, EventArgs e)
        {
            clearLabels();
            hideButtons();
            hidePanels();
            pnlDate.Visible = pnlSite.Visible = pnlSiteDate.Visible = btnImportLiabilityFromExcel.Visible = btnMiscLiabilitiesCreate.Visible = pnlSiteOK.Visible = true;
            gridMiscLiabDataBind();
            lblHeading.Text = "Diary to note down Liabilities.";
        }

        protected void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            clearLabels();
            hideButtons();
            hidePanels();
            pnlFileUpload.Visible = true;
            lblHeading.Text = "Import Data from Excel.";
        }

        //private void getDataFromExcel()
        //{
        //    /* 
        //    //if File is not selected then return  
        //    if (Request.Files["FileUpload1"].ContentLength <= 0)
        //    { return; }

        //    string filePath = @"D:\Exceldata.xlsx";

        //    //Get the file extension  
        //    //string fileExtension = Path.GetExtension(Request.Files["FileUpload1"].FileName);
        //    string fileExtension = Path.GetExtension(filePath);

        //    //If file is not in excel format then return  
        //    if (fileExtension != ".xls" && fileExtension != ".xlsx")
        //    { return; }

        //    //Get the File name and create new path to save it on server  
        //    string fileLocation = Server.MapPath("\\") + Request.Files["FileUpload1"].FileName;

        //    //if the File is exist on serevr then delete it  
        //    if (File.Exists(fileLocation))
        //    {
        //        File.Delete(fileLocation);
        //    }
        //    //save the file lon the server before loading  
        //    Request.Files["FileUpload1"].SaveAs(fileLocation);

        //    //Create the QueryString for differnt version of fexcel file  
        //    string strConn = "";
        //    switch (fileExtension)
        //    {
        //        case ".xls": //Excel 1997-2003  
        //            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
        //            break;
        //        case ".xlsx": //Excel 2007-2010  
        //            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0 xml;HDR=Yes;IMEX=1\"";
        //            break;
        //    }

        //    //Get the data from the excel sheet1 which is default  
        //    string query = "select * from  [Sheet1$]";
        //    OleDbConnection objConn;
        //    OleDbDataAdapter oleDA;
        //    DataTable dt = new DataTable();
        //    objConn = new OleDbConnection(strConn);
        //    objConn.Open();
        //    oleDA = new OleDbDataAdapter(query, objConn);
        //    oleDA.Fill(dt);
        //    objConn.Close();
        //    oleDA.Dispose();
        //    objConn.Dispose();

        //    //Bind the datatable to the Grid  
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();  
        //     */

        //    string fileLocation = string.Empty;

        //    if (FileUpload1.HasFile)
        //    {
        //        string fileExtension = Path.GetExtension(Request.Files["FileUpload1"].FileName);
        //        if ((fileExtension != ".xls") || (fileExtension != "xlsx"))
        //        {
        //            showWarningMessage("Only Excel files can be uploaded.");
        //        }
        //        else
        //        {
        //            fileLocation = Server.MapPath("\\") + Request.Files["FileUpload1"].FileName;
        //            if (File.Exists(fileLocation))
        //            {
        //                File.Delete(fileLocation);
        //            }
        //            Request.Files["FileUpload1"].SaveAs(fileLocation);
        //        }
        //    }
            
        //    //for .xls
        //    //string OleDbConStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= '{0}';Extended Properties='Excel 12.0 Xml;HDR=YES'", filePath);
        //    //for .xlsx
        //    string OleDbConStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= '{0}';Extended Properties='Excel 12.0 Xml;HDR=YES'", fileLocation);
        //    OConnection.ConnectionString = OleDbConStr;
        //    OConnection.Open();

        //    string sheet = "Data";
        //    //string selectData = string.Format("SELECT Account, Credit, Date, Debit, Description, SiteID FROM [{0}$]", sheet);
        //    string selectData = string.Format("SELECT * FROM [{0}$]", sheet);
        //    OleDbCommand selectDataCommand = new OleDbCommand(selectData, OConnection);

        //    OleDbDataReader dr = selectDataCommand.ExecuteReader();


        //    while (dr.Read())
        //    {
        //        if (!(dr[0].ToString().Equals("")))
        //        {
        //            ExcelData s = new ExcelData();

        //            s.AccountNumber = int.Parse(dr[1].ToString());

        //            Decimal Credit = 0;
        //            Decimal.TryParse(dr[6].ToString(), out Credit);                    
        //            s.Credit = Credit;

        //            s.Date = DateTime.Parse(dr[0].ToString());

        //            Decimal Debit = 0;
        //            Decimal.TryParse(dr[5].ToString(), out Debit);
        //            s.Debit = Debit; 
                    
        //            s.Description = dr[7].ToString();
        //            s.SiteID = int.Parse(dr[3].ToString());

        //            int paidBy = 0;
        //            int.TryParse(dr[8].ToString(), out paidBy);
        //            s.PaidBy = paidBy;

        //            data.Add(s);
        //        }
        //    }
        //    dr.Close();
        //    OConnection.Close();

        //    string insert = string.Empty;
        //    insert += "Begin Transaction ";
        //    insert += "  Delete from tblExcelData  ";

        //    for (int i = 0; i < data.Count; i++)
        //    {
        //        string insertQuery = string.Format("  INSERT INTO tblExcelData(Date, Account, PaidBy, Site, Debit, Credit, Description) VALUES ('{0}',{1},{2},{3},{4},{5},'{6}')", data[i].Date, data[i].AccountNumber, data[i].PaidBy, data[i].SiteID, data[i].Debit, data[i].Credit, data[i].Description);
        //        insert += insertQuery;
        //    }

        //    insert += "  Commit Transaction ";

        //    p.ExecuteQuery(insert);

        //    string selectQuery = "Select * from tblExcelData";
        //    DataTable dt = p.GetDataTable(selectQuery);
        //    gridExcelData.DataSource = dt;
        //    gridExcelData.DataBind();
        //}

        private void gridMiscLiabDataBind()
        {
            string query = string.Format("SELECT tblGenMiscLiabilities.id, tblGenMiscLiabilities.isSettled, tblGenSites.siteName, tblGenMiscLiabilities.description, tblGenMiscLiabilities.amount FROM tblGenMiscLiabilities INNER JOIN tblGenSites ON tblGenMiscLiabilities.siteID = tblGenSites.id WHERE tblGenMiscLiabilities.isSettled = 'false'");
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                gridMiscLiabilities.DataSource = dt;
                gridMiscLiabilities.DataBind();
                pnlMiscLiabilities.Visible = true;
            }
            else
            {
                gridMiscLiabilities.DataSource = dt;
                gridMiscLiabilities.DataBind();
                pnlMiscLiabilities.Visible = false;
            }
        }

        protected void btnMiscLiabilitiesCreate_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbActiveSites.SelectedIndex > 0)
            {
                try
                {
                    DateTime date;
                    DateTime.TryParse(txtDate.Text, out date);
                    Decimal amount = 0;
                    Decimal.TryParse(txtSiteAmount.Text, out amount);
                    string description = p.FixString(txtSiteDescription.Text);
                    int siteID = int.Parse(cmbActiveSites.SelectedValue);

                    string query = string.Format("INSERT INTO tblGenMiscLiabilities(date, siteID, description, amount) VALUES ('{0}',{1},'{2}',{3})", date, siteID, description, amount);
                    p.ExecuteQuery(query);
                    gridMiscLiabDataBind();
                    showSuccessMessage("Information recorded successfully.");
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("Please select a site and try again.");
            }
        }

        protected void btnImportLiabilitiesFromExcel_Click(object sender, EventArgs e)
        {
            clearLabels();

            string filePath = @"D:\Exceldata.xlsx";
            //for .xls
            //string OleDbConStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= '{0}';Extended Properties='Excel 12.0 Xml;HDR=YES'", filePath);
            //for .xlsx
            string OleDbConStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= '{0}';Extended Properties='Excel 12.0 Xml;HDR=YES'", filePath);
            OConnection.ConnectionString = OleDbConStr;
            OConnection.Open();

            string sheet = "Liabilities";
            //string selectData = string.Format("SELECT Account, Credit, Date, Debit, Description, SiteID FROM [{0}$]", sheet);
            string selectData = string.Format("SELECT * FROM [{0}$]", sheet);
            OleDbCommand selectDataCommand = new OleDbCommand(selectData, OConnection);

            OleDbDataReader dr = selectDataCommand.ExecuteReader();

            List<ExcelData> data = new List<ExcelData>();
            DateTime date;
            DateTime.TryParse(txtDate.Text, out date);

            string insert = string.Empty;
            insert += "  Begin Transaction ";

            while (dr.Read())
            {
                if (!(dr[0].ToString().Equals("")))
                {
                    Decimal amount = 0;
                    Decimal.TryParse(dr[5].ToString(), out amount);

                    string description = p.FixString(dr[3].ToString());
                    int siteID = int.Parse(dr[4].ToString());

                    string query = string.Format("  INSERT INTO tblGenMiscLiabilities(date, siteID, description, amount) VALUES ('{0}',{1},'{2}',{3})", date, siteID, description, amount);
                    insert += query;
                }
            }
            dr.Close();
            OConnection.Close();

            insert += "  Commit Transaction ";

            p.ExecuteQuery(insert);

            gridMiscLiabDataBind();
        }

        protected void gridMiscLiabilities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                miscLiabAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "amount"));
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Grand Total";
                e.Row.Cells[3].Font.Bold = true;

                e.Row.Cells[4].Text = miscLiabAmount.ToString("0,0.00");
                e.Row.Cells[4].Font.Bold = true;
            }
        }

        protected void btnMiscLiabilitiesUpdate_Click(object sender, EventArgs e)
        {
            clearLabels();
            try
            {
                for (int i = 0; i < gridMiscLiabilities.Rows.Count; i++)
                {
                    CheckBox chkChk = (CheckBox)gridMiscLiabilities.Rows[i].FindControl("chkSettled");

                    if (chkChk.Checked)
                    {
                        Label lbliD = (Label)gridMiscLiabilities.Rows[i].FindControl("lblID");
                        int transID = int.Parse(lbliD.Text);

                        string query = string.Format("UPDATE tblGenMiscLiabilities SET isSettled = '{0}' where id = {1}", true, transID);
                        p.ExecuteQuery(query);
                    }

                    else
                    {
                        Label lbliD = (Label)gridMiscLiabilities.Rows[i].FindControl("lblID");
                        int transID = int.Parse(lbliD.Text);

                        TextBox txtAmount = (TextBox)gridMiscLiabilities.Rows[i].FindControl("txtLiabilityAmount");
                        int revisedAmount = 0;
                        int.TryParse(txtAmount.Text, out revisedAmount);

                        string query = string.Format("UPDATE tblGenMiscLiabilities SET amount = {0} where id = {1}", revisedAmount, transID);
                        p.ExecuteQuery(query);
                    }
                }
                gridMiscLiabDataBind();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void cmbActiveSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbActiveSites.SelectedIndex > 0) && (btnMiscLiabilitiesCreate.Visible == true))
            {
                int siteID = int.Parse(cmbActiveSites.SelectedValue);
                
                string query = string.Format("SELECT tblGenMiscLiabilities.id, tblGenMiscLiabilities.isSettled, tblGenSites.siteName, tblGenMiscLiabilities.description, tblGenMiscLiabilities.amount FROM tblGenMiscLiabilities INNER JOIN tblGenSites ON tblGenMiscLiabilities.siteID = tblGenSites.id WHERE tblGenMiscLiabilities.isSettled = 'false' AND tblGenMiscLiabilities.siteID = {0}", siteID);

                DataTable dt = p.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    gridMiscLiabilities.DataSource = dt;
                    gridMiscLiabilities.DataBind();
                    pnlMiscLiabilities.Visible = true;
                }
                else
                {
                    gridMiscLiabilities.DataSource = dt;
                    gridMiscLiabilities.DataBind();
                    pnlMiscLiabilities.Visible = false;
                }
            }
        }

        protected void btnExcelDataSubmit_Click(object sender, EventArgs e)
        {
            clearLabels();
            try
            {
                for (int i = 0; i < gridExcelData.Rows.Count; i++)
                {
                    quantity = 0.0f;
                    rate = 0.0f;
                    UoM = string.Empty;
                    //Label lDate = (Label)gridExcelData.Rows[i].FindControl("lblDate"); 
                    Label lAccount = (Label)gridExcelData.Rows[i].FindControl("lblAccount");
                    Label lPaidBy = (Label)gridExcelData.Rows[i].FindControl("lblPaidBy");
                    Label lSite = (Label)gridExcelData.Rows[i].FindControl("lblSite");
                    Label lDebit = (Label)gridExcelData.Rows[i].FindControl("lblDebit");
                    Label lCredit = (Label)gridExcelData.Rows[i].FindControl("lblCredit");
                    Label lDescription = (Label)gridExcelData.Rows[i].FindControl("lblDescription");

                    DateTime date = Validator.ValidateDate(txtDate.Text, "Date"); 
                    int cashBookAccount = 0;
                    int otherAccount = 0;
                    int siteID = 0;
                    int chequeNumber = 0;
                    Decimal debitAmount = 0;
                    Decimal creditAmount = 0;
                    String accountChecksum = string.Empty;
                    String description = p.FixString(lDescription.Text);
                    splitString(description);

                    //DateTime.TryParse(lDate.Text, out date);
                    int.TryParse(lAccount.Text, out cashBookAccount);
                    int.TryParse(lPaidBy.Text, out otherAccount);
                    int.TryParse(lSite.Text, out siteID);
                    Decimal.TryParse(lDebit.Text, out debitAmount);
                    Decimal.TryParse(lCredit.Text, out creditAmount);

                    if (creditAmount > 0)
                    {
                        string startTransQuery = "Begin Transaction ";
                        string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", otherAccount, date, chequeNumber, quantity, UoM, rate, creditAmount, debitAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                        string creditTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", cashBookAccount, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, Session["UserName"].ToString());
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        enterTransaction();

                        showSuccessMessage("Entry recorded successfully");
                    }

                    else
                    {
                        string startTransQuery = "Begin Transaction ";
                        string debitTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', '{10}')", cashBookAccount, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, accountChecksum, Session["UserName"].ToString());
                        string creditTransQuery = string.Format("  INSERT INTO tblGenAccountsTmpTransactions (accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, operator) VALUES ({0},'{1}','{2}',{3},'{4}',{5},{6},{7},'{8}','{9}', {10}, '{11}')", otherAccount, date, chequeNumber, quantity, UoM, rate, creditAmount, debitAmount, description, accountChecksum, siteID, Session["UserName"].ToString());
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + debitTransQuery + creditTransQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);

                        enterTransaction();

                        showSuccessMessage("Entry recorded successfully");
                    }
                }
                string query = "DELETE FROM tblExcelData";                
                p.ExecuteQuery(query);

                //string selectQuery = "Select * from tblExcelData";
                //DataTable dt = p.GetDataTable(selectQuery);
                DataTable dt = new DataTable();
                gridExcelData.DataSource = dt;
                gridExcelData.DataBind();

            }

            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void enterTransaction()
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
                DateTime date = DateTime.Now;
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

                    string debitTransQuery = string.Format("   INSERT INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, siteID, voucherID, operator) VALUES ({0}, {1}, '{2}', '{3}', {4}, '{5}', {6}, {7}, {8}, '{9}', '{10}', {11}, {12}, '{13}')", transID, listTransaction[i].accountID, date, listTransaction[i].chequeNumber, listTransaction[i].quantity, listTransaction[i].UoM, listTransaction[i].rate, listTransaction[i].debit, listTransaction[i].credit, listTransaction[i].description, debitAccountChecksum, siteID, voucherID, Session["UserName"].ToString());
                    string voucherQuery = string.Format("  INSERT INTO tblGenVouchers(voucherID, voucherText) VALUES ({0},'{1}')", voucherID, voucher);
                    midQuery += debitTransQuery;
                    midQuery += voucherQuery;
                }

                string endTransQuery = " Commit Transaction";

                string transQuery = startTransQuery + midQuery + endTransQuery;

                p.ExecuteQuery(transQuery);

                showSuccessMessage("Transaction recorded successfully,");
                clearControls();

                string query = string.Format("DELETE FROM tblGenAccountsTmpTransactions WHERE operator = '{0}'", Session["UserName"].ToString());
                p.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void btnSubmitFile_Click(object sender, EventArgs e)
        {
            //getDataFromExcel();
            getData();
            pnlExcelImport.Visible = true;
        }

        private void getData()
        {
            string errorMessage = string.Empty;
            bool show = true;
            try
            {
                generalAccounts = p.getIntBoolDictionary("SELECT id FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (isActive = 'true') AND (remarks = 'General')");
                siteAccounts = p.getIntBoolDictionary("SELECT id FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (isActive = 'true') AND (remarks = 'Site')");
                activeSites = p.getIntBoolDictionary("SELECT id FROM tblGenSites WHERE (isactive = 'true')");
                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(FileUpload1.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    DataTable dt = new DataTable();

                    //Loop through the Worksheet rows.
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ExcelData d = new ExcelData();

                        int siteID = 0;
                        int.TryParse(dr[0].ToString(), out siteID);
                        d.SiteID = siteID;

                        int cashBook = 0;
                        int.TryParse(dr[1].ToString(), out cashBook);
                        d.cashBookAccount = cashBook;

                        int otherAcc = 0;
                        int.TryParse(dr[2].ToString(), out otherAcc);
                        d.otherAccount = otherAcc;

                        d.Description = dr[3].ToString();

                        Decimal debit = 0;
                        Decimal.TryParse(dr[4].ToString(), out debit);
                        d.Debit = debit;

                        Decimal credit = 0;
                        Decimal.TryParse(dr[5].ToString(), out credit);
                        d.Credit = credit;

                        bool siteAcc = false;
                        bool activeSite = false;
                        bool genAcc = false;

                        if (debit > 0 && credit > 0)
	                    {
                            errorMessage +=  " Transaction file contains an error line where debit is " + debit.ToString() + " and credit is " + credit.ToString() + " Please correct the error and try again.";
                            //data.Clear();
                            show = false;		 
	                    }

                        if (debit == 0 && credit == 0)
                        {
                            break;
                        }

                        if (siteID > 0)
                        {
                            siteAccounts.TryGetValue(otherAcc, out siteAcc);
                            activeSites.TryGetValue(siteID, out activeSite);
                            if (siteAcc && activeSite)
                            {
                                data.Add(d);
                            }
                            else
                            {
                                errorMessage += "Error in Line " + (i+1) + ". Either " + siteID.ToString() + " is not an active site or " + otherAcc.ToString() + " is not a valid site account. Please check and try again. \n";
                                //data.Clear();
                                show = false;
                            }
                        }
                        else
                        {
                            generalAccounts.TryGetValue(otherAcc, out genAcc);
                            if (genAcc)
                            {
                                data.Add(d);
                            }
                            else
                            {
                                errorMessage +=  "Error in Line " + (i+1) + ". " + otherAcc.ToString() + " is not a valid general account. Please check and try again. \n";
                                //data.Clear();
                                show = false;
                            }
                        }
                    }

                    //OlderVersion
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    DataRow dr = dt.Rows[i];
                    //    ExcelData d = new ExcelData();

                    //    int acc = 0;
                    //    int.TryParse(dr[1].ToString(), out acc);
                    //    d.AccountNumber = acc;

                    //    int siteID = 0;
                    //    int.TryParse(dr[3].ToString(), out siteID);
                    //    d.SiteID = siteID;

                    //    int paidBy = 0;
                    //    int.TryParse(dr[8].ToString(), out paidBy);
                    //    d.PaidBy = paidBy;

                    //    Decimal debit = 0;
                    //    Decimal.TryParse(dr[5].ToString(), out debit);
                    //    d.Debit = debit;

                    //    Decimal credit = 0;
                    //    Decimal.TryParse(dr[6].ToString(), out credit);
                    //    d.Credit = credit;

                    //    d.Description = dr[7].ToString();

                    //    bool siteAcc = false;
                    //    siteAccounts.TryGetValue(acc, out siteAcc);

                    //    bool genAcc = false;
                    //    generalAccounts.TryGetValue(paidBy, out genAcc);

                    //    if (siteAcc && genAcc)
                    //    {
                    //        data.Add(d);                    
                    //    }
                    //}

                    if (show)
                    {
                        gridExcelData.DataSource = data;
                        gridExcelData.DataBind();
                    }
                    else
                    {
                        data.Clear();
                        throw new Exception(errorMessage);
                    }

                    //GridView1.DataSource = dt;
                    //GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
                lblWarning.Visible = true;
            }
        }
    }
}