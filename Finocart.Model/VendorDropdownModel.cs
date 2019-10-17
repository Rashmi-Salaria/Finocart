using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.Model
{
    public class VendorDropdownModel
    {
        [Key]
        public int VendorID { get; set; }
        public string VendorName { get; set; }
    }

    
}
