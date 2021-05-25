using System.Linq;
using System.Web.Mvc;
using ECommerce.Models;
using System.Web.Security;
using System.Web.Mvc.Filters;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Security.Principal;
using System;

namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (var modelContext = new modelContext())
            {
                bool isValid = modelContext.Users.Any(x => x.Email == user.Email && x.Password == user.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(user.UserNamee, false);
                    User auth_user = (from obj in modelContext.Users
                                    where obj.Email == user.Email && obj.Password == user.Password
                                    select obj).FirstOrDefault();
                    Session["id"] = auth_user.Id;
                    if(auth_user.Id.Equals("1") || auth_user.Id.Equals("2"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    return RedirectToAction("Dashboard", "Home");
                }
                ModelState.AddModelError("", "Invalid email and password");
                return View();
            }

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
                var count = (from u in modelContext.Users select u).Count() + 1;
                user.Id = count.ToString();
                user.virtualWallet.VirtualWallet_Id = user.Id;
                user.virtualWallet.Balance = 3000;
                modelContext.Users.Add(user);
                modelContext.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpPost]
        public ActionResult manageAccount(User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    using (var modelContext = new modelContext())
                    {
                        
                        modelContext.Entry(user).State = EntityState.Modified;
                        modelContext.SaveChanges();
                    }
                }
                return RedirectToAction("Logout","Home");
            }
            return RedirectToAction("Login");
        }
        [Authorize]
        [HttpGet]
        public ActionResult manageAccount(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var modelContext = new modelContext())
                {
                    User user = new User();
                    user = (from obj in modelContext.Users
                            where obj.Id == id
                            select obj).FirstOrDefault();

                    return View(user);
                }

            }
            return RedirectToAction("Login");
        }


    }
}