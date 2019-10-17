using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class NotificationService: INotification
    {
        #region Declartion 

        /// <summary>
        /// Context
        /// </summary>
        private readonly VMSContext _vContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public NotificationService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion

        /// <summary>
        /// Get Count of Unread Notifications
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int Count(Int32? UserID)
        {
            return _vContext.Notification.Where(x=>x.IsRead == false && x.UserID == UserID).Count();
        }

        #region Notification List

        /// <summary>
        /// Get Notification List
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="UserID"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <returns></returns>
        public IEnumerable<NotificationModel> getNotificationList(string sortBy = "", int pageSize = 0, int? UserID = 0, Int64 skip = 0, string VendorName = "",int? isRead=0)
        {
            try
            {
                
                RepositoryService<NotificationModel> objNotificationModel = new RepositoryService<NotificationModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Notification", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@isRead", isRead, System.Data.SqlDbType.BigInt));

                var data = objNotificationModel.ExecWithStoreProcedure("proc_GetNotificationDet @Skip, @PageSize, @sortBy, @Notification, @UserID,@isRead", parameters.ToArray());
                IEnumerable<NotificationModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        #region Update all Notification as Read
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int UpdateUser(int? UserId, string role)
        {
            int result = 0;

            try
            {
                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@role", role, System.Data.SqlDbType.VarChar));
                result = objUserModel.ExecuteSqlCommand("proc_MarkAllReadNotifications  @UserID,@role", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion
    }
}
