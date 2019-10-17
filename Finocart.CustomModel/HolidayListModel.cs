using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
   public class HolidayListModel
    {

        [Key]
        public int? ID { get; set; }

        /// <summary>
        /// Date For
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Reason For
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// CreatedBy For
        /// </summary>
        public string CreatedBy { get; set; }


        /// <summary>
        /// CreatedDate For
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// UpdatedBy For
        /// </summary>
        public string UpdatedBy { get; set; }


        /// <summary>
        /// UpdatedDate For
        /// </summary>
        public string UpdatedDate { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
        /// <summary>
        /// IsDelete For
        /// </summary>
        //public Boolean IsDelete { get; set; }
    }
}
