using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Finocart.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finocart.Web.Controllers
{
    /// <summary>
    /// Request handler
    /// </summary>
    public class RequestHanlderMiddleware
    {
        #region Property/Variable
        private RequestDelegate _next;
        private readonly IConfiguration _configuration;
        //private readonly ICommon _CommonRepository;
        #endregion

        #region Constructor
        public RequestHanlderMiddleware(RequestDelegate next, IConfiguration ConfigurationRepository)
        {
            _next = next;
            _configuration = ConfigurationRepository;
          
        }
        #endregion

        #region Method
        public async Task Invoke(HttpContext context, ICommon _CommonRepository)
        {
            try
            {
                string requestedURL = context.Request.Path;

                IEnumerable<string> sesionKeys = context.Session.Keys;
                if (sesionKeys.Count() == 0)
                {
                    await context.Response.WriteAsync
                    ("<h2>Unauthorised access<h2>");
                    return;
                }

                string fileName = requestedURL.Substring(requestedURL.LastIndexOf("/") + 1);
                Int32? RequestUserId = context.Session.GetInt32("UserID") == null ? 0 : context.Session.GetInt32("UserID");

                if (requestedURL.Contains(_configuration["VendorDocumentsFolder"]))
                {
                    ////Validate with vendorDocument table 
                    bool IsvalidUser = false;
                    IsvalidUser = _CommonRepository.ValidateVendorUser(RequestUserId, fileName);
                    if (!IsvalidUser)
                    {
                        await context.Response.WriteAsync
                        ("<h2>Unauthorised access<h2>");
                        return;
                    }
                }
                else if (requestedURL.Contains(_configuration["AnchorDocumentsFolder"]))
                {
                    ////Validate with AnchorDocument 
                    bool IsvalidUser = false;
                    IsvalidUser = _CommonRepository.ValidateAnchorCompUser(RequestUserId, fileName);
                    if (!IsvalidUser)
                    {
                        await context.Response.WriteAsync
                        ("<h2>Unauthorised access<h2>");
                        return;
                    }
                }
                //http://localhost:4670/VendorDocuments/Vendor_PAN_20190405155223486.jpg
            }
            catch(Exception ex)
            {
                throw ex;
            }
            }
        
        #endregion
    }
}
