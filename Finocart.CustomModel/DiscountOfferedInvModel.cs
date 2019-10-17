using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finocart.CustomModel
{
    public class DiscountOfferedInvModel
    {
        [Key]
        public Int64? InvoiceID { get; set; }

        public int CompanyId { get; set; }

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
        /// Invoice Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal? InvoiceAmt { get; set; }

        /// <summary>
        /// Invoice Approved Amount
        /// </summary>
        public decimal? ApprovedAmt { get; set; }

        public string BucketCreatedDate { get; set; }

        /// <summary>
        /// Invoice Discount
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// Discount Amount
        /// </summary>
        public decimal? DiscountAmt { get; set; }

        /// <summary>
        /// Disbursement Amount
        /// </summary>
        public decimal? DisbursementAmt { get; set; }

        /// <summary>
        /// Invoice Payment Date
        /// </summary>
        public string PaymentDate { get; set; }

     
        /// <summary>
        /// Invoice Total Record 
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Invoice Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

        
        
    }

    public class OfferedDiscountInvModel
    {
        [Key]
        public Int64? InvoiceID { get; set; }

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
        /// Invoice Vendor Name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Invoice Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal? InvoiceAmt { get; set; }

        /// <summary>
        /// Invoice Discount
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// Payment Due Date
        /// </summary>
        public string PaymentDueDate { get; set; }

        /// <summary>
        /// Rejection Reason
        /// </summary>
        public string RejectionReason { get; set; }

        /// <summary>
        /// Invoice Approved Amount
        /// </summary>
        public decimal? ApprovedAmt { get; set; }

        /// <summary>
        /// Invoice Status
        /// </summary>
        public string InvStatus { get; set; }

        /// <summary>
        /// Days to pay after approval of invoice(used for t+x calculation)
        /// </summary>
        public int? InvoiceApprovePayDays { get; set; }

        /// <summary>
        /// Invoice approval date
        /// </summary>
        public string InvoiceApprovalDate { get; set; }

        public int CompanyID { get; set; }

        public decimal? invoicediscount { get; set; }

        public Int64? BucketID { get; set; }

        public string BucketCreatedDate { get; set; }

        public string ValidToDate { get; set; }

        public string BucketName { get; set; }

        /// <summary>
        /// Invoice Total Record 
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Invoice Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }
    }
}
