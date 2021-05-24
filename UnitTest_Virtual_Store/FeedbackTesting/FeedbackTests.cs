using System;
using ECommerce.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.Entity.Validation;

namespace UnitTest_Virtual_Store
{
    [TestClass]
    public class FeedbackTests
    {
        modelContext DB = new modelContext();
        bool result = false;
        [TestMethod]
        public void Feedback1()
        {

            var feedback = new FeedBack()
            {
              
                Id = ((from u in DB.feedBacks select u).Count() + 1).ToString()

            };

            DB.feedBacks.Add(feedback);
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

                        Assert.AreEqual("Required", ve.ErrorMessage);
                    }
                }

            }
        }
        [TestMethod]
        public void Feedback2()
        {
            var feedback = new FeedBack()
            {

                Id = ((from u in DB.feedBacks select u).Count() + 1).ToString(),
                Content="hello"

            };

            DB.feedBacks.Add(feedback);
            DB.SaveChanges();
            result = true; /// if there is error then this line will not be running
            if(result)
            Assert.AreEqual(true, result);
             
        }
     
    }
}
