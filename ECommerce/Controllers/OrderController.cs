using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        modelContext DB = new modelContext();
        ShoppingCart shoppingCart = new ShoppingCart();
        List<Item> items = new List<Item>();

        // GET: Order
        public ActionResult Index()
        {
            List<Order> orders = new List<Order>();
            orders = (from obj in DB.Orders
                      select obj).ToList();
            return View(orders);
        }
        
        public ActionResult OrderDetails(string id)
        {
            Order order = new Order();
            order = (from obj in DB.Orders
                      where obj.Id == id
                      select obj).FirstOrDefault();
            return View(order);
        }
        public ActionResult DeleteFromOrder(string id)
        {
            Item item = new Item();
            item = (from obj in DB.Items
                    where obj.Id == id
                    select obj).FirstOrDefault();
            items.Remove(item);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult PlaceOrder()
        {
            
            items = (from obj in DB.Items
                     select obj).ToList();
            return View(items);
           // return View();
        }
        [HttpPost]
        public ActionResult PlaceOrder(List<Item> items)
        {
            shoppingCart.addToShoppingCart(items);
            Order order = new Order();
            order.createOrder(shoppingCart);
            DB.Orders.Add(order);
            DB.SaveChanges();
            return View("Index");
        }
       
    }
}