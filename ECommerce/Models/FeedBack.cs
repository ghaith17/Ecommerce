using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class FeedBack 
    {
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        private string content;
        [Required(ErrorMessage = "Required")]
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        private string userName;
        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
    }
}