using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Order
    {

        private ShoppingCart shoppingCart = new ShoppingCart();
        public Order()  { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        Bill bill = new Bill();
        public Bill Bill
        {
            get { return this.bill; }
            set { this.bill = value; }
        }
        public void createOrder(ShoppingCart shoppingCart)
        {
            //this.shoppingCart = shoppingCart;
            //Item item = new Item();
            //foreach (var i in this.shoppingCart.ListOfITems)
            //{
            //    item = (from obj in DB.Items
            //            where obj.Id == i.Id
            //            select obj).FirstOrDefault();
            //}
            this.shoppingCart.Id = shoppingCart.Id;
            foreach (var item in shoppingCart.ListOfITems)
            {
                this.shoppingCart.ListOfITems.Add(item);
            }
            
        }
        public ShoppingCart getShoppingCart() {
            return shoppingCart;
        }
        public void getOrderStatus() { }

    }

}