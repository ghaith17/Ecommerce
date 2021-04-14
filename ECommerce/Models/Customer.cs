using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Customer : User
    {
        private string phoneNum;
        public string PhoneNum
        {
            get { return this.phoneNum; }
            set { this.phoneNum = value; }
        }
        public Customer() : base("name=Customer")
        {
        }
        //public Customer() : base() { }
        public Customer(string id, string userName, string address, string email, string password)
        : base(id, userName, address, email, password) { }
        ShoppingCart shoppingCart = new ShoppingCart();
        VirtualWallet virtualWallet = new VirtualWallet();


        public void signUp(string id, string userName, string address, string email, string password, string phoneNum)
        {

        }
        public void signIn(string email, string password) { }
 
        public void signOut(User user)
        {

        }
        public void manageAccount(User user)
        {

        }


        public void createOrder()
        {
            Order order = new Order();
            

        }
        public void viewUser()
        {

        }
        public void viewOrders()
        {

        }
        public void addItem()
        {

        }
        public void updateItem()
        {

        }
    }

}