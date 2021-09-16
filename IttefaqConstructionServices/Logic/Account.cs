using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IttefaqConstructionServices.Logic
{
    public class Account
    {
        public int ID { get; set; }
        public DateTime date { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int siteID { get; set; }
        public string siteName { get; set; }
        public int Association { get; set; }
        public bool IsActive { get; set; }
        public string typeName { get; set; }
        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }
        public string description { get; set; }
        public string oprator { get; set; }
        public int accountTypeID { get; set; }
    }
}