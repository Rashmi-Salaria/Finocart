using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorInvoiceApprovedTodayModel
    {
        /// <summary>
        /// Vendor ID
        /// </summary>
        [Key]
        public int VendorID { get; set; }

        /// <summary>
        /// Vendor Name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Total number of discounted invoices
        /// </summary>
        public int TotalDiscAppToday { get; set; }

        /// <summary>
        /// Total approved amt
        /// </summary>
        public decimal? TotalApprovedAmt { get; set; }

        /// <summary>
        /// Total disbursement amount
        /// </summary>
        public decimal? TotalDisbAmt { get; set; }

        /// <summary>
        /// Earning
        /// </summary>
        public decimal? Earning { get; set; }

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
