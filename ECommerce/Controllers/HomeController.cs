using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        protected Admin admin;
        protected Customer customer;
        static List<Item> listOfItems = new List<Item>();
        static List<Order> listOfOrders = new List<Order>();
        static List<Offer> listOfOffers = new List<Offer>();
        public ActionResult Index()
        {
            modelContext modelContext1 = new modelContext();
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
  
 

    }
}