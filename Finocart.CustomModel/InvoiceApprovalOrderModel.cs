using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class InvoiceApprovalOrderModel
    {
        /// <summary>
        /// Approval ID
        /// </summary>
        [Key]
        public int ApprovalID { get; set; }

        /// <summary>
        /// From Amount
        /// </summary>
        public decimal FromAmount { get; set; }

        /// <summary>
        /// To Amount
        /// </summary>
        public decimal ToAmount { get; set; }

        /// <summary>
        /// Approved By
        /// </summary>
        public int ApprovedBy { get; set; }

        /// <summary>
        /// Approved By Name
        /// </summary>
        public string ApprovedByName { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public string Users { get; set; }

        /// <summary>
        /// Users Name
        /// </summary>
        public string UsersName { get; set; }

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
