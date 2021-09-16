using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Membership
{
    public partial class UserRoles : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "administrator, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadGrid();
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

        private void loadGrid()
        {
            string query = "SELECT roleName AS [Role Name] FROM tblUserRoles ORDER BY [Role Name]";
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                gridTerSitesInfo.DataSource = dt;
                gridTerSitesInfo.DataBind();
                gridTerSitesInfo.UseAccessibleHeader = true;
                gridTerSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                string userRole = p.FixString(txtUserRole.Text.Trim().ToLower());

                string query = string.Format("SELECT roleName, remarks FROM tblUserRoles WHERE roleName = '{0}'", userRole);

                if (!p.ifRecordsExist(query))
                {
                    string insertQuery = string.Format("INSERT INTO tblUserRoles (roleName, remarks) VALUES ('{0}','{1}')", userRole, string.Empty);

                    p.ExecuteQuery(insertQuery);
                    showSuccessMessage("Role created successfully.");

                    loadGrid();
                }
                else
                {
                    loadGrid();
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
                loadGrid();
            }
        }
    }
}