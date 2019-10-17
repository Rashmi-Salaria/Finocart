using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.Model
{
    public class UserLockedResponse
    {
        /// <summary>
        /// Code
        /// </summary>
        [Key]
        public int Code { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}
