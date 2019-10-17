using Finocart.CustomModel;
using System;
using System.Collections.Generic;

namespace Finocart.Interface
{
    public interface IBucketManagement
    {
        IEnumerable<BucketManagementModel> getBucketList(string sortBy, int pageSize, Int64 skip, Int32? VendorId);

        IEnumerable<BucketManagementModel> getBucketListView(string sortBy, int pageSize, Int64 skip, Int32? VendorId, Int64? BucketID);
    }
}
