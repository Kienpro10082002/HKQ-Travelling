using HKQTravel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class Bill_TourController : BaseController
    {
        // GET: Admin/Bill_Tour
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.bill_tours.ToList().ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Details(int id)
        {
            bill_tour bh = data.bill_tours.FirstOrDefault(p => p.bill_tour_id == id);
            ViewBag.bill_tour_id = bh.bill_tour_id;
            if (bh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bh);
        }
    }
}