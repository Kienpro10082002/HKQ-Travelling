using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HKQTravel.Models
{
    public class FlightCartModel
    {
        HKQTravelDataContext data = new HKQTravelDataContext();

        public long iFlightId { set; get; }
        public string sFlight_Name { set; get; }
        public string sDeparture_Location { set; get; }
        public string sDestination_Location { set; get; }
        public decimal? dprice { set; get; }
        public DateTime? start_Time { set; get; }
        public DateTime? return_Time { set; get; }
        public int iQuantity { set; get; }
        public DateTime? CreateDate { set; get; }

        public decimal? dTotalpriceFlight
        {
            get { return dprice * iQuantity; }
        }

        public FlightCartModel(long id_Flight)
        {
            iFlightId = id_Flight;
            flight flights = data.flights.Single(n => n.flight_id == id_Flight);
            sFlight_Name = flights.flight_brand.flight_brand_name;
            sDeparture_Location = flights.departure_location;
            sDestination_Location = flights.destination_location;
            dprice = flights.price;
            start_Time = flights.start_time;
            return_Time = flights.return_time;
            iQuantity = 1;
            CreateDate = DateTime.Now;
        }
    }
}