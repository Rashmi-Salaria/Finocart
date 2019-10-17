using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Invoice bucket relationship
    /// </summary>
    public class InvoiceBucket
    {
        /// <summary>
        ///  Invoice bucket relationship Id
        /// </summary>
        [Key]
        public Int64 ID { get; set; }
        /// <summary>
        ///  Bucket ID
        /// </summary>
        public Int64 BucketID { get; set; }
        /// <summary>
        ///  Invoice ID
        /// </summary>
        public Int64 InvoiceID { get; set; }
    }
}
