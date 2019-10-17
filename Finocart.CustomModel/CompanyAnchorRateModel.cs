using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class CompanyAnchorRateModel
    {
        [Key]
        public int? IsExistPan { get; set; }

        /// <summary>
        /// Email Id Is Exist 
        /// </summary>
        public int? IsExistEmailId { get; set; }
    }
}
