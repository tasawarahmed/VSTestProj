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
    public partial class NewEvent : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadEventTypes();
            }
        }

        private void loadEventTypes()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Welfare Programs')";
            int id = p.getAccountID(query);

            string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE parentAccountID = {0} ORDER BY accountName ", id);

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbPrograms.DataSource = ddItems;
            cmbPrograms.DataTextField = "DisplayName";
            cmbPrograms.DataValueField = "Value";
            cmbPrograms.DataBind();
        }

        protected void btnEventCreate_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = lblWarning.Visible = false;
            try
            {
                if (cmbPrograms.SelectedIndex > 0)
                {
                    int programID = int.Parse(cmbPrograms.SelectedValue);
                    string programName = p.FixString(txtEventName.Text);

                    if (programName == string.Empty)
                    {
                        throw new Exception("You must specify an event name.");
                    }

                    if (!exists(programID, programName))
                    {
                        string query = string.Format("INSERT INTO tblGenEvents(eventName, eventTypeID) VALUES ('{0}',{1})", programName, programID);
                        p.ExecuteQuery(query);
                        lblSuccess.Text = p.getSuccessMessage("Event added successfully.");
                        lblSuccess.Visible = true;                        
                    }
                    else
                    {
                        lblWarning.Text = p.getWarningMessage("Event already exists");
                        lblWarning.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getExceptionMessage(ex.Message);
                lblWarning.Visible = true;
            }
        }

        private bool exists(int programID, string programName)
        {
            string query = string.Format("SELECT * FROM tblGenEvents WHERE eventName = '{0}' AND eventTypeID = {1}", programName, programID);
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