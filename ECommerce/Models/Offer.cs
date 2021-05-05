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
        private DateTime startDate = new DateTime();
        [Required(ErrorMessage = "The start date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy} HH:mm")]
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }
        private DateTime endDate = new DateTime();
        [Required(ErrorMessage = "The end date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy} HH:mm")]
        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult("EndDate must be greater than StartDate");
            }
        }
        private decimal discount;
        [DisplayName("Discount ")]
        public decimal Discount {
            get { return this.discount; }
            set { this.discount = value; }
        }
        public virtual List<Item> Items { get => items; set => items = value; }
        private List<Item> items = new List<Item>();
        public string Item_Id { get; set; }
        [ForeignKey("Item_Id")]
        public virtual Item  item { get ; set;}



        public void StopOffer(Offer offer)
        {
            using (modelContext DB = new modelContext())
            {
                // offer.Id = (DB.Offers.Count() + 1).ToString();

                foreach (var item in offer.Items)
                {

                    var result = DB.Items.SingleOrDefault(b => b.Item_Id.Equals(item.Item_Id));
                    if (result != null)
                    {
                        if (item.SealedItem)
                        {
                            result.Price = ((int)(Double.Parse(result.Price) + (Double.Parse(result.Price) * Double.Parse(offer.Discount.ToString())))).ToString();
                            result.SealedItem = item.SealedItem;
                        }
                        DB.SaveChanges();
                    }

                }


            }
        }
    

        public void StartOffer(Offer offer)
        {
            using (modelContext DB = new modelContext())
            {
               // offer.Id = (DB.Offers.Count() + 1).ToString();

                foreach (var item in offer.Items)
                {

                    var result = DB.Items.SingleOrDefault(b => b.Item_Id.Equals(item.Item_Id));
                    if (result != null)
                    {
                       if (item.SealedItem)
                        {
                            result.Price = ((int)(Double.Parse(result.Price) - (Double.Parse(result.Price) * Double.Parse(offer.Discount.ToString())))).ToString();
                            result.SealedItem = !result.SealedItem;
                        }
                        DB.SaveChanges();
                    }

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
                                    if (offer.StartDate.Equals(DateTime.Now))
                                    {
                                        offer.StartOffer(offer);
                                    }
                                    else if(offer.EndDate.Equals(DateTime.Now))
                                    {
                                        offer.StopOffer(offer);
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