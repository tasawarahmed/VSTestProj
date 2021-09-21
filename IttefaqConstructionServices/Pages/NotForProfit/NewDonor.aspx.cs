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
    public partial class NewDonor : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnDonorCreate_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = lblWarning.Visible = false;
            try
            {
                string donorName = p.FixString(txtName.Text);

                if (donorName == string.Empty)
                {
                    throw new Exception("You must specify an donor name.");
                }

                if (!exists(donorName))
                {
                    string address = p.FixString(txtAddress.Text);
                    string organization = p.FixString(txtOrganization.Text);
                    string contact = p.FixString(txtContact.Text);
                    string email = p.FixString(txtEmail.Text);
                    string remarks = p.FixString(txtRemarks.Text);

                    string query = string.Format("INSERT INTO tblGenDonors(name, address, organization, contact, email, remarks) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", donorName, address, organization, contact, email, remarks);
                    p.ExecuteQuery(query);
                    lblSuccess.Text = p.getSuccessMessage("Donor added successfully.");
                    lblSuccess.Visible = true;
                }
                else
                {
                    lblWarning.Text = p.getWarningMessage("Donor already exists");
                    lblWarning.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getExceptionMessage(ex.Message);
                lblWarning.Visible = true;
            }
        }

        private bool exists(string donorName)
        {
            string query = string.Format("SELECT id, name, address, organization, contact, email, remarks FROM tblGenDonors where name = '{0}'", donorName);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}