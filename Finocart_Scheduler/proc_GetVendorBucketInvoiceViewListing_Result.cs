//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Finocart_Scheduler
{
    using System;
    
    public partial class proc_GetVendorBucketInvoiceViewListing_Result
    {
        public Nullable<long> Row { get; set; }
        public string POID { get; set; }
        public string InvoiceID { get; set; }
        public string VendorName { get; set; }
        public Nullable<System.DateTime> InvoiceCreationDate { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<decimal> ApprovedAmount { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<System.DateTime> DiscountOfferedDate { get; set; }
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public Nullable<System.DateTime> DiscountValidUpto { get; set; }
        public Nullable<int> TotalRecord { get; set; }
        public Nullable<int> FilteredRecord { get; set; }
    }
}
