using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class DeliverySupplier
    {
        public DeliverySupplier() 
        {
        }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private string url;
        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }
        public void addToListOfOrders() { }
        public void updateOrderStatus() { }
    }


}