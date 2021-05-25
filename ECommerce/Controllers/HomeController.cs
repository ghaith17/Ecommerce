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
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult sendFeedback(FeedBack feedBack)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var modelContext = new modelContext())
                {
                    if (feedBack != null)
                    {
                        feedBack.Id = (modelContext.feedBacks.Count() + 1).ToString();
                        string id = Session["id"].ToString();
                        var user = modelContext.Users.SingleOrDefault(b => b.Id.Equals(id));
                        feedBack.UserName = user.UserNamee;
                        modelContext.feedBacks.Add(feedBack);
                        modelContext.SaveChanges();
                        return RedirectToAction("Dashboard");
                    }
                }     
            }
            return RedirectToAction("Login","Account");


        }
        [HttpGet]
        public ActionResult sendFeedback()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public ActionResult Store()
        {
          //  if (User.Identity.IsAuthenticated)
            {
                var modelContext = new modelContext();
                return View(modelContext.Items.ToList());
            }
          //  return RedirectToAction("Login", "Account");

        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login","Account");
        }






    }
}