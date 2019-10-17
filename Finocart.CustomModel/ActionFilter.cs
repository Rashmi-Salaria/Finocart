using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.CustomModel
{
    public class ActionFilter
    {
        [Key]
        public int filterID { get; set; }
        public string Roles { get; set; }
        public string RoleAccess { get; set; }
        public string controllerName { get; set; }
        public string ActionName { get; set; }


    }
}
