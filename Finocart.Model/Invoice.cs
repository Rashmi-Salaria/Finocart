using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Invoice Listing
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Invoice Id
        /// </summary>
        [Key]
        public Int64 InvoiceID { get; set; }

        /// <summary>
        /// Purchase Order Number
        /// </summary>
        public string PONumber { get; set; }

        /// <summary>
        /// Purchase Order Date
        /// </summary>
        public DateTime PODate { get; set; }
        
        /// <summary>
        /// Invoice Number
        /// </summary>
        public string InvoiceNo { get; set; }
        
        /// <summary>
        /// Anchor Company Id
        /// </summary>
        public Int64 AnchorCompID { get; set; }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal InvoiceAmt { get; set; }

        /// <summary>
        /// Payment Due Date
        /// </summary>
        public DateTime PaymentDueDate { get; set; }

        /// <summary>
        /// Discount uopn invoice
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Days
        /// </summary>
        public Int64 Days { get; set; }

        /// <summary>
        /// Invoice Status
        /// </summary>
        public int InvoiceStatus { get; set; }

        /// <summary>
        /// Invoice Rejection Reason
        /// </summary>
        public string RejectionReason { get; set; }

        /// <summary>
        /// Invoice Approved Amount
        /// </summary>
        public decimal ApprovedAmt { get; set; }
        
        /// <summary>
        /// Upload Invoice
        /// </summary>
        public string UploadInvoice { get; set; }

        /// <summary>
        /// Upload Other Document
        /// </summary>
        public string UploadDocument { get; set; }

        /// <summary>
        /// Invoice Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Invoice Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Invoice Updated By
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Invoice Updated Date
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Days to pay after approval of invoice(used for t+x calculation)
        /// </summary>
        public int InvoiceApprovePayDays { get; set; }
    }
}
