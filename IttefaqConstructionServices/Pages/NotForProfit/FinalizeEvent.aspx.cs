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
                bindBanksGrid();
            }
        }

        private void bindBanksGrid()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Cash And Bank Balances')";
            int id = p.getAccountID(query);

            string ddQuery = string.Format("SELECT accountName AS Name, id FROM tblGenAccounts WHERE parentAccountID = {0} ORDER BY accountName ", id);
            DataTable dt = p.GetDataTable(ddQuery);
            gridBanks.DataSource = dt;
            gridBanks.DataBind();
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
            string query = string.Empty;

            try
            {
                if (mathematicalAccuracy() && accountingAccuracy())
                {
                    if (cmbEvent.SelectedIndex > 0)
                    {
                        int eventID = int.Parse(cmbEvent.SelectedValue);
                        query += getEventUpdateQuery(eventID);
                        query += getBeneficiaryUpdateQuery(eventID);
                        p.ExecuteTransactionQuery(query);
                        lblSuccess.Text = p.getSuccessMessage("Event finalized successfully");
                        lblSuccess.Visible = true;
                    }
                    else
                    {
                        throw new Exception("Please select an event to finalize.");
                    }
                }

            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getWarningMessage(ex.Message);
                lblWarning.Visible = true;
            }
        }

        private string getBeneficiaryUpdateQuery(int eventID)
        {
            string query = string.Empty;

            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                Label lbl = (Label)Gridview1.Rows[i].FindControl("lblID");

                TextBox tb = (TextBox)Gridview1.Rows[i].FindControl("txtBenefit");
                TextBox tbNM = (TextBox)Gridview1.Rows[i].FindControl("txtNMonetary");

                string nmBenefits = tbNM.Text;
                
                int benefID = 0;
                int.TryParse(lbl.Text, out benefID);

                Decimal amount = 0;
                Decimal.TryParse(tb.Text, out amount);
                
                DateTime date = DateTime.Now;
                DateTime.TryParse(txtDate.Text, out date);

                string monthYear = date.Year.ToString() + "-" + date.Month.ToString();

                if (date.Month < 10)
                {
                    monthYear = date.Year.ToString() + "-0" + date.Month.ToString();
                }

                string q = string.Format("  UPDATE tblGenBeneficiariesInEvents SET benefitGiven = {0}, eventMonthYear = '{1}', nonMonetaryBenefits = '{2}' WHERE beneficiaryID = {3} AND eventID = {4}", amount, monthYear, nmBenefits, benefID, eventID);
                query += q;
            }

            return query;
        }

        private string getEventUpdateQuery(int eventID)
        {
            DateTime eventDate = DateTime.Parse(txtDate.Text);
            string place = p.FixString(txtPlace.Text);
            Decimal benefitsGiven = Decimal.Parse(txtBenefits.Text);
            string remarks = p.FixString(txtRemarks.Text);
            bool isFinalized = true;
            Decimal adminExpenses = Decimal.Parse(txtAdminExpenses.Text);

            string query = string.Format(" UPDATE tblGenEvents SET eventDate = '{0}', eventPlace = '{1}', benefitsGiven = {2}, remarks ='{3}', isFinalized ='{4}', adminExpenses = {5} WHERE id = {6}   ", eventDate, place, benefitsGiven, remarks, isFinalized, adminExpenses, eventID);
            return query;
        }

        private bool accountingAccuracy()
        {
            bool result = false;
            Decimal tBenefits = 0;
            Decimal tExpenses = 0;
            Decimal gTotal = 0;

            Decimal.TryParse(txtBenefits.Text, out tBenefits);
            Decimal.TryParse(txtAdminExpenses.Text, out tExpenses);

            Decimal gTotalBoxes = tBenefits + tExpenses;

            for (int i = 0; i < gridBanks.Rows.Count; i++)
            {
                TextBox t = (TextBox)gridBanks.Rows[i].FindControl("txtAmount");
                Decimal amount = 0;
                Decimal.TryParse(t.Text, out amount);
                gTotal = gTotal + amount;
            }

            if (gTotal == gTotalBoxes)
            {
                result = true;
            }

            else
            {
                string message = string.Format("Difference in figures: Grid amounts total: {0}, and Text Box Amount: {1}", gTotal, gTotalBoxes);
                throw new Exception(message);
            }
            return result;
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
                TextBox tbNM = (TextBox)Gridview1.Rows[i].FindControl("txtNMonetary");

                if (tbNM.Text == string.Empty)
                {
                    tbNM.Text = txtOtherBenefits.Text;                    
                }

                Decimal benefit = 0;
                Decimal.TryParse(tb.Text, out benefit);

                if (benefit == 0 && tbNM.Text == "A")
                {
                    
                }
                else if (benefit == 0)
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
            else
            {
                string message = string.Format("Difference in figures: Grid benefits total: {0}, and Text Box Amount: {1}", gridBenefits, totalBenefits);
                throw new Exception(message);
            }

            return result;
        }

    }
}