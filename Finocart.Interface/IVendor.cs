using Finocart.Model;
using System;
using System.Collections.Generic;
using Finocart.CustomModel;
using Microsoft.AspNetCore.Http;

namespace Finocart.Interface
{
    public interface IVendor
    {
        /// <summary>
        /// Find Vendor Name
        /// </summary>
        /// <param name="EmailID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        UserLoginModel validateUser(string EmailID, string Password);
        /// <summary>
        /// Get Vendor Doc List
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        IEnumerable<DocumentModel> GetVendorDocList(int? VendorID);
        /// <summary>
        /// Insert Document Det
        /// </summary>
        /// <param name="VendorDocument"></param>
        /// <returns></returns>
        int InsertDocumentDet(IFormFile AnchorDocument, int? iVendorID, int iDocumentTypeID,string ToSaveFileName,Int32? UserId);
        /// <summary>
        /// Get File Name By Id
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns></returns>
        VendorDocument GetFileNameById(Int64 DocID);

        /// <summary>
        /// Delete vendor document record
        /// </summary>
        /// <param name="vendorDocument"></param>
        /// <returns></returns>
        int DeleteVendorDocRecord(VendorDocument vendorDocument);

        /// <summary>
        /// Get anchor company name by company id
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        Invoice GetCompanyNameByCompanyId(Int64 CompanyId);

        #region Insert bucket discount details
        int InsertInvoiceBucketDiscountDet(BucketInvoiceModel bucketInvoiceModel);

        int SendNotification(BucketInvoiceModel bucketInvoiceModel, Int32? UserId, Int32? VendorId);

        int InsertFinoInvoiceBucketDiscountDet(BucketInvoiceModel bucketInvoiceModel);

        IEnumerable<GetBankMailTemplate> GetDiscInvMailTemplate(string Template);

        Company GetCompany(Int32? ID);
        #endregion

        #region Get Email id by invoice id
        /// <summary>
        /// returns email id by invoice id
        /// </summary>
        /// <param name="strInvoiceId"></param>
        /// <returns></returns>
        IEnumerable<BucketInvoiceModel> GetEmailByInvoiceId(string strInvoiceId);
        #endregion

        #region
        /// <summary>
        /// Get discount offered invoices list
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="AnchorCompanyName"></param>
        /// <param name="InvoiceStatus"></param>
        /// <param name="ConditionParam"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        IEnumerable<OfferedDiscountInvModel> GetDiscountOfferedInvoiceList(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string InvoiceAmount, string ConditionParam, int? CompanyID, Int32? VendorId);
        #endregion

        #region Get history of invoice journey
        /// <summary>
        /// Get history of invoice journey
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        IEnumerable<InvoiceJourneyHistoryModel> GetInvoiceJourneyHistory(Int64 InvoiceId,string PageName);
        #endregion

        #region
        /// <summary>
        /// Get EmailId By Vendor Id
        /// </summary>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        User GetEmailIdByVendorId(int? VendorId);
        #endregion

        IEnumerable<InvoiceModel> GetInvoicesByAmount(decimal sumAssuredAmount, int? VendorId, int Skip, int PageSize, string SortBy, string fundingDate, decimal discount);

        IEnumerable<InvoiceModel> GetInvoicesByAnchor(decimal sumAssuredAmount, int? VendorId, int Skip, int PageSize, string SortBy, string fundingDate, decimal discount, string anchoCompany);

        string GetMaxAvailableAmount(Int64? VendorID, string AnchoCompany);

        string GetMinFundPaymentDate(Int64? VendorID);

        IEnumerable<RecievableDue> GetRecieableduestoday(int? VendorId, string sortBy, int pageSize, Int64 skip, int Anchorid, string Anchorname, string TotalInvoiceAmount, string Page, int IsTotalCount);

        IEnumerable<ReceivabledueIndividual> GetReceivabledueIndividuals(Int64? VendorId, int? companyid,Int64 skip,int pageSize,string sortyBy,string PONumber, string Page, int IsTotalCount);

        IEnumerable<GraphDetailsModel> GetGraphDetails(string month,Int64? VendorId);

        #region ApprovalInvoice
        IEnumerable<DiscountOfferedInvModel> getInvoicetodaysList(string sortBy, int pageSize, Int64 skip, string Companyname, string Approvedamt, Int32? VendorId);
        #endregion


        #region Available for funding
        IEnumerable<AvailableFundCompModel> getAvailableforfundingList(string sortBy, int pageSize, Int64 skip, string Companyname, string Approvedamt,string Page, Int64? VendorId);
        #endregion


        IEnumerable<InvoiceHistoryModel> InvoiceHistorylist(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string TotalInvoiceAmmount, Int32? VendorID);

        /// <summary>
        /// Find user
        /// </summary>
        /// <param name="Rate"></param>
        /// <param name="valid Date"></param>
        /// <returns></returns>
        int InsertFundingRateAndDate(AutoFunding ObjAutoFunding);

        IEnumerable<AutoFunding> GetAutoFundings(Int64? AutoFudngId);

        int Updatefunding(string AutoFudngId, string DiscuntRate, string DiscuntVlidDate);

        IEnumerable<UpcomingPayableGraphModel> GetReceivableReceivedGraphData(Int64? AnchorCompId, Int64? vendorId, Int64? DataTypeId);

        IEnumerable<AnchorCompanyDropdownModel> GetAnchorCompanyForVendor(Int64? vendorId, Int64? DataTypeId);

        IEnumerable<GetRegisterMailTemplate> getRestraterMailTemplate();

        IEnumerable<HolidayDatesModel> GetHolidayDates();
    }
}
