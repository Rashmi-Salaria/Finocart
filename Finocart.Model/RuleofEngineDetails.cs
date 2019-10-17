using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    public class RuleofEngineDetails
    {
        [Key]
        /// <summary>
        /// Company ID
        /// </summary>
        public int CompanyID { get; set; }

        public decimal? InvoiceLimitAmt { get; set; }

        public decimal? PercentageRate { get; set; }
        public decimal? InternalFundLimit { get; set; }
        public decimal? BankLimit { get; set; }
        public decimal? AnchorRate { get; set; }
        public bool IsLimitUnlimited { get; set; }
        public int? PaymentDays { get; set; }
        public decimal? TotalDraw_funds { get; set; }
        public decimal? AvalableLimits { get; set; }
        public decimal? RemainingLimits { get; set; }



    }
}
