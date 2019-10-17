using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Services;
using Finocart.Web.Views.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finocart.Web.Controllers
{

    public class VendorAnalyticsController : BaseController
    {
        #region Interface declaration
        /// <summary>
        ///Vendor Repository
        /// </summary>
        private readonly IVendorAnalytics _VendorAnalyticsRepository;
        private readonly ICommon _CommonRepository;
        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="UserRepository"></param>
        public VendorAnalyticsController(IVendorAnalytics vendorAnalyticsRepository, ICommon CommonRepository)
        {
            _VendorAnalyticsRepository = vendorAnalyticsRepository;
            _CommonRepository = CommonRepository;
        }


        #endregion

        /// <summary>
        /// Analysis of Lost opportunities view
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult AnalyticsLostOpp()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Lost opportunities", "Analytic Lost opportunities List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<AnchorCompDropDownModel> lstCompany = _VendorAnalyticsRepository.getAnchorCompList(VendorId);
                return View("~/Views/Vendor/AnalyticLostOpp.cshtml", lstCompany);
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
        /// Getting list of lost opportunities
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLostOpportunitiesList(int companyID, DateTime fromDate, DateTime toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Lost opportunities", " Lost opportunities List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
               
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                

                var CompanyName = companyID;
                var FromDate = fromDate;
                var ToDate = toDate;

                IEnumerable<LostOpportunitiesModel> lstAnchorCompany = _VendorAnalyticsRepository.GetLostOpportunities(VendorId, sortBy, pageSize, skip, CompanyName, fromDate, toDate);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstAnchorCompany != null && lstAnchorCompany.Count() != 0)
                {
                    recordsFiltered = lstAnchorCompany.ElementAt(0).FilteredRecord;
                    recordsTotal = lstAnchorCompany.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAnchorCompany });
                
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
        /// Analysis of Funding Request
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult AnalyticsFundingRequest()
        {
            return View("~/Views/Vendor/AnalyticFundingReq.cshtml");
        }

        /// <summary>
        /// Getting Funding request list
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFundingRequestList(DateTime fromDate, DateTime toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Vendor Funding Request", "Funding request List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                

                IEnumerable<AnalyticsFundingReqModel> lstAnchorCompany = _VendorAnalyticsRepository.GetFundingRequest(VendorId, sortBy, pageSize, skip, fromDate, toDate);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstAnchorCompany != null && lstAnchorCompany.Count() != 0)
                {
                    recordsFiltered = lstAnchorCompany.ElementAt(0).FilteredRecord;
                    recordsTotal = lstAnchorCompany.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAnchorCompany });
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
        /// Export analysis of funding request view
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IActionResult ExportAnalyticsFundingRequest(DateTime fromDate, DateTime toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Analytics Funding Request", "Export CSV Analytics Funding request List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
            //var draw = "1";
            var start = "0";
            var length = "10";
            //Get Sort columns value
            var sortBy = "CompanyID asc";

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                
                IEnumerable<AnalyticsFundingReqModel> lstAnchorCompany = _VendorAnalyticsRepository.GetFundingRequest(VendorId, sortBy, pageSize, skip, fromDate, toDate);

                string csv = ExportgetAvailable(lstAnchorCompany);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "ApprovalInvoice.csv");
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
        /// Export get available
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportgetAvailable(IEnumerable<AnalyticsFundingReqModel> objData)
        {
            return CommonService.ListToCSV(objData, "TotalRecord,FilteredRecord");
        }

        /// <summary>
        /// Getting funding request amount list
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFundingRequestAmountList(int companyID, string fromDate, string toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Analytics Funding Request", "Funding request Amount List", UserID, 0);
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

                IEnumerable<AnalyticsFundingReqViewModel> lstFundReqView = _VendorAnalyticsRepository.GetFundingRequestAmount(VendorId, sortBy, pageSize, skip, companyID, FromDate, ToDate);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstFundReqView != null && lstFundReqView.Count() != 0)
                {
                    recordsFiltered = lstFundReqView.ElementAt(0).FilteredRecord;
                    recordsTotal = lstFundReqView.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstFundReqView });
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
        /// Get funding request percentage list
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFundingRequestPercentList(int companyID, string fromDate, string toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Funding Request", "Funding Request List", UserID, 0);
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

                IEnumerable<AnalyticsFundingReqViewModel> lstFundReqView = _VendorAnalyticsRepository.GetFundingRequestPercent(VendorId, sortBy, pageSize, skip, companyID, FromDate, ToDate);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstFundReqView != null && lstFundReqView.Count() != 0)
                {
                    recordsFiltered = lstFundReqView.ElementAt(0).FilteredRecord;
                    recordsTotal = lstFundReqView.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstFundReqView });
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
        /// Get lost opportunities list
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLostOpportunities()
        {
            try
            {
                return View("GetLostOpportunities");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}