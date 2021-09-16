using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace IttefaqConstructionServices.App_Code
{
    public class Connection
    {
        public string connectionString()
        {
            string connString = ConfigurationManager.ConnectionStrings["SchoolConnectionString"].ConnectionString;
            return connString;
        }

        public static string getConnectionString()
        {
            string connString = ConfigurationManager.ConnectionStrings["SchoolConnectionString"].ConnectionString;
            return connString;
        }
    }
}