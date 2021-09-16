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
    public partial class NewUser : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "administrator, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadGrid();
                    loadUsers();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadUsers()
        {
            string users = "SELECT [userName] FROM [tblUsers] WHERE [isHidden] = 'false' AND [isActive] = 'true' ORDER BY [userName] ";
            DataTable dt = p.GetDataTable(users);

            if (dt.Rows.Count > 0)
            {
                gridUsers.DataSource = dt;
                gridUsers.DataBind();

                gridUsers.UseAccessibleHeader = true;
                gridUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            string query = "SELECT id, roleName AS [Role Name] FROM tblUserRoles WHERE isactive = 'true' ORDER BY Remarks, [Role Name]";
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
                string userName = p.FixString(txtUserRole.Text.Trim().ToLower());
                string recordQuery = string.Format("SELECT * FROM tblUsers WHERE userName = '{0}'", userName);

                if (!p.ifRecordsExist(recordQuery))
                {
                    string password = p.FixString(txtPassword.Text);
                    string encryptedPassword = p.encryptPassword(password);
                    string roles = getRoles();
                    bool status = true;

                    if (!roles.Equals(string.Empty))
                    {
                        //user password is not included in calculating checksum.
                        string checkSum = p.getCheckSum(userName, roles, status.ToString());
                        string insertQuery = string.Format("INSERT INTO tblUsers (userName, userPassword, userRoles, userCheckSum, isActive, isHidden) VALUES ('{0}','{1}','{2}','{3}','{4}', '{5}')", userName, encryptedPassword, roles, checkSum, status, false);
                        p.ExecuteQuery(insertQuery);
                        showSuccessMessage("User created successfully.");
                        loadGrid();
                        loadUsers();
                    }
                    else
                    {
                        showWarningMessage("You must select at least one role.");
                    }
                }
                else
                {
                    showWarningMessage("User already exists.");
                    loadGrid();
                    loadUsers();
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
                loadGrid();
            }
        }

        private string getRoles()
        {
            string roles = string.Empty;

            for (int i = 0; i < gridTerSitesInfo.Rows.Count; i++)
            {
                CheckBox chkBoxChk = (CheckBox)gridTerSitesInfo.Rows[i].FindControl("chkBx");

                if (chkBoxChk.Checked)
                {
                    Label rolName = (Label)gridTerSitesInfo.Rows[i].FindControl("lblRllName");

                    string rollName = rolName.Text + " ";

                    roles += rollName;
                }                
            }
            return roles;
        }
    }
}