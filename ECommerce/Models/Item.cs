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
        public Item()
        {
            //offer = new Offer();
        }
        private string item_id;
        [Key]
        public string Item_Id
        {
            get { return this.item_id; }
            set { this.item_id = value; }
        }
        private int quantity;
        [Range(1, 50, ErrorMessage= "minimum 1 , maximum 50")]
        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }
        private string name;
        [Required(ErrorMessage = "Required")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private string description;
        [Required(ErrorMessage = "Required")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        private double price;
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
        public double Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        private double defualtPrice;
        public double DefualtPrice
        {
            get { return this.defualtPrice; }
            set { this.defualtPrice = value; }
        }
        private bool selectedItem;
        public bool SelectedItem
        {
            get { return this.selectedItem; }
            set { this.selectedItem = value; }
        }

        public virtual Offer? offer { get; set; }



    }
}