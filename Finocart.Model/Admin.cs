using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Admin 
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Admin Id
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Admin Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Admin Email Id
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Admin PAN Number
        /// </summary>
        public string PANNumber { get; set; }

        /// <summary>
        /// Admin Password
        /// </summary>
        ///[RegularExpression(pattern: "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{8,20})", ErrorMessage = "Password is not valid")]
        public string Password { get; set; }

        /// <summary>
        /// Admin Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Admin Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Admin Updated By
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Admin Updated Date
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
