using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finocart_Scheduler.Model
{
    public class VendorDetail
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int InstretedAs { get; set; }
        public int ComanyType { get; set; }
        public string PanNumber { get; set; }
        public string ContactTitle { get; set; }
        public string ContactName { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobile { get; set; }
        public string ContactComments { get; set; }
        public decimal PercentageRate { get; set; }
        public decimal InvoiceLimit { get; set; }
        public string VendorID { get; set; }
        
    }
    public class Global
    {
        public const string Role="";
    }
}
