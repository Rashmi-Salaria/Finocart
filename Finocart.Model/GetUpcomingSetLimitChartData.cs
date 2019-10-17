using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    public class GetUpcomingSetLimitChartData
    {

        //public Int32 ID { get; set; }
        [Key]
        public decimal Available_Limits { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
