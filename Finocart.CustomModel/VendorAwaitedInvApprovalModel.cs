using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorAwaitedInvApprovalModel
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
        /// Vendor Total Invoice Count
        /// </summary>
        public int TotalInvoiceCount { get; set; }

        /// <summary>
        /// Vendor Total Approval Amount
        /// </summary>
        public decimal TotalApprovedAmt { get; set; }

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
