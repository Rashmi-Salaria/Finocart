using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class BucketManagementService: IBucketManagement
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
        public BucketManagementService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion
        /// <summary>
        /// Getting bucket list
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="VendorId"></param>
        /// <returns></returns>
        public IEnumerable<BucketManagementModel> getBucketList(string sortBy, int pageSize, Int64 skip, Int32? VendorId)
        {
            try
            {
                
                RepositoryService<BucketManagementModel> objBucketManagementModel = new RepositoryService<BucketManagementModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.BigInt));
               
                var data = objBucketManagementModel.ExecWithStoreProcedure("proc_GetBucketDet  @Skip, @PageSize, @sortBy, @VendorID", parameters.ToArray());
                IEnumerable<BucketManagementModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="VendorId"></param>
        /// <param name="BucketID"></param>
        /// <returns></returns>
        public IEnumerable<BucketManagementModel> getBucketListView(string sortBy, int pageSize, Int64 skip, Int32? VendorId, Int64? BucketID)
        {
            try
            {
             
                RepositoryService<BucketManagementModel> objBucketManagementModel = new RepositoryService<BucketManagementModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@VendorID", VendorId, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@BucketID", BucketID, System.Data.SqlDbType.BigInt));
                
                var data = objBucketManagementModel.ExecWithStoreProcedure("proc_GetBucketDetView  @Skip, @PageSize, @sortBy, @VendorID, @BucketID", parameters.ToArray());
                IEnumerable<BucketManagementModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
    }
}
