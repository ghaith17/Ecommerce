using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Offer
    {
        public Offer() { }
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }
    }
}