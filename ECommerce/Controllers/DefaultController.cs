using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using System.Web.Security;
using System.Web.Mvc.Filters;


namespace ECommerce.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            var modelContext = new modelContext();
            var data = modelContext.Items.ToList();
            int count = 0;
            foreach(var item in data)
            {
                count++;
                item.Id = count.ToString();
            }
                modelContext.SaveChanges();
                return View(data);

                /*var count = from a in modelContext.Items
                            select 
                    (from u in modelContext.Items select u).Count() + 1;
                modelContext.Items = count.ToString();
                modelContext.feedBacks.Add(feedBack);
                modelContext.SaveChanges();
                return RedirectToAction("Login");*/
            
            return View();
        }
    }
}