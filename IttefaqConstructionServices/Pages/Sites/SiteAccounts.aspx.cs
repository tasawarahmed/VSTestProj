using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IttefaqConstructionServices;
using IttefaqConstructionServices.Logic;
using System.Data;

namespace IttefaqConstructionServices.Pages.Sites
{
    public partial class SiteAccounts : System.Web.UI.Page
    {
        Utilities p = new Utilities();

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
                    populatePrimaryAccountsList();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void populatePrimaryAccountsList()
        {
            string queryPrimaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 0) AND (isActive = '{0}') AND (accountName LIKE '%Site%') ORDER BY accountName", true);

            List<DDList> primaryAccounts = new List<DDList>();
            primaryAccounts = p.getDDList(queryPrimaryAccounts);

            cmbPrimary.DataSource = primaryAccounts;
            cmbPrimary.DataTextField = "DisplayName";
            cmbPrimary.DataValueField = "Value";
            cmbPrimary.DataBind();
        }

        protected void cmbPrimary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0)
            {
                populateSecondaryAccountsList();
            }
        }

        private void populateSecondaryAccountsList()
        {
            int parentAccountID = int.Parse(cmbPrimary.SelectedValue);

            string querySecondaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

            List<DDList> secondaryAccounts = new List<DDList>();
            secondaryAccounts = p.getDDList(querySecondaryAccounts);

            cmbSecondary.Items.Clear();
            cmbSecondary.DataSource = secondaryAccounts;
            cmbSecondary.DataTextField = "DisplayName";
            cmbSecondary.DataValueField = "Value";
            cmbSecondary.DataBind();
        }

        protected void cmbSecondary_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0 && cmbSecondary.SelectedIndex > 0)
            {
                populateTertiaryAccountsList();
            }
        }

        private void populateTertiaryAccountsList()
        {
            int parentAccountID = int.Parse(cmbSecondary.SelectedValue);

            string queryTertiaryAccounts = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", parentAccountID, true);

            List<DDList> tertiaryAccounts = new List<DDList>();
            tertiaryAccounts = p.getDDList(queryTertiaryAccounts);

            cmbTertiary.Items.Clear();
            cmbTertiary.DataSource = tertiaryAccounts;
            cmbTertiary.DataTextField = "DisplayName";
            cmbTertiary.DataValueField = "Value";
            cmbTertiary.DataBind();
        }

        protected void btnSecondaryCreate_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbPrimary.SelectedIndex > 0)
            {
                try
                {
                    string accountName = p.FixString(txtSecondaryName.Text);
                    string accountDescription = p.FixString(txtSecondaryDescription.Text);
                    int parentAccountID = int.Parse(cmbPrimary.SelectedValue);
                    string remarks = "Site";

                    if (!exists(accountName, parentAccountID))
                    {
                        addSecondaryAccount(accountName, parentAccountID, "00", "000", accountDescription, remarks);
                        populateSecondaryAccountsList();
                        txtSecondaryDescription.Text = txtSecondaryName.Text = string.Empty;
                    }

                    else
                    {
                        showWarningMessage("The given secondary account already exists.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        private void addSecondaryAccount(string accountName, int parentAccountID, string accountPfx, string accountPfx2, string accountDescription, string remarks)
        {
            string secondaryAccountPrefix = p.getNewSecondaryAccountPrefix(parentAccountID, accountPfx);
            string parentAccountPrefix = p.getParentAccountPrefix(parentAccountID);

            if (secondaryAccountPrefix.Length < 2)
            {
                secondaryAccountPrefix = "0" + secondaryAccountPrefix;
            }

            if (int.Parse(secondaryAccountPrefix) < 99)
            {
                string insertQuery = string.Format("INSERT INTO tblGenAccounts (parentAccountID, parentAccountPrefix, secondaryAccountPrefix, tertiaryAccountPrefix, accountName, accountPrefix, accountDescription, isActive, remarks) VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}', '{8}')", parentAccountID, parentAccountPrefix, secondaryAccountPrefix, accountPfx2, accountName, secondaryAccountPrefix, accountDescription, true, remarks);
                p.ExecuteQuery(insertQuery);
            }

            else
            {
                throw new Exception("Accounts creation limit under this head exceeded.");
            }
        }

        protected void btnTertiaryCreate_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbPrimary.SelectedIndex > 0 && cmbSecondary.SelectedIndex > 0)
            {
                try
                {
                    string accountName = p.FixString(txtTertiaryName.Text);
                    string accountDescription = p.FixString(txtTertiaryDescription.Text);
                    int parentAccountID = int.Parse(cmbSecondary.SelectedValue);
                    string remarks = "Site";

                    if (!exists(accountName, parentAccountID))
                    {
                        addTertiaryAccount(accountName, parentAccountID, "00", "000", accountDescription, remarks);
                        populateTertiaryAccountsList();
                        txtTertiaryDescription.Text = txtTertiaryName.Text = string.Empty;
                        showSuccessMessage("Tertiary account created successfully.");
                    }

                    else
                    {
                        showWarningMessage("The given tertiary account already exists.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        private void addTertiaryAccount(string accountName, int parentAccountID, string p1, string p2, string accountDescription, string remarks)
        {
            string tertiaryAccountPrefix = p.getNewTertiaryAccountPrefix(parentAccountID, p2);
            string secondaryAccountPrefix = p.getParentAccountPrefix(int.Parse(cmbSecondary.SelectedValue));
            string parentAccountPrefix = p.getParentAccountPrefix(int.Parse(cmbPrimary.SelectedValue));

            if (tertiaryAccountPrefix.Length < 3)
            {
                if (tertiaryAccountPrefix.Length == 2)
                {
                    tertiaryAccountPrefix = "0" + tertiaryAccountPrefix;
                }
                else
                {
                    tertiaryAccountPrefix = "00" + tertiaryAccountPrefix;
                }
            }

            if (int.Parse(tertiaryAccountPrefix) < 999)
            {
                string insertQuery = string.Format("INSERT INTO tblGenAccounts (parentAccountID, parentAccountPrefix, secondaryAccountPrefix, tertiaryAccountPrefix, accountName, accountPrefix, accountDescription, isActive, remarks) VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}', '{8}')", parentAccountID, parentAccountPrefix, secondaryAccountPrefix, tertiaryAccountPrefix, accountName, tertiaryAccountPrefix, accountDescription, true, remarks);
                p.ExecuteQuery(insertQuery);
            }
            else
            {
                throw new Exception("Accounts creation limit under this head exceeded.");
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

        private bool exists(string accountName, int parentAccountID)
        {
            bool exists = true;

            string query = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (accountName = '{0}') AND (parentAccountID = {1}) ORDER BY accountName", accountName, parentAccountID);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count == 0)
            {
                exists = false;
            }

            return exists;
        }
    }
}