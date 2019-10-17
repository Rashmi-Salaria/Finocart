using Finocart.CustomModel;
using System;
using System.Collections.Generic;

namespace Finocart.Interface
{
    public interface INotification
    {
        /// <summary>
        /// Get Count of Unread Notifications
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int Count(Int32? UserID);

        /// <summary>
        /// Get Notification List
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="UserID"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <returns></returns>
        IEnumerable<NotificationModel> getNotificationList(string sortBy = "", int pageSize = 0, int? UserID = 0, Int64 skip = 0, string VendorName = "", int? isRead = 0);

        /// <summary>
        /// Update all Notification as Read
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        int UpdateUser(int? UserId,string role);
    }
}
