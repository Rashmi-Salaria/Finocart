
using Microsoft.EntityFrameworkCore;
using Finocart.Model;
using Finocart.CustomModel;


namespace Finocart.DBContext
{
    /// <summary>
    /// Vendor Management System Context
    /// </summary>
    public class VMSContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public VMSContext(DbContextOptions<VMSContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>();

            base.OnModelCreating(modelBuilder);
        }

      
        /// <summary>
        /// Table: Employee 
        /// </summary>
        public virtual DbSet<Employee> Employee { get; set; }

        /// <summary>
        /// Table: LookupDetails 
        /// </summary>
        public virtual DbSet<LookupDetails> LookupDetails { get; set; }

        /// <summary>
        /// Table: User 
        /// </summary>
        public virtual DbSet<User> User { get; set; }

        /// <summary>
        /// Table: Invoice
        /// </summary>
        public virtual DbSet<Invoice> Invoice { get; set; }

        /// <summary>
        /// Table: RolesAccessMaster
        /// </summary>
        public virtual DbSet<RolesAccessMaster> RolesAccessMaster { get; set; }

        /// <summary>
        /// Vendor
        /// </summary>
        public virtual DbSet<Vendor> Vendor { get; set; }

        /// <summary>
        /// Table: FinocartMaster
        /// </summary>
        public virtual DbSet<FinocartMaster> FinocartMaster { get; set; }

        /// <summary>
        /// Table: ModuleMaster
        /// </summary>
        public virtual DbSet<ModuleMaster> ModuleMaster { get; set; }

        /// <summary>
        /// Table: Admin
        /// </summary>
        public virtual DbSet<Admin> Admin { get; set; }
        /// <summary>
        /// Table: InvoiceApprovalOrder
        /// </summary>
        public virtual DbSet<InvoiceApprovalOrder> InvoiceApprovalOrder { get; set; }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<SearchHistory> SearchHistory { get; set; }
        public virtual DbSet<VendorAssociatedCompany> VendorAssociatedCompany { get; set; }

        /// <summary>
        /// Table: Notification
        /// </summary>
        public virtual DbSet<Notification> Notification { get; set; }
        /// <summary>
        /// Table: BucketManagement
        /// </summary>
        public virtual DbSet<BucketManagement> BucketManagement { get; set; }
        /// <summary>
        /// Table: InvoiceBucket
        /// </summary>
        public virtual DbSet<InvoiceBucket> InvoiceBucket { get; set; }

        /// <summary>
        /// Table: UploadExcelPaths
        /// </summary>
        public virtual DbSet<UploadExcelPath> UploadExcelPath { get; set; }




        #region Custom Model 

        /// <summary>
        /// Table: EmployeeModel
        /// </summary>
        public virtual DbSet<EmployeeModel> EmployeeModel { get; set; }

        /// <summary>
        /// Table: InvoiceModel
        /// </summary>
        public virtual DbSet<InvoiceModel> InvoiceModel { get; set; }

        /// <summary>
        /// Table: AnchorNotifiaton
        /// </summary>
        public virtual DbSet<AnchorNotification> AnchorNotification { get; set; }

        /// <summary>
        /// Table: UserModel
        /// </summary>
        public virtual DbSet<UserModel> UserModel { get; set; }

        /// <summary>
        /// Table: PurchaseOrderModel
        /// </summary>
        public virtual DbSet<PurchaseOrderModel> PurchaseOrderModel { get; set; }

        /// <summary>
        /// Table: ChangePasswordModel
        /// </summary>
        public virtual DbSet<ChangePasswordModel> ChangePasswordModel { get; set; }

        /// <summary>
        /// Table: DocumentModel
        /// </summary>
        public virtual DbSet<DocumentModel> DocumentModel { get; set; }

        /// <summary>
        /// Table: UserLoginModel
        /// </summary>
        public virtual DbSet<UserLoginModel> UserLoginModel { get; set; }
        /// <summary>
        /// VendorDocument
        /// </summary>
        public virtual DbSet<VendorDocument> VendorDocument { get; set; }
        /// <summary>
        /// AnchorCompanyDocument
        /// </summary>
        public virtual DbSet<AnchorCompanyDocument> AnchorCompanyDocument { get; set; }
        /// <summary>
        /// InvoiceApprovalOrderModel
        /// </summary>
        public virtual DbSet<InvoiceApprovalOrderModel> InvoiceApprovalOrderModel { get; set; }

        public virtual DbSet<CompanyCredentialsModel> CompanyCredentialsModel { get; set; }
        public virtual DbSet<AdminLoginModel> AdminLoginModel { get; set; }
        public virtual DbSet<AnchorCompListingModel> AnchorCompListingModel { get; set; }
        public virtual DbSet<CompanyInvoiceListModel> CompanyInvoiceListModel { get; set; }
        public virtual DbSet<AvailableFundCompModel> AvailableFundCompModel { get; set; }
        // public virtual DbSet<InvoiceValueModel> InvoiceValueModel { get; set; }

        /// <summary>
        /// NotificationModel
        /// </summary>
        public virtual DbSet<NotificationModel> NotificationModel { get; set; }
        public virtual DbSet<BucketInvoiceModel> BucketInvoiceModel { get; set; }
        //public virtual DbSet<VendorDetModel> VendorDetModel { get; set; }

        /// <summary>
        /// BucketManagementModel
        /// </summary>
        public virtual DbSet<BucketManagementModel> BucketManagementModel { get; set; }
        public virtual DbSet<MaxAvailableAmount> MaxAvailableAmount { get; set; }


        /// <summary>
        /// DiscountOfferedInvModel
        /// </summary>
        public virtual DbSet<DiscountOfferedInvModel> DiscountOfferedInvModel { get; set; }
        /// <summary>
        /// InvoiceJourneyHistoryModel
        /// </summary>
        public virtual DbSet<InvoiceJourneyHistoryModel> InvoiceJourneyHistoryModel { get; set; }
        /// <summary>
        /// LostOpportunitiesModel
        /// </summary>
        public virtual DbSet<LostOpportunitiesModel> LostOpportunitiesModel { get; set; }
        /// <summary>
        /// AnchorCompDropDownModel
        /// </summary>
        public virtual DbSet<AnchorCompDropDownModel> AnchorCompDropDownModel { get; set; }
        /// <summary>
        /// AnalyticsFundingReqModel
        /// </summary>
        public virtual DbSet<AnalyticsFundingReqModel> AnalyticsFundingReqModel { get; set; }
        
        /// <summary>
        /// Invoice Receiveable due Today
        /// </summary>
        public virtual DbSet<RecievableDue> RecievableDue { get; set; }
        public virtual DbSet<ReceivabledueIndividual> ReceivabledueIndividuals { get; set; }
        /// <summary>
        /// AnalyticsFundingReqViewModel
        /// </summary>
        public virtual DbSet<AnalyticsFundingReqViewModel> AnalyticsFundingReqViewModel { get; set; }
        /// <summary>
        /// GraphDetailsModel
        /// </summary>
        public virtual DbSet<GraphDetailsModel> GraphDetailsModel { get; set; }


        ///<summary>
        /// AwaitInvoiceApproval        
        ///</summary>
        public virtual DbSet<AwaitInvoiceApprovalModel> AwaitInvoiceApprovalModel { get; set; }

        public virtual DbSet<DashboardListModel> DashboardListModel { get; set; }
        public virtual DbSet<InvoiceHistoryModel> InvoiceHistoryModel { get; set; }

        /// <summary>
        /// CriticalVendorsModel
        /// </summary>
        public virtual DbSet<CriticalVendorsModel> CriticalVendorsModel { get; set; }

        /// <summary>
        /// VendorsDropDownModel
        /// </summary>
        public virtual DbSet<VendorsDropDownModel> VendorsDropDownModel { get; set; }

     

        /// <summary>
        /// Anchor vendor list
        /// </summary>
        public virtual DbSet<VendorlistModel> VendorlistModel { get; set; }

        

        /// <summary>
        /// VendorPaymentDueModel
        /// </summary>
        public virtual DbSet<VendorPaymentDueModel> VendorPaymentDueModel { get; set; }

        /// <summary>
        /// VendorAwaitedInvApprovalModel
        /// </summary>
        public virtual DbSet<VendorAwaitedInvApprovalModel> VendorAwaitedInvApprovalModel { get; set; }

        /// <summary>
        /// VendorBucketAwaitedInvViewModel
        /// </summary>
        public virtual DbSet<VendorBucketAwaitedInvViewModel> VendorBucketAwaitedInvViewModel { get; set; }

        /// <summary>
        /// VendorBucketInvoiceViewModel
        /// </summary>
        public virtual DbSet<VendorBucketInvoiceViewModel> VendorBucketInvoiceViewModel { get; set; }

        /// <summary>
        /// VendorInvoiceApprovedTodayModel
        /// </summary>
        public virtual DbSet<VendorInvoiceApprovedTodayModel> VendorInvoiceApprovedTodayModel { get; set; }

        /// <summary>
        /// VendorInvApproveTodayViewModel
        /// </summary>
        public virtual DbSet<VendorInvApproveTodayViewModel> VendorInvApproveTodayViewModel { get; set; }

        /// <summary>
        /// VendorBucketWiseDiscInvViewModel
        /// </summary>
        public virtual DbSet<VendorBucketWiseDiscInvViewModel> VendorBucketWiseDiscInvViewModel { get; set; }

        /// <summary>
        /// VendorPaymentDueViewModel
        /// </summary>
        public virtual DbSet<VendorPaymentDueViewModel> VendorPaymentDueViewModel { get; set; }

        public virtual DbSet<AutoFunding> AutoFundings { get; set; }

        public virtual DbSet<GetCompanyModel> GetCompanyViewModel { get; set; }

        public virtual DbSet<GetLookUpModel> GetLookUpModel { get; set; }

        /// <summary>
        /// AnchorAnalyticLostOppModel
        /// </summary>
        public virtual DbSet<AnchorAnalyticLostOppModel> AnchorAnalyticLostOppModel { get; set; }

        public virtual DbSet<GetUploadVendorListModel> GetUploadVendorList { get; set; }

        public virtual DbSet<GetUploadVendorListModel1> GetUploadVendorList1 { get; set; }

        public virtual DbSet<GetRegisterMailTemplate> getRestraterMailTemplate { get; set; }

        public virtual DbSet<GetUserMailTemplate> getUserMailTemplate { get; set; }

        public virtual DbSet<GetVendorRegisterMailTemplate> GetVendorRegisterMailTemplate { get; set; }

        public virtual DbSet<GetInvoiceRegisterMailTemplate> GetInvoiceRegisterMailTemplate { get; set; }

        public virtual DbSet<GetForgetPasswordMailTemplate> getForgetPasswordMailTemplate { get; set; }

        public virtual DbSet<InvoiceStatus> GetInvoiceStatuses { get; set; }

        public virtual DbSet<Earning_Vendorwise> Earning_Vendorwises { get; set; }

        public virtual DbSet<Eearning_discountRateWise> Eearning_DiscountRates { get; set; }

        /// <summary>
        /// Finoassist MinPaymentDate
        /// </summary>
        public virtual DbSet<MinPaymentDate> MinPaymentDate { get; set; }

        /// <summary>
        /// Offered Discount Invoice Model
        /// </summary>
        public virtual DbSet<OfferedDiscountInvModel> OfferedDiscountInvModel { get; set; }

        /// <summary>
        /// UploadLogsModel
        /// </summary>
        public virtual DbSet<UploadLogsModel> UploadLogsModel { get; set; }

        /// <summary>
        /// CompanyBankView
        /// </summary>
        public virtual DbSet<CompanyBankView> CompanyBankView { get; set; }

        /// <summary>
        /// AnchorListModel
        /// </summary>
        public virtual DbSet<AnchorListModel> AnchorListModel { get; set; }

        public virtual DbSet<BankCompanyView> BankCompanyView { get; set; }
        #endregion

        #region Bank Model 
        public virtual DbSet<TopAnchorCompaniesInBankDashboardModel> TopAnchorCompany { get; set; }
        public virtual DbSet<DrawFundsFromBank> DrawFundsFromBank { get; set; }
        
         public virtual DbSet<BankDocumnet> BankDocumnet { get; set; }

        public virtual DbSet<BankDocumnet_list> BankDocumnet_list { get; set; }
        #endregion
        #region BankAnchorList Model 
        public virtual DbSet<BankAnchorListModel> GetBankSetLimitList { get; set; }
        #endregion
        public virtual DbSet<NoOfDaysForApproval> GetApprovalDays { get; set; }
        public virtual DbSet<SetBankAmountLimit> getDataByid { get; set; }

        public virtual DbSet<GetBankMailTemplate> GetBankMailTemplate { get; set; }


        /// <summary>
        /// Table: SetBankAmountLimit
        /// </summary>
        public virtual DbSet<SetBankAmountLimit> SetBankAmountLimit { get; set; }

        /// <summary>
        /// Table: SetBankAmountLimit
        /// </summary>
        public virtual DbSet<FundsRequestHistory> FundsRequestHistory { get; set; }

        /// <summary>
        /// Table: SetBankAmountLimit
        /// </summary>
        public virtual DbSet<FundsRequestHistoryView> FundsRequestHistoryView { get; set; }

        /// <summary>
        /// Table: RequestAmountReceivedToday
        /// </summary>
        public virtual DbSet<RequestAmountReceivedToday> RequestAmountReceivedToday { get; set; }


        /// <summary>
        /// Table: GetDisbursementList
        /// </summary>
        public virtual DbSet<DisbursementListModel> GetDisbursementList { get; set; }

        public virtual DbSet<DisbursementsModel> EditDisbursementDetail { get; set; }

        public virtual DbSet<BankLimitAnchorList> BankLimitAnchorLists { get; set; }

        /// <summary>
        /// BankGraphDetailsModel
        /// </summary>
        public virtual DbSet<BankGraphDetailsModel> BankGraphDetailsModel { get; set; }

        /// <summary>
        /// Table: BankDocumentDetails
        /// </summary>
        public virtual DbSet<BankDocumentDetails> BankDocumentDetails { get; set; }

        public virtual DbSet<GetBankKYCMailTemplate> GetBankKYCMailTemplate { get; set; }

        public virtual DbSet<KycUploadModel> GetKycUploadList { get; set; }

        public virtual DbSet<GetUploadDocumentListModel> GetUploadDocumentList { get; set; }

        public virtual DbSet<BankAnchorEmailDetails> GetAnchorEmail { get; set; }

        public virtual DbSet<GetBankApprovedMailTemplate> GetApprovedMailTemplate { get; set; }

        public virtual DbSet<FundsWithdrwanHistory> FundsWithdrwanHistories { get; set; } 
   

        public virtual DbSet<GetChangePasswordMailTemplate> GetChangePasswordMailTemplate { get; set; }

        public virtual DbSet<GetAuditTrailLogModel> GetAuditTrailLogListing { get; set; }

        public virtual DbSet<EditSendGridDetailsModel> SendGridEditPage { get; set; }

        public virtual DbSet<RuleofEngineDetails> RuleofEngineDetails { get; set; }

        public virtual DbSet<BankIDDetails> GetBankId { get; set; }
        public virtual DbSet<GetStatus> GetStatus { get; set; }

        public virtual DbSet<CompanyAnchorRateModel> CompanyAnchorRateModel { get; set; }

        public virtual DbSet<CheckSetLimit> CheckSetLimit { get; set; }

        public virtual DbSet<BankTotalAvailableLimits> BankTotalAvailableLimits { get; set; }


        public virtual DbSet<FundRequestFromBank> FundRequestFromBank { get; set; }

        public virtual DbSet<GetLeastAvail_bal> GetLeastAvail_bal { get; set; }
        public virtual DbSet<BankLimitAnchorListView> BankLimitAnchorListView { get; set; }

        public virtual DbSet<BankLimitLogView> BankLimitLogView { get; set; }
        public virtual DbSet<HolidayListModel> HolidayListModel { get; set; }

        //public virtual DbSet<HolidayList> HolidayList { get; set; }


        public virtual DbSet<GetDocument_Download> GetDocument_Download { get; set; }

        public virtual DbSet<SetLimitFromToDate> SetLimitFromToDate { get; set; }

        public virtual DbSet<InsertHoli_Reason> InsertHoli_Reason { get; set; }

        public virtual DbSet<DeleteHoli_Reason> DeleteHoli_Reason { get; set; }

        public virtual DbSet<EditHoli_Reason> EditHoli_Reason { get; set; }

        public virtual DbSet<BankLimitAnchorList> TotalExistingAnchor { get; set; }
        public virtual DbSet<GetEmailId> GetEmailId { get; set; }
        public virtual DbSet<ActionFilter> ActionFilter { get; set; }
        
        public virtual DbSet<GetSumOfAmountPaid> GetSumOfAmountPaid { get; set; }

        //public virtual DbSet<DocumentDetailList> DocumentDetailList { get; set; }

        public virtual DbSet<UserLockedResponse> userLockedResponse { get; set; }

        public virtual DbSet<TotalPendingKYC> TotalPendingKYC { get; set; }

        public virtual DbSet<UpcomingPayableGraphModel> upcomingPayableGraphModel { get; set; }
        public virtual DbSet<VendorDropdownModel> vendorDropdownModel { get; set; }
        public virtual DbSet<AnchorCompanyDropdownModel> anchorCompanyDropdownModel { get; set; }
        public virtual DbSet<UploadKYC> UploadKYC { get; set; }

        public virtual DbSet<SetLimitForAnchor> SetLimitForAnchor { get; set; }

        public virtual DbSet<GetUpcomingSetLimitChartData> GetUpcomingSetLimitChartData { get; set; }
        public virtual DbSet<HolidayDatesModel> holidayDates { get; set; }

        public virtual DbSet<AnchorVendorBankCount> AnchorVendorBankCount { get; set; }

        public virtual DbSet<NotificationDetail> NotificationDetail { get; set; }

        public virtual DbSet<GetNotificationDetail> GetNotificationDetail { get; set; }

        public virtual DbSet<SetLimitHistory> SetLimitHistory { get; set; }

        public virtual DbSet<AuditTrailHistory> AuditTrailHistory { get; set; }

        public virtual DbSet<TablesName> TablesName { get; set; }

       // public virtual DbSet<ColumnsName> ColumnsName { get; set; }
    }
}
