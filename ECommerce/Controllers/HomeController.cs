using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        // Get
        public ActionResult Dashboard()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    return View();
                }
               return new HttpNotFoundResult("Not Allowed");
              
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult sendFeedback(FeedBack feedBack)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    if (ModelState.IsValid)
                    {
                        using (var modelContext = new modelContext())
                        {
                            if (feedBack != null)
                            {
                                feedBack.Id = (modelContext.feedBacks.Count() + 1).ToString();
                                string id = Session["id"].ToString();
                                var userName = (from s in modelContext.Users where s.Id == id select s.UserNamee).FirstOrDefault();
                                feedBack.UserName = userName;
                                modelContext.feedBacks.Add(feedBack);
                                modelContext.SaveChanges();
                                return RedirectToAction("Dashboard");
                            }
                        }
                    }
                    return View();
                }
               return new HttpNotFoundResult("Not Allowed");
                

            }
            return RedirectToAction("Login","Account");


        }
        [HttpGet]
        public ActionResult sendFeedback()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    return View();
                }
               return new HttpNotFoundResult("Not Allowed");
              

            }
            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public ActionResult Store()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    return View();
                }
               return new HttpNotFoundResult("Not Allowed");
             

            }

           return RedirectToAction("Login", "Account");

        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login","Account");
        }






    }
}