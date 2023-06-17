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
    public class TourController : BaseController
    {
        // GET: Admin/Tour
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            ViewBag.destination_id = new SelectList(data.destination_points.OrderBy(c => c.destination_name), "destination_id", "destination_name");
            ViewBag.departure_id = new SelectList(data.departure_points.OrderBy(c => c.departure_name), "departure_id", "departure_name");
            ViewBag.discount_id = new SelectList(data.discounts.OrderBy(c => c.discount_name), "discount_id", "discount_name");
            ViewBag.tour_type_id = new SelectList(data.tour_types.OrderBy(c => c.tour_type_name), "tour_type_id", "tour_type_name");
            return View(data.tours.Where(p => p.status != 0).ToList().ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.destination_id = new SelectList(data.destination_points.OrderBy(c => c.destination_name), "destination_id", "destination_name");
            ViewBag.departure_id = new SelectList(data.departure_points.OrderBy(c => c.departure_name), "departure_id", "departure_name");
            ViewBag.discount_id = new SelectList(data.discounts.OrderBy(c => c.discount_name), "discount_id", "discount_name");
            ViewBag.tour_type_id = new SelectList(data.tour_types.OrderBy(c => c.tour_type_name), "tour_type_id", "tour_type_name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(tour Tour, FormCollection t)
        {
            var timego = t["timego"];
            var timereturn = t["timereturn"];
            if (ModelState.IsValid)
            {
                Tour.departure_time = DateTime.Parse(timego);
                Tour.return_time = DateTime.Parse(timereturn);
                Tour.status = 1;
                Tour.create_date = DateTime.Now;
                data.tours.InsertOnSubmit(Tour);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Tour);
        }




        public ActionResult Details(int id)
        {
            tour Tour = data.tours.FirstOrDefault(p => p.tour_id == id);
            ViewBag.destination_id = new SelectList(data.destination_points.OrderBy(c => c.destination_name), "destination_id", "destination_name");
            ViewBag.departure_id = new SelectList(data.departure_points.OrderBy(c => c.departure_name), "departure_id", "departure_name");
            ViewBag.discount_id = new SelectList(data.discounts.OrderBy(c => c.discount_name), "discount_id", "discount_name");
            ViewBag.tour_type_id = new SelectList(data.tour_types.OrderBy(c => c.tour_type_name), "tour_type_id", "tour_type_name");
            ViewBag.tour_id = Tour.tour_id;
            if (Tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Tour);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
          
            tour Tour = data.tours.FirstOrDefault(p => p.tour_id == id);
            if (Tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.destination_id = new SelectList(data.destination_points.OrderBy(c => c.destination_name), "destination_id", "destination_name",Tour.destination_id);
            ViewBag.departure_id = new SelectList(data.departure_points.OrderBy(c => c.departure_name), "departure_id", "departure_name", Tour.departure_id);
            ViewBag.discount_id = new SelectList(data.discounts.OrderBy(c => c.discount_name), "discount_id", "discount_name", Tour.discount_id);
            ViewBag.tour_type_id = new SelectList(data.tour_types.OrderBy(c => c.tour_type_name), "tour_type_id", "tour_type_name", Tour.tour_type_id);
            return View(Tour);
        }

        [HttpPost]
        public ActionResult Edit(tour model,FormCollection t)
        {
            ViewBag.destination_id = new SelectList(data.destination_points.OrderBy(c => c.destination_name), "destination_id", "destination_name");
            ViewBag.departure_id = new SelectList(data.departure_points.OrderBy(c => c.departure_name), "departure_id", "departure_name");
            ViewBag.discount_id = new SelectList(data.discounts.OrderBy(c => c.discount_name), "discount_id", "discount_name");
            ViewBag.tour_type_id = new SelectList(data.tour_types.OrderBy(c => c.tour_type_name), "tour_type_id", "tour_type_name");
            if (ModelState.IsValid)
            {
  
                    var Tour = data.tours.SingleOrDefault(c => c.tour_id == model.tour_id);
                    var timego = t["timego"];
                    var timereturn = t["timereturn"];
                if (Tour == null)
                    {
                        return HttpNotFound();
                    }
                    Tour.tour_name = model.tour_name;
                    Tour.price = model.price;
                    Tour.departure_time = DateTime.Parse(timego);
                    Tour.return_time = DateTime.Parse(timereturn);
                    Tour.destination_id = model.destination_id;
                    Tour.departure_id = model.departure_id;
                    Tour.discount_id = model.discount_id;
                    Tour.tour_type_id = model.tour_type_id;
                    Tour.update_create = DateTime.Now;
                    Tour.status = 2;
                    data.SubmitChanges();
                    return RedirectToAction("Index", "Tour");
                 
                   
             }
            return View(model);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            tour Tour = data.tours.SingleOrDefault(p => p.tour_id == id);
            ViewBag.tour_id = Tour.tour_id;
            if (Tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(Tour);
        }

        //chấp nhận xóa
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            tour Tour = data.tours.SingleOrDefault(p => p.tour_id == id);
            ViewBag.tour_id = Tour.tour_id;
            if (Tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.tours.DeleteOnSubmit(Tour);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            tour Tour = data.tours.SingleOrDefault(p => p.tour_id == id);
            Tour.status = 0;
            UpdateModel(Tour);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            tour Tour = data.tours.SingleOrDefault(p => p.tour_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            Tour.status = (Tour.status == 2) ? 1 : 2;
            UpdateModel(Tour);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            tour Tour = data.tours.SingleOrDefault(p => p.tour_id == id);
            Tour.status = 2;
            UpdateModel(Tour);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //hiện danh sách trong thùng rác
        public ActionResult Trashlist(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(data.tours.Where(m => m.status == 0).ToList().OrderBy(n => n.tour_id).ToPagedList(pagenumber, pagesize));
        }
    }
}