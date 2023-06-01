using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKQTravel.Models
{
    public class TourCartModel
    {
        HKQTravelDataContext data = new HKQTravelDataContext();

        public long iIdTour { get; set; }

        public string sTourName { get; set; }

        public int iQuantity { get; set; }

        public decimal? dprice { get; set; }
        
        public decimal? dTotalpriceTour
        {
            get { return dprice * iQuantity; }
        }

        public DateTime CreatedDate { get; set; } 

        public TourCartModel(long TourId) {
            iIdTour = TourId;
            tour tours = data.tours.Single(n=>n.tour_id == iIdTour);
            sTourName = tours.tour_name;
            iQuantity = 1;
            dprice = tours.price;
            CreatedDate = DateTime.Now; 
           
        }
    }
}