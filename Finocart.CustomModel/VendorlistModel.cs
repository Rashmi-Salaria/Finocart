using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorlistModel
    {
        [Key]
        public Int32 VendorId { get; set; }
        public string AnchorName { get; set; }
        public Int32 CompanyID { get; set; }
        public string Company_name { get; set; }
        public string Contact_mobile { get; set; }
        public string Contact_email { get; set; }
        public decimal? InvoiceLimitAmt { get; set; }
        public Int32? InvAmtLimitStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Int32? FilteredRecord { get; set; }
        public Int32? TotalRecord { get; set; }
        public string InvAmtLimitStatusValues { get; set; }
        //--Darshan Gajera-----//
        //public string Company_type { get; set; }
    }

    public class AnchorListModel
    {
        [Key]
        public Int32 CompanyId { get; set; }
        public Int32 VendorId { get; set; }
        public string VendorName { get; set; }
        public string Company_name { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_mobile { get; set; }
        public string Contact_email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Int32? FilteredRecord { get; set; }
        public Int32? TotalRecord { get; set; }
    }

}
