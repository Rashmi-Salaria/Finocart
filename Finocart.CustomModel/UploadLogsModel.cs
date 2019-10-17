using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.CustomModel
{
    public class UploadLogsModel
    {
        [Key]
        public Int64 ID { get; set; } 

        public string CompanyName { get; set; }

        public string ExcelFileName { get; set; }

        public string Name { get; set; }

        public string Logs { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int32? TotalRecord { get; set; }

        public int? FilteredRecord { get; set; }
    }
}
