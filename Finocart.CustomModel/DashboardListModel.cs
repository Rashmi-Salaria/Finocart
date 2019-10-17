using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class DashboardListModel
    {

        [Key]
        public int? CompanyId { get; set; }
        public Int32 TotalInvoice { get; set; }
        public string Company_name { get; set; }
        public decimal TotalInvoiceAmount { get; set; }
        public int? FilteredRecord { get; set; }
        public int? TotalRecord { get; set; }

    }
}
