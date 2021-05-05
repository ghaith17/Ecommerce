using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class User
    {
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private string userName;
        public string UserNamee
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
        private string address;
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }
        private string email;
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        private string password;
        private string v;
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{5,}", ErrorMessage = "Your password must be at least 5 characters long and contain at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public User() //: base("name=User")
        {
            virtualWallet = new VirtualWallet();
        }
        public User(string id, string userName, string address, string email, string password)
        {
            this.id = id;
            this.userName = userName;
            this.address = address;
            this.email = email;
            this.password = password;
        }

        protected User(string v)
        {
            this.v = v;
        }
        public string VirtualWallet_Id { get; set; }
        [ForeignKey("VirtualWallet_Id")]
        public virtual VirtualWallet virtualWallet { get; set; }
        //string s = "select * from User where email ='" + email + "' and password ='" + password + "'";

        /* public abstract void signIn(string email, string password);
         public abstract void signOut(User user);

         public void manageAccount(User user)*/

    }
        



}