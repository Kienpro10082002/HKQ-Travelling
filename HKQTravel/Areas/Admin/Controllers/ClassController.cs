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
    public class ClassController : BaseController
    {
        // GET: Admin/Tour
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.classes.ToList().ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(@class classes , FormCollection t)
        {
            var className = t["class_name"];
            if (ModelState.IsValid)
            {
                classes.class_name = className;
                data.classes.InsertOnSubmit(classes);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(classes);
        }




        public ActionResult Details(int id)
        {
            @class classes = data.classes.FirstOrDefault(p => p.class_id == id);
            ViewBag.class_id = classes.class_id;
            if (classes == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(classes);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            @class classes = data.classes.FirstOrDefault(p => p.class_id == id);
            if (classes == null)
            {
                return HttpNotFound();
            }
            return View(classes);
        }

        [HttpPost]
        public ActionResult Edit(@class classes, FormCollection t)
        {
            if (ModelState.IsValid)
            {
                var class1 = data.classes.SingleOrDefault(c => c.class_id == classes.class_id);
                var className = t["class_name"];
                if (class1 == null)
                {
                    return HttpNotFound();
                }
                class1.class_name = classes.class_name;
                data.SubmitChanges();
                return RedirectToAction("Index", "Class");


            }
            return View(classes);
        }
    }
}