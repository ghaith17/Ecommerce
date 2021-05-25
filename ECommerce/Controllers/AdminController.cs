using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Utility;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
       // string connection_string = "Data Source=DESKTOP-TOKQDII;Initial Catalog = DB; User ID = Ghaith; Password=Yde079078@";
        // GET: Admin
        public ActionResult Dashboard()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    return View();
                }
               return new HttpNotFoundResult("Not Allowed");
               
            }
            return RedirectToAction("Login", "Account");
        }
        private modelContext DB = new modelContext();

        public ActionResult viewUsers()
        {

            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    var data = (from User in DB.Users select User).ToList();
                    return View(data);
                }
               return new HttpNotFoundResult("Not Allowed");
             
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult viewItems()
        {

            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    return View(DB.Items.ToList());
                }
               return new HttpNotFoundResult("Not Allowed");
          
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult viewOrders()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    List<Order> orders = new List<Order>();
                    orders = DB.Orders.ToList();
                    foreach (var order in orders)
                    {
                        order.Bill = DB.Bills.SingleOrDefault(b => b.Id.Equals(order.Bill.Id));
                        using (SqlConnection connect = new SqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                        {

                            string query = "Select Items From [DBVirtualStore].[dbo].[Orders] where Id=@Order_Id;";

                            SqlCommand command = new SqlCommand(query, connect);
                            command.Parameters.AddWithValue("@Order_Id", order.Id);
                            connect.Open();
                            
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string o = reader["Items"].ToString();
                                    order.ItemsName = o.Split(new string[] { "," }, StringSplitOptions.None).ToList();

                                }

                            }
                        }
                    }
                    return View(DB.Orders.ToList());
                }
               return new HttpNotFoundResult("Not Allowed");
              
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult viewUsers(modelContext model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    var data = DB.Users.ToList();
                    return View(data);
                }
               return new HttpNotFoundResult("Not Allowed");
               
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public ActionResult viewFeedback()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    var data = (from FeedBack in DB.feedBacks select FeedBack).ToList();
                    return View(data);
                }
               return new HttpNotFoundResult("Not Allowed");
            
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult viewOffers()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    var data = DB.Offers.ToList();
                    DB.SaveChanges();
                    return View(data);
                }
               return new HttpNotFoundResult("Not Allowed");
               
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult manageOffer()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    
                        Offer offer = new Offer();
                        var items = DB.Items.ToList();
                        foreach (var item in items)
                        {

                            offer.Items.Add(item);
                        }


                        return View(offer);
                 
                }
               return new HttpNotFoundResult("Not Allowed");
               
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult manageOffer(Offer offer)
        {
            //<<1.0>> add offer to db
            //<<2.0>>   timeChecker that check  on Start_Date if equal to now of all DB.offers => call start_Offer of this offer.
            //<<3.0>>   timeChecker that check  on End_Date if equal to now of all DB.offers => call Stop_Offer of this offer.
            if (User.Identity.IsAuthenticated )
            {
                if (Session["Role"] !=null) 
                {
                    if (ModelState.IsValid)
                    {
                        if (offer != null)
                        {

                            offer.Offer_id = (DB.Offers.Count() + 1).ToString();
                            offer.Status = "Scheduled";
                            foreach (var item in offer.Items.ToList())
                            {
                                if (!item.SelectedItem)
                                {
                                    offer.Items.Remove(item);

                                }
                            }
                            foreach (var item in offer.Items.ToList())
                            {

                                DB.Entry<Item>(item).State = EntityState.Modified;
                                DB.SaveChanges();

                            }

                            DB.Offers.Add(offer);
                            DB.SaveChanges();

                            return RedirectToAction("viewOffers");

                        }
                    }
                    return View(offer);
                }
               return new HttpNotFoundResult("Not Allowed");
               
            }
            
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult GetDetails(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    Item item = new Item();
                    item = (from obj in DB.Items
                            where obj.Item_Id == id
                            select obj).FirstOrDefault();
                    return View(item);
                }
               return new HttpNotFoundResult("Not Allowed");
               
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult DeleteItem(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {

                    Item item = new Item();
                    item = (from obj in DB.Items
                            where obj.Item_Id == id
                            select obj).FirstOrDefault();
                    DB.Items.Remove(item);
                    DB.SaveChanges();
                    return RedirectToAction("viewItems");

                }
               return new HttpNotFoundResult("Not Allowed");
             
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult AddItem()
        {

            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {

                    return View();
                }
               return new HttpNotFoundResult("Not Allowed");
              
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        public ActionResult AddItem(Item item)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    if (ModelState.IsValid)
                    {
                        if (item != null)
                        {
                            item.Item_Id = (DB.Items.Count() + 1).ToString();
                            item.DefualtPrice = item.Price;
                            DB.Items.Add(item);
                            DB.SaveChanges();
                            return RedirectToAction("viewItems");
                        }
                    }
                    return View();

                }
               return new HttpNotFoundResult("Not Allowed");
              
            }
                return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult UpdateItem(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                {
                    Item item = new Item();
                    item = (from obj in DB.Items
                        where obj.Item_Id == id
                        select obj).FirstOrDefault();

                   DB.SaveChanges();
                   return View(item);
                }
               return new HttpNotFoundResult("Not Allowed");
              
            }
           
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult UpdateItem(Item item)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"] !=null)
                { 
                    if (ModelState.IsValid)
                    {
                        DB.Entry(item).State = EntityState.Modified;
                        DB.SaveChanges();
                        return RedirectToAction("viewItems");
                    }
                    return View();
                }
               return new HttpNotFoundResult("Not Allowed");
               
            }
            return RedirectToAction("Login", "Account");
        }
       





    }
}