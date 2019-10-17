using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Look up details
    /// </summary>
    public class LookupDetails
    {
        /// <summary>
        /// Unique lookup id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Lookup Key (Group)
        /// </summary>
        public string LookupKey { get; set; }

        /// <summary>
        /// Lookup value
        /// </summary>
        public string LookupValue { get; set; }

        /// <summary>
        /// Lookup for (Identification)
        /// </summary>
        public string LookupFor { get; set; }

    }
}
