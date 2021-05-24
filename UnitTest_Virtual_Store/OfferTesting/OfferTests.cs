using System;
using ECommerce.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.Entity.Validation;

namespace UnitTest_Virtual_Store
{
    [TestClass]
    public class OfferTests
    {
        modelContext DB = new modelContext();
      
        [TestMethod]
        public void StartDate1()
        {
            Offer offer = new Offer()
            {
            
                Offer_id = (DB.Offers .Count() + 1).ToString()
            };
            var date = new DateTime(2016, 6, 28);

            var time = new DateTime(1, 1, 1, 13, 13, 13);

            var combinedDateTime = date.AddTicks(time.TimeOfDay.Ticks);
            offer.EndDate = combinedDateTime;
            offer.StartDate = combinedDateTime;
            DB.Offers.Add(offer);
            try
            {
                DB.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {

                        Assert.AreEqual("EndDate must be greater than StartDate", ve.ErrorMessage);
                    }
                }

            }
        }
        [TestMethod]
        public void StartDate2()
        {
            Offer offer = new Offer()
            {
                Offer_id = (DB.Offers.Count() + 1).ToString()
            };
            var date = new DateTime(2016, 6, 28);

            var time = new DateTime(1, 1, 1, 13, 13, 13);

            var combinedDateTime = date.AddTicks(time.TimeOfDay.Ticks);
            offer.EndDate = combinedDateTime;
            var result = "The start date is required";
            
            DB.Offers.Add(offer);
            try
            {
                DB.SaveChanges();
                result = null;
            }
            catch (Exception e)
            {
                if (result != null)
                {
                    Assert.AreEqual("The start date is required", result);
                }
            }
        }
        [TestMethod]
        public void EndtDate1()
        {
            Offer offer = new Offer()
            {
                Offer_id = (DB.Offers.Count() + 1).ToString()
            };
            var date = new DateTime(2016, 6, 28);

            var time = new DateTime(1, 1, 1, 13, 13, 13);

            var combinedDateTime = date.AddTicks(time.TimeOfDay.Ticks);
            offer.StartDate = combinedDateTime;
            var result = "The end date is required";

            DB.Offers.Add(offer);
            try
            {
                DB.SaveChanges();
                result = null;
            }
            catch (Exception e)
            {
                if (result != null)
                {
                    Assert.AreEqual("The end date is required", result);
                }
            }
        }
        [TestMethod]
        public void EndtDateStartDate()
        {
            Offer offer = new Offer()
            {
                Offer_id = (DB.Offers.Count() + 1).ToString()
            };
            var date = new DateTime(2016, 6, 28);

            var time = new DateTime(1, 1, 1, 13, 13, 13);

            var combinedDateTime = date.AddTicks(time.TimeOfDay.Ticks);
            offer.StartDate = combinedDateTime;
            offer.EndDate = DateTime.Now;
            var result = false;

            DB.Offers.Add(offer);
            DB.SaveChanges();
            result = true; // if there is exception then this line will not running
            Assert.AreEqual(true, result);
              
        }
    }
}
