using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKQTravel.Areas.Admin.Data
{
    public class BillTourByMonth
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}