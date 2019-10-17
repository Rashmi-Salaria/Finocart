using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorBucketWiseDiscInvViewModel
    {
        /// <summary>
        /// Invoice ID
        /// </summary>
        [Key]
        public Int64 InvoiceID { get; set; }

        /// <summary>
        /// Purchase Order Number
        /// </summary>
        public string PONumber { get; set; }

        /// <summary>
        /// Invoice No
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Vendor Name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Invoice Creation Date
        /// </summary>
        public DateTime InvoiceCreatedDate { get; set; }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// Approved Amount
        /// </summary>
        public decimal ApprovedAmount { get; set; }

        /// <summary>
        /// Discount Rate
        /// </summary>
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// Discount Offered Date
        /// </summary>
        public DateTime DiscountOfferedDate { get; set; }

        /// <summary>
        /// Payment Due Date
        /// </summary>
        public DateTime PaymentDueDate { get; set; }

        /// <summary>
        /// Payment Date
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Payable Amount
        /// </summary>
        public decimal PayableAmount { get; set; }

        /// <summary>
        /// Earning
        /// </summary>
        public decimal Earning { get; set; }

        /// <summary>
        /// Vendor Total Record 
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Vendor Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

    }
}
