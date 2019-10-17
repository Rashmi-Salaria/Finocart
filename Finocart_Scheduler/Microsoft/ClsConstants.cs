using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finocart_Scheduler.Microsoft
{
    public class ClsConstants
    {
        public enum Status
        {
            Pending = 4,
            Approved,
            Rejected,
            Accepted
        }

        public enum AccessType
        {
            Vendor = 9,
            Anchor,
            Both
        }

        public const string VendorBillApprovalMsg = "Hello, Your bill #billId has been approved of #amount.";
    }
}
