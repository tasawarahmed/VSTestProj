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
    public partial class FinalizeEvent : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Decimal totalBenefits = 0;
        Decimal gridBenefits = 0;

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
            string ddQuery = "SELECT eventName, id FROM tblGenEvents WHERE (isFinalized = 'false') ORDER BY eventName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbEvent.DataSource = ddItems;
            cmbEvent.DataTextField = "DisplayName";
            cmbEvent.DataValueField = "Value";
            cmbEvent.DataBind();
        }

        protected void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEvent.SelectedIndex > 0)
            {
                int eventID = int.Parse(cmbEvent.SelectedValue);
                loadGrid(eventID);
            }
        }

        private void loadGrid(int criteria)
        {
            string dtQuery = string.Format("SELECT tblGenBeneficiariesInEvents.beneficiaryID AS id, tblGenBeneficiariesInEvents.eventID, tblBeneficiaries.name AS Name, tblBeneficiaries.address AS Address, tblBeneficiaries.city AS City, tblGenBeneficiariesInEvents.benefitGiven AS Benefit FROM tblGenBeneficiariesInEvents INNER JOIN tblBeneficiaries ON tblGenBeneficiariesInEvents.beneficiaryID = tblBeneficiaries.id WHERE tblGenBeneficiariesInEvents.eventID = {0}", criteria);
            DataTable dt = p.GetDataTable(dtQuery);

            Gridview1.DataSource = dt;
            Gridview1.DataBind();

            if (Gridview1.Rows.Count > 0)
            {
                refreshGrid();
            }
        }

        private void refreshGrid()
        {
            Gridview1.UseAccessibleHeader = true;
            Gridview1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnEventFinalize_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = lblWarning.Visible = false;

            try
            {
                if (mathematicalAccuracy())
                {
                    
                }
                else
                {
                    string message = string.Format("Difference in figures: Grid benefits total: {0}, and Text Box Amount: {1}", gridBenefits, totalBenefits);
                    throw new Exception(message);
                }

            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getWarningMessage(ex.Message);
                lblWarning.Visible = true;
            }
        }

        private bool mathematicalAccuracy()
        {
            bool result = false;

            totalBenefits = 0;
            Decimal.TryParse(txtBenefits.Text, out totalBenefits);

            Decimal benefitsPerHead = 0;
            Decimal.TryParse(txtBenefitsPerHead.Text, out benefitsPerHead);
            
            gridBenefits = 0;

            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                TextBox tb = (TextBox)Gridview1.Rows[i].FindControl("txtBenefit");
                Decimal benefit = 0;
                Decimal.TryParse(tb.Text, out benefit);

                if (benefit == 0)
                {
                    gridBenefits += benefitsPerHead;
                    tb.Text = benefitsPerHead.ToString();
                }
                else
                {
                    Decimal specialBenefit = 0;
                    Decimal.TryParse(tb.Text, out specialBenefit);
                    gridBenefits += specialBenefit;
                }
            }

            if (totalBenefits == gridBenefits)
            {
                result = true;
            }

            return result;
        }

    }
}