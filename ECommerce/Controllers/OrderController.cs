using ECommerce.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        modelContext DB = new modelContext();
       static ShoppingCart shoppingCart = new ShoppingCart();
        static List<Item> items = new List<Item>();
        static Order order = new Order();

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
                    where obj.Item_Id == id
                    select obj).FirstOrDefault();
            items.Remove(item);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult MyBill(Bill bill)
        {



            return View(bill);

        }
        [HttpGet]
        public ActionResult Checkouts()
        {



            return View(order);
            
        }
        [HttpPost]
        public ActionResult Checkouts(Order orderq)
        {
            orderq = order;
            
            if (Session["id"] != null)
            {
                

                string id = Session["id"].ToString();
                var user = DB.Users.SingleOrDefault(b => b.Id.Equals(id));
               
                user.virtualWallet.pay(Double.Parse(orderq.Bill.Value));
                foreach (var item in orderq.getShoppingCart().ListOfITems)
                {
                    var update = DB.Items.SingleOrDefault(b => b.Item_Id.Equals(item.Item_Id));
                    update.Quantity -= item.Quantity;
                }
          
            }
            return RedirectToAction("MyBill", orderq.Bill);
           // return View(orderq.Bill);

        }
        [HttpPost]
        public ActionResult PlaceOrder(List<Item> items)
        {

            foreach (var item in items)
            {
                var result = DB.Items.SingleOrDefault(b => b.Name.Equals(item.Name));
                item.Item_Id = result.Item_Id;
            }
            shoppingCart.Id = (DB.ShoppingCarts.Count() +1 ).ToString();
            shoppingCart.addToShoppingCart(items);
            order.Id =( DB.Orders.Count()+1).ToString();

            order.createOrder(shoppingCart);
          //  var modelContext = new modelContext();
            if (order != null)
            {
                // try
                // {
                Bill bill = new Bill();
                bill.Id = (DB.Bills.Count() + 1).ToString();
                bill.generateBill(order);
                order.Bill = bill;
               // DB.ShoppingCarts.Add(shoppingCart);
                DB.Orders.Add(order);
                DB.SaveChanges();
              //  }
                //catch (DbEntityValidationException ex)
                //{
                //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                //    {
                //        foreach (var validationError in entityValidationErrors.ValidationErrors)
                //        {
                //            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                //        }
                //    }


                //}
             
                
            }
            return RedirectToAction("Checkouts");

        }
    
    }
}