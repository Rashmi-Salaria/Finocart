﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    /// <summary>
    /// Document Model
    /// </summary>
    public class DocumentModel
    {
        /// <summary>
        /// ID For
        /// </summary>
        [Key]
        public Int64 DocID { get; set; }
        /// <summary>
        /// FileName For
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// DocumentType For
        /// </summary>
        public string DocumentType { get; set; }
    }
}
