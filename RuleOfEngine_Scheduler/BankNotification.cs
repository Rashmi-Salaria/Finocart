//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RuleOfEngine_Scheduler
{
    using System;
    using System.Collections.Generic;
    
    public partial class BankNotification
    {
        public long BankNotificationID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<long> RoleID { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ReadOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
}
