using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Store
{
    public partial class Locations : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "store, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadGridLocations();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadGridLocations()
        {
            string query = "SELECT name AS [Location Name] FROM tblInventoryLocations ORDER BY name";
            DataTable dt = new DataTable();
            dt = p.GetDataTable(query);

            gridExistingLocations.DataSource = dt;
            gridExistingLocations.DataBind();
        }

        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            clearLabels();
            string locationName = p.FixString(txtNewLocation.Text.Trim());

            try
            {
                string query = string.Format("SELECT name FROM tblInventoryLocations WHERE name = N'{0}'", locationName);

                if (!p.ifRecordsExist(query))
                {
                    query = string.Format("INSERT INTO tblInventoryLocations(name) VALUES (N'{0}')", locationName);
                    p.ExecuteQuery(query);

                    loadGridLocations();
                    showSuccessMessage("Location added successfully.");
                }
                else
                {
                    showWarningMessage("Location already exists.");
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
    }
}