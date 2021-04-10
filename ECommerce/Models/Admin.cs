using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Admin : User
    {
        //public Admin() : base() { }
        public Admin() : base("name=Admin")
        {

        }
        public Admin(string id, string userName, string address, string email, string password)
        : base(id, userName, address, email, password) { }

        public void createOrder() { }
        public void viewUser() { }
        public void viewOrders() { }
        public void addItem() { }
        public void updateItem() { }

        public override void manageAccount(User user)
        {
            //string id, string userName, string address, string email, string password
            throw new NotImplementedException();
        }

        public override void signIn(User user)
        {
            throw new NotImplementedException();
        }

        public override void signOut(User user)
        {
            throw new NotImplementedException();
        }


    }

}