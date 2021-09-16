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
    public partial class SalariesDisbursement : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Single totalStatus = 0.0f;
        List<AccountStructure> debitAccounts = new List<AccountStructure>();
        List<AccountStructure> creditAccounts = new List<AccountStructure>();
        IttefaqConstructionServices.Logic.Accounts a = new IttefaqConstructionServices.Logic.Accounts();
        List<Salary> salaries = new List<Salary>();
        int salaryMonth = 0;
        int salaryYear = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "hr, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadCashBook();
                    loadStaff();
                    loadSiteAccounts();
                    loadGenAccounts();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadGenAccounts()
        {
            string parentQuery = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Expenses: Operating')";
            int parentID = p.getIntValue(parentQuery);

            string query = string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", parentID);

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(query);

            cmbGenSalAccount.Items.Clear();
            cmbGenSalAccount.DataSource = ddItems;
            cmbGenSalAccount.DataTextField = "DisplayName";
            cmbGenSalAccount.DataValueField = "Value";
            cmbGenSalAccount.DataBind();
        }

        private void loadSiteAccounts()
        {
            string grandParentQuery = "SELECT id FROM tblGenAccounts WHERE (accountName = 'Site Inputs')";
            int gpAccID = p.getIntValue(grandParentQuery);

            string parentQuery = string.Format("SELECT id FROM tblGenAccounts WHERE (accountName = 'Salaries and Wages') AND (parentAccountID = {0})", gpAccID);
            int parentID = p.getIntValue(parentQuery);

            string query =  string.Format("SELECT accountName, id FROM tblGenAccounts WHERE (parentAccountID = {0}) ORDER BY accountName", parentID);

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(query);

            cmbSiteSalAccount.Items.Clear();
            cmbSiteSalAccount.DataSource = ddItems;
            cmbSiteSalAccount.DataTextField = "DisplayName";
            cmbSiteSalAccount.DataValueField = "Value";
            cmbSiteSalAccount.DataBind();
        }

        private void loadStaff()
        {
            string query = "SELECT staffName + ' - ' + staffFather AS Staff, id, currentSalary FROM tblGenStaff ORDER BY Staff";

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                gridStaff.DataSource = dt;
                gridStaff.DataBind();
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

        protected void gridPriSitesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lnkBtn = (Label)e.Row.FindControl("lblStatus");

                if (lnkBtn != null)
                {
                    totalStatus += Single.Parse(lnkBtn.Text);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = totalStatus.ToString("0,0.00");
                e.Row.Cells[1].Font.Bold = true;
            }
        }

        private void loadCashBook()
        {
            string cashInHandQuery = "SELECT id FROM tblGenAccounts WHERE accountName = 'Cash in Hand'";
            int cashInHandAccount = p.getIntValue(cashInHandQuery);

            string cashAtBankQuery = "SELECT id FROM tblGenAccounts WHERE accountName = 'Cash at Bank'";
            int cashAtBankAccount = p.getIntValue(cashAtBankQuery);

            string query = string.Format("select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = {0} OR ParentID = {1}", cashAtBankAccount, cashInHandAccount);
            DataTable dt = p.GetDataTable(query);
            gridPriSitesInfo.DataSource = dt;
            gridPriSitesInfo.DataBind();

            if (dt.Rows.Count > 0)
            {
                gridPriSitesInfo.UseAccessibleHeader = true;
                gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            clearLabels();
            salaryMonth = 0;
            salaryYear = 0;

            try
            {
                string[] particulars = txtAmount.Text.Split('-');
                salaryMonth = int.Parse(particulars[0]);
                salaryYear = int.Parse(particulars[1]);

                string query = string.Format("SELECT id, salaryPayable, salaryPaid, remarks FROM tblGenSalaryStatement WHERE salaryMonth = {0} AND salaryYear = {1}", salaryMonth, salaryYear);

                if (!p.ifRecordsExist(query))
                {
                    Decimal totalPayments = getTotalPayments();
                    Decimal totalSalary = getTotalSalary();

                    if (totalPayments == totalSalary)
                    {
                        if (cmbGenSalAccount.SelectedIndex > 0 && cmbSiteSalAccount.SelectedIndex > 0)
                        {
                            distributeAndAllocateSalaries();
                            a.finalizeTransaction(Session["UserName"].ToString());
                            showSuccessMessage("Salaries paid and allocated successfully.");
                        }
                        else
                        {
                            showWarningMessage("Please select proper salaries accounts first.");
                        }
                    }
                    else
                    {
                        showWarningMessage("Total Payments are not equal to Total Salary. Please check and try again.");
                    }
                }
                else
                {
                    showWarningMessage("Salary of month: " + salaryMonth.ToString() + " year: " + salaryYear.ToString() + " already exists.");
                }
            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private void distributeAndAllocateSalaries()
        {
            int siteSalAccount = int.Parse(cmbSiteSalAccount.SelectedValue);
            int genSalAccount = int.Parse(cmbGenSalAccount.SelectedValue);
            prepareDebitAccounts(salaries, siteSalAccount, genSalAccount);
            writeListsAndFinalize();
        }

        private void writeListsAndFinalize()
        {
            string user = Session["UserName"].ToString();
            string debitQuery = p.getQueryFromAccountStructure(debitAccounts, user);
            string creditQuery = p.getQueryFromAccountStructure(creditAccounts, user);
            string salariesQuery = string.Empty;

            for (int i = 0; i < salaries.Count; i++)
            {
                string q = string.Format("  INSERT INTO tblGenSalaryStatement(staffID, salaryMonth, salaryYear, salaryPayable, salaryPaid) VALUES ({0},{1},{2},{3},{4})", salaries[i].staffID, salaryMonth, salaryYear, salaries[i].salaryPayable, salaries[i].salaryPaid);
                salariesQuery += q;
            }

            string grandQuery = debitQuery + creditQuery + salariesQuery;

            p.ExecuteTransactionQuery(grandQuery);
        }

        private void prepareDebitAccounts(List<Salary> salaries, int siteSalAccount, int genSalAccount)
        {
            debitAccounts.Clear();

            for (int i = 0; i < salaries.Count; i++)
            {
                Decimal totalSalaryPaid = 0;
                int staffID = salaries[i].staffID;
                Decimal staffSalary = salaries[i].salaryPaid;

                string query = string.Format(@"SELECT tblGenStaffAllocations.staffID, tblGenStaffAllocationPlaces.allocationPlaceName, 
                                                        tblGenStaffAllocationPlaces.allocationPlaceType, tblGenStaffAllocationPlaces.siteID, 
                                                        tblGenStaffAllocations.allocatedTime FROM tblGenStaffAllocationPlaces INNER JOIN tblGenStaffAllocations ON tblGenStaffAllocationPlaces.id = tblGenStaffAllocations.locationID WHERE tblGenStaffAllocations.staffID = {0}", staffID);
                DataTable dt = p.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow dr = dt.Rows[j];
                        Decimal ratio = Decimal.Parse(dr[4].ToString()) ;
                        Decimal salary =(Decimal) (salaries[i].salaryPaid * ratio) / 100;
                        string description = "Salaries paid for month: " + salaryMonth.ToString() + " year: " + salaryYear.ToString() + " To: " + salaries[i].staffName;

                        if (dr[2].ToString().Equals("Sites"))
                        {
                            AccountStructure a = new AccountStructure();
                            a.accountID = siteSalAccount;
                            a.date = DateTime.Now;
                            a.siteID = int.Parse(dr[3].ToString());
                            a.chequeNumber = string.Empty;
                            a.quantity = 0f;
                            a.UoM = string.Empty;
                            a.rate = 0f;
                            a.debit = salary;
                            a.credit = 0;
                            a.description = description;
                            a.taxRate = 0f;
                            a.supplierID = 0;
                            a.bankName = string.Empty;
                            a.chequeDate = new DateTime(2000, 1, 1);

                            if ((j + 1) == dt.Rows.Count)//means if it is the last row
                            {
                                a.debit = salaries[i].salaryPaid - totalSalaryPaid;
                            }
                            else
                            {
                                totalSalaryPaid += salary;
                            }

                            debitAccounts.Add(a);                            
                        }
                        else
                        {
                            AccountStructure a = new AccountStructure();
                            a.accountID = genSalAccount;
                            a.date = DateTime.Now;
                            a.siteID = 0;
                            a.chequeNumber = string.Empty;
                            a.quantity = 0f;
                            a.UoM = string.Empty;
                            a.rate = 0f;
                            a.debit = salary;
                            a.credit = 0;
                            a.description = description;
                            a.taxRate = 0f;
                            a.supplierID = 0;
                            a.bankName = string.Empty;
                            a.chequeDate = new DateTime(2000, 1, 1);

                            if ((j + 1) == dt.Rows.Count)//means if it is the last row
                            {
                                a.debit = salaries[i].salaryPaid - totalSalaryPaid;
                            }
                            else
                            {
                                totalSalaryPaid += salary;
                            }

                            debitAccounts.Add(a);
                        }

                    }
                    
                }
            }
        }

        private Decimal getTotalSalary()
        {
            salaries.Clear();
            Decimal total = 0;

            for (int i = 0; i < gridStaff.Rows.Count ; i++)
            {
                Label lbl = (Label)gridStaff.Rows[i].FindControl("lblTotalSalary");
                TextBox txtBx = (TextBox)gridStaff.Rows[i].FindControl("txtSalary");
                Decimal amt = 0;
                Decimal totalSalary = 0;
                if (Decimal.TryParse(txtBx.Text, out amt))
	            {
                    if (Decimal.TryParse(lbl.Text, out totalSalary))
                    {
                        if (amt <= totalSalary)
                        {
                            total += amt;
                            Label lblName = (Label)gridStaff.Rows[i].FindControl("lblStaffName");
                            Label lblID = (Label)gridStaff.Rows[i].FindControl("lblStaffID");

                            Salary sal = new Salary();
                            sal.salaryMonth = salaryMonth;
                            sal.salaryYear = salaryYear;
                            sal.salaryPayable = totalSalary;
                            sal.salaryPaid = amt;
                            sal.staffID = int.Parse(lblID.Text);
                            sal.staffName = lblName.Text;

                            salaries.Add(sal);
                        }
                        else
                        {
                            throw new Exception("You can not pay more than the salary.");
                        }
                    }
	            }
            }

            return total;
        }

        private Decimal getTotalPayments()
        {
            creditAccounts.Clear();
            Decimal total = 0;
            string description = "Salaries paid for month: " + salaryMonth.ToString() + " year: " + salaryYear.ToString();

            for (int i = 0; i < gridPriSitesInfo.Rows.Count; i++)
            {
                Label lbl = (Label)gridPriSitesInfo.Rows[i].FindControl("lblStatus");
                TextBox txtBx = (TextBox)gridPriSitesInfo.Rows[i].FindControl("txtAmount");
                Decimal amt = 0;
                Decimal totalSalary = 0;
                if (Decimal.TryParse(txtBx.Text, out amt))
                {
                    if (Decimal.TryParse(lbl.Text, out totalSalary))
                    {
                        if (amt <= totalSalary)
                        {
                            total += amt;
                            Label lbl2 = (Label)gridPriSitesInfo.Rows[i].FindControl("lblID");
                            int accountID = int.Parse(lbl2.Text);

                            AccountStructure a = new AccountStructure();
                            a.accountID = accountID;
                            a.date = DateTime.Now;
                            a.siteID = 0;
                            a.chequeNumber = string.Empty;
                            a.quantity = 0f;
                            a.UoM = string.Empty;
                            a.rate = 0f;
                            a.debit = 0;
                            a.credit = amt;
                            a.description = description;
                            a.taxRate = 0f;
                            a.supplierID = 0;
                            a.bankName = string.Empty;
                            a.chequeDate = new DateTime(2000, 1, 1);

                            creditAccounts.Add(a);
                        }
                        else
                        {
                            throw new Exception("You can not pay more than the amount available.");
                        }
                    }
                }
            }

            return total;
        }
    }
}