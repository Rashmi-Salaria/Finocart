using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Finocart.Services
{
    public class BankService : IBank

    {
        //#region Declartion 

        /// <summary>
        /// Context
        /// </summary>
        private readonly VMSContext _vContext;

        public BankService(VMSContext context)
        {
            _vContext = context;
        }




        #region Top Anchor Company in Dashboard
        public IEnumerable<TopAnchorCompaniesInBankDashboardModel> GetTopAnchor(string sortBy, int pageSize, int skip)
        {
            RepositoryService<TopAnchorCompaniesInBankDashboardModel> obj = new RepositoryService<TopAnchorCompaniesInBankDashboardModel>(_vContext);
            IEnumerable<TopAnchorCompaniesInBankDashboardModel> topachorcompanies;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();


                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));


                topachorcompanies = (obj.ExecWithStoreProcedure("Proc_GetTopAnchorCoampanyBank @sortBy,@PageSize,@Skip", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return topachorcompanies;

        }
        #endregion

        #region Finocredit 
        public IEnumerable<UploadKYC> getDrawFundsList(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId ,string NameofBank, string KYCStatus)
        {
            RepositoryService<UploadKYC> obj = new RepositoryService<UploadKYC>(_vContext);
            IEnumerable<UploadKYC> drawfunds;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + AnchorCompId + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@NameofBank", "" + NameofBank + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@KYCStatus", "" + KYCStatus + "", System.Data.SqlDbType.VarChar));

                drawfunds = (obj.ExecWithStoreProcedure("Proc_GetDrawFundsList @sortBy,@PageSize,@Skip,@AnchorID,@NameofBank,@KYCStatus", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }




            return drawfunds;
        }
        #endregion

        public IEnumerable<DrawFundsFromBank> getFundsHistoryList(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId)
        {
            RepositoryService<DrawFundsFromBank> obj = new RepositoryService<DrawFundsFromBank>(_vContext);
            IEnumerable<DrawFundsFromBank> drawfundshistory;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + AnchorCompId + "", System.Data.SqlDbType.Int));


                drawfundshistory = (obj.ExecWithStoreProcedure("Proc_FundsHistoryList @sortBy,@PageSize,@Skip,@AnchorID", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }




            return drawfundshistory;
        }

        public IEnumerable<DrawFundsFromBank> getFundsHistoryview(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId, string BankName, string Drawfunds)
        {
            RepositoryService<DrawFundsFromBank> obj = new RepositoryService<DrawFundsFromBank>(_vContext);
            IEnumerable<DrawFundsFromBank> drawfundshistoryview;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + AnchorCompId + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BankName", "" + BankName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Drawfunds", "" + Drawfunds + "", System.Data.SqlDbType.VarChar));


                drawfundshistoryview = (obj.ExecWithStoreProcedure("Proc_GetDrawFundsHistoryView @sortBy,@PageSize,@Skip,@AnchorID,@BankName,@Drawfunds", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }




            return drawfundshistoryview;
        }

        public IEnumerable<FundsWithdrwanHistory> fundsWithdrwanHistories(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId)
        {
            try
            {
                RepositoryService<FundsWithdrwanHistory> objVendorPaymentDueViewModel = new RepositoryService<FundsWithdrwanHistory>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + AnchorCompId + "", System.Data.SqlDbType.VarChar));
                var data = objVendorPaymentDueViewModel.ExecWithStoreProcedure("Proc_GetWithdrawnFundList @sortBy,@PageSize,@Skip,@AnchorID", parameters.ToArray());
                IEnumerable<FundsWithdrwanHistory> lstPaymentDueVendorsView = data.ToList();
                return lstPaymentDueVendorsView;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<FundsWithdrwanHistory> fundsWithdrwanHistoriesview(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId, Int32? UserId, string bankName)
        {
            try
            {
                RepositoryService<FundsWithdrwanHistory> objVendorPaymentDueViewModel = new RepositoryService<FundsWithdrwanHistory>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + AnchorCompId + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BankName", "" + bankName + "", System.Data.SqlDbType.VarChar));
                var data = objVendorPaymentDueViewModel.ExecWithStoreProcedure("Proc_GetWithdrawnFundListView @sortBy,@PageSize,@Skip,@AnchorID,@BankName", parameters.ToArray());
                IEnumerable<FundsWithdrwanHistory> lstPaymentDueVendorsView = data.ToList();
                return lstPaymentDueVendorsView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DrawFundsFromBank> getFundsFromBank(string sortBy, int pageSize, Int64 skip, Int32? AnchorCompId)
        {
            RepositoryService<DrawFundsFromBank> obj = new RepositoryService<DrawFundsFromBank>(_vContext);
            IEnumerable<DrawFundsFromBank> drawfundsfrombank;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", AnchorCompId, System.Data.SqlDbType.Int));


                drawfundsfrombank = (obj.ExecWithStoreProcedure("Proc_GetFundsRequestfromBank @sortBy,@PageSize,@Skip,@AnchorID", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return drawfundsfrombank;

        }

        public IEnumerable<FundRequestFromBank> getFundWithDrawDetailsperBank(string sortBy,int pageSize,Int64 skip, int ID)
        {
            RepositoryService<FundRequestFromBank> obj = new RepositoryService<FundRequestFromBank>(_vContext);
            IEnumerable<FundRequestFromBank> drawfundsfrombank;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Anchor_Company_index", ID, System.Data.SqlDbType.VarChar));


                drawfundsfrombank = (obj.ExecWithStoreProcedure("Proc_FundwithdrawalHistoryPerBank @sortBy,@PageSize,@Skip,@Anchor_Company_index", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return drawfundsfrombank;

        }
        public IEnumerable<BankDocumnet> BankDocumnet(int? AnchorCompId, int? BankID)
        {
            RepositoryService<BankDocumnet> obj = new RepositoryService<BankDocumnet>(_vContext);
            IEnumerable<BankDocumnet> bankdocument;

            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + AnchorCompId + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BankName", "" + BankID + "", System.Data.SqlDbType.BigInt));



                bankdocument = (obj.ExecWithStoreProcedure("Proc_GetBankDocumentDetails @AnchorID,@BankName", parameters.ToArray())).ToList();
            }
            catch (Exception ex) { throw ex; }

            return bankdocument;
        }

        public IEnumerable<GetDocument_Download> DrawFundsDocumentView(string sortBy, int pageSize, Int64 skip, int documentTypeID, Int64? AnchorCompId)
        {
            RepositoryService<GetDocument_Download> obj = new RepositoryService<GetDocument_Download>(_vContext);
            IEnumerable<GetDocument_Download> bankdocument;

            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@DocmentType_ID", documentTypeID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", AnchorCompId, System.Data.SqlDbType.VarChar));
                //parameters.Add(SQLHelper.SqlInputParam("@BankName", "" +  + "", System.Data.SqlDbType.VarChar));



                bankdocument = (obj.ExecWithStoreProcedure("Proc_GetKYCDocumentView @CompanyID,@DocmentType_ID,@sortBy,@PageSize,@Skip", parameters.ToArray())).ToList();
            }
            catch (Exception ex) { throw ex; }

            return bankdocument;
        }

        public IEnumerable<BankDocumnet_list> BankDocumnet_list()
        {
            RepositoryService<BankDocumnet_list> obj = new RepositoryService<BankDocumnet_list>(_vContext);
            IEnumerable<BankDocumnet_list> BankDocumnet_list;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                BankDocumnet_list = (obj.ExecWithStoreProcedure("Proc_GetBankDocumentDetails", parameters.ToArray())).ToList();


            }
            catch (Exception ex) { throw ex; }

            return BankDocumnet_list;
        }

        #region Get No Of Days For Approval
        public int Documnet_Submit(BankDocumnet_list model)
        {
            RepositoryService<BankDocumnet_list> obj = new RepositoryService<BankDocumnet_list>(_vContext);
            int result = 0;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + model.AnchorID + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@DocmentType_ID", "" + model.DocumentType_ID + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FilePath", "" + model.FilePath + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Status", "" + model.Status + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BankName", "" + model.BankName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CreatedBy", "" + model.CreatedBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ModifiedBy", "" + model.ModifiedBy + "", System.Data.SqlDbType.VarChar));
                result = obj.ExecuteSqlCommand("proc_AddUpdateDocument_List  @AnchorID,@DocmentType_ID,@FilePath,@Status,@BankName,@CreatedBy,@ModifiedBy", parameters.ToArray());



            }
            catch (Exception ex) { throw ex; }
            return result;
        }
        #endregion
        #region Insert/Update limit reocrd
        public int InsertUpdateLimitAmount(SetBankAmountLimit setBankAmountLimit, Int32? UserID)
        {
            int result = 0;

            try
            {
                RepositoryService<SetBankAmountLimit> obj = new RepositoryService<SetBankAmountLimit>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SetLimitID", setBankAmountLimit.Id, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", setBankAmountLimit.Anchor_id, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@AchorName", setBankAmountLimit.Anchor_Name, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AvailabelLimit", setBankAmountLimit.Available_Limits, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@UtilizeLimit", setBankAmountLimit.Utilized_Limits, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@IntrestRate", setBankAmountLimit.Interest_rate, System.Data.SqlDbType.Decimal));
                //parameters.Add(SQLHelper.SqlInputParam("@IntrestRateMonth", setBankAmountLimit.Interest_rate_month, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@ValidUpto", setBankAmountLimit.Validity_upto, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@AdddtionalDcumnet", setBankAmountLimit.Additional_document, System.Data.SqlDbType.VarChar));//set CreatedBy dynamically
                parameters.Add(SQLHelper.SqlInputParam("@NoOfDaysApproval", setBankAmountLimit.NumberOfDays, System.Data.SqlDbType.VarChar));//set UpdatedBy dynamically
                parameters.Add(SQLHelper.SqlInputParam("@Commnet", setBankAmountLimit.Comment, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorEmail", setBankAmountLimit.AnchorEmail, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ModifiedUserName", setBankAmountLimit.ModifiedUserName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ValidityFromto", setBankAmountLimit.ValidityFromto, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@ODBD", setBankAmountLimit.ODBD, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@userid", UserID, System.Data.SqlDbType.Int));
                result = obj.ExecuteSqlCommand("proc_AddUpdateLimitAmount  @SetLimitID, @AnchorID, @AchorName, @AvailabelLimit,@UtilizeLimit, @IntrestRate,@ValidUpto,@AdddtionalDcumnet,@NoOfDaysApproval,@Commnet,@AnchorEmail,@ModifiedUserName,@ValidityFromto,@ODBD,@userid", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion
        public int InsertDocumentDetails(Int64 DocumentTypeID, string DocumentName, string DocumentDescription, Int32? BankID, int AnchorCompID)
        {
            int result = 0;

            try
            {
                RepositoryService<BankDocumnet> obj = new RepositoryService<BankDocumnet>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@DocumentType_ID", DocumentTypeID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", AnchorCompID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@BankID", BankID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@DocumentName", DocumentName, System.Data.SqlDbType.VarChar));         
                parameters.Add(SQLHelper.SqlInputParam("@DocumentDescription", DocumentDescription, System.Data.SqlDbType.VarChar));//set CreatedBy dynamically                
                result = obj.ExecuteSqlCommand("proc_AddUpdateDocumentDetails  @DocumentType_ID, @AnchorID, @BankID, @DocumentName, @DocumentDescription", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int DeleteDocumentDetail(int ID)
        {
            int result = 0;
            try
            {
                RepositoryService<InsertHoli_Reason> objComp = new RepositoryService<InsertHoli_Reason>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ID", "" + ID + "", System.Data.SqlDbType.Int));
                result = objComp.ExecuteSqlCommand("DeleteDocumentDetail @ID", parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #region get Anchor Data     
        public IEnumerable<SetBankAmountLimit> getDataByid()
        {
            RepositoryService<SetBankAmountLimit> obj = new RepositoryService<SetBankAmountLimit>(_vContext);
            IEnumerable<SetBankAmountLimit> noOfDays;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SPType", "BankModifyLimit", System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("proc_GetLimitAmoutDataByAnchorID 5 ,'TestName'", parameters.ToArray());
                noOfDays = data.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
            return noOfDays;
        }
        #endregion


        #region Fund Request Details
        /// <summary>
        /// Get Funds Request History List
        /// </summary>

        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="AnchorName"></param>

        /// <returns></returns>
        public IEnumerable<FundsRequestHistory> GetFundRequestHistoryList(string sortBy, int pageSize, Int64 skip)
        {
            try
            {
                RepositoryService<FundsRequestHistory> objVendorPaymentDueModel = new RepositoryService<FundsRequestHistory>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorPaymentDueModel.ExecWithStoreProcedure("Proc_GetFundingReqListing   @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<FundsRequestHistory> lstPaymentDueVendors = data.ToList();
                return lstPaymentDueVendors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Fund Request Details View
        /// </summary>

        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>

        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IEnumerable<FundsRequestHistoryView> GetFundsRequestHistoryView(Int64 Anchor_id, string sortBy, int pageSize, Int64 skip)
        {
            try
            {
                RepositoryService<FundsRequestHistoryView> objVendorPaymentDueViewModel = new RepositoryService<FundsRequestHistoryView>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Anchor_id", 11, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                var data = objVendorPaymentDueViewModel.ExecWithStoreProcedure("Proc_GetFundingReqListingView  @Anchor_id, @Skip, @PageSize, @sortBy", parameters.ToArray());
                IEnumerable<FundsRequestHistoryView> lstPaymentDueVendorsView = data.ToList();
                return lstPaymentDueVendorsView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region EDIT details of Company
        /// <summary>
        /// Edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SetBankAmountLimit EditPage(int id, int BankId)
        {
            SetBankAmountLimit objCompanyModels = new SetBankAmountLimit();
            try
            {
                RepositoryService<SetBankAmountLimit> objCompany = new RepositoryService<SetBankAmountLimit>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@companyID", id, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@BankId", BankId, System.Data.SqlDbType.Int));
                var data = objCompany.ExecWithStoreProcedure("proc_GetLimitAmoutDataByAnchorID  @companyID,@BankId", parameters.ToArray());
                IEnumerable<SetBankAmountLimit> lstCompany = data.ToList();
                foreach (var v in lstCompany)
                {
                    objCompanyModels.Anchor_id = lstCompany.ElementAt(0).Anchor_id;
                    objCompanyModels.Available_Limits = lstCompany.ElementAt(0).Available_Limits;
                    objCompanyModels.Utilized_Limits = lstCompany.ElementAt(0).Utilized_Limits;
                    objCompanyModels.Interest_rate = lstCompany.ElementAt(0).Interest_rate;
                    //objCompanyModels.Interest_rate_month = lstCompany.ElementAt(0).Interest_rate_month;
                    objCompanyModels.Validity_upto = lstCompany.ElementAt(0).Validity_upto;
                    objCompanyModels.Additional_document = lstCompany.ElementAt(0).Additional_document;
                    objCompanyModels.NumberOfDays = lstCompany.ElementAt(0).NumberOfDays;
                    objCompanyModels.Comment = lstCompany.ElementAt(0).Comment;
                    objCompanyModels.Anchor_Name = lstCompany.ElementAt(0).Anchor_Name;
                    objCompanyModels.AnchorEmail = lstCompany.ElementAt(0).AnchorEmail;
                    objCompanyModels.ModifiedUserName = lstCompany.ElementAt(0).ModifiedUserName;
                    objCompanyModels.ValidityFromto = lstCompany.ElementAt(0).ValidityFromto;
                    objCompanyModels.ODBD = lstCompany.ElementAt(0).ODBD;
                    objCompanyModels.KYCStatus = lstCompany.ElementAt(0).KYCStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCompanyModels;
        }

        public IEnumerable<BankDocumnet> GetDocumentsList(int CompanyID, int Bankid)
        {
            BankDocumnet objCompanyModels = new BankDocumnet();
            try
            {
                RepositoryService<BankDocumnet> objCompany = new RepositoryService<BankDocumnet>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@companyID", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@BankId", Bankid, System.Data.SqlDbType.Int));
                var data = objCompany.ExecWithStoreProcedure("proc_GetDocumentsByAnchorID @companyID, @BankId", parameters.ToArray());
                IEnumerable<BankDocumnet> lstCompany = data.ToList();
                //foreach (var v in lstCompany)
                //{
                //    objCompanyModels.Anchor_id = lstCompany.ElementAt(0).Anchor_id;
                //    objCompanyModels.Available_Limits = lstCompany.ElementAt(0).Available_Limits;
                //    objCompanyModels.Utilized_Limits = lstCompany.ElementAt(0).Utilized_Limits;
                //    objCompanyModels.Interest_rate = lstCompany.ElementAt(0).Interest_rate;
                //    //objCompanyModels.Interest_rate_month = lstCompany.ElementAt(0).Interest_rate_month;
                //    objCompanyModels.Validity_upto = lstCompany.ElementAt(0).Validity_upto;
                //    objCompanyModels.Additional_document = lstCompany.ElementAt(0).Additional_document;
                //    objCompanyModels.NumberOfDays = lstCompany.ElementAt(0).NumberOfDays;
                //    objCompanyModels.Comment = lstCompany.ElementAt(0).Comment;
                //    objCompanyModels.Anchor_Name = lstCompany.ElementAt(0).Anchor_Name;
                //    objCompanyModels.AnchorEmail = lstCompany.ElementAt(0).AnchorEmail;
                //    objCompanyModels.ModifiedUserName = lstCompany.ElementAt(0).ModifiedUserName;
                //    objCompanyModels.ValidityFromto = lstCompany.ElementAt(0).ValidityFromto;
                //    objCompanyModels.ODBD = lstCompany.ElementAt(0).ODBD;

                //}
                return lstCompany;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion

        /// <summary>
        /// Get Top Anchor Data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TopAnchorCompaniesInBankDashboardModel> GetTopAnchor(string sortBy, int pageSize, Int64 skip, int UserId)
        {
            RepositoryService<TopAnchorCompaniesInBankDashboardModel> obj = new RepositoryService<TopAnchorCompaniesInBankDashboardModel>(_vContext);
            IEnumerable<TopAnchorCompaniesInBankDashboardModel> topachorcompanies;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserId", UserId, System.Data.SqlDbType.VarChar));


                topachorcompanies = (obj.ExecWithStoreProcedure("Proc_GetTopAnchorCoampanyBank @sortBy,@PageSize,@Skip,@UserId", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return topachorcompanies;

        }


        /// <summary>
        /// Get Top Anchor Data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BankDocumentDetails> GetEmail(string BankEmailID)
        {
            RepositoryService<BankDocumentDetails> obj = new RepositoryService<BankDocumentDetails>(_vContext);
            IEnumerable<BankDocumentDetails> topachorcompanies;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@BankName", "" + BankEmailID + "", System.Data.SqlDbType.VarChar));



                topachorcompanies = obj.ExecWithStoreProcedure("Proc_GetMailID @BankName", parameters.ToArray()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return topachorcompanies;


        }

        #region Get BankSet LimitList
        /// <summary>
        /// Get Bank Anchor Data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BankAnchorListModel> GetBankSetLimitList(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page, Int32? UserID)
        {
            try
            {

                RepositoryService<BankAnchorListModel> obj = new RepositoryService<BankAnchorListModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", "" + UserID + "", System.Data.SqlDbType.BigInt));
                var data = obj.ExecWithStoreProcedure("proc_getAnchorListingForBank @Page,@Skip,@PageSize,@sortBy,@CompanyName,@UserID", parameters.ToArray());
                IEnumerable<BankAnchorListModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public IEnumerable<BankLimitAnchorList> GetBankSetLimitAnchorLists(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page, Int32? UserID)
        {
            try
            {

                RepositoryService<BankLimitAnchorList> obj = new RepositoryService<BankLimitAnchorList>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@userid", UserID, System.Data.SqlDbType.Int));
                var data = obj.ExecWithStoreProcedure("proc_GetBankSetLimitAnchorlist @Page,@Skip,@PageSize,@sortBy,@CompanyName,@userid", parameters.ToArray());
                IEnumerable<BankLimitAnchorList> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BankLimitAnchorListView> GetBankLimitAnchorLists(string sortBy, int pageSize, Int64 skip, string CompanyName,Int64 CompanyID, string Page, Int32? UserID)
        {
            try
            {

                RepositoryService<BankLimitAnchorListView> obj = new RepositoryService<BankLimitAnchorListView>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@userid", UserID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.Int));
                var data = obj.ExecWithStoreProcedure("proc_GetBankLimitAnchorlist @Page,@Skip,@PageSize,@sortBy,@CompanyName,@userid,@CompanyID", parameters.ToArray());
                IEnumerable<BankLimitAnchorListView> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BankLimitLogView> GetBankLimitLogLists(string sortBy, int pageSize, Int64 skip, Int64 SetLimitID)
        {
            try
            {

                RepositoryService<BankLimitLogView> obj = new RepositoryService<BankLimitLogView>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SetLimitID", SetLimitID, System.Data.SqlDbType.Int));
                var data = obj.ExecWithStoreProcedure("proc_GetBankLimitLoglist @Skip,@PageSize,@sortBy,@SetLimitID", parameters.ToArray());
                IEnumerable<BankLimitLogView> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Get Vendor Register Mail  Template
        public IEnumerable<GetBankMailTemplate> GetBankMailTemplate(string LookupKey)
        {
            try
            {

                RepositoryService<GetBankMailTemplate> obj = new RepositoryService<GetBankMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@LookupFor", LookupKey, System.Data.SqlDbType.VarChar));

                var data = obj.ExecWithStoreProcedure("Proc_getBankEmailTemplate @LookupFor", parameters.ToArray());
                IEnumerable<GetBankMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get No Of Days For Approval
        public IEnumerable<NoOfDaysForApproval> GetApprovalDays()
        {
            RepositoryService<NoOfDaysForApproval> obj = new RepositoryService<NoOfDaysForApproval>(_vContext);
            IEnumerable<NoOfDaysForApproval> noOfDays;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SPType", "BankModifyLimit", System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("proc_GetLimitApprovalDate", parameters.ToArray());
                noOfDays = data.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
            return noOfDays;
        }
        #endregion

        #region Insert/Update DisbursementData in Database
        /// <summary>
        /// Getting Request Amount Received Today
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public string GetRequestAmountReceivedToday()
        {

            try
            {
                RepositoryService<RequestAmountReceivedToday> objRequestAmountToday = new RepositoryService<RequestAmountReceivedToday>(_vContext);

                var data = objRequestAmountToday.ExecWithStoreProcedure("proc_GetRequestAmountReceivedToday");
                IEnumerable<RequestAmountReceivedToday> requestAmount = data.ToList();

                return requestAmount.FirstOrDefault().RequestAmount.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Insert/Update DisbursementData in Database
        public int AddEditDisbursementData(DisbursementsModel disbursementsModel)
        {
            int result = 0;

            try
            {
                RepositoryService<SetBankAmountLimit> obj = new RepositoryService<SetBankAmountLimit>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@iD", disbursementsModel.id, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@DrawRequestId", disbursementsModel.DrawRequestId, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorName", disbursementsModel.AnchorName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FundsRequested", disbursementsModel.FundsRequested, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@RequestDate", disbursementsModel.RequestDate, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@PaidAmount", disbursementsModel.PaidAmount, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@PaymentDate", disbursementsModel.PaymentDate, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@PaymentStatus", disbursementsModel.PaymentStatus, System.Data.SqlDbType.VarChar));//set CreatedBy dynamically
                parameters.Add(SQLHelper.SqlInputParam("@UTRDetails", disbursementsModel.UTRDetails, System.Data.SqlDbType.VarChar));//set UpdatedBy dynamically
                parameters.Add(SQLHelper.SqlInputParam("@CreatedBy", disbursementsModel.CreatedBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UpdatedBy", disbursementsModel.UpdatedBy, System.Data.SqlDbType.VarChar));

                result = obj.ExecuteSqlCommand("proc_AddUpdateDisbursementData @iD, @DrawRequestId, @AnchorName, @FundsRequested,@RequestDate, @PaidAmount, @PaymentDate, @PaymentStatus ,@UTRDetails,@CreatedBy, @UpdatedBy", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Get DisbursementData List
        /// <summary>
        /// Get Bank Anchor Data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DisbursementListModel> GetDisbursementList(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page)
        {
            try
            {

                RepositoryService<DisbursementListModel> obj = new RepositoryService<DisbursementListModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("proc_GetDisbursementList @Page,@Skip,@PageSize,@sortBy,@CompanyName", parameters.ToArray());
                IEnumerable<DisbursementListModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region EDIT DisbursementData of Company

        public DisbursementsModel EditDisbursementDetail(int id)
        {
            DisbursementsModel objCompanyModels = new DisbursementsModel();
            try
            {
                RepositoryService<DisbursementsModel> objCompany = new RepositoryService<DisbursementsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ID", id, System.Data.SqlDbType.Int));
                var data = objCompany.ExecWithStoreProcedure("proc_EditDisbursementDetail  @ID", parameters.ToArray());
                IEnumerable<DisbursementsModel> lstCompany = data.ToList();
                foreach (var v in lstCompany)
                {
                    objCompanyModels.DrawRequestId = lstCompany.ElementAt(0).DrawRequestId;
                    objCompanyModels.AnchorName = lstCompany.ElementAt(0).AnchorName;
                    objCompanyModels.FundsRequested = lstCompany.ElementAt(0).FundsRequested;
                    objCompanyModels.RequestDate = lstCompany.ElementAt(0).RequestDate;
                    objCompanyModels.PaidAmount = lstCompany.ElementAt(0).PaidAmount;
                    objCompanyModels.PaymentDate = lstCompany.ElementAt(0).PaymentDate;
                    objCompanyModels.PaymentStatus = lstCompany.ElementAt(0).PaymentStatus;
                    objCompanyModels.UTRDetails = lstCompany.ElementAt(0).UTRDetails;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCompanyModels;
        }
        #endregion

        #region EDIT Fund Request Data from Bank 

        public SetBankAmountLimit EditFundRequestFromBank(int id)
        {

            SetBankAmountLimit objsetBankAmountLimit = new SetBankAmountLimit();
            
            try
            {
                RepositoryService<SetBankAmountLimit> objBank = new RepositoryService<SetBankAmountLimit>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ID",id,System.Data.SqlDbType.Int));
                var data = objBank.ExecWithStoreProcedure("proc_EditFundRequestFromBankDetail  @ID", parameters.ToArray());
                IEnumerable<SetBankAmountLimit> lstBank = data.ToList();
                foreach (var v in lstBank)
                {
                    objsetBankAmountLimit.Draw_funds = lstBank.ElementAt(0).Draw_funds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objsetBankAmountLimit;
        }
        #endregion




        #region Get Kyc List
        /// <summary>
        /// Get Bank Anchor Data
        /// <returns></returns>
        /// </summary>
        public IEnumerable<KycUploadModel> GetKycUploadList(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page, Int64? BankID)
        {
            try
            {

                RepositoryService<KycUploadModel> obj = new RepositoryService<KycUploadModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BankID", "" + BankID + "", System.Data.SqlDbType.BigInt));
                var data = obj.ExecWithStoreProcedure("proc_GetKycUploadList @Page,@Skip,@PageSize,@sortBy,@CompanyName,@BankID", parameters.ToArray());
                IEnumerable<KycUploadModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetGraphDetails
        /// <summary>
        /// Get Bank Company Graph Details
        /// </summary>
        /// <param name="month"></param>
        /// <param name="AnchorCompId"></param>
        /// <returns></returns>
        public IEnumerable<BankGraphDetailsModel> GetGraphDetails(string month, string BankName)
        {
            try
            {
                RepositoryService<BankGraphDetailsModel> objGraphDetailsModel = new RepositoryService<BankGraphDetailsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SP_TYPE", month, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BankName", BankName, System.Data.SqlDbType.VarChar));
                var data = objGraphDetailsModel.ExecWithStoreProcedure("Proc_getBankGraphDet  @SP_TYPE, @BankName", parameters.ToArray());
                IEnumerable<BankGraphDetailsModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get Uplaod Vendor Listing

        public IEnumerable<GetUploadDocumentListModel> GetUploadDocumentList(int CompanyID, string sortBy, int pageSize, Int64 skip)
        {
            try
            {

                RepositoryService<GetUploadDocumentListModel> obj = new RepositoryService<GetUploadDocumentListModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + CompanyID + "", System.Data.SqlDbType.BigInt));
                var data = obj.ExecWithStoreProcedure("proc_GetUploadDocumentList @Skip,@PageSize,@sortBy,@AnchorID", parameters.ToArray());
                IEnumerable<GetUploadDocumentListModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Get Vendor Register Mail  Template
        public IEnumerable<GetBankKYCMailTemplate> GetBankKYCMailTemplate(string LookupKey)
        {
            try
            {

                RepositoryService<GetBankKYCMailTemplate> obj = new RepositoryService<GetBankKYCMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@LookupFor", LookupKey, System.Data.SqlDbType.VarChar));

                var data = obj.ExecWithStoreProcedure("Proc_getBankKYCEmailTemplate @LookupFor", parameters.ToArray());
                IEnumerable<GetBankKYCMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update Approved Status

        public int ApprovedStatus(string CompanyID, Int32? BankID)
        {
            int result = 0;

            try
            {
                RepositoryService<SetBankAmountLimit> obj = new RepositoryService<SetBankAmountLimit>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@companyID", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Status",1, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@BankID", BankID, System.Data.SqlDbType.BigInt));
                result = obj.ExecuteSqlCommand("proc_UpdateApprovedStatus @CompanyID,@Status,@BankID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Update Approved Status

        public int RejectStatus(string CompanyID)
        {
            int result = 0;

            try
            {
                RepositoryService<SetBankAmountLimit> obj = new RepositoryService<SetBankAmountLimit>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Status", 2, System.Data.SqlDbType.Int));
                result = obj.ExecuteSqlCommand("proc_UpdateApprovedStatus @CompanyID,@Status", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Get Anchor Email
        public IEnumerable<BankAnchorEmailDetails> GetAnchorEmail(string CompanyName)
        {
            RepositoryService<BankAnchorEmailDetails> obj = new RepositoryService<BankAnchorEmailDetails>(_vContext);
            IEnumerable<BankAnchorEmailDetails> topachorcompanies;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyName, System.Data.SqlDbType.VarChar));
                topachorcompanies = obj.ExecWithStoreProcedure("Proc_GetAnchorMailID @CompanyID", parameters.ToArray()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return topachorcompanies;
        }
        #endregion

        #region Get Key Approved/Reject Mail  Template
        public IEnumerable<GetBankApprovedMailTemplate> GetApprovedMailTemplate(string LookupKey, int id)
        {
            try
            {

                RepositoryService<GetBankApprovedMailTemplate> obj = new RepositoryService<GetBankApprovedMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@LookupFor", id, System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("Proc_getKYCApprovedEmailTemplate @LookupFor", parameters.ToArray());
                IEnumerable<GetBankApprovedMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Insert/Update  Funds Request FromBank
        public int UpdateFundsRequestFromBank(SetBankAmountLimit setBankAmountLimitdata)
        {
            int result = 0;

            try
            {
                RepositoryService<SetBankAmountLimit> obj = new RepositoryService<SetBankAmountLimit>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Id", setBankAmountLimitdata.Id, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@Draw_funds", setBankAmountLimitdata.Draw_funds, System.Data.SqlDbType.Decimal));


                result = obj.ExecuteSqlCommand("proc_UpdateDrawFundsValue @Id, @Draw_funds", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Get Change Password Mail Template
        public IEnumerable<GetChangePasswordMailTemplate> GetChangePasswordMailTemplate(string LookupKey)
        {
            try
            {

                RepositoryService<GetChangePasswordMailTemplate> obj = new RepositoryService<GetChangePasswordMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@LookupFor", LookupKey, System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("Proc_GetChangePasswordEmailTemplate @LookupFor", parameters.ToArray());
                IEnumerable<GetChangePasswordMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get Bank  ID
        public IEnumerable<BankIDDetails> GetBankId(Int32? CompanyName)
        {
            RepositoryService<BankIDDetails> obj = new RepositoryService<BankIDDetails>(_vContext);
            IEnumerable<BankIDDetails> topachorcompanies;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyName, System.Data.SqlDbType.VarChar));
                topachorcompanies = obj.ExecWithStoreProcedure("Proc_GetBankID @CompanyID", parameters.ToArray()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return topachorcompanies;
        }
        #endregion

        #region Get Bank  ID
        public IEnumerable<CheckSetLimit> CheckSetLimit(int CompanyId)
        {
            RepositoryService<CheckSetLimit> obj = new RepositoryService<CheckSetLimit>(_vContext);
            IEnumerable<CheckSetLimit> objcheckSetLimits;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyId, System.Data.SqlDbType.VarChar));
                objcheckSetLimits = obj.ExecWithStoreProcedure("Proc_GetCheckSetLimit @CompanyID", parameters.ToArray()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objcheckSetLimits;
        }
        #endregion

        public int getBankTotalFundLimits(Int64 AnchorCompId)
        {
            RepositoryService<BankTotalAvailableLimits> obj = new RepositoryService<BankTotalAvailableLimits>(_vContext);

            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + AnchorCompId + "", System.Data.SqlDbType.VarChar));

                var data = (obj.ExecWithStoreProcedure("Proc_GetBankTotalFundLimits @AnchorID", parameters.ToArray()));
                IEnumerable<BankTotalAvailableLimits> TotalAvailableLimits = data.ToList();
                return TotalAvailableLimits.FirstOrDefault().ApproveCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        //public string GetLeastAvilable_Bal(GetLeastAvail_bal leastAvail_Bal)
        //{

        //    RepositoryService<GetLeastAvail_bal> obj = new RepositoryService<GetLeastAvail_bal>(_vContext);
        //    try
        //    {
        //        ICollection<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(SQLHelper.SqlInputParam("@Id",leastAvail_Bal.Least_Id ,System.Data.SqlDbType.BigInt));
        //        parameters.Add(SQLHelper.SqlInputParam("@Anchor_Company_index", leastAvail_Bal.Anchor_Company_index, System.Data.SqlDbType.Decimal));

        //        result = obj.ExecuteSqlCommand("GetLeastAvailable_balance @Id, @Anchor_Company_index", parameters.ToArray());


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}


        public IEnumerable<GetDocument_Download> getDocumentDownload(int DocumentTypeID, int? AnchorCompId)
        {
            RepositoryService<GetDocument_Download> obj = new RepositoryService<GetDocument_Download>(_vContext);
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AnchorID", "" + AnchorCompId + "", System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@DocumentTypeID", "" + DocumentTypeID + "", System.Data.SqlDbType.Int));
                var document = (obj.ExecWithStoreProcedure("Get_Document_Download @AnchorID,@DocumentTypeID", parameters.ToArray()));

                IEnumerable<GetDocument_Download> Document_downloads = document.ToList();
                //if (Document_downloads.Count() > 0)
                //{
                //    return Document_downloads.ToString();
                //}
                //else
                //{
                //    return null;
                //}
                return Document_downloads;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int? CheckFromToDate(Int32? BankID, DateTime fromDate, DateTime toDate, Int64 anchorCompId, Int64 setLimitId)
        {
            int? isExist = 0;
            try
            {
                RepositoryService<SetLimitFromToDate> objCompanyCredentialsModel = new RepositoryService<SetLimitFromToDate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", anchorCompId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@FrmDt", fromDate, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@ToDt", toDate, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@BankID", BankID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@setLimitId", setLimitId, System.Data.SqlDbType.BigInt));
                var result = objCompanyCredentialsModel.ExecWithStoreProcedure("proc_CheckSetLimitFromToDate @CompanyID,@BankID,@FrmDt,@ToDt,@setLimitId", parameters.ToArray());
                IEnumerable<SetLimitFromToDate> FromToDateCheck = result.ToList();

                isExist = FromToDateCheck.FirstOrDefault().Count;
            }
            catch (Exception)
            {

                throw;
            }

            return isExist;
        }
        public IEnumerable<BankLimitAnchorList> TotalExistingAnchor(string sortBy, int pageSize, Int64 skip, string CompanyName, string Page)
        {
            try
            {

                RepositoryService<BankLimitAnchorList> obj = new RepositoryService<BankLimitAnchorList>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
               
                var data = obj.ExecWithStoreProcedure("TotalExistingAnchor @Page,@Skip,@PageSize,@sortBy,@CompanyName", parameters.ToArray());
                IEnumerable<BankLimitAnchorList> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BankLimitAnchorListView> GetTotalBankLimitAnchorLists(string sortBy, int pageSize, Int64 skip, string CompanyName,string Page, Int32? UserID)
        {
            try
            {

                RepositoryService<BankLimitAnchorListView> obj = new RepositoryService<BankLimitAnchorListView>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@userid", UserID, System.Data.SqlDbType.Int));
               
                var data = obj.ExecWithStoreProcedure("proc_GetTotalBankLimitAnchorlist @Page,@Skip,@PageSize,@sortBy,@CompanyName,@userid", parameters.ToArray());
                IEnumerable<BankLimitAnchorListView> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TotalPendingKYC> GetTotalCompanyPendingKYCs()
        {
            try
            {
                RepositoryService<TotalPendingKYC> obj = new RepositoryService<TotalPendingKYC>(_vContext);

                var data = obj.ExecWithStoreProcedure("proc_TotalCompanyPendingKYCs");
                IEnumerable<TotalPendingKYC> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SetLimitForAnchor> GetSetLimitForAnchor()
        {
            try
            {
                RepositoryService<SetLimitForAnchor> obj = new RepositoryService<SetLimitForAnchor>(_vContext);

                var data = obj.ExecWithStoreProcedure("proc_GetSetLimitforAnchorCompany");
                IEnumerable<SetLimitForAnchor> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GetUpcomingSetLimitChartData> GetSetLimitforAnchorCompany(int AnchorCompID)
        {
            try
            {
                RepositoryService<GetUpcomingSetLimitChartData> obj = new RepositoryService<GetUpcomingSetLimitChartData>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@P_AnchorCompID", AnchorCompID, System.Data.SqlDbType.Int));
                var data = obj.ExecWithStoreProcedure("proc_GetUpcomingSetLimitChartData @P_AnchorCompID", parameters.ToArray());
                IEnumerable<GetUpcomingSetLimitChartData> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<HolidayListModel> GetHolidayListing(string sortBy, int pageSize, Int64 skip,string Reason,int? UserID)
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
    }
}
