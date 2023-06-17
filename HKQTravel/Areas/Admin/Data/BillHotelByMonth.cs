using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKQTravel.Areas.Admin.Data
{
    public class BillHotelByMonth
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}