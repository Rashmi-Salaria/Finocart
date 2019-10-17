using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Web.Views.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Finocart.Web.Controllers
{
    public class NotificationController : BaseController
    {

        #region Interface declaration

        /// <summary>
        /// Invoice Data Repository
        /// </summary>
        private readonly INotification _notificationRepository;
        private readonly IBank _bankRepository;
        private readonly IAnchorCompany _AnchorCompanyRepository;
        private readonly IUser _Userepository;
        private readonly ICommon _CommonRepository;
        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        public NotificationController(INotification notificationRepository, IBank bankRepository, IAnchorCompany anchorCompanyRepository, IUser UserRepository, ICommon CommonRepository)
        {
            _notificationRepository = notificationRepository;
            _bankRepository = bankRepository;
            _AnchorCompanyRepository = anchorCompanyRepository;
            _Userepository = UserRepository;
            _CommonRepository = CommonRepository;
        }

        #endregion

        #region Notification List

        /// <summary>
        /// Get Notification List in Layout
        /// </summary>
        /// <returns></returns>
        public JsonResult NotificationsList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                IEnumerable<NotificationModel> lstNotification = null;
                var Res = _CommonRepository.AuditTrailLog("Notification", "Vendor Notification  List", UserID, 0);
                int pageSize = 5;
                var UserId = HttpContext.Session.GetInt32("CompanyID");
                var role = HttpContext.Session.GetString("Role");
                var roleAccess = HttpContext.Session.GetString("RoleAccess");
                int isRead = 0;

                int i = 0;

                if (roleAccess == "Vendor")
                {

                    lstNotification = _Userepository.getUserNotificationList("", pageSize, UserId, 0, "", isRead);
                    if (lstNotification != null && lstNotification.Count() != 0)
                    {
                        i = lstNotification.Count();
                    }
                    //var result = new { data = lstUserNotification, data2 = i };
                    //return Json(result);
                }

                else if(roleAccess == "Anchor Company")
                {
                    lstNotification = _AnchorCompanyRepository.getAnchorNotificationList("", pageSize, UserId,0,"",isRead);
                    if (lstNotification != null && lstNotification.Count() != 0)
                    {
                        i = lstNotification.Count();
                    }
                    
                }

                else if (roleAccess == "Bank")
                {
                    lstNotification = _AnchorCompanyRepository.getBankNotificationList("", pageSize, UserId, 0, "", isRead);
                    if (lstNotification != null && lstNotification.Count() != 0)
                    {
                        i = lstNotification.Count();
                    }

                }

                var result = new { data = lstNotification, data2 = i };
                return Json(result);

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

        /// <summary>
        /// Call Notification Page
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "AdminLogin")]
        [CustomFilter]
        public IActionResult NotificationList()
        {
            return View("~/Views/Notification/Notification.cshtml");
        }

        /// <summary>
        /// Get Notifications List in Page
        /// </summary>
        /// <returns></returns>
       
        [HttpPost]
        public JsonResult getNotificationList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                int isRead = 0;
                var Res = _CommonRepository.AuditTrailLog("Notification", "Vendor Notification  List", UserID, 0);
                IEnumerable<NotificationModel> lstNotification = null;

                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var Notification = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var UserId = HttpContext.Session.GetInt32("UserID");

                var role = HttpContext.Session.GetString("Role");
                var roleAccess = HttpContext.Session.GetString("RoleAccess");
                if (roleAccess == "Vendor Company")
                {
                    lstNotification = _notificationRepository.getNotificationList(sortBy, pageSize, UserId, skip, Notification,isRead);


                }
                else if (roleAccess == "Anchor Company")
                {
                    lstNotification = _AnchorCompanyRepository.getAnchorNotificationList("", pageSize, UserId,0,"",isRead);
                }

                else if(roleAccess == "Bank")
                {
                    lstNotification = _AnchorCompanyRepository.getBankNotificationList("", pageSize, UserId, 0, "", isRead);
                }

                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstNotification != null && lstNotification.Count() != 0)
                {
                    recordsFiltered = lstNotification.ElementAt(0).FilteredRecord;
                    recordsTotal = lstNotification.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstNotification });

            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                // throw ex;
                return Json(new
                {
                    redirectUrl = Url.Action("ErrorPage", "Common"),
                    isRedirect = true
                });

               // RedirectToAction("ErrorPage", "Common");

               //return Json(null);
               // var redirectUrl = new UrlHelper(Request.).Action("ErrorPage", "Common");
               // return Json(null);
              // return Json(new {Url= redirectUrl });

                //var redirectUrl = new UrlHelper(Request.RequestContext).Action("ErrorPage", "Common");
               // return Json(new { Url = redirectUrl });


            }

        }

        #endregion

        #region  Update all Notification as Read
        /// <summary>
        /// All notification read view
        /// </summary>
        /// <returns></returns>
        public IActionResult AllNotificationsRead()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Notification ", "Mark All Read Notification List", UserID, 0);
                var UserId = HttpContext.Session.GetInt32("UserID");
                var role = HttpContext.Session.GetString("Role");
                var Result = _notificationRepository.UpdateUser(UserId,role);
                TempData["UpdateResult"] = Result;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return RedirectToAction("NotificationList", "Notification");
        }
        #endregion

    }
}