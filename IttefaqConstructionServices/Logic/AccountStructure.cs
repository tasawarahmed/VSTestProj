using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IttefaqConstructionServices.Logic
{
    public class AccountStructure
    {
        public int accountID { get; set; }
        public int siteID { get; set; }
        public int voucherID { get; set; }
        public DateTime date { get; set; }
        public string chequeNumber { get; set; }
        public Single quantity { get; set; }
        public string UoM { get; set; }
        public Single rate { get; set; }
        public Decimal debit { get; set; }
        public Decimal credit { get; set; }
        public string description { get; set; }
        public Single  taxRate { get; set; }
        public bool isInHand { get; set; }
        public int supplierID { get; set; }
        public string bankName { get; set; }
        public DateTime chequeDate { get; set; }
        public int chequeIssuedTo { get; set; }
    }
}