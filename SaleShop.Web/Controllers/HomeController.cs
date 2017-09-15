using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleShop.Web.Controllers
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
        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView(); // PartialView("Footer"); chỉ ra đúng tên view khi action và view không trùng tên
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView(); // PartialView("Header"); chỉ ra đúng tên view khi action và view không trùng tên
        }
        [ChildActionOnly]
        public ActionResult Category()
        {
            return PartialView(); 
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}