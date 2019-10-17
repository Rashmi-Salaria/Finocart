using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RuleOfEngine_Scheduler
{
    public class ClsCommon
    {
        FinocartDemo1Entities finocartEntities = new FinocartDemo1Entities();
        public void SendEmail(string emailToAddress, string subject, string body, bool isBodyHtml)
        {
            var LookupDetails = (from data in finocartEntities.LookupDetails select data).ToList();
            string gmailUserName = LookupDetails.ElementAt(0).LookupValue;
            string gmailUserPassword = LookupDetails.ElementAt(1).LookupValue;
            string SMTPSERVER = LookupDetails.ElementAt(2).LookupValue;
            int PORTNO = Convert.ToInt32(LookupDetails.ElementAt(3).LookupValue);

            SmtpClient smtpClient = new SmtpClient(SMTPSERVER, PORTNO);
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(gmailUserName, gmailUserPassword);
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(gmailUserName, "Finocart");
                    message.Subject = subject == null ? "" : subject;
                    message.Body = body == null ? "" : body;
                    message.IsBodyHtml = isBodyHtml;
                    message.To.Add(emailToAddress);

                    try
                    {
                        smtpClient.Send(message);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
