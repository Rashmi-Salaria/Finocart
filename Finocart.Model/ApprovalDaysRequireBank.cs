using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.Model
{
   public class NoOfDaysForApproval
    {
        /// <summary>
        /// Unique  id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Days
        /// </summary>
        public long NoOfDays { get; set; }

    }
}
