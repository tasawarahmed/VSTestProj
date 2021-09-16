using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices
{
    public partial class GoogleCharts : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Connection c = new Connection();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] getPersonalLedgers()
        {
            decimal advances = 0;
            decimal liabilities = 0;

            List<Account> dataList = new List<Account>();
            string query = "select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = 12";
            string connString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                Decimal amount = 0;
                Decimal.TryParse(dr[2].ToString(), out amount);

                if (amount < 0)
                {
                    advances = advances + (amount * -1);
                }
                else
                {
                    liabilities = liabilities + amount;
                }
            }

            dr.Close();
            connection.Close();

            Account adv = new Account();
            adv.Name = "Advances";
            adv.Debit = advances;
            dataList.Add(adv);

            Account liab = new Account();
            liab.Name = "Liabilities";
            liab.Debit = liabilities;
            dataList.Add(liab);

            //DataTable dt = p.getDataTable(query);

            var chartData = new object[dataList.Count + 1];

            chartData[0] = new object[] { 
                "Account Name",
                "Balance"
            };

            int j = 0;
            foreach (var i in dataList)
            {
                j++;
                chartData[j] = new object[] { i.Name, i.Debit };
            }
            return chartData;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] getSubContractorsLahore()
        {
            decimal advances = 0;
            decimal liabilities = 0;

            List<Account> dataList = new List<Account>();
            string query = "select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = 10";
            string connString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                Decimal amount = 0;
                Decimal.TryParse(dr[2].ToString(), out amount);

                if (amount < 0)
                {
                    advances = advances + (amount * -1);
                }
                else
                {
                    liabilities = liabilities + amount;
                }
            }

            dr.Close();
            connection.Close();

            Account adv = new Account();
            adv.Name = "Advances";
            adv.Debit = advances;
            dataList.Add(adv);

            Account liab = new Account();
            liab.Name = "Liabilities";
            liab.Debit = liabilities;
            dataList.Add(liab);

            //DataTable dt = p.getDataTable(query);

            var chartData = new object[dataList.Count + 1];

            chartData[0] = new object[] { 
                "Sub Contractor Name",
                "Balance"
            };

            int j = 0;
            foreach (var i in dataList)
            {
                j++;
                chartData[j] = new object[] { i.Name, i.Debit };
            }
            return chartData;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] getSuppliersKarachi()
        {
            decimal advances = 0;
            decimal liabilities = 0;

            List<Account> dataList = new List<Account>();
            string query = "select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = 21";
            string connString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                Decimal amount = 0;
                Decimal.TryParse(dr[2].ToString(), out amount);

                if (amount < 0)
                {
                    advances = advances + (amount * -1);
                }
                else
                {
                    liabilities = liabilities + amount;
                }
            }

            dr.Close();
            connection.Close();

            Account adv = new Account();
            adv.Name = "Advances";
            adv.Debit = advances;
            dataList.Add(adv);

            Account liab = new Account();
            liab.Name = "Liabilities";
            liab.Debit = liabilities;
            dataList.Add(liab);

            //DataTable dt = p.getDataTable(query);

            var chartData = new object[dataList.Count + 1];

            chartData[0] = new object[] { 
                "Supplier Name",
                "Balance"
            };

            int j = 0;
            foreach (var i in dataList)
            {
                j++;
                chartData[j] = new object[] { i.Name, i.Debit };
            }
            return chartData;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] getSuppliersLahore() {
            decimal advances = 0;
            decimal liabilities = 0;
            
            List<Account> dataList = new List<Account>();
            string query = "select id, Name, Status from viewGenAccountsGroupByPrimary Where ParentID = 20";
            string connString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                Decimal amount = 0;
                Decimal.TryParse(dr[2].ToString(), out amount);

                if (amount < 0)
                {
                    advances = advances + (amount * -1);
                }
                else
                {
                    liabilities = liabilities + amount;
                }
            }

            dr.Close();
            connection.Close();

            Account adv = new Account();
            adv.Name = "Advances";
            adv.Debit = advances;
            dataList.Add(adv);

            Account liab = new Account();
            liab.Name = "Liabilities";
            liab.Debit = liabilities;
            dataList.Add(liab);

            //DataTable dt = p.getDataTable(query);

            var chartData = new object[dataList.Count + 1];

            chartData[0] = new object[] { 
                "Supplier Name",
                "Balance"
            };

            int j = 0;
            foreach (var i in dataList)
            {
                j++;
                chartData[j] = new object[] { i.Name, i.Debit };
            }
            return chartData;
        }
    }
}