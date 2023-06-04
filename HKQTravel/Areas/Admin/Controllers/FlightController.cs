using HKQTravel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class FlightController : BaseController
    {
        // GET: Admin/Flight
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            ViewBag.class_id = new SelectList(data.classes.OrderBy(c => c.class_name), "class_id", "class_name");
            ViewBag.flight_brand_id = new SelectList(data.flight_brands.OrderBy(c => c.flight_brand_name), "flight_brand_id", "flight_brand_name");
            return View(data.flights.ToList().ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.class_id = new SelectList(data.classes.OrderBy(c => c.class_name), "class_id", "class_name");
            ViewBag.flight_brand_id = new SelectList(data.flight_brands.OrderBy(c => c.flight_brand_name), "flight_brand_id", "flight_brand_name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(flight Flight, FormCollection f)
        {
            var timego = f["timego"];
            var timereturn = f["timereturn"];
            if (ModelState.IsValid)
            {
                Flight.start_time = DateTime.Parse(timego);
                Flight.return_time = DateTime.Parse(timereturn);
                Flight.status = 1;
                data.flights.InsertOnSubmit(Flight);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Flight);
        }




        public ActionResult Details(int id)
        {
            flight Flight = data.flights.FirstOrDefault(p => p.flight_id == id);
            ViewBag.class_id = new SelectList(data.classes.OrderBy(c => c.class_name), "class_id", "class_name");
            ViewBag.flight_brand_id = new SelectList(data.flight_brands.OrderBy(c => c.flight_brand_name), "flight_brand_id", "flight_brand_name");
            ViewBag.flight_id = Flight.flight_id;
            if (Flight == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Flight);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {

            flight Flight = data.flights.FirstOrDefault(p => p.flight_id == id);
            if (Flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.class_id = new SelectList(data.classes.OrderBy(c => c.class_name), "class_id", "class_name");
            ViewBag.flight_brand_id = new SelectList(data.flight_brands.OrderBy(c => c.flight_brand_name), "flight_brand_id", "flight_brand_name");
            return View(Flight);
        }

        [HttpPost]
        public ActionResult Edit(flight Flight, FormCollection t)
        {
            ViewBag.class_id = new SelectList(data.classes.OrderBy(c => c.class_name), "class_id", "class_name");
            ViewBag.flight_brand_id = new SelectList(data.flight_brands.OrderBy(c => c.flight_brand_name), "flight_brand_id", "flight_brand_name");
            if (ModelState.IsValid)
            {

                var Flights = data.flights.SingleOrDefault(c => c.flight_id == Flight.flight_id);
                var timego = t["timego"];
                var timereturn = t["timereturn"];
                if (Flights == null)
                {
                    return HttpNotFound();
                }
                Flights.flight_name = Flight.flight_name;
                Flights.price = Flight.price;
                Flights.start_time = DateTime.Parse(timego);
                Flights.return_time = DateTime.Parse(timereturn);
                Flights.departure_location = Flight.destination_location;
                Flights.destination_location = Flight.departure_location;
                Flights.number_of_passenger = Flight.number_of_passenger;
                Flights.class_id = Flight.class_id;
                Flights.flight_brand_id = Flight.flight_brand_id;
                Flights.status = 2;
                data.SubmitChanges();
                return RedirectToAction("Index", "Flight");


            }
            return View(Flight);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            flight Flight = data.flights.SingleOrDefault(p => p.flight_id == id);
            ViewBag.flight_id = Flight.flight_id;
            if (Flight == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(Flight);
        }

        //chấp nhận xóa
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            flight Flight = data.flights.SingleOrDefault(p => p.flight_id == id);
            ViewBag.flight_id = Flight.flight_id;
            if (Flight == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.flights.DeleteOnSubmit(Flight);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            flight Flight = data.flights.SingleOrDefault(p => p.flight_id == id);
            Flight.status = 0;
            UpdateModel(Flight);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            flight Flight = data.flights.SingleOrDefault(p => p.flight_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            Flight.status = (Flight.status == 2) ? 1 : 2;
            UpdateModel(Flight);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            flight Flight = data.flights.SingleOrDefault(p => p.flight_id == id);
            Flight.status = 2;
            UpdateModel(Flight);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //hiện danh sách trong thùng rác
        public ActionResult Trashlist(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(data.flights.Where(m => m.status == 0).ToList().OrderBy(n => n.flight_id).ToPagedList(pagenumber, pagesize));
        }
    }
}