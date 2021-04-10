using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        modelContext DB = new modelContext();
        // GET: Product
        public ActionResult Index()
        {
            List<Item> items = new List<Item>();
            items = (from obj in DB.Items
                     select obj).ToList();
            return View(items);
        }
        [HttpGet]
        public ActionResult getdetails(string id)
        {
            Item item = new Item();
            item = (from obj in DB.Items
                     where obj.Id == id
                     select obj).FirstOrDefault();
            return View(item);
        }
        [HttpPost]
        public ActionResult deleteItem(string id)
        {
            Item item = new Item();
            item = (from obj in DB.Items
                     where obj.Id == id
                     select obj).FirstOrDefault();
            DB.Items.Remove(item);
            DB.SaveChanges();
            return View();
        }
       
        [HttpPost]
        public ActionResult addItem(Item item)
        {


            if (item != null)
            {
                DB.Items.Add(item);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();


        }

    }
}