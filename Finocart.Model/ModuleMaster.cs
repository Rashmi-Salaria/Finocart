using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Module Master 
    /// </summary>
    public class ModuleMaster
    {
        /// <summary>
        /// Module Status Id
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Module Status Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Module Name
        /// </summary>
        public string ModuleName { get; set; }
    }
}
