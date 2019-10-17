using Finocart.CustomModel;
using Finocart.Model;
using System;
using System.Collections.Generic;

namespace Finocart.Interface
{
    public interface IInvoice
    {
        #region Get Invoice Listing
        /// <summary>
        /// Get Module Status
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModuleMaster> getModuleStatus();
        IEnumerable<AwaitInvoiceApprovalModel> ViewAwaitInvoiceApproval(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string TotalInvoiceAmmount, string ConditionParameters, int? CompanyID, Int64? VendorId);
        IEnumerable<InvoiceModel> ViewAvailableforfunding(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string TotalInvoiceAmmount, string ConditionParameters, int? CompanyID, Int32? VendorId);
        IEnumerable<InvoiceModel> getInvoiceList(string sortBy, int pageSize, Int64 skip, string AnchorCompanyName, string InvoiceStatus,string ConditionParameters, int? CompanyID, Int64? VendorId);
        #endregion

        #region Get last search history
        IEnumerable<SearchHistory> GetSearchHistories();

        #endregion


        #region Get Max Vendor Amount Available
        string GetMaxAvailableAmount(int? VendorID);
        #endregion 
    }
}
