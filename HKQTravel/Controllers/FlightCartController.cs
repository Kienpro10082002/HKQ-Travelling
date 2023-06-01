using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKQTravel.Controllers
{
    public class FlightCartController : Controller
    {
        HKQTravelDataContext data = new HKQTravelDataContext();
        // GET: TourCart
        public ActionResult Index()
        {
            return View();
        }

        public List<FlightCartModel> GetBill_Flights()
        {
            List<FlightCartModel> bill_flights = Session["bill_flight"] as List<FlightCartModel>;
            if (bill_flights == null)
            {
                bill_flights = new List<FlightCartModel>();
                Session["bill_flight"] = bill_flights;
            }
            return bill_flights;
        }
        public ActionResult add_TourCartFlight(long id, string strURL)
        {
            List<FlightCartModel> lsbill_flights = GetBill_Flights();
            FlightCartModel FlightCarts = lsbill_flights.Find(n => n.iFlightId == id);
            if (FlightCarts == null)
            {
                FlightCarts = new FlightCartModel(id);
                lsbill_flights.Add(FlightCarts);
            }
            else
            {
                FlightCarts.iQuantity++;
            }
            return RedirectToAction("FlightCart", "FlightCart");
        }

        public int dTotalQuatityFlight()
        {
            int itotalQuantityFlight = 0;
            List<FlightCartModel> lsbill_tour = Session["bill_flight"] as List<FlightCartModel>;

            if (lsbill_tour != null)
            {
                itotalQuantityFlight = lsbill_tour.Sum(n => n.iQuantity);
            }
            return itotalQuantityFlight;
        }

        public decimal dTotalpriceFlight()
        {
            decimal dTotalpriceFlight = 0;
            List<FlightCartModel> lsbill_flights = Session["bill_flight"] as List<FlightCartModel>;
            if (lsbill_flights != null)
            {
                dTotalpriceFlight = lsbill_flights.Sum(n => n.dTotalpriceFlight.GetValueOrDefault());
            }
            return dTotalpriceFlight;
        }
        public long lidFlight()
        {
            long idFlight = 0;
            List<FlightCartModel> lsbill_flights = Session["bill_flight"] as List<FlightCartModel>;
            if (lsbill_flights != null)
            {
                idFlight = lsbill_flights.Sum(n => n.iFlightId);
            }
            return idFlight;
        }
        public ActionResult FlightCart()
        {
            List<FlightCartModel> lsbill_flights = GetBill_Flights();
            if (lsbill_flights.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.dTotalQuatityFlight = dTotalQuatityFlight();
            ViewBag.dTotalpriceFlight = dTotalpriceFlight();
            return View(lsbill_flights);

        }
        public ActionResult FlightCartPartial()
        {
            ViewBag.dTotalpriceFlight = dTotalpriceFlight();
            ViewBag.dTotalQuatityFlight = dTotalQuatityFlight();
            return PartialView();
        }
        //Xoa Giohang
        public ActionResult deleteFlightCart(int id)
        {
            //Lay gio hang tu Session
            List<FlightCartModel> lsFlightCart = GetBill_Flights();
            //Kiem tra sach da co trong Session["Giohang"]
            FlightCartModel FlightCart = lsFlightCart.SingleOrDefault(n => n.iFlightId == id);
            //Neu ton tai thi cho sua Soluong

            if (lsFlightCart.Count == 0)
                return RedirectToAction("Flight", "Home");
            if (FlightCart != null)
            {
                lsFlightCart.RemoveAll(n => n.iFlightId == id);
                return RedirectToAction("FlightCart", "FlightCart");
            }
            return RedirectToAction("FlightCart", "FlightCart");
        }
        //Cap nhat Giỏ hàng
        public ActionResult updateFlightCart(int id, FormCollection f)
        {

            //Lay gio hang tu Session
            List<FlightCartModel> FlightCarts = GetBill_Flights();
            //Kiem tra sach da co trong Session["Giohang"]
            FlightCartModel FlightCart = FlightCarts.SingleOrDefault(n => n.iFlightId == id);
            //Neu ton tai thi cho sua Soluong
            if (FlightCart != null)
            {
                FlightCart.iQuantity = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("FlightCart");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult deleteAllFlightCart()
        {
            //Lay gio hang tu Session
            List<FlightCartModel> FlightCarts = GetBill_Flights();
            FlightCarts.Clear();
            return RedirectToAction("Index", "Home");
        }
        //Hien thi View bookingTour de cap nhat cac thong tin cho Don hang
        [HttpGet]
        public ActionResult bookingFlight()
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
            List<FlightCartModel> lstGiohang = GetBill_Flights();
            ViewBag.dTotalQuatityFlight = dTotalQuatityFlight();
            ViewBag.dTotalpriceFlight = dTotalpriceFlight();
            ViewBag.lidFlight = lidFlight();
            /*ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();*/

            return View(lstGiohang);
        }
        //Xay dung chuc nang bookingTour
        [HttpPost]
        public ActionResult bookingFlight(FormCollection f)
        {
            //Them Don hang
            bill_flight bill_flights = new bill_flight();
            user_account user = (user_account)Session["user_account"];
            List<FlightCartModel> FlightCarts = GetBill_Flights();
            bill_flights.user_id = user.user_id;
            bill_flights.create_date = DateTime.Now;
            foreach (var item in FlightCarts)
            {
                bill_flights.quantity = item.iQuantity;
                bill_flights.total = item.dTotalpriceFlight;
                bill_flights.flight_id = item.iFlightId;
            }
            data.bill_flights.InsertOnSubmit(bill_flights);
            data.SubmitChanges();
            Session["bill_flight"] = null;
            return RedirectToAction("SubmitFlightCart", "FlightCart");

        }
        public ActionResult SubmitFlightCart()
        {
            return View();
        }

    }

}