using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class CriticalVendorsModel
    {
        [Key]
        /// <summary>
        /// Critical Vendors Company ID
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// Critical Vendors Company Name
        /// </summary>
        public string Company_name { get; set; }

        /// <summary>
        /// Critical Vendors Percentage Rate
        /// </summary>
        public decimal? PercentageRate { get; set; }

        /// <summary>
        /// Critical Vendors Invoice Limit Status ID
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Critical Vendors Invoice Limit Status
        /// </summary>
        public string InvAmtLimitStatus { get; set; }
        
        /// <summary>
        /// Critical Vendors Invoice Limit Amount
        /// </summary>
        public decimal? InvoiceLimitAmt { get; set; }

        /// <summary>
        /// Critical Vendors Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Critical Vendors Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }
    }
}
