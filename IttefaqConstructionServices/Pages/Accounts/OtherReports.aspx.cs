using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace IttefaqConstructionServices.Pages.Accounts
{
    public partial class OtherReports : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        IttefaqConstructionServices.Logic.Accounts a = new IttefaqConstructionServices.Logic.Accounts();
        Decimal totalStatus = 0;
        Decimal totalDebits = 0;
        Decimal totalCredits = 0;
        List<Account> startingList = new List<Account>();
        List<Account> endingList = new List<Account>();
        List<Account> finalList = new List<Account>();
        List<Account> list = new List<Account>();
        List<Account> mainClosingList = new List<Account>();
        List<Account> coverPage = new List<Account>();
        List<Account> primaryAccounts = new List<Account>();
        List<Account> secondaryAccounts = new List<Account>();
        List<Account> tertiaryAccounts = new List<Account>();
        List<Account> balances = new List<Account>();
        Dictionary<int, string> siteNames = new Dictionary<int, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect(@"~\Pages\Membership\Login.aspx");
            }
            if (p.Authenticated(Session["UserName"].ToString(), "accounts, viewer", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    txtStartDate.Text = txtDate.Text = txtEndDate.Text = p.fixDateWithMonthNames(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                    //loadBanks();
                    populatePrimaryAccountsList();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void populatePrimaryAccountsList()
        {
            string queryPrimaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 0) AND (isActive = '{0}') AND (accountName NOT LIKE '%Site%') ORDER BY accountName", true);

            List<DDList> primaryAccounts = new List<DDList>();
            primaryAccounts = p.getDDList(queryPrimaryAccounts);

            cmbPrimary.DataSource = primaryAccounts;
            cmbPrimary.DataTextField = "DisplayName";
            cmbPrimary.DataValueField = "Value";
            cmbPrimary.DataBind();
        }

        protected void cmbPrimary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0)
            {
                populateSecondaryAccountsList();
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

        protected void cmbSecondary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0 && cmbSecondary.SelectedIndex > 0)
            {
                populateTertiaryAccountsList();
            }
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

        protected void cmbTertiary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbTertiary.SelectedIndex > 0)
            {
                int accountID = int.Parse(cmbTertiary.SelectedValue);
                loadBankReconciliationGrid(accountID);
            }
        }

        private void loadBanks()
        {
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

        private void loadActiveAccountsList()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts WHERE ((parentAccountID = 1) OR (parentAccountID = 2) OR (parentAccountID = 3) OR (parentAccountID = 4) OR (parentAccountID = 6)) AND (isActive = 'true') ORDER BY accountName";

            List<DDList> ddItems = new List<DDList>();

            //Adding my own items in ddList
            //List<DDList> myOwnList = new List<DDList>();

            //DDList dd = new DDList();
            //dd.DisplayName = "Cash Book";
            //dd.Value = 1000;

            //myOwnList.Add(dd);

            //ddItems = p.getDDList(myOwnList, ddQuery);
            ddItems = p.getDDList(ddQuery);

            cmbAccounts.Items.Clear();
            cmbAccounts.DataSource = ddItems;
            cmbAccounts.DataTextField = "DisplayName";
            cmbAccounts.DataValueField = "Value";
            cmbAccounts.DataBind();
        }

        private void loadInactiveAccountsList()
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts WHERE ((parentAccountID = 1) OR (parentAccountID = 2) OR (parentAccountID = 3) OR (parentAccountID = 4) OR (parentAccountID = 6)) AND (isActive = 'false') ORDER BY accountName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbAccounts.Items.Clear();
            cmbAccounts.DataSource = ddItems;
            cmbAccounts.DataTextField = "DisplayName";
            cmbAccounts.DataValueField = "Value";
            cmbAccounts.DataBind();
        }

        private void refreshGrids()
        {
            try
            {
                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {
            }
        }

        protected void gridPriSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("viewSecDetails");

                if (lnkBtn != null)
                {
                    totalStatus += Decimal.Parse(lnkBtn.Text);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = totalStatus.ToString("0,0.00");
                e.Row.Cells[1].Font.Bold = true;
            }
        }

        protected void gridPriSitesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (cmbAccounts.SelectedIndex > 0)
            {
                hidePenals();
                clearLabels();
                try
                {
                    int accountID = int.Parse(e.CommandArgument.ToString());
                    string accountNameQuery = string.Format("SELECT accountName FROM tblGenAccounts where id = {0}", accountID);
                    Session["AccountNameForStatement"] = p.getStringValue(accountNameQuery);

                    DateTime startDate = DateTime.Now;
                    DateTime.TryParse(txtStartDate.Text, out startDate);

                    DateTime endDate = DateTime.Now;
                    DateTime.TryParse(txtEndDate.Text, out endDate);

                    if (startDate < endDate)
                    {
                        int parentAccountID = int.Parse(cmbAccounts.SelectedValue);
                        loadAccountStatementGrid(accountID, startDate, endDate, parentAccountID);

                        pnlAccCoverPage.Visible = pnlPriAccounts.Visible = pnlStartEndDate.Visible = pnlAccountStatement.Visible = true;
                        refreshGrids();
                    }
                    else
                    {
                        showWarningMessage("Start date must be earlier than end date.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("Select proper value from accounts menu then try again.");
            }
        }

        private void loadAccountStatementGrid(int accountID, DateTime startDate, DateTime endDate, int parentAccountID)
        {
            a.getAccountStatement(accountID, startDate, endDate, parentAccountID);
            DataTable dt = p.GetDataTable("SELECT date AS [Date], description AS [Description], debit AS [Debit], credit AS [Credit], runningBalance AS [Balance], transactionID, cheque AS [Cheque] FROM viewAccountStatement ORDER BY reportingOrder");
            gridAccountStatement.DataSource = dt;
            gridAccountStatement.DataBind();
        }

        private void hidePenals()
        {
            pnlTerSiteInfo.Visible =
                pnlBSCondOK.Visible =
                pnlChartOfAccounts.Visible =
                pnlCoverPage.Visible =
                pnlMainClosing.Visible =
                pnlReconciliationOK.Visible =
                pnlReconciliationStatement.Visible =
                pnlBS.Visible =
                pnlBSOK.Visible =
                pnlPLS.Visible =
                pnlPLSOK.Visible =
                pnlTransactionsDateOK.Visible =
                pnlTransactionsRangeOK.Visible =
                pnlBanks.Visible =
                pnlReconciliation.Visible =
                pnlDate.Visible =
                pnlTrialBalanceOK.Visible =
                pnlStartEndDate.Visible =
                pnlTransaction.Visible =
                pnlAccCoverPage.Visible =
                pnlTrialBalance.Visible =
                pnlAccountStatement.Visible =
                pnlPriAccounts.Visible = false;
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

        protected void btnActiveAccounts_Click(object sender, EventArgs e)
        {
            hidePenals();
            clearLabels();
            loadActiveAccountsList();
            pnlPriAccounts.Visible = pnlStartEndDate.Visible = true;
        }

        protected void cmbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbAccounts.SelectedIndex > 0)
            {
                int value = int.Parse(cmbAccounts.SelectedValue);
                if (value == 1000)
                {
                    loadCashBook();
                    pnlAccCoverPage.Visible = true;
                }
                else
                {
                    int accountID = value;
                    loadPrimaryInfoGrid(accountID);
                    pnlAccCoverPage.Visible = true;
                }
            }
        }

        private void loadPrimaryInfoGrid(int accountID)
        {
            string query = string.Format("select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = {0}", accountID);
            DataTable dt = p.GetDataTable(query);
            gridPriSitesInfo.DataSource = dt;
            gridPriSitesInfo.DataBind();

            if (dt.Rows.Count > 0)
            {
                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnClosedAccounts_Click(object sender, EventArgs e)
        {
            hidePenals();
            clearLabels();
            loadInactiveAccountsList();
            pnlPriAccounts.Visible = true;
        }

        protected void gridAccountStatement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            clearLabels();

            try
            {
                if (e.CommandName.ToString().Equals("viewTransaction"))
                {
                    clearLabels();
                    List<Account> transactions = getListOfTransactions("viewTransaction", e.CommandArgument.ToString());
                    gridTransaction.DataSource = transactions;
                    gridTransaction.DataBind();
                    pnlTransaction.Visible = true;
                }
                else if (e.CommandName.ToString().Equals("viewTransactionByDate"))
                {
                    clearLabels();
                    List<Account> transactions = getListOfTransactions("viewTransactionByDate", e.CommandArgument.ToString());
                    gridTransaction.DataSource = transactions;
                    gridTransaction.DataBind();
                    pnlTransaction.Visible = true;
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        internal List<Account> getListOfTransactions(string commandName, string commandValue)
        {
            List<Account> myTrans = new List<Account>();
            string query = string.Empty;
            if (commandName.Equals("viewTransactionByDate"))
            {
                DateTime date = DateTime.Parse(commandValue.ToString());
                query = string.Format("SELECT tblGenAccountsTransactions.siteID, tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.operator, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id WHERE (tblGenAccountsTransactions.date = '{0}') ORDER BY tblGenAccountsTransactions.transactionID, tblGenAccountsTransactions.creditAmount", date);
            }
            else if (commandName == "viewTransaction")
            {
                int transID = int.Parse(commandValue.ToString());

                if (transID != 0)
                {
                    query = string.Format("SELECT tblGenAccountsTransactions.siteID, tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.operator, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id WHERE (tblGenAccountsTransactions.transactionID = {0}) ORDER BY tblGenAccountsTransactions.transactionID, tblGenAccountsTransactions.creditAmount", transID);
                }
            }
            else
            {
                query = commandValue;
            }

            updateSiteNames();
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //site, date, account, description, debit, credit, operator
                    DataRow dr = dt.Rows[i];
                    Account acc = new Account();
                    int siteID = 0;
                    int.TryParse(dr[0].ToString(), out siteID);
                    string siteName = string.Empty;
                    siteNames.TryGetValue(siteID, out siteName);

                    DateTime date1;
                    DateTime.TryParse(dr[1].ToString(), out date1);

                    string accountName = dr[2].ToString();
                    string description = dr[3].ToString();
                    Decimal debit = 0;
                    Decimal.TryParse(dr[8].ToString(), out debit);
                    Decimal credit = 0;
                    Decimal.TryParse(dr[9].ToString(), out credit);
                    string op = dr[4].ToString();

                    acc.siteName = siteName;
                    acc.date = date1;
                    acc.Name = accountName;
                    acc.description = description;
                    acc.Debit = debit;
                    acc.Credit = credit;
                    acc.oprator = op;
                    myTrans.Add(acc);
                }
            }
            return myTrans;
        }

        protected void btnTrialBalance_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlTrialBalanceOK.Visible = true;
        }

        protected void btnTrialBalanceOK_Click(object sender, EventArgs e)
        {
            clearLabels();
            DateTime date = DateTime.Now;
            try
            {
                DateTime.TryParse(txtDate.Text, out date);
                a.getTrialBalance(date);

                DataTable dt = p.GetDataTable("SELECT accountName, debit, credit FROM viewTrialBalance ORDER BY accountTypeID, accountName");
                gridTrialBalance.DataSource = dt;
                gridTrialBalance.DataBind();
                pnlTrialBalance.Visible = true;
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void gridTrialBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDb = (Label)e.Row.FindControl("lblDebit");
                Label lblCr = (Label)e.Row.FindControl("lblCredit");

                if (lblDb != null)
                {
                    totalDebits += Decimal.Parse(lblDb.Text);
                }

                if (lblCr != null)
                {
                    totalCredits += Decimal.Parse(lblCr.Text);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = totalDebits.ToString("0,0.00");
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[2].Text = totalCredits.ToString("0,0.00");
                e.Row.Cells[2].Font.Bold = true;
            }
        }

        protected void btnPntTrialBalance_Click(object sender, EventArgs e)
        {
            refreshGrids();
        }

        protected void btnAccountReconciliationStatement_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlBanks.Visible = true;
        }

        protected void cmbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clearLabels();
            //if (cmbBank.SelectedIndex > 0)
            //{
            //    int bankID = int.Parse(cmbBank.SelectedValue);
            //    loadBankReconciliationGrid(bankID);
            //}
        }

        private void loadBankReconciliationGrid(int accountID)
        {
            string query = string.Format("SELECT [id], [date], [description], [chequeNumber], [debitAmount], [creditAmount], [isReconciled] FROM [tblGenAccountsTransactions] WHERE (isReconciled = 'false') AND (accountID = {0}) ORDER BY [date]", accountID);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                gridReconciliation.DataSource = dt;
                gridReconciliation.DataBind();
                btnUpdateReconciliation.Visible = true;
                pnlReconciliation.Visible = true;
            }
            else
            {
                gridReconciliation.DataSource = null;
                gridReconciliation.DataBind();
                btnUpdateReconciliation.Visible = false;
                showSuccessMessage("No unreconciled entries exist for: " + cmbTertiary.SelectedItem.ToString());
            }

            string queryUnrecon = string.Format("SELECT [id], [date], [description], [chequeNumber], [debitAmount], [creditAmount], [isReconciled] FROM [tblGenAccountsTransactions] WHERE (isReconciled = 'true') AND (accountID = {0}) ORDER BY [date]", accountID);
            DataTable dt1 = p.GetDataTable(queryUnrecon);

            if (dt1.Rows.Count > 0)
            {
                gridUnreconciliation.DataSource = dt1;
                gridUnreconciliation.DataBind();
                btnUnreconciliation.Visible = true;
                pnlReconciliation.Visible = true;
            }
            else
            {
                gridUnreconciliation.DataSource = null;
                gridUnreconciliation.DataBind();
                btnUnreconciliation.Visible = false;
                showWarningMessage("No Reconciled entries exist for: " + cmbTertiary.SelectedItem.ToString());
            }
        }

        protected void btnUpdateReconciliation_Click(object sender, EventArgs e)
        {
            clearLabels();
            string query = string.Empty;

            for (int i = 0; i < gridReconciliation.Rows.Count; i++)
            {
                try
                {
                    Label lblId = (Label)gridReconciliation.Rows[i].FindControl("lblIDRecon");
                    CheckBox chkRecon = (CheckBox)gridReconciliation.Rows[i].FindControl("chkReconciledRecon");

                    if (chkRecon.Checked)
                    {
                        int transID = 0;
                        int.TryParse(lblId.Text, out transID);

                        string q = string.Format("   UPDATE tblGenAccountsTransactions SET isReconciled = '{0}' WHERE (ID = {1})", true, transID);
                        query += q;
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }

            p.ExecuteTransactionQuery(query);
            loadBankReconciliationGrid(int.Parse(cmbTertiary.SelectedValue));
            showSuccessMessage("Reconciliation status updated successfully.");
        }

        protected void btnUnreconciliation_Click(object sender, EventArgs e)
        {
            clearLabels();
            string query = string.Empty;

            for (int i = 0; i < gridUnreconciliation.Rows.Count; i++)
            {
                try
                {
                    Label lblId = (Label)gridUnreconciliation.Rows[i].FindControl("lblIDUnrecon");
                    CheckBox chkRecon = (CheckBox)gridUnreconciliation.Rows[i].FindControl("chkReconciledUnrecon");

                    if (!chkRecon.Checked)
                    {
                        int transID = 0;
                        int.TryParse(lblId.Text, out transID);

                        string q = string.Format("   UPDATE tblGenAccountsTransactions SET isReconciled = '{0}' WHERE (ID = {1})", false, transID);
                        query += q;
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }

            p.ExecuteTransactionQuery(query);
            loadBankReconciliationGrid(int.Parse(cmbTertiary.SelectedValue));
            showSuccessMessage("Reconciliation status updated successfully.");
        }

        protected void btnTransactionsDateOK_Click(object sender, EventArgs e)
        {
            clearLabels();
            updateSiteNames();
            List<Account> transactions = getListOfTransactions("viewTransactionByDate", txtDate.Text);
            gridTransaction.DataSource = transactions;
            gridTransaction.DataBind();
            pnlTransaction.Visible = true;
        }

        protected void btnTransactionsRangeOK_Click(object sender, EventArgs e)
        {
            clearLabels();
            updateSiteNames();
            DateTime startDate = DateTime.Parse(txtStartDate.Text);
            DateTime endDate = DateTime.Parse(txtEndDate.Text);
            string query = string.Format("SELECT tblGenAccountsTransactions.siteID, tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.operator, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id WHERE (tblGenAccountsTransactions.date >= '{0}') AND (tblGenAccountsTransactions.date <= '{1}') ORDER BY tblGenAccountsTransactions.transactionID", startDate, endDate);
            List<Account> transactions = getListOfTransactions("Other", query);
            gridTransaction.DataSource = transactions;
            gridTransaction.DataBind();
            pnlTransaction.Visible = true;
        }

        private void updateSiteNames()
        {
            siteNames.Clear();
            siteNames = p.getSiteNamesDictionary();
        }

        protected void btnTransactionsDate_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlTransactionsDateOK.Visible = true;

        }

        protected void btnTransactionRange_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlStartEndDate.Visible = pnlTransactionsRangeOK.Visible = true;
        }

        protected void btnPLS_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlStartEndDate.Visible = pnlPLSOK.Visible = true;
        }

        private void getProfitAndLoss(List<Account> startingList, List<Account> endingList)
        {
            finalList = a.getProfitAndLoss(startingList, endingList);
        }

        private List<Account> getTrialBalance(DateTime date)
        {
            List<Account> list = new List<Account>();
            DataTable dt = new DataTable();

            DateTime asOn = date;
            a.getTrialBalance(asOn);

            dt = p.GetDataTable("SELECT accountName, accountTypeID, debit, credit FROM viewTrialBalance ORDER BY accountTypeID, accountName");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    Account b = new Account();

                    b.Name = dr[0].ToString();
                    b.accountTypeID = int.Parse(dr[1].ToString());

                    Decimal deb = 0;
                    Decimal.TryParse(dr[2].ToString(), out deb);
                    b.Debit = deb;

                    Decimal cre = 0;
                    Decimal.TryParse(dr[3].ToString(), out cre);
                    b.Credit = cre;

                    //if (b.accountTypeID == 4 || b.accountTypeID == 5 || b.accountTypeID == 6 || b.accountTypeID == 7)
                    if (b.accountTypeID == 4 || b.accountTypeID == 6)
                    {
                        list.Add(b);
                    }
                }
            }

            return list;
        }

        protected void btnBS_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlBSOK.Visible = true;
        }

        protected void gridPLS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalDebits += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
                totalCredits += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit"));
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Grand Total";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = totalDebits.ToString("0,0.00");
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[2].Text = totalCredits.ToString("0,0.00");
                e.Row.Cells[2].Font.Bold = true;
            }
        }

        protected void btnPLSOK_Click(object sender, EventArgs e)
        {
            clearLabels();
            try
            {
                totalDebits = totalCredits = 0;
                DateTime startingDate = DateTime.Parse(txtStartDate.Text);
                startingDate = startingDate.AddDays(-1);
                DateTime endingDate = DateTime.Parse(txtEndDate.Text);

                if (endingDate > startingDate)
                {
                    startingList.Clear();
                    startingList = getTrialBalance(startingDate);

                    endingList.Clear();
                    endingList = getTrialBalance(endingDate);

                    getProfitAndLoss(startingList, endingList);

                    if (finalList.Count > 0)
                    {
                        gridPLS.DataSource = finalList;
                        gridPLS.DataBind();

                        Decimal profit = totalCredits - totalDebits;

                        txtPLS.Text = profit.ToString("0,0.00");
                        pnlPLS.Visible = true;
                    }
                    else
                    {
                        showWarningMessage("No Profit and Loss account to show.");
                        pnlPLS.Visible = false;
                    }

                }
                else
                {
                    throw new Exception("End Date can't be earlier than Start Date.");
                }

            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void gridBS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalDebits += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
                totalCredits += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit"));
                e.Row.Cells[0].Font.Size = 8;
                e.Row.Cells[1].Font.Size = 8;
                e.Row.Cells[2].Font.Size = 8;
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Grand Total";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = totalDebits.ToString("0,0.00");
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[2].Text = totalCredits.ToString("0,0.00");
                e.Row.Cells[2].Font.Bold = true;
            }
        }

        protected void btnBSOK_Click(object sender, EventArgs e)
        {
            clearLabels();
            totalCredits = totalDebits = 0;

            try
            {
                DateTime asOn = DateTime.Parse(txtDate.Text);
                getTrialBalance(asOn);

                fillList();
                adjustList();

                gridBS.DataSource = list;
                gridBS.DataBind();
                pnlBS.Visible = true;
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void fillList()
        {
            DataTable dt = p.GetDataTable("SELECT accountName, accountTypeID, debit, credit, accountID FROM viewTrialBalance ORDER BY accountTypeID, accountName");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    Account a = new Account();

                    a.Name = dr[0].ToString();
                    a.accountTypeID = int.Parse(dr[1].ToString());
                    a.ID = int.Parse(dr[4].ToString());

                    Decimal deb = 0;
                    Decimal.TryParse(dr[2].ToString(), out deb);
                    a.Debit = deb;

                    Decimal cre = 0;
                    Decimal.TryParse(dr[3].ToString(), out cre);
                    a.Credit = cre;

                    list.Add(a);
                }
            }
        }

        private void adjustList()
        {
            Decimal totalDebits = 0;
            Decimal totalCredits = 0;
            List<Account> plsAcc = new List<Account>();
            List<string> accounts = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].accountTypeID == 4 || list[i].accountTypeID == 6)
                //if (list[i].accountTypeID == 4 || list[i].accountTypeID == 5 || list[i].accountTypeID == 6 || list[i].accountTypeID == 7)
                {
                    totalDebits += list[i].Debit;
                    totalCredits += list[i].Credit;
                    Account my = new Account();
                    my.Debit = list[i].Debit;
                    my.Credit = list[i].Credit;
                    my.Name = list[i].Name;
                    plsAcc.Add(my);
                    //accounts.Add(list[i].Name);
                }
            }

            Decimal profit = totalCredits - totalDebits;

            for (int i = 0; i < plsAcc.Count; i++)
            //for (int i = 0; i < accounts.Count; i++)
            {
                int index = getIndex(plsAcc[i].Name, plsAcc[i].Debit, plsAcc[i].Credit);
                //int index = getIndex(accounts[i]);
                list.RemoveAt(index);
            }

            Account a = new Account();
            a.Name = "Profit or Loss";
            a.typeName = "";
            a.Debit = 0;
            a.Credit = profit;

            list.Add(a);
        }

        private int getIndex(string p1, decimal p2, decimal p3)
        {
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                //if (list[i].Name.Contains("Closing"))
                //{

                //}
                if (list[i].Name.Equals(p1) && list[i].Debit == p2 && list[i].Credit == p3)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private int getIndex(string p)
        {
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name.Equals(p))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        protected void btnReconciliationStatement_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlReconciliationOK.Visible = pnlBanks.Visible = true;

        }

        protected void btnReonciliationOK_Click(object sender, EventArgs e)
        {
            clearLabels();
            Single unreconciledDebits = 0f;
            Single unreconciledCredits = 0f;
            pnlReconciliation.Visible = false;

            if (cmbTertiary.SelectedIndex > 0)
            {
                try
                {
                    DateTime date = DateTime.Parse(txtDate.Text);
                    int accountID = int.Parse(cmbTertiary.SelectedValue);

                    string debitTransactionQuery = string.Format("SELECT date, description, chequeNumber, debitAmount FROM tblGenAccountsTransactions where accountID  = {0} AND date <= '{1}' AND debitAmount > 0 AND isReconciled = 'false'", accountID, date);
                    DataTable debitDataTable = p.GetDataTable(debitTransactionQuery);

                    if (debitDataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < debitDataTable.Rows.Count; i++)
                        {
                            DataRow dr = debitDataTable.Rows[i];
                            Single debit = 0f;
                            Single.TryParse(dr[3].ToString(), out debit);
                            unreconciledDebits += debit;
                        }

                        txtUnreconciledDebits.Text = unreconciledDebits.ToString("0,0.00");
                        gridUnreconciledDebits.DataSource = debitDataTable;
                        gridUnreconciledDebits.DataBind();
                    }

                    string creditTransactionQuery = string.Format("SELECT date, description, chequeNumber, creditAmount FROM tblGenAccountsTransactions where accountID = {0} AND date <= '{1}' AND creditAmount > 0 AND isReconciled = 'false'", accountID, date);
                    DataTable creditDataTable = p.GetDataTable(creditTransactionQuery);

                    if (creditDataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < creditDataTable.Rows.Count; i++)
                        {
                            DataRow dr = creditDataTable.Rows[i];
                            Single credit = 0f;
                            Single.TryParse(dr[3].ToString(), out credit);
                            unreconciledCredits += credit;
                        }

                        txtUnreconciledCredits.Text = unreconciledCredits.ToString("0,0.00");
                        gridUnreconciledCredits.DataSource = creditDataTable;
                        gridUnreconciledCredits.DataBind();
                    }

                    Single openingBalance = a.getDebitAccountBalance(accountID, date);
                    txtBalance.Text = openingBalance.ToString("0,0.00");

                    Single bankAccountBalance = openingBalance - unreconciledDebits + unreconciledCredits;
                    txtBankStatementBalance.Text = bankAccountBalance.ToString("0,0.00");
                    pnlReconciliationStatement.Visible = true;
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("Please select a bank.");
            }
        }

        private void loadCashBook()
        {
            string query = "select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = 61 OR ParentID = 62";
            DataTable dt = p.GetDataTable(query);
            gridPriSitesInfo.DataSource = dt;
            gridPriSitesInfo.DataBind();

            if (dt.Rows.Count > 0)
            {
                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnShowAbdullahSbClosing_Click(object sender, EventArgs e)
        {
            hidePenals();
            clearLabels();

            mainClosingList.Clear();

            try
            {
                string query = "SELECT id, accountName FROM tblGenAccounts WHERE (parentAccountID = 1) OR (parentAccountID = 2) OR (parentAccountID = 3) OR (parentAccountID = 4) OR (parentAccountID = 6) ORDER BY parentAccountID";

                DataTable dt = p.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        Account a = new Account();
                        a.Name = dr[1].ToString();
                        a.typeName = "Head: ";
                        mainClosingList.Add(a);

                        int accountID = 0;
                        int.TryParse(dr[0].ToString(), out accountID);

                        int parentAccountID = p.getParentAccountID(accountID);
                        populateAccountBalances(accountID, parentAccountID);
                    }
                }

                Account siteStatus = new Account();
                siteStatus.Name = "Site Status";
                siteStatus.typeName = "Head: ";
                mainClosingList.Add(siteStatus);

                populateSitesData();

                gridMainClosing.DataSource = mainClosingList;
                gridMainClosing.DataBind();

                pnlMainClosing.Visible = true;
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void populateSitesData()
        {
            string query = "SELECT Name, Status FROM viewActiveSitesSummary";

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Account a = new Account();

                    //a.Name = "-----" + dr[0].ToString();
                    a.Name = dr[0].ToString();
                    Decimal debit = 0;
                    Decimal.TryParse(dr[1].ToString(), out debit);
                    a.Debit = debit * -1;

                    mainClosingList.Add(a);
                    coverPage.Add(a);
                }
            }
        }

        protected void gridMainClosing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string text1 = e.Row.Cells[0].Text;
                if (text1.Contains("&amp;"))
                {
                    text1 = text1.Replace("&amp;", "&");
                }

                bool specialRow = false;

                for (int i = 0; i < mainClosingList.Count; i++)
                {
                    if (mainClosingList[i].Name == text1 && mainClosingList[i].typeName == "Head: ")
                    {
                        specialRow = true;
                        break;
                    }
                }

                if (specialRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 12;
                    e.Row.Cells[0].BackColor = Color.Beige;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.Beige;
                    e.Row.Cells[2].Text = string.Empty;
                    e.Row.Cells[2].BackColor = Color.Beige;
                }
            }
        }

        private void populateAccountBalances(int accountID, int parentAccountID)
        {
            if (parentAccountID == 1 || parentAccountID == 6)
            {
                string query = string.Format("SELECT Name, Status FROM viewGenAccountsGroupByPrimary WHERE (ParentID = {0})", accountID);

                DataTable dt = p.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        Account a = new Account();

                        //a.Name = "-----" + dr[0].ToString();
                        a.Name = dr[0].ToString();
                        Decimal debit = 0;
                        Decimal.TryParse(dr[1].ToString(), out debit);
                        a.Debit = debit;

                        mainClosingList.Add(a);
                    }
                }
            }

            else
            {
                string query = string.Format("SELECT Name, Status FROM viewGenAccountsGroupByPrimaryCreditSide WHERE (ParentID = {0})", accountID);

                DataTable dt = p.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        Account a = new Account();

                        //a.Name = "-----" + dr[0].ToString();
                        a.Name = dr[0].ToString();
                        Decimal credit = 0;
                        Decimal.TryParse(dr[1].ToString(), out credit);
                        a.Credit = credit;

                        mainClosingList.Add(a);
                    }
                }
            }
        }

        protected void gridTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string text1 = e.Row.Cells[0].Text;
                string text2 = e.Row.Cells[1].Text;
                string text3 = e.Row.Cells[2].Text;
                string text4 = e.Row.Cells[3].Text;
                string text5 = e.Row.Cells[4].Text;
                string text6 = e.Row.Cells[5].Text;

                if (text4 == "00.00")
                {
                    e.Row.Cells[3].Text = string.Empty;
                }

                if (text5 == "00.00")
                {
                    e.Row.Cells[4].Text = string.Empty;
                }

                //e.Row.Cells[0].Width = 90;
                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[0].Font.Size = 8;
                e.Row.Cells[1].Font.Size = 8;
                e.Row.Cells[2].Font.Size = 8;
                e.Row.Cells[3].Font.Size = 8;
                e.Row.Cells[4].Font.Size = 8;
                e.Row.Cells[5].Font.Size = 8;
                e.Row.Cells[6].Font.Size = 8;
            }
        }

        protected void gridAccountStatement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string text1 = e.Row.Cells[0].Text;
                string text2 = e.Row.Cells[1].Text;
                string text3 = e.Row.Cells[2].Text;
                string text4 = e.Row.Cells[3].Text;
                string text5 = e.Row.Cells[4].Text;
                string text6 = e.Row.Cells[5].Text;

                if (text5 == "00.00")
                {
                    e.Row.Cells[4].Text = string.Empty;
                }

                if (text6 == "00.00")
                {
                    e.Row.Cells[5].Text = string.Empty;
                }

                //e.Row.Cells[0].Width = 90;
                e.Row.Cells[0].Wrap = false;
                e.Row.Cells[0].Font.Size = 8;
                e.Row.Cells[1].Font.Size = 8;
                e.Row.Cells[2].Font.Size = 8;
                e.Row.Cells[3].Font.Size = 8;
                e.Row.Cells[4].Font.Size = 8;
                e.Row.Cells[5].Font.Size = 8;
                e.Row.Cells[6].Font.Size = 8;
            }
        }

        protected void btnChartOfAccounts_Click(object sender, EventArgs e)
        {
            hidePenals();
            clearLabels();
            pnlChartOfAccounts.Visible = true;

            List<String> coa = new List<String>();

            string queryPrimary = "SELECT id, accountName FROM tblGenAccounts WHERE (parentAccountID = 0) ORDER BY id";
            DataTable dtPrimary = p.GetDataTable(queryPrimary);

            if (dtPrimary.Rows.Count > 0)
            {
                for (int i = 0; i < dtPrimary.Rows.Count; i++)
                {
                    DataRow dr = dtPrimary.Rows[i];

                    Account acc = new Account();
                    int id = 0;
                    int.TryParse(dr[0].ToString(), out id);
                    acc.ID = id;
                    acc.Name = dr[1].ToString();
                    primaryAccounts.Add(acc);
                }
            }

            for (int i = 0; i < primaryAccounts.Count; i++)
            {
                coa.Add(primaryAccounts[i].Name);
                int accID = primaryAccounts[i].ID;
                fillSecAccList(accID);

                for (int j = 0; j < secondaryAccounts.Count; j++)
                {
                    coa.Add("---" + secondaryAccounts[j].Name);
                    int secAccID = secondaryAccounts[j].ID;
                    fillTerAccList(secAccID);

                    for (int k = 0; k < tertiaryAccounts.Count; k++)
                    {
                        coa.Add("------" + tertiaryAccounts[k].ID.ToString() + ": " + tertiaryAccounts[k].Name);
                    }
                }
            }

            gridCoA.DataSource = coa;
            gridCoA.DataBind();
        }

        private void fillTerAccList(int secAccID)
        {
            tertiaryAccounts.Clear();

            string queryTertiary = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY id", secAccID);
            DataTable dtTertiary = p.GetDataTable(queryTertiary);

            if (dtTertiary.Rows.Count > 0)
            {
                for (int i = 0; i < dtTertiary.Rows.Count; i++)
                {
                    DataRow dr = dtTertiary.Rows[i];

                    Account acc = new Account();
                    int id = 0;
                    int.TryParse(dr[0].ToString(), out id);
                    acc.ID = id;
                    acc.Name = dr[1].ToString();
                    tertiaryAccounts.Add(acc);
                }
            }
        }

        private void fillSecAccList(int accID)
        {
            secondaryAccounts.Clear();

            string querySecondary = string.Format("SELECT id, accountName FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY id", accID);
            DataTable dtSecondary = p.GetDataTable(querySecondary);

            if (dtSecondary.Rows.Count > 0)
            {
                for (int i = 0; i < dtSecondary.Rows.Count; i++)
                {
                    DataRow dr = dtSecondary.Rows[i];

                    Account acc = new Account();
                    int id = 0;
                    int.TryParse(dr[0].ToString(), out id);
                    acc.ID = id;
                    acc.Name = dr[1].ToString();
                    secondaryAccounts.Add(acc);
                }
            }
        }

        protected void btnCoverSheet_Click(object sender, EventArgs e)
        {
            try
            {
                clearLabels();
                hidePenals();
                pnlCoverPage.Visible = true;
                coverPage.Clear();

                populateBalances();
                processSites();
                processProjectManagers();
                processSuppliers();
                processInvestors();
                processPersonalLedgers();
                processCashInHand();
                processCashAtBank();
                processIncome();
                processExpenses();
                processAssets();
                processLiabilities();

                string query = "Begin Transaction ";
                query += "  DELETE FROM tblTmpCoverPage  ";

                for (int i = 0; i < coverPage.Count; i++)
                {
                    string s = string.Format("   INSERT INTO tblTmpCoverPage (typeName, name, amount) VALUES ('{0}','{1}',{2})", coverPage[i].typeName, coverPage[i].Name, coverPage[i].Debit);
                    query += s;
                }

                query += " Commit Transaction";

                p.ExecuteQuery(query);

                gridCoverPage.DataSource = coverPage;
                gridCoverPage.DataBind();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void processLiabilities()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Other Liabilities:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 2) AND (isActive = 'true') AND (specialNotes = 'Others') ORDER BY accountName";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            Account item = new Account();
                            item.Name = drLiab[0].ToString();
                            item.Debit = balance;
                            coverPage.Add(item);
                            total += balance;
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void processAssets()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Other Assets:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 1) AND (isActive = 'true') AND (specialNotes = 'Others') ORDER BY accountName";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            Account item = new Account();
                            item.Name = drLiab[0].ToString();
                            item.Debit = balance;
                            coverPage.Add(item);
                            total += balance;
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void processCashAtBank()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Bank Accounts:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Banks') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            Account item = new Account();
                            item.Name = drLiab[0].ToString();
                            item.Debit = balance;
                            coverPage.Add(item);
                            total += balance;
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void processCashInHand()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Cash and Petty Cash:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Cash') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            Account item = new Account();
                            item.Name = drLiab[0].ToString();
                            item.Debit = balance;
                            coverPage.Add(item);
                            total += balance;
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void processExpenses()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Expenses:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 6) AND (isActive = 'true') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            Account item = new Account();
                            item.Name = drLiab[0].ToString();
                            item.Debit = balance;
                            coverPage.Add(item);
                            total += balance;
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void processIncome()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Incomes:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 4) AND (isActive = 'true') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID);
                            Account item = new Account();
                            item.Name = drLiab[0].ToString();
                            item.Debit = balance;
                            coverPage.Add(item);
                            total += balance;
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void processPersonalLedgers()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Personal Ledgers:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Personal Ledgers') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal totalLiab = 0;
                    Decimal totalAdv = 0;
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID);

                            if (balance > 0)
                            {
                                totalLiab += balance;
                                total += balance;

                            }
                            else
                            {
                                totalAdv += balance;
                                total += balance;
                            }
                        }

                        Account advances = new Account();
                        advances.Name = "Total Advances";
                        advances.Debit = totalAdv;
                        coverPage.Add(advances);

                        Account liabilities = new Account();
                        liabilities.Name = "Total Liabilities";
                        liabilities.Debit = totalLiab;
                        coverPage.Add(liabilities);

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void processSuppliers()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Suppliers:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Suppliers') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal totalLiab = 0;
                    Decimal totalAdv = 0;
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID);

                            if (balance > 0)
                            {
                                totalLiab += balance;
                                total += balance;

                            }
                            else
                            {
                                totalAdv += balance;
                                total += balance;
                            }
                        }

                        Account advances = new Account();
                        advances.Name = "Total Advances";
                        advances.Debit = totalAdv;
                        coverPage.Add(advances);

                        Account liabilities = new Account();
                        liabilities.Name = "Total Liabilities";
                        liabilities.Debit = totalLiab;
                        coverPage.Add(liabilities);

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void processInvestors()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Investors:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Investments') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal totalLiab = 0;
                    Decimal totalAdv = 0;
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    coverPage.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID);

                            if (balance > 0)
                            {
                                totalLiab += balance;
                                total += balance;

                            }
                            else
                            {
                                totalAdv += balance;
                                total += balance;
                            }
                        }

                        Account advances = new Account();
                        advances.Name = "Total Advances";
                        advances.Debit = totalAdv;
                        coverPage.Add(advances);

                        Account liabilities = new Account();
                        liabilities.Name = "Total Liabilities";
                        liabilities.Debit = totalLiab;
                        coverPage.Add(liabilities);

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        coverPage.Add(totalItem);
                    }
                }
            }
        }

        private void populateBalances()
        {
            balances.Clear();
            string query = "SELECT id, Status FROM viewGenAccountsGroupByPrimaryCreditSide";
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Account a = new Account();
                    int id = 0;
                    int.TryParse(dr[0].ToString(), out id);
                    Decimal balance = 0;
                    Decimal.TryParse(dr[1].ToString(), out balance);

                    a.ID = id;
                    a.Debit = balance;

                    balances.Add(a);
                }
            }
        }

        private void processProjectManagers()
        {
            //Setup head
            Account head = new Account();
            head.Name = "PMs and Site Liabilities:";
            head.typeName = "Head: ";
            coverPage.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Project Manager") + " ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int pmID = 0;
                    int.TryParse(dr[1].ToString(), out pmID);
                    acc.Name = dr[0].ToString();
                    acc.Debit = getBalance(pmID);
                    coverPage.Add(acc);
                    total += acc.Debit;

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT tblGenAccounts.accountName, tblSiteLiabAssociationWithPM.liabAccountID FROM tblSiteLiabAssociationWithPM INNER JOIN                          tblGenAccounts ON tblSiteLiabAssociationWithPM.liabAccountID = tblGenAccounts.id WHERE projectManagerID = {0}", pmID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            Account accLiab = new Account();
                            int liabID = 0;
                            int.TryParse(drLiab[1].ToString(), out liabID);
                            accLiab.ID = liabID;
                            accLiab.Name = "----" + drLiab[0].ToString();
                            accLiab.Debit = getBalance(liabID);
                            coverPage.Add(accLiab);
                            total += accLiab.Debit;
                        }
                    }

                    Account totalAcc = new Account();
                    totalAcc.Name = "--------" + acc.Name + ": Total";
                    totalAcc.Debit = total;
                    coverPage.Add(totalAcc);
                }
            }
        }

        private decimal getBalance(int pmID)
        {
            Decimal balance = 0;

            for (int i = 0; i < balances.Count; i++)
            {
                if (balances[i].ID == pmID)
                {
                    balance = balances[i].Debit;
                }
            }
            return balance;
        }

        private void processSites()
        {
            Account head = new Account();
            head.Name = "Site Status";
            head.typeName = "Head: ";
            coverPage.Add(head);

            populateSitesDataForCoverPage();
        }

        private void populateSitesDataForCoverPage()
        {
            string query = "SELECT Name, Status FROM viewActiveSitesSummary ORDER BY Name";

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Account a = new Account();

                    //a.Name = "-----" + dr[0].ToString();
                    a.Name = dr[0].ToString();
                    Decimal debit = 0;
                    Decimal.TryParse(dr[1].ToString(), out debit);
                    a.Debit = debit;

                    mainClosingList.Add(a);
                    coverPage.Add(a);
                }
            }
        }

        protected void gridCoverPage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string text1 = e.Row.Cells[0].Text;
                if (text1.Contains("&amp;"))
                {
                    text1 = text1.Replace("&amp;", "&");
                }

                bool specialRow = false;
                bool subRow = false;

                for (int i = 0; i < coverPage.Count; i++)
                {
                    if (coverPage[i].Name == text1 && coverPage[i].typeName == "Head: ")
                    {
                        specialRow = true;
                        break;
                    }
                    if (coverPage[i].Name == text1 && coverPage[i].typeName == "Sub")
                    {
                        subRow = true;
                        break;
                    }
                }

                if (specialRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 15;
                    e.Row.Cells[0].BackColor = Color.Beige;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.Beige;
                }

                if (subRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 12;
                    e.Row.Cells[0].BackColor = Color.ForestGreen;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.ForestGreen;
                }
            }
        }

        protected void btnBSCondensed_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlBSCondOK.Visible = true;
        }

        private List<Account> makeCondensedBS()
        {
            List<Account> condBS = new List<Account>();
            List<Account> finalCondBS = new List<Account>();
            //fill secondary accounts in list
            string query = "SELECT id, accountName FROM tblGenAccounts WHERE (parentAccountID = 1) OR (parentAccountID = 2) OR (parentAccountID = 3) OR (parentAccountID = 5) OR (parentAccountID = 7) ORDER BY parentAccountID, accountName";
            DataTable dt = p.GetDataTable(query);

            string parentAccountQuery = "SELECT id, parentAccountID FROM tblGenAccounts";
            Dictionary<int, int> parentAccountIDs = p.getIntIntDictionary(parentAccountQuery);
            Account plsItem = new Account();
            bool isPLSFound = false;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    Account a = new Account();

                    int id = 0;
                    int.TryParse(dr[0].ToString(), out id);
                    a.ID = id;
                    a.Name = dr[1].ToString();
                    condBS.Add(a);
                }
            }

            for (int i = 0; i < condBS.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    int parentAccID = 0;
                    parentAccountIDs.TryGetValue(list[j].ID, out parentAccID);

                    if (condBS[i].ID == parentAccID)
                    {
                        condBS[i].Debit += list[j].Debit;
                        condBS[i].Credit += list[j].Credit;
                    }

                    if (list[j].Name == "Profit or Loss" && isPLSFound == false)
                    {
                        plsItem.Name = "Profit or Loss";
                        plsItem.Debit = list[j].Debit;
                        plsItem.Credit = list[j].Credit;
                    }
                }
            }

            condBS.Add(plsItem);

            for (int i = 0; i < condBS.Count; i++)
            {
                if (condBS[i].Debit != 0 || condBS[i].Credit != 0)
                {
                    finalCondBS.Add(condBS[i]);
                }
            }
            return finalCondBS;
        }

        protected void btnBSCondOK_Click(object sender, EventArgs e)
        {
            clearLabels();
            totalCredits = totalDebits = 0;

            try
            {
                DateTime asOn = DateTime.Parse(txtDate.Text);
                getTrialBalance(asOn);

                fillList();
                adjustList();
                List<Account> condensedBS = makeCondensedBS();

                gridBS.DataSource = condensedBS;
                gridBS.DataBind();
                pnlBS.Visible = true;
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }
    }
}