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
    public class PurchaseOrderService : IPurchaseOrder
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
        public PurchaseOrderService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion


        /// <summary>
        /// Get Module Status
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleMaster> getModuleStatus()
        {
            RepositoryService<ModuleMaster> objModuleMaster = new RepositoryService<ModuleMaster>(_vContext);
            
            return objModuleMaster.SelectAll();
        }

        #region Purchase Order Listing

        /// <summary>
        /// Get Purchase Order List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PurchaseOrderModel> getPurchaseOrderList(string sortBy, int pageSize, Int64 skip, string PurchaseOrderNo, string VendorName)
        {
            try
            {
                
                RepositoryService<PurchaseOrderModel> objPurchaseOrderModel = new RepositoryService<PurchaseOrderModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@PurchaseOrderNo", PurchaseOrderNo, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@VendorName", VendorName, System.Data.SqlDbType.VarChar));

                var data = objPurchaseOrderModel.ExecWithStoreProcedure("proc_GetPurchaseOrderDet  @Skip, @PageSize, @sortBy, @PurchaseOrderNo, @VendorName", parameters.ToArray());
                IEnumerable<PurchaseOrderModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        #region Purchase Order View

        /// <summary>
        /// Get Invoice List By Purchase Order Id
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InvoiceModel> getInvoiceListByPOId(string sortBy, int pageSize, Int64 skip, string InvoiceNo, string InvoiceStatus, string PONumber)
        {
            try
            {
                
                RepositoryService<InvoiceModel> objInvoiceModel = new RepositoryService<InvoiceModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompanyName", null, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceStatus", InvoiceStatus, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PONumber", PONumber, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceNo", InvoiceNo, System.Data.SqlDbType.VarChar));

                var data = objInvoiceModel.ExecWithStoreProcedure("proc_GetInvoiceDet  @Skip, @PageSize, @sortBy, @AnchorCompanyName, @InvoiceStatus, @PONumber, @InvoiceNo", parameters.ToArray());
                IEnumerable<InvoiceModel> lstInvoice = data.ToList();
                return lstInvoice;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Get record by po number
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        public IEnumerable<PurchaseOrderModel> GetRecordByPONumber(string PONumber)
        {
            
            RepositoryService<PurchaseOrderModel> objPurchaseOrderModel = new RepositoryService<PurchaseOrderModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SQLHelper.SqlInputParam("@PONumber", PONumber, System.Data.SqlDbType.VarChar));
            
            var data = objPurchaseOrderModel.ExecWithStoreProcedure("proc_GetPurchaseOrderDetByPONumber  @PONumber", parameters.ToArray());
            IEnumerable<PurchaseOrderModel> lstInvoice = data.ToList();
            return lstInvoice;
        }
    }
}
