using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Controllers
{
    public class HotelCartController : Controller
    {
        HKQTravelDataContext data = new HKQTravelDataContext();

        public List<HotelCartModel> GetBill_Hotels()
        {
            List<HotelCartModel> bill_Hotels = Session["bill_hotel"] as List<HotelCartModel>;
            if (bill_Hotels == null)
            {
                bill_Hotels = new List<HotelCartModel>();
                Session["bill_hotel"] = bill_Hotels;
            }
            return bill_Hotels;
        }
        public ActionResult add_HotelCart(long id, string strURL)
        {
            List<HotelCartModel> lsbill_Hotels = GetBill_Hotels();
            HotelCartModel HotelCarts = lsbill_Hotels.Find(n => n.iHotelId == id);
            if (HotelCarts == null)
            {
                HotelCarts = new HotelCartModel(id);
                lsbill_Hotels.Add(HotelCarts);
            }
            else
            {
                HotelCarts.iQuantity++;
            }
            return RedirectToAction("HotelCart", "HotelCart");
        }

        public int dTotalQuatityHotel()
        {
            int itotalQuantityHotel = 0;
            List<HotelCartModel> lsbill_tour = Session["bill_hotel"] as List<HotelCartModel>;

            if (lsbill_tour != null)
            {
                itotalQuantityHotel = lsbill_tour.Sum(n => n.iQuantity);
            }
            return itotalQuantityHotel;
        }

        public decimal dTotalpriceHotel()
        {
            decimal dTotalpriceHotel = 0;
            List<HotelCartModel> lsbill_Hotels = Session["bill_hotel"] as List<HotelCartModel>;
            if (lsbill_Hotels != null)
            {
                dTotalpriceHotel = lsbill_Hotels.Sum(n => n.dTotalpriceHotel.GetValueOrDefault());
            }
            return dTotalpriceHotel;
        }
        public long lIdHotel()
        {
            long idHotel = 0;
            List<HotelCartModel> lsbill_Hotels = Session["bill_hotel"] as List<HotelCartModel>;
            if (lsbill_Hotels != null)
            {
                idHotel = lsbill_Hotels.Sum(n => n.iHotelId);
            }
            return idHotel;
        }
        public ActionResult HotelCart()
        {
            List<HotelCartModel> lsbill_Hotels = GetBill_Hotels();
            if (lsbill_Hotels.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.dTotalQuatityHotel = dTotalQuatityHotel();
            ViewBag.dTotalpriceHotel = dTotalpriceHotel();
            return View(lsbill_Hotels);

        }
        public ActionResult HotelCartPartial()
        {
            ViewBag.dTotalpriceHotel = dTotalpriceHotel();
            ViewBag.dTotalQuatityHotel = dTotalQuatityHotel();
            return PartialView();
        }
        //Xoa Giohang
        public ActionResult deleteHotelCart(int id)
        {
            //Lay gio hang tu Session
            List<HotelCartModel> lsHotelCart = GetBill_Hotels();
            //Kiem tra sach da co trong Session["Giohang"]
            HotelCartModel HotelCart = lsHotelCart.SingleOrDefault(n => n.iHotelId == id);
            //Neu ton tai thi cho sua Soluong

            if (lsHotelCart.Count == 0)
                return RedirectToAction("Hotel", "Home");
            if (HotelCart != null)
            {
                lsHotelCart.RemoveAll(n => n.iHotelId == id);
                return RedirectToAction("HotelCart", "HotelCart");
            }
            return RedirectToAction("HotelCart", "HotelCart");
        }
        //Cap nhat Giỏ hàng
        public ActionResult updateHotelCart(int id, FormCollection f)
        {

            //Lay gio hang tu Session
            List<HotelCartModel> HotelCarts = GetBill_Hotels();
            //Kiem tra sach da co trong Session["Giohang"]
            HotelCartModel HotelCart = HotelCarts.SingleOrDefault(n => n.iHotelId == id);
            //Neu ton tai thi cho sua Soluong
            if (HotelCart != null)
            {
                HotelCart.iQuantity = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("HotelCart");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult deleteAllHotelCart()
        {
            //Lay gio hang tu Session
            List<HotelCartModel> HotelCarts = GetBill_Hotels();
            HotelCarts.Clear();
            return RedirectToAction("Index", "Home");
        }
        //Hien thi View bookingTour de cap nhat cac thong tin cho Don hang
        [HttpGet]
        public ActionResult bookingHotel()
        {
            //Kiem tra dang nhap
            if (Session["user_account"] == null || Session["user_account"].ToString() == "")
            {
                return RedirectToAction("Login", "Guest");
            }
            if (Session["user_account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Lay gio hang tu Session
            List<HotelCartModel> lstGiohang = GetBill_Hotels();
            ViewBag.dTotalQuatityHotel = dTotalQuatityHotel();
            ViewBag.dTotalpriceHotel = dTotalpriceHotel();
            ViewBag.lIdHotel = lIdHotel();
            /*ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();*/

            return View(lstGiohang);
        }
        //Xay dung chuc nang bookingTour
        [HttpPost]
        public ActionResult bookingHotel(FormCollection f)
        {
            //Them Don hang
            bill_hotel bill_Hotels = new bill_hotel();
            user_account user = (user_account)Session["user_account"];
            List<HotelCartModel> HotelCarts = GetBill_Hotels();
            bill_Hotels.user_id = user.user_id;
            bill_Hotels.create_date = DateTime.Now;
            foreach (var item in HotelCarts)
            {
                bill_Hotels.quantity = item.iQuantity;
                bill_Hotels.total = item.dTotalpriceHotel;
                bill_Hotels.hotel_id = item.iHotelId;
            }
            data.bill_hotels.InsertOnSubmit(bill_Hotels);
            data.SubmitChanges();
            Session["bill_hotel"] = null;
            return RedirectToAction("SubmitHotelCart", "HotelCart");

        }
        public ActionResult SubmitHotelCart()
        {
            return View();
        }


    }
}