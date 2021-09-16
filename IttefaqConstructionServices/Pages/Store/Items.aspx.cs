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
    public partial class Items : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "store, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadGridCategories();
                    loadCategoriesList();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadCategoriesList()
        {
            string queryPrimaryAccounts = "SELECT name, id FROM tblInventoryCategories ORDER BY name";

            List<DDList> primaryAccounts = new List<DDList>();
            primaryAccounts = p.getDDList(queryPrimaryAccounts);

            cmbCategories.DataSource = primaryAccounts;
            cmbCategories.DataTextField = "DisplayName";
            cmbCategories.DataValueField = "Value";
            cmbCategories.DataBind();
        }

        private void loadGridCategories()
        {
            string query = "SELECT name AS [Category Name] FROM tblInventoryCategories ORDER BY name";
            DataTable dt = new DataTable();
            dt = p.GetDataTable(query);

            gridExistingCategories.DataSource = dt;
            gridExistingCategories.DataBind();
        }

        protected void btnAddCetegory_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                string category = p.FixString(txtNewCategory.Text.Trim());

                string query = string.Format("SELECT name, id FROM tblInventoryCategories WHERE name = '{0}'", category);

                if (!p.ifRecordsExist(query))
                {
                    query = string.Format("INSERT INTO tblInventoryCategories(name) VALUES ('{0}')", category);
                    p.ExecuteQuery(query);
                    loadCategoriesList();
                    loadGridCategories();
                    txtNewCategory.Text = "";
                    showSuccessMessage("Category added successfully.");
                }
                else
                {
                    showWarningMessage("Category already exists.");
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

        protected void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbCategories.SelectedIndex > 0)
            {
                loadItems();
            }
        }

        private void loadItems()
        {            
            int ID = int.Parse(cmbCategories.SelectedValue);
            string query = string.Format("SELECT name, id FROM tblInventoryItems WHERE categoryID = {0}", ID);

            List<DDList> primaryAccounts = new List<DDList>();
            primaryAccounts = p.getDDList(query);

            cmbItems.DataSource = primaryAccounts;
            cmbItems.DataTextField = "DisplayName";
            cmbItems.DataValueField = "Value";
            cmbItems.DataBind();
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                if (cmbCategories.SelectedIndex > 0)
                {
                    int categoryID = int.Parse(cmbCategories.SelectedValue);
                    string item = p.FixString(txtNewItem.Text.Trim());

                    string query = string.Format("SELECT id, name FROM tblInventoryItems WHERE categoryID = {0} AND name = N'{1}'", categoryID, item);

                    if (!p.ifRecordsExist(query))
                    {
                        query = string.Format("INSERT INTO tblInventoryItems (categoryID, name) VALUES ({0}, N'{1}')", categoryID, item);
                        p.ExecuteQuery(query);
                        loadItems();
                        txtNewItem.Text = "";
                        showSuccessMessage("Item added successfully.");
                    }
                    else
                    {
                        showWarningMessage("Item already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }
    }
}