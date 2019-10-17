using System;
using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Model;
using Finocart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Finocart.Web.Controllers
{
    /// <summary>
    /// Public pages - Login methods
    /// </summary>
    public class AccountController : Controller
    {

        #region Interface declaration

        /// <summary>
        /// Vendor Data Repository
        /// </summary>
        private readonly IVendor _venderRepository;

        /// <summary>
        /// User Data Repository
        /// </summary>
        private readonly IUser _userRepository;

        /// <summary>
        /// Finocart Master Repository
        /// </summary>
        private readonly IFinoCartMaster _SuperAdminRepository;

        /// <summary>
        /// Admin Data Repository
        /// </summary>
        private readonly IAdmin _adminRepository;

        /// <summary>
        /// Common Data Repository
        /// </summary>
        private readonly ICommon _CommonRepository;

        private readonly ILookupDetails _lookUpRepository;
        /// <summary>
        /// Configuration Repository
        /// </summary>
        private readonly IConfiguration _configuration;

        private readonly IBank _companyRepository;

        private readonly ICompany _company;

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        public AccountController(IBank companyRepository, IVendor venderRepository, IUser userRepository, IFinoCartMaster superadminRepository, IAdmin adminRepository, ICommon commonRepository, ILookupDetails lookUpRepository, IConfiguration configurationRepository, ICompany company)
        {
            _companyRepository = companyRepository;
            _venderRepository = venderRepository;
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _SuperAdminRepository = superadminRepository;
            _CommonRepository = commonRepository;
            _lookUpRepository = lookUpRepository;
            _configuration = configurationRepository;
            _company = company;

        }

        #endregion

        #region Methods

        #region Vendor Login
        /// <summary>
        /// Vendor login view
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin()

        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");

            string ErrorMessage = string.Empty;
            try
            {
                if (Request.Cookies["Email"] != null)
                {
                    ViewBag.Email = Request.Cookies["Email"];

                }


                if (Request.Cookies["Role"] != null && Request.Cookies["Role"].ToString() == "Vendor")
                {

                    return RedirectToAction("VendorDashboard", "Vendor");
                }
                else
                {
                    ViewBag.InvalidCredential = TempData["InvalidCredential"];
                    return View();
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
        #region Validate vendor login credentials
        /// <summary>
        /// Validate vendor login credentials
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
       [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ValidateUserLogOn(string Email, string Password, string IsRemember)
        {

            if (!ReCaptchaPassed(
            Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
            _configuration.GetSection("GoogleReCaptcha:SecretKey").Value
            ))
            {
                TempData["InvalidCredential"] = "Captcha verification failed.";
                return RedirectToAction("UserLogin", "Account");
            }

            string ControllerActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? LoginUserID = HttpContext.Session.GetInt32("UserID");

            string ErrorMessage = string.Empty;
            try
            {

                string pw = SecurityHelperService.Decrypt("8qLsuVEBHnIzavnjCKcC5g==");
                Password = SecurityHelperService.Encrypt(Password);
                UserLoginModel userModel = _venderRepository.validateUser(Email, Password);
                string Controller = "";
                string ActionName = "";
                HttpContext.Session.SetString("Role", "InternalUser");
                if (userModel == null)
                {
                    string strLockRes = _adminRepository.LockedUser(Email);
                    TempData["InvalidCredential"] = strLockRes;
                    //TempData["InvalidCredential"] = "Enter valid credential";
                    return RedirectToAction("UserLogin", "Account");
                }
                else
                {
                    var UserID = Convert.ToInt32(userModel.UserID);
                    var UserName = userModel.Name;
                    var CompanyId = userModel.CompanyID;
                    var RoleAccess = userModel.RoleAccess;
                    var Companyname = userModel.Companyname;

                    Response.Cookies.Delete("RoleAccess");

                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Append("RoleAccess", RoleAccess, option);

                    HttpContext.Session.SetInt32("UserID", UserID);
                    HttpContext.Session.SetString("LoginName", UserName);
                    HttpContext.Session.SetInt32("CompanyID", CompanyId);
                    HttpContext.Session.SetString("RoleAccess", RoleAccess);
                    HttpContext.Session.SetString("Companyname", Companyname);
                    HttpContext.Session.SetString("JWToken", GenerateJWTToken(UserName));

                    if (IsRemember == "1")
                    {
                        SetCookie(UserID.ToString(), UserName, "InternalUser");
                        Response.Cookies.Append("Email", Email, option);
                    }
                    var Res = _CommonRepository.AuditTrailLog("LoginPage", "LoginPage", UserID, 0);
                    if (userModel.IsTemporaryPassword == false)
                    {
                        if (RoleAccess == "Vendor Company")
                        {
                            Controller = "Vendor";
                            ActionName = "VendorDashboardMain";
                        }
                        if (RoleAccess == "Anchor Company")
                        {
                            var lstchecklimit = _companyRepository.CheckSetLimit(CompanyId);
                            string PercentageRate = Convert.ToString(lstchecklimit.ElementAt(0).PercentageRate);
                            string PaymentDays = Convert.ToString(lstchecklimit.ElementAt(0).PaymentDays);
                            //if (PercentageRate != "" && PaymentDays != "")
                            //{
                            Controller = "AnchorCompany";
                            ActionName = "AnchorDashboard";
                            //}
                            //else
                            //{
                            //    //Controller = "AnchorCompany";
                            //    //ActionName = "AnchorDashboard";
                            //    Controller = "AnchorCompany";
                            //    ActionName = "SetLimit";

                            //}
                        }

                        if (RoleAccess == "Both")
                        {
                            Controller = "AnchorCompany";
                            ActionName = "AnchorDashboard";
                        }
                        if (RoleAccess == "Bank")
                        {
                            Controller = "BankCompany";
                            ActionName = "BankDashboard";
                        }
                        return RedirectToAction(ActionName, Controller);
                    }
                    else
                    {
                        var data = Encoding.UTF8.GetBytes("InternalUser");
                        var Role = Convert.ToBase64String(data);
                        HttpContext.Session.SetString("Email", userModel.Email);
                        Controller = "Common";
                        ActionName = "ChangePassword";
                        SetCookie(UserID.ToString(), UserName, Role);
                        return RedirectToAction(ActionName, Controller, new { RoleName = Role });
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ControllerActionName, ex.Message, ErrorLine, LoginUserID);
                return RedirectToAction("ErrorPage", "Common");
            }
        }

        #endregion
        /// <summary>
        /// Vendor Log off
        /// </summary>
        /// <returns></returns>
        public IActionResult VendorLogOff()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                Response.Cookies.Delete("RoleAccess");
                HttpContext.Session.Clear();
                return RedirectToAction("UserLogin", "Account");
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

        #region User Login
        /// <summary>
        /// Method used for anchor company login
        /// </summary>
        /// <returns></returns>
        public ActionResult AnchorCompanyLogin()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                if (Request.Cookies["Role"] != null && Request.Cookies["Role"].ToString() == "InternalUser")
                {
                    return RedirectToAction("InvoiceList", "Invoice");
                }
                else
                {
                    return View();
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

        /// <summary>
        /// Validating anchor company details
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        /// 
     
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult ValidateAnchorCompanyLogOn(string Email, string Password)
        {
            if (!ReCaptchaPassed(
            Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
            _configuration.GetSection("GoogleReCaptcha:SecretKey").Value
            ))
            {
                TempData["InvalidCredential"] = "Captcha verification failed.";
                return RedirectToAction("AnchorCompanyLogin", "Account");
            }
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserLoginID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                Password = SecurityHelperService.Encrypt(Password);
                User userdata = _userRepository.FindUserName(Email, Password);
                HttpContext.Session.SetString("Role", "InternalUser");


                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(2);
                Response.Cookies.Append("RoleAccess", "Anchor Company", option);

                if (userdata == null)
                {
                    ViewBag.InvalidCredential = "Enter valid credential";
                    return View("AnchorCompanyLogin");
                }
                else
                {
                    var UserID = userdata.UserID;
                    var UserName = userdata.Name;
                    HttpContext.Session.SetInt32("UserID", UserID);
                    HttpContext.Session.SetString("UserName", UserName);
                    HttpContext.Session.SetString("JWToken", GenerateJWTToken(UserName));
                    SetCookie(UserID.ToString(), UserName, "InternalUser");
                    return RedirectToAction("InvoiceList", "Invoice");
                }

            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserLoginID);
                return RedirectToAction("ErrorPage", "Common");
            }
        }

        /// <summary>
        /// Logging off anchor company
        /// </summary>
        /// <returns></returns>
        public IActionResult AnchorCompanyLogOff()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                HttpContext.Session.Remove("Role");
                HttpContext.Session.Remove("UserID");
                HttpContext.Session.Remove("UserName");
                Response.Cookies.Delete("RoleAccess");
                ClearCookie();
                return RedirectToAction("UserLogin", "Account");
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

        #region Super Admin Login
        /// <summary>
        /// View super admin login screen
        /// </summary>
        /// <returns></returns>
        public ActionResult SuperAdminLogin()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                if (Request.Cookies["EmailID"] != null)
                {
                    ViewBag.EmailID = Request.Cookies["EmailID"];

                }
                if (Request.Cookies["Role"] != null)
                {

                    AssignedCookieValue();

                    return RedirectToAction("SuperAdminDashBoard", "AnchorCompany");
                }
                else
                {
                    ViewBag.InvalidCredential = TempData["InvalidCredential"];
                    return View("FinocartMasterLogin");
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
        #region Validate super admin login credentials
        /// <summary>
        /// Validate super admin login credentials
        /// </summary>
        /// <param name="EmailID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateSuperAdminLogOn(string EmailID, string Password, string IsRemember)
        {
            //EmailID = "rashmi.salaria@brainvire.com";

            if (!ReCaptchaPassed(
            Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
            _configuration.GetSection("GoogleReCaptcha:SecretKey").Value
            ))
            {
                TempData["InvalidCredential"] = "Captcha verification failed.";
                return RedirectToAction("SuperAdminLogin", "Account");
            }

            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserLoginID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Password = SecurityHelperService.Encrypt(Password);
                FinocartMaster objDatawithSP = _SuperAdminRepository.ValidateLogin(EmailID, Password);
                HttpContext.Session.SetString("Role", "SuperAdmin");
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(2);
                Response.Cookies.Append("RoleAccess", "SuperAdmin", option);

                if (objDatawithSP == null)
                {
                    TempData["InvalidCredential"] = "Enter valid credential";
                    return RedirectToAction("SuperAdminLogin", "Account");
                }
                else
                {
                    var UserID = objDatawithSP.ID;
                    var UserName = objDatawithSP.Name;
                    HttpContext.Session.SetInt32("UserID", UserID);
                    HttpContext.Session.SetString("LoginName", UserName);
                    HttpContext.Session.SetString("JWToken", GenerateJWTToken(UserName));
                    if (IsRemember == "1")
                    {
                        SetCookie(UserID.ToString(), UserName, "SuperAdmin");
                        Response.Cookies.Append("EmailID", EmailID, option);
                    }

                    var Res = _CommonRepository.AuditTrailLog("Login", "Super Admin Login", UserID, 0);

                    if (objDatawithSP.IsTemporaryPassword == false)
                    {

                        TempData["Role"] = "SuperAdmin";

                        return RedirectToAction("SuperAdminDashBoard", "AnchorCompany");
                    }
                    else
                    {
                        var data = Encoding.UTF8.GetBytes("SuperAdmin");
                        var Role = Convert.ToBase64String(data);
                        HttpContext.Session.SetString("Email", objDatawithSP.EmailId);
                        return RedirectToAction("ChangePassword", "Common", new { RoleName = Role });
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserLoginID);
                return RedirectToAction("ErrorPage", "Common");
            }
        }
        #endregion

        #region Super Admin LogOff
        /// <summary>
        /// Super Admin LogOff
        /// </summary>
        /// <returns></returns>
        /// 

        public IActionResult SuperAdminLogOff()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Response.Cookies.Delete("RoleAccess");
                HttpContext.Session.Clear();
                return RedirectToAction("SuperAdminLogin", "Account");
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

        #region Admin Login

        /// <summary>
        /// Admin Login
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminLogin()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                if (Request.Cookies["PANNumber"] != null)
                {
                    ViewBag.PANNumber = Request.Cookies["PANNumber"];

                }
                if (Request.Cookies["UserID"] != null)
                {
                    AssignedCookieValue();
                    if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == "MainAdmin")
                    {
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddDays(2);
                        Response.Cookies.Append("RoleAccess", "AdminLogin", option);
                        return RedirectToAction("UserList", "User");
                    }
                    else
                    {
                        return RedirectToAction("UserList", "User");
                    }

                }
                else
                {
                    ViewBag.InvalidCredential = TempData["InvalidCredential"];
                    return View();
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

        #region
        /// <summary>
        /// Validate Admin Log On
        /// </summary>
        /// <param name="PANNumber"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
       
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateAdminLogOn(string PANNumber, string Password, string IsRemember)
        {

            if (!ReCaptchaPassed(
            Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
            _configuration.GetSection("GoogleReCaptcha:SecretKey").Value
            ))
            {
                TempData["InvalidCredential"] = "Captcha verification failed.";
                return RedirectToAction("AdminLogin", "Account");
            }

            string pw = SecurityHelperService.Decrypt("9b1L+4cRvb5EYXkEw8dl3bgkBFnKqjSQ");

            string InputCredential = PANNumber;
            string Controller = "";
            string ActionName = "";

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(InputCredential);

            Password = SecurityHelperService.Encrypt(Password);
            AdminLoginModel objDatawithSP = _adminRepository.FindName(PANNumber, Password);

            if (objDatawithSP == null)
            {
                string strLockRes = _adminRepository.LockedAdminUser(PANNumber);
                TempData["InvalidCredential"] = strLockRes;
                return RedirectToAction("AdminLogin", "Account");
            }
            else
            {

                var AdminID = objDatawithSP.CompanyID;
                var AdminName = objDatawithSP.ContactPersonName;
                var InternalRole = objDatawithSP.Role;
                var companyname = objDatawithSP.Name;

                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(2);
                Response.Cookies.Append("RoleAccess", "AdminLogin", option);


                TempData["InternalRole"] = InternalRole;
                HttpContext.Session.SetInt32("UserID", AdminID);
                HttpContext.Session.SetString("LoginName", AdminName);
                HttpContext.Session.SetString("Role", "MasterAdmin");
                HttpContext.Session.SetString("CompanyType", InternalRole);
                HttpContext.Session.SetString("Companyname", companyname);
                HttpContext.Session.SetString("JWToken", GenerateJWTToken(AdminName));
                //HttpContext.Session.SetString("RoleAccess", RoleAccess);
                if (objDatawithSP.IsTemporaryPassword == false)
                {

                    TempData["Role"] = "MasterAdmin";

                    if (IsRemember == "1")
                    {
                        SetCookie(AdminID.ToString(), AdminName, "MasterAdmin");
                        Response.Cookies.Append("PANNumber", PANNumber, option);
                    }
                    return RedirectToAction("UserList", "User");

                }
                else
                {
                    var data = Encoding.UTF8.GetBytes("MasterAdmin");
                    var Role = Convert.ToBase64String(data);
                    Controller = "Common";
                    ActionName = "ChangePassword";

                    HttpContext.Session.SetString("Email", objDatawithSP.PANNumber);

                    if (IsRemember == "1")
                    {
                        SetCookie(AdminID.ToString(), AdminName, "MasterAdmin");
                        Response.Cookies.Append("PANNumber", PANNumber, option);
                    }
                    return RedirectToAction(ActionName, Controller, new { RoleName = Role });
                }

            }

        }
        #endregion

        #region Admin log out
        /// <summary>
        /// Admin LogOff
        /// </summary>
        /// <returns></returns>
        public IActionResult AdminLogOff()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Remove(UserID.ToString());
                return RedirectToAction("AdminLogin", "Account");
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

        #region

        public ActionResult unauthorized()
        {
            return View();
        }
        #endregion

        #region Forgot password screen
        /// <summary>
        /// Forgot password screen
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public IActionResult ForgetPassword(string Role)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                ForgetPasswordModel objForgetPasswordModel = new ForgetPasswordModel();
                var data = Convert.FromBase64String(Role);
                objForgetPasswordModel.RoleName = Encoding.UTF8.GetString(data);
                return View("~/Views/Account/ForgotPassword.cshtml", objForgetPasswordModel);
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

        #region Send mail after password change in forgot password screen
        /// <summary>
        /// Send mail after password change in forgot password screen
        /// </summary>
        /// <param name="objForgetPassword"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendForgetPasswordMail(ForgetPasswordModel objForgetPassword)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var RoleName = "";
            FinocartMaster objDatawithSP1 = null;
            Company objDatawithSP2 = null;
            User objDatawithSP3 = null;
            IEnumerable<User> objDatawithSP = _CommonRepository.CheckUserPassword();
            IEnumerable<User> objDatawithSP4 = _CommonRepository.CheckUserPassword();
            try
            {
                var Name = "";
                string ID = "";

                //// string randomPassword = _CommonRepository.GeneratePassword();
                // string EncryptToken = SecurityHelperService.Encrypt(Token);
                string EmailID = objForgetPassword.EmailID.Trim();
                var data = Encoding.UTF8.GetBytes(objForgetPassword.RoleName);
                RoleName = Convert.ToBase64String(data);
                if (objForgetPassword.RoleName == "MasterAdmin")
                {
                    //IEnumerable<Company> objDatawithSP = _CommonRepository.CheckAdminPassword();
                    //objDatawithSP = objDatawithSP.Where(x => x.Contact_email == EmailID && x.IsDelete == false);
                    objDatawithSP2 = _CommonRepository.CheckAdminByEmailExists(EmailID);
                    if (objDatawithSP2 != null &&(!objDatawithSP2.IsDelete || objDatawithSP2.LoginAttempt == 3))
                    {
                        Name = objDatawithSP2.Company_name;
                        ID = Convert.ToString(objDatawithSP2.CompanyID);
                    }
                }
                if (objForgetPassword.RoleName == "InternalUser")
                {
                    //IEnumerable<User> objDatawithSP = _CommonRepository.CheckUserPassword();
                    //objDatawithSP = objDatawithSP.Where(x => x.Email == EmailID && x.IsDelete == true);
                    objDatawithSP3 = _CommonRepository.CheckUserByEmail(EmailID, true);
                    if (objDatawithSP3 != null)
                    {
                        Name = objDatawithSP3.Name;
                        ID = Convert.ToString(objDatawithSP3.UserID);
                    }
                }
                if (objForgetPassword.RoleName == "Vendor")
                {

                    objDatawithSP = objDatawithSP.Where(x => x.Email == EmailID);
                    if (objDatawithSP.Any())
                    {
                        Name = objDatawithSP.ElementAt(0).Name;
                        ID = Convert.ToString(objDatawithSP.ElementAt(0).UserID);
                    }
                }

                if (objForgetPassword.RoleName == "Anchor Company")
                {

                    objDatawithSP4 = objDatawithSP.Where(x => x.Email == EmailID && x.IsDelete == false);
                    if (objDatawithSP4.Any())
                    {
                        Name = objDatawithSP4.ElementAt(0).Name;
                        ID = Convert.ToString(objDatawithSP4.ElementAt(0).UserID);

                    }
                }
                if (objForgetPassword.RoleName == "SuperAdmin")
                {

                    objDatawithSP1 = _CommonRepository.CheckSuperAdminByEmail(EmailID, false);
                    if (objDatawithSP1 != null)
                    {
                        Name = objDatawithSP1.Name;
                        ID = Convert.ToString(objDatawithSP1.ID);
                    }
                }

                if (Name != "")
                {
                    string Token = ID+"~"+objForgetPassword.RoleName+"~"+DateTime.Now;
                    
                    string EncryptToken = SecurityHelperService.Encrypt(Token);
                    // var Result = _CommonRepository.UpdatePassword(Password, EmailID, objForgetPassword.RoleName);
                    // if (Result > 0)
                    //{

                    IEnumerable<GetForgetPasswordMailTemplate> lstAwaitedInvVendorsView = _lookUpRepository.getForgetPasswordMailTemplate();
                    string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                    string EMAIL_TOKEN_PAYMENT_LINK = "##$$LOGIN_LINK$$##";
                   //string paymentLink ="http://localhost:4670/Account/ResetPassword?token=" + EncryptToken; ///change url
                    string paymentLink ="http://dotnet.brainvire.com/Finocart/Account/ResetPassword?token=" + EncryptToken; ///change url
                    string emailToAddress =EmailID;
                    string subject = "Reset Password";
                    string body = path;
                    body = body.Replace("@@User@@", Name);
                    body = body.Replace("@@ProjectName@@", "Finocart");
                    body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);
                    body = body.Replace("http://dotnet.brainvire.com/Finocart/Account/AdminLogin", paymentLink);
                    // body = body.Replace("@@Password@@", randomPassword);
                    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                    _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);
                    TempData["MailSuccess"] = "Mail sent successfully";
                    // }
                }
                else
                {
                    TempData["WrongMail"] = "Email ID is not valid";
                }
            }
            catch (Exception ex)
            {
                TempData["FailureMessage"] = "We are sorry, something went wrong. Please try again later";
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }

            return RedirectToAction("ForgetPassword", "Account", new { Role = RoleName });
        }
        #endregion

        #region Cookie

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        private void SetCookie(string UserID, string UserName, string UserRole)
        {
            try
            {
                if (Convert.ToString(Request.Cookies.ContainsKey("UserID")) == "False")
                {
                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("UserID", SecurityHelperService.Encrypt(UserID), option);
                    Response.Cookies.Append("LoginName", SecurityHelperService.Encrypt(UserName), option);
                    Response.Cookies.Append("Role", SecurityHelperService.Encrypt(UserRole), option);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Method for assigning cookie value
        /// </summary>
        private void AssignedCookieValue()
        {
            try
            {
                HttpContext.Session.SetInt32("UserID", Convert.ToInt32(SecurityHelperService.Decrypt(Request.Cookies["UserID"])));
                HttpContext.Session.SetString("LoginName", Convert.ToString(SecurityHelperService.Decrypt(Request.Cookies["LoginName"])));
                HttpContext.Session.SetString("Role", Convert.ToString(SecurityHelperService.Decrypt(Request.Cookies["Role"])));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Method to clear cookie
        /// </summary>
        private void ClearCookie()
        {
            try
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
        #region
        //[HttpPost]
        public ActionResult ResetPassword(string token)
        {
            if(token.Contains(" "))
            {
                token = token.Replace(" ", "+");
            }
            string EncryptToken = "";
            try
            {

                EncryptToken = Convert.ToString(SecurityHelperService.Decrypt(token));
                //EncryptToken = token;
            }
            catch (Exception ex) { }
            string[] strlist = EncryptToken.Split(new[] { "~", "##" }, StringSplitOptions.RemoveEmptyEntries);

            DateTime tokendate = Convert.ToDateTime(strlist[2]);
            double totaldays = (DateTime.Now- tokendate).TotalDays;
            if (totaldays > 2)
            {
                return RedirectToAction("TokenExpired", "Account");
            }
            else
            {
                IEnumerable<GetEmailId> cm = _lookUpRepository.GetEmailId(int.Parse(strlist[0]),strlist[1]);
                ViewBag.Username = cm.ElementAt(0).EmailID;
                ViewBag.role = strlist[1];
                return View();
            }

           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitPassword(string Username, string Role, string NewPassword)
        {
            var Result = _CommonRepository.UpdatePassword(SecurityHelperService.Encrypt(NewPassword), Username, Role);

            if(Role== "SuperAdmin")
            { 
            return RedirectToAction("SuperAdminLogin", "Account");
            }
            else if (Role == "MasterAdmin")
            {
                return RedirectToAction("AdminLogin", "Account");
            }
            else
            {
                return RedirectToAction("UserLogin", "Account"); //(Role == "Vendor" || Role == "Bank" || Role == "Anchor")
            }
        }


        #endregion
        #endregion




        #region GenerateToken
        public string GenerateJWTToken(string Username)
        {
            var claims = new Claim[]
                    {
                    new Claim(JwtRegisteredClaimNames.GivenName, Username),
                    };
            var JWToken = new JwtSecurityToken(
                issuer: _configuration["URL:ApplicationUrl"],
                audience: _configuration["URL:ApplicationUrl"],
                claims: claims,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Auth0:ClientSecret"])),
                                    SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(JWToken);
        }
        #endregion

        #region Google Captcha Verification Method
        public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }
        #endregion

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult TokenExpired()
        {
            return View();
        }
    }
}