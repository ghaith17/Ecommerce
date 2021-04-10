using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        protected Admin admin;
        protected Customer customer;
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public void manageAccount(User user, string id, string userName, string address, string email, string password)
        {
            if (user is Admin)
            {
                admin.manageAccount((Admin)user);
            }
            if (user is Customer)
            {
                customer.manageAccount((Customer)user);
            }
        }
    }
}