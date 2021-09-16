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
    public partial class AllocationPlaces : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "hr, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadSites();
                    loadAllocationPlaces();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadAllocationPlaces()
        {
            string query = "SELECT allocationPlaceName FROM tblGenStaffAllocationPlaces";
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                gridAllocationPlaces.DataSource = dt;
                gridAllocationPlaces.DataBind();

                gridAllocationPlaces.UseAccessibleHeader = true;
                gridAllocationPlaces.HeaderRow.TableSection = TableRowSection.TableHeader;
                pnlAllocationPlaces.Visible = true;                
            }
            else
            {
                pnlAllocationPlaces.Visible = false;
            }

        }

        private void loadSites()
        {
            string ddQuery = "SELECT siteName, id FROM tblGenSites WHERE isActive = 'true' order by siteName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSites.Items.Clear();
            cmbSites.DataSource = ddItems;
            cmbSites.DataTextField = "DisplayName";
            cmbSites.DataValueField = "Value";
            cmbSites.DataBind();
        }

        protected void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            pnlSites.Visible = chkSwap.Checked;
            pnlOthers.Visible = !chkSwap.Checked;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (chkSwap.Checked)
            {
                if (cmbSites.SelectedIndex > 0)
                {
                    int siteID = int.Parse(cmbSites.SelectedValue);
                    string siteName = cmbSites.SelectedItem.ToString();
                    string type = "Sites";

                    string nameQuery = string.Format("SELECT * FROM tblGenStaffAllocationPlaces WHERE allocationPlaceName = '{0}' AND allocationPlaceType = '{1}'", siteName, type);

                    if (!p.ifRecordsExist(nameQuery))
                    {
                        string query = string.Format("INSERT INTO tblGenStaffAllocationPlaces(allocationPlaceName, allocationPlaceType, siteID) VALUES ('{0}','{1}',{2})", siteName, type, siteID);
                        p.ExecuteQuery(query);
                        showSuccessMessage("Allocation place added successfully.");
                        loadAllocationPlaces();
                    }
                }
                else
                {
                    showWarningMessage("Please select a site.");
                    loadAllocationPlaces();
                }
            }
            else
            {
                string siteName = p.FixString(txtAllocationPlace.Text);
                string type = "Others";

                string nameQuery = string.Format("SELECT * FROM tblGenStaffAllocationPlaces WHERE allocationPlaceName = '{0}' AND allocationPlaceType = '{1}'", siteName, type);

                if (!p.ifRecordsExist(nameQuery))
                {
                    string query = string.Format("INSERT INTO tblGenStaffAllocationPlaces(allocationPlaceName, allocationPlaceType) VALUES ('{0}','{1}')", siteName, type);
                    p.ExecuteQuery(query);
                    showSuccessMessage("Allocation place added successfully.");
                    loadAllocationPlaces();
                }
                else
                {
                    showWarningMessage("Allocation place already exists.");
                    loadAllocationPlaces();
                }
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
    }
}