using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// User module
    /// </summary>
    public class User
    {
        /// <summary>
        /// User ID
        /// </summary>
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// User's Name 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User's Email Id
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's Password 
        /// </summary>
        ///[RegularExpression(pattern: " ((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{8,20})", ErrorMessage = "Password is not valid")]
        public string Password { get; set; }

        /// <summary> 
        /// User's Mobile Number
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// User's Designation 
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// User's Roles Access 
        /// </summary> 
        public string RolesAccess { get; set; }

        /// <summary>
        /// User's Roles Access ID
        /// </summary>
        public int? RoleAccessID { get; set; }

        /// <summary>
        /// User's Access View
        /// </summary>
        public int? AccessViewID { get; set; }

        /// <summary>
        /// User's Role
        /// </summary>
        public int? RoleID { get; set; }

        /// <summary>
        /// User's belonging company
        /// </summary>
        public int? CompanyID { get; set; }

        /// <summary>
        /// User Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// User Updated Date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// User Created By
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// User Updated By
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// User's delete status
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// User's PANNumber 
        /// </summary>
        public string PANNumber { get; set; }

        /// <summary>
        /// User's Temporary Password Status
        /// </summary>
        public bool IsTemporaryPassword { get; set; }
    }

}
