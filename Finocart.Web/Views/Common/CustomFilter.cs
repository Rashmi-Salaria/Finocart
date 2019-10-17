
using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Services;
using Finocart.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Finocart.Web.Views.Common
{

    public class CustomFilter : ActionFilterAttribute, IExceptionFilter
    {
        private readonly ICommon _CommonRepository;
        string Result = "";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string RoleAccess = filterContext.HttpContext.Session.GetString("RoleAccess");
            string Role = filterContext.HttpContext.Session.GetString("Role");
            string controllername = (string)filterContext.RouteData.Values["Controller"];
            string actionname = (string)filterContext.RouteData.Values["Action"];
            
            try
            {
               var _loginService = (ICommon)filterContext.HttpContext.RequestServices.GetService(typeof(ICommon));
                Result = _loginService.CustomUserFilter(controllername, actionname, RoleAccess, Role);

            }
            catch (Exception ex) { }

            if (Result == "0")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Account" },
                                          { "action", "AccessDenied" }

                                         });
            }
            base.OnActionExecuting(filterContext);
        }

        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
  

    }
   
}
