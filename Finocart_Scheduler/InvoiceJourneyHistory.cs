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
    using System.Collections.Generic;
    
    public partial class InvoiceJourneyHistory
    {
        public long InvoiceJourneyID { get; set; }
        public Nullable<long> InvoiceID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> InvoiceStatus { get; set; }
        public int InvoiceUpdatedBy { get; set; }
        public Nullable<System.DateTime> InvoiceUpdatedAt { get; set; }
        public string RejectionReason { get; set; }
        public Nullable<long> BucketID { get; set; }
    
        public virtual ModuleMaster ModuleMaster { get; set; }
    }
}
