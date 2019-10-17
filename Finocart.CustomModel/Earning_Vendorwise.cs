using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class Earning_Vendorwise
    {

        [Key]
        public int CompanyID { get; set; }
        /// <summary>
        /// Property vendor Name
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// No Of Offered Discount
        /// </summary>
        public int InvoiceCount { get; set; }
        /// <summary>
        /// Invooice amount
        /// </summary>
        public decimal? InvoiceAmount { get; set; }
        /// <summary>
        /// Approved invoices Amount
        /// </summary>
        public decimal? ApprovedAmount { get; set; }
        /// <summary>
        /// Amount Paid
        /// </summary>
        public decimal? Amountpaid { get; set; }
        /// <summary>
        /// Amount Earning
        /// </summary>
        public decimal? Earning { get; set; }


        public int? FilteredRecord { get; set; }
        public int? TotalRecord { get; set; }
    }
    public class Eearning_discountRateWise
    {

        [Key]
        public Int64 SequenceNumber { get; set; }
        public decimal Discount { get; set; }
        public int InvoiceCount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Earning { get; set; }

        public int? FilteredRecord { get; set; }
        public int? TotalRecord { get; set; }

    }
}
