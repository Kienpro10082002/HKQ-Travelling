using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        
        HKQTravelDataContext data = new HKQTravelDataContext();
        
        // GET: Admin/Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string taikhoan, string matkhau)
        {
            string erro = null;
            admin_account ad = data.admin_accounts.Where(p => p.admin_user == taikhoan && p.admin_password == matkhau).FirstOrDefault();
            if (ad == null)
            {
                erro = "sai bét rồi bạn ơi!";
            }
            else
            {
                Session["admin_fulllname"] = ad.admin_fulllname;
                Session["admin_user"] = taikhoan;
                Session["admin_id"] = ad.admin_id;
                return RedirectToAction("Index", "DashBoard");
            }
            ViewBag.Error = erro;

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["admin_user"] = null;
            Session["admin_id"] = null;
            return Redirect("~/Admin/Login");
        }
    }
}