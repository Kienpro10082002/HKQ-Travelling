using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HKQTravel.Models;
using PagedList;
namespace HKQTravel.Areas.Admin.Controllers
{
    public class Bill_FlightController : BaseController
    {
        // GET: Admin/Bill_Flight
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.bill_flights.ToList().ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Details(int id)
        {
            bill_flight bf = data.bill_flights.FirstOrDefault(p => p.bill_flight_id == id);
            ViewBag.tour_type_id = bf.bill_flight_id;
            if (bf == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bf);
        }
    }
}