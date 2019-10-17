using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class InvoiceStatus
    {
        [Key]
        public Int64 InvoiceID { get; set; }


        /// <summary>
        /// Invoice Anchor Company Name
        /// </summary>
        public string  VendorName { get; set; } 
        /// <summary>
        /// Invoice Purchase Order Number
        /// </summary>
        public string PONumber { get; set; }

        /// <summary>
        /// Invoice Number
        /// </summary>
        public string InvoiceNo { get; set; }



        /// <summary>
        /// Invoice Invoice Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal InvoiceAmt { get; set; }


        /// <summary>
        /// Invoice Approval date
        /// </summary>
       public decimal InvoiceApprovedAmount { get; set; }



        /// <summary>
        /// Invoice status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Discount Bucket ID
        /// </summary>
        public string DiscountBucketBucketName { get; set; }


        /// <summary>
        /// Amount Paid
        /// </summary>
        public decimal? AmountPaid { get; set; }


        /// <summary>
        /// Earnings
        /// </summary>
        public decimal? Earnings { get; set; }
       

        /// <summary>
        /// Payments Date
        /// </summary>
      public DateTime? PaymentDate { get; set; }
       
         /// <summary>
        /// UTR Details 
        /// </summary>
        public string UTRDetails { get; set; }


        /// <summary>
        /// Invoice Discount Offered date
        /// </summary>
        public string DiscountDate { get; set; }

        
        public string IPAddress { get; set; }

        public string FinoLimUnLim { get; set; }

        public string Limits { get; set; }

        public int? FilteredRecord { get; set; }

        public int? TotalRecord { get; set; }
    }

    public class InvoiceUTRDetails
    {
        [Key]
        public int InvoiceID { get; set; }

        public string UTRDetails { get; set; }
    }
}
