using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Controllers
{
    public class SearchingController : Controller
    {
        HKQTravelDataContext data = new HKQTravelDataContext();
        // GET: Searching
        [HttpPost]
        public ActionResult Searching(FormCollection collection)
        {
            var findValue = collection["valueOfSearching"].ToString();

            //Tìm tên địa điểm đến
            var tour = data.tours.SingleOrDefault(t => t.destination_point.destination_name.Contains(findValue));
            //Tìm tên tên khách sạn
            var hotel = data.hotels.SingleOrDefault(h => h.hotel_name.Contains(findValue));
            //Tìm tên hãng máy bay
            var flight = data.flight_brands.SingleOrDefault(f => f.flight_brand_name.Contains(findValue));

            if(tour != null)
            {
                var listTour = data.tours.OrderByDescending(a => a.destination_point.destination_name.Contains(findValue)).ToList();
                return View("TourSearchingView", listTour);
            }
            else if(hotel != null)
            {
                var listHotel = data.hotels.OrderByDescending(a => a.hotel_name.Contains(findValue)).ToList();
                return View("HotelSearchingView", listHotel);
            }
            else if(flight != null)
            {
                var listFlight= data.flights.OrderByDescending(a => a.destination_location.Contains(findValue)).ToList();
                return View("FlightSearchingView", listFlight);
            }
            else
            {
               return RedirectToAction("Index", "Home");
            }
        }
    }
}