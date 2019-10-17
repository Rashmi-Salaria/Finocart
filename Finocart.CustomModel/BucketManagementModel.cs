using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class BucketManagementModel
    {
        [Key]
        public Int64 SequenceNumber { get; set; }
        
        public Int64 BucketID { get; set; }

        public string BucketName { get; set; }

        public int? TotalInvoiceCount { get; set; }

        public decimal? TotalInvoiceAmt { get; set; }

        public DateTime? BucketCreatedDate { get; set; }

        public DateTime? ValidUptoDate { get; set; }

        public string AnchorCompStatus { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }

        public string AnchorCompName { get; set; }

        public string InvoiceNo { get; set; }

        public decimal? InvoiceAmt { get; set; }

        public decimal? ApprovedAmt { get; set; }

        public decimal? Discount { get; set; }
    }
}
