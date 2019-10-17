using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Finocart.CustomModel;
using Finocart.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Finocart.Web.Views.Common;

namespace Finocart.Web.Controllers
{
   
    public class AnchorAnalyticsController : BaseController
    {
        #region Interface declaration
        /// <summary>
        ///Anchor Company Repository
        /// </summary>
        private readonly IAnchorCompany _AnchorCompanyRepository;
        private readonly ICommon _CommonRepository;
        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="anchorCompanyRepository"></param>
        public AnchorAnalyticsController(IAnchorCompany anchorCompanyRepository, ICommon CommonRepository)
        {
            _AnchorCompanyRepository = anchorCompanyRepository;
            _CommonRepository = CommonRepository;
        }
        #endregion

        #region Analytics

        #region Anchor Analytics Lost Opportunity
        /// <summary>
        /// Showing anchor's lost opprtunities
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult AnchorLostOpportunities()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<AnchorCompDropDownModel> lstCompany = _AnchorCompanyRepository.getVendorList(AnchorCompId);
                return View("~/Views/AnchorCompany/GetLostOpportunities.cshtml", lstCompany);
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            
        }

        /// <summary>
        /// Getting Anchor lost oppurtunities
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAnchorLostOpportunities(int companyID, string fromDate, string toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Lost Opportunities", "Anchore Lost opportinities  List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var FromDate = fromDate;
                var ToDate = toDate;

                IEnumerable<AnchorAnalyticLostOppModel> lstAnalyticsLostOpp = _AnchorCompanyRepository.GetLostOpportunity(VendorId, sortBy, pageSize, skip, companyID, FromDate, ToDate);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstAnalyticsLostOpp != null && lstAnalyticsLostOpp.Count() != 0)
                {
                    recordsFiltered = lstAnalyticsLostOpp.ElementAt(0).FilteredRecord;
                    recordsTotal = lstAnalyticsLostOpp.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAnalyticsLostOpp });
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
        #endregion

    }
}