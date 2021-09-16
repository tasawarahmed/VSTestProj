using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IttefaqConstructionServices.Logic
{
    public class Connection
    {
        public string connectionString()
        {
            string connString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ConnectionString;
            return connString;
        }

        public static string getConnectionString()
        {
            string connString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ConnectionString;
            return connString;
        }
    }
}