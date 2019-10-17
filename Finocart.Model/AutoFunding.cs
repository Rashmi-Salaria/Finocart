using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    public  class AutoFunding
    {

        /// <summary>
        /// Atuo funding id
        /// </summary>
        /// 
        [Key]
        public Int64 AutoFudngId { get; set; }
        /// <summary>
        /// Discount Rate
        /// </summary>
        public decimal DiscuntRate { get; set; }

        /// <summary>
        /// Discount valid Date 
        /// </summary>
        public DateTime DiscuntVlidDate { get; set; }
    }
}
