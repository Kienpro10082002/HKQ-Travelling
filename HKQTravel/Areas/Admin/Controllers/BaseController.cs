using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            // Kiểm tra đăng nhập
            if (System.Web.HttpContext.Current.Session["admin_user"].Equals(""))
            {
                // Người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Auth/Login");
            }
        }
    }
}