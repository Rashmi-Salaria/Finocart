using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    public class HolidayList
    {

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// FileName For
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Reason For
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// CreatedBy For
        /// </summary>
        public int? CreatedBy { get; set; }


        /// <summary>
        /// CreatedDate For
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// UpdatedBy For
        /// </summary>
        public int? UpdatedBy { get; set; }


      
        /// <summary>
        /// UpdatedDate For
        /// </summary>
        public string UpdatedDate { get; set; }


        /// <summary>
        /// IsDelete For
        /// </summary>
        public int IsDelete { get; set; }

    
    }
}
