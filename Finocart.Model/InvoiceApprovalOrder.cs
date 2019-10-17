using System;

namespace Finocart.Model
{
    /// <summary>
    /// Access to approve invoice
    /// </summary>
    public class InvoiceApprovalOrder
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Company ID
        /// </summary>
        public int CompanyID { get; set; }
        /// <summary>
        /// To approve invoice's amount range
        /// </summary>
        public decimal FromAmount { get; set; }
        /// <summary>
        /// To approve invoice's amount range
        /// </summary>
        public decimal ToAmount { get; set; }
        /// <summary>
        /// Invoice approved by
        /// </summary>
        public int ApprovedBy { get; set; }
        /// <summary>
        /// Users' list
        /// </summary>
        public string Users { get; set; }
        /// <summary>
        /// List created by
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// List created date
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// List updatedBy by
        /// </summary>
        public int UpdatedBy { get; set; }
        /// <summary>
        /// List updated date
        /// </summary>
        public DateTime UpdatedOn { get; set; }
        /// <summary>
        /// Deletion status
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// Deleting user
        /// </summary>
        public int DeletedBy { get; set; }
        /// <summary>
        /// Deleted date
        /// </summary>
        public DateTime DeletedOn { get; set; }
    }
}
