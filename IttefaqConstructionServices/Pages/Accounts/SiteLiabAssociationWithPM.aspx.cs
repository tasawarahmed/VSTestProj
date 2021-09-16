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
    public partial class SiteLiabAssociationWithPM : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        List<Account> liabilities = new List<Account>();

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
                    loadExistingAssociations();
                    loadRevisedAssociations();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
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

        private void loadRevisedAssociations()
        {
            clearLabels();
            try
            {
                string query = "SELECT id, accountName FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Site Liabilities") + " ORDER BY accountName ";
                DataTable dt = p.GetDataTable(query);
                gridReviseAssociation.DataSource = dt;
                gridReviseAssociation.DataBind();
            }

            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void loadExistingAssociations()
        {
            clearLabels();
            try
            {
                string query = "SELECT tblGenAccounts.accountName AS Liability, tblGenAccounts_1.accountName AS PM FROM tblSiteLiabAssociationWithPM INNER JOIN tblGenAccounts ON tblSiteLiabAssociationWithPM.liabAccountID = tblGenAccounts.id INNER JOIN tblGenAccounts AS tblGenAccounts_1 ON tblSiteLiabAssociationWithPM.projectManagerID = tblGenAccounts_1.id";
                DataTable dt = p.GetDataTable(query);
                gridCurrentAssociation.DataSource = dt;
                gridCurrentAssociation.DataBind();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void gridReviseAssociation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string ddQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Project Manager") + " ORDER BY accountName ";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList cmbProjectManagers = (DropDownList)e.Row.FindControl("cmbProjectManagers");
                cmbProjectManagers.DataSource = ddItems;
                cmbProjectManagers.DataTextField = "DisplayName";
                cmbProjectManagers.DataValueField = "Value";
                cmbProjectManagers.DataBind();
            }
        }

        protected void btnReviseAssociation_Click(object sender, EventArgs e)
        {
            string query = string.Empty;

            for (int i = 0; i < gridReviseAssociation.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gridReviseAssociation.Rows[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                    DropDownList ddl = (DropDownList)gridReviseAssociation.Rows[i].FindControl("cmbProjectManagers");

                    if (ddl.SelectedIndex > 0)
                    {
                        Label lbl = (Label)gridReviseAssociation.Rows[i].FindControl("lblID");
                        int liabAccID = 0;
                        int projectManagerID = int.Parse(ddl.SelectedValue.ToString());
                        int.TryParse(lbl.Text, out liabAccID);

                        query += string.Format(" DELETE FROM tblSiteLiabAssociationWithPM WHERE (liabAccountID = {0})", liabAccID);
                        query += string.Format("  INSERT INTO tblSiteLiabAssociationWithPM (liabAccountID, projectManagerID) VALUES ({0},{1})", liabAccID, projectManagerID);
                    }
                }
            }
            p.ExecuteTransactionQuery(query);
            loadExistingAssociations();
        }
    }
}