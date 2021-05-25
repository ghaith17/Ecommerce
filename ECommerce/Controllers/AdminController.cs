using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        string connection_string = "Data Source=DESKTOP-TOKQDII;Initial Catalog = DB; User ID = Ghaith; Password=Yde079078@";
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }
        private modelContext DB = new modelContext();

        public ActionResult viewUsers()
        {
            var data = (from User in DB.Users select User).ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult viewItems()
        {
            //  offer.StopOffer(offer);

            return View(DB.Items.ToList());
        }
        [HttpGet]
        public ActionResult viewOrders()
        {
            //  offer.StopOffer(offer);
        //    var items = DB.Orders.SingleOrDefault(b => b.Id.Equals(item.Name));
            List<Order> orders = new List<Order>();
            orders = DB.Orders.ToList();
            foreach( var order in orders)
            {
                order.Bill = DB.Bills.SingleOrDefault(b => b.Id.Equals(order.Bill.Id));
                using (SqlConnection connect = new SqlConnection(connection_string))
                {

                   // var a = String.Join(",", order.ItemsName);
                    string query = "Select Items From [DB].[dbo].[Orders] where Id=@Order_Id;";

                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@Order_Id", order.Id);
                    connect.Open();
                    //command.ExecuteNonQuery();
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
        [HttpGet]
        public ActionResult viewUsers(modelContext model)
        {
            var data = DB.Users.ToList();
            
            return View(data); 
        }


        [HttpGet]
        public ActionResult viewFeedback()
        {
          //  offer.StopOffer(offer);
            var data = (from FeedBack in DB.feedBacks select FeedBack).ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult viewOffers()
        {
            var data = DB.Offers.ToList();
            DB.SaveChanges();
            return View(data);
        }
        [HttpGet]
        public ActionResult manageOffer()
        {

            Offer offer = new Offer();
            var items = DB.Items.ToList();
            foreach (var item in items)
            {
                
                offer.Items.Add(item);
            }

            
            return View(offer);
        }
        [HttpPost]
        public ActionResult manageOffer(Offer offer)
        {
            //<<1.0>> add offer to db
            //<<2.0>>  make timeChecker that check  on Start_Date if equal to now of all DB.offers => call start_Offer of this offer.
            //<<3.0>>  make timeChecker that check  on End_Date if equal to now of all DB.offers => call Stop_Offer of this offer.
          if (ModelState.IsValid)
            {
                if (offer != null)
                {
                    //  offer.StartDate.AddHours(DateTime.Now.Hour);
                    //TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    //offer.StartDate = offer.StartDate + ts;
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
                //    offer.StartOffer();
                      
                   
                   

                }
            }
            return RedirectToAction("viewOffers");
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
                return RedirectToAction("viewItems");
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
                    item.Item_Id = (DB.Items.Count() + 1).ToString();
                    item.DefualtPrice = item.Price;
                    DB.Items.Add(item);
                    DB.SaveChanges();
                    return RedirectToAction("viewItems");
                }
               
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
                return RedirectToAction("viewItems");
            }
            return RedirectToAction("Login", "Account");
        }





    }
}