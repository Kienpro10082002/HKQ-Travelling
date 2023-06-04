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
    public class HotelController : BaseController
    {
        // GET: Admin/Hotel
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.hotels.Where(p => p.status != 0).ToList().ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(hotel ht, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {
              
                if (fileImage != null && fileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Admin/img_hotel/"), fileName);
                    var fileCount = 1;
                    //Kiểm tra sự tồn tại của file
                    while (System.IO.File.Exists(path))
                    {
                        fileName = Path.GetFileNameWithoutExtension(fileImage.FileName) + "-" + fileCount.ToString() + Path.GetExtension(fileImage.FileName);
                        path = Path.Combine(Server.MapPath("~/Assets/Admin/img_hotel/"), fileName);
                        fileCount++;
                    }

                    ht.status = 1;
                    fileImage.SaveAs(path);
                    ht.image = "~/Assets/Admin/img_hotel/" + fileName;
                    data.hotels.InsertOnSubmit(ht);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("fileUpLoad", "thếu hình");
                    return View(ht);
                }
            }
            return View(ht);
        }




        public ActionResult Details(int id)
        {
            hotel ht = data.hotels.FirstOrDefault(p => p.hotel_id == id);
            ViewBag.hotel_id = ht.hotel_id;
            if (ht == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ht);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            hotel ht = data.hotels.FirstOrDefault(p => p.hotel_id == id);
            if (ht == null)
            {
                return HttpNotFound();
            }
            return View(ht);
        }

        [HttpPost]
        public ActionResult Edit(hotel model, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {
                hotel ht = data.hotels.FirstOrDefault(c => c.hotel_id == model.hotel_id);
                if (ht == null)
                {
                    return HttpNotFound();
                }
                ht.hotel_name = model.hotel_name;
                ht.location = model.location;
                ht.number_room = model.number_room;
                ht.price = model.price;
                ht.ranking = model.ranking;
                if (fileImage != null && fileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/Admin/img_hotel/"), fileName);

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
                        path = Path.Combine(Server.MapPath("~/Assets/Admin/img_hotel/"), fileName);
                        fileCount++;
                    }
                    fileImage.SaveAs(path);
                    ht.image = "~/Assets/Admin/img_hotel/" + fileName;

                    data.SubmitChanges();
                    return RedirectToAction("Index", "Hotel");
                }
                else
                {
                    ModelState.AddModelError("fileUpLoad", "thếu hình");
                    return View(ht);
                }

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            hotel ht = data.hotels.FirstOrDefault(p => p.hotel_id == id);
            ViewBag.hotel_id = ht.hotel_id;
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
            hotel ht = data.hotels.FirstOrDefault(p => p.hotel_id == id);
            ViewBag.tour_type_id = ht.hotel_id;
            if (ht == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.hotels.DeleteOnSubmit(ht);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            hotel ht = data.hotels.FirstOrDefault(p => p.hotel_id == id);
            ht.status = 0;
            UpdateModel(ht);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            hotel ht = data.hotels.FirstOrDefault(p => p.hotel_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            ht.status = (ht.status == 2) ? 1 : 2;
            UpdateModel(ht);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            hotel ht = data.hotels.FirstOrDefault(p => p.hotel_id == id);
            ht.status = 2;
            UpdateModel(ht);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //hiện danh sách trong thùng rác
        public ActionResult Trashlist(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(data.hotels.Where(m => m.status == 0).ToList().OrderBy(n => n.hotel_id).ToPagedList(pagenumber, pagesize));
        }
    }
}