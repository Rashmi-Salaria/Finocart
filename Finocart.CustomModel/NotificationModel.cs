using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class NotificationModel
    {
        /// <summary>
        /// Notification ID
        /// </summary>
        [Key]
        public Int64 NotificationID { get; set; }

        /// <summary>
        /// Notification Description
        /// </summary>
        public string Notifications { get; set; }

        /// <summary>
        /// Notification CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Total Notification Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Filtered Notification Record
        /// </summary>
        public int? FilteredRecord { get; set; }
    }
}
