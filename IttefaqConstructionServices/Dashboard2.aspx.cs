using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices
{
    public partial class Dashboard2 : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        List<Account> data1 = new List<Account>();
        List<Account> data2 = new List<Account>();
        List<Account> data3 = new List<Account>();
        List<Account> data4 = new List<Account>();
        List<Account> balances = new List<Account>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "administrator, deo", Session["userRoles"].ToString()))
            {
                string userName = Session["UserName"].ToString();
                TextBox1.Text = userName;

                if (!IsPostBack)
                {
                    data1.Clear();
                    data2.Clear();
                    data3.Clear();
                    data4.Clear();
                    balances.Clear();

                    populateBalances();
                    processSites();
                    processProjectManagers();
                    processSuppliers();
                    processPersonalLedgers();
                    processCashInHand();
                    processCashAtBank();
                    processIncome();
                    processExpenses();
                    processAssets();
                    processLiabilities();

                    GridView1.DataSource = data1;
                    GridView1.DataBind();
                    GridView2.DataSource = data2;
                    GridView2.DataBind();
                    GridView3.DataSource = data3;
                    GridView3.DataBind();
                    GridView4.DataSource = data4;
                    GridView4.DataBind();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void processLiabilities()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Other Liabilities:";
            head.typeName = "Head: ";
            data4.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 2) AND (isActive = 'true') AND (specialNotes = 'Others') ORDER BY accountName";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    data4.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            if (balance != 0)
                            {
                                Account item = new Account();
                                item.Name = drLiab[0].ToString();
                                item.Debit = balance;
                                item.description = "AI:" + suppSubID.ToString();
                                data4.Add(item);
                                total += balance;
                            }
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        data4.Add(totalItem);
                    }
                }
            }
        }

        private void processAssets()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Other Assets:";
            head.typeName = "Head: ";
            data4.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 1) AND (isActive = 'true') AND (specialNotes = 'Others') ORDER BY accountName";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    data4.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            if (balance != 0)
                            {
                                Account item = new Account();
                                item.Name = drLiab[0].ToString();
                                item.Debit = balance;
                                item.description = "AI:" + suppSubID.ToString();
                                data4.Add(item);
                                total += balance;
                            }
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        data4.Add(totalItem);
                    }
                }
            }
        }

        private void processCashAtBank()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Bank Accounts:";
            head.typeName = "Head: ";
            data3.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Banks') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    data3.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            if (balance != 0)
                            {
                                Account item = new Account();
                                item.Name = drLiab[0].ToString();
                                item.Debit = balance;
                                item.description = "PI:" + suppID.ToString();
                                data3.Add(item);
                                total += balance;
                            }
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        data3.Add(totalItem);
                    }
                }
            }
        }

        private void processCashInHand()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Cash and Petty Cash:";
            head.typeName = "Head: ";
            data3.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Cash') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    data3.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            Account item = new Account();
                            if (balance != 0)
                            {
                                item.Name = drLiab[0].ToString();
                                item.Debit = balance;
                                item.description = "PI:" + suppID.ToString();
                                data3.Add(item);
                                total += balance;
                            }
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        data3.Add(totalItem);
                    }
                }
            }
        }

        private void processExpenses()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Expenses:";
            head.typeName = "Head: ";
            data3.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 6) AND (isActive = 'true') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    data3.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID) * -1;
                            Account item = new Account();
                            if (balance != 0)
                            {
                                item.Name = drLiab[0].ToString();
                                item.Debit = balance;
                                item.description = "PI:" + suppID.ToString();
                                data3.Add(item);
                                total += balance;
                            }
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        data3.Add(totalItem);
                    }
                }
            }
        }

        private void processIncome()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Incomes:";
            head.typeName = "Head: ";
            data3.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = 4) AND (isActive = 'true') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    data3.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID);
                            Account item = new Account();
                            if (balance != 0)
                            {
                                item.Name = drLiab[0].ToString();
                                item.Debit = balance;
                                item.description = "PI:" + suppID.ToString();
                                data3.Add(item);
                                total += balance;
                            }
                        }

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        data3.Add(totalItem);
                    }
                }
            }
        }

        private void processPersonalLedgers()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Personal Ledgers:";
            head.typeName = "Head: ";
            data2.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Personal Ledgers') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal totalLiab = 0;
                    Decimal totalAdv = 0;
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    data2.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID);

                            if (balance > 0)
                            {
                                totalLiab += balance;
                                total += balance;

                            }
                            else
                            {
                                totalAdv += balance;
                                total += balance;
                            }
                        }

                        Account advances = new Account();
                        advances.Name = "Total Advances";
                        advances.Debit = totalAdv;
                        advances.description = "PI:" + suppID.ToString();
                        data2.Add(advances);

                        Account liabilities = new Account();
                        liabilities.Name = "Total Liabilities";
                        liabilities.Debit = totalLiab;
                        liabilities.description = "PI:" + suppID.ToString();
                        data2.Add(liabilities);

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        data2.Add(totalItem);
                    }
                }
            }
        }

        private void processSuppliers()
        {
            //Setup head
            Account head = new Account();
            head.Name = "Suppliers:";
            head.typeName = "Head: ";
            data2.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts WHERE (specialNotes = 'Suppliers') ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal totalLiab = 0;
                    Decimal totalAdv = 0;
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int suppID = 0;
                    int.TryParse(dr[1].ToString(), out suppID);
                    acc.Name = dr[0].ToString();
                    acc.typeName = "Sub";
                    data2.Add(acc);

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", suppID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            int suppSubID = 0;
                            int.TryParse(drLiab[1].ToString(), out suppSubID);
                            Decimal balance = getBalance(suppSubID);

                            if (balance > 0)
                            {
                                totalLiab += balance;
                                total += balance;

                            }
                            else
                            {
                                totalAdv += balance;
                                total += balance;
                            }
                        }

                        Account advances = new Account();
                        advances.Name = "Total Advances";
                        advances.Debit = totalAdv;
                        advances.description = "PI:" + suppID.ToString();
                        data2.Add(advances);

                        Account liabilities = new Account();
                        liabilities.Name = "Total Liabilities";
                        liabilities.Debit = totalLiab;
                        liabilities.description = "PI:" + suppID.ToString();
                        data2.Add(liabilities);

                        Account totalItem = new Account();
                        totalItem.Name = "Total:";
                        totalItem.Debit = total;
                        data2.Add(totalItem);
                    }
                }
            }
        }

        private void populateBalances()
        {
            balances.Clear();
            string query = "SELECT id, Status FROM viewGenAccountsGroupByPrimaryCreditSide";
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Account a = new Account();
                    int id = 0;
                    int.TryParse(dr[0].ToString(), out id);
                    Decimal balance = 0;
                    Decimal.TryParse(dr[1].ToString(), out balance);

                    a.ID = id;
                    a.Debit = balance;

                    balances.Add(a);
                }
            }
        }

        private void processProjectManagers()
        {
            //Setup head
            Account head = new Account();
            head.Name = "PMs and Site Liabilities:";
            head.typeName = "Head: ";
            data2.Add(head);

            //Get Project Managers
            string PMQuery = "SELECT accountName, id FROM tblGenAccounts " + p.getWhereClauseForActiveAccounts("Project Manager") + " ORDER BY accountName ";
            DataTable dtPM = p.GetDataTable(PMQuery);

            if (dtPM.Rows.Count > 0)
            {
                for (int i = 0; i < dtPM.Rows.Count; i++)
                {
                    Decimal total = 0;
                    DataRow dr = dtPM.Rows[i];
                    Account acc = new Account();
                    int pmID = 0;
                    int.TryParse(dr[1].ToString(), out pmID);
                    acc.Name = dr[0].ToString();
                    acc.Debit = getBalance(pmID);
                    acc.description = "AI:" + pmID.ToString();
                    data2.Add(acc);
                    total += acc.Debit;

                    //Get Site Liabilities associated with each project manager
                    string liabQuery = string.Format("SELECT tblGenAccounts.accountName, tblSiteLiabAssociationWithPM.liabAccountID FROM tblSiteLiabAssociationWithPM INNER JOIN                          tblGenAccounts ON tblSiteLiabAssociationWithPM.liabAccountID = tblGenAccounts.id WHERE projectManagerID = {0}", pmID);
                    DataTable dtLiab = p.GetDataTable(liabQuery);

                    if (dtLiab.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtLiab.Rows.Count; j++)
                        {
                            DataRow drLiab = dtLiab.Rows[j];
                            Account accLiab = new Account();
                            int liabID = 0;
                            int.TryParse(drLiab[1].ToString(), out liabID);
                            accLiab.ID = liabID;
                            accLiab.Name = drLiab[0].ToString();
                            accLiab.Debit = getBalance(liabID);
                            accLiab.description = "AI:" + liabID.ToString();
                            data2.Add(accLiab);
                            total += accLiab.Debit;
                        }
                    }

                    Account totalAcc = new Account();
                    totalAcc.Name = "Total: " + acc.Name;
                    totalAcc.Debit = total;
                    data2.Add(totalAcc);
                }
            }
        }

        private void processSites()
        {
            //Account head = new Account();
            //head.Name = "Site Status:";
            //head.typeName = "Head: ";
            //data1.Add(head);

            populateSitesDataForCoverPage();
        }

        private void populateSitesDataForCoverPage()
        {
            string query = "SELECT Name, Status FROM viewActiveSitesSummary ORDER BY Name";

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Account a = new Account();

                    //a.Name = "-----" + dr[0].ToString();
                    Decimal debit = 0;
                    Decimal.TryParse(dr[1].ToString(), out debit);

                    if (debit != 0)
                    {
                        a.Name = dr[0].ToString();
                        a.Debit = debit;
                        a.description = "Sites";
                        data1.Add(a);
                    }
                }
            }
        }

        private decimal getBalance(int pmID)
        {
            Decimal balance = 0;

            for (int i = 0; i < balances.Count; i++)
            {
                if (balances[i].ID == pmID)
                {
                    balance = balances[i].Debit;
                }
            }
            return balance;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string text1 = e.Row.Cells[0].Text;
                if (text1.Contains("&amp;"))
                {
                    text1 = text1.Replace("&amp;", "&");
                }

                bool specialRow = false;
                bool subRow = false;

                for (int i = 0; i < data1.Count; i++)
                {
                    if (data1[i].Name == text1 && data1[i].typeName == "Head: ")
                    {
                        specialRow = true;
                        break;
                    }
                    if (data1[i].Name == text1 && data1[i].typeName == "Sub")
                    {
                        subRow = true;
                        break;
                    }
                }

                if (specialRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 11;
                    e.Row.Cells[0].BackColor = Color.Beige;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.Beige;
                }

                if (subRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 8;
                    e.Row.Cells[0].BackColor = Color.ForestGreen;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.ForestGreen;
                }
                else
                {
                    e.Row.Cells[0].Font.Size = 7;
                    e.Row.Cells[1].Font.Size = 8;
                    e.Row.Height = 3;
                }
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string text1 = e.Row.Cells[0].Text;
                if (text1.Contains("&amp;"))
                {
                    text1 = text1.Replace("&amp;", "&");
                }

                bool specialRow = false;
                bool subRow = false;
                bool totalRow = false;

                for (int i = 0; i < data2.Count; i++)
                {
                    if (data2[i].Name == text1 && data2[i].typeName == "Head: ")
                    {
                        specialRow = true;
                        break;
                    }
                    if (data2[i].Name == text1 && data2[i].typeName == "Sub")
                    {
                        subRow = true;
                        break;
                    }
                    if (text1.Contains("Total:"))
                    {
                        totalRow = true;
                        break;
                    }
                }

                if (specialRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 11;
                    e.Row.Cells[0].BackColor = Color.Beige;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.Beige;
                }

                else if (subRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 8;
                    e.Row.Cells[0].BackColor = Color.ForestGreen;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.ForestGreen;
                }

                else if (totalRow)
                {
                    LinkButton lnkButton = (LinkButton)(e.Row.FindControl("viewStatus"));
                    lnkButton.Enabled = false;
                    e.Row.Cells[0].Font.Size = 8;
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[1].Font.Size = 8;
                    e.Row.Cells[1].Font.Bold = true;
                    lnkButton.ForeColor = System.Drawing.Color.Black;
                }

                else
                {
                    e.Row.Cells[0].Font.Size = 7;
                    e.Row.Cells[1].Font.Size = 8;
                    e.Row.Height = 3;
                }
            }
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string text1 = e.Row.Cells[0].Text;
                if (text1.Contains("&amp;"))
                {
                    text1 = text1.Replace("&amp;", "&");
                }

                bool specialRow = false;
                bool subRow = false;
                bool totalRow = false;

                for (int i = 0; i < data3.Count; i++)
                {
                    if (data3[i].Name == text1 && data3[i].typeName == "Head: ")
                    {
                        specialRow = true;
                        break;
                    }
                    if (data3[i].Name == text1 && data3[i].typeName == "Sub")
                    {
                        subRow = true;
                        break;
                    }
                    if (text1.Contains("Total:"))
                    {
                        totalRow = true;
                        break;
                    }
                }

                if (specialRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 11;
                    e.Row.Cells[0].BackColor = Color.Beige;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.Beige;
                }

                else if (subRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 8;
                    e.Row.Cells[0].BackColor = Color.ForestGreen;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.ForestGreen;
                }
                else if (totalRow)
                {
                    LinkButton lnkButton = (LinkButton)(e.Row.FindControl("viewStatus"));
                    lnkButton.Enabled = false;
                    e.Row.Cells[0].Font.Size = 8;
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[1].Font.Size = 8;
                    e.Row.Cells[1].Font.Bold = true;
                    lnkButton.ForeColor = System.Drawing.Color.Black;
                }

                else
                {
                    e.Row.Cells[0].Font.Size = 7;
                    e.Row.Cells[1].Font.Size = 8;
                    e.Row.Height = 3;
                }
            }
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string text1 = e.Row.Cells[0].Text;
                if (text1.Contains("&amp;"))
                {
                    text1 = text1.Replace("&amp;", "&");
                }

                bool specialRow = false;
                bool subRow = false;
                bool totalRow = false;

                for (int i = 0; i < data4.Count; i++)
                {
                    if (data4[i].Name == text1 && data4[i].typeName == "Head: ")
                    {
                        specialRow = true;
                        break;
                    }
                    if (data4[i].Name == text1 && data4[i].typeName == "Sub")
                    {
                        subRow = true;
                        break;
                    }
                    if (text1.Contains("Total:"))
                    {
                        totalRow = true;
                        break;
                    }
                }

                if (specialRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 11;
                    e.Row.Cells[0].BackColor = Color.Beige;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.Beige;
                }

                else if (subRow)
                //if (e.Row.Cells[0].Text.StartsWith("::"))
                {
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = 8;
                    e.Row.Cells[0].BackColor = Color.ForestGreen;
                    e.Row.Cells[1].Text = string.Empty;
                    e.Row.Cells[1].BackColor = Color.ForestGreen;
                }
                else if (totalRow)
                {
                    LinkButton lnkButton = (LinkButton)(e.Row.FindControl("viewStatus"));
                    lnkButton.Enabled = false;
                    e.Row.Cells[0].Font.Size = 8;
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[1].Font.Size = 8;
                    e.Row.Cells[1].Font.Bold = true;
                    lnkButton.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    e.Row.Cells[0].Font.Size = 7;
                    e.Row.Cells[1].Font.Size = 8;
                    e.Row.Height = 3;
                }
            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //hidePenals();
            //clearLabels();
            //int a = 0;
            try
            {
                string command = e.CommandArgument.ToString();
                if (command != string.Empty)
                {
                    if (command.Contains("Site"))
                    {
                        string url = string.Format("SiteDetails.aspx?command={0}", command);
                        Response.Write("<script>window.open('" + url + "','_blank');</script>");
                    }
                    else
                    {
                        string url = string.Format("AccountDetails.aspx?command={0}", command);
                        Response.Write("<script>window.open('" + url + "','_blank');</script>");
                    }
                }
            }
            catch (Exception )
            {
                //showWarningMessage(ex.Message);
            }
        }
    }
}