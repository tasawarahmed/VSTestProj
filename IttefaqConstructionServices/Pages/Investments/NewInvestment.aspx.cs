using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Investments
{
    public partial class NewInvestment : System.Web.UI.Page
    {
        Utilities u = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadInvestmentAccountsAndGrid();
            }
        }

        private void loadInvestmentAccountsAndGrid()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Investments')";
            int accountID = u.getAccountID(query);

            string investmentsQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

            List<DDList> investmentAccounts = new List<DDList>();
            investmentAccounts = u.getDDList(investmentsQuery);

            cmbAccount.DataSource = investmentAccounts;
            cmbAccount.DataTextField = "DisplayName";
            cmbAccount.DataValueField = "Value";
            cmbAccount.DataBind();

            string investmentsGridQuery = string.Format("SELECT accountName AS [Account Name] FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

            DataTable dt = u.GetDataTable(investmentsGridQuery);

            gridRegions.DataSource = null;
            gridRegions.DataSource = dt;
            gridRegions.DataBind();
        }

        protected void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbAccount.SelectedIndex > 0)
            {
                try
                {
                    int accountID = int.Parse(cmbAccount.SelectedValue);
                    string query = string.Format("SELECT accountDescription, address, phoneNumber, email, specialNotes FROM tblGenAccounts WHERE id = {0}", accountID);

                    DataTable dt = u.GetDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        txtAddress.Text = dr[1].ToString();
                        txtContact.Text = dr[2].ToString();
                        txtDescription.Text = dr[0].ToString();
                        txtEmail.Text = dr[3].ToString();
                        txtSpecialNotes.Text = dr[4].ToString();
                    }
                }

                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbAccount.SelectedIndex > 0)
            {
                try
                {
                    int accountID = int.Parse(cmbAccount.SelectedValue);
                    string address = u.FixString(txtAddress.Text);
                    string contact = u.FixString(txtContact.Text);
                    string description = u.FixString(txtDescription.Text);
                    string email = u.FixString(txtEmail.Text);
                    string notes = u.FixString(txtSpecialNotes.Text);

                    string query = string.Format("UPDATE tblGenAccounts SET accountDescription = '{0}', address = '{1}', phoneNumber = '{2}', email = '{3}', specialNotes = '{4}', remarks = '{5}' WHERE id = {6}", description, address, contact, email, notes, description, accountID);
                    u.ExecuteQuery(query);

                    showSuccessMessage("Information updated successfully.");

                    loadInvestmentAccountsAndGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        private void clearLabels()
        {
            lblSuccess.Visible = lblWarning.Visible = false;
        }

        private void showWarningMessage(string s)
        {
            lblWarning.Visible = true;
            lblWarning.Text = u.getWarningMessage(s);
        }

        private void showSuccessMessage(string s)
        {
            lblSuccess.Visible = true;
            lblSuccess.Text = u.getSuccessMessage(s);
        }
    }
}