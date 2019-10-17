using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorBucketAwaitedInvViewModel
    {
        /// <summary>
        /// Vendor Bucket ID
        /// </summary>
        [Key]
        public Int64 BucketID { get; set; }

        /// <summary>
        /// Vendor Bucket Name
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// Vendor ID
        /// </summary>
        public int VendorID { get; set; }

        /// <summary>
        /// Vendor Total Invoice Count
        /// </summary>
        public int TotalInvoiceCount { get; set; }

        /// <summary>
        /// Vendor Total Approved Amount
        /// </summary>
        public decimal TotalApprovedAmt { get; set; }

        /// <summary>
        /// Vendor Discount Offered Date
        /// </summary>
        public DateTime DiscOfferedDate { get; set; }

        /// <summary>
        /// Vendor Discount Valid Upto
        /// </summary>
        public DateTime? DiscValidUpto { get; set; }

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
