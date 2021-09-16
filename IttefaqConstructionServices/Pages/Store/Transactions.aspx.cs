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
    public partial class Transactions : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Single gridTotal = 0.0f;
        string inventoryQuery = string.Empty;
        Dictionary<int, int> itemsCategoryDictionary = new Dictionary<int, int>();
        Dictionary<int, string> locationDictionary = new Dictionary<int, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "store, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadCategories();
                    loadLocations();
                    loadBanksCashAndSuppliers();
                    loadPurchaseAccounts();
                    loadExpenses();
                    txtDate.Text = p.fixDateWithMonthNames(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadItemIDCategoryIDDictionary()
        {
            itemsCategoryDictionary.Clear();

            string query = "SELECT id, categoryID FROM tblInventoryItems";
            itemsCategoryDictionary = p.getIntIntDictionary(query);
        }

        private void loadExpenses()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName LIKE '%Expenses:%')";
            List<int> accountIDs = p.getIntList(query);

            if (accountIDs.Count > 0)
            {
                string whereClause = p.getORWhereClauseForIntegers(accountIDs, "parentAccountID");
                string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE ({0}) AND (isActive = '{1}') ORDER BY accountName", whereClause, true);

                List<DDList> ddItems = new List<DDList>();
                ddItems = p.getDDList(ddQuery);

                cmbWastageAccount.DataSource = ddItems;
                cmbWastageAccount.DataTextField = "DisplayName";
                cmbWastageAccount.DataValueField = "Value";
                cmbWastageAccount.DataBind();
            }
        }

        private void loadPurchaseAccounts()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Furniture, Fixture, Tools')";
            int accountID = p.getAccountID(query);

            if (accountID != 0)
            {
                string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) AND (isActive = '{1}') ORDER BY accountName", accountID, true);

                List<DDList> ddItems = new List<DDList>();
                ddItems = p.getDDList(ddQuery);

                cmbInventory.DataSource = ddItems;
                cmbInventory.DataTextField = "DisplayName";
                cmbInventory.DataValueField = "Value";
                cmbInventory.DataBind();
            }
        }

        private void loadBanksCashAndSuppliers()
        {
            string query = "SELECT id FROM tblGenAccounts WHERE (accountName LIKE '%Supplier%' OR accountName = 'Cash in Hand' OR accountName = 'Cash at Bank')";
            List<int> accountIDs = p.getIntList(query);

            if (accountIDs.Count > 0)
            {
                string whereClause = p.getORWhereClauseForIntegers(accountIDs, "parentAccountID");
                string ddQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE ({0}) AND (isActive = '{1}') ORDER BY accountName", whereClause, true);

                List<DDList> ddItems = new List<DDList>();
                ddItems = p.getDDList(ddQuery);

                cmbSuppliers.DataSource = ddItems;
                cmbSuppliers.DataTextField = "DisplayName";
                cmbSuppliers.DataValueField = "Value";
                cmbSuppliers.DataBind();
            }
        }

        private void loadLocations()
        {
            string query = "SELECT [name], [id] FROM [tblInventoryLocations]";
            List<DDList> classesList = new List<DDList>();

            classesList = p.getDDList(query);

            cmbSourceLocation.DataSource = classesList;
            cmbSourceLocation.DataValueField = "Value";
            cmbSourceLocation.DataTextField = "DisplayName";
            cmbSourceLocation.DataBind();

            cmbDestinationLocation.DataSource = classesList;
            cmbDestinationLocation.DataValueField = "Value";
            cmbDestinationLocation.DataTextField = "DisplayName";
            cmbDestinationLocation.DataBind();
        }

        private void loadCategories()
        {
            string query = "SELECT [name], [id] FROM [tblInventoryCategories]";
            List<DDList> classesList = new List<DDList>();

            classesList = p.getDDList(query);

            cmbCategories.DataSource = classesList;
            cmbCategories.DataValueField = "Value";
            cmbCategories.DataTextField = "DisplayName";
            cmbCategories.DataBind();
        }

        protected void btnReceiveItem_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlDetails.Visible = pnlParties.Visible = pnlPurchasesAccount.Visible = pnlReceiveItem.Visible = true;
            loadMainStoreGridForPurchase();
            pnlInventoryPurchase.Visible = true;
        }

        private void hidePenals()
        {
            pnlCategories.Visible =
                pnlInventoryPurchase.Visible =
                pnlInventorySale.Visible = 
                pnlInventoryGenIssuance.Visible = 
                pnlDate.Visible =
                pnlDetails.Visible =
                pnlInternalIssuance.Visible =
                pnlLocation1.Visible =
                pnlLocation2.Visible =
                pnlParties.Visible =
                pnlPurchasesAccount.Visible =
                pnlReceiveItem.Visible =
                pnlSaleItem.Visible =
                pnlSubCategories.Visible =
                pnlWastage.Visible =
                pnlSimpleQty.Visible =
                pnlWastageAccount.Visible = false;                
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

        protected void btnSaleItem_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlDetails.Visible = pnlParties.Visible = pnlPurchasesAccount.Visible = pnlSaleItem.Visible = true;
            loadMainStoreGridForSales();
            pnlInventorySale.Visible = true;
        }

        private void loadMainStoreGridForSales()
        {
            string query = "SELECT tblInventoryCategories.name AS Category, tblInventoryItems.name AS [Item Name], SUM(tblInventoryTransactions.receiptQuantity) AS Receipts, SUM(tblInventoryTransactions.issueQuantity) AS Issues, SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity) AS [Quantity in Hand], tblInventoryItems.id AS id FROM tblInventoryTransactions INNER JOIN tblInventoryCategories ON tblInventoryTransactions.categoryID = tblInventoryCategories.id INNER JOIN tblInventoryItems ON tblInventoryTransactions.itemID = tblInventoryItems.id WHERE        (tblInventoryTransactions.itemLocation = 1) GROUP BY tblInventoryCategories.name, tblInventoryItems.name, tblInventoryItems.id HAVING        (SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity) > 0)";
            DataTable dt = p.GetDataTable(query);

            gridInventorySale.DataSource = dt;
            gridInventorySale.DataBind();
        }

        protected void btnInternalIssuance_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlSimpleQty.Visible = pnlLocation1.Visible = pnlLocation2.Visible = pnlInternalIssuance.Visible = true;
            pnlInternalIssuance.Visible = true;
        }

        protected void btnWastage_Click(object sender, EventArgs e)
        {
            clearLabels();
            hidePenals();
            pnlDate.Visible = pnlDetails.Visible = pnlWastageAccount.Visible = pnlPurchasesAccount.Visible = pnlWastage.Visible = true;
            loadMainStoreGridForSales();
            pnlInventorySale.Visible = true;
        }

        protected void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();

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

        protected void loadMainStoreGridForPurchase()
        {
            string query = "SELECT tblInventoryItems.id, tblInventoryCategories.name AS [Category], tblInventoryItems.name AS [Item Name] FROM tblInventoryCategories INNER JOIN tblInventoryItems ON tblInventoryCategories.id = tblInventoryItems.categoryID";
            DataTable dt = p.GetDataTable(query);

            gridInventorySalesAndPurchase.DataSource = dt;
            gridInventorySalesAndPurchase.DataBind();
        }

        protected void cmbSourceLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSourceLocation.SelectedIndex > 0)
            {
                int locationID = int.Parse(cmbSourceLocation.SelectedValue);
                loadStockAtLocationGrid(locationID);
                pnlInventoryGenIssuance.Visible = true; 
            }
        }

        private void loadStockAtLocationGrid(int locationID)
        {
            string query = string.Format("SELECT tblInventoryItems.id, tblInventoryCategories.name AS Category, tblInventoryItems.name AS [Item Name], SUM(tblInventoryTransactions.receiptQuantity) AS Receipts, SUM(tblInventoryTransactions.issueQuantity) AS Issues, SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity) AS [Quantity in Hand] FROM tblInventoryTransactions INNER JOIN tblInventoryCategories ON tblInventoryTransactions.categoryID = tblInventoryCategories.id INNER JOIN tblInventoryItems ON tblInventoryTransactions.itemID = tblInventoryItems.id WHERE (tblInventoryTransactions.itemLocation = {0}) GROUP BY tblInventoryCategories.name, tblInventoryItems.name, tblInventoryItems.id HAVING (SUM(tblInventoryTransactions.receiptQuantity) - SUM(tblInventoryTransactions.issueQuantity) > 0)", locationID);
            DataTable dt = p.GetDataTable(query);

            gridInventoryIssuance.DataSource = dt;
            gridInventoryIssuance.DataBind();
        }

        protected void btnReceiveItem1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSuppliers.SelectedIndex > 0 && cmbInventory.SelectedIndex > 0)
            {
                try
                {
                    loadItemIDCategoryIDDictionary();
                    gridTotal = 0.0f;
                    inventoryQuery = string.Empty;
                    Single enteredAmount = 0.0f;
                    Single.TryParse(txtAmount.Text, out enteredAmount);
                    loopThroughGridForReceipts();

                    if (enteredAmount == gridTotal && enteredAmount > 0f)
                    {
                        DateTime date = DateTime.Parse(txtDate.Text);
                        string description = p.FixString(txtDescription.Text);

                        int debitAccountID = int.Parse(cmbInventory.SelectedValue);
                        int creditAccountID = int.Parse(cmbSuppliers.SelectedValue);
                        string chequeNumber = p.FixString(txtChequeNumber.Text);
                        Single quantity = 0f;
                        string UoM = string.Empty;
                        Single rate = 0f;
                        Single zero = 0f;
                        int transID = p.getTransactionID("tblGenAccountsTransactions");
                        string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + enteredAmount + description + "HeatFoodWithElectricity";
                        string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + enteredAmount + description + "HeatFoodWithElectricity";
                        string debitAccountChecksum = p.EncryptStringToHex(debitString);
                        string creditAccountChecksum = p.EncryptStringToHex(creditString);

                        string startTransQuery = "Begin Transaction ";
                        string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, enteredAmount, zero, description, debitAccountChecksum);
                        string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, enteredAmount, description, creditAccountChecksum);
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + debitTransQuery + creditTransQuery + inventoryQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);
                        loadMainStoreGridForPurchase();
                        showSuccessMessage("Purchase recorded and record updated successfully.");                       
                    }
                    else
                    {
                        showWarningMessage("The entered amount is not equal to the total amount entered in the grid. Please check and try again.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("First select proper values from dropdowns and then try again.");
            }
        }

        private void loopThroughGridForReceipts()
        {
            DateTime date = DateTime.Parse(txtDate.Text);
            string description = p.FixString(txtDescription.Text);
            for (int i = 0; i < gridInventorySalesAndPurchase.Rows.Count; i++)
            {
                TextBox txtQty = (TextBox)gridInventorySalesAndPurchase.Rows[i].FindControl("txtQty");
                TextBox txtAmt = (TextBox)gridInventorySalesAndPurchase.Rows[i].FindControl("txtAmount");

                Single quantity = 0f;
                Single amount = 0f;

                Single.TryParse(txtQty.Text, out quantity);
                Single.TryParse(txtAmt.Text, out amount);

                if (quantity > 0f && amount > 0f)
                {
                    Label lblID = (Label)gridInventorySalesAndPurchase.Rows[i].FindControl("lblItemID");

                    int itemID = 0;
                    int.TryParse(lblID.Text, out itemID);
                    int categoryID = 0;
                    itemsCategoryDictionary.TryGetValue(itemID, out categoryID);
                    int itemLocation = 1;

                    string query = string.Format("  INSERT INTO tblInventoryTransactions(date, categoryID, itemID, itemLocation, description, receiptQuantity, amount) VALUES ('{0}',{1},{2},{3},'{4}',{5},{6})", date, categoryID, itemID, itemLocation, description, quantity, amount);
                    inventoryQuery = inventoryQuery + query;
                    gridTotal += amount;
                }
            }
        }

        protected void btnSaleItem1_Click(object sender, EventArgs e)
        {
            clearLabels();
            gridTotal = 0.0f;
            clearLabels();
            if (cmbSuppliers.SelectedIndex > 0 && cmbInventory.SelectedIndex > 0)
            {
                try
                {
                    loadItemIDCategoryIDDictionary();
                    gridTotal = 0.0f;
                    inventoryQuery = string.Empty;
                    Single enteredAmount = 0.0f;
                    Single.TryParse(txtAmount.Text, out enteredAmount);
                    loopThroughGridForSales();

                    if (enteredAmount == gridTotal && enteredAmount > 0f)
                    {
                        DateTime date = DateTime.Parse(txtDate.Text);
                        string description = p.FixString(txtDescription.Text);

                        int creditAccountID = int.Parse(cmbInventory.SelectedValue);
                        int debitAccountID = int.Parse(cmbSuppliers.SelectedValue);
                        string chequeNumber = p.FixString(txtChequeNumber.Text);
                        Single quantity = 0f;
                        string UoM = string.Empty;
                        Single rate = 0f;
                        Single zero = 0f;
                        int transID = p.getTransactionID("tblGenAccountsTransactions");
                        string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + enteredAmount + description + "HeatFoodWithElectricity";
                        string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + enteredAmount + description + "HeatFoodWithElectricity";
                        string debitAccountChecksum = p.EncryptStringToHex(debitString);
                        string creditAccountChecksum = p.EncryptStringToHex(creditString);

                        string startTransQuery = "Begin Transaction ";
                        string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, enteredAmount, zero, description, debitAccountChecksum);
                        string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, enteredAmount, description, creditAccountChecksum);
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + debitTransQuery + creditTransQuery + inventoryQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);
                        loadMainStoreGridForSales();
                        showSuccessMessage("Sales recorded and record updated successfully.");
                    }
                    else
                    {
                        showWarningMessage("The entered amount is not equal to the total amount entered in the grid. Please check and try again.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("First select proper values from dropdowns and then try again.");
            }
        }

        private void loopThroughGridForSales()
        {
            DateTime date = DateTime.Parse(txtDate.Text);
            string description = p.FixString(txtDescription.Text);
            for (int i = 0; i < gridInventorySale.Rows.Count; i++)
            {
                TextBox txtQty = (TextBox)gridInventorySale.Rows[i].FindControl("txtQty");
                TextBox txtAmt = (TextBox)gridInventorySale.Rows[i].FindControl("txtAmount");

                Single quantity = 0f;
                Single amount = 0f;

                Single.TryParse(txtQty.Text, out quantity);
                Single.TryParse(txtAmt.Text, out amount);

                if (quantity > 0f && amount > 0f)
                {
                    Label lblInHandQty = (Label)gridInventorySale.Rows[i].FindControl("lblQtyInHand");
                    Single QIH = 0f;
                    Single.TryParse(lblInHandQty.Text, out QIH);

                    if (QIH >= quantity)
                    {
                        Label lblID = (Label)gridInventorySale.Rows[i].FindControl("lblItemID");
                        int itemID = 0;
                        int.TryParse(lblID.Text, out itemID);
                        int categoryID = 0;
                        itemsCategoryDictionary.TryGetValue(itemID, out categoryID);
                        int itemLocation = 1;

                        string query = string.Format("  INSERT INTO tblInventoryTransactions(date, categoryID, itemID, itemLocation, description, issueQuantity, amount) VALUES ('{0}',{1},{2},{3},'{4}',{5},{6})", date, categoryID, itemID, itemLocation, description, quantity, amount);
                        inventoryQuery = inventoryQuery + query;
                        gridTotal += amount;
                    }
                    else
                    {
                        throw new Exception("You can't sale/issue/waste more than you have. Please revise the grid quantities and try again.");
                    }
                }
            }
        }

        protected void btnInternalIssuance1_Click(object sender, EventArgs e)
        {
            clearLabels();
            if (cmbSourceLocation.SelectedIndex > 0  && cmbDestinationLocation.SelectedIndex > 0)
            {
                inventoryQuery = string.Empty;
                try
                {
                    loadItemIDCategoryIDDictionary();
                    loopthroughGridForInternalIssuance();
                    string startTransQuery = "Begin Transaction ";
                    string endTransQuery = " Commit Transaction";

                    string transQuery = startTransQuery + inventoryQuery + endTransQuery;

                    p.ExecuteQuery(transQuery);
                    gridInventoryIssuance.DataSource = null;
                    gridInventoryIssuance.DataBind();
                    cmbSourceLocation.SelectedIndex = cmbDestinationLocation.SelectedIndex = 0;
                    showSuccessMessage("Internal issuance recorded and record updated successfully.");

                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("Please choose proper values from both locations and try again.");
            }
        }

        private void loopthroughGridForInternalIssuance()
        {
            loadLocationDictionary();
            DateTime date = DateTime.Parse(txtDate.Text);
            string source = string.Empty;
            string destination = string.Empty;

            int sourceLocation = int.Parse(cmbSourceLocation.SelectedValue);
            int destinationLocation = int.Parse(cmbDestinationLocation.SelectedValue);

            locationDictionary.TryGetValue(sourceLocation, out source);
            locationDictionary.TryGetValue(destinationLocation, out destination);

            string sourceDescription = "Transferred to: " + destination;
            string destinationDescription = "Transferred from: " + source;

            for (int i = 0; i < gridInventoryIssuance.Rows.Count; i++)
            {
                TextBox txtQty = (TextBox)gridInventoryIssuance.Rows[i].FindControl("txtQty");

                Single quantity = 0f;

                Single.TryParse(txtQty.Text, out quantity);

                if (quantity > 0f)
                {
                    Label lblInHandQty = (Label)gridInventoryIssuance.Rows[i].FindControl("lblQtyInHand");
                    Single QIH = 0f;
                    Single.TryParse(lblInHandQty.Text, out QIH);

                    if (QIH >= quantity)
                    {
                        Label lblID = (Label)gridInventoryIssuance.Rows[i].FindControl("lblItemID");
                        int itemID = 0;
                        int.TryParse(lblID.Text, out itemID);
                        int categoryID = 0;
                        itemsCategoryDictionary.TryGetValue(itemID, out categoryID);

                        string issueQuery = string.Format("  INSERT INTO tblInventoryTransactions(date, categoryID, itemID, itemLocation, description, issueQuantity) VALUES ('{0}',{1},{2},{3},'{4}',{5})", date, categoryID, itemID, sourceLocation, sourceDescription, quantity);
                        string receiptQuery = string.Format("  INSERT INTO tblInventoryTransactions(date, categoryID, itemID, itemLocation, description, receiptQuantity) VALUES ('{0}',{1},{2},{3},'{4}',{5})", date, categoryID, itemID, destinationLocation, destinationDescription, quantity); 
                        inventoryQuery = inventoryQuery + issueQuery + receiptQuery;
                    }
                    else
                    {
                        throw new Exception("You can't sale/issue/waste more than you have. Please revise the grid quantities and try again.");
                    }
                }
            }
        }

        private void loadLocationDictionary()
        {
            locationDictionary.Clear();
            string query = "SELECT id, name FROM tblInventoryLocations";
            locationDictionary = p.getIntStringDictionary(query);
        }

        protected void btnWastage1_Click(object sender, EventArgs e)
        {
            clearLabels();
            gridTotal = 0.0f;
            clearLabels();
            if (cmbWastageAccount.SelectedIndex > 0 && cmbInventory.SelectedIndex > 0)
            {
                try
                {
                    loadItemIDCategoryIDDictionary();
                    gridTotal = 0.0f;
                    inventoryQuery = string.Empty;
                    Single enteredAmount = 0.0f;
                    Single.TryParse(txtAmount.Text, out enteredAmount);
                    loopThroughGridForSales();

                    if (enteredAmount == gridTotal && enteredAmount > 0f)
                    {
                        DateTime date = DateTime.Parse(txtDate.Text);
                        string description = p.FixString(txtDescription.Text);

                        int creditAccountID = int.Parse(cmbInventory.SelectedValue);
                        int debitAccountID = int.Parse(cmbWastageAccount.SelectedValue);
                        string chequeNumber = p.FixString(txtChequeNumber.Text);
                        Single quantity = 0f;
                        string UoM = string.Empty;
                        Single rate = 0f;
                        Single zero = 0f;
                        int transID = p.getTransactionID("tblGenAccountsTransactions");
                        string debitString = transID.ToString() + debitAccountID.ToString() + chequeNumber + quantity + UoM + rate + enteredAmount + description + "HeatFoodWithElectricity";
                        string creditString = transID.ToString() + creditAccountID.ToString() + chequeNumber + quantity + UoM + rate + enteredAmount + description + "HeatFoodWithElectricity";
                        string debitAccountChecksum = p.EncryptStringToHex(debitString);
                        string creditAccountChecksum = p.EncryptStringToHex(creditString);

                        string startTransQuery = "Begin Transaction ";
                        string debitTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}')", transID, debitAccountID, date, chequeNumber, quantity, UoM, rate, enteredAmount, zero, description, debitAccountChecksum);
                        string creditTransQuery = string.Format("  Insert INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}')", transID, creditAccountID, date, chequeNumber, quantity, UoM, rate, zero, enteredAmount, description, creditAccountChecksum);
                        string endTransQuery = " Commit Transaction";

                        string transQuery = startTransQuery + debitTransQuery + creditTransQuery + inventoryQuery + endTransQuery;

                        p.ExecuteQuery(transQuery);
                        loadMainStoreGridForSales();
                        showSuccessMessage("Sales recorded and record updated successfully.");
                    }
                    else
                    {
                        showWarningMessage("The entered amount is not equal to the total amount entered in the grid. Please check and try again.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("First select proper values from dropdowns and then try again.");
            }
        }
    }
}