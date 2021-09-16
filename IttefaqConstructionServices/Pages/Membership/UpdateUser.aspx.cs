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
    public partial class UpdateUser : System.Web.UI.Page
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
                    loadPMs();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadPMs()
        {
            string parentQuery = "SELECT id FROM tblGenAccounts WHERE (specialNotes = 'Project Manager')";
            List<int> PMParents = p.getIntList(parentQuery);

            string query = string.Empty;
            string whereClause = "Where ";

            for (int i = 0; i < PMParents.Count; i++)
            {
                query = "Select accountName, id from tblgenaccounts ";
                int ID = PMParents[i];
                whereClause += " (parentAccountID = " + ID.ToString() + ") ";

                if ((i + 1) < PMParents.Count) 
                {
                    whereClause += " OR";
                }                
            }

            if (query != string.Empty && !whereClause.Equals("Where "))
            {
                query = query + whereClause;
                List<DDList> PMs = new List<DDList>();
                PMs = p.getDDList(query);

                cmbPMs.DataSource = PMs;
                cmbPMs.DataTextField = "DisplayName";
                cmbPMs.DataValueField = "Value";
                cmbPMs.DataBind();
            }
        }

        private void loadUsers()
        {
            string queryPrimaryAccounts = "SELECT userName, id FROM tblUsers WHERE (isActive = 'true') AND (isHidden = 'false')";

            List<DDList> primaryAccounts = new List<DDList>();
            primaryAccounts = p.getDDList(queryPrimaryAccounts);

            cmbActiveUsers.DataSource = primaryAccounts;
            cmbActiveUsers.DataTextField = "DisplayName";
            cmbActiveUsers.DataValueField = "Value";
            cmbActiveUsers.DataBind();
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

        private string getRoles()
        {
            string roles = string.Empty;

            for (int i = 0; i < gridTerSitesInfo.Rows.Count; i++)
            {
                CheckBox chkBoxChk = (CheckBox)gridTerSitesInfo.Rows[i].FindControl("chkBx");

                if (chkBoxChk.Checked)
                {
                    Label rolName = (Label)gridTerSitesInfo.Rows[i].FindControl("lblRN");

                    string rollName = rolName.Text + " ";

                    roles += rollName;
                }
            }
            return roles;
        }

        protected void cmbActiveUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbActiveUsers.SelectedIndex > 0)
            {
                int userID = int.Parse(cmbActiveUsers.SelectedValue);
                loadGrid();
                updateRolesGrid(userID);
            }
        }

        private void updateRolesGrid(int userID)
        {
            string query = string.Format("SELECT userRoles FROM tblUsers where id = {0}", userID);
            string roles = p.getStringValue(query);

            for (int i = 0; i < gridTerSitesInfo.Rows.Count; i++)
            {
                Label lblRl = (Label)gridTerSitesInfo.Rows[i].FindControl("lblRN");
                CheckBox chkCk = (CheckBox)gridTerSitesInfo.Rows[i].FindControl("chkBx");

                if (roles.Contains(lblRl.Text))
                {
                    chkCk.Checked = true;
                }
            }
        }

        protected void btnUpdateRoles_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbActiveUsers.SelectedIndex > 0)
        	{
                try
                {
                    int userID = int.Parse(cmbActiveUsers.SelectedValue);
                    string roles = getRoles();
                    string userName = cmbActiveUsers.SelectedItem.ToString();
                    bool status = true;

                    if (!roles.Equals(string.Empty))
                    {
                        string checkSum = p.getCheckSum(userName, roles, status.ToString());
                        string updateQuery = string.Format("UPDATE tblUsers SET userRoles = '{0}', userCheckSum = '{1}' WHERE id = {2}", roles, checkSum, userID);
                        p.ExecuteQuery(updateQuery);
                        showSuccessMessage("User information updated successfully.");
                    }
                    else
                    {
                        showWarningMessage("You must select at least one role.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
        	}
            else
        	{
                showWarningMessage("Select a user first.");
	        }
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbActiveUsers.SelectedIndex > 0)
            {
                try
                {
                    int userID = int.Parse(cmbActiveUsers.SelectedValue);
                    string password = p.FixString(txtPassword.Text);

                    string encryptedPassword = p.encryptPassword(password);
                    string updateQuery = string.Format("UPDATE tblUsers SET userPassword  = '{0}' WHERE id = {1}", encryptedPassword, userID);
                    p.ExecuteQuery(updateQuery);
                    showSuccessMessage("User information updated successfully.");
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("User must be selected first.");
            }
        }

        protected void btnUpdateSites_Click(object sender, EventArgs e)
        {
            clearLabels();
            string query = string.Empty;

            if (cmbActiveUsers.SelectedIndex > 0)
            {
                try
                {
                    int userID = int.Parse(cmbActiveUsers.SelectedValue);
                    for (int i = 0; i < gridRegions.Rows.Count; i++)
                    {
                        CheckBox chkBoxChk = (CheckBox)gridRegions.Rows[i].FindControl("chkSite");

                        if (chkBoxChk.Checked)
                        {
                            if (cmbPMs.SelectedIndex > 0)
                            {
                                Label site = (Label)gridRegions.Rows[i].FindControl("lblSiteID");
                                int pmID = int.Parse(cmbPMs.SelectedValue);
                                int siteID = 0;
                                int.TryParse(site.Text, out siteID);

                                if (siteID != 0)
                                {
                                    string q = string.Format("  INSERT INTO tblUsersSitesAssociation(userID, siteID, PMID) VALUES ({0},{1},{2})", userID, siteID, pmID);
                                    query += q;
                                }
                            }
                            else
                            {
                                throw new Exception("Project Manager must be selected.");

                            }

                        }
                    }

                    if (!query.Equals(string.Empty))
                    {
                        string q = string.Format("DELETE FROM tblUsersSitesAssociation WHERE userID = {0}", userID);
                        query = q + query;
                        p.ExecuteQuery(query);
                        showSuccessMessage("User information updated successfully.");
                    }
                    else
                    {
                        showWarningMessage("You must select at least one site.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("Select a user first.");
            }
        }
    }
}