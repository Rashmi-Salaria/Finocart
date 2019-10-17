using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public  class AwaitInvoiceApprovalModel
    {
        [Key]
        public Int64 SequenceNumber { get; set; }

        /// <summary>
        /// Invoice ID
        /// </summary>
        public Int64 InvoiceID { get; set; }

        /// <summary>
        /// Invoice Purchase Order Number
        /// </summary>
        public string PONumber { get; set; }

        /// <summary>
        /// Invoice Number
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Invoice Anchor Company Name
        /// </summary>
        public string AnchorCompName { get; set; }

        /// <summary>
        /// Anchor company id
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// Invoice Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal InvoiceAmt { get; set; }

        /// <summary>
        /// Invoice Discount
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// Invoice Payment Due Date
        /// </summary>
        public string PaymentDueDate { get; set; }

        /// <summary>
        /// Invoice Approved Amount
        /// </summary>
        public decimal? ApprovedAmt { get; set; }

        /// <summary>
        /// Invoice Status
        /// </summary>
        public string InvStatus { get; set; }

        /// <summary>
        /// discount valid date 
        /// </summary>
        public string ValidUptoDate { get; set; }
        /// <summary>
        /// Discount offer date 
        /// </summary>
        public string BucketCreatedDate { get; set; }
        public int? FilteredRecord { get; set; }
        public int? TotalRecord { get; set; }
    }
}
