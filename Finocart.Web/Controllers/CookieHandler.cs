using Finocart.Services;
using Microsoft.AspNetCore.Http;
using System;


namespace Finocart.Web.Controllers
{
    public sealed class CookieHandler : BaseController
    {
        /// <summary>
        /// Set cookie method
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <param name="UserRole"></param>
        public void SetCookie(string UserID, string UserName, string UserRole)
        {
            try
            {
                if (Convert.ToString(Request.Cookies.ContainsKey("UserID")) == null)
                {
                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(10);
                    Response.Cookies.Append("UserID", UserID, option);
                    Response.Cookies.Append("UserName", SecurityHelperService.Encrypt(UserName), option);
                    Response.Cookies.Append("Role", SecurityHelperService.Encrypt(UserRole), option);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get cookie method
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <param name="UserRole"></param>
        public void GetCookie(out string UserID,out string UserName,out string UserRole)
        {

            UserID = string.Empty;
            UserName = string.Empty;
            UserRole = string.Empty;
            try
            {
                if (Request.Cookies["UserID"] == null)
                {
                    UserID = SecurityHelperService.Decrypt(Request.Cookies["UserID"].ToString());
                    UserName = SecurityHelperService.Decrypt(Request.Cookies["UserName"].ToString());
                    UserRole = SecurityHelperService.Decrypt(Request.Cookies["UserRole"].ToString());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
