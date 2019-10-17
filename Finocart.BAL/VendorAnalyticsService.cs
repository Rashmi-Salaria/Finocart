using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class VendorAnalyticsService:IVendorAnalytics
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
        public VendorAnalyticsService(VMSContext context)
        {
            _vContext = context;
        }
        #endregion
        #region methods

        /// <summary>
        /// Getting lost opportunities
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IEnumerable<LostOpportunitiesModel> GetLostOpportunities(int? VendorId, string sortBy, int pageSize, Int64 skip, int companyID, DateTime fromDate, DateTime toDate)
        {
            try
            {
                RepositoryService<LostOpportunitiesModel> objLostOpportunitiesModel = new RepositoryService<LostOpportunitiesModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", companyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FromDate", fromDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", toDate, System.Data.SqlDbType.VarChar));
                var data = objLostOpportunitiesModel.ExecWithStoreProcedure("Proc_GetLostOppListing  @VendorID, @Skip, @PageSize, @sortBy, @CompanyID, @FromDate, @ToDate", parameters.ToArray());
                IEnumerable<LostOpportunitiesModel> lstLostOpportunities = data.ToList();
                return lstLostOpportunities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get anchor company list
        /// </summary>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        public IEnumerable<AnchorCompDropDownModel> getAnchorCompList(Int64? VendorId)
        {
            RepositoryService<AnchorCompDropDownModel> objAnchorCompDropDownModel = new RepositoryService<AnchorCompDropDownModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
            var data = objAnchorCompDropDownModel.ExecWithStoreProcedure("poc_getAnchorCompDropDown @VendorID", parameters.ToArray());
            IEnumerable<AnchorCompDropDownModel> AnchorCompDetails = data.ToList();
            return AnchorCompDetails;
        }

        /// <summary>
        /// Get funding request
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDates"></param>
        /// <returns></returns>
        public IEnumerable<AnalyticsFundingReqModel> GetFundingRequest(int? VendorId, string sortBy, int pageSize, Int64 skip, DateTime fromDate, DateTime toDates)
        {
            try
            {
                RepositoryService<AnalyticsFundingReqModel> objAnalyticsFundingReqModel = new RepositoryService<AnalyticsFundingReqModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FromDate", fromDate, System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", toDates, System.Data.SqlDbType.DateTime));
                var data = objAnalyticsFundingReqModel.ExecWithStoreProcedure("Proc_GetFundingReqListing  @VendorID, @Skip, @PageSize, @sortBy, @FromDate, @ToDate", parameters.ToArray());
                IEnumerable<AnalyticsFundingReqModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get funding request amount
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDates"></param>
        /// <returns></returns>
        public IEnumerable<AnalyticsFundingReqViewModel> GetFundingRequestAmount(int? VendorId, string sortBy, int pageSize, Int64 skip, int companyID, string fromDate, string toDates)
        {
            try
            {
                RepositoryService<AnalyticsFundingReqViewModel> objAnalyticsFundingReqViewModel = new RepositoryService<AnalyticsFundingReqViewModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", companyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FromDate", fromDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", toDates, System.Data.SqlDbType.VarChar));
                var data = objAnalyticsFundingReqViewModel.ExecWithStoreProcedure("Proc_GetFundingReqAmtListing  @VendorID, @Skip, @PageSize, @sortBy, @CompanyID, @FromDate, @ToDate", parameters.ToArray());
                IEnumerable<AnalyticsFundingReqViewModel> lstFundReqView = data.ToList();
                return lstFundReqView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get funding request percentage
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDates"></param>
        /// <returns></returns>
        public IEnumerable<AnalyticsFundingReqViewModel> GetFundingRequestPercent(int? VendorId, string sortBy, int pageSize, Int64 skip, int companyID, string fromDate, string toDates)
        {
            try
            {
                RepositoryService<AnalyticsFundingReqViewModel> objAnalyticsFundingReqViewModel = new RepositoryService<AnalyticsFundingReqViewModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", companyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@FromDate", fromDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ToDate", toDates, System.Data.SqlDbType.VarChar));
                var data = objAnalyticsFundingReqViewModel.ExecWithStoreProcedure("Proc_GetFundingReqPercentListing  @VendorID, @Skip, @PageSize, @sortBy, @CompanyID, @FromDate, @ToDate", parameters.ToArray());
                IEnumerable<AnalyticsFundingReqViewModel> lstFundReqView = data.ToList();
                return lstFundReqView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
