using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class CompanyCredentialsModel
    {
        /// <summary>
        /// PAN Number Is Exist 
        /// </summary>
        [Key]
        public int? IsExistPan { get; set; }

        public string RoleAccess { get; set; }
        /// <summary>
        /// Email Id Is Exist 
        /// </summary>
        public int? IsExistEmailId { get; set; }
    }
}
