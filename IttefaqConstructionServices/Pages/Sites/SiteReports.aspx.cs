using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Sites
{
    public partial class SiteReports : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Single totalStatus = 0.0f;
        Single totalDebits = 0.0f;
        Single totalCredits = 0.0f;
        Single totalStatusSec = 0.0f;
        List<MaterialsConsumption> workingList = new List<MaterialsConsumption>();
        List<MaterialsConsumption> finalList = new List<MaterialsConsumption>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect(@"~\Pages\Membership\Login.aspx");
            }
            if (p.Authenticated(Session["UserName"].ToString(), "sites, viewer", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    //loadActiveSites();
                    //loadClosedSites();
                    laodSites();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void laodSites()
        {
            string ddQuery = "SELECT siteName, id FROM tblGenSites order by siteName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSites.Items.Clear();
            cmbSites.DataSource = ddItems;
            cmbSites.DataTextField = "DisplayName";
            cmbSites.DataValueField = "Value";
            cmbSites.DataBind();
        }

        private void refreshGrids()
        {
            try
            {
                //gridPriSitesInfo.UseAccessibleHeader = true;
                //gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                gridSecSitesInfo.UseAccessibleHeader = true;
                gridSecSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                gridTerSitesInfo.UseAccessibleHeader = true;
                gridTerSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {
            }
        }

        private void loadActiveSites()
        {
            string ddQuery = "SELECT siteName, id FROM tblGenSites Where isActive= 'true' order by siteName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbActiveSites.Items.Clear();
            cmbActiveSites.DataSource = ddItems;
            cmbActiveSites.DataTextField = "DisplayName";
            cmbActiveSites.DataValueField = "Value";
            cmbActiveSites.DataBind();
        }

        private void loadClosedSites()
        {
            string ddQuery = "SELECT siteName, id FROM tblGenSites Where isActive= 'false' order by siteName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbClosedSites.Items.Clear();
            cmbClosedSites.DataSource = ddItems;
            cmbClosedSites.DataTextField = "DisplayName";
            cmbClosedSites.DataValueField = "Value";
            cmbClosedSites.DataBind();
        }

        protected void btnActiveSites_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePrimaryPenals();
            hideSecondaryPenals();
            hideTertiaryPenals();
            pnlTransaction.Visible = false;

            try
            {
                bool active = true;
                loadSitesInfoGrid(active);

                pnlPriSiteInfo.Visible = true;
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void btnClosedSites_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePrimaryPenals();
            hideSecondaryPenals();
            hideTertiaryPenals();
            pnlTransaction.Visible = false;

            try
            {
                bool active = false;
                loadSitesInfoGrid(active);

                pnlPriSiteInfo.Visible = true;
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
                refreshGrids();
            }
        }

        private void loadSitesInfoGrid(bool active)
        {
            if (active == true)
            {
                DataTable dt = p.GetDataTable("select  id, Name, Status from viewActiveSitesSummary");
                gridPriSitesInfo.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    gridPriSitesInfo.DataBind();
                    //gridPriSitesInfo.UseAccessibleHeader = true;
                    //gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            else
            {
                DataTable dt = p.GetDataTable("select  id, Name, Status from viewClosedSitesSummary");
                gridPriSitesInfo.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    gridPriSitesInfo.DataBind();
                    gridPriSitesInfo.UseAccessibleHeader = true;
                    gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void hidePenals()
        {
            pnlMaterialsConsumption.Visible = pnlMaterialsConsumptionReport.Visible = pnlMaterialsConsumptionDetailedReport.Visible = pnlTransaction.Visible = false;
        }

        private void hideTertiaryPenals()
        {
            pnlTerSiteInfo.Visible = false;
        }

        private void hideSecondaryPenals()
        {
            pnlSecSiteInfo.Visible = false;
        }

        private void hidePrimaryPenals()
        {
            pnlPriActiveSites.Visible = pnlPriClosedSites.Visible = pnlPriSiteInfo.Visible = false;
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

        protected void cmbActiveSites_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void cmbClosedSites_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void gridPriSitesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hideSecondaryPenals();
            hideTertiaryPenals();
            try
            {
                int siteID = int.Parse(e.CommandArgument.ToString());
                string siteNameQuery = string.Format("SELECT siteName FROM tblGenSites WHERE id = {0}", siteID);
                Session["siteName"] = p.getStringValue(siteNameQuery);
                loadSecInfoGrid(siteID);
                pnlSecSiteInfo.Visible = true;

                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                gridSecSitesInfo.UseAccessibleHeader = true;
                gridSecSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void loadSecInfoGrid(int siteID)
        {
            hideSecondaryPenals();
            hideTertiaryPenals();
            pnlSecSiteInfo.Visible = true;

            string query = string.Format("select  SiteID, Account, accID, Status from viewSecSiteAccountInfo WHERE siteID = {0}", siteID);

            DataTable dt = p.GetDataTable(query);
            gridSecSitesInfo.DataSource = null;
            gridSecSitesInfo.DataBind();

            gridSecSitesInfo.DataSource = dt;
            gridSecSitesInfo.DataBind();
        }

        protected void gridSecSitesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hideTertiaryPenals();
            try
            {
                int siteID = 0;
                int accountID = 0;

                string argString = e.CommandArgument.ToString();
                
                string[] args = new string[2];
                args = e.CommandArgument.ToString().Split(';');

                int.TryParse(args[0], out accountID);
                int.TryParse(args[1], out siteID);
                string accountNameQuery = string.Format("SELECT accountName FROM tblGenAccounts WHERE id = {0}", accountID);
                Session["siteAccountName"] = p.getStringValue(accountNameQuery);
                loadTerInfoGrid(siteID, accountID);
                pnlTerSiteInfo.Visible = true;

                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                gridSecSitesInfo.UseAccessibleHeader = true;
                gridSecSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                //gridTerSitesInfo.UseAccessibleHeader = true;
                //gridTerSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);

                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                gridSecSitesInfo.UseAccessibleHeader = true;
                gridSecSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void loadTerInfoGrid(int siteID, int accountID)
        {
            hideTertiaryPenals();

            string query = string.Format("SELECT date, transactionID, description, quantity, UoM, rate, debitAmount, creditAmount FROM tblGenAccountsTransactions where accountid = {0} and siteid = {1} order by date", accountID, siteID);

            DataTable dt = p.GetDataTable(query);
            gridTerSitesInfo.DataSource = null;
            gridTerSitesInfo.DataBind();

            gridTerSitesInfo.DataSource = dt;
            gridTerSitesInfo.DataBind();
        }

        protected void gridPriSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("viewSecDetails");

                if (lnkBtn != null)
                {
                    totalStatus += Single.Parse(lnkBtn.Text);
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

        protected void gridSecSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("viewTertiaryDetails");

                if (lnkBtn != null)
                {
                    totalStatusSec += Single.Parse(lnkBtn.Text);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = totalStatusSec.ToString("0,0.00");
                e.Row.Cells[1].Font.Bold = true;
            }
        }

        protected void gridTerSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDb = (Label)e.Row.FindControl("lblDebit");
                Label lblCr = (Label)e.Row.FindControl("lblCredit");

                if (lblDb != null)
                {
                    totalDebits += Single.Parse(lblDb.Text);
                }

                if (lblCr != null)
                {
                    totalCredits += Single.Parse(lblCr.Text);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total";
                e.Row.Cells[2].Font.Bold = true;

                e.Row.Cells[7].Text = totalDebits.ToString("0,0.00");
                e.Row.Cells[7].Font.Bold = true;

                e.Row.Cells[8].Text = totalCredits.ToString("0,0.00");
                e.Row.Cells[8].Font.Bold = true;
            }
        }

        protected void btnPntTrialBalance_Click(object sender, EventArgs e)
        {
            refreshGrids();
        }

        protected void btnMaterialsConsumption_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePrimaryPenals();
            hideSecondaryPenals();
            hideTertiaryPenals();
            hidePenals();
            pnlMaterialsConsumption.Visible = true;
        }

        protected void cmbSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSites.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbSites.SelectedValue);
                    int parentAccountID = p.getIntValue("SELECT id FROM tblGenAccounts WHERE (accountName LIKE '%mater%') AND (parentAccountID = 7)");

                    string accountsIDQuery = string.Format("SELECT id FROM tblGenAccounts WHERE (parentAccountID = {0})", parentAccountID);
                    List<int> listAccounts = p.getIntList(accountsIDQuery);

                    for (int i = 0; i < listAccounts.Count; i++)
                    {
                        int accountID = listAccounts[i];
                        string accountNameQuery = string.Format("SELECT accountName FROM tblGenAccounts WHERE (ID = {0})", accountID);
                        string accountName = p.getStringValue(accountNameQuery);
                        processReport(accountID, siteID, accountName);
                        gridMatConsumption.DataSource = finalList;
                        gridMatConsumption.DataBind();
                        pnlMaterialsConsumptionReport.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        private void processReport(int accountID, int siteID, string accountName)
        {
            string query = string.Format("SELECT quantity, UoM, rate, debitAmount, creditAmount FROM tblGenAccountsTransactions WHERE accountID = {0} AND siteID = {1}", accountID, siteID);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                Single matQty = 0f;
                Single matAmt = 0f;
                string UoM = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Single q = 0f;
                    Single.TryParse(dr[0].ToString(), out q);
                    matQty += q;

                    Single am = 0f;
                    Single.TryParse(dr[3].ToString(), out am);

                    Single cr = 0f;
                    Single.TryParse(dr[4].ToString(), out cr);

                    if (cr > 0f)
                    {
                        matAmt -= am;
                    }
                    else
                    {
                        matAmt += am;
                    }

                    UoM = dr[1].ToString().ToLower();                    
                }

                Single average = 0f;

                if (matQty > 0f)
                {
                    average = matAmt / matQty;
                }

                MaterialsConsumption mc = new MaterialsConsumption();
                mc.accountName = accountName;
                mc.quantity = matQty;
                mc.UoM = UoM;
                mc.amount = matAmt;
                mc.averageRate = average;

                finalList.Add(mc);
            }
        }

        protected void gridTerSitesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int transID = int.Parse(e.CommandArgument.ToString());

                if (transID != 0)
                {
                    string query = string.Format("SELECT tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.chequeNumber, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id WHERE (tblGenAccountsTransactions.transactionID = {0})", transID);
                    DataTable dt = p.GetDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        gridTransaction.DataSource = dt;
                        gridTransaction.DataBind();
                        pnlTransaction.Visible = true;
                        refreshGrids();
                    }
                }
                else
                {
                    showWarningMessage("Not a valid transaction. Please choose another one.");
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void btnDetailedMaterialConsumption_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            hidePrimaryPenals();
            hideSecondaryPenals();
            hideTertiaryPenals();
            pnlMaterialsConsumption.Visible = true;

            if (cmbSites.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbSites.SelectedValue);
                    int parentAccountID = p.getIntValue("SELECT id FROM tblGenAccounts WHERE (accountName LIKE '%mater%') AND (parentAccountID = 7)");

                    string accountsIDQuery = string.Format("SELECT id FROM tblGenAccounts WHERE (parentAccountID = {0})", parentAccountID);
                    List<int> listAccounts = p.getIntList(accountsIDQuery);

                    for (int i = 0; i < listAccounts.Count; i++)
                    {
                        int accountID = listAccounts[i];
                        string accountNameQuery = string.Format("SELECT accountName FROM tblGenAccounts WHERE (ID = {0})", accountID);
                        string accountName = p.getStringValue(accountNameQuery);
                        processDetailedReport(accountID, siteID, accountName);
                        gridMatConsumptionDetailed.DataSource = finalList;
                        gridMatConsumptionDetailed.DataBind();
                        pnlMaterialsConsumptionDetailedReport.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        private void processDetailedReport(int accountID, int siteID, string accountName)
        {
            string query = string.Format("SELECT quantity, UoM, rate, debitAmount, creditAmount, date FROM tblGenAccountsTransactions WHERE accountID = {0} AND siteID = {1}", accountID, siteID);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Single matQty = 0f;
                    string UoM = dr[1].ToString().ToLower();
                    Single rate = 0f;
                    Single matDebit = 0f;
                    Single matCredit = 0f;
                    DateTime date;

                    Single.TryParse(dr[0].ToString(), out matQty);
                    Single.TryParse(dr[2].ToString(), out rate);
                    Single.TryParse(dr[3].ToString(), out matDebit);
                    Single.TryParse(dr[4].ToString(), out matCredit);
                    DateTime.TryParse(dr[5].ToString(), out date);
                    
                    MaterialsConsumption mc = new MaterialsConsumption();
                    mc.accountName = accountName;
                    mc.quantity = matQty;
                    mc.UoM = UoM;
                    mc.debit = matDebit;
                    mc.credit = matCredit;
                    mc.date = date;
                    finalList.Add(mc);
                }
            }
        }
    }
}