using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class VendorsDropDownModel
    {
        [Key]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }
    }
}
