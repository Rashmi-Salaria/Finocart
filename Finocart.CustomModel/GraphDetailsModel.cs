using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class GraphDetailsModel
    {
        [Key]
        public Int64 InvoiceID { get; set; }

        public decimal ApprovedAmt { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
