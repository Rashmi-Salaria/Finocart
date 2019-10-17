using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class AuditTrailHistory
    {

        [Key]
        public Int64 AuditId { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public int CreatedBy { get; set; }
    
        public string CreatedOn { get; set; }

        /// <summary>
        /// Funds Rquest Total Record 
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Funds Filtered Record
        /// </summary>
        public Int32? FilteredRecord { get; set; }
    }

    public class TablesName
    {
        [Key]
        public Int64 TableId { get; set; }

        public string TableName { get; set; }

        public Int64? ColumnId { get; set; }

        public string ColumnName { get; set; }

    }

    //public class ColumnsName
    //{
    //    [Key]
    //    public Int64 ColumnId { get; set; }

    //    public string ColumnName { get; set; }

    //}
}
