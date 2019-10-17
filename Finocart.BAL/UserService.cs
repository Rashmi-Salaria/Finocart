using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class UserService : IUser
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
        public UserService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion

        #region Method definition

        /// <summary>
        /// Getting user page list
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="UserName"></param>
        /// <param name="UserRole"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> GetUserPageListing(string sortBy, int pageSize, Int64 skip, string UserName, string UserRole, Int32? UserId)
        {
            RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@UserName", UserName, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@RoleAccess", UserRole, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@CompanyID", UserId, System.Data.SqlDbType.BigInt));

            var data = objUserModel.ExecWithStoreProcedure("proc_OutputUser  @Skip, @PageSize, @sortBy, @UserName, @RoleAccess, @CompanyID", parameters.ToArray());
            IEnumerable<UserModel> lstUser = data.ToList();
            return lstUser; 
        }

        /// <summary>
        /// Get role access list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RolesAccessMaster> GetRoleAccessList()
        {
            RepositoryService<RolesAccessMaster> objRolesAccessMaster = new RepositoryService<RolesAccessMaster>(_vContext);
            var Data = objRolesAccessMaster.SelectAll();
            List<RolesAccessMaster> RolesAccessDropDownList = (from a in Data.AsEnumerable()
                                                       select new RolesAccessMaster
                                                       {
                                                           RoleAccessID = a.RoleAccessID,
                                                           Permission = a.Permission

                                                       }).ToList();

            IEnumerable<RolesAccessMaster> lstRoleAccess = Data.ToList();
            return lstRoleAccess;
        }

        /// <summary>
        /// Find user name 
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public User FindUserName(string EmailId, string Password)
        {
            RepositoryService<User> objUser = new RepositoryService<User>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            //Int64 VendorId = 0;

            parameters.Add(SQLHelper.SqlInputParam("@EmailId", EmailId, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@Password", Password, System.Data.SqlDbType.VarChar));
            var data = objUser.ExecWithStoreProcedure("proc_CheckUserLogin @EmailId, @Password", parameters.ToArray());
            User user = data.SingleOrDefault();
            return user;
        }
        #endregion
        #region Insert/Update user reocrd
        /// <summary>
        /// Insert update user record
        /// </summary>
        /// <param name="objUserPage"></param>
        /// <returns></returns>
        public int InsertUpdateUserRecord(UserModel objUserPage)
        {
            int result = 0;

            try
            {
                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserID", objUserPage.UserID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@Name", objUserPage.Name, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Email", objUserPage.Email, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Mobile", objUserPage.Mobile, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Designation", objUserPage.Designation, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@RolesAccess", objUserPage.RoleAccessID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@AccessViewID", objUserPage.AccessViewID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@CreatedBy", objUserPage.CreatedBy, System.Data.SqlDbType.VarChar));//set CreatedBy dynamically
                parameters.Add(SQLHelper.SqlInputParam("@UpdatedBy", objUserPage.UpdatedBy, System.Data.SqlDbType.VarChar));//set UpdatedBy dynamically
                parameters.Add(SQLHelper.SqlInputParam("@Password", objUserPage.Password, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", objUserPage.CreatedBy, System.Data.SqlDbType.VarChar));
                result = objUserModel.ExecuteSqlCommand("proc_AddUpdateUser  @UserID, @Name, @Email, @Mobile, @Designation, @RolesAccess,@CreatedBy,@UpdatedBy,@AccessViewID,@Password,@CompanyID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Get details of user
        /// <summary>
        /// Edit page by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel EditPage(int id)
        {
            UserModel objUserModels = new UserModel();
            try
            {
                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserID", id, System.Data.SqlDbType.Int));
                var data = objUserModel.ExecWithStoreProcedure("proc_EditUser  @UserID", parameters.ToArray());
                IEnumerable<UserModel> lstUser = data.ToList();
                foreach(var v in lstUser)
                {
                    objUserModels.UserID= lstUser.ElementAt(0).UserID;
                    objUserModels.Name = lstUser.ElementAt(0).Name;
                    objUserModels.Mobile = lstUser.ElementAt(0).Mobile;
                    objUserModels.Email = lstUser.ElementAt(0).Email;
                    objUserModels.Designation = lstUser.ElementAt(0).Designation;
                    objUserModels.RolesAccess = lstUser.ElementAt(0).RolesAccess;
                    objUserModels.RoleAccessID = lstUser.ElementAt(0).RoleAccessID;
                    objUserModels.AccessViewID = lstUser.ElementAt(0).AccessViewID;
                    objUserModels.LookupValue = lstUser.ElementAt(0).LookupValue;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objUserModels;
        }
        #endregion

        #region Delete user record
        /// <summary>
        /// delete page by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUserPage(int id)
        {
            int result = 0;

            try
            {
              
                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@UserID", id, System.Data.SqlDbType.Int));
                result = objUserModel.ExecuteSqlCommand("proc_deleteUser  @UserID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        #endregion

        #region Insert Notification on Register Company
        /// <summary>
        /// Insert anchor company document record
        /// </summary>
        /// <param name="AnchorDocument"></param>
        /// <param name="iAnchorCompanyID"></param>
        /// <param name="iDocumentTypeID"></param>
        /// <param name="LocalFolderFileName"></param>
        /// <returns></returns>
        public int AddUserNotificationMessage(int? UserID, string Description, int? RoleID, int? CreatedBy)
        {
            int result = 0;
            try
            {
                RepositoryService<Notification> objNotificationModel = new RepositoryService<Notification>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserId", UserID, System.Data.SqlDbType.BigInt));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@RoleID", RoleID, System.Data.SqlDbType.BigInt));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@Description", Description, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CreatedBy", CreatedBy, System.Data.SqlDbType.VarChar));
                result = objNotificationModel.ExecuteSqlCommand("Proc_InsertNotificationForUser  @UserId,@RoleID,@Description,@CreatedBy", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

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
        public IEnumerable<NotificationModel> getUserNotificationList(string sortBy = "", int pageSize = 0, int? UserID = 0, Int64 skip = 0, string VendorName = "", int? isRead = 0)
        {
            try
            {
               
                RepositoryService<NotificationModel> objUserNotification = new RepositoryService<NotificationModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Notification", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@isRead", isRead, System.Data.SqlDbType.BigInt));

                var data = objUserNotification.ExecWithStoreProcedure("proc_GetUserNotificationDet @Skip, @PageSize, @sortBy, @Notification, @UserID, @isRead", parameters.ToArray());
                IEnumerable<NotificationModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion
    }
}
