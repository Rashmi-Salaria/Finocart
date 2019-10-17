using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class AnchorCompListingModel
    {
        /// <summary>
        /// Anchor Company ID
        /// </summary>
        [Key]
        public int CompanyID { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Company_name { get; set; }

        /// <summary>
        /// Anchor Company Contact Name
        /// </summary>
        public string Contact_Name { get; set; }

        /// <summary>
        /// Anchor Company Mobile Number
        /// </summary>
        public string Contact_mobile { get; set; }

        /// <summary>
        /// Anchor Company Email ID
        /// </summary>
        public string Contact_email { get; set; }

        /// <summary>
        /// Anchor Company Total Invoices
        /// </summary>
        public int TotalInvoice { get; set; }

        /// <summary>
        /// Anchor Company Total Invoice Amount
        /// </summary>
        /// 
        
        public decimal? TotalInvoiceAmount { get; set; }
                      
        /// <summary>
        /// Anchor Company Total Invoice Approved Amount
        /// </summary>
        public decimal? TotalInvoiceAppAmount { get; set; }

        /// <summary>
        /// Anchor Company Approved Invoices
        /// </summary>
        public int? ApprovedInv { get; set; }

        /// <summary>
        /// Anchor Company Rejected Invoices
        /// </summary>
        public int? RejectedInv { get; set; }

        /// <summary>
        /// Anchor Company Pending Invoices
        /// </summary>
        public int? PendingInv { get; set; }

        /// <summary>
        /// Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Anchor Company Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

        /// <summary>
        /// Pending Discount Invoice
        /// </summary>
        public int PendingDiscInv { get; set; }

        /// <summary>
        /// Pending Invoice Total Amount
        /// </summary>
        public decimal AwaitTotalInvoiceAmount { get; set; }

        ///<summary>
        ///Anchor DashBoard Invoice Amount
        ///</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Pending Invoice Total Awaited Amount
        /// </summary>
        public decimal AwaitTotalInvoiceAppAmount { get; set; }
    }
}
