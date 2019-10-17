using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.Model
{
    public class AnchorCompanyDropdownModel
    {
        [Key]
        public int AnchorCompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
