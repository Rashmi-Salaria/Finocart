using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public  class ReceivabledueIndividual
    {
        /// <summary>
        /// Invoice Id for Primary Key
        /// </summary>
        /// 
        [Key]
        public Int64 InvoiceID { get; set; }

        /// <summary>
        /// 
        /// </summary>
    
        public string PONumber { get; set; }
        public string InvoiceNO { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal InvoiceAmt { get; set; }

        public decimal Discount { get; set; }
        public decimal ApprovedAmt { get; set; }
        public decimal? NetReceivableAmount { get; set; }


       
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Anchor Company Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }

    }
}
