using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Vendor
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// Vendor ID
        /// </summary>
        [Key]
        public Int64  VendorID { get; set; }

        /// <summary>
        /// Vendor Name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Vendor ContactNumber
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Vendor Email ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// Maximum limit
        /// </summary>
        public Int32 MaxLimit { get; set; }
        /// <summary>
        /// Vendor's status
        /// </summary>
        public Int32 VendorStatus { get; set; }
        /// <summary>
        /// Vendor created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Vendor created user
        /// </summary>
        public long Createdby { get; set; }
        /// <summary>
        /// Vendor updated date
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Vendor updated by
        /// </summary>
        public long? Updatedby { get; set; }
        /// <summary>
        /// Vendor's contact person name
        /// </summary>
        public string ContactPersonName { get; set; }
        /// <summary>
        /// Vendor's contact person number
        /// </summary>
        public Int32 ContactPersonNumber { get; set; }
        /// <summary>
        /// Vendor's contact person email id
        /// </summary>
        public string ContactPersonEmailId { get; set; }
        /// <summary>
        /// Vendor's deletion status
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Vendor's PAN number
        /// </summary>
        public string PANNumber { get; set; }
        /// <summary>
        /// Vendor's contact person designation
        /// </summary>
        public string ContactPersonDesignation { get; set; }
        /// <summary>
        /// Vendor's password
        /// </summary>
        ///[RegularExpression(pattern:" ((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{8,20})",ErrorMessage ="Password is not valid")]
        public string Password{ get; set; }
        /// <summary>
        /// Status of temporary password
        /// </summary>
        public bool IsTemporaryPassword { get; set; }

        /// <summary>
        /// CompanyID of vendor
        /// </summary>
        public int CompanyID { get; set; }
    }
}
