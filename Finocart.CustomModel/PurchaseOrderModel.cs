using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    /// <summary>
    /// Purchase order Model
    /// </summary>
    public class PurchaseOrderModel
    {
        /// <summary>
        /// Purchase Order Number
        /// </summary>
        [Key]
        public string PONumber { get; set; }

        /// <summary>
        /// Purchase Order Number of Invoices
        /// </summary>
        public Int32 NoOfInvoices { get; set; }

        /// <summary>
        /// Purchase Order Vendor Name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Purchase Order Total Invoice Amount
        /// </summary>
        public Decimal TotalInvoiceAmount { get; set; }

        /// <summary>
        /// Purchase Order Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Purchase Order Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }
    }
}
