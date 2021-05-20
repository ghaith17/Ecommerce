using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Order
    {

        
      
        public Order()  {
            ItemsName = new List<string>() ;
        }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        Bill bill = new Bill();
        public virtual Bill Bill
        {
            get { return this.bill; }
            set { this.bill = value; }
        }
        List<Item> items = new List<Item>();

        internal List<string> _ItemsName { get; set; }

        [NotMapped]
        public List<string> ItemsName
        { 
            get { return _ItemsName ; }
            set { _ItemsName = value; }
        }

        public  List<Item> Items { get => items; set => items = value; }

        public void createOrder(ShoppingCart shoppingCart)
        {

            foreach (var item in shoppingCart.ListOfITems)
            {
                this.ItemsName.Add(item.Name);
                this.Items.Add(item);
            }

        }


    }

}