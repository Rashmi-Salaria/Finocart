using Finocart.CustomModel;
using System;
using System.Collections.Generic;
using Finocart.Model;

namespace Finocart.Interface
{
    public interface IUser
    {
        /// <summary>
        /// Get User Page listing
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="UserName"></param>
        /// <param name="UserRole"></param>
        /// <returns></returns>
        IEnumerable<UserModel> GetUserPageListing(string sortBy, int pageSize, Int64 skip, string UserName, string UserRole, Int32? UserId);

        /// <summary>
        /// Get Role access list
        /// </summary>
        /// <returns></returns>
        IEnumerable<RolesAccessMaster> GetRoleAccessList();

        /// <summary>
        /// Insert update records
        /// </summary>
        /// <param name="objUserModel"></param>
        /// <returns></returns>
        int InsertUpdateUserRecord(UserModel objUserModel);

        /// <summary>
        /// Edip page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserModel EditPage(int id);

        /// <summary>
        /// Delete user page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteUserPage(int id);

        /// <summary>
        /// Find user
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        User FindUserName(string EmailId, string Password);

        /// <summary>
        /// Insert User Notification
        /// </summary>
        /// <param name="User Notification"></param>
        /// <returns></returns>
        int AddUserNotificationMessage(int? UserID, string Description, int? RoleID, int? CreatedBy);

        /// <summary>
        /// Get User Notification List
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="UserID"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <returns></returns>
        IEnumerable<NotificationModel> getUserNotificationList(string sortBy = "", int pageSize = 0, int? UserID = 0, Int64 skip = 0, string VendorName = "", int? isRead = 0);
    }
}
