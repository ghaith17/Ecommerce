using ECommerce.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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
        string connection_string = "Data Source=DESKTOP-TOKQDII;Initial Catalog = DB; User ID = Ghaith; Password=Yde079078@";
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
        public ActionResult MyBill(Order o)
        {
            o = order;


            return View(o);

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
                foreach (var item in orderq.Items)
                {
                    var update = (from obj in DB.Items
                                         where obj.Item_Id == item.Item_Id
                                         select obj).FirstOrDefault();
                    update.Quantity--;
                }
          
            }
            return RedirectToAction("MyBill", orderq);
           // return View(orderq.Bill);

        }
        [HttpGet]
        public ActionResult PlaceOrder()
        {
            return RedirectToAction("Checkouts");
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
            order.Id =( DB.Orders.Count()+2).ToString();

            order.createOrder(shoppingCart);

            if (order != null)
            {

                Bill bill = new Bill();
                bill.Id = (DB.Bills.Count() + 2).ToString();
                bill.generateBill(order);
                order.Bill = bill;


                DB.Bills.Add(order.Bill);
                DB.SaveChanges();
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                   
                    var a = String.Join(",", order.ItemsName);
                    string query = "Insert Into [DB].[dbo].[Orders] (Id, Bill_Id, Items)" +
                        "Values('" + order.Id + "','" + order.Bill.Id + "','" + a + "')";

                    SqlCommand command = new SqlCommand(query, connect);
                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return RedirectToAction("Checkouts");

        }
    
    }
}