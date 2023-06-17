using HKQTravel.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        HKQTravelDataContext data = new HKQTravelDataContext();
        public ActionResult Index(int? page)
        {

            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.user_accounts.Where(p => p.status != 0).ToList().ToPagedList(pagenumber, pagesize));
        }
        private string mahoamd5(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                // Nếu input rỗng hoặc null, trả về chuỗi rỗng.
                return string.Empty;
            }
            using (var md5 = MD5.Create())
            {
                var dulieu = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();

                for (int i = 0; i < dulieu.Length; i++)
                {
                    builder.Append(dulieu[i].ToString("x2"));
                }
                return builder.ToString();
            }

        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(user_account user, HttpPostedFileBase fileImage, FormCollection kh)
        {
            if (ModelState.IsValid)
            {
                string BIRTHDAY = kh["ngaysinh"];
                var SEX = kh["gioitinh"];
                var PASSWORD = user.user_password;
                if (fileImage != null && fileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/User/img_account/"), fileName);
                    var fileCount = 1;
                    //Kiểm tra sự tồn tại của file
                    while (System.IO.File.Exists(path))
                    {
                        fileName = Path.GetFileNameWithoutExtension(fileImage.FileName) + "-" + fileCount.ToString() + Path.GetExtension(fileImage.FileName);
                        path = Path.Combine(Server.MapPath("~/Assets/User/img_account/"), fileName);
                        fileCount++;
                    }
                    user.user_password = mahoamd5(PASSWORD); ;
                    user.birthday = DateTime.Parse(BIRTHDAY);
                    user.sex = SEX;
                    user.create_date = DateTime.Now;
                    user.status = 1;
                    fileImage.SaveAs(path);
                    user.user_image = "~/Assets/User/img_account/" + fileName;
                    data.user_accounts.InsertOnSubmit(user);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("fileUpLoad", "thếu hình");
                    return View(user);
                }
            }
            return View(user);
        }




        public ActionResult Details(int id)
        {
            user_account user = data.user_accounts.FirstOrDefault(p => p.user_id == id);
            ViewBag.user_id = user.user_id;
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            user_account user = data.user_accounts.FirstOrDefault(p => p.user_id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(user_account model, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var user = data.user_accounts.SingleOrDefault(c => c.user_id == model.user_id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    user.user_name = model.user_name;
                    user.user_password = mahoamd5(model.user_password);
                    user.user_fullname = model.user_fullname;
                    user.phone_number = model.phone_number;
                    user.email = model.email;
                    user.birthday = model.birthday;
                    user.address = model.address;
                    user.sex = model.sex;
                    user.update_date = DateTime.Now;

                    if (fileImage != null && fileImage.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(fileImage.FileName);
                        var path = Path.Combine(Server.MapPath("~/Assets/User/img_account/"), fileName);

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
                            path = Path.Combine(Server.MapPath("~/Assets/User/img_account/"), fileName);
                            fileCount++;
                        }
                        fileImage.SaveAs(path);
                        user.user_image = "~/Assets/User/img_account/" + fileName;

                        data.SubmitChanges();
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("fileUpLoad", "thếu hình");
                        return View(user);
                    }
                }

            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            user_account user = data.user_accounts.SingleOrDefault(p => p.user_id == id);
            ViewBag.MAKH = user.user_id;
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(user);
        }

        //chấp nhận xóa
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            user_account user = data.user_accounts.SingleOrDefault(p => p.user_id == id);
            ViewBag.MAKH = user.user_id;
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.user_accounts.DeleteOnSubmit(user);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Xóa tạm = 0
        public ActionResult DeleteTemporary(int id)
        {
            user_account user = data.user_accounts.SingleOrDefault(p => p.user_id == id);
            user.status = 0;
            UpdateModel(user);
            data.SubmitChanges();
            return RedirectToAction("");
        }

        //trạng thái từ 1->2 , 2->1
        public ActionResult Status(int? id)
        {

            user_account user = data.user_accounts.SingleOrDefault(p => p.user_id == id);

            //dùng toán tử 3 ngôi biến = biểu thức?biểu thức 2: biểu thức 3; (nếu biểu thức 1 đúng trả về biểu thức 2 cỏn sai thì trả bt3 ) 
            user.status = (user.status == 2) ? 1 : 2;
            UpdateModel(user);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //khôi phục = 2
        public ActionResult Restore(int id)
        {
            user_account user = data.user_accounts.SingleOrDefault(p => p.user_id == id);
            user.status = 2;
            UpdateModel(user);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //hiện danh sách trong thùng rác
        public ActionResult Trashlist(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(data.user_accounts.Where(m => m.status == 0).ToList().OrderBy(n => n.user_id).ToPagedList(pagenumber, pagesize));
        }
    }
}