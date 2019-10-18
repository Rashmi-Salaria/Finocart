using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Finocart.CustomModel;
using Finocart.Model;
using NPOI.SS.UserModel;
using System.Data;

namespace Finocart.Interface
{
    public interface IAnchorCompany
    {
        /// <summary>
        /// Insert Document Det
        /// </summary>
        /// <param name="AnchorDocument"></param>
        /// <returns></returns>
        int InsertDocumentDet(IFormFile AnchorDocument, int? iAnchorCompanyID, int iDocumentTypeID,string ToSaveFileName,Int32? UserId);
        /// <summary>
        /// Get Anchor Doc List 
        /// </summary>
        /// <param name="AnchorCompanyID"></param>
        /// <returns></returns>
        IEnumerable<DocumentModel> GetAnchorDocList(int? AnchorCompanyID);
        /// <summary>
        /// GetFileNameById
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns></returns>
        AnchorCompanyDocument GetFileNameById(Int64 DocID);

        /// <summary>
        /// Delete Anchor company document record
        /// </summary>
        /// <param name="anchorCompanyDocument"></param>
        /// <returns></returns>
        int DeleteAnchorCompDocRecord(AnchorCompanyDocument anchorCompanyDocument);

        /// <summary>
        /// Get Anchor company listing by vendor Id
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        IEnumerable<AnchorCompListingModel> GetAnchorCompListingByVendorId(int? VendorId, string sortBy, int pageSize, Int64 skip, string CompanyID, string CompanyName, string TotalInvoiceAmt, string Page, int IsTotalCount);

        /// <summary>
        /// Get Anchor company listing by vendor Id
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        
         IEnumerable<AnchorCompListingModel> GetAnchordashboardCompListingByVendorId(int? VendorId, string sortBy, int pageSize, Int64 skip, string CompanyID, string CompanyName, string TotalInvoiceAmt, string Page, int IsTotalCount);



        IEnumerable<DashboardListModel> GetDashboardList(int? VendorId, string sortBy, int pageSize, Int64 skip, string CompanyID, string CompanyName, string TotalInvoiceAmt, string Page, int IsTotalCount);

        /// <summary>
        /// Get Vendor Invoice List by Company Id
        /// </summary>
        /// <param name="iSelCompanyId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        IEnumerable<CompanyInvoiceListModel> GetVendorInvoiceListByCompanyID(int iSelCompanyId, string sortBy, int pageSize, Int64 skip);

        IEnumerable<VendorlistModel> GetVendorListing(int iSelCompanyId, string sortBy, int pageSize, Int64 skip, string CompanyName,string ContactNo, string EmailId,string VendorStatus);


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
        IEnumerable<CriticalVendorsModel> GetCriticalVendors(Int32? CompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string TotalInvAmtLimit);


        /// <summary>
        /// Get Anchor Notification List
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="UserID"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <returns></returns>
        IEnumerable<NotificationModel> getAnchorNotificationList(string sortBy = "", int pageSize = 0, int? UserID = 0, Int64 skip = 0, string VendorName = "", int? isRead = 0);
        IEnumerable<NotificationModel> getBankNotificationList(string sortBy = "", int pageSize = 0, int? UserID = 0, Int64 skip = 0, string VendorName = "", int? isRead = 0);

        #region Get Vendors Drop Down
        /// <summary>
        /// Get Vendors Drop Down
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        IEnumerable<VendorsDropDownModel> GetVendorsDropDown(Int32? CompanyID);
        #endregion

        /// <summary>
        /// Insert And Update Critical Vendors
        /// </summary>
        /// <param name="objCriticalVendors"></param>
        /// <returns></returns>
        int InsertUpdateCriticalVendor(CriticalVendorsModel objCriticalVendors);

        /// <summary>
        /// Delete Critical Vendors
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteCriticalVendors(int id);

        #endregion

        /// <summary>
        /// Get Count of Unread Notifications
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int Count(Int32? UserID);

        /// <summary>
        /// Update all Notification as Read
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        int AnchorUpdateUser(int? UserId);

        

        #region Invoice Details
        /// <summary>
        /// Get Payment Due Today List
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="VendorName"></param>
        /// <param name="TotalInvAmtLimit"></param>
        /// <returns></returns>
        IEnumerable<VendorPaymentDueModel> GetPaymentDueToday(Int32? CompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string TotalAppInvAmt);

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
        IEnumerable<VendorPaymentDueViewModel> GetPaymentDueTodayView(Int32? CompanyID, string sortBy, int pageSize, Int64 skip, string InvoiceID, string InvoiceAmt, Int64 VendorID);

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
        IEnumerable<VendorAwaitedInvApprovalModel> GetAwaitedInvoiceApproval(Int64? CompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string TotalAppInvAmt);

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
        IEnumerable<VendorBucketAwaitedInvViewModel> GetAwaitedInvoiceApprovalView(Int32? CompanyID, int VendorID, string sortBy, int pageSize, Int64 skip, string BucketID, string TotalAppInvAmt);

        /// <summary>
        /// Get Bucket Wise Invoice View
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
        IEnumerable<VendorBucketInvoiceViewModel> GetBucketWiseInvoiceView(Int32? CompanyID, int VendorID, Int64 BucketID, string sortBy, int pageSize, Int64 skip, string InvoiceID, string POID);

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
        IEnumerable<VendorInvoiceApprovedTodayModel> GetInvoiceApprovedToday(Int32? CompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string TotalAppInvAmt);

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
        IEnumerable<VendorInvApproveTodayViewModel> GetInvoiceApprovedTodayView(Int32? CompanyID, int VendorID, string sortBy, int pageSize, Int64 skip, string BucketID, string TotalAppInvAmt);

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
        IEnumerable<VendorBucketWiseDiscInvViewModel> GetBucketWiseDiscInvView(Int32? CompanyID, int VendorID, Int64 BucketID, string sortBy, int pageSize, Int64 skip, string InvoiceID, string POID);

        /// <summary>
        /// Get Anchor Company Graph Details
        /// </summary>
        /// <param name="month"></param>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        IEnumerable<GraphDetailsModel> GetGraphDetails(string month, Int64? AnchorCompId);

        #endregion

        #region Upload Vendor And Invoices

        /// <summary>
        /// Insert Vendor Record
        /// </summary>
        /// <param name="VendorDetails"></param>
        /// <param name="AnchorCompId"></param>
        /// <param name="randomPassword"></param>
        /// <returns></returns>
        int InsertVendorRecord(DataRow VendorDetails, Int64? AnchorCompId, string randomPassword);

        /// <summary>
        /// Insert Invoice Record
        /// </summary>
        /// <param name="VendorDetails"></param>
        /// <param name="AnchorCompId"></param>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        int InsertInvoiceRecord(IRow VendorDetails, Int64? AnchorCompId);

        Company CheckEmailID(string PanNumber);
        #endregion

        #region Analytics
        IEnumerable<AnchorCompDropDownModel> getVendorList(Int64? VendorId);

        IEnumerable<AnchorAnalyticLostOppModel> GetLostOpportunity(int? VendorId, string sortBy, int pageSize, Int64 skip, int companyID, string fromDate, string toDate);
        #endregion

        #region Invoice Status
        IEnumerable<InvoiceStatus> GetInvoiceStatuses(int? VendorId, int? AnchorCompanyID, string sortBy, int pageSize, Int64 skip, string VendorName, string Invoicetotatalamt);
        #endregion

        #region Analytics Earning
        IEnumerable<Earning_Vendorwise> GetEarning_Vendorwises(int? VendorId, string sortBy, int pageSize, Int64 skip, DateTime fromDate, DateTime toDates);
        #endregion
        #region Earning Discount wise

        IEnumerable<Eearning_discountRateWise> eearning_DiscountRateWises(int? VendorId,int? AnchorCompId, string sortBy, int pageSize, Int64 skip, string fromDate, string toDates);

        IEnumerable<Eearning_discountRateWise> discountRateWisesinPercent(int? VendorId, int? AnchorCompID, string sortBy, int pageSize, Int64 skip, string fromDate, string toDates);
        #endregion

        /// <summary>
        /// Insert Anchor Notification
        /// </summary>
        /// <param name="AnchorDocument"></param>
        /// <returns></returns>
        int AddNotificationMessage(int? iAnchorCompanyID, string Description, int? RoleID, int? UserID);

        int AddNotificationMessageForBank(int? iAnchorCompanyID, string Description, int? RoleID, int? UserID);

        int UpdateRuleOfEngine(int? iAnchorCompanyID, string RulePercentage, string RuleLimit, string Internalfund, string FromBank, string chkUnlimited, string AnchorRate);

        RuleofEngineDetails GetRuleofEngineData(int? iAnchorCompanyID);


        IEnumerable<AnchorVendorBankCount> GetVendorAnchorBankCount(string noofcompanies);
        //int InsertExcelPath(string fullPath, string FileName, Int64? AnchCompanyId, string CompanyName, string Upload);

        IEnumerable<UploadLogsModel> GetVendorInvoiceLogs(string sortBy, int pageSize, Int64 skip, Int32? CompanyID, string Upload);

        UploadExcelPath GetUploadDetils(Int64 ID);

        IEnumerable<AnchorListModel> GetVendorAnchorListing(int iSelCompanyId, string sortBy, int pageSize, Int64 skip, string CompanyName, string InAmount);

        //IEnumerable<HolidayList> GetHolidayListing();

        int InsertHolidayListRecord(HolidayList objholidaylist);


        IEnumerable<HolidayListModel> GetAnchorHolidayListing(string sortBy, int pageSize, Int64 skip,string Reason,int? UserID);

        int UpdateUTRDetails(Int64 InvoiceID, string UTRDetails);

        /// <summary>
        /// Get Anchor Company Upcoming Invoice Payable Graph Details
        /// </summary>
        /// <param name="AnchorCompId"></param>
        /// <param name="VendorId"></param>
        /// <param name="DataTypeId"></param>
        /// <returns></returns>
        IEnumerable<UpcomingPayableGraphModel> GetUpcomingPayableGraphData(Int64? AnchorCompId, Int64? vendorId, Int64? DataTypeId);

        /// <summary>
        /// Get Vendors For Anchors
        /// </summary>
        /// <param name="AnchorCompId"></param>
        /// <param name="DataTypeId"></param>
        /// <returns></returns>
        IEnumerable<VendorDropdownModel> GetVendorsForAnchor(Int64? AnchorCompId, Int64? DataTypeId);


        /// <summary>
        /// Get Vendors For Anchors
        /// </summary>
        /// <param name="AnchorCompId"></param>
        /// <param name="DataTypeId"></param>
        /// <returns></returns>
        int InsertNotificationDetail(string BugType,string BugDetail,string CompanyName, int CompanyId);

        IEnumerable<GetNotificationDetail> GetNotificationList(string sortBy, int pageSize, Int64 skip, string SearchFieldName);

        IEnumerable<SetLimitHistory> GetSetLimitHistory(string sortBy, int pageSize, Int64 skip,string SearchFieldName,int? CompanyID);

        IEnumerable<TablesName> GetTablesName();

        IEnumerable<AuditTrailHistory> GetAuditTrailHistory(string sortBy, int pageSize, Int64 skip, string SearchFieldName,string TableName, string ColumnName,string FormDate2,string ToDate2);
        
        IEnumerable<TablesName> GetColumnsName(string TableName);
    }

}
