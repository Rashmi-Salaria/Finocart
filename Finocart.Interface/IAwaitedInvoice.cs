using Finocart.CustomModel;
using Finocart.Model;
using System;
using System.Collections.Generic;
namespace Finocart.Interface
{
    public interface IAwaitedInvoice
    {
        #region Get Awaited Invoice Listing
        /// <summary>
        /// Get Module Status
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModuleMaster> getModuleStatus();

        /// <summary>
        /// Get Anchor company listing by vendor Id
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        IEnumerable<AnchorCompListingModel> GetAnchorCompListingByVendorId(int? VendorId, string sortBy, int pageSize, Int64 skip, string CompanyID, string CompanyName, string TotalInvoiceAmt, string Page, int IsTotalCount);

        #endregion

        
    }
}
