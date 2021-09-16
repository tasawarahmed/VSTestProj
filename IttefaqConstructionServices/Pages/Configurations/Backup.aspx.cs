using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Configurations
{
    public partial class Backup : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTakeBackup_Click(object sender, EventArgs e)
        {
            lblWarning.Text = lblSuccess.Text = "";
            lblWarning.Visible = lblSuccess.Visible = false;

            string filePath = txtPath.Text;
            string fileName = txtName.Text;

            if (!filePath.EndsWith(":"))
            {
                filePath = filePath + ":";
            }
            try
            {
                //query to take backup database
                DateTime date = DateTime.Now;
                int year = date.Year;
                int month = date.Month;
                int day = date.Day;

                //string month = GetMonth(m);

                int hour = date.Hour;
                int minutes = date.Minute;
                int seconds = date.Second;

                string time = hour + "-" + minutes + "-" + seconds;

                string backupDate = year + "-" + month + "-" + day + "__" + time;

                string query = "backup database "+ fileName +  " to disk='" + filePath + "\\ " + fileName + backupDate + ".Bak'";
                p.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                lblWarning.Visible = true;
                lblWarning.Text = "There is some error performing the operation. " + ex.Message + " Please correct the error and try again later.";
            }

            finally
            {
                lblSuccess.Visible = true;
                lblSuccess.Text = "Database backup was successful.";
            }
        }
    }
}