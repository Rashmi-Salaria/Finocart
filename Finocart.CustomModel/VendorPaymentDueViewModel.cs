using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorPaymentDueViewModel
    {
        [Key]
        public Int64 SequenceNumber { get; set; }
        /// <summary>
        /// Vendor ID
        /// </summary>
        public int VendorID { get; set; }

        /// <summary>
        /// Vendor Invoice PO ID
        /// </summary>
        public string POID { get; set; }

        /// <summary>
        /// Vendor Invoice ID
        /// </summary>
        public string InvoiceID { get; set; }

        /// <summary>
        /// Vendor Invoice Date
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Vendor Invoice Amount
        /// </summary>
        public decimal InvoiceAmt { get; set; }

        /// <summary>
        /// Vendor Invoice Approved Amount
        /// </summary>
        public decimal ApprovedAmt { get; set; }

        /// <summary>
        /// Vendor Invoice Earning
        /// </summary>
        public decimal Earning { get; set; }

        /// <summary>
        /// Vendor Invoice Net Payable Amount
        /// </summary>
        public decimal NetPayableAmt { get; set; }

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
