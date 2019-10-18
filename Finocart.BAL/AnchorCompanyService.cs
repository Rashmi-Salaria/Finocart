using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class AnchorCompanyService : IAnchorCompany
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
        public AnchorCompanyService(VMSContext context)
        {
            _vContext = context;
        }



        #endregion

        #region Insert anchor company document record
        /// <summary>
        /// Insert anchor company document record
        /// </summary>
        /// <param name="AnchorDocument"></param>
        /// <param name="iAnchorCompanyID"></param>
        /// <param name="iDocumentTypeID"></param>
        /// <param name="LocalFolderFileName"></param>
        /// <returns></returns>
        public int InsertDocumentDet(IFormFile AnchorDocument, int? iAnchorCompanyID, int iDocumentTypeID, string LocalFolderFileName, Int32? UserId)
        {
            int result = 0;
            try
            {
                RepositoryService<DocumentModel> objDocumentModel = new RepositoryService<DocumentModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyId", iAnchorCompanyID, System.Data.SqlDbType.BigInt));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@FileName", AnchorDocument.FileName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@DocumentTypeID", iDocumentTypeID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@UploadedBy", UserId, System.Data.SqlDbType.Int));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@LocalFolderFileName", LocalFolderFileName, System.Data.SqlDbType.VarChar));
                result = objDocumentModel.ExecuteSqlCommand("Proc_InsertAnchorCompanyDoc  @AnchorCompanyId,@FileName, @DocumentTypeID, @UploadedBy,@LocalFolderFileName", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get anchor company's document list
        /// <summary>
        /// Get anchor company's document list
        /// </summary>
        /// <param name="AnchorCompanyID"></param>
        /// <returns></returns>
        public IEnumerable<DocumentModel> GetAnchorDocList(int? AnchorCompanyID)
        {
            RepositoryService<DocumentModel> objDocumentModel = new RepositoryService<DocumentModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyId", AnchorCompanyID, System.Data.SqlDbType.Int));
            var data = objDocumentModel.ExecWithStoreProcedure("Proc_GetAnchorCompDocument  @AnchorCompanyId", parameters.ToArray());
            IEnumerable<DocumentModel> lstAnchorDocList = data.ToList();
            return lstAnchorDocList;
        }
        #endregion

        #region Get File Name By Id
        /// <summary>
        /// GetFileNameById
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public AnchorCompanyDocument GetFileNameById(Int64 ID)
        {
            AnchorCompanyDocument anchorCompanyDocument = new AnchorCompanyDocument();
            RepositoryService<AnchorCompanyDocument> objAnchorCompanyDocument = new RepositoryService<AnchorCompanyDocument>(_vContext);
            anchorCompanyDocument = objAnchorCompanyDocument.GetByID(ID);
            return anchorCompanyDocument;
        }
        #endregion

        #region Delete anchor company document record
        /// <summary>
        /// Delete anchor company document record
        /// </summary>
        /// <param name="anchorCompanyDocument"></param>
        /// <returns></returns>
        public int DeleteAnchorCompDocRecord(AnchorCompanyDocument anchorCompanyDocument)
        {
            int result = 0;
            try
            {
                RepositoryService<AnchorCompanyDocument> objAnchorCompanyDocument = new RepositoryService<AnchorCompanyDocument>(_vContext);
                result = objAnchorCompanyDocument.Delete(anchorCompanyDocument);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Get anchor listing by vendor id
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <param name="Page"></param>
        /// <param name="IsTotalCount"></param>
        /// <returns></returns>
        public IEnumerable<AnchorCompListingModel> GetAnchorCompListingByVendorId(int? VendorId, string sortBy, int pageSize, Int64 skip, string CompanyID, string CompanyName, string TotalInvoiceAmt, string Page, int IsTotalCount)
        {
            try
            {
                RepositoryService<AnchorCompListingModel> objAnchorCompListingModel = new RepositoryService<AnchorCompListingModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Company_ID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Company_name", CompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAwaitedInvAmt", TotalInvoiceAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@IsTotalCount", IsTotalCount, System.Data.SqlDbType.Int));
                var data = objAnchorCompListingModel.ExecWithStoreProcedure("Proc_GetAnchorCompListing  @Page, @VendorID, @Company_ID, @Company_name, @TotalAppInvAmt, @Skip, @PageSize, @sortBy, @IsTotalCount, @TotalAwaitedInvAmt", parameters.ToArray());
                IEnumerable<AnchorCompListingModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get anchor dashboard list by vendor id
        /// </summary
        /// 
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <param name="Page"></param>
        /// <param name="IsTotalCount"></param>
        /// <returns></returns>
        public IEnumerable<AnchorCompListingModel> GetAnchordashboardCompListingByVendorId(int? VendorId, string sortBy, int pageSize, Int64 skip, string CompanyID, string CompanyName, string TotalInvoiceAmt, string Page, int IsTotalCount)
        {
            try
            {
                RepositoryService<AnchorCompListingModel> objAnchorCompListingModel = new RepositoryService<AnchorCompListingModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Company_ID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Company_name", CompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAwaitedInvAmt", TotalInvoiceAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@IsTotalCount", IsTotalCount, System.Data.SqlDbType.Int));
                var data = objAnchorCompListingModel.ExecWithStoreProcedure("Proc_GetAnchorDashboardCompListing  @Page, @AnchCompanyID, @Company_ID, @Company_name, @TotalAppInvAmt, @Skip, @PageSize, @sortBy, @IsTotalCount, @TotalAwaitedInvAmt", parameters.ToArray());
                IEnumerable<AnchorCompListingModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get vendor invoice list by company id
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IEnumerable<CompanyInvoiceListModel> GetVendorInvoiceListByCompanyID(int CompanyID, string sortBy, int pageSize, Int64 skip)
        {
            try
            {
                RepositoryService<CompanyInvoiceListModel> objCompanyInvoiceListModel = new RepositoryService<CompanyInvoiceListModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objCompanyInvoiceListModel.ExecWithStoreProcedure("Proc_GetVendorInvoiceList  @CompanyID, @sortBy, @PageSize, @Skip", parameters.ToArray());
                IEnumerable<CompanyInvoiceListModel> lstInvoiceVendorCompList = data.ToList();
                return lstInvoiceVendorCompList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Getting vendor list
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyName"></param>
        /// <param name="InAmount"></param>
        /// <returns></returns>
        public IEnumerable<VendorlistModel> GetVendorListing(int CompanyID, string sortBy, int pageSize, Int64 skip, string CompanyName, string ContactNo, string EmailId, string VendorStatus)
        {
            try
            {
                RepositoryService<VendorlistModel> objVendorlistModel = new RepositoryService<VendorlistModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", CompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ContactNo", ContactNo, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@EmailId", EmailId, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorStatus", VendorStatus, System.Data.SqlDbType.VarChar));
                var data = objVendorlistModel.ExecWithStoreProcedure("Proc_GetVendorList  @CompanyID, @sortBy, @PageSize, @Skip, @CompanyName,@ContactNo,@EmailId,@VendorStatus", parameters.ToArray());
                IEnumerable<VendorlistModel> lstInvoiceVendorCompList = data.ToList();
                return lstInvoiceVendorCompList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Getting dashboard listing
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <param name="Page"></param>
        /// <param name="IsTotalCount"></param>
        /// <returns></returns>
        public IEnumerable<DashboardListModel> GetDashboardList(int? VendorId, string sortBy, int pageSize, Int64 skip, string CompanyID, string CompanyName, string TotalInvoiceAmt, string Page, int IsTotalCount)
        {
            try
            {
                RepositoryService<DashboardListModel> objDashboardListModel = new RepositoryService<DashboardListModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Company_name", CompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objDashboardListModel.ExecWithStoreProcedure("Proc_GetDashboardList  @Page, @VendorID, @Company_name, @TotalAppInvAmt, @Skip, @PageSize, @sortBy ", parameters.ToArray());
                IEnumerable<DashboardListModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Critical Vendors
        /// <summary>
        /// Get Critical Vendors List
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <param name="TotalInvAmtLimit"></param>
        /// <returns></returns>
        public IEnumerable<CriticalVendorsModel> GetCriticalVendors(Int32? CompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string TotalInvAmtLimit)
        {
            try
            {
                RepositoryService<CriticalVendorsModel> objCriticalVendorsModel = new RepositoryService<CriticalVendorsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Company_ID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorName", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalInvAmtLimit", TotalInvAmtLimit, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objCriticalVendorsModel.ExecWithStoreProcedure("Proc_GetCriticalVendorsListing  @Company_ID, @VendorName, @TotalInvAmtLimit, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<CriticalVendorsModel> lstCriticalVendors = data.ToList();
                return lstCriticalVendors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Vendors Drop Down
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public IEnumerable<VendorsDropDownModel> GetVendorsDropDown(Int32? CompanyID)
        {
            try
            {
                RepositoryService<VendorsDropDownModel> objVendorsDropDownModel = new RepositoryService<VendorsDropDownModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Company_ID", CompanyID, System.Data.SqlDbType.VarChar));
                var data = objVendorsDropDownModel.ExecWithStoreProcedure("proc_VendorsDropDown  @Company_ID", parameters.ToArray());
                IEnumerable<VendorsDropDownModel> lstVendorsDropDown = data.ToList();
                return lstVendorsDropDown;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert And Update Critical Vendors
        /// </summary>
        /// <param name="objCriticalVendors"></param>
        /// <returns></returns>
        public int InsertUpdateCriticalVendor(CriticalVendorsModel objCriticalVendors)
        {
            int result = 0;

            try
            {
                RepositoryService<CriticalVendorsModel> objCriticalVendorsModel = new RepositoryService<CriticalVendorsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", objCriticalVendors.CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@InvAmtLimitStatus", objCriticalVendors.Status, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PercentageRate", objCriticalVendors.PercentageRate, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceLimitAmt", objCriticalVendors.InvoiceLimitAmt, System.Data.SqlDbType.BigInt));
                result = objCriticalVendorsModel.ExecuteSqlCommand("proc_AddUpdateCriticalVendors  @CompanyID, @InvAmtLimitStatus, @PercentageRate, @InvoiceLimitAmt", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Delete Critical Vendors
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCriticalVendors(int id)
        {
            int result = 0;

            try
            {

                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", id, System.Data.SqlDbType.Int));
                result = objUserModel.ExecuteSqlCommand("proc_deleteCriticalVendors  @CompanyID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion
        /// <summary>
        /// Get Count of Unread Notifications
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int Count(Int32? UserID)
        {
            return _vContext.Notification.Where(x => x.IsRead == false && x.UserID == UserID).Count();
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
        public IEnumerable<NotificationModel> getAnchorNotificationList(string sortBy = "", int pageSize = 0, int? UserID = 0, Int64 skip = 0, string VendorName = "", int? isRead = 0)
        {
            try
            {
                //Int64 i = 0;
                RepositoryService<NotificationModel> objAnchorNotification = new RepositoryService<NotificationModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Notification", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@isRead", isRead, System.Data.SqlDbType.BigInt));

                var data = objAnchorNotification.ExecWithStoreProcedure("proc_AnchorGetNotificationDet @Skip, @PageSize, @sortBy, @Notification, @UserID, @isRead", parameters.ToArray());
                IEnumerable<NotificationModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<NotificationModel> getBankNotificationList(string sortBy = "", int pageSize = 0, int? UserID = 0, Int64 skip = 0, string VendorName = "", int? isRead = 0)
        {
            try
            {
                //Int64 i = 0;
                RepositoryService<NotificationModel> objAnchorNotification = new RepositoryService<NotificationModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Notification", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@isRead", isRead, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@RoleId", 25, System.Data.SqlDbType.Int));
                var data = objAnchorNotification.ExecWithStoreProcedure("proc_BankGetNotificationDet @Skip, @PageSize, @sortBy, @Notification, @UserID, @isRead, @RoleId", parameters.ToArray());
                IEnumerable<NotificationModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #region Update all Anchor Notification as Read

        /// <summary>
        /// Anchor update user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int AnchorUpdateUser(int? UserId)
        {
            int result = 0;

            try
            {
                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserId, System.Data.SqlDbType.BigInt));
                result = objUserModel.ExecuteSqlCommand("proc_MarkAllReadAnchorNotifications  @UserID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion


        #region Invoice Details
        /// <summary>
        /// Get Payment Due Today List
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <param name="TotalAppInvAmt"></param>
        /// <returns></returns>
        public IEnumerable<VendorPaymentDueModel> GetPaymentDueToday(Int32? CompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string TotalAppInvAmt)
        {
            try
            {
                RepositoryService<VendorPaymentDueModel> objVendorPaymentDueModel = new RepositoryService<VendorPaymentDueModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorName", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", TotalAppInvAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorPaymentDueModel.ExecWithStoreProcedure("proc_GetVendorPaymentDueListing  @AnchCompanyID, @VendorName, @TotalAppInvAmt, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<VendorPaymentDueModel> lstPaymentDueVendors = data.ToList();
                return lstPaymentDueVendors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Payment Due Today View
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="InvoiceID"></param>
        /// <param name="InvoiceAmt"></param>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IEnumerable<VendorPaymentDueViewModel> GetPaymentDueTodayView(Int32? CompanyID, string sortBy, int pageSize, Int64 skip, string InvoiceID, string InvoiceAmt, Int64 VendorID)
        {
            try
            {
                RepositoryService<VendorPaymentDueViewModel> objVendorPaymentDueViewModel = new RepositoryService<VendorPaymentDueViewModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceID", InvoiceID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceAmt", InvoiceAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorPaymentDueViewModel.ExecWithStoreProcedure("proc_GetVendorPaymentDueViewListing  @AnchCompanyID, @VendorID, @InvoiceID, @InvoiceAmt, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<VendorPaymentDueViewModel> lstPaymentDueVendorsView = data.ToList();
                return lstPaymentDueVendorsView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Awaited Invoice Approval
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <param name="TotalAppInvAmt"></param>
        /// <returns></returns>
        public IEnumerable<VendorAwaitedInvApprovalModel> GetAwaitedInvoiceApproval(Int64? CompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string TotalAppInvAmt)
        {
            try
            {
                RepositoryService<VendorAwaitedInvApprovalModel> objVendorAwaitedInvApprovalModel = new RepositoryService<VendorAwaitedInvApprovalModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@VendorName", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", TotalAppInvAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.BigInt));
                var data = objVendorAwaitedInvApprovalModel.ExecWithStoreProcedure("proc_GetVendorInvAwaitedAppListing  @AnchCompanyID,@VendorName,@TotalAppInvAmt,@Skip,@PageSize,@sortBy", parameters.ToArray());
                IEnumerable<VendorAwaitedInvApprovalModel> lstAwaitedInvoiceVendors = data.ToList();
                return lstAwaitedInvoiceVendors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Awaited Invoice Approval View
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="VendorID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="BucketID"></param>
        /// <param name="TotalAppInvAmt"></param>
        /// <returns></returns>
        public IEnumerable<VendorBucketAwaitedInvViewModel> GetAwaitedInvoiceApprovalView(Int32? CompanyID, int VendorID, string sortBy, int pageSize, Int64 skip, string BucketID, string TotalAppInvAmt)
        {
            try
            {
                RepositoryService<VendorBucketAwaitedInvViewModel> objVendorBucketAwaitedInvViewModel = new RepositoryService<VendorBucketAwaitedInvViewModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@BucketID", BucketID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", TotalAppInvAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorBucketAwaitedInvViewModel.ExecWithStoreProcedure("proc_GetVendorInvAwaitedAppViewListing  @AnchCompanyID, @VendorID, @BucketID, @TotalAppInvAmt, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<VendorBucketAwaitedInvViewModel> lstAwaitedInvVendorsView = data.ToList();
                return lstAwaitedInvVendorsView;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Data is Null. This method or property cannot be called on Null values.")
                {
                    return null;
                }
                throw ex;
            }
        }

        /// <summary>
        /// Ge tBucket Wise Invoice View
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="VendorID"></param>
        /// <param name="BucketID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="InvoiceID"></param>
        /// <param name="POID"></param>
        /// <returns></returns>
        public IEnumerable<VendorBucketInvoiceViewModel> GetBucketWiseInvoiceView(Int32? CompanyID, int VendorID, Int64 BucketID, string sortBy, int pageSize, Int64 skip, string InvoiceID, string POID)
        {
            try
            {
                RepositoryService<VendorBucketInvoiceViewModel> objVendorBucketInvoiceViewModel = new RepositoryService<VendorBucketInvoiceViewModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@BucketID", BucketID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceID", InvoiceID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@POID", POID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorBucketInvoiceViewModel.ExecWithStoreProcedure("proc_GetVendorBucketInvoiceViewListing  @AnchCompanyID, @VendorID, @BucketID, @InvoiceID, @POID, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<VendorBucketInvoiceViewModel> lstBucketInvoiceView = data.ToList();
                return lstBucketInvoiceView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Invoice Approved Today
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <param name="TotalAppInvAmt"></param>
        /// <returns></returns>
        public IEnumerable<VendorInvoiceApprovedTodayModel> GetInvoiceApprovedToday(Int32? CompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string TotalAppInvAmt)
        {
            try
            {
                RepositoryService<VendorInvoiceApprovedTodayModel> objVendorInvoiceApprovedTodayModel = new RepositoryService<VendorInvoiceApprovedTodayModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorName", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", TotalAppInvAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorInvoiceApprovedTodayModel.ExecWithStoreProcedure("proc_GetVendorINVApprovedTodayListing  @AnchCompanyID, @VendorName, @TotalAppInvAmt, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<VendorInvoiceApprovedTodayModel> lstInvoiceApprovedToday = data.ToList();
                return lstInvoiceApprovedToday;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Invoice Approved Today View
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="VendorID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="BucketID"></param>
        /// <param name="TotalAppInvAmt"></param>
        /// <returns></returns>
        public IEnumerable<VendorInvApproveTodayViewModel> GetInvoiceApprovedTodayView(Int32? CompanyID, int VendorID, string sortBy, int pageSize, Int64 skip, string BucketID, string TotalAppInvAmt)
        {
            try
            {
                RepositoryService<VendorInvApproveTodayViewModel> objVendorInvApproveTodayViewModel = new RepositoryService<VendorInvApproveTodayViewModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@BucketID", BucketID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", TotalAppInvAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorInvApproveTodayViewModel.ExecWithStoreProcedure("proc_GetVendorApprovedTodayViewListing  @AnchCompanyID, @VendorID, @BucketID, @TotalAppInvAmt, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<VendorInvApproveTodayViewModel> lstInvApproveTodayView = data.ToList();
                return lstInvApproveTodayView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get BucketWise Discount Invoice View
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="VendorID"></param>
        /// <param name="BucketID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="InvoiceID"></param>
        /// <param name="POID"></param>
        /// <returns></returns>
        public IEnumerable<VendorBucketWiseDiscInvViewModel> GetBucketWiseDiscInvView(Int32? CompanyID, int VendorID, Int64 BucketID, string sortBy, int pageSize, Int64 skip, string InvoiceID, string POID)
        {
            try
            {
                RepositoryService<VendorBucketWiseDiscInvViewModel> objVendorBucketWiseDiscInvViewModel = new RepositoryService<VendorBucketWiseDiscInvViewModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchCompanyID", CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@BucketID", BucketID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceID", InvoiceID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@POID", POID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorBucketWiseDiscInvViewModel.ExecWithStoreProcedure("proc_GetVendorBucketWiseDiscInvViewListing  @AnchCompanyID, @VendorID, @BucketID, @InvoiceID, @POID, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<VendorBucketWiseDiscInvViewModel> lstBucketInvoiceView = data.ToList();
                return lstBucketInvoiceView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Anchor Company Graph Details
        /// </summary>
        /// <param name="month"></param>
        /// <param name="AnchorCompId"></param>
        /// <returns></returns>
        public IEnumerable<GraphDetailsModel> GetGraphDetails(string month, Int64? AnchorCompId)
        {
            try
            {
                RepositoryService<GraphDetailsModel> objGraphDetailsModel = new RepositoryService<GraphDetailsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SP_TYPE", month, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", AnchorCompId, System.Data.SqlDbType.VarChar));
                var data = objGraphDetailsModel.ExecWithStoreProcedure("Proc_getAnchorGraphDet  @SP_TYPE, @AnchorCompID", parameters.ToArray());
                IEnumerable<GraphDetailsModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserting vendor record
        /// </summary>
        /// <param name="VendorDetails"></param>
        /// <param name="AnchorCompId"></param>
        /// <param name="randomPassword"></param>
        /// <returns></returns>
        public int InsertVendorRecord(DataRow VendorDetails, Int64? AnchorCompId, string randomPassword)
        {
            int result = 0;

            try
            {
                RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", AnchorCompId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@VendorName", VendorDetails[0].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Password", randomPassword, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PanNumber", VendorDetails[1].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@MIME", VendorDetails[2].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UANNumber", VendorDetails[3].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ContactPersonName", VendorDetails[4].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ContactPersonDesignation", VendorDetails[5].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@EmailID", VendorDetails[6].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ContactNumber", VendorDetails[7].ToString(), System.Data.SqlDbType.VarChar));
                result = objCompany.ExecuteSqlCommand("proc_InsertVendorData  @AnchorCompID, @VendorName, @Password, @PanNumber, @MIME, @UANNumber, @ContactPersonName, @ContactPersonDesignation, @EmailID, @ContactNumber", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Inseting invoice record
        /// </summary>
        /// <param name="VendorDetails"></param>
        /// <param name="AnchorCompId"></param>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public int InsertInvoiceRecord(IRow VendorDetails, Int64? AnchorCompId)
        {
            int result = 0;

            try
            {
                RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", AnchorCompId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PONumber", VendorDetails.Cells[0].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PODate", VendorDetails.Cells[1].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceNo", VendorDetails.Cells[2].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceAmt", VendorDetails.Cells[3].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PaymentDueDate", VendorDetails.Cells[4].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ApprovedAmt", VendorDetails.Cells[5].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PaymentDays", VendorDetails.Cells[6].ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PanNumber", VendorDetails.Cells[7].ToString(), System.Data.SqlDbType.VarChar));
                result = objCompany.ExecuteSqlCommand("proc_InsertInvoiceData  @PONumber, @PODate, @InvoiceNo, @AnchorCompID, @InvoiceAmt, @PaymentDueDate, @ApprovedAmt, @PaymentDays, @PanNumber", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public Company CheckEmailID(string PanNumber)
        {
            RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
            Company company = objCompany.SelectAll().Where(x => x.Pan_number == PanNumber).FirstOrDefault();
            return company;
        }
        #endregion

        #region Analytics
        /// <summary>
        /// Getting vendor listing
        /// </summary>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        public IEnumerable<AnchorCompDropDownModel> getVendorList(Int64? VendorId)
        {
            RepositoryService<AnchorCompDropDownModel> objAnchorCompDropDownModel = new RepositoryService<AnchorCompDropDownModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", VendorId, System.Data.SqlDbType.Int));
            var data = objAnchorCompDropDownModel.ExecWithStoreProcedure("poc_getVendorDropDown @AnchorCompID", parameters.ToArray());
            IEnumerable<AnchorCompDropDownModel> AnchorCompDetails = data.ToList();
            return AnchorCompDetails;
        }

        /// <summary>
        /// getting lost opportunities
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IEnumerable<AnchorAnalyticLostOppModel> GetLostOpportunity(int? VendorId, string sortBy, int pageSize, Int64 skip, int companyID, string fromDate, string toDate)
        {
            try
            {
                RepositoryService<AnchorAnalyticLostOppModel> objAnchorAnalyticLostOppModel = new RepositoryService<AnchorAnalyticLostOppModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", companyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FromDate", fromDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", toDate, System.Data.SqlDbType.VarChar));
                var data = objAnchorAnalyticLostOppModel.ExecWithStoreProcedure("Proc_GetAnchorLostOppListing  @AnchorCompID, @Skip, @PageSize, @sortBy, @CompanyID, @FromDate, @ToDate", parameters.ToArray());
                IEnumerable<AnchorAnalyticLostOppModel> lstLostOpportunities = data.ToList();
                return lstLostOpportunities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Getting invoice status
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="AnchorCompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceStatus> GetInvoiceStatuses(int? VendorId, int? AnchorCompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string Invoicetotatalamt)
        {

            try
            {
                RepositoryService<InvoiceStatus> objInvoiceStatus = new RepositoryService<InvoiceStatus>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", AnchorCompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorName", VendorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Invoicetotatalamt", Invoicetotatalamt, System.Data.SqlDbType.VarChar));
                var data = objInvoiceStatus.ExecWithStoreProcedure("proc_GetAnchorInvoiceStatus  @Skip, @PageSize, @VendorID, @AnchorCompID,@SortBy,@VendorName,@Invoicetotatalamt", parameters.ToArray());
                IEnumerable<InvoiceStatus> lstInvoiceVendorCompList = data.ToList();
                return lstInvoiceVendorCompList;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Data is Null. This method or property cannot be called on Null values.")
                {
                    return null;
                }

                throw ex;
            }
        }

        /// <summary>
        /// Get earning by vendor
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDates"></param>
        /// <returns></returns>
        public IEnumerable<Earning_Vendorwise> GetEarning_Vendorwises(int? VendorId, string sortBy, int pageSize, Int64 skip, DateTime fromDate, DateTime toDates)
        {
            try
            {
                RepositoryService<Earning_Vendorwise> repositoryService = new RepositoryService<Earning_Vendorwise>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorId", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FromDate", fromDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", toDates, System.Data.SqlDbType.VarChar));
                var data = repositoryService.ExecWithStoreProcedure("Proc_GetEarningListing @VendorId,@Skip,@PageSize,@sortBy,@FromDate,@ToDate", parameters.ToArray());
                IEnumerable<Earning_Vendorwise> earning_Vendorwises = data.ToList();
                return earning_Vendorwises;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Getting earning discount vise
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDates"></param>
        /// <returns></returns>
        public IEnumerable<Eearning_discountRateWise> eearning_DiscountRateWises(int? VendorId, int? AnchorCompId, string sortBy, int pageSize, Int64 skip, string fromDate, string toDates)
        {
            try
            {
                RepositoryService<Eearning_discountRateWise> objEearning_discountRateWise = new RepositoryService<Eearning_discountRateWise>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorId", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@AncCompanyID", AnchorCompId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FromDate", fromDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", toDates, System.Data.SqlDbType.VarChar));
                var data = objEearning_discountRateWise.ExecWithStoreProcedure("Proc_GetAmountDiscountRatewise @VendorId,@AncCompanyID,@Skip,@PageSize,@sortBy,@FromDate,@ToDate", parameters.ToArray());
                IEnumerable<Eearning_discountRateWise> _DiscountRateWises = data.ToList();
                return _DiscountRateWises;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        /// <summary>
        /// GEtting discount rate vise in percentage
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDates"></param>
        /// <returns></returns>
        public IEnumerable<Eearning_discountRateWise> discountRateWisesinPercent(int? VendorId, int? AnchorCompID, string sortBy, int pageSize, Int64 skip, string fromDate, string toDates)
        {
            try
            {
                RepositoryService<Eearning_discountRateWise> repositoryService = new RepositoryService<Eearning_discountRateWise>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorId", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@AncCompanyID", AnchorCompID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FromDate", fromDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", toDates, System.Data.SqlDbType.VarChar));
                var data = repositoryService.ExecWithStoreProcedure("Proc_GetAmountDiscountRatewiseInPercent @VendorId,@AncCompanyID,@Skip,@PageSize,@sortBy,@FromDate,@ToDate", parameters.ToArray());
                IEnumerable<Eearning_discountRateWise> percentdiscountratewise = data.ToList();
                return percentdiscountratewise;

            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region Insert Notification on Register Company
        /// <summary>
        /// Insert anchor company document record
        /// </summary>
        /// <param name="AnchorDocument"></param>
        /// <param name="iAnchorCompanyID"></param>
        /// <param name="iDocumentTypeID"></param>
        /// <param name="LocalFolderFileName"></param>
        /// <returns></returns>
        public int AddNotificationMessage(int? iAnchorCompanyID, string Description, int? RoleID, int? UserID)
        {
            int result = 0;
            try
            {
                RepositoryService<AnchorNotification> objDocumentModel = new RepositoryService<AnchorNotification>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyId", iAnchorCompanyID, System.Data.SqlDbType.BigInt));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@RoleID", RoleID, System.Data.SqlDbType.BigInt));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@Description", Description, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.VarChar));
                result = objDocumentModel.ExecuteSqlCommand("Proc_InsertNotificationAnchorCompany  @CompanyId,@RoleID,@Description,@UserID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public int AddNotificationMessageForBank(int? iAnchorCompanyID, string Description, int? RoleID, int? UserID)
        {
            int result = 0;
            try
            {
                RepositoryService<AnchorNotification> objDocumentModel = new RepositoryService<AnchorNotification>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyId", iAnchorCompanyID, System.Data.SqlDbType.BigInt));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@RoleID", RoleID, System.Data.SqlDbType.BigInt));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@Description", Description, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.VarChar));
                result = objDocumentModel.ExecuteSqlCommand("Proc_InsertNotificationAnchorCompany  @CompanyId,@RoleID,@Description,@UserID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        //public int InsertExcelPath(string fullPath, string FileName, Int64? AnchCompanyId, string CompanyName, string Upload)
        //{
        //    int result = 0;

        //    try
        //    {
        //        RepositoryService<UploadExcelPaths> objCompany = new RepositoryService<UploadExcelPaths>(_vContext);
        //        ICollection<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(SQLHelper.SqlInputParam("@FullPath", fullPath, System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@FileName", FileName, System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@Upload", Upload, System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", AnchCompanyId, System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@CompanyName", CompanyName, System.Data.SqlDbType.VarChar));
        //        result = objCompany.ExecuteSqlCommand("proc_InsertExcelData  @FullPath, @FileName, @Upload, @AnchorCompID, @CompanyName", parameters.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return result;
        //}

        /// <summary>
        /// Update Rule of Engine
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int UpdateRuleOfEngine(int? iAnchorCompanyID, string RulePercentage, string RuleLimit, string Internalfund, string FromBank, string chkUnlimited, string AnchorRate)
        {
            int result = 0;

            try
            {
                RepositoryService<AutoFunding> objAutoFunding = new RepositoryService<AutoFunding>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", iAnchorCompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@RulePercentage", RulePercentage, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@RuleLimit", RuleLimit, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@Internalfund", Internalfund, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@FromBank", FromBank, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorRate", AnchorRate, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@IsUnlimited", chkUnlimited, System.Data.SqlDbType.BigInt));

                result = objAutoFunding.ExecuteSqlCommand("proc_UpdateRuleofEngineData @CompanyID, @RulePercentage, @RuleLimit, @Internalfund, @FromBank, @AnchorRate, @IsUnlimited", parameters.ToArray());


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public RuleofEngineDetails GetRuleofEngineData(int? iAnchorCompanyID)
        {

            RepositoryService<RuleofEngineDetails> objFinocartMaster = new RepositoryService<RuleofEngineDetails>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SQLHelper.SqlInputParam("@CompanyID", iAnchorCompanyID, System.Data.SqlDbType.Int));
            var data = objFinocartMaster.ExecWithStoreProcedure("proc_GetRuleOfEngineData @CompanyID", parameters.ToArray());
            RuleofEngineDetails SuperAdmin = data.SingleOrDefault();
            return SuperAdmin;
        }

        public IEnumerable<UploadLogsModel> GetVendorInvoiceLogs(string sortBy, int pageSize, Int64 skip, Int32? CompanyID, string Upload)
        {
            try
            {
                RepositoryService<UploadLogsModel> objUploadLogsModel = new RepositoryService<UploadLogsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Upload", Upload, System.Data.SqlDbType.VarChar));
                var data = objUploadLogsModel.ExecWithStoreProcedure("proc_GetExcelLogs  @AnchorCompID, @sortBy, @PageSize, @Skip, @Upload", parameters.ToArray());
                IEnumerable<UploadLogsModel> lstUploadLogs = data.ToList();
                return lstUploadLogs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<AnchorVendorBankCount> GetVendorAnchorBankCount(string noofcompanies)
        {

            RepositoryService<AnchorVendorBankCount> objFinocartMaster = new RepositoryService<AnchorVendorBankCount>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SQLHelper.SqlInputParam("@personcompany", noofcompanies, System.Data.SqlDbType.VarChar));
            var data = objFinocartMaster.ExecWithStoreProcedure("proc_GetVendorAnchorCount @personcompany", parameters.ToArray());
            List<AnchorVendorBankCount> SuperAdmin = data.ToList();
            return SuperAdmin;
        }

        public UploadExcelPath GetUploadDetils(Int64 ID)
        {
            RepositoryService<UploadExcelPath> objCompany = new RepositoryService<UploadExcelPath>(_vContext);
            UploadExcelPath company = objCompany.SelectAll().Where(x => x.ID == ID).FirstOrDefault();
            return company;
        }

        public IEnumerable<AnchorListModel> GetVendorAnchorListing(int CompanyID, string sortBy, int pageSize, Int64 skip, string CompanyName, string InAmount)
        {
            try
            {
                RepositoryService<AnchorListModel> objVendorlistModel = new RepositoryService<AnchorListModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", CompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InAmount", InAmount, System.Data.SqlDbType.VarChar));
                var data = objVendorlistModel.ExecWithStoreProcedure("Proc_GetVendorAnchorList  @CompanyID, @sortBy, @PageSize, @Skip, @CompanyName, @InAmount", parameters.ToArray());
                IEnumerable<AnchorListModel> lstInvoiceVendorCompList = data.ToList();
                return lstInvoiceVendorCompList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<HolidayListModel> GetAnchorHolidayListing(string sortBy, int pageSize, Int64 skip, string Reason,int? UserID)
        {
            //{ Finocart.Services.RepositoryService<Finocart.Model.HolidayList>}

            try
            {
                RepositoryService<HolidayListModel> objHolidayListModel = new RepositoryService<HolidayListModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Reason", "" + Reason + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", "" + UserID + "", System.Data.SqlDbType.Int));
                var data = objHolidayListModel.ExecWithStoreProcedure("Get_HolidayList_Details @sortBy,@PageSize,@Skip,@Reason,@UserID", parameters.ToArray());
                IEnumerable<HolidayListModel> lsHolidayList = data.ToList();
                return lsHolidayList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertHolidayListRecord(HolidayList objHolidayList)
        {
            int result = 0;

            try
            {

                RepositoryService<HolidayList> objHolidayL= new RepositoryService<HolidayList>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Date", objHolidayList.Date, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@reason", objHolidayList.Reason, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CreatedBy", objHolidayList.CreatedBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CreatedDate", objHolidayList.CreatedDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UpdatedBy", objHolidayList.UpdatedBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UpdatedDate", objHolidayList.UpdatedDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@IsDelete", objHolidayList.IsDelete, System.Data.SqlDbType.Bit));

                result = objHolidayL.ExecuteSqlCommand("proc_InsertHolidayListData  @Date, @reason,@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@IsDelete", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public int UpdateUTRDetails(Int64 InvoiceID, string UTRDetails)
        {
            int result = 0;

            try
            {
                RepositoryService<SetBankAmountLimit> obj = new RepositoryService<SetBankAmountLimit>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceID",InvoiceID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@UTRDetails",UTRDetails, System.Data.SqlDbType.VarChar));


                result = obj.ExecuteSqlCommand("proc_UpdateInvoiceUTRDetails @InvoiceID, @UTRDetails", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Get Anchor Company Graph Details
        /// </summary>
        /// <param name="AnchorCompId"></param>
        /// <param name="vendorId"></param>
        /// <param name="DataTypeId"></param>
        /// <returns></returns>
        public IEnumerable<UpcomingPayableGraphModel> GetUpcomingPayableGraphData(Int64? AnchorCompId, Int64? vendorId, Int64? DataTypeId)
        {
            try
            {
                RepositoryService<UpcomingPayableGraphModel> objGraphDetailsModel = new RepositoryService<UpcomingPayableGraphModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@P_AnchorCompID", AnchorCompId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@P_VendorId", vendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@P_DataTypeId", DataTypeId, System.Data.SqlDbType.Int));
                var data = objGraphDetailsModel.ExecWithStoreProcedure("proc_GetUpcomingPayableChartData @P_AnchorCompID,@P_VendorId,@P_DataTypeId", parameters.ToArray());
                IEnumerable<UpcomingPayableGraphModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Anchor Company Graph Details
        /// </summary>
        /// <param name="AnchorCompId"></param>
        /// <param name="DataTypeId"></param>
        /// <returns></returns>
        public IEnumerable<VendorDropdownModel> GetVendorsForAnchor(Int64? AnchorCompId, Int64? DataTypeId)
        {
            try
            {
                RepositoryService<VendorDropdownModel> vendorDropdownModel = new RepositoryService<VendorDropdownModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@P_AnchorCompID", AnchorCompId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@P_DataTypeId", DataTypeId, System.Data.SqlDbType.Int));
                //parameters.Add(SQLHelper.SqlInputParam("@P_VendorId", vendorId, System.Data.SqlDbType.Int));
                var data = vendorDropdownModel.ExecWithStoreProcedure("proc_GetVendorsListForInvoices @P_AnchorCompID,@P_DataTypeId", parameters.ToArray());
                IEnumerable<VendorDropdownModel> lstVendors = data.ToList();
                return lstVendors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertNotificationDetail(string BugType, string BugDetail, string CompanyName, int CompanyId)
        {
            int result = 0;
            try
            {
                RepositoryService<NotificationDetail> obj = new RepositoryService<NotificationDetail>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@BugType", BugType, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BugDetail", BugDetail, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", CompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyId", CompanyId, System.Data.SqlDbType.Int));

                result = obj.ExecuteSqlCommand("proc_InsertBugNotification @BugType, @BugDetail,@CompanyName,@CompanyId", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }
        public IEnumerable<GetNotificationDetail> GetNotificationList(string sortBy, int pageSize, Int64 skip, string SearchFieldName)
        {
           

            try
            {
                RepositoryService<GetNotificationDetail> objNotificationList = new RepositoryService<GetNotificationDetail>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SearchFieldName", "" + SearchFieldName + "", System.Data.SqlDbType.VarChar));
                var data = objNotificationList.ExecWithStoreProcedure("proc_GetBugNotification @sortBy,@PageSize,@Skip,@SearchFieldName", parameters.ToArray());
                IEnumerable<GetNotificationDetail> lstNotificationList = data.ToList();
                return lstNotificationList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SetLimitHistory> GetSetLimitHistory(string sortBy, int pageSize, Int64 skip,string SearchFieldName,int? CompanyID)
        {


            try
            {
                RepositoryService<SetLimitHistory> objSetLimitHistoryList = new RepositoryService<SetLimitHistory>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SearchFieldName", "" + SearchFieldName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", "" + CompanyID + "", System.Data.SqlDbType.Int));
                var data = objSetLimitHistoryList.ExecWithStoreProcedure("proc_GetSetlimitHistoryList @sortBy,@PageSize,@Skip,@SearchFieldName,@CompanyID", parameters.ToArray());
                IEnumerable<SetLimitHistory> lstSetLimitHistory = data.ToList();
                return lstSetLimitHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<AuditTrailHistory> GetAuditTrailHistory(string sortBy, int pageSize, Int64 skip, string SearchFieldName, string TableName, string ColumnName, string FormDate2, string ToDate2)
        {


            try
            {
                RepositoryService<AuditTrailHistory> objGetAuditTrailHistory = new RepositoryService<AuditTrailHistory>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SearchFieldName", "" + SearchFieldName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TableName", "" + TableName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ColumnName", "" + ColumnName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FormDate", "" + FormDate2 + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", "" + ToDate2 + "", System.Data.SqlDbType.VarChar));
              
                var data = objGetAuditTrailHistory.ExecWithStoreProcedure("proc_GetAuditTrailHistory @sortBy,@PageSize,@Skip,@SearchFieldName,@TableName,@ColumnName,@FormDate,@ToDate", parameters.ToArray());
                IEnumerable<AuditTrailHistory> lstGetAuditTrailHistory = data.ToList();
                return lstGetAuditTrailHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public IEnumerable<TablesName> GetTablesName()
        {
            try
            {
                RepositoryService<TablesName> objGetTableName = new RepositoryService<TablesName>(_vContext);

                var data = objGetTableName.ExecWithStoreProcedure("proc_GetTablesName");
                IEnumerable<TablesName> lstGetTableName = data.ToList();
                return lstGetTableName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TablesName> GetColumnsName(string TableName)
        {
            try
            {
                RepositoryService<TablesName> objGetColumnName = new RepositoryService<TablesName>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@TableName", "" + TableName + "", System.Data.SqlDbType.VarChar));
                var data = objGetColumnName.ExecWithStoreProcedure("proc_GetColumnName @TableName", parameters.ToArray());
                IEnumerable<TablesName> lstGetAuditTrailHistory = data.ToList();
                return lstGetAuditTrailHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }
    }
}

