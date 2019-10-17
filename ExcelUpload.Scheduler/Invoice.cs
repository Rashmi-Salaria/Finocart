//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExcelUpload.Scheduler
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        public long InvoiceID { get; set; }
        public string PONumber { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<int> VendorID { get; set; }
        public int AnchorCompID { get; set; }
        public Nullable<decimal> InvoiceAmt { get; set; }
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<long> Days { get; set; }
        public Nullable<int> InvoiceStatus { get; set; }
        public string RejectionReason { get; set; }
        public Nullable<decimal> ApprovedAmt { get; set; }
        public string UploadInvoice { get; set; }
        public string UploadDocument { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> InvoiceApprovePayDays { get; set; }
        public Nullable<System.DateTime> InvoiceApprovalDate { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public Nullable<decimal> Earning { get; set; }
        public string FinoLimUnLim { get; set; }
        public string UTRDetails { get; set; }
        public string Limits { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ModuleMaster ModuleMaster { get; set; }
    }
}
