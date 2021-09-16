using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IttefaqConstructionServices.Logic
{
    public class Salary
    {
        public int staffID { get; set; }
        public string staffName { get; set; }
        public int salaryMonth { get; set; }
        public int salaryYear { get; set; }
        public Decimal salaryPayable { get; set; }
        public Decimal salaryPaid { get; set; }
    }
}