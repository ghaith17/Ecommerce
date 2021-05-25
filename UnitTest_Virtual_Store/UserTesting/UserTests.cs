using System;
using System.Data.Entity.Validation;
using ECommerce.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace UnitTest_Virtual_Store.UserTesting
{
    [TestClass]
    public class UserTests
    {
        modelContext DB = new modelContext();
        string result = "failed";
        [TestMethod]
        public void UserName1()
        {
            var user = new User()
            {
                UserNamee = null,
                Password = "T#1Q0@559)",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
    
            DB.Users.Add(user);
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
        public void UserName2()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "rEuMBxFiIG'EXyibBrNOpu_iEfplZqXDBeazEXEoNaybfOfqVJFaVdoVBIfNnTGgg",
                Password = "T#1Q0@559)",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);

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

                        Assert.AreEqual("UserName must not be more than 64 char", ve.ErrorMessage);
                    }
                }
           
            }

        }
        [TestMethod]
        public void UserName3()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "T#1Q0@559)",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";     
            Assert.AreEqual("pass", result);

        }
        [TestMethod]
        public void Email1()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "T#1Q0@559)",
                Email = null,
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
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
        public void Email2()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "T#1Q0@559)",
                Email = "LRSxDLktGFiunXgzkrxNNJUbXPJeiqRdezfqoYGIcKoyHlvRLg",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
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

                        Assert.AreEqual("Invalid Email Address", ve.ErrorMessage);
                    }
                }

            }

        }
        [TestMethod]
        public void Email3()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "T#1Q0@559)",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";
            Assert.AreEqual("pass", result);

        }
        [TestMethod]
        public void Password1()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = null,
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
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
        public void Password2()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "12345678",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
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

                        Assert.AreEqual("Your password must be at least 8 characters long and contain at least 1 letter and 1 number", ve.ErrorMessage);
                    }
                }

            }
        }
        [TestMethod]
        public void Password3()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "Yde079078@",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";
            Assert.AreEqual("pass", result);
        }
        [TestMethod]
        public void PhoneNum1()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "Yde079078@",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = null,
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
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
        public void PhoneNum2()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "Yde079078@",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "123420",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
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

                        Assert.AreEqual("Not a valid phone number", ve.ErrorMessage);
                    }
                }

            }
        }
        [TestMethod]
        public void PhoneNum3()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "Yde079078@",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";
            Assert.AreEqual("pass", result);
        }
        [TestMethod]
        public void Address1()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "Yde079078@",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = null,
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
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
        public void Address2()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "Yde079078@",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "mosco",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
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

                        Assert.AreEqual("outside jordan location", ve.ErrorMessage);
                    }
                }

            }
        }
        [TestMethod]
        public void Address3()
        {
            var user = new ECommerce.Models.User()
            {
                UserNamee = "Tareq",
                Password = "Yde079078@",
                Email = "TareqAbuQamar@gmail.com",
                PhoneNum = "0778497757",
                Address = "Irbid",
                Id = ((from u in DB.Users select u).Count() + 1).ToString()

            };
            DB.Users.Add(user);
            DB.Users.Add(user);
            DB.SaveChanges();    // pass otherwise i will get exception and testcase will be fail
            result = "pass";
            Assert.AreEqual("pass", result);
        }
    }
}
