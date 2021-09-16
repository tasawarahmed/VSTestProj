using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Sites
{
    public partial class SubRegions : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect(@"~\Pages\Membership\Login.aspx");
            }
            if (p.Authenticated(Session["UserName"].ToString(), "sites, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadRegions();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadRegions()
        {
            string ddQuery = "SELECT regionName, id FROM tblGenRegions order by regionName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbRegions.DataSource = ddItems;
            cmbRegions.DataTextField = "DisplayName";
            cmbRegions.DataValueField = "Value";
            cmbRegions.DataBind();
        }

        protected void btnAddRegion_Click(object sender, EventArgs e)
        {
            clearLabels();
            
            try
            {
                int regionID = int.Parse(cmbRegions.SelectedValue);
                string regionName = p.FixString(txtRegionName.Text);
                string description = p.FixString(txtDescription.Text);

                string query = string.Format("SELECT id, regionID, subRegionName, description FROM tblGenSubRegions WHERE regionID = {0} AND subRegionName = '{1}'", regionID, regionName);

                if (cmbRegions.SelectedIndex > 0 && !p.ifExists(query))
                {
                    string insertQuery = string.Format("INSERT INTO tblGenSubRegions(regionID, subRegionName, description) VALUES ({0},'{1}','{2}')", regionID, regionName, description);
                    p.ExecuteQuery(insertQuery);
                    clearControls();
                    gridRegions.DataBind();
                    gridRegions.UseAccessibleHeader = true;
                    gridRegions.HeaderRow.TableSection = TableRowSection.TableHeader;
                    showSuccessMessage("Sub region inserted successfully.");                    
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
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

        private void clearControls()
        {
            txtRegionName.Text = txtDescription.Text = "";
        }

        protected void cmbRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRegions.SelectedIndex > 0)
            {
                try
                {
                    gridRegions.DataBind();
                    gridRegions.UseAccessibleHeader = true;
                    gridRegions.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                catch (Exception )
                {
                    gridRegions.DataBind();
                }
            }
        }
    }
}