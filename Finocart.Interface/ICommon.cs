using Finocart.CustomModel;
using Finocart.Model;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Finocart.Interface
{
    public interface ICommon
    {       

        /// <summary>
        /// Check User Old Password
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> CheckUserPassword();

        /// <summary>
        /// Check Admin Old Password
        /// </summary>
        /// <returns></returns>
        IEnumerable<Company> CheckAdminPassword();

        /// <summary>
        /// Check Super Admin Old Password
        /// </summary>
        /// <returns></returns>
        IEnumerable<FinocartMaster> CheckSuperAdminPassword();

        /// <summary>
        /// Update new password
        /// </summary>
        /// <param name="objChangePassword"></param>
        /// <returns></returns>
        int UpdateNewPassword(ChangePasswordModel objChangePassword);

        /// <summary>
        /// Upload File To Server
        /// </summary>
        /// <param name="formFile"></param>
        bool UploadFileToServer(IFormFile formFile,string documentFolderPath, string exportFileName);
        /// <summary>
        /// Delete File
        /// </summary>
        /// <param name="DocumientPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        bool DeleteFile(string DocumientPath, string FileName);

        /// <summary>
        /// Validate Vendor User
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns></returns>
        bool ValidateVendorUser(Int32? UserID,string FileName);
        /// <summary>
        /// Validate AnchorComp User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        bool ValidateAnchorCompUser(Int32? UserID,string fileName);

        /// <summary>
        /// Generate random password
        /// </summary>
        /// <returns></returns>
        string GeneratePassword();

        /// <summary>
        /// Get User list
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUserList();

        /// <summary>
        /// Update Random Password
        /// </summary>
        /// <param name="objUserModel"></param>
        /// <returns></returns>
        int UpdatePassword(string Password, string EmailID, string Role);

        /// <summary>
        /// Check if email id exists
        /// </summary>
        /// <param name="EmailID"></param>
        /// <returns></returns>
        int? CheckIfEmailIdExists(string EmailID,string Type);

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="lookupDetails"></param>
        /// <param name="emailToAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        void SendEmail(IEnumerable<LookupDetails> lookupDetails, string emailToAddress, string subject, string body, bool isBodyHtml);

        /// <summary>
        /// checking super admin by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        FinocartMaster CheckSuperAdminByEmail(string email, bool isDelete);

        /// <summary>
        /// Check admin by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        Company CheckAdminByEmail(string email, bool isDelete);

        /// <summary>
        /// Check admin by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Company CheckAdminByEmailExists(string email);

        /// <summary>
        /// checking user by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        User CheckUserByEmail(string email, bool isDelete);

        /// <summary>
        /// checking super admin by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        FinocartMaster CheckSuperAdmin(string email, string password);

        /// <summary>
        /// Check admin by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        Company CheckAdmin(string email, string password);

        /// <summary>
        /// checking user by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        User CheckUser(string email, string password);

        int LogManagement(string ControllerName, string ActionName, string ErrorMessage, int ErrorLine, Int32? UserID);

        int AuditTrailLog(string ModuleName, string PageName, Int32? UserID, int CompanyID);

        string CustomUserFilter(string controllername, string actionname, string RoleAccess,string Role);
    }
}
