using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class AnalyticsFundingReqModel
    {
        [Key]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public int DiscOfferInvoice { get; set; }

        public decimal DiscOfferInvoiceAmount { get; set; }

        public decimal DiscOfferAppInvoiceAmount { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
    }
}
