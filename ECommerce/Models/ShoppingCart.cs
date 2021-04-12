using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class ShoppingCart
    {
        private List<Item> ListOfITems;
        public ShoppingCart() { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public void addToShoppingCart(List<Item> items)
        {
            foreach (var item in items)
            {
                ListOfITems.Add(item);
            }
        }
        public void updateQuantity() { }
        public void removeItem() { }
    }
}
