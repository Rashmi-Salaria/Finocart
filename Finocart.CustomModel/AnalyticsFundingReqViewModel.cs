using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class AnalyticsFundingReqViewModel
    {
        [Key]
        public Int64 SequenceNumber { get; set; }

        public int AnchorCompID { get; set; }

        public decimal Discount { get; set; }

        public decimal ApprovedAmt { get; set; }

        public decimal RejectedAmt { get; set; }

        public decimal PendingAmt { get; set; }

        public decimal Total { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
    }
}
