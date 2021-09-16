using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices
{
    public partial class SiteDetails : System.Web.UI.Page
    {
        string command = string.Empty;
        int id = 0;
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["command"] != null)
            {
                command = Request.QueryString["command"];
                processCommand();
            }
        }

        private void processCommand()
        {
            if (command.Contains("Site"))
            {
                Session["accountDetails"] = "Sites Information";
                loadSitesInfoGrid(true);
            }
            else if (command.Contains("AI"))
            {
                string[] commands = command.Split(':');
                int.TryParse(commands[1], out id);
                Session["accountDetails"] = "Account Information";
            }
            else
            {
                string[] commands = command.Split(':');
                int.TryParse(commands[1], out id);
                Session["accountDetails"] = "Account Group Information";
            }
        }

        private void loadSitesInfoGrid(bool active)
        {
            hidePanels();
            if (active == true)
            {
                DataTable dt = p.GetDataTable("select  id, Name, Status from viewActiveSitesSummary ORDER BY Name");
                gridPriSitesInfo.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    gridPriSitesInfo.DataBind();
                    gridPriSitesInfo.UseAccessibleHeader = true;
                    gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
                    pnlPriSiteInfo.Visible = true;
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
                    pnlPriSiteInfo.Visible = true;
                }
            }
        }

        private void hidePanels()
        {
            pnlPriSiteInfo.Visible =
                pnlSecSiteInfo.Visible =
                pnlTerSiteInfo.Visible =
                pnlTransaction.Visible = false;
        }

        //private void hideTertiaryPenals()
        //{
        //    pnlTerSiteInfo.Visible = false;
        //}

        //private void hideSecondaryPenals()
        //{
        //    pnlSecSiteInfo.Visible = false;
        //}

        //private void hidePrimaryPenals()
        //{
        //    pnlPriSiteInfo.Visible = false;
        //}

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
            hidePanels();
            //hideSecondaryPenals();
            //hideTertiaryPenals();
            try
            {
                int siteID = int.Parse(e.CommandArgument.ToString());
                string siteNameQuery = string.Format("SELECT siteName FROM tblGenSites WHERE id = {0}", siteID);
                Session["siteName"] = p.getStringValue(siteNameQuery);
                loadSecInfoGrid(siteID);
                pnlSecSiteInfo.Visible =
                    pnlPriSiteInfo.Visible = true;

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
            hidePanels();
            //hideSecondaryPenals();
            //hideTertiaryPenals();
            pnlPriSiteInfo.Visible =
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
            hidePanels();
            //hideTertiaryPenals();
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
                pnlPriSiteInfo.Visible =
                    pnlSecSiteInfo.Visible =
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
            hidePanels();
            pnlPriSiteInfo.Visible =
                pnlSecSiteInfo.Visible =
                pnlTerSiteInfo.Visible = true;
            //hideTertiaryPenals();

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
                e.Row.Cells[0].Font.Size = 10;
                e.Row.Cells[1].Font.Size = 10;
            }
        }

        protected void gridSecSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Font.Size = 10;
                e.Row.Cells[1].Font.Size = 10;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        protected void gridTerSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Font.Size = 10;
                e.Row.Cells[1].Font.Size = 10;
                e.Row.Cells[2].Font.Size = 10;
                e.Row.Cells[3].Font.Size = 10;
                e.Row.Cells[4].Font.Size = 10;
                e.Row.Cells[5].Font.Size = 10;
                e.Row.Cells[6].Font.Size = 10;
                e.Row.Cells[7].Font.Size = 10;
                e.Row.Cells[8].Font.Size = 10;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        protected void btnPntTrialBalance_Click(object sender, EventArgs e)
        {
            refreshGrids();
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
                        hidePanels();
                        gridTransaction.DataSource = dt;
                        gridTransaction.DataBind();
                        pnlPriSiteInfo.Visible =
                            pnlSecSiteInfo.Visible =
                            pnlTerSiteInfo.Visible =
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
    }
}