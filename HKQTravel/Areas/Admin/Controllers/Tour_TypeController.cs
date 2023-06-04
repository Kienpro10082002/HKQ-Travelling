using HKQTravel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class Tour_TypeController : BaseController
    {
        // GET: Admin/Tour_Type
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.tour_types.Where(p => p.status != 0).ToList().ToPagedList(pagenumber, pagesize));
        }
       

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(tour_type tt)
        {
            if (ModelState.IsValid)
            {
                tt.status = 1;
                data.tour_types.InsertOnSubmit(tt);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(tt);
        }




        public ActionResult Details(int id)
        {
            tour_type tt = data.tour_types.FirstOrDefault(p => p.tour_type_id == id);
            ViewBag.tour_type_id = tt.tour_type_id;
            if (tt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tt);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            tour_type tt = data.tour_types.FirstOrDefault(p => p.tour_type_id == id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }

        [HttpPost]
        public ActionResult Edit(tour_type model, FormCollection tt)
        {
            if (ModelState.IsValid)
            {
                var tout = data.tour_types.FirstOrDefault(c => c.tour_type_id == model.tour_type_id);
                if (tout == null)
                {
                    return HttpNotFound();
                }
                tout.tour_type_name = model.tour_type_name;
                tout.status = 2;
                data.SubmitChanges();
                return RedirectToAction("Index", "Tour_Type");
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            tour_type tt = data.tour_types.FirstOrDefault(p => p.tour_type_id == id);
            ViewBag.tour_type_id = tt.tour_type_id;
            if (tt == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(tt);
        }

        //chấp nhận xóa
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            tour_type tt = data.tour_types.FirstOrDefault(p => p.tour_type_id == id);
            ViewBag.tour_type_id = tt.tour_type_id;
            if (tt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.tour_types.DeleteOnSubmit(tt);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            tour_type tt = data.tour_types.FirstOrDefault(p => p.tour_type_id == id);
            tt.status = 0;
            UpdateModel(tt);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            tour_type tt = data.tour_types.FirstOrDefault(p => p.tour_type_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            tt.status = (tt.status == 2) ? 1 : 2;
            UpdateModel(tt);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            tour_type tt = data.tour_types.FirstOrDefault(p => p.tour_type_id == id);
            tt.status = 2;
            UpdateModel(tt);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //hiện danh sách trong thùng rác
        public ActionResult Trashlist(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(data.tour_types.Where(m => m.status == 0).ToList().OrderBy(n => n.tour_type_id).ToPagedList(pagenumber, pagesize));
        }
    }
}