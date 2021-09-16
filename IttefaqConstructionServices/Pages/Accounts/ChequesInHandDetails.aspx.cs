using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Accounts
{
    public partial class ChequesInHandDetails : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "accounts, viewer", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadChequesGrid();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }                
        }

        private void loadChequesGrid()
        {
            try
            {
                int chequesInHandID = p.getIntValue("SELECT id FROM tblGenAccounts WHERE (accountName = 'Cheques in Hand')");

                string query = string.Format("SELECT tblGenAccountsTransactions.chequeNumber AS [Cheque Number], tblGenAccountsTransactions.debitAmount AS Amount, tblGenAccountsTransactions.description AS Description, tblGenAccountsTransactions.taxRate AS [Tax Rate], tblGenAccountsTransactions.isInHand AS [In Hand Status], tblGenAccounts.accountName AS [Received From], tblGenAccountsTransactions.bankName AS [Bank Name], tblGenAccountsTransactions.chequeDate AS [Cheque Date], tblGenAccounts_1.accountName AS [Issued To] FROM tblGenAccounts INNER JOIN tblGenAccountsTransactions ON tblGenAccounts.id = tblGenAccountsTransactions.supplierID INNER JOIN tblGenAccounts AS tblGenAccounts_1 ON tblGenAccountsTransactions.chequeIssuedTo = tblGenAccounts_1.id WHERE tblGenAccountsTransactions.accountID = {0}", chequesInHandID);
                DataTable dt = p.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    gridPriSitesInfo.DataSource = dt;
                    gridPriSitesInfo.DataBind();

                    gridPriSitesInfo.UseAccessibleHeader = true;
                    gridPriSitesInfo.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}