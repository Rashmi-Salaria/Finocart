using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class InvoiceService : IInvoice
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
        public InvoiceService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion

        #region Invoice Listing
        /// <summary>
        /// Get Module Status
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleMaster> getModuleStatus()
        {
            RepositoryService<ModuleMaster> objModuleMaster = new RepositoryService<ModuleMaster>(_vContext);
            //obj.SelectAll();
            return objModuleMaster.SelectAll();
        }

        /// <summary>
        /// Get Invoice List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InvoiceModel> getInvoiceList(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string InvoiceStatus,string ConditionParam,int? CompanyID, Int64? VendorId)
        {
            try
            {
                RepositoryService<InvoiceModel> objInvoiceModel = new RepositoryService<InvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyName", AnchorCompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceStatus", InvoiceStatus, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PONumber", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceNo", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyId", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@IsDiscountNull", ConditionParam== "VDashboardScreen" ? 3:1, System.Data.SqlDbType.SmallInt));//Here 1 and 3 are decision making parametres.It's definition is in sql
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.BigInt));

                var data = objInvoiceModel.ExecWithStoreProcedure("proc_GetInvoiceDet  @Skip, @PageSize, @sortBy, @AnchorCompanyName, @InvoiceStatus,@PONumber,@InvoiceNo,@CompanyId,@IsDiscountNull,@VendorID", parameters.ToArray());
                IEnumerable<InvoiceModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                //dbErrorLogging.LogError(ex);
                throw ex;
            }
        }

        #endregion

        #region Get last searched list
        /// <summary>
        /// Get record of last searched keys
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SearchHistory> GetSearchHistories()
        {
            try
            {
                RepositoryService<SearchHistory> objSearchHistory = new RepositoryService<SearchHistory>(_vContext);
                var data = objSearchHistory.SelectAll();
                IEnumerable<SearchHistory> lstsearchHistories = data.ToList();
                return lstsearchHistories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get Max Vendor Amount
       
        /// <summary>
        /// Getting max avail amount
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public string GetMaxAvailableAmount(int? VendorID)
        {
            //string maxAvailableAmount = string.Empty;
            try
            {
                RepositoryService<MaxAvailableAmount> objMaxAvailableAmount = new RepositoryService<MaxAvailableAmount>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VedorID",VendorID, System.Data.SqlDbType.Int));
                

                var data = objMaxAvailableAmount.ExecWithStoreProcedure("proc_GetMaxAvailableVendorAmount @VedorID", parameters.ToArray());
                IEnumerable<MaxAvailableAmount> maxAmount = data.ToList();
                //maxAvailableAmount = maxAmount.First().MaxVendorAmountAvailable.ToString();
                return maxAmount.FirstOrDefault().MaxVendorAmountAvailable.ToString();

            }
            catch (Exception ex)
            {
                //dbErrorLogging.LogError(ex);
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// View available amount for funding
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="AnchorCompanyName"></param>
        /// <param name="TotalInvoiceAmmount"></param>
        /// <param name="ConditionParam"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceModel> ViewAvailableforfunding(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string TotalInvoiceAmmount, string ConditionParam, int? CompanyID, Int32? VendorId)
        {
            try
            {
                RepositoryService<InvoiceModel> objInvoiceModel = new RepositoryService<InvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyName", AnchorCompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Invoicetotatalamt", TotalInvoiceAmmount, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PONumber", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceNo", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyId", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@IsDiscountNull", ConditionParam == "VDashboardScreen" ? 3 : 1, System.Data.SqlDbType.SmallInt));//Here 1 and 3 are decision making parametres.It's definition is in sql
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.BigInt));

                var data = objInvoiceModel.ExecWithStoreProcedure("proc_GetViewAvailableForFunding  @Skip, @PageSize, @sortBy, @AnchorCompanyName, @Invoicetotatalamt,@PONumber,@InvoiceNo,@CompanyId,@IsDiscountNull,@VendorID", parameters.ToArray());
                IEnumerable<InvoiceModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }

        /// <summary>
        /// View await invoice amount
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="AnchorCompanyName"></param>
        /// <param name="TotalInvoiceAmmount"></param>
        /// <param name="ConditionParam"></param>
        /// <param name="CompanyID"></param>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IEnumerable<AwaitInvoiceApprovalModel> ViewAwaitInvoiceApproval(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string TotalInvoiceAmmount, string ConditionParam, int? CompanyID, Int64? VendorID)
        {
            try
            {
                RepositoryService<AwaitInvoiceApprovalModel> objAwaitInvoiceApprovalModel = new RepositoryService<AwaitInvoiceApprovalModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyName", AnchorCompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Invoicetotatalamt", TotalInvoiceAmmount, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PONumber", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceNo", "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyId", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@IsDiscountNull", null, System.Data.SqlDbType.SmallInt));//Here 1 and 3 are decision making parametres.It's definition is in sql

                var data = objAwaitInvoiceApprovalModel.ExecWithStoreProcedure("proc_GetViewAwaitInvoiceApproval  @Skip, @PageSize, @sortBy, @AnchorCompanyName, @Invoicetotatalamt,@PONumber,@InvoiceNo,@CompanyId,@VendorID,@IsDiscountNull", parameters.ToArray());
                IEnumerable<AwaitInvoiceApprovalModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

    }
}
