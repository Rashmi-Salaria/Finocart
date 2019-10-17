using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    /// <summary>
    /// Vendor Documents' description
    /// </summary>
    public class VendorDocument
    {
        /// <summary>
        /// Vendor Document ID 
        /// </summary>
        [Key]
        public Int64 ID { get; set; }

        /// <summary>
        /// Vendor Document FileName 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Vendor ID For Vendor Document
        /// </summary>
        public Int64 VendorID { get; set; }

        /// <summary>
        /// Vendor Document Type
        /// </summary>
        public Int64 DocumentTypeId { get; set; }

        /// <summary>
        /// Vendor Document Uploaded By
        /// </summary>
        public int UploadedBy { get; set; }

        /// <summary>
        /// Vendor Document Uploaded Date
        /// </summary>
        public DateTime UploadedOn { get; set; }
        /// <summary>
        /// LocalFolderFileName 
        /// </summary>
        public string LocalFolderFileName { get; set; }
    }
}
