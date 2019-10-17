using Finocart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Finocart.Web.Views.Common;
using Finocart.Interface;
using System.Security.Claims;

namespace Finocart.Web.Controllers
{
    /// <summary>
    /// Manage Session management
    /// </summary>
    /// 
   
    public class BaseController : Controller
    {
        /// <summary>
        /// On Action execution
        /// </summary>
        /// <param name="filterContext"></param>
        /// 


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                var context = filterContext.HttpContext;
                string controllerName = (string)filterContext.RouteData.Values["Controller"];
                string actionName = (string)filterContext.RouteData.Values["Action"];

                string cookieValueFromReq = Request.Cookies["RoleAccess"];

                var descriptor = filterContext.ActionDescriptor;

                IEnumerable<string> sesionKeys = HttpContext.Session.Keys.ToList();

                if (sesionKeys.Count() == 0)
                {
                    if (Request.Cookies["UserID"] != null)
                    {
                        AssignedCookieValue();
                    }
                    else
                    {

                        if (cookieValueFromReq == "Anchor Company")
                        {

                            filterContext.Result = RedirectToAction("UserLogin", "Account");
                        }

                        else if (cookieValueFromReq == "Vendor")
                        {

                            filterContext.Result = RedirectToAction("UserLogin", "Account");


                        }

                        else if (cookieValueFromReq == "Both")
                        {

                            filterContext.Result = RedirectToAction("UserLogin", "Account");
                        }

                        else if (cookieValueFromReq == "AdminLogin")
                        {

                            filterContext.Result = RedirectToAction("AdminLogin", "Account");
                        }

                        else if (cookieValueFromReq == "SuperAdmin")
                        {

                            filterContext.Result = RedirectToAction("SuperAdminLogin", "Account");

                        }

                        else if (cookieValueFromReq == "Bank")
                        {

                            filterContext.Result = RedirectToAction("UserLogin", "Account");

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        #region AssignedCookieValue
        private void AssignedCookieValue()
        {
            try
            {
                HttpContext.Session.SetInt32("UserID", Convert.ToInt32(SecurityHelperService.Decrypt(Request.Cookies["UserID"])));
                HttpContext.Session.SetString("UserName", Convert.ToString(SecurityHelperService.Decrypt(Request.Cookies["UserName"])));
                HttpContext.Session.SetString("Role", Convert.ToString(SecurityHelperService.Decrypt(Request.Cookies["Role"])));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        public class JWTAuthorizeAttribute : TypeFilterAttribute
        {
            public JWTAuthorizeAttribute(string controller, string action)
            : base(typeof(JWTAuthorizeActionFilter))
            {
                Arguments = new object[] { controller, action };
            }
        }

        public class JWTAuthorizeActionFilter : IAuthorizationFilter
        {
            private readonly string _controller;
            private readonly string _action;
            public JWTAuthorizeActionFilter(string controller, string action)
            {
                _controller = controller;
                _action = action;
            }
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var currentUser = context.HttpContext.User;
                if (!currentUser.HasClaim(x => x.Type == ClaimTypes.GivenName))
                {
                    context.Result = new RedirectToActionResult(_action, _controller, new { ReturnUrl = "" });
                }
            }
        }

    }

    
}