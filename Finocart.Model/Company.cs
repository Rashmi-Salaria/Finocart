using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    public class Company
    {
        [Key]
        /// <summary>
        /// Company ID
        /// </summary>
        public int CompanyID { get; set; }
        /// <summary>
        /// Anchor company name
        /// </summary>
        public string Company_name { get; set; }
        /// <summary>
        /// Logegd in user login ID 
        /// </summary>
        public  string Login_id { get; set; }
        /// <summary>
        /// Logegd in user password
        /// </summary>
        [RegularExpression(pattern: "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{8,20})", ErrorMessage = "Password is not valid")]
        public string Password { get; set; }
        /// <summary>
        /// Registered user's address 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Registered user interested as
        /// </summary>
        public int? InterestedAs { get; set; }
        /// <summary>
        /// Registered user's company type
        /// </summary>
        public int CompanyType { get; set; }
        /// <summary>
        /// Registered user's contact credential
        /// </summary>
        public string Contact_CIN { get; set; }
        /// <summary>
        /// Registered user's PAN number
        /// </summary>
        public string Pan_number { get; set; }
        /// <summary>
        /// Contatct person's title
        /// </summary>
        public string Contact_Title { get; set; }
        /// <summary>
        /// Contatct person's name 
        /// </summary>
        public string Contact_Name { get; set; }
        /// <summary>
        /// Contatct person's designation
        /// </summary>
        public string Contact_Designation { get; set; }
        /// <summary>
        /// Registered user's emailid to email
        /// </summary>
        public string Contact_email { get; set; }
        /// <summary>
        /// Contatct person's mobile number
        /// </summary>
        public string Contact_mobile { get; set; }
        /// <summary>
        /// Comments to contact person
        /// </summary>
      
        public string Contact_Comments { get; set; }

        public decimal? PercentageRate { get; set; }

        public decimal? InvoiceLimitAmt { get; set; }
        /// <summary>
        ///Active Status
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        ///Delete Status
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        ///Registration Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        ///Registration updated date
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        ///Registration done by
        /// </summary>
        public int? CreatedBy { get; set; }
        /// <summary>
        ///Registration updated by
        /// </summary>
        public int? UpdatedBy { get; set; }

        public bool IsTemporaryPassword { get; set; }

        public bool? IsCritical { get; set; }

        public int? InvAmtLimitStatus { get; set; }

        public Int64? NoOfDays { get; set; }

        public Int64? FactoryOrReverseFactory { get; set; }

        public int? status { get; set; }

        public int? PaymentDays { get; set; }

        public int? LoginAttempt { get; set; }
    }

    public class GetCompanyModel
    {       
        [Key]
        public int CompanyID { get; set; }
        public string Company_name { get; set; }
        public string Contact_CIN { get; set; }
        public string Pan_number { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_email { get; set; }
        public string Address { get; set; }
        public string Contact_Comments { get; set; }
        public string Contact_Designation { get; set; }
        public string Contact_Title { get; set; }
        public string Contact_mobile { get; set; }
        public string LookUp { get; set; }
        public Int32? TotalRecord { get; set; }
        public int? FilteredRecord { get; set; }
    }

    public class GetLookUpModel
    {
        [Key]
        public int ID { get; set; }
        public string LookUpValue { get; set; }
    }

    public class GetStatus
    {
        [Key]

        public int ID { get; set; }

        public string Status { get; set; }
    }
    public class GetUploadVendorListModel
    {
        [Key]
        public string PONumber { get; set; }
        public string VendorCompany { get; set; }
        public string PODate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceAmt { get; set; }
        public string PaymentDueDate { get; set; }
        public string ApprovedAmt { get; set; }
        public string InvoiceStatus { get; set; }
        public string IntresetedAs { get; set; }
        public Int32? InvoiceApprovePayDays { get; set; }
        public string PaymentDate { get; set; }
        public Int32? TotalRecord { get; set; }
        public int? FilteredRecord { get; set; }
    }

    public class GetUploadVendorListModel1
    {
        [Key]
        public string PONumber { get; set; }
        public DateTime PODate { get; set; }
        public String InvoiceNo { get; set; }
        public Decimal InvoiceAmt { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public Decimal ApprovedAmt { get; set; }
        public int InvoiceStatus { get; set; }
        public int InvoiceApprovePayDays { get; set; }

    }

    public class GetRegisterMailTemplate
    {
        [Key]
        public string Template { get; set; }

    }

    public class GetUserMailTemplate
    {
        [Key]
        public string Template { get; set; }

    }

    public class GetForgetPasswordMailTemplate
    {
        [Key]
        public string Template { get; set; }

    }

    public class GetEmailId
    {
        [Key]
        //public string UserID { get; set; }
        //public string RoleID { get; set; }
       public string EmailID { get; set; }

    }
    public class ResetPassword
    {

        public string Username { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

    }
    public class GetVendorRegisterMailTemplate
    {
        [Key]
        public string Template { get; set; }

    }

    public class GetInvoiceRegisterMailTemplate
    {
        [Key]
        public string Template { get; set; }
        public string TableTag { get; set; }

    }
    public class GetBankMailTemplate
    {
        [Key]
        public string Template { get; set; }

    }
    public class GetBankKYCMailTemplate
    {
        [Key]
        public string Template { get; set; }

    }

    public class GetBankApprovedMailTemplate
    {
        [Key]
        public string Template { get; set; }

        public int? Id { get; set; }

    }

    public class UploadExcelPath
    {
        [Key]
        public Int64 ID { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }

        public Int64 Uploaded { get; set; }

        public Int64 Remaining { get; set; }

        public string Name { get; set; }

        public Int64 CompanyID { get; set; }

        public string CompanyName { get; set; }

        public int Status { get; set; }

        public Int64? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Logs { get; set; }
    }

    public class GetAuditTrailLogModel
    {
        [Key]
        public string ModuleName { get; set; }
        public string PageName { get; set; }
        public DateTime LogDate { get; set; }
        public string Contact_Name { get; set; }
        public Int32? TotalRecord { get; set; }
        public int? FilteredRecord { get; set; }
    }

    public class EditSendGridDetailsModel
    {
        [Key]

        public string LookupValue { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public string PortNO { get; set; }
    }

    public class EditSendGridDetails
    {
        public string LookupValue { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public string PortNO { get; set; }
    }


    public class GetChangePasswordMailTemplate
    {
        [Key]
        public string Template { get; set; }
    }

    public class CompanyBankView
    {
        [Key]
        public Int32 CompanyID { get; set; }

        public string AnchCompName { get; set; }

        public string BankName { get; set; }

        public decimal? AvailableLimits { get; set; }

        public decimal? UtilizedLimits { get; set; }

        public decimal? Interestrate { get; set; }

        public DateTime? ValidityUpto { get; set; }

        public decimal? DrawFunds { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
    }

    public class BankCompanyView
    {
        [Key]
        public Int32 AnchorCompId { get; set; }

        public string AnchorCompany { get; set; }

        public string BankName { get; set; }

        public decimal? AvailableLimits { get; set; }

        public decimal? UtilizedLimits { get; set; }

        public decimal? Interestrate { get; set; }

        public DateTime? ValidityUpto { get; set; }

        public decimal? DrawFunds { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
    }
    public class InsertHoli_Reason
    {
        [Key]
        public int? UserID { get; set; }

        public DateTime? Holidate { get; set; }

        public string Reason { get; set; }
    }

    public class DeleteHoli_Reason
    {
        [Key]

        public int ID { get; set; }
    }

    public class EditHoli_Reason
    {
        [Key]
        public int ID { get; set; }

        public string Holidate { get; set; }

        public string Reason { get; set; }

    }

    public class GetSumOfAmountPaid
    {
        [Key]
        public int ID { get; set; }

        public string AmountPaid { get; set; }
    }
}
