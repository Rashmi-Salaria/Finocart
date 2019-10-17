using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finocart.CustomModel
{
    /// <summary>
    /// Bucket Invoice Model
    /// </summary>
    public class BucketInvoiceModel
    {
        [Key]
        /// <summary>
        /// Invoice Id
        /// </summary>
        public Int64 Invoiceid { get; set; }

        /// <summary>
        /// String of invoice id
        /// </summary>
        [NotMapped]
        public string InvoiceIDStr { get; set; }

        /// <summary>
        /// Bucket name
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// Discount percentage
        /// </summary>
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// Discount valid upto date
        /// </summary>
        public string ValidToDate { get; set; }

        /// <summary>
        /// Total invoice count
        /// </summary>
        [NotMapped]
        public int? TotalInvoiceCount { get; set; }

        /// <summary>
        /// Total invoice amount
        /// </summary>
        [NotMapped]
        public float? TotalInvoiceAmount { get; set; }

        /// <summary>
        /// Amount after applying discount
        /// </summary>
        [NotMapped]
        public float? AfterDiscountAmount { get; set; }

        /// <summary>
        /// Discoounted amount
        /// </summary>
        [NotMapped]
        public float? DiscountAmount { get; set; }

        /// <summary>
        /// Bucket ID
        /// </summary>
        public Int64? BucketID { get; set; }

        /// <summary>
        /// Bucket status
        /// </summary>
        [NotMapped]
        public int? BucketStatus { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        [NotMapped]
        public int? UserId { get; set; }

        /// <summary>
        /// Email ID
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// company ID
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// Invoice No
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Emaill id of vendor(discount offering user)
        /// </summary>
       

        public string Company_ContactName { get; set; }

    }
}
