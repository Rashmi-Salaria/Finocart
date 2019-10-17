using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class LostOpportunitiesModel
    {
        [Key]
        public int CompanyID { get; set; }

        public int NoOfInvoice { get; set; }

        public decimal TotalAppAmt { get; set; }

        public int NoOfDiscInvoices { get; set; }

        public decimal TotalDiscAppAmt { get; set; }

        public decimal ApprovedInv { get; set; }

        public decimal DiscOffPercent { get; set; }

        public decimal SuccAppAmtPercent { get; set; }

        public decimal SuccDiscAmtPercent { get; set; }

        public DateTime CreatedDate { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
    }
}
