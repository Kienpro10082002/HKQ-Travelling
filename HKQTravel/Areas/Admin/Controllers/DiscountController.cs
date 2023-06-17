using HKQTravel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class DiscountController : BaseController
    {
        // GET: Admin/Discount
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {

            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.discounts.Where(p => p.status != 0).ToList().ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(discount dc, FormCollection dis)
        {
            string value = dis["value"];
            string timeactive = dis["timeactive"];
           
            if (ModelState.IsValid)
            {

                    dc.percentage = float.Parse(value);
                    dc.discount_month = DateTime.Parse(timeactive);
                    dc.create_date = DateTime.Now;
                    dc.status = 1;
                    data.discounts.InsertOnSubmit(dc);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
            }
            return View(dc);
        }




        public ActionResult Details(int id)
        {
            discount dc = data.discounts.FirstOrDefault(p => p.discount_id == id);
            ViewBag.discount_id = dc.discount_id;
            if (dc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dc);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            discount dc = data.discounts.FirstOrDefault(p => p.discount_id == id);
            if (dc == null)
            {
                return HttpNotFound();
            }
            return View(dc);
        }

        [HttpPost]
        public ActionResult Edit(discount model, FormCollection dc)
        {
            string value = dc["value"];
            string timeactive = dc["timeactive"];
            if (ModelState.IsValid)
            {           
                    var dis = data.discounts.SingleOrDefault(c => c.discount_id == model.discount_id);
                    if (dis == null)
                    {
                        return HttpNotFound();
                    }
                    dis.discount_name = model.discount_name;
                    dis.percentage =model.percentage;
                    dis.discount_month = DateTime.Parse(timeactive);
                    dis.update_date = DateTime.Now;
                    data.SubmitChanges();
                    return RedirectToAction("Index", "Discount");             
                }
            return View(model);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            discount dc = data.discounts.SingleOrDefault(p => p.discount_id == id);
            ViewBag.discount_id = dc.discount_id;
            if (dc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(dc);
        }

        //chấp nhận xóa
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            discount dc = data.discounts.SingleOrDefault(p => p.discount_id == id);
            ViewBag.discount_id = dc.discount_id;
            if (dc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.discounts.DeleteOnSubmit(dc);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            discount dc = data.discounts.SingleOrDefault(p => p.discount_id == id);
            dc.status = 0;
            UpdateModel(dc);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            discount dc = data.discounts.SingleOrDefault(p => p.discount_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            dc.status = (dc.status == 2) ? 1 : 2;
            UpdateModel(dc);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            discount dc = data.discounts.SingleOrDefault(p => p.discount_id == id);
            dc.status = 2;
            UpdateModel(dc);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //hiện danh sách trong thùng rác
        public ActionResult Trashlist(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(data.discounts.Where(m => m.status == 0).ToList().OrderBy(n => n.discount_id).ToPagedList(pagenumber, pagesize));
        }
    }
}