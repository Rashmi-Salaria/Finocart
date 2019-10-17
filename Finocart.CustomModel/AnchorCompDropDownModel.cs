using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class AnchorCompDropDownModel
    {
        [Key]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }
    }

    public class AnchorVendorBankCount
    {
        [Key]
        public int CompanyID { get; set; }
    }
}
