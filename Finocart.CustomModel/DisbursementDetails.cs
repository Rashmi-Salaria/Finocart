using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Finocart.CustomModel
{


    public class DisbursementDetails
    {
            /// <summary>
            /// Anchor Company ID
            /// </summary>
            [Key]
            public int id { get; set; }

            /// <summary>
            /// Anchor Company Name
            /// </summary>
            public string DrawRequestId { get; set; }

            /// <summary>
            /// Anchor Company Name
            /// </summary>
            public string AnchorName { get; set; }

            /// <summary>
            /// Anchor Company Name
            /// </summary>
            public decimal? FundsRequested { get; set; }

            /// <summary>
            /// Validity From date
            /// </summary>
            public DateTime? RequestDate { get; set; }

            /// <summary>
            /// Anchor Company Name
            /// </summary>
            public decimal? PaidAmount { get; set; }

            /// <summary>
            /// Validity From date
            /// </summary>
            public DateTime? PaymentDate { get; set; }

            /// <summary>
            /// Anchor Company Name
            /// </summary>
            public string PaymentStatus { get; set; }

            /// <summary>
            /// Anchor Company Name
            /// </summary>
            public string UTRDetails { get; set; }

            /// <summary>
            /// Validity From date
            /// </summary>
            public DateTime? CreatedDate { get; set; }

            /// <summary>
            /// Validity From date
            /// </summary>
            public DateTime? ModifiedDate { get; set; }
       
    }

}
