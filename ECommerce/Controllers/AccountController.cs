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
using static ECommerce.Models.User;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using System.IO;
using ECommerce.Utility;

namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Login()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                using (var modelContext = new modelContext())
                {
                    Password = CommonMethods.ConvertToEncrypt(Password);
                    var auth_user = (from s in modelContext.Users where ((s.Email == Email) && (s.Password == Password)) select s).FirstOrDefault();
                    if (auth_user != null)
                    {
                        if (auth_user.Is_Active)
                        {
                            FormsAuthentication.SetAuthCookie(auth_user.UserNamee, false);

                            Session["id"] = auth_user.Id;
                            if (!String.IsNullOrEmpty(auth_user.Role) && auth_user.Role.Equals("Admin"))
                            {
                                Session["Role"] = auth_user.Role;
                                return RedirectToAction("Dashboard", "Admin");

                            }
                            return RedirectToAction("Dashboard", "Home");
                        }
                        ModelState.AddModelError("", "Activate your email please.");
                        return View();
                    }
                    ModelState.AddModelError("", "Invalid email and/or password");
                    return View();
                }
               
            }
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public  async Task<ActionResult> Signup(User user)
        {
            if (ModelState.IsValid)
            {
                using (var modelContext = new modelContext())
                {
                    var chkUser = (from s in modelContext.Users where s.Email == user.Email select s).FirstOrDefault();
                    if (chkUser == null)
                    {

                        
                            var count = (from u in modelContext.Users select u).Count() + 1;
                            user.Id = count.ToString();
                            user.virtualWallet.VirtualWallet_Id = user.Id;
                            //user.virtualWallet.Balance = 3000;
                            user.Password = CommonMethods.ConvertToEncrypt(user.Password);
                            Guid code = Guid.NewGuid();
                            user.Activation_code = code.ToString();
                            modelContext.Users.Add(user);
                            modelContext.SaveChanges();
                           
                            var callbackUrl = Url.Action("Activation", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            string body = string.Empty;
                            using (StreamReader reader = new StreamReader(Server.MapPath("~/MailTemplate/AccountConfirmation.html")))
                            {
                                body = reader.ReadToEnd();
                            }
                            body = body.Replace("{ConfirmationLink}", callbackUrl);
                            body = body.Replace("{UserName}", user.UserNamee);
                            bool IsSendEmail = SendEmail.EmailSend(user.Email, "Confirm your account", body, true);
                            if (IsSendEmail)
                            {
                            ModelState.AddModelError("", "Activation Link Send to to Your Email.");
                            return View("Login");
                            }
                        
                      
                        return RedirectToAction("Login");

                    }
                    ModelState.AddModelError("", "Email already registerd");
                    return View();
                }
            }

            return View();

        }
        public ActionResult Activation(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return new HttpNotFoundResult("Not Allowed");
            }
            
                using (var modelContext = new modelContext())
                {
                    User userActivation = modelContext.Users.Where(p => p.Activation_code == code &&  p.Id == userId).FirstOrDefault();
                    if (userActivation != null)
                    {
                        userActivation.Is_Active = true;
                        modelContext.Entry(userActivation).State = EntityState.Modified;
                        modelContext.SaveChanges();
                       ModelState.AddModelError("","Activation successful.");
                      // ViewBag.Message = "Activation successful.";
                    }
                }
            

            return View("Login");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult manageAccount(User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    using (var modelContext = new modelContext())
                    {
                        user.Password = CommonMethods.ConvertToEncrypt(user.Password);
                       
                        modelContext.Entry(user).State = EntityState.Modified;
                        modelContext.SaveChanges();
                    }
                    return RedirectToAction("Logout", "Home");
                }
                return View();
            }
            return RedirectToAction("Login");
        }
        [Authorize]
        [HttpGet]
        public ActionResult manageAccount()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var modelContext = new modelContext())
                {
                    string id = Session["Id"].ToString();
                    User user = new User();
                    user = (from obj in modelContext.Users
                            where obj.Id == id
                            select obj).FirstOrDefault();
                    user.Password = CommonMethods.ConvertToDecrypt(user.Password);

                    return View(user);
                }

            }
            return RedirectToAction("Login");
        }


    }
}