using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;

namespace HKQTravel.Controllers
{
    public class HomeController : Controller
    {
        HKQTravelDataContext data = new HKQTravelDataContext();

        //Get the tour list from database;
        private List<tour> getTour(int count)
        {
            return data.tours.OrderByDescending(a => a.tour_id).Take(count).ToList();
        }
        private List<hotel> getHotel(int count)
        {
            return data.hotels.OrderByDescending(a => a.hotel_id).Take(count).ToList();
        }
        private List<flight> getFlight(int count)
        {
            return data.flights.OrderByDescending(a => a.flight_id).Take(count).ToList();
        }
        private List<destination_point> getDestination(int count)
        {
            return data.destination_points.OrderByDescending(a => a.destination_id).Take(count).ToList();
        }

        //Options in Service
        public ActionResult Service()
        {
            return View();
        }

        //Click to depend on the index action and the travel action.
        public ActionResult DetailTour(long id)
        {
            var details = from detail in data.tours where detail.tour_id == id select detail;
            return View(details.Single());
        }
        public ActionResult DetailHotel(long id)
        {
            var detailHotels = from detailhotel in data.hotels where detailhotel.hotel_id == id select detailhotel;
            return View(detailHotels.Single());
        }
        public ActionResult DetailFlight(long id)
        {
            var detailFlights = from detailflight in data.flights where detailflight.flight_id == id select detailflight;
            return View(detailFlights.Single());
        }

        #region hotel
        public ActionResult Hotel(int? page)
        {
            var hotel = getHotel(1000);
            int pagesize = 10;
            int pagenum = (page ?? 1);
            return View(hotel.ToPagedList(pagenum, pagesize));
        }
        #endregion
        #region flight
        public ActionResult Flight(int? page)
        {
            var flight = getFlight(1000);
            int pagesize = 10;
            int pagenum = (page ?? 1);
            return View(flight.ToPagedList(pagenum, pagesize));
        }
        #endregion

        #region Default website

        //Display homepage
        public ActionResult Index(int? page)
        {
            var tour = getTour(1000);
            var destination = getDestination(1000);
            int pagesize = 10;
            int pagenum = (page ?? 1);
            return View(tour.ToPagedList(pagenum, pagesize));
        }

        //Display tour and destination
        public ActionResult Travel(int? page)
        {
            var tour = getTour(1000);
            var destination = getDestination(1000);
            int pagesize = 10;
            int pagenum = (page ?? 1);
            return View(tour.ToPagedList(pagenum, pagesize));


        }

        //Display About
        public ActionResult About()
        {


            return View();
        }

        //Display contact
        public ActionResult Contact()
        {
            return View();
        }

        #endregion
    }
}