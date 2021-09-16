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
    public partial class Reports : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "store, viewer", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadLocationsList();
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
            string categoryQuery = "SELECT [name], [id] FROM [tblInventoryCategories]";
            List<DDList> categoriesList = new List<DDList>();

            categoriesList = p.getDDList(categoryQuery);

            cmbCategories.DataSource = categoriesList;
            cmbCategories.DataValueField = "Value";
            cmbCategories.DataTextField = "DisplayName";
            cmbCategories.DataBind();
        }

        private void loadLocationsList()
        {
            string query = "SELECT [name], [id] FROM [tblInventoryLocations]";
            List<DDList> classesList = new List<DDList>();

            classesList = p.getDDList(query);

            cmbSourceLocation.DataSource = classesList;
            cmbSourceLocation.DataValueField = "Value";
            cmbSourceLocation.DataTextField = "DisplayName";
            cmbSourceLocation.DataBind();

            cmbLocationsForTransactions.DataSource = classesList;
            cmbLocationsForTransactions.DataValueField = "Value";
            cmbLocationsForTransactions.DataTextField = "DisplayName";
            cmbLocationsForTransactions.DataBind();
        }

        protected void btnStockTotal_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            gridTotalStockDataBind();
        }

        private void gridTotalStockDataBind()
        {
            string query = "SELECT tblInventoryLocations.name AS [Location Name], tblInventoryCategories.name AS Category, tblInventoryItems.name AS [Item Name], SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity) AS [Quantity in Hand], tblInventoryItemValues.costValue AS [Cost Price], tblInventoryItemValues.marketValue AS [Market Price], (SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity)) * tblInventoryItemValues.costValue AS [Cost Value], (SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity)) * tblInventoryItemValues.marketValue AS [Market Value] FROM tblInventoryTransactions INNER JOIN tblInventoryCategories ON tblInventoryTransactions.categoryID = tblInventoryCategories.id INNER JOIN tblInventoryItems ON tblInventoryTransactions.itemID = tblInventoryItems.id INNER JOIN tblInventoryItemValues ON tblInventoryTransactions.itemID = tblInventoryItemValues.itemID AND tblInventoryTransactions.categoryID = tblInventoryItemValues.categoryID INNER JOIN tblInventoryLocations ON tblInventoryTransactions.itemLocation = tblInventoryLocations.id GROUP BY tblInventoryLocations.name, tblInventoryCategories.name, tblInventoryItems.name, tblInventoryTransactions.itemID, tblInventoryTransactions.categoryID, tblInventoryItemValues.costValue, tblInventoryItemValues.marketValue, tblInventoryLocations.name HAVING (SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity) > 0)";
            DataTable dt = new DataTable();

            dt = p.GetDataTable(query);

            gridInventory.DataSource = dt;
            gridInventory.DataBind();
        }

        private void hidePenals()
        {
            pnlCategories.Visible =
                pnlLocationForTransactions.Visible =
                pnlSubCategories.Visible =
                pnlLocation.Visible = false;
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

        protected void btnStockLocationWise_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlLocation.Visible = true;
        }

        protected void btnTransactionsOfItem_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlCategories.Visible = pnlSubCategories.Visible = true;
        }

        protected void btnTransactionsOfLocation_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlLocationForTransactions.Visible = true;
        }

        protected void cmbSourceLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSourceLocation.SelectedIndex > 0)
            {
                try
                {
                    gridLocationWiseStockDataBind();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        private void gridLocationWiseStockDataBind()
        {
            int locationID = int.Parse(cmbSourceLocation.SelectedValue);
            string query = string.Format("SELECT tblInventoryCategories.name AS Category, tblInventoryItems.name AS [Item Name], SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity) AS [Quantity in Hand], tblInventoryItemValues.costValue AS [Cost Price], tblInventoryItemValues.marketValue AS [Market Price], (SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity)) * tblInventoryItemValues.costValue AS [Cost Value], (SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity)) * tblInventoryItemValues.marketValue AS [Market Value] FROM tblInventoryTransactions INNER JOIN tblInventoryCategories ON tblInventoryTransactions.categoryID = tblInventoryCategories.id INNER JOIN tblInventoryItems ON tblInventoryTransactions.itemID = tblInventoryItems.id INNER JOIN tblInventoryItemValues ON tblInventoryTransactions.itemID = tblInventoryItemValues.itemID AND tblInventoryTransactions.categoryID = tblInventoryItemValues.categoryID WHERE (tblInventoryTransactions.itemLocation = {0}) GROUP BY tblInventoryCategories.name, tblInventoryItems.name, tblInventoryTransactions.itemID, tblInventoryTransactions.categoryID, tblInventoryItemValues.costValue, tblInventoryItemValues.marketValue HAVING (SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity) > 0)", locationID);
            DataTable dt = new DataTable();

            dt = p.GetDataTable(query);

            gridInventory.DataSource = dt;
            gridInventory.DataBind();
        }

        protected void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategories.SelectedIndex > 0)
            {
                int category = int.Parse(cmbCategories.SelectedValue);
                string query = string.Format("SELECT name, id FROM tblInventoryItems WHERE categoryID = {0}", category);
                List<DDList> classesList = new List<DDList>();

                classesList = p.getDDList(query);

                cmbSubCategories.DataSource = classesList;
                cmbSubCategories.DataValueField = "Value";
                cmbSubCategories.DataTextField = "DisplayName";
                cmbSubCategories.DataBind();
            }
        }

        protected void cmbSubCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategories.SelectedIndex > 0 && cmbSubCategories.SelectedIndex > 0)
            {
                gridItemTransactionsDataBind();                
            }
        }

        private void gridItemTransactionsDataBind()
        {
            int categoryID = int.Parse(cmbCategories.SelectedValue);
            int itemID = int.Parse(cmbSubCategories.SelectedValue);
            string query = string.Format("SELECT tblInventoryTransactions.date AS Date, tblInventoryTransactions.description AS [Transaction Details], tblInventoryTransactions.receiptQuantity AS Receipts, tblInventoryTransactions.issueQuantity AS Issues FROM tblInventoryTransactions INNER JOIN tblInventoryCategories ON tblInventoryTransactions.categoryID = tblInventoryCategories.id INNER JOIN tblInventoryItems ON tblInventoryTransactions.itemID = tblInventoryItems.id WHERE (tblInventoryTransactions.categoryID = {0}) AND (tblInventoryTransactions.itemID = {1})", categoryID, itemID);
            DataTable dt = new DataTable();

            dt = p.GetDataTable(query);

            gridInventory.DataSource = dt;
            gridInventory.DataBind();
        }

        protected void cmbLocationsForTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocationsForTransactions.SelectedIndex != 0)
            {
                int locationID = int.Parse(cmbLocationsForTransactions.SelectedValue);
                string query = string.Format("SELECT tblInventoryTransactions.date, tblInventoryItems.name AS Item, tblInventoryTransactions.description, tblInventoryTransactions.receiptQuantity AS Receipts, tblInventoryTransactions.issueQuantity AS Issues FROM  tblInventoryTransactions INNER JOIN tblInventoryItems ON tblInventoryTransactions.itemID = tblInventoryItems.id WHERE (tblInventoryTransactions.itemLocation = {0}) ORDER BY tblInventoryTransactions.date", locationID);
                DataTable dt = new DataTable();

                dt = p.GetDataTable(query);

                gridInventory.DataSource = dt;
                gridInventory.DataBind();
            }
        }
    }
}