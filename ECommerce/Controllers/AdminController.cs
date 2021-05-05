using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
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
        public ActionResult viewUsers(modelContext model)
        {
            var data = DB.Users.ToList();
            DB.SaveChanges();
            return View(data); 
        }

        [HttpGet]
        public ActionResult viewItems()
        {
            
            var data = DB.Items.ToList();
            DB.SaveChanges();
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
            foreach (var item in DB.Items.ToList())
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
                    TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    offer.StartDate = offer.StartDate + ts;

                    foreach (var item in offer.Items)
                    {
                        if (item.SealedItem)
                        {
                            var sOffer = new Offer
                            {

                                Item_Id = item.Item_Id,
                                Offer_id= (DB.Offers.Count() + 1).ToString(),
                                StartDate=offer.StartDate,
                                EndDate=offer.EndDate,
                                Discount=offer.Discount
                                

                            };
                                DB.Offers.Add(sOffer);
                            DB.SaveChanges();
                        }
                    }
                   
                   

                }
            }
            return RedirectToAction("viewOffers");
        }
       




    }
}