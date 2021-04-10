using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class LoginController : Controller
    {
        protected Admin admin;
        protected Customer customer;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public void signUp(string id, string userName, string address, string email, string password, string phoneNum)
        {
            customer.signUp(id, userName, address, email, password, phoneNum);
        }
        public void signIn(User user)
        {
            if (user is Admin)
            {
                admin.signIn((Admin)user);
            }
            if (user is Customer)
            {
                customer.signIn((Customer)user);
            }
        }
        public void signOut(User user)
        {
            if (user is Admin)
            {
                admin.signOut((Admin)user);
            }
            if (user is Customer)
            {
                customer.signOut((Customer)user);
            }
        }
    }
}