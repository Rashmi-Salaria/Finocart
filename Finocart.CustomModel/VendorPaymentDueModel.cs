using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorPaymentDueModel
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
        /// Vendor Number of Invoices
        /// </summary>
        public int NoOfInvoices { get; set; }

        /// <summary>
        /// Vendor Total Invoice Amount
        /// </summary>
        public decimal? TotalInvoiceAmount { get; set; }

        /// <summary>
        /// Vendor Total Invoice Approved Amount
        /// </summary>
        public decimal? TotalInvoiceAppAmt { get; set; }

        /// <summary>
        /// Vendor Invoice Amount Receive Today
        /// </summary>
        public decimal? AmountPaymenttoday { get; set; }

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
