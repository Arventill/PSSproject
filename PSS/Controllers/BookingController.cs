using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSS.Models;

namespace PSS.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            return View(nameof(Index), new BookingModel());
        }

        [HttpPost]
        public ActionResult HandleBooking_Post(BookingModel postModel)
        {
            string ip = Request.UserHostAddress;
            string ip2 = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrWhiteSpace(ip2))
            {
                ip2 = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (!string.IsNullOrWhiteSpace(ip) && ip != "0")
            {
                postModel.IP = ip;
            }
            else if (!string.IsNullOrWhiteSpace(ip2) && ip2 != "0")
            {
                postModel.IP = ip2;
            }
            else
            {
                postModel.IP = "0";
            }

            bool res = Utils.SetSession(postModel);

            return View(nameof(Index), new BookingModel());
        }
    }
}