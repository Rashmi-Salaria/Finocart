using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class AnchorAnalyticLostOppModel
    {
        /// <summary>
        /// Company ID
        /// </summary>
        [Key]
        public int? CompanyID { get; set; }

        /// <summary>
        /// Number of Discounted Invoices
        /// </summary>
        public int? NoOfDiscInvoice { get; set; }

        /// <summary>
        /// Total Discount Invoices Approved Amount
        /// </summary>
        public decimal? TotalDiscAppAmt { get; set; }

        /// <summary>
        /// Total Approved Invoices Count
        /// </summary>
        public int? TotalAppCount { get; set; }

        /// <summary>
        /// Discount Approved Invoice Approved Amount
        /// </summary>
        public decimal? ApprovedDiscInvAmt { get; set; }

        /// <summary>
        /// Percent of Discounted Invoice Approved
        /// </summary>
        public decimal? DiscOffPercent { get; set; }

        /// <summary>
        /// Amount Paid
        /// </summary>
        public decimal? AmountPaid { get; set; }

        /// <summary>
        /// Amount Earned After Discount
        /// </summary>
        public decimal? AmountEarned { get; set; }

        /// <summary>
        /// Opportunities Lost on Amount
        /// </summary>
        public decimal? OppLostOnAmt { get; set; }

        /// <summary>
        /// Percent of Opportunity Lost on Received Invoices
        /// </summary>
        public decimal? PerOppLost { get; set; }

        /// <summary>
        /// Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }
    }
}
