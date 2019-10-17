using Finocart.CustomModel;
using Finocart.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finocart.Interface
{
    public interface ICompany
    {
        /// <summary>
        /// Insert Anchor company registration
        /// </summary>
        /// <param name="objUserModel"></param>
        /// <returns></returns>
        int InsertAnchorCompanyRegister(Company objUserModel);

        //void updateCompanyRate_Days(string PercentageRate, int PaymentDays);

        int UpdateProfile(Company objUserModel);

        int UpdateUserProfile(UserModel objUserModel);


        int DeleteCompanyById(string companyName,string panNumber);

        /// <summary>
        /// Validate Pan number 
        /// </summary>
        /// <param name="pan"></param>
        /// <returns></returns>
        string CheckPan(string pan);

        /// <summary>
        /// Validate Pan number 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        int? CheckEmail(string email);

        /// <summary>
        /// Get Company details
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        Company GetCompany(Int32? CompanyId);

        IEnumerable<GetCompanyModel> GetCompanyListing(string sortBy, int pageSize, Int64 skip, string CompanyName, string InterestedAs, string name);

        IEnumerable<GetAuditTrailLogModel> GetAuditTrailLogListing(string sortBy, int pageSize, Int64 skip, string CompanyName, string ContactPerson);

        int UpdateSendGrid(EditSendGridDetailsModel objSendGridModel);

        /// <summary>
        /// Get Company details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Company EditPage(int id);

        Company ProfileEditPage(Int32? id);

        UserModel ProfileUserEditPage(Int32? UserID);

        EditSendGridDetailsModel SendGridEditPage();

        IEnumerable<GetLookUpModel> GetLookUpDropDown();

        IEnumerable<GetLookUpModel> GetFactoryOrReverse();

        IEnumerable<GetStatus> GetCuurentStatus();

        IEnumerable<GetUploadVendorListModel> GetUploadVendorList(Int64? CompanyID, string VendorID, Int64? AnchorCompID,string POName,string InvoiceDate, string INVOICENO, string InvoiceAmt,string ApprovedAmt,string InvoiceStatus,string PaymentDays, string sortBy, int pageSize, Int64 skip);

        IEnumerable<GetUploadVendorListModel1> GetUploadVendorList1(Int64? VendorId);

        IEnumerable<GetRegisterMailTemplate> getRestraterMailTemplate();

        IEnumerable<GetVendorRegisterMailTemplate> GetVendorRegisterMailTemplate(string Template);

        IEnumerable<GetInvoiceRegisterMailTemplate> GetInvoiceRegisterMailTemplate(string Template);

        int? CheckAnchorRate(Int32? CompanyID, decimal AnchorRate);

        IEnumerable<CompanyBankView> GetCompanyBankListing(string sortBy, int pageSize, Int64 skip, Int64 CompanyID, string Search);

        IEnumerable<BankCompanyView> GetBankCompanyListing(string sortBy, int pageSize, Int64 skip, Int64 CompanyID, string Search);
        //void updateCompanyRate_Days(int? companyID, decimal percentageRate, int paymentDays);
        IEnumerable<GetSumOfAmountPaid> UpdateCompanyRate_Days(int? companyID, decimal percentageRate, int paymentDays, string InternalFundLimits, bool chkUnlimited);


        int InsertHolidateReason(int? UserID,DateTime? holidate, string reason);

        int DeleteHolidateReason(int ID);

        int EditHolidateReason(int ID,string Holidate, string Reason);

      
    }
}
