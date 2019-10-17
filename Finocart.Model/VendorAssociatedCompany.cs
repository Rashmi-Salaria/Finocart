using System;

namespace Finocart.Model
{
    /// <summary>
    /// Vendor-Company relation
    /// </summary>
    public class VendorAssociatedCompany
    {
        /// <summary>
        /// Vendor-Company relation Id
        /// </summary>
        public Int64 ID { get; set; }
        /// <summary>
        /// Vendor ID
        /// </summary>
        public Int64 VendorID { get; set; }
        /// <summary>
        /// Company ID
        /// </summary>
        public int CompanyID { get; set; }
    }
}
