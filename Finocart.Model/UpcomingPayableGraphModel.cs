using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.Model
{
    public class UpcomingPayableGraphModel
    {
        [Key]
        public Int64 ID { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
