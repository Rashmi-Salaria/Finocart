using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Notification module
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Notification ID
        /// </summary>
        
        [Key]
        public Int64 NotificationID { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public Int64 UserID { get; set; }

        /// <summary>
        /// User Role ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Notification Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Notification IsRead
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Notification's delete status
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Notification Created Date
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Notification Read Date
        /// </summary>
        public DateTime ReadOn { get; set; }
    }
}
