using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Sites
{
    public partial class Regions : System.Web.UI.Page
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
            try
            {
                gridRegions.DataBind();
                gridRegions.UseAccessibleHeader = true;
                gridRegions.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {
                showWarningMessage("No Regions Exist.");
            }               
        }

        protected void btnAddRegion_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                string regionName = p.FixString(txtRegionName.Text);
                string description = p.FixString(txtDescription.Text);

                string query = string.Format("SELECT id, regionName, remarks FROM tblGenRegions WHERE regionName = '{0}'", regionName);

                bool exists = p.ifExists(query);

                if (!exists)
                {
                    string insertQuery = string.Format("INSERT INTO tblGenRegions(regionName, remarks) VALUES ('{0}','{1}')", regionName, description);
                    p.ExecuteQuery(insertQuery);
                    showSuccessMessage("Region added successfully.");
                    clearControls();
                    loadRegions();
                }

                else
                {
                    showWarningMessage("Region already exists.");
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
    }
}