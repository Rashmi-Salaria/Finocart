using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Finocart.CustomModel
{
    /// <summary>
    ///  Set Bank Limit Model
    /// </summary
    public class SetBankAmountLimit
    {
        /// <summary>
        ///  Set Limit Amount ID
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  Anchor_id
        /// </summary>
        public Int32 Anchor_id { get; set; }

        /// <summary>
        ///  Anchor_Name
        /// </summary>
        public string Anchor_Name { get; set; }


        /// <summary>
        ///  Anchor Name
        /// </summary>
        public decimal? Available_Limits { get; set; }


        /// <summary>
        ///  Utilize Limit
        /// </summary>
        public decimal? Utilized_Limits { get; set; }

        /// <summary>
        ///   Intrest rate
        /// </summary>
        public decimal? Interest_rate { get; set; }


        /// <summary>
        ///   Validity Form to
        /// </summary>
        public DateTime? Validity_upto { get; set; }

        /// <summary>
        ///   Validity upto
        /// </summary>
        public DateTime? ValidityFromto { get; set; }



        /// <summary>
        ///  nO OF Days  
        /// </summary>
        public Int32? NumberOfDays { get; set; }

        /// <summary>
        ///   Additional Document  
        /// </summary>
        public string Additional_document { get; set; }

        /// <summary>
        ///  Commensts    
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///  Modified User Name    
        /// </summary>
        public string ModifiedUserName { get; set; }

        /// <summary>
        ///  Anchor Email  
        /// </summary>
        public string AnchorEmail { get; set; }

        ///// <summary>
        /////  Document description  
        ///// </summary>
        //public string Document_desc { get; set; }

        /// <summary>
        ///   Draw_funds
        /// </summary>
        public decimal? Draw_funds { get; set; }
        public string ODBD { get; set; }


        public string RequestStatus { get; set; }

        ///<summary>
        ///Remaining Available Limits
        /// add by abc
        /// </summary>
        public decimal? Remaining_avail_bal { get; set; }

        [NotMapped]
        public string PageName { get; set; }


        public int? KYCStatus { get; set; }

        ///
        /// Bank name
        /// add by abc
        ///
        //public string BankName { get; set; }

    }

    public class FundsRequestHistory
    {
        /// <summary>
        ///  Anchor_id
        /// </summary>
        [Key]
        public int Anchor_id { get; set; }

        /// <summary>
        ///  Anchor_Name
        /// </summary>
        public string Anchor_Name { get; set; }

        /// <summary>
        ///  Request Amount
        /// </summary>
        public decimal RequestAmount { get; set; }

        /// <summary>
        ///   Validity upto
        /// </summary>
        public DateTime Validity_upto { get; set; }

        /// <summary>
        /// Funds Rquest Total Record 
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Funds Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }
    }

    public class FundsRequestHistoryView
    {
        /// <summary>
        ///  Anchor_id
        /// </summary>
        [Key]
        public int Anchor_id { get; set; }

        /// <summary>
        ///  Anchor_Name
        /// </summary>
        public string Anchor_Name { get; set; }


        /// <summary>
        ///  Request Amount
        /// </summary>
        public decimal Available_Limits { get; set; }


        /// <summary>
        ///  Request Amount
        /// </summary>
        public string RateAndMonth { get; set; }

        /// <summary>
        ///   Validity upto
        /// </summary>
        public DateTime Validity_upto { get; set; }

        /// <summary>
        /// Funds Bank Name 
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Request Amount 
        /// </summary>
        public string RequestStatus { get; set; }

        /// <summary>
        /// Funds Rquest Total Record 
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Funds Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }
    }

    public class LogManagement
        {
        string ControllerName { get; set; }
        string ActionName { get; set; }
        string ErrorMessage { get; set;}
        string ErrorLine { get; set; }
    }

    public class SetLimitHistory
    {

        [Key]
        public int Id { get; set; }

         public string Date { get; set; }

         public string InitialAmount { get; set; }

         public int InitialRate { get; set; }

         public string UpdatedAmount { get; set; } 

         public int UpdatedRate { get; set; }

         public string PersonUpdated { get; set; }

        /// <summary>
        /// Funds Rquest Total Record 
        /// </summary>
        public Int32? TotalRecord { get; set; }

        /// <summary>
        /// Funds Filtered Record
        /// </summary>
        public int? FilteredRecord { get; set; }
    }

}
