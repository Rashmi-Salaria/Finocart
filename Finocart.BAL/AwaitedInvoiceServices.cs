using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;

namespace Finocart.Services
{

    public class AwaitedInvoiceServices : IAwaitedInvoice
    {
       
        #region Declartion 

        /// <summary>
        /// Context
        /// </summary>
        public readonly VMSContext _vContext;

        #endregion

        #region Constructor
        public AwaitedInvoiceServices(VMSContext context)
        {
            _vContext = context;
        }
        public IEnumerable<ModuleMaster> getModuleStatus()
        {
            RepositoryService<ModuleMaster> objModuleMaster = new RepositoryService<ModuleMaster>(_vContext);
            return objModuleMaster.SelectAll();
        }

        public IEnumerable<AnchorCompListingModel> GetAnchorCompListingByVendorId(int? VendorId, string sortBy, int pageSize, Int64 skip, string CompanyID, string CompanyName, string TotalInvoiceAmt, string Page, int IsTotalCount)
        {
            try
            {
                RepositoryService<AnchorCompListingModel> objAnchorCompListingModel = new RepositoryService<AnchorCompListingModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
               // parameters.Add(SQLHelper.SqlInputParam("@Page", Page, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Company_ID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Company_name", CompanyName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@TotalAppInvAmt", TotalInvoiceAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@IsTotalCount", IsTotalCount, System.Data.SqlDbType.Int));

                var data = objAnchorCompListingModel.ExecWithStoreProcedure("Proc_GetAnchorCompListing  @Page, @VendorID, @Company_ID, @Company_name, @TotalAppInvAmt, @Skip, @PageSize, @sortBy, @IsTotalCount", parameters.ToArray());
                IEnumerable<AnchorCompListingModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
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
          
            try
            {
                RepositoryService<MaxAvailableAmount> objMaxAvailableAmount = new RepositoryService<MaxAvailableAmount>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VedorID", VendorID, System.Data.SqlDbType.Int));


                var data = objMaxAvailableAmount.ExecWithStoreProcedure("proc_GetMaxAvailableVendorAmount @VedorID", parameters.ToArray());
                IEnumerable<MaxAvailableAmount> maxAmount = data.ToList();
                
                return maxAmount.FirstOrDefault().MaxVendorAmountAvailable.ToString();

            }
            catch (Exception ex)
            {
            
                throw ex;
            }
        }

        /// <summary>
        /// Getting awaited invoice list
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceModel> getAwaitedInvoiceList(int? VendorId, string sortBy, int pageSize, long skip, string CompanyName, string TotalInvoiceAmt)
        {
            throw new NotImplementedException();
        }
        
        #endregion

    }
}
