using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    /// <summary>
    /// Invoice Model
    /// </summary>
    public class InvoiceModel
    {
        /// <summary>
        /// Invoice Id
        /// </summary>
        [Key]
        public Int64 InvoiceID { get; set; }

        /// <summary>
        /// Invoice Purchase Order Number
        /// </summary>
        public string PONumber { get; set; }

        /// <summary>
        /// Invoice Number
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Invoice Anchor Company Name
        /// </summary>
        public string AnchorCompName { get; set; }

        /// <summary>
        /// Invoice Vendor Name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Invoice Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal? InvoiceAmt { get; set; }

        /// <summary>
        /// Invoice Discount
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// Days
        /// </summary>
        public Int64? Days { get; set; }

        /// <summary>
        /// Invoice Payment Due Date
        /// </summary>
        public DateTime? PaymentDueDate { get; set; }

        /// <summary>
        /// Invoice Rejection Reason
        /// </summary>
        public string RejectionReason { get; set; }

        /// <summary>
        /// Invoice Approved Amount
        /// </summary>
        public decimal? ApprovedAmt { get; set; }

        /// <summary>
        /// Invoice Status
        /// </summary>
        public string InvStatus { get; set; }

        /// <summary>
        /// Invoice Total Record 
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Invoice Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

        /// <summary>
        /// Days to pay after approval of invoice(used for t+x calculation)
        /// </summary>
        public int? InvoiceApprovePayDays { get; set; }

        /// <summary>
        /// Invoice approval date
        /// </summary>
        public DateTime? InvoiceApprovalDate { get; set; }

        /// <summary>
        /// Anchor company id
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// Amount after discount
        /// </summary>
        public decimal? DiscountAmt { get; set; }

        /// <summary>
        /// Disbursement amount
        /// </summary>
        public decimal? DisbursementAmt { get; set; }
    }



   #region Vendor MaxAvailable Amount 
    public class MaxAvailableAmount
    {
        [Key]
        public decimal MaxVendorAmountAvailable { get; set; }
    }
    #endregion

    #region Finoassist Min Payment Date
    public class MinPaymentDate
    {   [Key]  
        public int row { get; set; }
        public DateTime? MinFundPaymentDate { get; set; }
    }
    #endregion

}
