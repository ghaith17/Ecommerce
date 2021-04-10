using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Item
    {
        public Item()  { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private int quantity;
        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private double price;
        public double Price
        {
            get { return this.price; }
            set { this.price = value; }
        }


    }
}