using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleOfEngine_Scheduler
{
    public interface ISendEmailAndNotification
    {
        bool GetInvoice();

        bool GetRemainingAvailLimit();
    }
    public class SendEmailAndNotification : ISendEmailAndNotification
    {
        FinocartDemo1Entities finocartEntities = new FinocartDemo1Entities();
        ClsCommon clsCommon = new ClsCommon();
        public bool GetInvoice()
        {
            try
            {

                var invoice = (from data in finocartEntities.Invoices where data.AmountPaid !=null && data.AmountPaid != 0 && data.PaymentDate == DateTime.Today.Date && 
                               data.InvoiceStatus == (from data1 in finocartEntities.ModuleMasters where data1.Value == "Approved" select data1.ID).FirstOrDefault()
                               group data.AmountPaid by new { data.VendorID, data.AnchorCompID} into data1 select new { data1.Key.VendorID,
                data1.Key.AnchorCompID, AmountPaid = data1.Sum()}).ToList();
                List<AnchorNotification> lstNotification = new List<AnchorNotification>();
                foreach (var inv in invoice)
                {
                    AnchorNotification anchorNotification = new AnchorNotification();
                    anchorNotification.UserID = inv.AnchorCompID;
                    anchorNotification.RoleID = (from data in finocartEntities.LookupDetails where data.LookupValue == "Anchor Company" select data.ID).FirstOrDefault();
                    anchorNotification.Description = (from data in finocartEntities.Companies where data.CompanyID == inv.VendorID select data.Company_name).FirstOrDefault() + " for an amount of " + inv.AmountPaid + " is due today. Kindly release funds for this client.";
                    anchorNotification.IsRead = false;
                    anchorNotification.IsDelete = false;
                    anchorNotification.CreatedOn = DateTime.Now;
                    lstNotification.Add(anchorNotification);

                    var user = (from data in finocartEntities.Users where data.CompanyID == inv.AnchorCompID && 
                                (data.AccessViewID == (from data1 in finocartEntities.LookupDetails where data1.LookupValue == "Anchor Company" select data1.ID).FirstOrDefault() || 
                                data.AccessViewID == (from data1 in finocartEntities.LookupDetails where data1.LookupValue == "Both" select data1.ID).FirstOrDefault())
                                select data).ToList();
                    var Path = (from data in finocartEntities.PaymentDueTodayTemplates select data.Template).FirstOrDefault();
                    foreach (var users in user)
                    {
                        string body = Path;
                        body = body.Replace("@@User@@", users.Name);
                        body = body.Replace("@@CompanyName@@", (from data in finocartEntities.Companies where data.CompanyID == inv.VendorID select data.Company_name).FirstOrDefault());
                        body = body.Replace("@@AmountPaid@@", inv.AmountPaid.ToString());
                        clsCommon.SendEmail(users.Email, "Payment for vendor due today", body, true);
                    }
                }
                finocartEntities.AnchorNotifications.AddRange(lstNotification);
                finocartEntities.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetRemainingAvailLimit()
        {
            try
            {

                var BankLimits = (from data in finocartEntities.SetBankAmountLimits
                               where data.Remaining_avail_bal == 0
                               group data.Remaining_avail_bal by new { data.Anchor_id, data.Anchor_Name, data.BankName } into data1
                               select new
                               {
                                   data1.Key.Anchor_id,
                                   data1.Key.Anchor_Name,
                                   data1.Key.BankName,
                                   RemainingAvailBal = data1.Sum()
                               }).ToList();
                List<BankNotification> lstNotification = new List<BankNotification>();
                
                foreach (var bank in BankLimits)
                {
                    BankNotification bankNotification = new BankNotification();
                    bankNotification.UserID = Convert.ToInt64(bank.BankName);
                    bankNotification.RoleID = (from data in finocartEntities.LookupDetails where data.LookupValue == "Bank" select data.ID).FirstOrDefault();
                    bankNotification.Description = (from data in finocartEntities.Companies where data.CompanyID == bank.Anchor_id select data.Company_name).FirstOrDefault() + " set limit has exhausted. Please assign additional limits to the client.";
                    bankNotification.IsRead = false;
                    bankNotification.IsDelete = false;
                    bankNotification.CreatedOn = DateTime.Now;
                    lstNotification.Add(bankNotification);

                    Int64 BankID = Convert.ToInt64(bank.BankName);
                    var user = (from data in finocartEntities.Users
                                where data.CompanyID == BankID &&
                               (data.AccessViewID == (from data1 in finocartEntities.LookupDetails where data1.LookupValue == "Bank" select data1.ID).FirstOrDefault())
                                select data).ToList();
                    var Path = (from data in finocartEntities.BankSetLimitMailTemplates select data.Template).FirstOrDefault();
                    foreach (var users in user)
                    {
                        string body = Path;
                        body = body.Replace("@@User@@", (from data in finocartEntities.Companies where data.CompanyID == BankID select data.Company_name).FirstOrDefault());
                        body = body.Replace("@@CompanyName@@", (from data in finocartEntities.Companies where data.CompanyID == bank.Anchor_id select data.Company_name).FirstOrDefault());
                        clsCommon.SendEmail(users.Email, "Bank limit has exhausted", body, true);
                    }
                }
                finocartEntities.BankNotifications.AddRange(lstNotification);
                finocartEntities.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
