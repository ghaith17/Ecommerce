using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Order
    {

        private ShoppingCart shoppingCart;
        public Order()  { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        Bill bill = new Bill();
        public void createOrder(ShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
            Item item = new Item();
            foreach( var i in this.shoppingCart.ListOfITems)
            {
                item = (from obj in DB.Items
                         where obj.Id == item.Id
                         select obj).FirstOrDefault();
            }
        }
        public void getShoppingCart() { }
        public void getOrderStatus() { }
    }

}