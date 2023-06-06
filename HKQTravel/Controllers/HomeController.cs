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

        //Options in Service
        public ActionResult Service()
        {
            return View();
        }

        #region Detail

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

        #endregion

        #region hotel
        public ActionResult Hotel(int? page)
        {
            int pagenum = (page ?? 1);
            int pagesize = 5;
            return View(data.hotels.Where(p => p.status == 1).ToList().ToPagedList(pagenum, pagesize));
        }
        #endregion

        #region flight
        public ActionResult Flight(int? page)
        {
            int pagenum = (page ?? 1);
            int pagesize = 5;
            return View(data.flights.Where(p => p.status == 1).ToList().ToPagedList(pagenum, pagesize));
        }
        #endregion

        #region Default website

        //Display homepage
        public ActionResult Index(int? page)
        {
            int pagenum = (page ?? 1);
            int pagesize = 5;
            return View(data.tours.Where(p => p.status == 1).ToList().ToPagedList(pagenum, pagesize));
        }

        //Display tour and destination
        public ActionResult Travel(int? page)
        {
            int pagenum = (page ?? 1);
            int pagesize = 5;
            return View(data.tours.Where(p => p.status == 1).ToList().ToPagedList(pagenum, pagesize));
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