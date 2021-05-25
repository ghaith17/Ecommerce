using System;
using ECommerce.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.Entity.Validation;

namespace UnitTest_Virtual_Store
{
    [TestClass]
    public class ItemTests
    {
        modelContext DB = new modelContext();
        string result = "failed";
        [TestMethod]
        public void Name1()
        {
            var item = new Item()
            {
                Name = null,
                Description = "good",
                Price = 250,
                Quantity = 20,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
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
        public void Name2()
        {
            var item = new Item()
            {
                Name = "HP",
                Description = "good",
                Price = 250,
                Quantity = 20,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";
            Assert.AreEqual("pass", result);
        }
        [TestMethod]
        public void Description1()
        {
            var item = new Item()
            {
                Name = "HP",
                Description = null,
                Price = 250,
                Quantity = 20,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
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
        public void Description2()
        {
            var item = new Item()
            {
                Name = "HP",
                Description = "Bad",
                Price = 250,
                Quantity = 20,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";
            Assert.AreEqual("pass", result);
        }
        [TestMethod]
        public void Price1()
        {
            var item = new Item()
            {
                Name = "Dell",
                Description = "good",
                Quantity = 20,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
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

                        Assert.AreEqual("Please enter a value greater than 1", ve.ErrorMessage);
                    }
                }

            }
        }
        [TestMethod]
        public void Price2()
        {
            var item = new Item()
            {
                Name = "Dell",
                Description = "good",
                Price=-250,
                Quantity = 20,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
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

                        Assert.AreEqual("Please enter a value greater than 1", ve.ErrorMessage);
                    }
                }

            }
        }
        [TestMethod]
        public void Price3()
        {
            var item = new Item()
            {
                Name = "HP",
                Description = "Bad",
                Price = 250,
                Quantity = 20,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";
            Assert.AreEqual("pass", result);
        }
        [TestMethod]
        public void Quntity1()
        {
            var item = new Item()
            {
                Name = "HP",
                Description = "Bad",
                Price = 250,
                Quantity=70,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
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

                        Assert.AreEqual("minimum 1 , maximum 50", ve.ErrorMessage);
                    }
                }

            }
        }
        [TestMethod]
        public void Quntity2()
        {
            var item = new Item()
            {
                Name = "HP",
                Description = "Bad",
                Price = 250,
                Quantity = 20,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";
            Assert.AreEqual("pass", result);
        }
        [TestMethod]
        public void Quntity3()
        {
            var item = new Item()
            {
                Name = "HP",
                Description = "Bad",
                Price = 250,
                Item_Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Items.Add(item);
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

                        Assert.AreEqual("minimum 1 , maximum 50", ve.ErrorMessage);
                    }
                }

            }
        }
    }
}
