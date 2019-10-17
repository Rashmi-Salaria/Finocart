using System;
using System.Collections.Generic;
using System.Text;
using Finocart.CustomModel;
using Finocart.Model;

namespace Finocart.Interface
{
    public interface IBank
    {

        IEnumerable<TopAnchorCompaniesInBankDashboardModel> GetTopAnchor(string sortBy, int pageSize, Int64 skip, int BankID);
        IEnumerable<BankAnchorListModel> GetBankSetLimitList(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page, Int32? UserID);
        IEnumerable<DisbursementListModel> GetDisbursementList(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page);
        IEnumerable<KycUploadModel> GetKycUploadList(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page, Int64? BankID);
        IEnumerable<GetUploadDocumentListModel> GetUploadDocumentList(int CompanyID, string sortBy, int pageSize, Int64 skip);

        IEnumerable<BankLimitAnchorList> GetBankSetLimitAnchorLists(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page, Int32? UserID);

        IEnumerable<BankLimitAnchorListView> GetBankLimitAnchorLists(string sortBy, int pageSize, Int64 skip, string CompanyName,Int64 CompanyID, string Page, Int32? UserID);

        IEnumerable<BankLimitLogView> GetBankLimitLogLists(string sortBy, int pageSize, Int64 skip, Int64 SetLimitID);

        /// <summary>
        /// Get  Approval Days
        /// </summary>

        IEnumerable<NoOfDaysForApproval> GetApprovalDays();

        /// <summary>
        /// Insert update records
        /// </summary>
        /// <param name="ObjSetLimitModel"></param>
        /// <returns></returns>
        int InsertUpdateLimitAmount(SetBankAmountLimit objUserModel, Int32? UserId);

        int InsertDocumentDetails(Int64 DocumentTypeID, string DocumentName, string DocumentDescription, Int32? BankID, int AnchorCompID);

        int DeleteDocumentDetail(int ID);
        /// <summary>
        /// Get Data by Anchor Id   
        /// </summary>

        IEnumerable<SetBankAmountLimit> getDataByid();
        SetBankAmountLimit EditPage(int id, int BankId);

        IEnumerable<BankDocumnet> GetDocumentsList(int CompanyID, int Bankid);

        IEnumerable<GetBankMailTemplate> GetBankMailTemplate(string Template);

        IEnumerable<GetBankApprovedMailTemplate> GetApprovedMailTemplate(string Template, int Id);

        IEnumerable<GetChangePasswordMailTemplate> GetChangePasswordMailTemplate(string Template);



        #region Funds Request History   
        /// <summary>
        /// Get Funds Request History List
        /// </summary>

        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="AnchorName"></param>
        IEnumerable<UploadKYC> getDrawFundsList(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId,string NameofBank, string KYCStatus);


        IEnumerable<FundsWithdrwanHistory> fundsWithdrwanHistories(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId);

        IEnumerable<FundsWithdrwanHistory> fundsWithdrwanHistoriesview(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId, Int32? UserId, string bankName);


        IEnumerable<DrawFundsFromBank> getFundsFromBank(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId);

        /// <summary>
        /// by abc
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="AnchorCompId"></param>
        /// <returns></returns>
        IEnumerable<FundRequestFromBank> getFundWithDrawDetailsperBank(string sortBy, int pageSize, Int64 skip,int ID);

        /// <returns></returns>
        IEnumerable<FundsRequestHistory> GetFundRequestHistoryList(string sortBy, int pageSize, Int64 skip);
        #endregion

        /// <summary>
        /// Funds Request History View
        /// </summary>
        /// <param name="Anchore ID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        IEnumerable<FundsRequestHistoryView> GetFundsRequestHistoryView(Int64 Anchor_id, string sortBy, int pageSize, Int64 skip);

        #region  Finocredit
        int Documnet_Submit(BankDocumnet_list model);
        IEnumerable<BankDocumnet> BankDocumnet(int? AnchorCompId, int? BankID);

        IEnumerable<GetDocument_Download> DrawFundsDocumentView(string sortBy, int pageSize, Int64 skip, int documentTypeID, Int64? AnchorCompId);

        IEnumerable<DrawFundsFromBank> getFundsHistoryList(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId);

        IEnumerable<DrawFundsFromBank> getFundsHistoryview(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId, string BankName, string Drawfunds);
        #endregion

        #region Get Request Amount Received Today
        string GetRequestAmountReceivedToday();
        #endregion

        /// <summary>
        /// Insert update records
        /// </summary>
        /// <param name="ObjSetLimitModel"></param>
        /// <returns></returns>
        int AddEditDisbursementData(DisbursementsModel disbursementsModel);

        int UpdateFundsRequestFromBank(SetBankAmountLimit setBankAmountLimitdata);

        int ApprovedStatus(string CompanyID, Int32? BankID);
        int RejectStatus(string CompanyID);

        DisbursementsModel EditDisbursementDetail(int CompanyID);

        SetBankAmountLimit EditFundRequestFromBank(int id);

        /// <summary>
        /// Get Bank Company Graph Details
        /// </summary>
        /// <param name="month"></param>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        IEnumerable<BankGraphDetailsModel> GetGraphDetails(string month, string BankName);

        IEnumerable<BankDocumentDetails> GetEmail(string BankEmailID);
        IEnumerable<BankAnchorEmailDetails> GetAnchorEmail(string CompanyName);

        IEnumerable<BankIDDetails> GetBankId(Int32? CompanyName);

        IEnumerable<CheckSetLimit> CheckSetLimit(int CompanyId);

        IEnumerable<GetBankKYCMailTemplate> GetBankKYCMailTemplate(string Template);

        int getBankTotalFundLimits(Int64 AnchorCompId);


        /// <summary>
        /// add by abc
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 

        /// <summary>
        /// add by abc
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 

        IEnumerable<GetDocument_Download> getDocumentDownload(Int32 DocumentTypeID,Int32? AnchorCompId);

        int? CheckFromToDate(Int32? BankID, DateTime fromDate, DateTime toDate, Int64 anchorCompId, Int64 setLimitId);

        IEnumerable<BankLimitAnchorList> TotalExistingAnchor(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page);

        IEnumerable<BankLimitAnchorListView> GetTotalBankLimitAnchorLists(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page, Int32? UserID);

        IEnumerable<TotalPendingKYC> GetTotalCompanyPendingKYCs();

        IEnumerable<SetLimitForAnchor> GetSetLimitForAnchor();

        IEnumerable<GetUpcomingSetLimitChartData> GetSetLimitforAnchorCompany(int AnchorCompID);

        IEnumerable<HolidayListModel> GetHolidayListing(string sortBy, int pageSize, Int64 skip,string Reason,int? UserID);


    }
}
