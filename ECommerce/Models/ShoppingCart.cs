using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class ShoppingCart
    {
        public ShoppingCart() { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public void addToShoppingCart() { }
        public void updateQuantity() { }
        public void removeItem() { }
    }
}
