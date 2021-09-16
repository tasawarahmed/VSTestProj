using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Accounts
{
    public partial class Status : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Logic.Accounts a = new Logic.Accounts();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect(@"~\Pages\Membership\Login.aspx");
            }
            if (p.Authenticated(Session["UserName"].ToString(), "accounts, administrator", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadActiveAccounts();
                    loadInactiveAccounts();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }                

        }

        private void loadInactiveAccounts()
        {
            string queryPrimaryAccounts = string.Format("SELECT CONVERT(nvarchar(50), id) + ' : ' + accountName AS accountName, id FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (isActive = '{0}') ORDER BY id", false);

            List<DDList> primaryAccounts = new List<DDList>();
            primaryAccounts = p.getDDList(queryPrimaryAccounts);

            cmbInactive.DataSource = primaryAccounts;
            cmbInactive.DataTextField = "DisplayName";
            cmbInactive.DataValueField = "Value";
            cmbInactive.DataBind();
        }

        private void loadActiveAccounts()
        {
            string queryPrimaryAccounts = string.Format("SELECT CONVERT(nvarchar(50), id) + ' : ' + accountName AS accountName, id FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (isActive = '{0}') ORDER BY id", true);

            List<DDList> primaryAccounts = new List<DDList>();
            primaryAccounts = p.getDDList(queryPrimaryAccounts);

            cmbActiveAccounts.DataSource = primaryAccounts;
            cmbActiveAccounts.DataTextField = "DisplayName";
            cmbActiveAccounts.DataValueField = "Value";
            cmbActiveAccounts.DataBind();
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {

                if (cmbActiveAccounts.SelectedIndex > 0)
                {
                    int accountID = int.Parse(cmbActiveAccounts.SelectedValue);
                    Single status = a.checkStatus(accountID);

                    if (status == 0f)
                    {
                        string updateQuery = string.Format("UPDATE tblGenAccounts SET isActive = 'false' WHERE id = {0}", accountID);
                        p.ExecuteQuery(updateQuery);
                        loadInactiveAccounts();
                        loadActiveAccounts();
                        showSuccessMessage("Account closed successfully.");
                    }
                    else
                    {
                        showWarningMessage("The account balance is not zero, it can't be closed. Please see account statement for details.");
                    }

                }
                else
                {
                    showWarningMessage("Please select account first.");
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        protected void btnReOpen_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {

                if (cmbInactive.SelectedIndex > 0)
                {
                    int accountID = int.Parse(cmbInactive.SelectedValue);
                    string updateQuery = string.Format("UPDATE tblGenAccounts SET isActive = 'true' WHERE id = {0}", accountID);
                    p.ExecuteQuery(updateQuery);
                    loadInactiveAccounts();
                    loadActiveAccounts();
                    showSuccessMessage("Account reopened successfully.");
                }
                else
                {
                    showWarningMessage("Please select account first.");
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }
    }
}