using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorBucketInvoiceViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string POID { get; set; }

        /// <summary>
        /// Invoice ID
        /// </summary>
        public string InvoiceID { get; set; }

        /// <summary>
        /// Vendor Name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Invoice Creation Date 
        /// </summary>
        public DateTime InvoiceCreationDate { get; set; }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// Invoice Approved Amount
        /// </summary>
        public decimal ApprovedAmount { get; set; }

        /// <summary>
        /// Discount
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Discount Offered Date
        /// </summary>
        public DateTime DiscountOfferedDate { get; set; }

        /// <summary>
        /// Payment Due Date
        /// </summary>
        public DateTime PaymentDueDate { get; set; }

        /// <summary>
        /// Discount Valid Upto
        /// </summary>
        public DateTime? DiscountValidUpto { get; set; }

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
