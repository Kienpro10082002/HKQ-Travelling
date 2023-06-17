using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKQTravel.Models
{
    public class HotelCartModel
    {
        HKQTravelDataContext data = new HKQTravelDataContext();
        public long iHotelId { set; get; }
        public string sHotel_name { set; get; }
        public string sLocation { set; get; }
        public int? iNumRoom { set; get; }
        public decimal? dprice { set; get; }
        public string sImage { set; get; }
        public int? iRanking { set; get; }
        public int iQuantity { set; get; }
        public DateTime? CreateDate { set; get; }

        public decimal? dTotalpriceHotel
        {
            get { return dprice * iQuantity; }
        }

        public HotelCartModel(long id_Hotel) {
            iHotelId = id_Hotel;
            hotel hotels = data.hotels.Single(n => n.hotel_id == id_Hotel);
            sHotel_name = hotels.hotel_name; 
            sImage = hotels.image;
            iNumRoom = hotels.number_room;
            iRanking = hotels.ranking;
            sLocation = hotels.location;
            iQuantity = 1;
            dprice = hotels.price;
            CreateDate = DateTime.Now;

        }
    }
}