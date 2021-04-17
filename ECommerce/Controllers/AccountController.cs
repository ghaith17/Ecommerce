using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using System.Web.Security;



namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        modelContext DB = new modelContext();
        
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public ActionResult Login(User user)
        {
            bool isValid = DB.Users.Any(x => x.Email == user.Email && x.Password == user.Password);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(user.UserNamee, false);
                return RedirectToAction("Index", "Users");
            }
                ModelState.AddModelError("","Invalid email and password");
                return View();
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            using (var modelContext = new modelContext())
            {
                modelContext.Users.Add(user);
                modelContext.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult manageAccount()
        {

            return View();
        }

        [HttpPost]
        public ActionResult manageAccount(User newUser)
        {
            User oldUser = new User();
            oldUser = (from obj in DB.Users
                       where obj.Id == newUser.Id
                       select obj).FirstOrDefault();
            
            newUser.Id = oldUser.Id;
            newUser.UserNamee = oldUser.UserNamee;
            newUser.Address = oldUser.Address;
            newUser.Email = oldUser.Email;
            newUser.Password = oldUser.Password;
            
            DB.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult manageAccount(string id)
        {
            User oldUser = new User();
            oldUser = (from obj in DB.Users
                       where obj.Id == id
                       select obj).FirstOrDefault();
            DB.SaveChanges();
            return View(oldUser);
            
            
        }

        [HttpPost]
        public ActionResult sendFeedback(FeedBack feedBack)
        {

            if (feedBack != null)
            {
                DB.feedBacks.Add(feedBack);
                DB.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();

        }
        [HttpGet]
        public ActionResult sendFeedback()
        {

            return View();

        }
    }
}