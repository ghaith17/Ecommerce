using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class VirtualWallet
    {

        public VirtualWallet()  { }
        private string virtualWallet_id;
        [Key]
        public string VirtualWallet_Id
        {
            get { return this.virtualWallet_id; }
            set { this.virtualWallet_id = value; }
        }
        private double balance;
        public double Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }
        public void pay(double price) 
        {
            this.balance -= price;
        }
    }
}