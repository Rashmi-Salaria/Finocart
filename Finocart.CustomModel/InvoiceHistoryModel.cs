using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class InvoiceHistoryModel
    {
        [Key]
        public Int64 InvoiceID { get; set; }


        /// <summary>
        /// Invoice Anchor Company Name
        /// </summary>
        public string AnchorCompName { get; set; }
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
        /// Invoice Discount Offered
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// Invoice Discount Offered date
        /// </summary>
        public string DiscountDate { get; set; }


        /// <summary>
        /// Invoice approval date
        /// </summary>
        public string InvoiceApprovalDate { get; set; }

        /// <summary>
        /// Amount after discount
        /// </summary>
        public decimal? DiscountAmt { get; set; }

        /// <summary>
        /// Disbursement amount
        /// </summary>
        public decimal? DisbursementAmt { get; set; }

        /// <summary>
        /// Discount Bucket ID
        /// </summary>
        public string DiscountBucketBucketName { get; set; }


        /// <summary>
        /// Amount Paid
        /// </summary>
        public decimal? AmountPaid{ get; set; }

        public string InvoiceStatus { get; set; }

        /// <summary>
        /// UTR Details 
        /// </summary>
        public string UTRDetails  { get; set; }

        public decimal? ApprovedAmt { get; set; }

        public string FinoLimUnLim { get; set; }

        public int? FilteredRecord { get; set; }
        public int? TotalRecord { get; set; }
    }
}
