using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Order
    {
        public Order()  { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        Bill bill = new Bill();
        public void createOrder() { }
        public void getShoppingCart() { }
        public void getOrderStatus() { }
    }

}