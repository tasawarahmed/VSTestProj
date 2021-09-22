using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.NotForProfit
{
    public partial class Donations : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDonors();
                bindAccountsGrid();
            }
        }

        private void bindAccountsGrid()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Income From Donations')";
            int id = p.getAccountID(query);

            string ddQuery = string.Format("SELECT accountName AS Name, id FROM tblGenAccounts WHERE parentAccountID = {0} ORDER BY accountName ", id);
            DataTable dt = p.GetDataTable(ddQuery);
            gridAccounts.DataSource = dt;
            gridAccounts.DataBind();
        }

        private void loadDonors()
        {
            string query = "SELECT name, id FROM tblGenDonors ORDER BY name";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(query);

            cmbDonor.DataSource = ddItems;
            cmbDonor.DataTextField = "DisplayName";
            cmbDonor.DataValueField = "Value";
            cmbDonor.DataBind();
        }

        protected void btnEventCreate_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = lblWarning.Visible = false;
            DateTime date = DateTime.Now;
            Decimal amount = 0;

            try
            {
                if (cmbDonor.SelectedIndex > 0 && DateTime.TryParse(txtDate.Text, out date) && Decimal.TryParse(txtAmount.Text, out amount))
                {
                    int donorID = int.Parse(cmbDonor.SelectedValue);

                    if (amount > 0)
                    {
                        string query = string.Format("INSERT INTO tblGenDonations(donorID, date, amount) VALUES ({0},'{1}',{2})", donorID, date, amount);
                        p.ExecuteQuery(query);
                        lblSuccess.Text = p.getSuccessMessage("Donation received successfully.");
                        lblSuccess.Visible = true;
                    }
                    else
                    {
                        throw new Exception("Donation amount must be more than zero.");
                    }
                }
                else
                {
                    throw new Exception("Please check all values and try again.");
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getExceptionMessage(ex.Message);
                lblWarning.Visible = true;
            }
        }
    }
}