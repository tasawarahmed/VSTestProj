using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Sites
{
    public partial class NewSite : System.Web.UI.Page
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
                    loadSites();
                    dataBindGridRegions();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        protected void btnNewSite_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                string siteName = p.FixString(txtSiteName.Text);
                string query = string.Format("SELECT * FROM tblGenSites WHERE siteName = '{0}'", siteName);
                if (!p.ifExists(query))
                {
                    if (siteName == string.Empty )
                    {
                        showWarningMessage("Site Name can not be empty. Please try again.");
                    }
                    else
                    {
                        string insertQuery = string.Format("INSERT INTO tblGenSites(siteName) VALUES ('{0}')", siteName);
                        p.ExecuteQuery(insertQuery);
                        clearControls();
                        loadSites();
                        dataBindGridRegions();
                        showSuccessMessage("Site name entered successfully now add site details.");
                    }
                }

                else
                {
                    showWarningMessage("The entered site already exists.");
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void dataBindGridRegions()
        {
            try
            {
                gridRegions.DataBind();
                gridRegions.UseAccessibleHeader = true;
                gridRegions.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {
            }
        }
        
        protected void cmbActiveSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();
            dataBindGridRegions();

            try
            {
                if (cmbActiveSites.SelectedIndex > 0)
                {
                    int siteID = int.Parse(cmbActiveSites.SelectedValue);
                    loadRegions();
                    loadRawSubRegions();
                    getControlValues(siteID);
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void getControlValues(int siteID)
        {
            string query = string.Format("SELECT siteName, siteAddress, siteContacts, siteRegionID, siteSubRegionID, siteProjectManagerName, siteRemarks FROM tblGenSites WHERE id = {0}", siteID);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtDescription.Text = dr[6].ToString();
                txtPMName.Text = dr[5].ToString();
                txtSiteAddress.Text = dr[1].ToString();
                txtSiteContacts.Text = dr[2].ToString();
                int regionID = 0;
                int subregionID = 0;

                int.TryParse(dr[3].ToString(), out regionID);
                int.TryParse(dr[4].ToString(), out subregionID);

                cmbRegions.SelectedIndex = cmbRegions.Items.IndexOf(cmbRegions.Items.FindByValue(regionID.ToString()));//Right way to do that
                cmbSubRegions.SelectedIndex = cmbSubRegions.Items.IndexOf(cmbSubRegions.Items.FindByValue(subregionID.ToString()));//Right way to do that
            }
        }

        protected void btnEditSite_Click(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbActiveSites.SelectedIndex > 0 && cmbRegions.SelectedIndex > 0 && cmbSubRegions.SelectedIndex > 0)
            {
                try
                {
                    int siteID = int.Parse(cmbActiveSites.SelectedValue);
                    string siteAddress = p.FixString(txtSiteAddress.Text);
                    string description = p.FixString(txtDescription.Text);
                    string pMName = p.FixString(txtPMName.Text);
                    string contacts = p.FixString(txtSiteContacts.Text);
                    int regionID = int.Parse(cmbRegions.SelectedValue);
                    int subregionID = int.Parse(cmbSubRegions.Text);

                    string updateQuery = string.Format("UPDATE tblGenSites SET siteAddress = '{0}', siteContacts = '{1}', siteRegionID = {2}, siteSubRegionID = {3}, siteProjectManagerName = '{4}', siteRemarks = '{5}', isactive = '{6}' WHERE id = {7}", siteAddress, contacts, regionID, subregionID, pMName, description, true, siteID);
                    p.ExecuteQuery(updateQuery);
                    clearControls();
                    gridRegions.DataBind();
                    showSuccessMessage("Site information updated successfully.");
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        protected void cmbRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRegions.SelectedIndex > 0)
            {
                loadSubRegions();
            }
        }

        private void loadRawSubRegions()
        {
            string ddQuery = "SELECT subRegionName, id FROM tblGenSubRegions order by subRegionName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSubRegions.Items.Clear();
            cmbSubRegions.DataSource = ddItems;
            cmbSubRegions.DataTextField = "DisplayName";
            cmbSubRegions.DataValueField = "Value";
            cmbSubRegions.DataBind();
        }

        private void loadSites()
        {
            //cmbActiveSites = p.getDropDown("SELECT siteName, id FROM tblGenSites order by siteName");
            string ddQuery = "SELECT siteName, id FROM tblGenSites order by siteName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbActiveSites.Items.Clear();
            cmbActiveSites.DataSource = ddItems;
            cmbActiveSites.DataTextField = "DisplayName";
            cmbActiveSites.DataValueField = "Value";
            cmbActiveSites.DataBind();
        }

        private void loadRegions()
        {
            string ddQuery = "SELECT regionName, id FROM tblGenRegions order by regionName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbRegions.Items.Clear();
            cmbRegions.DataSource = ddItems;
            cmbRegions.DataTextField = "DisplayName";
            cmbRegions.DataValueField = "Value";
            cmbRegions.DataBind();
        }

        private void loadSubRegions()
        {
            int regionID = int.Parse(cmbRegions.SelectedValue);
            string ddQuery = string.Format("SELECT subRegionName, id FROM tblGenSubRegions WHERE regionID = {0} order by subRegionName", regionID);

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbSubRegions.Items.Clear();
            cmbSubRegions.DataSource = ddItems;
            cmbSubRegions.DataTextField = "DisplayName";
            cmbSubRegions.DataValueField = "Value";
            cmbSubRegions.DataBind();
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
            txtDescription.Text = txtPMName.Text = txtSiteAddress.Text = txtSiteContacts.Text = txtSiteName.Text = "";
        }
    }
}