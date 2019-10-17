using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    /// <summary>
    /// User Login Model
    /// </summary>
    public class UserLoginModel
    {
        /// <summary>
        /// Vendor Id
        /// </summary>
        [Key]       
        public int UserID { get; set; }
        
        /// <summary>
        /// Vendor Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email ID
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Temporary Password
        /// </summary>
        public bool IsTemporaryPassword { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        public int CompanyID { get; set; }


        /// <summary>
        /// Company Name
        /// </summary>
        public string Companyname { get; set; }
        

        /// <summary>
        /// Role Access ID
        /// </summary>
        public int RoleAccessID { get; set; }

        /// <summary>
        /// Role Access
        /// </summary>
        public string RoleAccess { get; set; }

       
    }
}
