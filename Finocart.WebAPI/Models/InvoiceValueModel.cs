using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finocart.WebAPI.Models
{
    public class InvoiceValueModel
    {  
            public string Value { get; set; }
            public string REF_DOC { get; set; }
            public List<InvoiceValueModel> lstInvoiceValue { get; set; }
        
    }
}