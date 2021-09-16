using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IttefaqConstructionServices.Logic
{
    public class Beneficiary
    {
        public string benefName { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string city { get; set; }
        public string cnic { get; set; }
        public string education { get; set; }
        public DateTime dob { get; set; }
        public Decimal income { get; set; }
        public string incomeSource { get; set; }
        public string primeDisability { get; set; }
        public string specialIssue { get; set; }
        public int totalFamily { get; set; }
        public bool hasHouse { get; set; }
        public string remarks { get; set; }
        public List<Family> familyMembers { get; set; }
    }
}