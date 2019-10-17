using Finocart.CustomModel;
using System;
using System.Collections.Generic;

namespace Finocart.Interface
{
    public interface IVendorAnalytics
    {
        IEnumerable<LostOpportunitiesModel> GetLostOpportunities(int? VendorId, string sortBy, int pageSize, Int64 skip, int companyID, DateTime fromDate, DateTime toDate);

        IEnumerable<AnchorCompDropDownModel> getAnchorCompList(Int64? VendorId);

        IEnumerable<AnalyticsFundingReqModel> GetFundingRequest(int? VendorId, string sortBy, int pageSize, Int64 skip, DateTime fromDate, DateTime toDate);

        IEnumerable<AnalyticsFundingReqViewModel> GetFundingRequestAmount(int? VendorId, string sortBy, int pageSize, Int64 skip, int companyID, string fromDate, string toDate);

        IEnumerable<AnalyticsFundingReqViewModel> GetFundingRequestPercent(int? VendorId, string sortBy, int pageSize, Int64 skip, int companyID, string fromDate, string toDate);
    }
}
