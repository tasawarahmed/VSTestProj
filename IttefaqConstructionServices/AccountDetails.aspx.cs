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
    public partial class AccountDetails : System.Web.UI.Page
    {
        string command = string.Empty;
        int id = 0;
        bool account = false;
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
            account = false;
            if (command.Contains("Site"))
            {
                Session["accountDetails"] = "Sites Information";
            }
            else if (command.Contains("AI"))
            {
                string[] commands = command.Split(':');
                int.TryParse(commands[1], out id);
                Session["accountDetails"] = "Account Information";
                account = true;
                loadSecInfoGrid(id);
                //loadAccInfoGrid(id);
            }
            else
            {
                string[] commands = command.Split(':');
                int.TryParse(commands[1], out id);
                Session["accountDetails"] = "Account Group Information";
                loadPrimaryGrid();
                
            }
        }

        private void loadPrimaryGrid()
        {
            hidePanels();
            int parentID = id;
            int parentAccountID = p.getParentAccountID(parentID);
            string query = string.Empty;

            if (parentAccountID == 1 || parentAccountID == 6)
            {
                query = string.Format("select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = {0}", parentID);
            }

            else
            {
                query = string.Format("select id, Name, Status from viewGenAccountsGroupByPrimaryCreditSide Where ParentID = {0}", parentID);
            }

            DataTable dt = p.GetDataTable(query);
            gridPriSitesInfo.DataSource = dt;
            gridPriSitesInfo.DataBind();
            pnlPriSiteInfo.Visible = true;

            if (dt.Rows.Count > 0)
            {
                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void hidePanels()
        {
            pnlPriSiteInfo.Visible =
                pnlSecSiteInfo.Visible = 
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

        protected void gridPriSitesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hidePanels();
            //hideSecondaryPenals();
            //hideTertiaryPenals();
            try
            {
                int siteID = int.Parse(e.CommandArgument.ToString());
                string siteNameQuery = string.Format("SELECT accountName from tblGenAccounts WHERE id = {0}", siteID);
                Session["accountName"] = p.getStringValue(siteNameQuery);

                loadSecInfoGrid(siteID);
                refreshGrids();
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
            try
            {
                if (account)
                {
                    pnlSecSiteInfo.Visible = true;
                    string siteNameQuery = string.Format("SELECT accountName from tblGenAccounts WHERE id = {0}", siteID);
                    Session["accountName"] = p.getStringValue(siteNameQuery);
                }
                else
                {
                    pnlSecSiteInfo.Visible = true;
                    pnlPriSiteInfo.Visible = true;

                }
                string query = string.Format("SELECT date AS Date, transactionID, description AS Description, chequeNumber AS Cheque, debitAmount, creditAmount FROM tblGenAccountsTransactions WHERE accountID = {0} ORDER BY Date, transactionID", siteID);

                DataTable dt = p.GetDataTable(query);
                gridSecSitesInfo.DataSource = null;
                gridSecSitesInfo.DataBind();

                gridSecSitesInfo.DataSource = dt;
                gridSecSitesInfo.DataBind();
                gridSecSitesInfo.UseAccessibleHeader = true;
                gridSecSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {
            }
        }

        protected void gridSecSitesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hidePanels();
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
                        pnlPriSiteInfo.Visible = 
                            pnlSecSiteInfo.Visible =
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
                e.Row.Cells[2].Font.Size = 10;
                e.Row.Cells[3].Font.Size = 10;
                e.Row.Cells[4].Font.Size = 10;
                e.Row.Cells[5].Font.Size = 10;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        protected void btnPntTrialBalance_Click(object sender, EventArgs e)
        {
            refreshGrids();
        }

        private void refreshGrids()
        {
            try
            {
                //gridPriSitesInfo.UseAccessibleHeader = true;
                //gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                gridSecSitesInfo.UseAccessibleHeader = true;
                gridSecSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                //gridTerSitesInfo.UseAccessibleHeader = true;
                //gridTerSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {
            }
        }
    }
}