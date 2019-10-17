using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
   public class NotificationDetail
    {
        [Key]
        public int BugID { get; set; }

        public string BugType { get; set; }

        public string BugDetail { get; set; }
    }

    public class NotificationType
    {
        public int BugType { get; set; }

        public string FeedbackMsg { get; set; }
    }

    public class GetNotificationDetail
    {
        [Key]
        public Int64 Id { get; set; }

        public string Date { get; set; }

        public string CompanyName { get; set; }

        public string BugType { get; set; }

        public string BugMessage { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
    }
}
