using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Configuration;

namespace ECommerce.Models
{
    public class Jobclass : IJob
    {
        public static readonly string SchedulingStatus = ConfigurationManager.AppSettings["ExecuteTaskServiceCallSchedulingStatus"];
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (SchedulingStatus.Equals("ON"))
                {
                    try
                    {
                        //Do whatever stuff you want
                        using (var message = new MailMessage("ghaithsuleiman04@gmail.com", "ghaitheldalahmeh@gmail.com"))
                        {
                            message.Subject = "Message Subject test";
                            message.Body = "Message body test at " + DateTime.Now;
                            using (SmtpClient client = new SmtpClient
                            {
                                EnableSsl = true,
                                Host = "smtp.gmail.com",
                                Port = 587,
                                Credentials = new NetworkCredential("ghaithsuleiman04@gmail.com", "yde079078")
                            })
                            {
                                client.Send(message);
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
            //Task IJob.Execute(IJobExecutionContext context)
            //{
            //    throw new NotImplementedException();
            //}

            //Task IJob.Execute(IJobExecutionContext context)
            //{
            //    using (var message = new MailMessage("testuser@gmail.com", "testdestinationmail@gmail.com"))
            //    {
            //        message.Subject = "Message Subject test";
            //        message.Body = "Message body test at " + DateTime.Now;
            //        using (SmtpClient client = new SmtpClient
            //        {
            //            EnableSsl = true,
            //            Host = "smtp.gmail.com",
            //            Port = 587,
            //            Credentials = new NetworkCredential("testuser@gmail.com", "123546")
            //        })
            //        {
            //            client.Send(message);
            //        }
            //    }
            //}
        }
}