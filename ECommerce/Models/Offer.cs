using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Quartz;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Web.Mvc;
using ECommerce.Controllers;
using ECommerce.Utility;
using System.Web.Hosting;

namespace ECommerce.Models
{
    public class Offer : IJob, IValidatableObject
    {
        public Offer() { }
        private string id;
        [Key]
        public string Offer_id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private string status;
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        private DateTime startDate = new DateTime();
        [Required(ErrorMessage = "The start date is required")] //, ApplyFormatInEditMode = true
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }
        private DateTime endDate = new DateTime();
        [Required(ErrorMessage = "The end date is required")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult("EndDate must be greater than StartDate");
            }
         
        }
        private decimal discount;
        [Required(ErrorMessage = "Required")]
        [DisplayName("Discount ")]
        public decimal Discount {
            get { return this.discount; }
            set { this.discount = value; }
        }
        public virtual List<Item> Items { get => items; set => items = value; }
        private List<Item> items = new List<Item>();



       

        public void StopOffer()
        {
            using (modelContext DB = new modelContext())
            {
                var offer = DB.Offers.SingleOrDefault(b => b.Offer_id.Equals(this.Offer_id));

                foreach (var item in this.Items)
                {

                    var result = DB.Items.SingleOrDefault(b => b.Item_Id.Equals(item.Item_Id));

                    if (result != null)
                    {
                        if (result.SelectedItem)
                        {
                            result.Price = result.Price + (result.DefualtPrice * Double.Parse(this.Discount.ToString()));


                            result.SelectedItem = false;
                            DB.Set<Item>().AddOrUpdate(result);
                        }

                        offer.Items.Remove(result);
                        DB.SaveChanges();
                    }

                }
                offer.Status = "Expired";
                DB.Set<Offer>().AddOrUpdate(offer);
                DB.SaveChanges();


            }
        }
    

        public void StartOffer()
        {
            using (modelContext DB = new modelContext())
            {
                var offer = DB.Offers.SingleOrDefault(b => b.Offer_id.Equals(this.Offer_id));
                foreach (var item in this.Items)
                {

                    var result = DB.Items.SingleOrDefault(b => b.Item_Id.Equals(item.Item_Id));
                    if (result != null)
                    {
                        if (item.SelectedItem)
                        {
                            result.Price = result.Price - (result.Price * Double.Parse(this.Discount.ToString()));

                            DB.Set<Item>().AddOrUpdate(result);
                        }


                        DB.SaveChanges();
                    }

                }
                offer.Status = "Active";
                DB.Set<Offer>().AddOrUpdate(offer);
                DB.SaveChanges();


            }
        }
        public void Notify_Users()
        {
            using (var modelContext = new modelContext())
            {
                foreach (var user in modelContext.Users)
                {
                    var callbackUrl = "http://localhost:59441/Account/Login";
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath("~/MailTemplate/OfferNotification.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ConfirmationLink}", callbackUrl);
                    body = body.Replace("{UserName}", user.UserNamee);
                    bool IsSendEmail = SendEmail.EmailSend(user.Email, "Offer Notification", body, true);
                }
            }
        }
        public static readonly string SchedulingStatus = ConfigurationManager.AppSettings["ExecuteTaskServiceCallSchedulingStatus"];
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (SchedulingStatus.Equals("ON") )
                
                {
                    try
                    {
                        
                        using (modelContext DB = new modelContext())
                        {
                            
                            foreach (var offer in DB.Offers)
                            {

                                if (offer != null)
                                {

                                    if (offer.StartDate.ToString("MM / dd / yyyy hh: mm tt").Equals(DateTime.Now.ToString("MM / dd / yyyy hh: mm tt")) && offer.Status.Equals("Scheduled"))
                                    {
                                       
                                   
                                        offer.StartOffer();
                                        offer.Notify_Users();
                                    }
                                    else
                                    if (offer.EndDate.ToString("MM / dd / yyyy hh: mm tt").Equals(DateTime.Now.ToString("MM / dd / yyyy hh: mm tt")) && offer.Status.Equals("Active"))
                                    {
                                       

                                        offer.StopOffer();
                                    }

                                }

                            }


                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }
            });
            return task;
        }
    }
}