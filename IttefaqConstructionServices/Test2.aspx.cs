using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices
{
    public partial class Test2 : System.Web.UI.Page
    {
        public Decimal advancesFromCustomers = 2595205.09m;
        public string aFC = string.Empty; 

        public Decimal receivablesFromSites = 102450611m;
        public string rFS = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            aFC = advancesFromCustomers.ToString("c", CultureInfo.CurrentCulture);
            rFS = receivablesFromSites.ToString("c", CultureInfo.CurrentCulture);
        }
    }
}