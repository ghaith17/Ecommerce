using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Item
    {
        public Item()  { }
        private string item_id;
        [Key]
        public string Item_Id
        {
            get { return this.item_id; }
            set { this.item_id = value; }
        }
        private int quantity;
        [Range(1, 50)]
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
        private string price;
        public string Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        private bool sealedItem ;
        public bool SealedItem
        {
            get { return this.sealedItem; }
            set { this.sealedItem = value; }
        }



    }
}