using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IttefaqConstructionServices.Logic
{
    public class MaterialsConsumption
    {
        public int accountID { get; set; }
        public string accountName { get; set; }
        public Single quantity { get; set; }
        public string UoM { get; set; }
        public Single amount { get; set; }
        public Single  debit { get; set; }
        public Single credit { get; set; }
        public Single averageRate { get; set; }
        public DateTime date { get; set; }
    }
}