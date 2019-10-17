using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Maintain search history
    /// </summary>
    public class SearchHistory
    {
        /// <summary>
        /// Search History Id
        /// </summary>
        [Key]
        public Int64 ID { get; set; }
        /// <summary>
        /// Company ID searched in 
        /// </summary>
        public int CompanyID { get; set; }
        /// <summary>
        /// Search Page Name
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Search Value
        /// </summary>
        public string SearchKeyValue { get; set; }
        /// <summary>
        /// Maximum limit to search
        /// </summary>
        public int? MaxSearchLimit { get; set; }
    }
}
