using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace HKQTravel.Areas.Admin.Controllers
{
    public class Bill_HotelController : BaseController
    {
        // GET: Admin/Bill_Hotel
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.bill_hotels.ToList().ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Details(int id)
        {
            bill_hotel bh = data.bill_hotels.FirstOrDefault(p => p.bill_hotel_id == id);
            ViewBag.bill_hotel_id = bh.bill_hotel_id;
            if (bh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bh);
        }
    }
}