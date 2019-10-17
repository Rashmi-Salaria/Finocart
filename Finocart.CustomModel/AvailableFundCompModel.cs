using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class AvailableFundCompModel
    {
        [Key]
        public int CompanyID { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string AnchorCompName { get; set; }

        public Int32? InvoiceNo { get; set; }

        public decimal? InvoiceAmt { get; set; }

        public decimal? ApprovedAmt { get; set; }


        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Invoice Filtered Record
        /// </summary>
        public Int32? FilteredRecord { get; set; }
    }
}
