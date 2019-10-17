using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Maintains roles' access
    /// </summary>
    public class RolesAccessMaster
    {
        /// <summary>
        /// Role Access Id
        /// </summary>
        [Key]
        public int? RoleAccessID { get; set; }

        /// <summary>
        /// Role permission
        /// </summary>
        public string Permission { get; set; }
    }
}
