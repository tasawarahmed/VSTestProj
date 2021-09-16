using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IttefaqConstructionServices.Logic
{
    public class ExcelData
    {
        public DateTime Date { get; set; }
        public int SiteID { get; set; }
        public int cashBookAccount { get; set; }
        public int otherAccount { get; set; }
        public String Description { get; set; }
        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }
    }
}