using HKQTravel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class Departure_PoinController : BaseController
    {
        // GET: Admin/Departure_Poin
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.departure_points.Where(p => p.status != 0).ToList().ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(departure_point dp)
        {
            if (ModelState.IsValid)
            {
                    dp.status = 1;
                    data.departure_points.InsertOnSubmit(dp);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
             }
            return View(dp);
        }




        public ActionResult Details(int id)
        {
            departure_point dp = data.departure_points.FirstOrDefault(p => p.departure_id == id);
            ViewBag.departure_id = dp.departure_id;
            if (dp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dp);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            departure_point dp = data.departure_points.FirstOrDefault(p => p.departure_id == id);
            if (dp == null)
            {
                return HttpNotFound();
            }
            return View(dp);
        }

        [HttpPost]
        public ActionResult Edit(departure_point model)
        {
            if (ModelState.IsValid)
            {
                var dp = data.departure_points.FirstOrDefault(c => c.departure_id == model.departure_id);
                if (dp == null)
                {
                    return HttpNotFound();
                }
                dp.departure_name = model.departure_name;
                dp.status = 2;
                data.SubmitChanges();
                return RedirectToAction("Index", "Departure_Poin");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            departure_point dp = data.departure_points.FirstOrDefault(p => p.departure_id == id);
            ViewBag.departure_id = dp.departure_id;
            if (dp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(dp);
        }

        //chấp nhận xóa
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            departure_point dp = data.departure_points.FirstOrDefault(p => p.departure_id == id);
            ViewBag.departure_id = dp.departure_id;
            if (dp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.departure_points.DeleteOnSubmit(dp);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            departure_point dp = data.departure_points.FirstOrDefault(p => p.departure_id == id);
            dp.status = 0;
            UpdateModel(dp);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            departure_point dp = data.departure_points.FirstOrDefault(p => p.departure_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            dp.status = (dp.status == 2) ? 1 : 2;
            UpdateModel(dp);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            departure_point dp = data.departure_points.FirstOrDefault(p => p.departure_id == id);
            dp.status = 2;
            UpdateModel(dp);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //hiện danh sách trong thùng rác
        public ActionResult Trashlist(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(data.departure_points.Where(m => m.status == 0).ToList().OrderBy(n => n.departure_id).ToPagedList(pagenumber, pagesize));
        }
    }
}