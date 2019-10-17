using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.Model
{
    public class HolidayDatesModel
    {
        [Key]
        public int RowId { get; set; }
        public DateTime HolidayDates { get; set; }
    }
}
