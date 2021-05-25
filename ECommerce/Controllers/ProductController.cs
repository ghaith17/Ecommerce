using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if (User.Identity.IsAuthenticated)
            {
                List<Item> items = new List<Item>();
                items = (from obj in DB.Items
                         select obj).ToList();
                return View(items);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult GetDetails(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Item item = new Item();
                item = (from obj in DB.Items
                        where obj.Item_Id == id
                        select obj).FirstOrDefault();
                return View(item);
            }
            return RedirectToAction("Login", "Account");
        }
        
        public ActionResult DeleteItem(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Item item = new Item();
                item = (from obj in DB.Items
                        where obj.Item_Id == id
                        select obj).FirstOrDefault();
                DB.Items.Remove(item);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult AddItem()
        {

            if (User.Identity.IsAuthenticated)
            {

                return View();
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        public ActionResult AddItem(Item item)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (item != null)
                {
                    DB.Items.Add(item);
                    DB.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("Index");
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public ActionResult UpdateItem(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Item item = new Item();
                item = (from obj in DB.Items
                        where obj.Item_Id == id
                        select obj).FirstOrDefault();

                DB.SaveChanges();
                return View(item);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult UpdateItem(Item item)
        {
            //Item Oitem = new Item();
            //Oitem = (from obj in DB.Items
            //        where obj.Item_Id == item.Item_Id
            //         select obj).FirstOrDefault();
            //Oitem.Name = item.Name;
            //Oitem.Price = item.Price;
            //Oitem.Quantity = item.Quantity;
            if (User.Identity.IsAuthenticated)
            {
                DB.Entry(item).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
        }

    }
}