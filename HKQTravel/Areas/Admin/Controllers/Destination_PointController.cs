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
    public class Destination_PointController : BaseController
    {
        // GET: Admin/Destination_Point
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.destination_points.Where(p => p.status != 0).ToList().ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(destination_point dp, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {

                if (fileImage != null && fileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Admin/img_des/"), fileName);
                    var fileCount = 1;
                    //Kiểm tra sự tồn tại của file
                    while (System.IO.File.Exists(path))
                    {
                        fileName = Path.GetFileNameWithoutExtension(fileImage.FileName) + "-" + fileCount.ToString() + Path.GetExtension(fileImage.FileName);
                        path = Path.Combine(Server.MapPath("~/Assets/Admin/img_des/"), fileName);
                        fileCount++;
                    }

                    dp.status = 1;
                    fileImage.SaveAs(path);
                    dp.destination_image = "~/Assets/Admin/img_des/" + fileName;
                    data.destination_points.InsertOnSubmit(dp);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("fileUpLoad", "thếu hình");
                    return View(dp);
                }
            }
            return View(dp);
        }




        public ActionResult Details(int id)
        {
            destination_point des = data.destination_points.FirstOrDefault(p => p.destination_id == id);
            ViewBag.hotel_id = des.destination_id;
            if (des == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(des);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            destination_point des = data.destination_points.FirstOrDefault(p => p.destination_id == id);
            if (des == null)
            {
                return HttpNotFound();
            }
            return View(des);
        }

        [HttpPost]
        public ActionResult Edit(destination_point model, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {
                destination_point des = data.destination_points.FirstOrDefault(c => c.destination_id == model.destination_id);
                if (des == null)
                {
                    return HttpNotFound();
                }
                des.destination_name = model.destination_name;
                des.status = 2;    
                if (fileImage != null && fileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Admin/img_des/"), fileName);

                    //Xóa file cũ trước đó
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }


                    var fileCount = 1;
                    //Kiểm tra sự tồn tại của file
                    while (System.IO.File.Exists(path))
                    {
                        fileName = Path.GetFileNameWithoutExtension(fileImage.FileName) + "-" + fileCount.ToString() + Path.GetExtension(fileImage.FileName);
                        path = Path.Combine(Server.MapPath("~/Assets/Admin/img_des/"), fileName);
                        fileCount++;
                    }
                    fileImage.SaveAs(path);
                    des.destination_image = "~/Assets/Admin/img_des/" + fileName;

                    data.SubmitChanges();
                    return RedirectToAction("Index", "Destination_Point");
                }
                else
                {
                    ModelState.AddModelError("fileUpLoad", "thếu hình");
                    return View(des);
                }

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            destination_point ht = data.destination_points.FirstOrDefault(p => p.destination_id == id);
            ViewBag.destination_id = ht.destination_id;
            if (ht == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(ht);
        }

        //chấp nhận xóa
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            destination_point dp = data.destination_points.FirstOrDefault(p => p.destination_id == id);
            ViewBag.destination_id = dp.destination_id;
            if (dp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.destination_points.DeleteOnSubmit(dp);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            destination_point dp = data.destination_points.FirstOrDefault(p => p.destination_id == id);
            dp.status = 0;
            UpdateModel(dp);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            destination_point dp = data.destination_points.FirstOrDefault(p => p.destination_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            dp.status = (dp.status == 2) ? 1 : 2;
            UpdateModel(dp);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            destination_point dp = data.destination_points.FirstOrDefault(p => p.destination_id == id);
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
            return View(data.destination_points.Where(m => m.status == 0).ToList().OrderBy(n => n.destination_id).ToPagedList(pagenumber, pagesize));
        }
    }
}