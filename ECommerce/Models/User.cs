using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Models
{
    public class User: IValidatableObject
    {
        private string id;
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private string userName;
        [Required(ErrorMessage = "Required")]
        [StringLength(64, ErrorMessage = "UserName must not be more than 64 char")]
        public string UserNamee
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
        private string address;
        [Required(ErrorMessage = "Required")]
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var exist = false;
            Countries.Countries c = new Countries.Countries();
            foreach (var city in Countries.Countries.countries)
            {
                if (city.Value.Equals(Address))
                {
                    exist = true;
                    break;
                }
               
            }
            if(!exist)
            yield return new ValidationResult("outside jordan location");
        }
        private string email;
        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$" ,ErrorMessage = "Invalid Email Address")]
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        private string password;
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{8,}", ErrorMessage = "Your password must be at least 8 characters long and contain at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        private string phoneNum;
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNum
        {
            get { return this.phoneNum; }
            set { this.phoneNum = value; }
        }
        private string role ="Customer";
        public string Role
        {
            get { return this.role; }
            set { this.role = value; }
        }
        private string activation_code;
        public string Activation_code
        {
            get { return this.activation_code; }
            set { this.activation_code = value; }
        }
        private bool is_Active;
        public bool Is_Active
        {
            get { return this.is_Active; }
            set { this.is_Active = value; }
        }
        public User()
        {

        }
        public User(string id, string userName, string address, string email, string password)
        {
            this.id = id;
            this.userName = userName;
            this.address = address;
            this.email = email;
            this.password = password;
        }

       
        public virtual VirtualWallet virtualWallet { get; set; }
    }
        public static class CommonMethods
        {
            public static string Key = "adef@ekfxcove";
            public static string ConvertToEncrypt(string password)
            {
                if (string.IsNullOrEmpty(password)) return "";
                password += Key;
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordBytes);
            }
            public static string ConvertToDecrypt(string base64EncodeData)
            {
                if (string.IsNullOrEmpty(base64EncodeData)) return "";
                var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
                var result = Encoding.UTF8.GetString(base64EncodeBytes);
                result= result.Substring(0, result.Length - Key.Length);
                return result;
            }

        }
        



}