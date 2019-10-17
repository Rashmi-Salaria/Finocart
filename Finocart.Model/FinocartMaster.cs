using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Finocart Master (Superadmin)
    /// </summary>
    public class FinocartMaster
    {
        /// <summary>
        /// User Id of superadmin
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Name of superadmin
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email Id of superadmin
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Password of superadmin
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Superadmin created by
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Superadmin created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Superadmin updatedBy by
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Superadmin updated on
        /// </summary>
        public DateTime? UpdatedDate  { get; set; }

        /// <summary>
        /// Temporary Password Status
        /// </summary>
        public bool IsTemporaryPassword { get; set; }

        /// <summary>
        /// Deletion status
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
