using Finocart.CustomModel;
using Finocart.Model;
using System;
using System.Collections.Generic;

namespace Finocart.Interface
{
    public interface IPurchaseOrder
    {
        /// <summary>
        /// Get module status
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModuleMaster> getModuleStatus();

        /// <summary>
        /// Get Purchase order list
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="PurchaseOrderNo"></param>
        /// <param name="VendorName"></param>
        /// <returns></returns>
        IEnumerable<PurchaseOrderModel> getPurchaseOrderList(string sortBy, int pageSize, Int64 skip, string PurchaseOrderNo, string VendorName);

        /// <summary>
        /// Get Invoice list by Purchase order Id
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="InvoiceNo"></param>
        /// <param name="InvoiceStatus"></param>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        IEnumerable<InvoiceModel> getInvoiceListByPOId(string sortBy, int pageSize, Int64 skip, string InvoiceNo, string InvoiceStatus, string PONumber);

        /// <summary>
        /// Get Record by Purchase order number
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        IEnumerable<PurchaseOrderModel> GetRecordByPONumber(string PONumber);
    }
}
