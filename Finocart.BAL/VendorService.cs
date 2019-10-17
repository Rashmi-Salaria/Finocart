using Finocart.DBContext;
using Finocart.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Finocart.CustomModel;
using Microsoft.AspNetCore.Http;
using Finocart.Model;
using System.Globalization;

namespace Finocart.Services
{
    public class VendorService : IVendor
    {
        #region Declartion 

        /// <summary>
        /// Context
        /// </summary>
        private readonly VMSContext _vContext;
        //private readonly VendorDocument _VM;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public VendorService(VMSContext context)
        {
            _vContext = context;
           // _VM = vdocument;
        }

        #endregion

        #region

        /// <summary>
        /// validate vendor login
        /// </summary>
        /// <param name="PANNumber"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public UserLoginModel validateUser(string EmailID, string Password)
        {
            RepositoryService<UserLoginModel> objUserLoginModel = new RepositoryService<UserLoginModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@EmailID", EmailID, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@Password", Password, System.Data.SqlDbType.VarChar));
            var data = objUserLoginModel.ExecWithStoreProcedure("proc_CheckLogin @EmailID, @Password", parameters.ToArray());
            UserLoginModel vendor = data.SingleOrDefault();

            return vendor;
        }
        #endregion
        /// <summary>
        /// Get vendor documents
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IEnumerable<DocumentModel> GetVendorDocList(int? VendorID)  
        {
            RepositoryService<DocumentModel> objDocumentModel = new RepositoryService<DocumentModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorID, System.Data.SqlDbType.Int));
            var data = objDocumentModel.ExecWithStoreProcedure("Proc_GetVendorDocumentDet  @VendorID", parameters.ToArray());
            IEnumerable<DocumentModel> lstAnchorDocList = data.ToList();
            return lstAnchorDocList;
        }
        #region Get list of Today Recievable Due

        /// <summary>
        /// Get receivable dues today
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="Anchorid"></param>
        /// <param name="Anchorname"></param>
        /// <param name="TotalInvoiceAmount"></param>
        /// <param name="Page"></param>
        /// <param name="IsTotalCount"></param>
        /// <returns></returns>
        public IEnumerable<RecievableDue> GetRecieableduestoday(int? VendorId, string sortBy, int pageSize, Int64 skip, int Anchorid, string Anchorname, string TotalInvoiceAmount, string Page, int IsTotalCount)
        {
            RepositoryService<RecievableDue> objRecievableDue = new RepositoryService<RecievableDue>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SQLHelper.SqlInputParam("@vendorId", VendorId, System.Data.SqlDbType.Int));
            parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
            parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Decimal));
            parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@Anchorid",Anchorid, System.Data.SqlDbType.Int));
            parameters.Add(SQLHelper.SqlInputParam("@Anchorname",Anchorname, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@TotalInvoiceAmount", TotalInvoiceAmount, System.Data.SqlDbType.VarChar));
            var data = objRecievableDue.ExecWithStoreProcedure("recievabledue @vendorId,@Skip,@PageSize,@sortBy,@Anchorid,@Anchorname,@TotalInvoiceAmount",parameters.ToArray());
            IEnumerable<RecievableDue> list = data.ToList();
            return list;
        }
        #endregion

        #region

        /// <summary>
        /// Get receivale due individuals
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="companyid"></param>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortyBy"></param>
        /// <param name="PONumber"></param>
        /// <param name="Page"></param>
        /// <param name="IsTotalCount"></param>
        /// <returns></returns>
        public IEnumerable<ReceivabledueIndividual> GetReceivabledueIndividuals(Int64? VendorId, int? companyid, Int64 skip, int pageSize, string sortyBy, string PONumber, string Page, int IsTotalCount)
        {
            RepositoryService<ReceivabledueIndividual> objReceivabledueIndividual = new RepositoryService<ReceivabledueIndividual>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.BigInt));
            parameters.Add(SQLHelper.SqlInputParam("@CompanyId", companyid, System.Data.SqlDbType.Int));
            parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
            parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
            parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortyBy, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@PONumber", PONumber, System.Data.SqlDbType.VarChar));
            var data = objReceivabledueIndividual.ExecWithStoreProcedure("proc_GetDailyReceiveinvoiceDetails @CompanyId,@Skip,@PageSize,@sortBy,@PONumber,@VendorID", parameters.ToArray());
            IEnumerable<ReceivabledueIndividual> list = data.ToList();
            return list;


        }
        #endregion

        #region Insert vendor's document details
        /// <summary>
        /// Inser vendor's document details
        /// </summary>
        /// <param name="AnchorDocument"></param>
        /// <param name="iVendorID"></param>
        /// <param name="iDocumentTypeID"></param>
        /// <param name="ToSaveFileName"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int InsertDocumentDet(IFormFile AnchorDocument, int? iVendorID, int iDocumentTypeID,string ToSaveFileName,Int32? UserId)
        {
            int result = 0;
            try
            {
                RepositoryService<DocumentModel> objDocumentModel = new RepositoryService<DocumentModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", iVendorID, System.Data.SqlDbType.BigInt));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@FileName", AnchorDocument.FileName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@DocumentTypeID", iDocumentTypeID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@UploadedBy", UserId, System.Data.SqlDbType.Int));//set it dynamic
                parameters.Add(SQLHelper.SqlInputParam("@LocalFolderFileName", ToSaveFileName, System.Data.SqlDbType.VarChar));
                result = objDocumentModel.ExecuteSqlCommand("Proc_InsertVendorDocDet  @VendorID,@FileName, @DocumentTypeID, @UploadedBy,@LocalFolderFileName", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get File Name By Id
        /// <summary>
        /// Get File Name By Id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public VendorDocument GetFileNameById(Int64 ID)
        {
            VendorDocument vm = new VendorDocument();
            RepositoryService<VendorDocument> objVendorDocument = new RepositoryService<VendorDocument>(_vContext);
            vm = objVendorDocument.GetByID(ID);
          
            return vm;
        }
        #endregion

        #region Remove vendor document reocrd from db
        /// <summary>
        /// Remove vendor document reocrd from db
        /// </summary>
        /// <param name="vendorDocument"></param>
        /// <returns></returns>
        public int DeleteVendorDocRecord(VendorDocument vendorDocument)
        {
            int result = 0;
            try
            {
                RepositoryService<VendorDocument> objVendorDocument = new RepositoryService<VendorDocument>(_vContext);
                result = objVendorDocument.Delete(vendorDocument);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        #region Get anchor company name by company id
        /// <summary>
        /// Get anchor company name by company id
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public Invoice GetCompanyNameByCompanyId(Int64 CompanyId)
        {
            Invoice invoice = new Invoice();
            RepositoryService<Invoice> objInvoice = new RepositoryService<Invoice>(_vContext);
            invoice = objInvoice.GetByID(CompanyId);

            return invoice;
        }
        #endregion

        #region
        /// <summary>
        /// Insert invoice bucket discount
        /// </summary>
        /// <param name="bucketInvoiceModel"></param>
        /// <returns></returns>
        public int InsertInvoiceBucketDiscountDet(BucketInvoiceModel bucketInvoiceModel)
        {
            int result = 0;
            string PaymentDate = null;
            string InvoiceIdStr = bucketInvoiceModel.InvoiceIDStr.TrimEnd(',');
            if (bucketInvoiceModel.ValidToDate != null)
            { PaymentDate = bucketInvoiceModel.ValidToDate.TrimEnd(','); }
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                

                RepositoryService<BucketInvoiceModel> objBucketInvoiceModel = new RepositoryService<BucketInvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@BucketId", bucketInvoiceModel.BucketID == null ? 0 : bucketInvoiceModel.BucketID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@BucketName", bucketInvoiceModel.BucketName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BucketStatus", bucketInvoiceModel.BucketStatus == null ? 4 : bucketInvoiceModel.BucketStatus, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Discount", bucketInvoiceModel.DiscountPercentage, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@UserId", bucketInvoiceModel.UserId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@ValidToDate", PaymentDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceIdString", InvoiceIdStr, System.Data.SqlDbType.VarChar));

                result = objBucketInvoiceModel.ExecuteSqlCommand("Proc_InsertInvoiceBucketDet  @BucketId,@BucketName, @BucketStatus, @Discount, @UserId, @ValidToDate,@InvoiceIdString", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int SendNotification(BucketInvoiceModel bucketInvoiceModel, Int32? UserId, Int32? VendorId)
        {
            int result = 0;
            string InvoiceIdStr = bucketInvoiceModel.InvoiceIDStr.TrimEnd(',');
            try
            {
                RepositoryService<BucketInvoiceModel> objBucketInvoiceModel = new RepositoryService<BucketInvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@RoleID", 1, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceIdString", InvoiceIdStr, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Discount", bucketInvoiceModel.DiscountPercentage, System.Data.SqlDbType.Decimal));

                result = objBucketInvoiceModel.ExecuteSqlCommand("Proc_InsertAnchorNotify  @RoleID,@UserID,@VendorID,@InvoiceIdString,@Discount", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Company GetCompany(Int32? ID)
        {
            Company company = new Company();
            RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
            company = objCompany.GetByID(ID);
            return company;
        }

        /// <summary>
        /// Insert invoice bucket discount
        /// </summary>
        /// <param name="bucketInvoiceModel"></param>
        /// <returns></returns>
        public int InsertFinoInvoiceBucketDiscountDet(BucketInvoiceModel bucketInvoiceModel)
        {
            int result = 0;
            string InvoiceIdStr = bucketInvoiceModel.InvoiceIDStr.TrimEnd(',');
            DateTime PaymentDate = Convert.ToDateTime(bucketInvoiceModel.ValidToDate.TrimEnd(','));
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);


                RepositoryService<BucketInvoiceModel> objBucketInvoiceModel = new RepositoryService<BucketInvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@BucketId", bucketInvoiceModel.BucketID == null ? 0 : bucketInvoiceModel.BucketID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@BucketName", bucketInvoiceModel.BucketName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BucketStatus", bucketInvoiceModel.BucketStatus == null ? 4 : bucketInvoiceModel.BucketStatus, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Discount", bucketInvoiceModel.DiscountPercentage, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@UserId", bucketInvoiceModel.UserId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@ValidToDate", PaymentDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceIdString", InvoiceIdStr, System.Data.SqlDbType.VarChar));

                result = objBucketInvoiceModel.ExecuteSqlCommand("Proc_InsertFinoInvoiceBucketDet  @BucketId,@BucketName, @BucketStatus, @Discount, @UserId, @ValidToDate,@InvoiceIdString", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public IEnumerable<GetBankMailTemplate> GetDiscInvMailTemplate(string LookupKey)
        {
            try
            {

                RepositoryService<GetBankMailTemplate> obj = new RepositoryService<GetBankMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();


                var data = obj.ExecWithStoreProcedure("Proc_getDiscInvEmailTemplate", parameters.ToArray());
                IEnumerable<GetBankMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get Email Id by invoice Id
        /// <summary>
        /// Get email by invoice id
        /// </summary>
        /// <param name="strInvoiceId"></param>
        /// <returns></returns>
        public IEnumerable<BucketInvoiceModel> GetEmailByInvoiceId(string strInvoiceId)
        {
            try
            {
                RepositoryService<BucketInvoiceModel> objBucketInvoiceModel = new RepositoryService<BucketInvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceIdString", strInvoiceId, System.Data.SqlDbType.VarChar));

                var data = objBucketInvoiceModel.ExecWithStoreProcedure("PROC_GetEmailIDByInvoiceId  @InvoiceIdString", parameters.ToArray());
                IEnumerable<BucketInvoiceModel> lstEmailInv = data.ToList();
                return lstEmailInv;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get Finoassist Invoice By Amount
        /// <summary>
        /// Get invoice by amount
        /// </summary>
        /// <param name="sumAssuredAmount"></param>
        /// <param name="VendorId"></param>
        /// <param name="Skip"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortBy"></param>
        /// <param name="fundingDate"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceModel> GetInvoicesByAmount(decimal sumAssuredAmount, int? VendorId, int Skip, int PageSize, string SortBy, string fundingDate, decimal discount)
        {
            try
            {
                RepositoryService<InvoiceModel> objInvoiceModel = new RepositoryService<InvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@vendorId", VendorId, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sumAssuredAmount", sumAssuredAmount, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", Skip, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", PageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", SortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@fundingDate", fundingDate, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@discount", discount, System.Data.SqlDbType.VarChar));
                var data = objInvoiceModel.ExecWithStoreProcedure("Proc_GetInvoiceListByAmount  @vendorId, @sumAssuredAmount, @Skip, @PageSize, @sortBy, @fundingDate, @discount", parameters.ToArray());
                IEnumerable<InvoiceModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Finoassist Invoice By Anchor Company
        /// <summary>
        /// Get max available amount
        /// </summary>
        /// <param name="VendorID"></param>
        /// <param name="AnchoCompany"></param>
        /// <returns></returns>
        public string GetMaxAvailableAmount(Int64? VendorID, string AnchoCompany)
        {
           
            try
            {
                RepositoryService<MaxAvailableAmount> objMaxAvailableAmount = new RepositoryService<MaxAvailableAmount>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VedorID", VendorID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyID", AnchoCompany, System.Data.SqlDbType.VarChar));


                var data = objMaxAvailableAmount.ExecWithStoreProcedure("proc_GetMaxAvailableAmtByAnchorComp @VedorID, @AnchorCompanyID", parameters.ToArray());
                IEnumerable<MaxAvailableAmount> maxAmount = data.ToList();
                return maxAmount.FirstOrDefault().MaxVendorAmountAvailable.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get invoice by anchor
        /// </summary>
        /// <param name="sumAssuredAmount"></param>
        /// <param name="VendorId"></param>
        /// <param name="Skip"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortBy"></param>
        /// <param name="fundingDate"></param>
        /// <param name="discount"></param>
        /// <param name="anchoCompany"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceModel> GetInvoicesByAnchor(decimal sumAssuredAmount, int? VendorId, int Skip, int PageSize, string SortBy, string fundingDate, decimal discount, string anchoCompany)
        {
            try
            {
                RepositoryService<InvoiceModel> objInvoiceModel = new RepositoryService<InvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@vendorId", VendorId, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sumAssuredAmount", sumAssuredAmount, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", Skip, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", PageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", SortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@fundingDate", fundingDate, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@discount", discount, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyID", anchoCompany, System.Data.SqlDbType.VarChar));
                var data = objInvoiceModel.ExecWithStoreProcedure("Proc_GetInvoiceListByAnchor  @vendorId, @sumAssuredAmount, @Skip, @PageSize, @sortBy, @fundingDate, @discount, @AnchorCompanyID", parameters.ToArray());
                IEnumerable<InvoiceModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region
        /// <summary>
        /// Get min payment date
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public string GetMinFundPaymentDate(Int64? VendorID)
        {
          
            try
            {
                RepositoryService<MinPaymentDate> objMinPaymentDate = new RepositoryService<MinPaymentDate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@vendorId", VendorID, System.Data.SqlDbType.Int));
                var data = objMinPaymentDate.ExecWithStoreProcedure("Proc_GetFinoassistMinPaymentDate @vendorId", parameters.ToArray());
                IEnumerable<MinPaymentDate> maxAmount = data.ToList();
                return maxAmount.FirstOrDefault().MinFundPaymentDate.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get discount offered invoices list
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
        public IEnumerable<OfferedDiscountInvModel> GetDiscountOfferedInvoiceList(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string InvoiceAmt, string ConditionParam, int? CompanyID, Int32? VendorId)
        {
            try
            {
                RepositoryService<OfferedDiscountInvModel> obj = new RepositoryService<OfferedDiscountInvModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyName", AnchorCompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceAmt", InvoiceAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PONumber", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceNo", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyId", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@IsDiscountNull", 2, System.Data.SqlDbType.SmallInt));//Here 2 is a decision making parameter
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.BigInt));

                var data = obj.ExecWithStoreProcedure("proc_GetDiscountOfferedInvoiceDet  @Skip, @PageSize, @sortBy, @AnchorCompanyName, @InvoiceAmt,@PONumber,@InvoiceNo,@CompanyId,@IsDiscountNull,@VendorID", parameters.ToArray());
                IEnumerable<OfferedDiscountInvModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get invoice journey history
        /// <summary>
        /// Get invoice journey history
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <param name="PageName"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceJourneyHistoryModel> GetInvoiceJourneyHistory(Int64 InvoiceId,string PageName)
        {
            try
            {
                RepositoryService<InvoiceJourneyHistoryModel> objInvoiceJourneyHistoryModel = new RepositoryService<InvoiceJourneyHistoryModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceId", InvoiceId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@PageName", PageName, System.Data.SqlDbType.VarChar));
                var data = objInvoiceJourneyHistoryModel.ExecWithStoreProcedure("Proc_GetInvoiceJourneyHistory  @InvoiceId,@PageName", parameters.ToArray());
                IEnumerable<InvoiceJourneyHistoryModel> lstInvoiceJourneyHistory = data.ToList();
                return lstInvoiceJourneyHistory;   
            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }
        #endregion

        #region
        /// <summary>
        /// Get email by vendor
        /// </summary>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        public User GetEmailIdByVendorId(int? VendorId)
        {
            try
            {
                RepositoryService<User> objUser = new RepositoryService<User>(_vContext);
                User user = new User();
                user = objUser.GetByID(VendorId);
                
                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Get graphic details
        /// </summary>
        /// <param name="month"></param>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        public IEnumerable<GraphDetailsModel> GetGraphDetails(string month,Int64? VendorId)
        {
            try
            {
                RepositoryService<GraphDetailsModel> objGraphDetailsModel = new RepositoryService<GraphDetailsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SP_TYPE", month, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@vendorId", VendorId, System.Data.SqlDbType.VarChar));
                var data = objGraphDetailsModel.ExecWithStoreProcedure("Proc_getVendorGraphDet  @SP_TYPE, @vendorId", parameters.ToArray());
                IEnumerable<GraphDetailsModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region ApprovalInvoice
        /// <summary>
        /// Get invoice todays list
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="Companyname"></param>
        /// <param name="Approvedamt"></param>
        /// <returns></returns>
        public IEnumerable<DiscountOfferedInvModel> getInvoicetodaysList(string sortBy, int pageSize, Int64 skip, string Companyname, string Approvedamt, Int32? VendorId)
        {
            try
            {
                RepositoryService<DiscountOfferedInvModel> objDiscountOfferedInvModel = new RepositoryService<DiscountOfferedInvModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyName", Companyname, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Approvedamt", Approvedamt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.VarChar));

                var data = objDiscountOfferedInvModel.ExecWithStoreProcedure("proc_GetTodaysInvoiceDet  @Skip, @PageSize, @sortBy, @AnchorCompanyName, @Approvedamt, @VendorID", parameters.ToArray());
                IEnumerable<DiscountOfferedInvModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Available for funding
        /// <summary>
        /// get available for funding list
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="Companyname"></param>
        /// <param name="Approvedamt"></param>
        /// <param name="Page"></param>
        /// <param name="VendorId"></param>
        /// <returns></returns>
         public IEnumerable<AvailableFundCompModel> getAvailableforfundingList(string sortBy, int pageSize, Int64 skip, string Companyname, string Approvedamt,string Page,Int64? VendorId)
        {
            try
            {
                RepositoryService<AvailableFundCompModel> objAvailableFundCompModel = new RepositoryService<AvailableFundCompModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();              
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyName", Companyname, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceAmt", Approvedamt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                var data = objAvailableFundCompModel.ExecWithStoreProcedure("proc_GetAvailableForFunding  @Skip, @PageSize, @sortBy, @AnchorCompanyName, @InvoiceAmt,@Page,@VendorID", parameters.ToArray());
                IEnumerable<AvailableFundCompModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Funding rate Update
        #endregion
        /// <summary>
        /// Insert funding rate and date
        /// </summary>
        /// <param name="ObjAutoFunding"></param>
        /// <returns></returns>
        public int InsertFundingRateAndDate(AutoFunding ObjAutoFunding)
        {
            int result = 0;
            try
            {
                RepositoryService<AutoFunding> objAutoFunding = new RepositoryService<AutoFunding>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Rate", ObjAutoFunding.DiscuntRate, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@Date", ObjAutoFunding.DiscuntVlidDate, System.Data.SqlDbType.DateTime));                
                result = objAutoFunding.ExecuteSqlCommand("proc_AddFundingRate  @Rate,@Date", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Invoice history list
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="AnchorCompanyName"></param>
        /// <param name="TotalInvoiceAmmount"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceHistoryModel> InvoiceHistorylist(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string TotalInvoiceAmmount, Int32? VendorID)
        {
            try
            {
                RepositoryService<InvoiceHistoryModel> objInvoiceHistoryModel = new RepositoryService<InvoiceHistoryModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyName", AnchorCompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Invoicetotatalamt", TotalInvoiceAmmount, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceStatus", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceNo", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorID, System.Data.SqlDbType.VarChar));

                var data = objInvoiceHistoryModel.ExecWithStoreProcedure("proc_GetInvoiceHistory  @sortBy, @PageSize, @Skip, @AnchorCompanyName, @Invoicetotatalamt,@InvoiceStatus,@VendorID", parameters.ToArray());
                IEnumerable<InvoiceHistoryModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get auto funding
        /// </summary>
        /// <param name="AutoFudngId"></param>
        /// <returns></returns>
        public IEnumerable<AutoFunding> GetAutoFundings(Int64? AutoFudngId)
        {
            try
            {
                RepositoryService<AutoFunding> objAutoFunding = new RepositoryService<AutoFunding>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AutoFudngId", AutoFudngId, System.Data.SqlDbType.Int));
                var data = objAutoFunding.ExecWithStoreProcedure("GetFunding  @AutoFudngId", parameters.ToArray());
                IEnumerable<AutoFunding> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Update funding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int Updatefunding(string id,string rate,string date)
        {
            int result = 0;

            try
            {
                string AutoFudngId = id;
                string DiscuntRate = rate;
                string DiscuntVlidDate = date;
                RepositoryService<AutoFunding> objAutoFunding = new RepositoryService<AutoFunding>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@AutoFudngId", AutoFudngId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@DiscuntRate", DiscuntRate, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@DiscuntVlidDate", DiscuntVlidDate, System.Data.SqlDbType.DateTime));
               result = objAutoFunding.ExecuteSqlCommand("UPDATEFUNDING  @AutoFudngId,@DiscuntRate,@DiscuntVlidDate", parameters.ToArray());


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
        public IEnumerable<UpcomingPayableGraphModel> GetReceivableReceivedGraphData(Int64? AnchorCompId, Int64? vendorId, Int64? DataTypeId)
        {
            try
            {
                RepositoryService<UpcomingPayableGraphModel> objGraphDetailsModel = new RepositoryService<UpcomingPayableGraphModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@P_AnchorCompID", AnchorCompId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@P_VendorId", vendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@P_DataTypeId", DataTypeId, System.Data.SqlDbType.Int));
                var data = objGraphDetailsModel.ExecWithStoreProcedure("proc_GetPayableAndReceivedChartData @P_AnchorCompID,@P_VendorId,@P_DataTypeId", parameters.ToArray());
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
        /// <param name="vendorId"></param>
        /// <param name="DataTypeId"></param>
        /// <returns></returns>
        public IEnumerable<AnchorCompanyDropdownModel> GetAnchorCompanyForVendor(Int64? vendorId, Int64? DataTypeId)
        {
            try
            {
                RepositoryService<AnchorCompanyDropdownModel> anchorCompanyDropdownModel = new RepositoryService<AnchorCompanyDropdownModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@P_VendorId", vendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@P_DataTypeId", DataTypeId, System.Data.SqlDbType.Int));
                var data = anchorCompanyDropdownModel.ExecWithStoreProcedure("proc_GetAnchorCompanyListForInvoices @P_VendorId,@P_DataTypeId", parameters.ToArray());
                IEnumerable<AnchorCompanyDropdownModel> lstAnchorCompany = data.ToList();
                return lstAnchorCompany;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GetRegisterMailTemplate> getRestraterMailTemplate()
        {
            try
            {

                RepositoryService<GetRegisterMailTemplate> objGetRegisterMailTemplate = new RepositoryService<GetRegisterMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                var data = objGetRegisterMailTemplate.ExecWithStoreProcedure("Proc_getRegisterEmailTemplate");
                IEnumerable<GetRegisterMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Get Holiday Dates
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HolidayDatesModel> GetHolidayDates()
        {
            try
            {
                RepositoryService<HolidayDatesModel> holidayDateList = new RepositoryService<HolidayDatesModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                var data = holidayDateList.ExecWithStoreProcedure("proc_GetHolidaysList", parameters.ToArray());
                IEnumerable<HolidayDatesModel> lstDates = data.ToList();
                return lstDates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
