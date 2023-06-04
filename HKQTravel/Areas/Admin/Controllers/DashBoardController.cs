using HKQTravel.Areas.Admin.Data;
using HKQTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace HKQTravel.Areas.Admin.Controllers
{
    public class DashBoardController : BaseController
    {
        HKQTravelDataContext data = new HKQTravelDataContext();
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DashBoardForBillTour()
        {
            var listDashBoard = data.bill_tours
                    .GroupBy(b => new { b.create_date.Value.Month, b.create_date.Value.Year })
                    .Select(g => new BillTourByMonth
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        TotalRevenue = (decimal)g.Sum(b => b.total)
                    })
                    .ToList();

            return View("DashBoardForBillTour", listDashBoard);
        }

        [HttpGet]
        public ActionResult DashBoardForBillHotel()
        {
            var listDashBoard = data.bill_hotels
                    .GroupBy(b => new { b.create_date.Value.Month, b.create_date.Value.Year })
                    .Select(g => new BillHotelByMonth
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        TotalRevenue = (decimal)g.Sum(b => b.total)
                    })
                    .ToList();

            return View("DashBoardForBillHotel", listDashBoard);
        }

        [HttpGet]
        public ActionResult DashBoardForBillFlight()
        {
            var listDashBoard = data.bill_flights
                .GroupBy(b => new { b.create_date.Value.Month, b.create_date.Value.Year })
                .Select(g => new BillFlightByMonth
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        TotalRevenue = (decimal)g.Sum(b => b.total)
                    })
                    .ToList();

            return View("DashBoardForBillFlight", listDashBoard);
        }
    }
}