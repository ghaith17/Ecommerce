using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class VirtualWallet
    {

        public VirtualWallet()  { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private double balance;
        public double Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }
        public void pay() { }
    }
}