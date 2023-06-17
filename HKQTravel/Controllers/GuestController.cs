using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Antlr.Runtime.Tree;
using HKQTravel.Models;

namespace HQKTravel.Controllers
{
    public class GuestController : Controller
    {
        HKQTravelDataContext data = new HKQTravelDataContext();

        #region Functions
        //mã hóa md5-HKQ-27/4 (MD5 Descript password)
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

        // check tk tồn tại chưa-HKQ-27/4 (Check exist account)
        private bool checktk(string tk)
        {
            return data.user_accounts.Count(x => x.user_name == tk) > 0;
        }


        // check email tồn tại chưa-HKQ-27/4
        private bool checkemail(string em)
        {
            return data.user_accounts.Count(x => x.email == em) > 0;
        }
        // check sđt tồn tại chưa-HKQ-30/4
        private bool checkstd(string sdt)
        {
            return data.user_accounts.Count(x => x.phone_number == sdt) > 0;
        }
        #endregion

        #region Register
        //đăng ký-HKQ-27/4
        [HttpGet] // khi truy cập vào đường dẫn này phương thức sẽ được gọi ra và xử lý yêu cầu 
        public ActionResult Register()
        {
            if (Session["user_account"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, user_account guest, HttpPostedFileBase ImageFile)
        {
            var FULLNAME = collection["fullname"];
            var USER = collection["user"];
            var EMAIL = collection["email"];
            var PASSWORD = collection["password"];
            var RE_PASS = collection["repassword"];
            string BIRTHDAY = Request.Form["day"] + "-" + Request.Form["month"] + "-" + Request.Form["year"];
            //DateTime birthDate = DateTime.ParseExact(BIRTHDAY, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var SEX = collection["sex"];
            var PHONE = collection["phone"];
            var ADDRESS = collection["adress"];

            #region check 

            if (checktk(USER))
            {
                ViewData["erro_user"] = "Tài khoản đã tồn tại!";
                return View();
            }
            else if (checkemail(EMAIL))
            {
                ViewData["erro_email"] = "Email đã tồn tại!";
                return View();
            }
            else if (checkemail(PHONE))
            {
                ViewData["erro_phone"] = "Số điện thoại đã tồn tại!";
                return View();
            }

            else if (string.IsNullOrEmpty(FULLNAME))
            {
                ViewData["erro_fullname"] = "Họ tên không được trống !";
            }
            else if (FULLNAME.Any(char.IsDigit))
            {
                ViewData["erro_fullname"] = "Họ tên không được nhập số ";
            }
            else if (string.IsNullOrEmpty(USER))
            {
                ViewData["erro_user"] = "Tài khoản không được trống !";
            }
            else if (string.IsNullOrEmpty(PASSWORD))
            {
                ViewData["erro_pass"] = "Mật khẩu không được trống !";
            }
            else if (PASSWORD.Length < 6)
            {
                ViewData["erro_pass"] = "mật khẩu không được dưới 6 ký tự";
            }
            else if (string.IsNullOrEmpty(RE_PASS))
            {
                ViewData["erro_repass"] = "Vui lòng không để trống ô nhập lại mật khẩu !";
            }
            else if (PASSWORD != RE_PASS)
            {
                ViewData["erro_repass"] = "xác nhận mật khẩu không đúng";
            }
            else if (!PASSWORD.Any(char.IsUpper))
            {
                ViewData["erro_pass"] = "Mật khẩu phải có ít nhất 1 chữ cái in hoa, 1 chữ cái thường, 1 chữ số , 1 ký tự đặc biệt";
            }
            else if (!PASSWORD.Any(char.IsLower))
            {
                ViewData["erro_pass"] = "Mật khẩu phải có ít nhất 1 chữ cái in hoa, 1 chữ cái thường, 1 chữ số , 1 ký tự đặc biệt";
            }
            else if (!PASSWORD.Any(char.IsDigit))
            {
                ViewData["erro_pass"] = "Mật khẩu phải có ít nhất 1 chữ cái in hoa, 1 chữ cái thường, 1 chữ số , 1 ký tự đặc biệt";
            }
            else if (!PASSWORD.Any(c => !char.IsLetterOrDigit(c)))
            {
                ViewData["erro_pass"] = "Mật khẩu phải có ít nhất 1 chữ cái in hoa, 1 chữ cái thường, 1 chữ số , 1 ký tự đặc biệt";
            }
            else if (string.IsNullOrEmpty(RE_PASS))
            {
                ViewData["erro_repass"] = "Vui lòng nhập lại mật khẩu !";
            }
            else if (string.IsNullOrEmpty(EMAIL))
            {
                ViewData["erro_email"] = "Email không được trống !";
            }
            else if (string.IsNullOrEmpty(PHONE))
            {
                ViewData["erro_phone"] = "Số điện thoại không được trống !";
            }
            else if (PHONE.Length < 10)
            {
                ViewData["erro_phone"] = "Số điện thoại phải là 10 số";
            }
            else if (string.IsNullOrEmpty(ADDRESS))
            {
                ViewData["erro_address"] = "Địa chỉ không được trống !";
            }
            #endregion

            else
            {
                guest.user_fullname = FULLNAME;
                guest.user_name = USER;
                guest.email = EMAIL;
                guest.user_password = mahoamd5(PASSWORD);
                guest.birthday = DateTime.Parse(BIRTHDAY);
                guest.sex = SEX;
                guest.phone_number = PHONE;
                guest.address = ADDRESS;
                guest.create_date = DateTime.Now;
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/User/img_account/"), fileName);
                    ImageFile.SaveAs(path);
                    ViewBag.FileStatus = "File uploaded successfully.";
                    guest.user_image = "~/Assets/User/img_account/" + fileName;
                }
                else
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
                data.user_accounts.InsertOnSubmit(guest);
                data.SubmitChanges();
                return RedirectToAction("Index", "Home");
            }
            return this.Register();
        }
        #endregion

        #region Login
        //HKQ 1-5-2023
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["user_account"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var user = collection["user"];
            var pass = mahoamd5(collection["pass"]);
            if (string.IsNullOrEmpty(user))
            {
                ViewData["erro_user"] = "Vui lòng nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(pass))
            {
                ViewData["erro_pass"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                var guest = data.user_accounts.FirstOrDefault(p => p.user_name == user && p.user_password == pass);
                if (guest != null)
                {
                    ViewBag.thongbao = "đăng nhập thành công";
                    Session["user_account"] = guest;
                    Session["fullName"] = guest.user_fullname;
                    Session["Email"] = guest.email;
                    Session["user_id"] = guest.user_id;
                    return RedirectToAction("Index", "Home");
                }
                else if (!checktk(user))
                {
                    ViewBag.thongbao = "Tài khoản không tồn tại";
                }

                else
                {
                    ViewBag.thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return this.Login();

        }
        #endregion

        #region Check profile and Log out
        [HttpGet]
        public ActionResult profile()
        {

            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Guest");
            }
            // Lấy thông tin người dùng từ Session
            var userid = (long)Session["user_id"];
            // Lấy thông tin người dùng từ CSDL dựa vào userID
            var user = data.user_accounts.FirstOrDefault(p => p.user_id == userid);
            return View(user);
        }

        [HttpPost]
        public ActionResult profile(String Fullname, String Phone, String Email)
        {
            var userid = (long)Session["user_id"];
            // Lấy thông tin khách hàng cũ từ database
            var oldCustomer = data.user_accounts.FirstOrDefault(c => c.user_id == userid);
            if (oldCustomer == null)
            {
                // Nếu không tìm thấy khách hàng, trả về trang lỗi 404
                return HttpNotFound();
            }

            // Cập nhật thông tin khách hàng mới
            if (!string.IsNullOrEmpty(Fullname))
            {
                oldCustomer.user_fullname = Fullname;
            }

            if (!string.IsNullOrEmpty(Phone))
            {
                oldCustomer.phone_number = Phone;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                oldCustomer.email = Email;
            }

            data.SubmitChanges();

            // Hiển thị thông báo cập nhật thành công
            TempData["SuccessMessage"] = "Cập nhật thông tin thành công.";

            // Chuyển hướng về trang Profile
            return RedirectToAction("profile");
        }


        //HKQ 1-5-2023
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Change and Foget password
        [HttpGet]
        public ActionResult changePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult changePassword(FormCollection collection)
        {
            //Get Session["user_account"] are logged in before
            var userAccount = (user_account)Session["user_account"];

            //Get collection from changePassword.html with the attribute is name
            var pass = mahoamd5(collection["current_password"]);
            var new_pass = mahoamd5(collection["new_password"]);
            var confirm_pass = mahoamd5(collection["confirm_password"]);

            //create variable and check info
            var check_user = data.user_accounts.SingleOrDefault(model => model.user_name == userAccount.user_name && model.user_password == userAccount.user_password);
            var check_password = data.user_accounts.SingleOrDefault(model => model.user_password == pass);

            if (check_user == null)
            {
                ViewData["WrongUser"] = "Vui lòng nhập đúng thông tin";
            }
            else if (check_password == null)
            {
                ViewData["WrongPassword"] = "Vui lòng nhập đúng mật khẩu";
            }
            else if (confirm_pass != new_pass)
            {
                ViewData["WrongNewPassword"] = "Vui lòng nhập khớp mật khẩu mới";
            }
            else //successful
            {
                check_user.user_password = confirm_pass;
            }
            data.SubmitChanges();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Fogot password
        [HttpGet]
        public ActionResult findAccountToSendPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult findAccountToSendPassword(FormCollection collection)
        {
            var findUser = collection["userName"];
            var findEmail = collection["eMail"];

            var user = data.user_accounts.SingleOrDefault(account => account.user_name == findUser);
            var mail = data.user_accounts.SingleOrDefault(account => account.email == findEmail);
            if (user == null)
            {
                ViewData["Message_forgotPassword"] = "Không tìm thấy tài khoản của bạn!";
                return View();
            }
            else if (mail == null)
            {
                ViewData["Message_forgotPassword"] = "Không tìm thấy email của bạn!";
                return View();
            }
            else
            {
                //Tạo đối tượng MailMessage
                //MailMessage mailMessage = new MailMessage();
                //mailMessage.From = new MailAddress("zuka10082002@gmail.com");
                //mailMessage.To.Add(findEmail);
                //mailMessage.Subject = "Mã xác thực tài khoản của bạn là: 179325! (HKQ Travelling)";
                //mailMessage.Body = "Nếu bạn yêu cầu đổi mật khẩu thì vui lòng quay lại trang để nhập mật khẩu mới!";
                //var authorityNumber = "179325";

                //Cấu hình SMTP để gửi email
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp-relay.gmail.com";
                //smtp.Port = 587; // Có thể thay đổi theo cấu hình của bạn
                //smtp.EnableSsl = true; // Sử dụng SSL nếu cần thiết
                //smtp.Credentials = new System.Net.NetworkCredential("zuka10082002@gmail.com", "Neik.G034415615620028001");

                //Gửi email
                //smtp.Send(mailMessage);

                //return RedirectToAction("typeAuthorityNumber", new { userName = findUser });
                return RedirectToAction("forgotPassword", new { userName = findUser });
            }
        }

        //[HttpGet]
        //public ActionResult typeAuthorityNumber(string userName)
        //{
        //    ViewData["userName"] = userName;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult typeAuthorityNumber(FormCollection collection)
        //{
        //    var username = collection["userName"];
        //    var authorityNumber = collection["acceptNumber"];
        //    if (179325 == int.Parse(authorityNumber))
        //    {
        //        return RedirectToAction("forgotPassword", "Guest", new { userName = username });
        //    }
        //    else
        //    {
        //        ViewBag["Message"] = "Nhập sai mã xác thực";
        //        return View();
        //    }
        //}

        [HttpGet]
        public ActionResult forgotPassword(string userName)
        {
            ViewData["userName"] = userName;
            return View();
        }

        [HttpPost]
        public ActionResult forgotPassword(FormCollection collection)
        {
            //Get collection from changePassword.html with the attribute is name
            var userName = collection["userName"];
            var new_pass = mahoamd5(collection["new_password"]);
            var confirm_pass = mahoamd5(collection["confirm_password"]);

            //create variable and check info
            var check_user = data.user_accounts.SingleOrDefault(model => model.user_name == userName);

            if (check_user == null)
            {
                return HttpNotFound();
            }
            else if (new_pass == null && confirm_pass == null)
            {
                ViewData["WrongPassword"] = "Vui lòng nhập đầy đủ thông tin";
            }
            else if (confirm_pass != new_pass)
            {
                ViewData["WrongPassword"] = "Vui lòng nhập khớp mật khẩu mới";
            }
            else //successful
            {
                ViewData["WrongPassword"] = "Đổi mật khẩu thành công";
                check_user.user_password = confirm_pass;
            }
            data.SubmitChanges();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region history
        [HttpGet]
        public ActionResult historyTour()
        {
            var userid = (long)Session["user_id"];
            var listTour = (from bill_tour in data.bill_tours
                            where bill_tour.user_id == userid
                            select new HistoryTour
                            {
                                BillId = bill_tour.bill_tour_id,
                                CreateDate = bill_tour.create_date.ToString(),
                                FullName = bill_tour.user_account.user_fullname.ToString(),
                                TourName = bill_tour.tour.tour_name.ToString(),
                                Price = bill_tour.tour.price != null ? (decimal)bill_tour.tour.price : 0,
                                Quantity = bill_tour.quantity.ToString(),
                                TotalPrice = bill_tour.total != null ? (decimal)bill_tour.total : 0
                            }).OrderByDescending(t => t.BillId).ToList();
            return View(listTour);
        }

        [HttpGet]
        public ActionResult historyHotel()
        {
            var userid = (long)Session["user_id"];
            var listHotel = (from bill_hotel in data.bill_hotels
                             where bill_hotel.user_id == userid
                             select new HistoryHotel
                             {
                                 BillId = bill_hotel.bill_hotel_id,
                                 CreateDate = bill_hotel.create_date.ToString(),
                                 FullName = bill_hotel.user_account.user_fullname.ToString(),
                                 HotelName = bill_hotel.hotel.hotel_name.ToString(),
                                 Price = bill_hotel.hotel.price != null ? (decimal)bill_hotel.hotel.price : 0,
                                 Quantity = bill_hotel.quantity.ToString(),
                                 TotalPrice = bill_hotel.total != null ? (decimal)bill_hotel.total : 0
                             }).OrderByDescending(t => t.BillId).ToList();
            return View(listHotel);
        }

        [HttpGet]
        public ActionResult historyFlight()
        {
            var userid = (long)Session["user_id"];
            var listFlight = (from bill_flight in data.bill_flights
                              where bill_flight.user_id == userid
                              select new HistoryFlight
                              {
                                  BillId = bill_flight.bill_flight_id,
                                  CreateDate = bill_flight.create_date.ToString(),
                                  FullName = bill_flight.user_account.user_fullname.ToString(),
                                  FlightName = bill_flight.flight.flight_brand.flight_brand_name.ToString(),
                                  Price = bill_flight.flight.price != null ? (decimal)bill_flight.flight.price : 0,
                                  Quantity = bill_flight.quantity.ToString(),
                                  TotalPrice = bill_flight.total != null ? (decimal)bill_flight.total : 0
                              }).OrderByDescending(t => t.BillId).ToList();
            return View(listFlight);
        }
        #endregion
    }
}