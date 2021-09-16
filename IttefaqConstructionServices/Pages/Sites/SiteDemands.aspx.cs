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
    public partial class SiteDemands : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Single totalDemand = 0f;

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
                    loadGrid();
                    totalDemand = 0f;
                    txtDate.Text = p.fixDateWithMonthNames(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadGrid()
        {
            string query = "SELECT tblGenSiteRequests.date, tblGenSites.siteName, tblGenSiteRequests.requestingPerson, tblGenSiteRequests.amount, tblGenSiteRequests.description, tblGenSiteRequests.isPending, tblGenSiteRequests.id FROM tblGenSiteRequests INNER JOIN tblGenSites ON tblGenSiteRequests.siteID = tblGenSites.id WHERE tblGenSiteRequests.isPending = 'false'";
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                gridAccounts.DataSource = dt;
                gridAccounts.DataBind();
                gridAccounts.UseAccessibleHeader = true;
                gridAccounts.HeaderRow.TableSection = TableRowSection.TableHeader;

                pnlStatus.Visible = true;
            }
            else
            {
                pnlStatus.Visible = false;
            }
        }

        private void loadSites()
        {
            string ddQuery = "SELECT siteName, id FROM tblGenSites order by siteName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSite.Items.Clear();
            cmbSite.DataSource = ddItems;
            cmbSite.DataTextField = "DisplayName";
            cmbSite.DataValueField = "Value";
            cmbSite.DataBind();
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

        protected void gridAccounts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalDemand += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "amount"));
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Grand Total:";
                e.Row.Cells[5].Font.Bold = true;

                e.Row.Cells[6].Text = totalDemand.ToString("0,0.00");
                e.Row.Cells[6].Font.Bold = true;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                if (cmbSite.SelectedIndex > 0)
                {
                    int siteID = int.Parse(cmbSite.SelectedValue);
                    DateTime date = DateTime.Parse(txtDate.Text);
                    Single amount = 0f;
                    Single.TryParse(txtAmount.Text, out amount);
                    string description = p.FixString(txtDescription.Text);
                    string contactPerson = p.FixString(txtForwardedBy.Text);

                    string query = string.Format("INSERT INTO tblGenSiteRequests(siteID, date, amount, requestingPerson, description, isPending) VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", siteID, date, amount, contactPerson, description, false);
                    p.ExecuteQuery(query);
                    showSuccessMessage("Requests updated successfully.");
                    loadGrid();
                }
                else
                {
                    showWarningMessage("Please select a proper site.");
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            clearLabels();

            string query = string.Empty;

            for (int i = 0; i < gridAccounts.Rows.Count; i++)
            {
                CheckBox chkB = (CheckBox)gridAccounts.Rows[i].FindControl("CheckBox1");

                if (chkB.Checked)
                {
                    Label lbl = (Label)gridAccounts.Rows[i].FindControl("Label1");
                    int ID = int.Parse(lbl.Text);

                    string updateQuery = string.Format("  UPDATE tblGenSiteRequests SET isPending = '{0}' WHERE id = {1}", true, ID);

                    query += updateQuery;
                }
            }

            if (!query.Equals(string.Empty))
            {
                p.ExecuteTransactionQuery(query);
                loadGrid();
                showSuccessMessage("Pending status updated successfully.");
            }
            else
            {
                loadGrid();
                showWarningMessage("Please select some entry to update.");
            }
        }
    }
}