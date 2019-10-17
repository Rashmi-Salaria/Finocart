using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorInvApproveTodayViewModel
    {
        /// <summary>
        /// Bucket ID
        /// </summary>
        [Key]
        public Int64 BucketID { get; set; }

        /// <summary>
        /// Bucket Name
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// Vendor ID
        /// </summary>
        public int VendorID { get; set; }

        /// <summary>
        /// Total Invoice Count
        /// </summary>
        public int TotalInvoiceCount { get; set; }

        /// <summary>
        /// Total Approved Amount
        /// </summary>
        public decimal TotalApprovedAmt { get; set; }

        /// <summary>
        /// Discount Offered Date
        /// </summary>
        public DateTime DiscOfferedDate { get; set; }

        /// <summary>
        /// Total Disbursement Amount
        /// </summary>
        public decimal TotalDisbAmt { get; set; }

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
