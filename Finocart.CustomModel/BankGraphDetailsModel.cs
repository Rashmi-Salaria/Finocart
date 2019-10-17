using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class BankGraphDetailsModel
    {
        [Key]
        public int id { get; set; }


        public string Anchor_Name { get; set; }

        public decimal Available_Limits { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
