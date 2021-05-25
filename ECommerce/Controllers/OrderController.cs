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
        // static ShoppingCart shoppingCart = new ShoppingCart();
        // static List<Item> items = new List<Item>();
        static Order CHorder = new Order();
      
        // GET: Order
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated )
            {
                if (Session["Role"] !=null)
                {
                    List<Order> orders = new List<Order>();
                    orders = (from obj in DB.Orders
                              select obj).ToList();
                    return View(orders);
                }
               return new HttpNotFoundResult("Not Allowed");
              
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult OrderDetails(string id)
        {
            if (User.Identity.IsAuthenticated )
            {  if (Session["Role"] !=null)
                {
                    Order order = new Order();
                    order = (from obj in DB.Orders
                             where obj.Id == id
                             select obj).FirstOrDefault();
                    return View(order);
                }
               return new HttpNotFoundResult("Not Allowed");
        
            }
            return RedirectToAction("Login", "Account");

        }
    
        [HttpGet]
        public ActionResult MyBill(Order o)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    return View(CHorder);
                }
               return new HttpNotFoundResult("Not Allowed");
              
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public ActionResult Checkouts()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {



                    return View(CHorder);
                }
               return new HttpNotFoundResult("Not Allowed");
      
            }
            return RedirectToAction("Login", "Account");


        }
        [HttpPost]
        public ActionResult Checkouts(Order order)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    
                    string id = Session["id"].ToString();
                    var user = DB.Users.SingleOrDefault(b => b.Id.Equals(id));
                    if (order != null)
                        {

                            Bill bill = new Bill();
                            bill.Id = (DB.Bills.Count() + 2).ToString();
                            bill.generateBill(CHorder);
                            order.Bill = bill;


                            DB.Bills.Add(order.Bill);
                            user.virtualWallet.pay(Double.Parse(order.Bill.Value));
                            DB.SaveChanges();
                            using (SqlConnection connect = new SqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                            {

                                var a = String.Join(",", CHorder.ItemsName);
                                string query = "Insert Into [DBVirtualStore].[dbo].[Orders] (Id, Bill_Id, Items)" +
                                    "Values('" + order.Id + "','" + order.Bill.Id + "','" + a + "')";

                                SqlCommand command = new SqlCommand(query, connect);
                                connect.Open();
                                command.ExecuteNonQuery();
                           
                            }
                       
                         CHorder.Bill = order.Bill;
                            return RedirectToAction("MyBill", order);

                        }

                        RedirectToAction("Store", "Home");

                }
               return new HttpNotFoundResult("Not Allowed");
            
            }
            return RedirectToAction("Login", "Account");



        }
        [HttpGet]
        public ActionResult PlaceOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    return RedirectToAction("Checkouts");
                }
               return new HttpNotFoundResult("Not Allowed");
            
            }
          
           return RedirectToAction("Login", "Account");


    }
    [HttpPost]
        public ActionResult PlaceOrder(List<Item> items)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] == null)
                {
                    foreach (var item in items)
                    {
                        var result = DB.Items.SingleOrDefault(b => b.Name.Equals(item.Name));
                        result.Quantity -= item.Quantity;
                        item.Item_Id = result.Item_Id;
                        DB.SaveChanges();
                    }
                    ShoppingCart shoppingCart = new ShoppingCart();

                    shoppingCart.Id = (DB.ShoppingCarts.Count() + 1).ToString();
                    shoppingCart.addToShoppingCart(items);
                    Order order = new Order();
                    order.Id = (DB.Orders.Count() + 1).ToString();

                    order.createOrder(shoppingCart);
                   
                    CHorder = order;
                    return RedirectToAction("Checkouts");
                   
                }
               return new HttpNotFoundResult("Not Allowed");
    
            }

            return RedirectToAction("Login", "Account");


        }

    }
}