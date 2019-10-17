using System;
using System.Collections.Generic;
using System.Text;

namespace Finocart.CustomModel
{
    class NewAnchor
    {
        /// <summary>
        /// Vendor ID
        /// </summary>
        
        public int AnchorId { get; set; }

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
