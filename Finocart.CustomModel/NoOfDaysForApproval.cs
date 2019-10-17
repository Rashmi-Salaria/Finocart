using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.CustomModel
{
    class NoOfDaysForApproval
    {

        /// <summary>
        /// Unique  id
        /// </summary>
        [Key]
        public Int64 Id { get; set; }

        /// <summary>
        /// Days
        /// </summary>
        public Int64 NoOfDays { get; set; }
    }
}
