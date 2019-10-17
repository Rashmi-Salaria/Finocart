using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    /// <summary>
    /// User model
    /// </summary
    public class UserModel 
    {
        #region Declarations

        /// <summary>
        /// User ID
        /// </summary>
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; set; }
        //[EmailAddress(ErrorMessage = "Invalid Email Address.")]

        /// <summary>
        /// User Email ID
        /// </summary>
        /// 
    
        public string Email { get; set; }

        /// <summary>
        /// User Mobile Number
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// User Designation
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// User Roles Access
        /// </summary>
        public string RolesAccess { get; set; }

        /// <summary>
        /// User Look up Value
        /// </summary>
        public string LookupValue { get; set; }

        /// <summary>
        /// User Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// User Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// User Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

        /// <summary>
        /// User Role Access ID
        /// </summary>
        public int RoleAccessID { get; set; }

        ///// <summary>
        ///// User Permission
        ///// </summary>
        //public string Permission { get; set; }

        /// <summary>
        /// User Access View ID
        /// </summary>
        public int AccessViewID { get; set; }

        ///// <summary>
        ///// User Role ID
        ///// </summary>
        //public int RoleID { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User Created By
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// User Updated By
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// User Company ID
        /// </summary>
        public int? CompanyID { get; set; }
        #endregion
    }
    public class Check_duplicate
    {
        public int UserExist { get; set; }
    }
}
