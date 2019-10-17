using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel
{
    public class AdminLoginModel
    {
        /// <summary>
        /// Admin Id
        /// </summary>
        [Key]
        public int CompanyID { get; set; }

        /// <summary>
        /// Admin Name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// ContactPerson Name 
        /// </summary>

        public string ContactPersonName { get; set; }




        ///// <summary>
        ///// Admin Email Id
        ///// </summary>
        //public string EmailId { get; set; }

        /// <summary>
        /// Admin PAN Number
        /// </summary>
        public string PANNumber { get; set; }

        /// <summary>
        /// Admin Password
        /// </summary>
        ///[RegularExpression(pattern: " ((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{8,20})", ErrorMessage = "Password is not valid")]
        public string Password { get; set; }

        ///// <summary>
        ///// PortalAccessType For
        ///// </summary>
        //public string PortalAccessType { get; set; }

        /// <summary>
        /// Admin IsTemporaryPassword
        /// </summary>
        public bool IsTemporaryPassword { get; set; }

        /// <summary>
        /// Role For
        /// </summary>
        public string Role { get; set; }

       
    }
}
