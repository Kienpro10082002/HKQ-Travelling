using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HKQTravel.Models;
using Microsoft.Ajax.Utilities;

namespace HKQTravel.Controllers
{
    public class TourCartController : Controller
    {
        HKQTravelDataContext data = new HKQTravelDataContext();

        public List<TourCartModel> GetBill_Tours()
        {
            List<TourCartModel> bill_Tours = Session["bill_tour"] as List<TourCartModel>;
            if (bill_Tours ==null) {
                bill_Tours = new List<TourCartModel>();
                Session["bill_tour"] = bill_Tours;
            }
            return bill_Tours;
        }
        public ActionResult add_TourCart(long id, string strURL)
        {
            List<TourCartModel> lsbill_Tours = GetBill_Tours();
            TourCartModel tourCartss = lsbill_Tours.Find(n => n.iIdTour == id);
            if (tourCartss == null)
            {
                tourCartss = new TourCartModel(id);
                lsbill_Tours.Add(tourCartss);
            }
            else
            {
                tourCartss.iQuantity++;
            }
            return RedirectToAction("TourCart", "TourCart");
        }

        public int dTotalQuatityTour()
        {
            int itotalQuatityTour = 0;
            List<TourCartModel> lsbill_tour = Session["bill_tour"] as List<TourCartModel>;
            
            if (lsbill_tour !=null)
            {
               itotalQuatityTour = lsbill_tour.Sum(n => n.iQuantity);
            }
            return itotalQuatityTour;
        }

        public decimal dTotalpriceTour()
        {
            decimal dTotalpriceTour = 0;
            List <TourCartModel> lsbill_tours = Session["bill_tour"] as List<TourCartModel>;
            if (lsbill_tours != null)
            {
                dTotalpriceTour = lsbill_tours.Sum(n=> n.dTotalpriceTour.GetValueOrDefault());
            }
            return dTotalpriceTour;
        }
        public long lIdTour()
        {
            long idTour = 0;
            List<TourCartModel> lsbill_Tours = Session["bill_tour"] as List <TourCartModel>;
            if(lsbill_Tours != null)
            {
                idTour = lsbill_Tours.Sum(n => n.iIdTour);
            }
            return idTour;
        }
        public ActionResult TourCart()
        {
            List<TourCartModel> lsbill_tours = GetBill_Tours();
            if(lsbill_tours.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.dTotalQuatityTour = dTotalQuatityTour();
            ViewBag.dTotalpriceTour = dTotalpriceTour();
            return View(lsbill_tours);

        }
        public ActionResult TourCartPartial()
        {
            ViewBag.dTotalpriceTour = dTotalpriceTour();
            ViewBag.dTotalQuatityTour = dTotalQuatityTour();
            return PartialView();
        }
        //Xoa Giohang
        public ActionResult deleteTourCart(int id)
        {
            //Lay gio hang tu Session
            List<TourCartModel> lstourCarts = GetBill_Tours();
            //Kiem tra sach da co trong Session["Giohang"]
            TourCartModel tourCarts = lstourCarts.SingleOrDefault(n => n.iIdTour == id);
            //Neu ton tai thi cho sua Soluong

            if (lstourCarts.Count == 0)
                return RedirectToAction("Index", "Home");
            if (tourCarts != null)
            {
                lstourCarts.RemoveAll(n => n.iIdTour == id);
                return RedirectToAction("TourCart", "TourCart");
            }
            return RedirectToAction("Travel", "Home");
        }
        //Cap nhat Giỏ hàng
        public ActionResult updateTourCart(int id, FormCollection f)
        {

            //Lay gio hang tu Session
            List<TourCartModel> tourCartss = GetBill_Tours();
            //Kiem tra sach da co trong Session["Giohang"]
            TourCartModel tourCarts = tourCartss.SingleOrDefault(n => n.iIdTour == id);
            //Neu ton tai thi cho sua Soluong
            if (tourCarts != null)
            {
                tourCarts.iQuantity = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("TourCart");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult deleteAllTourCart()
        {
            //Lay gio hang tu Session
            List<TourCartModel> tourCartss = GetBill_Tours();
            tourCartss.Clear();
            return RedirectToAction("Index", "Home");
        }
        //Hien thi View bookingTour de cap nhat cac thong tin cho Don hang
        [HttpGet]
        public ActionResult bookingTour()
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
            List<TourCartModel> lstGiohang = GetBill_Tours();
            ViewBag.dTotalQuatityTour = dTotalQuatityTour();
            ViewBag.dTotalpriceTour = dTotalpriceTour();
            ViewBag.lIdTour = lIdTour();
            /*ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();*/

            return View(lstGiohang);
        }
        //Xay dung chuc nang bookingTour
        [HttpPost]
        public ActionResult bookingTour(FormCollection f)
        {
            //Them Don hang
            bill_tour bill_Tours = new bill_tour();
            user_account user = (user_account)Session["user_account"];
            List<TourCartModel> tourCarts = GetBill_Tours();
            bill_Tours.user_id = user.user_id;
            bill_Tours.create_date = DateTime.Now;
            foreach (var item in tourCarts)
            {
                bill_Tours.quantity = item.iQuantity;
                bill_Tours.total = item.dTotalpriceTour;
                bill_Tours.tour_id = item.iIdTour;
            }
            data.bill_tours.InsertOnSubmit(bill_Tours);
            data.SubmitChanges();
            Session["bill_tour"] = null;
            return RedirectToAction("SubmitTourCart", "TourCart");
            
        }
        public ActionResult SubmitTourCart()
        {
            return View();
        }


    }
}