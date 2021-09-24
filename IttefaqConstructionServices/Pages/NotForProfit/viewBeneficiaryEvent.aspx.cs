using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.NotForProfit
{
    public partial class viewBeneficiaryEvent : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        public string eventName = string.Empty;
        public string totalBeneficiaries = string.Empty;
        public string eventPlace = string.Empty;
        public string eventHead = string.Empty;
        public string eventStatus = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //loadGrid();
                loadEvents();
            }
        }

        private void loadEvents()
        {
            string ddQuery = "SELECT eventName, id FROM tblGenEvents ORDER BY eventName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbEvent.DataSource = ddItems;
            cmbEvent.DataTextField = "DisplayName";
            cmbEvent.DataValueField = "Value";
            cmbEvent.DataBind();
        }

        protected void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSuccess.Visible = lblWarning.Visible = pnlDetails.Visible = false;

            try
            {
                if (cmbEvent.SelectedIndex > 0)
                {
                    int eventID = int.Parse(cmbEvent.SelectedValue);
                    eventName = cmbEvent.SelectedItem.ToString();
                    string query = string.Format("SELECT tblBeneficiaries.name AS Name, tblBeneficiaries.cnic AS CNIC, tblBeneficiaries.telephone AS Contact, tblBeneficiaries.address AS Address, tblBeneficiaries.city AS City, tblGenEvents.eventPlace, tblGenAccounts.accountName, tblGenEvents.isFinalized FROM tblGenBeneficiariesInEvents INNER JOIN tblBeneficiaries ON tblGenBeneficiariesInEvents.beneficiaryID = tblBeneficiaries.id INNER JOIN tblGenEvents ON tblGenBeneficiariesInEvents.eventID = tblGenEvents.id INNER JOIN tblGenAccounts ON tblGenEvents.eventTypeID = tblGenAccounts.id WHERE (tblGenBeneficiariesInEvents.eventID = {0}) ORDER BY tblGenBeneficiariesInEvents.beneficiaryID", eventID);
                    //string query = string.Format("SELECT tblBeneficiaries.name AS Name, tblBeneficiaries.cnic AS CNIC, tblBeneficiaries.telephone AS Contact, tblBeneficiaries.address AS Address, tblBeneficiaries.city AS City FROM tblGenBeneficiariesInEvents INNER JOIN tblBeneficiaries ON tblGenBeneficiariesInEvents.beneficiaryID = tblBeneficiaries.id WHERE tblGenBeneficiariesInEvents.eventID = {0} ORDER BY tblGenBeneficiariesInEvents.beneficiaryID ", eventID);

                    DataTable dt = p.GetDataTable(query);
                    totalBeneficiaries = dt.Rows.Count.ToString();

                    DataRow dr = dt.Rows[0];
                    eventPlace = dr[5].ToString();
                    eventHead = dr[6].ToString();
                    bool finalized = false;
                    bool.TryParse(dr[7].ToString(), out finalized);

                    if (finalized == true)
                    {
                        eventStatus = "Event has been finalized.";
                    }
                    else
                    {
                        eventStatus = "Event has not been finalized yet.";
                    }

                    gridBeneficiaries.DataSource = dt;
                    gridBeneficiaries.DataBind();
                    pnlDetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getExceptionMessage(ex.Message);
            }
        }
    }
}