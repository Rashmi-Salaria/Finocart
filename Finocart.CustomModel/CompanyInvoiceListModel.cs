using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class CompanyInvoiceListModel
    {
        /// <summary>
        /// Company Invoice ID
        /// </summary>
        [Key]
        public Int64? InvoiceID { get; set; }

        /// <summary>
        /// Company Invoice Number
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Company Vendor Name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        public int? CompanyID { get; set; }

        /// <summary>
        /// Company Purchase Order Number
        /// </summary>
        public string PONumber { get; set; }

        /// <summary>
        /// Company Purchase Order Date
        /// </summary>
        public string PODate { get; set; }

        /// <summary>
        /// Company Invoice Amount
        /// </summary>
        public decimal? InvoiceAmt { get; set; }

        /// <summary>
        /// Company Payment Due Date
        /// </summary>
        public string PaymentDueDate { get; set; }

        /// <summary>
        /// Company Discount
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// Company Invoice Status
        /// </summary>
        public string INVStatus { get; set; }

        /// <summary>
        /// Company Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

        /// <summary>
        /// Company Rejection Reason 
        /// </summary>
        public string RejectionReason { get; set; }

        /// <summary>
        /// Company Approved Amount
        /// </summary>
        public decimal? ApprovedAmt { get; set; }
    }
}
