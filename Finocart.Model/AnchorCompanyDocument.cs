using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.Model
{
    public class AnchorCompanyDocument
    {
        /// <summary>
        /// ID For
        /// </summary>
        [Key]
        public long ID { get; set; }
 
        /// <summary>
        /// FileName For
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// AnchorCompanyId For
        /// </summary>
        public long AnchorCompanyId { get; set; }
        
        /// <summary>
        /// DocumentTypeId For
        /// </summary>
        public long DocumentTypeId { get; set; }
        
        /// <summary>
        /// UploadedBy For
        /// </summary>
        public int UploadedBy { get; set; }

        /// <summary>
        /// UploadedOn For
        /// </summary>
        public DateTime UploadedOn { get; set; }
        /// <summary>
        /// LocalFolderFileName For
        /// </summary>
        public string LocalFolderFileName { get; set; }
    }
}
