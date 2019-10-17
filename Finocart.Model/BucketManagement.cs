using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Bucket details
    /// </summary>
    public class BucketManagement
    {
        /// <summary>
        /// Bucket ID
        /// </summary>
        [Key]
        public Int64 BucketID { get; set; }
        /// <summary>
        /// Bucket Name
        /// </summary>
        public string BucketName { get; set; }
        /// <summary>
        /// Bucket Status
        /// </summary>
        public int BucketStatus { get; set; }
        /// <summary>
        /// Discount applied on bucket
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// Discount valid upto date
        /// </summary>
        public DateTime ValidUptoDate { get; set; }
        /// <summary>
        /// Bucket created date
        /// </summary>
        public DateTime BucketCreatedDate { get; set; }
        /// <summary>
        /// Bucket created by
        /// </summary>
        public int BucketCreatedBy { get; set; }
        /// <summary>
        /// Bucket modified by
        /// </summary>
        public int BucketModifiedBy { get; set; }
        /// <summary>
        /// Bucket modified date
        /// </summary>
        public DateTime BucketModifiedAt { get; set; }
    }
}
