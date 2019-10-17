using System;
using System.ComponentModel.DataAnnotations;
namespace Finocart.CustomModel
{

    public class RecievableDue
    {


        [Key]
        public int AnchorID { get; set; }

        public string Anchorname { get; set; }

        public int NoOfInvoices { get; set; }
        public decimal TotalInvoiceAmount { get; set; }
        public decimal ApprovedAmt { get; set; }

        public decimal? AmountReceiabletoday { get; set; }

        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }




    }
}
