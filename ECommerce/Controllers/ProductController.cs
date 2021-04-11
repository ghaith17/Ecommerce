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
        public ActionResult GetDetails(string id)
        {
            Item item = new Item();
            item = (from obj in DB.Items
                     where obj.Id == id
                     select obj).FirstOrDefault();
            return View(item);
        }
        
        public ActionResult DeleteItem(string id)
        {
            Item item = new Item();
            item = (from obj in DB.Items
                     where obj.Id == id
                     select obj).FirstOrDefault();
            DB.Items.Remove(item);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AddItem()
        {


            
            return View();

        }
        [HttpPost]
        public ActionResult AddItem(Item item)
        {


            if (item != null)
            {
                DB.Items.Add(item);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");


        }
        [HttpGet]
        public ActionResult UpdateItem(string id)
        {
            Item item = new Item();
            item = (from obj in DB.Items
                     where obj.Id ==id
                     select obj).FirstOrDefault();
           
            DB.SaveChanges();
            return View(item);
        }
        [HttpPost]
        public ActionResult UpdateItem(Item item)
        {
            Item Oitem = new Item();
            Oitem = (from obj in DB.Items
                    where obj.Id == item.Id
                     select obj).FirstOrDefault();
            Oitem.Name = item.Name;
            Oitem.Price = item.Price;
            Oitem.Quantity = item.Quantity;
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}