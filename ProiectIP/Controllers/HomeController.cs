using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectIP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Restaurants()
        {
            return View();
        }

        public ActionResult Museums()
        {
            return View();
        }

        public ActionResult Cafes()
        {
            return View();
        }

        public ActionResult MovieTheaters()
        {
            return View();
        }

        public ActionResult ShoppingMalls()
        {
            return View();
        }

        public ActionResult Bars()
        {
            return View();
        }

        public ActionResult Parks()
        {
            return View();
        }
    }
}