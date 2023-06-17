using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKQTravel.Models
{
    public class HistoryHotel
    {
        private long billId;
        private String createDate, fullName, hotelName, quantity;
        private Decimal price, totalPrice;
        public long BillId { get; set; }
        public String CreateDate { get; set; }
        public String FullName { get; set; }
        public String HotelName { get; set; }
        public Decimal Price { get; set; }
        public String Quantity { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}