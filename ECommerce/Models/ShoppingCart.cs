using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class ShoppingCart
    {
        private List<Item> listOfITems = new List<Item>();
        public ShoppingCart() { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        
        public virtual List<Item> ListOfITems { get => listOfITems; set => listOfITems = value; }
        public string Item_Id { get; set; }
        [ForeignKey("Item_Id")]
        public virtual Item item { get; set; }
        public void addToShoppingCart(List<Item> items)
        {

            ListOfITems = items;

        }
        

    }
}
