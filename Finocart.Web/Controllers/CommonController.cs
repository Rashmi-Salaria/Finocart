using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Model;
using Finocart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finocart.Web.Controllers
{
    public class CommonController : Controller
    {
        #region Interface declaration

        /// <summary>
        /// Invoice Data Repository
        /// </summary>
        private readonly ICommon _empRepository;

        /// <summary>
        /// Lookup data Repository
        /// </summary>
        private readonly ILookupDetails _lookUpRepository;
        private readonly ICommon _CommonRepository;
        private readonly IBank _companyRepository;
        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        public CommonController(ICommon commonRepository, ILookupDetails lookupDetailsRepository, ICommon CommonRepository, IBank companyRepository)
        {
            _empRepository = commonRepository;
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
            _companyRepository = companyRepository;
        }

        #endregion

        #region  Methods

        #region Change Password
        ChangePasswordModel objChangePassword = new ChangePasswordModel();
       
        /// <summary>
        /// Call Change Password Page
        /// </summary>
        /// <returns></returns>
        public IActionResult ChangePassword(string RoleName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var data = Convert.FromBase64String(RoleName);
                objChangePassword.Role = Encoding.UTF8.GetString(data);
                objChangePassword.UserId = HttpContext.Session.GetInt32("UserID");

                if (TempData["WrongPassword"] == null || TempData["WrongPassword"] != null)
                {
                    if (HttpContext.Session.GetInt32("UserID") == null)
                    {

                        if (objChangePassword.Role == "MasterAdmin")
                        {
                            return RedirectToAction("AdminLogin", "Account");
                        }
                        else if (objChangePassword.Role == "InternalUser")
                        {
                            return RedirectToAction("UserLogin", "Account");
                        }
                        else
                        {
                            return RedirectToAction("SuperAdminLogin", "Account");
                        }
                    }
                }

                objChangePassword.Email = HttpContext.Session.GetString("Email");
            }

            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }

            return View("~/Views/Account/ChangePassword.cshtml", objChangePassword);
        }     

        
        /// <summary>
        /// Set Changed Password
        /// </summary>
        /// <param name="objChangePassword"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult setChangePassword(ChangePasswordModel objChangePassword)

        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                //string pw = SecurityHelperService.Decrypt("ueNl4Gl98pKy3m0G6CaqBsi6jSC58DX8");
                var Result = "";
                var Actions = "";
                objChangePassword.NewPassword = SecurityHelperService.Encrypt(objChangePassword.NewPassword);
                objChangePassword.OldPassword = SecurityHelperService.Encrypt(objChangePassword.OldPassword);
                var data = Encoding.UTF8.GetBytes(objChangePassword.Role);
                var Role = Convert.ToBase64String(data);
                if (HttpContext.Session.GetInt32("UserID") != null)
                {
                    if (objChangePassword.Role == "InternalUser")
                    {
                        Actions = "UserLogin";
                        //IEnumerable<User> objDatawithSP = _empRepository.CheckUserPassword();
                        //objDatawithSP = objDatawithSP.Where(x => x.Password == objChangePassword.OldPassword && x.Email == objChangePassword.Email);
                        User objDatawithSP = _empRepository.CheckUser(objChangePassword.Email, objChangePassword.OldPassword);
                        if (objDatawithSP != null)
                        {
                            Result = objDatawithSP.Password;
                        }
                    }
                    if (objChangePassword.Role == "MasterAdmin")
                    {
                        Actions = "AdminLogin";
                        //IEnumerable<Company> objDatawithSP = _empRepository.CheckAdminPassword();
                        ////objDatawithSP = objDatawithSP.Where(x => x.Password == objChangePassword.OldPassword && (x.Email == objChangePassword.Email || x.PANNumber == objChangePassword.Email));
                        //objDatawithSP = objDatawithSP.Where(x => x.Password == objChangePassword.OldPassword && x.Pan_number == objChangePassword.Email);
                        Company objDatawithSP = _empRepository.CheckAdmin(objChangePassword.Email, objChangePassword.OldPassword);
                        if (objDatawithSP != null)
                        {
                            Result = objDatawithSP.Password;
                        }

                    }
                    if (objChangePassword.Role == "SuperAdmin")
                    {
                        Actions = "SuperAdminLogin";
                        //IEnumerable<FinocartMaster> objDatawithSP = _empRepository.CheckSuperAdminPassword();
                        //objDatawithSP = objDatawithSP.Where(x => x.Password == objChangePassword.OldPassword && x.EmailId == objChangePassword.Email);
                        FinocartMaster objDatawithSP = _empRepository.CheckSuperAdmin(objChangePassword.Email,objChangePassword.OldPassword);
                        if (objDatawithSP!=null)
                        {
                            Result = objDatawithSP.Password;
                        }
                    }

                    if (Result != "")
                    {
                        var Data = _empRepository.UpdateNewPassword(objChangePassword);
                        return RedirectToAction(Actions, "Account");
                    }
                    else
                    {
                        TempData["WrongPassword"] = "Old password is not valid";
                        return RedirectToAction("ChangePassword", "Common", new { RoleName = Role });
                    }
                }
                else
                {

                    return RedirectToAction("ChangePassword", "Common", new { RoleName = Role });
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }


        }
        #endregion

        #region Check if email id exists
        /// <summary>
        /// Check if email id exists 
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        public ActionResult CheckIfEmailIdExists(string EmailID, string Module)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var result = _empRepository.CheckIfEmailIdExists(EmailID, Module);
                return Json(result);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
        }
        #endregion
        #endregion

        #region  Change Password

        ChangePasswordModel changePassword = new ChangePasswordModel();

        /// <summary>
        /// Call Change Password Page
        /// </summary>
        /// <returns></returns>
        public IActionResult ChangePasswordNew()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                changePassword.UserId = HttpContext.Session.GetInt32("UserID");
                changePassword.Role = HttpContext.Session.GetString("Role");
                changePassword.Email = HttpContext.Session.GetString("Email");
            }

            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }

            return View("~/Views/Common/ChangePasswordNew.cshtml", changePassword);
        }
        
/// <summary>
/// Set New Password
/// </summary>
/// <param name="changePassword"></param>
/// <returns></returns>
        [HttpPost]
        public IActionResult updateNewPassword(ChangePasswordModel changePassword)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var lstEmailIDofBank = _companyRepository.GetAnchorEmail(Convert.ToString(changePassword.UserId));
            changePassword.Email = lstEmailIDofBank.ElementAt(0).Contact_email;
            try
            {
                var Result = "";
                var Actions = "";
                changePassword.NewPassword = SecurityHelperService.Encrypt(changePassword.NewPassword);
                changePassword.OldPassword = SecurityHelperService.Encrypt(changePassword.OldPassword);
                var data = Encoding.UTF8.GetBytes(changePassword.Role);
                var Role = Convert.ToBase64String(data);
                if (HttpContext.Session.GetInt32("UserID") != null)
                {
                    if (changePassword.Role == "InternalUser")
                    {
                        Actions = "UserLogin";
                        User objDatawithSP = _empRepository.CheckUser(changePassword.Email, changePassword.OldPassword);
                        if (objDatawithSP != null)
                        {
                            Result = objDatawithSP.Password;
                        }
                    }
                    if (changePassword.Role == "MasterAdmin")
                    {
                        Actions = "AdminLogin";
                        Company objDatawithSP = _empRepository.CheckAdmin(changePassword.Email, changePassword.OldPassword);
                        if (objDatawithSP != null)
                        {
                            Result = objDatawithSP.Password;
                        }

                    }
                    if (changePassword.Role == "SuperAdmin")
                    {
                        Actions = "SuperAdminLogin";
                        FinocartMaster objDatawithSP = _empRepository.CheckSuperAdmin(changePassword.Email, changePassword.OldPassword);
                        if (objDatawithSP != null)
                        {
                            Result = objDatawithSP.Password;
                        }
                    }

                    if (Result != "")
                    {
                        var Data = _empRepository.UpdateNewPassword(changePassword);
                       
                    }

                    else
                    {
                        TempData["WrongPassword"] = "Old password is not valid";
                        return RedirectToAction("ChangePassword", "Common", new { RoleName = Role });
                    }
                    if (Result != "")
                    {
                        string emailToAddress = lstEmailIDofBank.ElementAt(0).Contact_email;
                        string AnchorName = lstEmailIDofBank.ElementAt(0).Contact_Name;
                        string BankName = HttpContext.Session.GetString("LoginName");
                        string Template = string.Empty;
                        int Id = 1;
                        IEnumerable<GetChangePasswordMailTemplate> lstAwaitedInvVendorsView = _companyRepository.GetChangePasswordMailTemplate(Template);
                        string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                        //string path = "";
                        string subject = "Change Password";
                        string body = path;
                        body = body.Replace("@@User@@", AnchorName);
                        body = body.Replace("@@BankName@@", BankName);
                        body = body.Replace("@@mentiondateandtime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                        _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);

                    }
                    return RedirectToAction(Actions, "Account");
                }
                else
                {

                    return RedirectToAction("ChangePassword", "Common", new { RoleName = Role });
                }

               


            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }


        }

        #endregion

        #region Error Log 
       private void ErrorLog(Exception eoe)
        {
            try
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                Int32? UserID = HttpContext.Session.GetInt32("UserID");
                string ErrorMessage = string.Empty;
                var st = new StackTrace(eoe, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, eoe.Message, ErrorLine, UserID);
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public ActionResult ErrorPage()
        {
            return View("~/Views/Account/ErrorPage.cshtml");
        }

    }
}