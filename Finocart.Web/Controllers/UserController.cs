using System;
using System.Collections.Generic;
using System.Linq;
using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Model;
using Microsoft.AspNetCore.Mvc;
using Finocart.Services;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Finocart.Web.Views.Common;

namespace Finocart.Web.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    /// 


    
    public class UserController : BaseController
    {
        #region Interface declaration

        /// <summary>
        /// User data Repository
        /// </summary>
        private readonly IUser _Userepository;
        private readonly ILookupDetails _lookUpRepository;
        private readonly ICommon _CommonRepository;

        #endregion


        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="UserRepository"></param>
        public UserController(IUser UserRepository, ILookupDetails lookupDetailsRepository, ICommon CommonRepository)
        {
            _Userepository = UserRepository;
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
        }


        #endregion

        #region Action Results
        /// <summary>
        /// Add user view
        /// </summary>
        /// <returns></returns>
        /// 

        public IActionResult AddUser()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string companyType = HttpContext.Session.GetString("CompanyType");
            string ErrorMessage = string.Empty;

            try
            {
                
            
                UserModel objUserModel = new UserModel();
                IEnumerable<LookupDetails> lstUserStatus = _lookUpRepository.getLookupDetailByKey("UserStatus");
                IEnumerable<LookupDetails> lstPortalAccessType = _lookUpRepository.getLookupDetailByKey("AccessView");
                IEnumerable<LookupDetails> lstRoleType = _lookUpRepository.getLookupDetailByKey("RoleType");
                string strLookupKey = string.Empty;
                if (companyType.Trim().ToUpper() == "ANCHOR COMPANY")
                {
                    strLookupKey = "AnchorCompanyView";
                    lstPortalAccessType = lstPortalAccessType.Where(x => x.LookupKey == strLookupKey).ToList();
                }
                else if (companyType.Trim().ToUpper() == "VENDOR COMPANY")
                {
                    strLookupKey = "VendorView";
                    lstPortalAccessType = lstPortalAccessType.Where(x => x.LookupKey == strLookupKey).ToList();
                }
                else if (companyType.Trim().ToUpper() == "BOTH")
                {
                    strLookupKey = "BankView";
                    lstPortalAccessType = lstPortalAccessType.Where(x => x.LookupKey != strLookupKey).ToList();
                }
                else if (companyType.Trim().ToUpper() == "BANK")
                {
                    strLookupKey = "BankView";
                    lstPortalAccessType = lstPortalAccessType.Where(x => x.LookupKey == strLookupKey).ToList();
                }
                Tuple<UserModel, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>> objUserModelData =
                    new Tuple<UserModel, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>>(objUserModel, lstUserStatus, lstPortalAccessType, lstRoleType);

                return View("~/Views/User/AddUserPage.cshtml", objUserModelData);
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
        /// User list view
        /// </summary>
        /// <returns></returns>
        /// 
        [CustomFilter]
        [JWTAuthorize("Account", "AdminLogin")]
        public IActionResult UserList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                ViewBag.AddResult = TempData["AddResult"];
                ViewBag.UpdateResult = TempData["UpdateResult"];
                ViewBag.DeleteResult = TempData["DeleteResult"];
                IEnumerable<RolesAccessMaster> lstRoleAccess = _Userepository.GetRoleAccessList();
                List<string> RoleAccess = new List<string>()
                { "Enable","Disable"};
                ViewBag.roleace = RoleAccess;
                ViewBag.Role = TempData["Role"];
                return View("~/Views/User/UserPageListing.cshtml", lstRoleAccess);
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
        #region Get user listing
        /// <summary>
        /// Get users' listing
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUserList()
      {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var UserName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var UserRole = Request.Form["columns[6][search][value]"].FirstOrDefault() == "0" ? "" : Request.Form["columns[6][search][value]"].FirstOrDefault();

                IEnumerable<UserModel> lstUser = _Userepository.GetUserPageListing(sortBy, pageSize, skip, UserName, UserRole, UserId);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstUser != null && lstUser.Count() != 0)
                {
                    recordsFiltered = lstUser.ElementAt(0).FilteredRecord;
                    recordsTotal = lstUser.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstUser });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }

        }

        #endregion

        #region Export User Data To CSV
        /// <summary>
        /// Export Invoice Data List To CSV
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportUsersData(string UserName, string RoleName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                var sortBy = "UserID asc";
                int pageSize = 100000;
                int skip = 0;
                RoleName = RoleName == "0" ? "" : RoleName;
                IEnumerable<UserModel> lstUser = _Userepository.GetUserPageListing(sortBy, pageSize, skip, UserName, RoleName, UserId);
                string date = lstUser.ElementAt(0).CreatedDate.ToString();
                string csv = ExportDatastr(lstUser);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "UserList.csv");
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
        /// Export data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportDatastr(IEnumerable<UserModel> objData)
        {
            return CommonService.ListToCSV(objData, "UserID,UpdatedDate,TotalRecord,FilteredRecord");
        }
        #endregion




        #region Insert/Update user record
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="objUserPage"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserModel objUserPage)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                string randomPassword = _CommonRepository.GeneratePassword();
                if (objUserPage.UserID == 0)
                {
                    objUserPage.Password = SecurityHelperService.Encrypt(randomPassword);
                }
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                Int32? AccessViewId = objUserPage.AccessViewID;
                string CompanyName = HttpContext.Session.GetString("Companyname");

                objUserPage.CreatedBy = UserId;
                objUserPage.UpdatedBy = UserId;
                
                var Result = _Userepository.InsertUpdateUserRecord(objUserPage);

                if (objUserPage.UserID == 0)
                {
                    if (Result > 0)
                    {
                        //string Template = string.Empty;
                        IEnumerable<GetUserMailTemplate> lstAwaitedInvVendorsView = _lookUpRepository.getUserMailTemplate(AccessViewId);
                        string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                        string EMAIL_TOKEN_PAYMENT_LINK = "##$$PAYMENT_LINK$$##";
                        string paymentLink = "http://dotnet.brainvire.com/Finocart/Account/UserLogin";///change url
                      
                        string emailToAddress = objUserPage.Email;
                        string subject = "User registration";
                        string body = path;
                        body = body.Replace("@@Panno@@", objUserPage.Email);
                        body = body.Replace("@@UserName@@", objUserPage.Name);
                        body = body.Replace("@@CompanyName@@", CompanyName);
                        body = body.Replace("@@ProjectName@@", "Finocart");
                        body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);
                        body = body.Replace("@@Password@@", randomPassword);
                        IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                        _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);

                        User objDatawithSP = _CommonRepository.CheckUserByEmail(objUserPage.Email, false);
                        if (objDatawithSP != null)
                        {
                            string DescriptionMessage = "Congratulations your registered on the Finocart Portal. Now you can access the portal and create your users ";

                            var Result1 = _Userepository.AddUserNotificationMessage(objDatawithSP.UserID, DescriptionMessage, null, UserId);
                        }


                    }
                    TempData["AddResult"] = Result;

                }
                else
                {
                    TempData["UpdateResult"] = Result;
                }

            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
                TempData["FailureMessage"] = "We are sorry, something went wrong. Please try again later";
            }

            return RedirectToAction("UserList", "User");
        }

    

        #endregion

        #region Get details of user
        /// <summary>
        /// Getting details to edit user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditUserPage(string id)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            UserModel objUserModel = new UserModel();
            try
            {
                //IEnumerable<RolesAccessMaster> lstRoleAccess = _Userepository.GetRoleAccessList();
                IEnumerable<LookupDetails> lstUserStatus = _lookUpRepository.getLookupDetailByKey("UserStatus");
                //var data = Convert.FromBase64String(id);
                var data = IsBase64Encoded(id);
                if (data == true)
                {
                    var result = Convert.FromBase64String(id);
                    int ids = Convert.ToInt16(Encoding.UTF8.GetString(result));
                    objUserModel = _Userepository.EditPage(ids);
                    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("AccessView");
                    IEnumerable<LookupDetails> lstRoleType = _lookUpRepository.getLookupDetailByKey("RoleType");
                    Tuple<UserModel, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>> objUserModelData =
                    new Tuple<UserModel, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>>(objUserModel, lstUserStatus, lookupDetails, lstRoleType);
                    return View("~/Views/User/AddUserPage.cshtml", objUserModelData);
                }
                else
                {
                    return View("~/Views/User/PageNotFound.cshtml");
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

        public bool IsBase64Encoded(String str)
        {
            try
            {
                byte[] data = Convert.FromBase64String(str);
                return (str.Replace(" ", "").Length % 4 == 0);
            }
            catch
            {
                return false;
            }
        }

        #region Delete user record
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUserPage(int id)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                int result = 0;
                result = _Userepository.DeleteUserPage(id);
                TempData["DeleteResult"] = result;
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return RedirectToAction("UserList", "User");
        }
        #endregion

    }
}
