using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    /// <summary>
    /// Change Passowrd
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// User ID
        /// </summary>
        [Key]
        public int? UserId { get; set; }

        /// <summary>
        /// Old Password
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// New Password
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User Email ID
        /// </summary>
        public string Email { get; set; }
    }
}
