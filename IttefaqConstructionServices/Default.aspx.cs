using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices
{
    public partial class Default : System.Web.UI.Page
    {
        //Utilities p = new Utilities();
        ////List<Account> data = new List<Account>();
        //Decimal totalStatus = 0;
        //Decimal totalDebits = 0;
        //Decimal totalCredits = 0;
        List<Account> data = new List<Account>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //loadCoverPage();                
                //loadActiveAccountsList();
            }
        }


        //private void loadCoverPage()
        //{
        //    try
        //    {
        //        data.Clear();
        //        string query = "SELECT typeName, name, amount FROM tblTmpCoverPage ORDER BY id";
        //        DataTable dt = p.GetDataTable(query);

        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                DataRow dr = dt.Rows[i];
        //                Account acc = new Account();

        //                acc.typeName = dr[0].ToString();
        //                acc.Name = dr[1].ToString();
        //                acc.Debit = Decimal.Parse(dr[2].ToString());
        //                data.Add(acc);
        //            }                    
        //        }
        //        gridCoverPage.DataSource = data;
        //        gridCoverPage.DataBind();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //protected void gridCoverPage_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string text1 = e.Row.Cells[0].Text;
        //        if (text1.Contains("&amp;"))
        //        {
        //            text1 = text1.Replace("&amp;", "&");
        //        }

        //        bool specialRow = false;
        //        bool subRow = false;

        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            if (data[i].Name == text1 && data[i].typeName == "Head: ")
        //            {
        //                specialRow = true;
        //                break;
        //            }
        //            if (data[i].Name == text1 && data[i].typeName == "Sub")
        //            {
        //                subRow = true;
        //                break;
        //            }
        //        }

        //        if (specialRow)
        //        //if (e.Row.Cells[0].Text.StartsWith("::"))
        //        {
        //            e.Row.Cells[0].Font.Bold = true;
        //            e.Row.Cells[0].Font.Size = 15;
        //            e.Row.Cells[0].ForeColor = Color.White;
        //            e.Row.Cells[0].BackColor = Color.DarkBlue;
        //            e.Row.Cells[1].Text = string.Empty;
        //            e.Row.Cells[1].BackColor = Color.DarkBlue;
        //        }

        //        else if (subRow)
        //        //if (e.Row.Cells[0].Text.StartsWith("::"))
        //        {
        //            e.Row.Cells[0].Font.Bold = true;
        //            e.Row.Cells[0].Font.Size = 12;
        //            e.Row.Cells[0].BackColor = Color.ForestGreen;
        //            e.Row.Cells[1].Text = string.Empty;
        //            e.Row.Cells[1].BackColor = Color.ForestGreen;
        //        }
        //        else
        //        {
        //            e.Row.Cells[0].Font.Size = 8;
        //            e.Row.Cells[0].BackColor = Color.White;
        //            e.Row.Cells[1].Font.Size = 8;
        //            e.Row.Cells[1].BackColor = Color.White;
        //        }
        //    }
        //}

        //private void loadActiveAccountsList()
        //{
        //    string ddQuery = "SELECT accountName, id FROM tblGenAccounts WHERE ((parentAccountID = 1) OR (parentAccountID = 2) OR (parentAccountID = 3) OR (parentAccountID = 4) OR (parentAccountID = 6)) AND (isActive = 'true') ORDER BY accountName";

        //    List<DDList> ddItems = new List<DDList>();

        //    //Adding my own items in ddList
        //    //List<DDList> myOwnList = new List<DDList>();

        //    //DDList dd = new DDList();
        //    //dd.DisplayName = "Cash Book";
        //    //dd.Value = 1000;

        //    //myOwnList.Add(dd);

        //    //ddItems = p.getDDList(myOwnList, ddQuery);
        //    ddItems = p.getDDList(ddQuery);

        //    cmbAccounts.Items.Clear();
        //    cmbAccounts.DataSource = ddItems;
        //    cmbAccounts.DataTextField = "DisplayName";
        //    cmbAccounts.DataValueField = "Value";
        //    cmbAccounts.DataBind();
        //}

        //private void loadAccountsList()
        //{
        //    string ddQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 1) OR (parentAccountID = 2) OR (parentAccountID = 3) OR (parentAccountID = 4) OR (parentAccountID = 6) ORDER BY accountName";

        //    List<DDList> ddItems = new List<DDList>();
        //    ddItems = p.getDDList(ddQuery);

        //    cmbAccounts.Items.Clear();
        //    cmbAccounts.DataSource = ddItems;
        //    cmbAccounts.DataTextField = "DisplayName";
        //    cmbAccounts.DataValueField = "Value";
        //    cmbAccounts.DataBind();
        //}

        //private void refreshGrids()
        //{
        //    try
        //    {
        //        gridPriSitesInfo.UseAccessibleHeader = true;
        //        gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        gridSecSitesInfo.UseAccessibleHeader = true;
        //        gridSecSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void loadSitesInfoGrid(bool active)
        //{
        //    try
        //    {
        //        if (active == true)
        //        {
        //            int parentID = int.Parse(cmbAccounts.SelectedValue);
        //            int parentAccountID = p.getParentAccountID(parentID);
        //            string query = string.Empty;

        //            if (parentAccountID == 1 || parentAccountID == 6)
        //            {
        //                query = string.Format("select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = {0}", parentID);
        //            }

        //            else
        //            {
        //                query = string.Format("select id, Name, Status from viewGenAccountsGroupByPrimaryCreditSide Where ParentID = {0}", parentID);
        //            }

        //            DataTable dt = p.GetDataTable(query);
        //            gridPriSitesInfo.DataSource = dt;
        //            gridPriSitesInfo.DataBind();

        //            if (dt.Rows.Count > 0)
        //            {
        //                gridPriSitesInfo.UseAccessibleHeader = true;
        //                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            }
        //        }
        //        else
        //        {
        //            DataTable dt = p.GetDataTable("select id, Name, Status from viewClosedSitesSummary");
        //            gridPriSitesInfo.DataSource = dt;
        //            gridPriSitesInfo.DataBind();
        //            gridPriSitesInfo.UseAccessibleHeader = true;
        //            gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        showWarningMessage(ex.Message);
        //    }
        //}

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

        //private void showWarningMessage(string s)
        //{
        //    lblWarning.Visible = true;
        //    lblWarning.Text = p.getWarningMessage(s);
        //}

        //private void showSuccessMessage(string s)
        //{
        //    lblSuccess.Visible = true;
        //    lblSuccess.Text = p.getSuccessMessage(s);
        //}

        //private void clearLabels()
        //{
        //    lblSuccess.Visible = lblWarning.Visible = false;
        //}

        //protected void gridPriSitesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    hideSecondaryPenals();
        //    hideTertiaryPenals();
        //    try
        //    {
        //        int siteID = int.Parse(e.CommandArgument.ToString());
        //        string siteNameQuery = string.Format("SELECT accountName from tblGenAccounts WHERE id = {0}", siteID);
        //        Session["accountName"] = p.getStringValue(siteNameQuery);

        //        loadSecInfoGrid(siteID);
        //        pnlSecSiteInfo.Visible = true;
        //        refreshGrids();
        //    }
        //    catch (Exception ex)
        //    {
        //        showWarningMessage(ex.Message);
        //    }
        //}

        //private void loadSecInfoGrid(int siteID)
        //{
        //    try
        //    {
        //        hideSecondaryPenals();
        //        hideTertiaryPenals();
        //        pnlSecSiteInfo.Visible = true;
        //        string query = string.Format("SELECT date AS Date, transactionID, description AS Description, chequeNumber AS Cheque, debitAmount, creditAmount FROM tblGenAccountsTransactions WHERE accountID = {0} ORDER BY Date", siteID);

        //        DataTable dt = p.GetDataTable(query);
        //        gridSecSitesInfo.DataSource = null;
        //        gridSecSitesInfo.DataBind();

        //        gridSecSitesInfo.DataSource = dt;
        //        gridSecSitesInfo.DataBind();
        //        gridSecSitesInfo.UseAccessibleHeader = true;
        //        gridSecSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void loadCashBook()
        //{
        //    string cashInHandQuery = "SELECT id FROM tblGenAccounts WHERE accountName = 'Cash in Hand'";
        //    int cashInHandAccount = p.getIntValue(cashInHandQuery);

        //    string cashAtBankQuery = "SELECT id FROM tblGenAccounts WHERE accountName = 'Cash at Bank'";
        //    int cashAtBankAccount = p.getIntValue(cashAtBankQuery);

        //    string query = string.Format("select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = {0} OR ParentID = {1}", cashAtBankAccount, cashInHandAccount);
        //    DataTable dt = p.GetDataTable(query);
        //    gridPriSitesInfo.DataSource = dt;
        //    gridPriSitesInfo.DataBind();

        //    if (dt.Rows.Count > 0)
        //    {
        //        gridPriSitesInfo.UseAccessibleHeader = true;
        //        gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }
        //}

        //protected void cmbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}

        //private void hidePenals()
        //{
        //    pnlTransaction.Visible = false;
        //}

        //protected void gridPriSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        LinkButton lnkBtn = (LinkButton)e.Row.FindControl("viewSecDetails");

        //        if (lnkBtn != null)
        //        {
        //            totalStatus += Decimal.Parse(lnkBtn.Text);
        //        }
        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.Cells[0].Text = "Total";
        //        e.Row.Cells[0].Font.Bold = true;

        //        e.Row.Cells[1].Text = totalStatus.ToString("0,0.00");
        //        e.Row.Cells[1].Font.Bold = true;
        //    }
        //}

        //protected void gridSecSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblDb = (Label)e.Row.FindControl("lblDebit");
        //        Label lblCr = (Label)e.Row.FindControl("lblCredit");

        //        if (lblDb != null)
        //        {
        //            totalDebits += Decimal.Parse(lblDb.Text);
        //        }

        //        if (lblCr != null)
        //        {
        //            totalCredits += Decimal.Parse(lblCr.Text);
        //        }
        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.Cells[2].Text = "Total";
        //        e.Row.Cells[2].Font.Bold = true;

        //        e.Row.Cells[4].Text = totalDebits.ToString("0,0.00");
        //        e.Row.Cells[4].Font.Bold = true;

        //        e.Row.Cells[5].Text = totalCredits.ToString("0,0.00");
        //        e.Row.Cells[5].Font.Bold = true;
        //    }
        //}

        //protected void btnPntTrialBalance_Click(object sender, EventArgs e)
        //{
        //    refreshGrids();
        //}

        //protected void gridSecSitesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        int transID = int.Parse(e.CommandArgument.ToString());

        //        if (transID != 0)
        //        {
        //            string query = string.Format("SELECT tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.chequeNumber, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id WHERE (tblGenAccountsTransactions.transactionID = {0})", transID);
        //            DataTable dt = p.GetDataTable(query);

        //            if (dt.Rows.Count > 0)
        //            {
        //                gridTransaction.DataSource = dt;
        //                gridTransaction.DataBind();
        //                pnlTransaction.Visible = true;
        //                refreshGrids();
        //            }
        //        }
        //        else
        //        {
        //            showWarningMessage("Not a valid transaction. Please choose another one.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        showWarningMessage(ex.Message);
        //    }
        //}

        //protected void btnAccountsOK_Click(object sender, EventArgs e)
        //{
        //    clearLabels();
        //    hidePrimaryPenals();
        //    hideSecondaryPenals();
        //    hideTertiaryPenals();
        //    hidePenals();

        //    if (cmbAccounts.SelectedIndex > 0)
        //    {
        //        try
        //        {
        //            int value = int.Parse(cmbAccounts.SelectedValue);

        //            if (value == 1000)
        //            {
        //                loadCashBook();
        //                pnlPriSiteInfo.Visible = true;
        //            }
        //            else
        //            {
        //                bool active = true;
        //                loadSitesInfoGrid(active);
        //                pnlPriSiteInfo.Visible = true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            showWarningMessage(ex.Message);
        //        }
        //    }
        //}
    }
}