using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace HKQTravel.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin/Admin
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.admin_accounts.Where(p => p.status != 0).ToList().ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(admin_account dc)
        {
          

            if (ModelState.IsValid)
            {

             
                dc.create_date = DateTime.Now;
                dc.status = 1;
                data.admin_accounts.InsertOnSubmit(dc);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(dc);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            admin_account ad = data.admin_accounts.FirstOrDefault(p => p.admin_id == id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        [HttpPost]
        public ActionResult Edit(admin_account model)
        {
           
            if (ModelState.IsValid)
            {
                var ad = data.admin_accounts.SingleOrDefault(c => c.admin_id == model.admin_id);
                if (ad == null)
                {
                    return HttpNotFound();
                }
                ad.admin_user = model.admin_user;
                ad.admin_password = model.admin_password;
                ad.admin_fulllname = model.admin_fulllname;
                ad.email = model.email;
                ad.update_date = DateTime.Now;
                data.SubmitChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }
        public ActionResult Details(int id)
        {
            admin_account ad = data.admin_accounts.FirstOrDefault(p => p.admin_id == id);
            ViewBag.admin_id = ad.admin_id;
            if (ad == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ad);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            admin_account ad = data.admin_accounts.SingleOrDefault(p => p.admin_id == id);
            ViewBag.admin_id = ad.admin_id;
            if (ad == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(ad);
        }

        //chấp nhận xóa
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            admin_account ad = data.admin_accounts.SingleOrDefault(p => p.admin_id == id);
            ViewBag.discount_id = ad.admin_id;
            if (ad == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.admin_accounts.DeleteOnSubmit(ad);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            admin_account ad = data.admin_accounts.SingleOrDefault(p => p.admin_id == id);
            ad.status = 0;
            UpdateModel(ad);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            admin_account ad = data.admin_accounts.SingleOrDefault(p => p.admin_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            ad.status = (ad.status == 2) ? 1 : 2;
            UpdateModel(ad);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            admin_account ad = data.admin_accounts.SingleOrDefault(p => p.admin_id == id);
            ad.status = 2;
            UpdateModel(ad);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //hiện danh sách trong thùng rác
        public ActionResult Trashlist(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(data.admin_accounts.Where(m => m.status == 0).ToList().OrderBy(n => n.admin_id).ToPagedList(pagenumber, pagesize));
        }
    }
}