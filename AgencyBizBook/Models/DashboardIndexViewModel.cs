using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class DashboardIndexViewModel
    {
        public int? TotalProducts { get; set; }
        public double? TotalCredit { get; set; }
        public double? Balance { get; set; }
        public double? TotalDebit { get; set; }
    }
}