using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        private modelContext DB = new modelContext();

        public ActionResult viewUsers()
        {
            var data = (from User in DB.Users select User).ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult viewUsers(modelContext model)
        {
            var data = DB.Users.ToList();
            DB.SaveChanges();
            return View(data); 
        }

        [HttpGet]
        public ActionResult viewFeedback()
        {
            var data = (from FeedBack in DB.feedBacks select FeedBack).ToList();
            return View(data);
        }




    }
}