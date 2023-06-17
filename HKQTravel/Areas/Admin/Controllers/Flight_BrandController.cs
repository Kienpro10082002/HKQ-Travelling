using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.IO;
using System.Reflection;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class Flight_BrandController : BaseController
    {
        // GET: Admin/Flight_Brand
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.flight_brands.ToList().ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(flight_brand flight_Brand, HttpPostedFileBase fileImage, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var flight_brand_name = f["flight_brand_name"];
                if (fileImage != null && fileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/User/img_flight_brand/"), fileName);
                    var fileCount = 1;
                    while (System.IO.File.Exists(path))
                    {
                        fileName = Path.GetFileNameWithoutExtension(fileImage.FileName) + "-" + fileCount.ToString() + Path.GetExtension(fileImage.FileName);
                        path = Path.Combine(Server.MapPath("~/Assets/User/img_flight_brand/"), fileName);
                        fileCount++;
                    }
                    flight_Brand.flight_brand_name = flight_brand_name;
                    fileImage.SaveAs(path);
                    flight_Brand.flight_brand_image = "~/Assets/User/img_flight_brand/" + fileName;
                    data.flight_brands.InsertOnSubmit(flight_Brand);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("fileUpLoad", "thiếu hình");
                    return View(flight_Brand);

                }
            }
            return View(flight_Brand);
        }




        public ActionResult Details(int id)
        {
            flight_brand flight_Brand = data.flight_brands.FirstOrDefault(p => p.flight_brand_id == id);
            ViewBag.flight_brand_id = flight_Brand.flight_brand_id;
            if (flight_Brand == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(flight_Brand);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {

            flight_brand flight_Brand = data.flight_brands.FirstOrDefault(p => p.flight_brand_id == id);
            if (flight_Brand == null)
            {
                return HttpNotFound();
            }
            return View(flight_Brand);
        }

        [HttpPost]
        public ActionResult Edit(flight_brand flight_Brand, FormCollection t, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var flight_Brands = data.flight_brands.SingleOrDefault(c => c.flight_brand_id == flight_Brand.flight_brand_id);
                    if (flight_Brands == null)
                    {
                        return HttpNotFound();
                    }
                    flight_Brands.flight_brand_name = flight_Brand.flight_brand_name;
                    if (fileImage != null && fileImage.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(fileImage.FileName);
                        var path = Path.Combine(Server.MapPath("~/Assets/User/img_flight_brand/"), fileName);

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
                            path = Path.Combine(Server.MapPath("~/Assets/User/img_flight_brand/"), fileName);
                            fileCount++;
                        }
                        fileImage.SaveAs(path);
                        flight_Brand.flight_brand_image = "~/Assets/User/img_flight_brand/" + fileName;

                        data.SubmitChanges();
                        return RedirectToAction("Index", "Flight_Brand");
                    }
                    else
                    {
                        ModelState.AddModelError("fileUpLoad", "thiếu hình");
                        return View(flight_Brand);
                    }
                }
            }

            return View(flight_Brand);
        }
        public ActionResult Delete(int id)
        {
            flight_brand dp = data.flight_brands.FirstOrDefault(p => p.flight_brand_id == id);
            ViewBag.flight_brand_id = dp.flight_brand_id;
            if (dp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.flight_brands.DeleteOnSubmit(dp);
            data.SubmitChanges();
            return RedirectToAction("Index");

        }
    }
}