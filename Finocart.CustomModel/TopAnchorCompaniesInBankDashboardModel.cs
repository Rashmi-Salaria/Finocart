using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Finocart.CustomModel
{
    #region Top Anchor Company in Dashboard
    public class TopAnchorCompaniesInBankDashboardModel
    {
        [Key]
        public int id { set; get; }
        public int? Anchor_id { set; get; }
        public string Anchor_Name { set; get; }
        public decimal? Available_Limits { set; get; }
        public decimal? Utilized_Limits { set; get; }
        public DateTime? Validity_upto { set; get; }
        public Int32? TotalRecord { get; set; }
        public int? FilteredRecord { get; set; }
    }
    #endregion

    public class BankAnchorListModel
    {
        /// <summary>
        /// Anchor Company ID
        /// </summary>
        [Key]
        public int CompanyID { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Company_name { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_Name { get; set; }
        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_mobile { get; set; }
        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_email { get; set; }
        /// <summary>
        /// Available Limits Amount
        /// </summary>
        public decimal? Available_Limits { get; set; }

        /// <summary>
        /// Utilized Limits Amount
        /// </summary>
        public decimal? Utilized_Limits { get; set; }

        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal? Interest_rate { get; set; }

        ///// <summary>
        ///// Interest rate month
        ///// </summary>
        //public int? Interest_rate_month { get; set; }

        /// <summary>
        /// Validity From date
        /// </summary>
        public DateTime? ValidityFromto { get; set; }

        /// <summary>
        ///   Validity to date
        /// </summary>
        public DateTime? Validity_upto { get; set; }

        /// <summary>
        /// Anchor Company Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

      

    }

    public class BankLimitAnchorList
    {
        [Key]

        public int CompanyID { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Company_name { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_Name { get; set; }
        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_mobile { get; set; }
        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_email { get; set; }

        /// <summary>
        /// Anchor Company Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }


    }

    public class BankLimitAnchorListView
    {
        [Key]

        public int SetLimitId { get; set; } 

        public int CompanyID { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Company_name { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_Name { get; set; }
        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_mobile { get; set; }
        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string Contact_email { get; set; }
        /// <summary>
        /// Available Limits Amount
        /// </summary>
        public decimal? Available_Limits { get; set; }

        /// <summary>
        /// Utilized Limits Amount
        /// </summary>
        public decimal? Utilized_Limits { get; set; }

        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal? Interest_rate { get; set; }

        /// <summary>
        /// Validity From date
        /// </summary>
        public string ValidityFromto { get; set; }

        /// <summary>
        ///   Validity to date
        /// </summary>
        public string Validity_upto { get; set; }

        ///<summary>
        /// ODBD
        /// </summary>
        /// 
        public string ODBD { get; set; }

        /// <summary>
        /// Anchor Company Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

        /// <summary>
        ///   Additional Document  
        /// </summary>
        //public string Additional_document { get; set; }
        ///// <summary>
        /////  Document description  
        ///// </summary>
        //public string Document_desc { get; set; }
    }

    public class BankLimitLogView
    {
        [Key]
        public Int64 ID { get; set; }
        
        /// <summary>
        /// Available Limits Amount
        /// </summary>
        public decimal? Available_Limits { get; set; }

        /// <summary>
        /// Utilized Limits Amount
        /// </summary>
        public decimal? Utilized_Limits { get; set; }

        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal? Interest_rate { get; set; }

        /// <summary>
        /// Validity From date
        /// </summary>
        public string ValidityFromto { get; set; }

        /// <summary>
        ///   Validity to date
        /// </summary>
        public string Validity_upto { get; set; }

        ///<summary>
        /// ODBD
        /// </summary>
        /// 
        public string ODBD { get; set; }

        /// <summary>
        /// Anchor Company Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

        public string ModifiedDate { get; set; }


    }

    #region Request Amount Received Today
    public class RequestAmountReceivedToday
    {
        [Key]
        public decimal RequestAmount { get; set; }
    }
    #endregion

    #region Disbursements model
    public class DisbursementsModel
    {
        /// <summary>
        /// Anchor Company ID
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string DrawRequestId { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string AnchorName { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public decimal? FundsRequested { get; set; }

        /// <summary>
        /// Validity From date
        /// </summary>
        public DateTime? RequestDate { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public decimal? PaidAmount { get; set; }

        /// <summary>
        /// Validity From date
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Anchor Company Name
        /// </summary>
        public string UTRDetails { get; set; }

        /// <summary>
        /// Validity From date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Validity From date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

      

    }
    #endregion

    #region 
    public class DisbursementListModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Draw Request Id
        /// </summary>
        public string DrawRequestId { get; set; }

        /// <summary>
        /// Anchor Name
        /// </summary>
        public string AnchorName { get; set; }
        /// <summary>
        /// Funds Requested
        /// </summary>
        public decimal? FundsRequested { get; set; }
        /// <summary>
        /// Request Date
        /// </summary>
        public string RequestDate { get; set; }
        /// <summary>
        /// Paid Amount
        /// </summary>
        public decimal? PaidAmount { get; set; }

        /// <summary>
        /// Payment Date
        /// </summary>
        public string PaymentDate { get; set; }

        /// <summary>
        /// Payment Status
        /// </summary>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// UTR Details
        /// </summary>
        public string UTRDetails { get; set; }

        /// <summary>
        /// Anchor Company Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

    }
    #endregion

    #region Withdraw Funds
    public class FundsWithdrwanHistory
    {

        [Key]
        public int? id { get; set; }

        public string Anchor_Name { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? UtilizedAmount { get; set; }

        public decimal? Balance { get; set; }

        public string BankName { get; set; }

        public string Approvaldate { get; set; }

        public Int32? TotalRecord { get; set; }
        public int? FilteredRecord { get; set; }


    }
    #endregion

    #region Finocredit Draw Funds
    public class DrawFundsFromBank
    {
        [Key]
        public int id { get; set; }
        public String Anchor_Name { get; set; }
        public decimal? Available_Limits { get; set; }
        public decimal? Utilized_Limits { get; set; }
        public string Validity_upto { get; set; }
        public decimal? Interest_rate { set; get; }
        //public int? Interest_rate_month { set; get; }
        public string BankID { get; set; }
        public String BankName { set; get; }
        public decimal? Draw_funds { get; set; }
        public string RequestStatus { get; set; }
        public string Status { get; set; }
        public string ODBD { get; set; }
        public Int32? TotalRecord { get; set; }
        public int? FilteredRecord { get; set; }
        /*----------add below parameter ------------*/
        public decimal? Remaining_avail_bal { get; set; }
        public string ValidityFromto { get; set; }
        public bool IsVisibleAction { get; set; }
    }

    public class UploadKYC
    {
        [Key]
        public Int64 id { get; set; }
        public String Anchor_Name { get; set; }
        public string BankID { get; set; }
        public String BankName { set; get; }
        public string Status { get; set; }
        public Int32? TotalRecord { get; set; }
        public int? FilteredRecord { get; set; }
    }

    public class BankDocumnet
    {
        [Key]
        public int DocumentType_ID { get; set; }
       
        public string Document_Name { get; set; }
        public string Document_Description { get; set; }
        public string Status { get; set; }
        public string Uploaded_Date { get; set; }
        public string FilePath { get; set; }
       [NotMapped]
        public string myfile { get; set; }



    }
    public class BankDocumnet_list
    {
        [Key]
        public int id { get; set; }
        public int DocumentType_ID { get; set; }
        public int? AnchorID { get; set; }
      
       
        public string FilePath { get; set; }
        public int Status { get; set; }
        public string BankName { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }

    public class BankDocumnet_list_1
    {
        [Key]
        public string FilePath { get; set; }
    }

    public class BankDocumentDetails
    {
        [Key]
        public string BankEmail { get; set; }
    }

    public class BankAnchorEmailDetails
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Contact_email { get; set; }
        public string Contact_Name { get; set; }
    }

    #endregion

    #region  KYC Upload
    public class KycUploadModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int CompanyID { get; set; }

        /// <summary>
        /// Anchor Name
        /// </summary>
        public string Company_name { get; set; }
       
        /// <summary>
        /// Anchor Company Total Record
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Status of KYC document
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

    }
    #endregion

    public class GetUploadDocumentListModel
    {
        [Key]
       
        public string FilePath { get; set; }
        public string Document_Name { get; set; }
        public Int32? TotalRecord { get; set; }
        public int? FilteredRecord { get; set; }

    }

    public class BankIDDetails
    {
        [Key]

        public int CompanyId { get; set; }

        public string Name { get; set; }
    }

    public class CheckSetLimit
    {
        [Key]

        public Int64 SeqNum { get; set; }

        public Decimal? PercentageRate { get; set; }

        public int? PaymentDays { get; set; }
    }

    public class BankTotalAvailableLimits
    {
        [Key]
        public int ApproveCount { get; set; }
        //public decimal? TotalAvailableLimits { get; set; }
    }


    public class FundRequestFromBank
    {
        [Key]

        public int Fund_id { get; set; }

        public DateTime Transaction_History { get; set; }

        public decimal Remaining_avail_bal { get; set; }

        public decimal Draw_funds { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
    }

    public class GetLeastAvail_bal
    { 
        [Key]
        public int Anchor_Company_index { get; set; }
    }

    public class GetDocument_Download
    {
        [Key]
        public int ID { get; set; }

        public string FilePath { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }

    }

    public class SetLimitFromToDate
    {
        [Key]
        public int Count { get; set; }
    }

    public class TotalPendingKYC
    {
        [Key]

        public int NoPendingKYCCompanys { get; set; }
        
    }

    public class SetLimitForAnchor
    {
        [Key]
        
        public int Anchor_id { get; set; }

        public string Anchor_Name { get; set; }
    }
    
}
