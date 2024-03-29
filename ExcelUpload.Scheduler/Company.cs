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
    
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public int CompanyID { get; set; }
        public string Company_name { get; set; }
        public string Login_Id { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Nullable<int> InterestedAs { get; set; }
        public Nullable<int> CompanyType { get; set; }
        public string Pan_number { get; set; }
        public string Contact_Title { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_Designation { get; set; }
        public string Contact_email { get; set; }
        public string Contact_mobile { get; set; }
        public string Contact_Comments { get; set; }
        public Nullable<decimal> PercentageRate { get; set; }
        public Nullable<decimal> InvoiceLimitAmt { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string Contact_CIN { get; set; }
        public Nullable<bool> IsTemporaryPassword { get; set; }
        public Nullable<bool> IsCritical { get; set; }
        public Nullable<int> InvAmtLimitStatus { get; set; }
        public Nullable<long> NoOfDays { get; set; }
        public string MSME { get; set; }
        public string UANNumber { get; set; }
        public Nullable<decimal> InternalFundLimit { get; set; }
        public Nullable<decimal> BankLimit { get; set; }
        public Nullable<decimal> AnchorRate { get; set; }
        public Nullable<bool> IsLimitUnlimited { get; set; }
        public Nullable<long> FactoryOrReverseFactory { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> PaymentDays { get; set; }
        public Nullable<int> LoginAttempt { get; set; }
    
        public virtual LookupDetail LookupDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
