using System;
using System.ComponentModel.DataAnnotations;
namespace Finocart.CustomModel
{
    public class InvoiceJourneyHistoryModel
    {
        [Key]
        
        //public Int64 InvoiceID { get; set; }
        public int InvoiceStatus { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string invoicestatusvalue { get; set; }
        public string Remark { get; set; }
        public string InvoiceStatusText { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string SentToPerson { get; set; }
        public string Company_name { get; set; }
        public DateTime? InvoiceCreatedDate { get; set; }
        public string PONumber { get; set; }
        public DateTime PaymentDueDate { get; set; }
    }
}
