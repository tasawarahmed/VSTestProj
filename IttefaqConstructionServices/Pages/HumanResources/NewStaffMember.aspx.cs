using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.HumanResources
{
    public partial class NewStaffMember : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "hr, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadStaffMembers();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadStaffMembers()
        {
            string query = "SELECT staffName + ' - ' + staffFather AS Staff, id FROM tblGenStaff ORDER BY Staff";

            List<DDList> staff = new List<DDList>();
            staff = p.getDDList(query);

            cmbStaffMember.DataSource = staff;
            cmbStaffMember.DataTextField = "DisplayName";
            cmbStaffMember.DataValueField = "Value";
            cmbStaffMember.DataBind();

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

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                string staffName = txtStaffName.Text;
                string staffAddress = txtStaffAddress.Text;
                string staffContact = txtStaffContact.Text;
                string fatherName = txtStaffFather.Text;
                Single salary = 0f;
                Single.TryParse(txtSalary.Text, out salary);
                string staffEmail = Validator.ValidateEmail(txtStaffEmail.Text, "Email");
                string staffIDCard = txtStaffIDCard.Text;

                string insertQuery = string.Format("INSERT INTO tblGenStaff(staffName, staffAddress, staffEmail, staffContact, staffIDCard, isActive, staffFather, currentSalary) VALUES ('{0}','{1}','{2}','{3}','{4}', '{5}', '{6}', {7})", staffName, staffAddress, staffEmail, staffContact, staffIDCard, true, fatherName, salary);
                p.ExecuteQuery(insertQuery);
                showSuccessMessage("Staff inserted successfully.");
                loadStaffMembers();
                clearNewControls();
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }            
        }

        private void clearNewControls()
        {
            txtStaffIDCard.Text = txtSalary.Text = txtStaffAddress.Text = txtStaffContact.Text = txtStaffEmail.Text = txtStaffFather.Text = txtStaffName.Text = string.Empty;
        }

        protected void cmbStaffMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbStaffMember.SelectedIndex > 0)
            {
                try
                {
                    int staffMemberID = int.Parse(cmbStaffMember.SelectedValue);
                    string query = string.Format("SELECT staffName, staffFather, currentSalary, staffAddress, staffEmail, staffContact, staffIDCard, isActive FROM tblGenStaff WHERE id = {0}", staffMemberID);

                    DataTable dt = p.GetDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        txtUpdateStaffName.Text = dr[0].ToString();
                        txtUpdateFather.Text = dr[1].ToString();
                        txtUpdateSalary.Text = dr[2].ToString();
                        txtUpdateAddress.Text = dr[3].ToString();
                        txtUpdateEmail.Text = dr[4].ToString();
                        txtUpdateContact.Text = dr[5].ToString();
                        txtUpdateIDCard.Text = dr[6].ToString();
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            clearLabels();
            try
            {
                if (cmbStaffMember.SelectedIndex > 0)
                {
                    int staffID = int.Parse(cmbStaffMember.SelectedValue);
                    string staffName = txtUpdateStaffName.Text;
                    string staffFather = txtUpdateFather.Text;
                    string staffAddress = txtUpdateAddress.Text;
                    string staffContact = txtUpdateContact.Text;
                    string fatherName = txtUpdateFather.Text;
                    Single salary = 0f;
                    Single.TryParse(txtUpdateSalary.Text, out salary);
                    string staffEmail = Validator.ValidateEmail(txtUpdateEmail.Text, "Email");
                    string staffIDCard = txtUpdateIDCard.Text;

                    string insertQuery = string.Format("UPDATE tblGenStaff SET staffName = '{0}', staffFather = '{1}', currentSalary = {2}, staffAddress = '{3}', staffEmail = '{4}', staffContact = '{5}', staffIDCard = {6} WHERE id = {7}", staffName, staffFather, salary, staffAddress, staffEmail, staffContact, staffIDCard, staffID);
                    p.ExecuteQuery(insertQuery);
                    showSuccessMessage("Staff updated successfully.");
                    loadStaffMembers();
                    clearUpdateControls();
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }

        }

        private void clearUpdateControls()
        {
            txtUpdateAddress.Text = txtUpdateContact.Text = txtUpdateEmail.Text = txtUpdateFather.Text = txtUpdateIDCard.Text = txtUpdateSalary.Text = txtUpdateStaffName.Text = string.Empty;
        }
    }
}