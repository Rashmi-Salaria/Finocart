using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Model;
using Finocart.Services;
using Finocart.Web.Views.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finocart.Web.Controllers
{
    public class AnchorCompDashboardController : BaseController
    {
        #region Interface declaration
        private readonly IAnchorCompany _AnchorCompanyRepository;
        private readonly ILookupDetails _lookUpRepository;
        private readonly IBank _companyRepository;
        private readonly ICommon _CommonRepository;
        #endregion


        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AnchorCompanyRepository"></param>
        public AnchorCompDashboardController(IAnchorCompany anchorCompanyRepository, ILookupDetails lookupDetailsRepository,IBank companyRepository, ICommon CommonRepository)
        {
            _AnchorCompanyRepository = anchorCompanyRepository;
            _lookUpRepository = lookupDetailsRepository;
            _companyRepository = companyRepository;
            _CommonRepository = CommonRepository;
        }


        #endregion

        #region Critical Vendors

        #region Get Critical Vendors Page
        /// <summary>
        /// Getting critical vendors
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public IActionResult CriticalVendors(int? result)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Critical Vendor", "Critical Vendor List", UserID, 0);
                TempData["DeleteResult"] = result;
                CriticalVendorsModel criticalVendorsModel = new CriticalVendorsModel();
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<VendorsDropDownModel> lstVendors = _AnchorCompanyRepository.GetVendorsDropDown(CompanyID);
                IEnumerable<LookupDetails> lstInvoiceLimitStatus = _lookUpRepository.getLookupDetailByKey("CriticalVendor");
                Tuple<CriticalVendorsModel, IEnumerable<VendorsDropDownModel>, IEnumerable<LookupDetails>> objCriticalVendorData =
                new Tuple<CriticalVendorsModel, IEnumerable<VendorsDropDownModel>, IEnumerable<LookupDetails>>(criticalVendorsModel, lstVendors, lstInvoiceLimitStatus);
                return View("~/Views/AnchorCompany/CriticalVendors.cshtml", objCriticalVendorData);
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
        #endregion

        #region Get Critical Vendors List
        /// <summary>
        /// Getting list of critical vendors
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCriticalVendorsList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Critical Vendor", "Critical Vendor List", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var VendorName = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var TotalInvAmtLimit = Request.Form["columns[4][search][value]"].FirstOrDefault();

                IEnumerable<CriticalVendorsModel> lstCriticalVendors = _AnchorCompanyRepository.GetCriticalVendors(CompanyID, sortBy, pageSize, skip, VendorName, TotalInvAmtLimit);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstCriticalVendors != null && lstCriticalVendors.Count() != 0)
                {
                    recordsFiltered = lstCriticalVendors.ElementAt(0).FilteredRecord;
                    recordsTotal = lstCriticalVendors.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstCriticalVendors });
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

        #region Insert And Update Critical Vendors
        /// <summary>
        /// Adding critical vendors
        /// </summary>
        /// <param name="objCriticalVendors"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCriticalVendors(CriticalVendorsModel objCriticalVendors)
        {            
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Critcal Vendor", "Add Critical Vendor (List page)", UserID, 0);
                var Result = _AnchorCompanyRepository.InsertUpdateCriticalVendor(objCriticalVendors);
                TempData["AddResult"] = Result;
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

            return RedirectToAction("CriticalVendors", "AnchorCompDashboard");
        }
        #endregion

        #region Delete Critical Vendors
        /// <summary>
        /// Deleting critical vendors
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteCriticalVendors(int id)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            int result = 0;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Critical Vendor", "Delete Critical Vendor (List page)", UserID, 0);
                result = _AnchorCompanyRepository.DeleteCriticalVendors(id);
                TempData["DeleteResult"] = result;
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }
            return Json(result);
        }
        #endregion

        #region Critical Vendors Export to CSV
        /// <summary>
        /// Exporting list of critical vendors
        /// </summary>
        /// <param name="VendorName"></param>
        /// <param name="TotalInvAmtLimit"></param>
        /// <returns></returns>
        public IActionResult ExportCriticalVendors(string VendorName, string TotalInvAmtLimit)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Critical Vendor", "Export CSV Critical Vendor List", UserID, 0);
            Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            var sortBy = "VendorName asc";
            int pageSize = 100000;
            int skip = 0;
        
            IEnumerable<CriticalVendorsModel> lstCriticalVendors = _AnchorCompanyRepository.GetCriticalVendors(CompanyID, sortBy, pageSize, skip, VendorName, TotalInvAmtLimit);

            string csv = ExportCriticalVendorDatastr(lstCriticalVendors);

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "CriticalVendors.csv");
        }

        /// <summary>
        /// Export vendor list
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportCriticalVendorDatastr(IEnumerable<CriticalVendorsModel> objData)
        {
            return CommonService.ListToCSV(objData, "Status,TotalRecord,FilteredRecord");
        }
        #endregion
        #endregion

        #region Invoice Details
        #region Payment Due Today
        #region Get Payment Due Today Page
        /// <summary>
        /// Payment due today view
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult PaymentDueToday()
        {
            return View("~/Views/AnchorCompany/PaymentDueToday.cshtml");
        }
        #endregion

        #region Get Payment Due Today List
        /// <summary>
        /// List of payment due today
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPaymentDueTodayList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Payable Due Date", "Payement Due Date List", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var VendorName = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var TotalAppInvAmt = Request.Form["columns[4][search][value]"].FirstOrDefault();

                IEnumerable<VendorPaymentDueModel> lstPaymentDueVendors = _AnchorCompanyRepository.GetPaymentDueToday(CompanyID, sortBy, pageSize, skip, VendorName, TotalAppInvAmt);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstPaymentDueVendors != null && lstPaymentDueVendors.Count() != 0)
                {
                    recordsFiltered = lstPaymentDueVendors.ElementAt(0).FilteredRecord;
                    recordsTotal = lstPaymentDueVendors.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstPaymentDueVendors });
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

        #region
        //Export To CSV PaymentDueToday
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="VendorName"></param>
        /// <param name="TotalAppInvAmt"></param>
        /// <returns></returns>
        public IActionResult ExportPaymentDueTodayListToCSV(int CompanyId, string VendorName, string TotalAppInvAmt)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("payment Due Today", "CSV Export payment Due Today List  ", UserID, 0);
            Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
           
            //Get Sort columns value
            var sortBy = "VendorID asc";
            int pageSize = 100000;
            int skip = 0;
            VendorName = "";
            TotalAppInvAmt = "";
            IEnumerable<VendorPaymentDueModel> lstPaymentDueVendors = _AnchorCompanyRepository.GetPaymentDueToday(CompanyID, sortBy, pageSize, skip, VendorName, TotalAppInvAmt);
            string csv = ExportPaymentDueTodaylist(lstPaymentDueVendors);

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "PaymentDueToday.csv");
        }

        /// <summary>
        /// Exporting vendor list
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportPaymentDueTodaylist(IEnumerable<VendorPaymentDueModel> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord");
        }

        #endregion




        #region Get Payment Due Today View Page
        /// <summary>
        /// Getting list of payment due by vendor id
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IActionResult PaymentDueTodayView(int VendorID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            VendorPaymentDueViewModel vendorPaymentDueViewModel = new VendorPaymentDueViewModel();
            try
            {
                int id = Convert.ToInt32(VendorID);
                var Res = _CommonRepository.AuditTrailLog("Payable Due Today", "Payment Due Today View List", UserID, id);
                vendorPaymentDueViewModel.VendorID = VendorID;
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return View("~/Views/AnchorCompany/PaymentDueTodayView.cshtml", vendorPaymentDueViewModel);
        }
        #endregion

        #region Get Payment Due Today View List 
        /// <summary>
        /// Getting payment due today view
        /// </summary>
        /// <param name="vendorPaymentDueViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPaymentDueTodayViewList(VendorPaymentDueViewModel vendorPaymentDueViewModel)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Payment Due date", "payment Today View Lisy  ", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var InvoiceID = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var InvoiceAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<VendorPaymentDueViewModel> lstPaymentDueVendorView = _AnchorCompanyRepository.GetPaymentDueTodayView(CompanyID, sortBy, pageSize, skip, InvoiceID, InvoiceAmt, vendorPaymentDueViewModel.VendorID);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstPaymentDueVendorView != null && lstPaymentDueVendorView.Count() != 0)
                {
                    recordsFiltered = lstPaymentDueVendorView.ElementAt(0).FilteredRecord;
                    recordsTotal = lstPaymentDueVendorView.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstPaymentDueVendorView });
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

        #region Invoice Pending For Action
        #region  Get Invoice Pending For Action Page
        /// <summary>
        /// Await approval view page
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult AwaitedInvoiceApproval()
        {
            return View("~/Views/AnchorCompany/DiscApprovalAwaitedInvoice.cshtml");
        }
        #endregion

        #region Get Invoice Pending For Action List
        /// <summary>
        /// Action on awaited invoice approval list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAwaitedInvoiceApprovalList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoice Pending For Action", "Awaited Invoice Approval List", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var VendorName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var TotalAppInvAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<VendorAwaitedInvApprovalModel> lstAwaitedInvoiceVendors = _AnchorCompanyRepository.GetAwaitedInvoiceApproval(CompanyID, sortBy, pageSize, skip, VendorName, TotalAppInvAmt);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstAwaitedInvoiceVendors != null && lstAwaitedInvoiceVendors.Count() != 0)
                {
                    recordsFiltered = lstAwaitedInvoiceVendors.ElementAt(0).FilteredRecord;
                    recordsTotal = lstAwaitedInvoiceVendors.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAwaitedInvoiceVendors });
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
        // export to excel anchor compamy list by  
        public IActionResult ExportAnchorCompListByVendorID(string VendorName, string TotalAppInvAmt)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                VendorName = "";
                TotalAppInvAmt = "";
                IEnumerable<VendorAwaitedInvApprovalModel> lstAwaitedInvoiceVendors = _AnchorCompanyRepository.GetAwaitedInvoiceApproval(CompanyID, sortBy, pageSize, skip, VendorName, TotalAppInvAmt);

                string csv = ExportAwaitedApprovl(lstAwaitedInvoiceVendors);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "AnchorCompanyByVendor.csv");
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
        public string ExportAwaitedApprovl(IEnumerable<VendorAwaitedInvApprovalModel> objData)
        {
            return CommonService.ListToCSV(objData, "ApprovedInv,RejectedInv,PendingInv,Location,TotalRecord,FilteredRecord");
        }

        //Export to excel get wait for action by vendorid
        public IActionResult ExportAwaitedInvoiceApprovalView(string VendorName, string TotalAppInvAmt, string VendorId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Awaited Invoice Approval ", "Awaited invoice View List CSV file", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                Int32 VendorID = Convert.ToInt32(VendorId);
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                VendorName = "";
                TotalAppInvAmt = "";
                var BucketID = "";
                IEnumerable<VendorBucketAwaitedInvViewModel> lstAwaitedInvVendorsView = _AnchorCompanyRepository.GetAwaitedInvoiceApprovalView(CompanyID, VendorID, sortBy, pageSize, skip, BucketID, TotalAppInvAmt);

                string csv = ExportAwaitedApprovalInvoiceView(lstAwaitedInvVendorsView);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "AnchorCompanyByVendorView.csv");

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
        public string ExportAwaitedApprovalInvoiceView(IEnumerable<VendorBucketAwaitedInvViewModel> objData)
        {
            return CommonService.ListToCSV(objData, "ApprovedInv,RejectedInv,PendingInv,Location,TotalRecord,FilteredRecord");
        }
        #endregion

        #region Get Invoice Pending For Action View Page
        /// <summary>
        /// Awaited invoice approval list by vendor
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IActionResult AwaitedInvoiceApprovalView(string VendorIDs)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            VendorAwaitedInvApprovalModel vendorAwaitedInvApproval = new VendorAwaitedInvApprovalModel();
            try
            {
                var data = IsBase64Encoded(VendorIDs);
                if (data == true)
                {
                    var result = Convert.FromBase64String(VendorIDs);
                    int VenID = Convert.ToInt16(Encoding.UTF8.GetString(result));
                    vendorAwaitedInvApproval.VendorID = VenID;
                    return View("~/Views/AnchorCompany/DiscApprovalAwaitedInvoiceView.cshtml", vendorAwaitedInvApproval);
                }
                else
                {
                    return View("~/Views/User/PageNotFound.cshtml");
                }
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

        #region Get Invoice Pending For Action View List
        /// <summary>
        /// Action on awaited incoice approcal view list
        /// </summary>
        /// <param name="vendorAwaitedInvApproval"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAwaitedInvoiceApprovalViewList(VendorAwaitedInvApprovalModel vendorAwaitedInvApproval)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoice Pending For Action", "Awaited Invoice Approval View List", UserID, 0);
                //var data = IsBase64Encoded(Convert.ToString(vendorAwaitedInvApproval.VendorID));
                //if (data == true)
                //{
                //    var result = Convert.FromBase64String(Convert.ToString(vendorAwaitedInvApproval.VendorID));
                //    vendorAwaitedInvApproval.VendorID = Convert.ToInt16(Encoding.UTF8.GetString(result));
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                    var draw = Request.Form["draw"].FirstOrDefault();
                    var start = Request.Form["start"].FirstOrDefault();
                    var length = Request.Form["length"].FirstOrDefault();
                    //Get Sort columns value
                    var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    var BucketID = Request.Form["columns[0][search][value]"].FirstOrDefault();
                    var TotalAppInvAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                    IEnumerable<VendorBucketAwaitedInvViewModel> lstAwaitedInvVendorsView = _AnchorCompanyRepository.GetAwaitedInvoiceApprovalView(CompanyID, vendorAwaitedInvApproval.VendorID, sortBy, pageSize, skip, BucketID, TotalAppInvAmt);
                    int? recordsFiltered = 0;
                    int? recordsTotal = 0;

                    if (lstAwaitedInvVendorsView != null && lstAwaitedInvVendorsView.Count() != 0)
                    {
                        recordsFiltered = lstAwaitedInvVendorsView.ElementAt(0).FilteredRecord;
                        recordsTotal = lstAwaitedInvVendorsView.ElementAt(0).TotalRecord;
                    }

                    return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAwaitedInvVendorsView });
                //}
                //else
                //{
                //    return Json(null);
                //}

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

        #region Bucket Wise Invoice View Page
        /// <summary>
        /// Getting bucket detail
        /// </summary>
        /// <param name="BucketID"></param>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IActionResult BucketWiseInvoiceViewList(string BuckID, string VendID,string VendorCompany)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            VendorBucketAwaitedInvViewModel vendorBucketAwaitedInvView = new VendorBucketAwaitedInvViewModel();
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoice Pending For Action ", "BucketWise Invoice View List(Awaited Invoice Approval View List)", UserID, 0);
                var data = IsBase64Encoded(BuckID);
                var data1 = IsBase64Encoded(VendID);
                var data2 = IsBase64Encoded(VendorCompany);
                if (data == true && data1 == true && data2 == true)
                {
                    var result = Convert.FromBase64String(BuckID);
                    Int64 BuckIDs = Convert.ToInt64(Encoding.UTF8.GetString(result));
                    var result1 = Convert.FromBase64String(VendID);
                    int VendIDs = Convert.ToInt16(Encoding.UTF8.GetString(result1));
                    var result2 = Convert.FromBase64String(VendorCompany);
                    string VendComps = Convert.ToString(Encoding.UTF8.GetString(result2));
                    vendorBucketAwaitedInvView.VendorID = VendIDs;
                    vendorBucketAwaitedInvView.BucketID = BuckIDs;
                    vendorBucketAwaitedInvView.BucketName = VendComps;
                }
                else
                {
                    return View("~/Views/User/PageNotFound.cshtml");
                }
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return View("~/Views/AnchorCompany/BucketWiseInvoicesView.cshtml", vendorBucketAwaitedInvView);
        }
        #endregion

        #region Get Bucket Wise Invoice View List
        /// <summary>
        /// Getting bucket wise invoice list view
        /// </summary>
        /// <param name="vendorBucketInvoiceView"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBucketWiseInvoiceViewList(VendorBucketAwaitedInvViewModel vendorBucketInvoiceView)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoice Pending For Action ", "BucketWise Invoice View List(Awaited Invoice Approval View List)", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var BucketID = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var TotalAppInvAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<VendorBucketInvoiceViewModel> lstBucketInvVendorsView = _AnchorCompanyRepository.GetBucketWiseInvoiceView(CompanyID, vendorBucketInvoiceView.VendorID, vendorBucketInvoiceView.BucketID, sortBy, pageSize, skip, BucketID, TotalAppInvAmt);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstBucketInvVendorsView != null && lstBucketInvVendorsView.Count() != 0)
                {
                    recordsFiltered = lstBucketInvVendorsView.ElementAt(0).FilteredRecord;
                    recordsTotal = lstBucketInvVendorsView.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstBucketInvVendorsView });
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

        #region Discounted Invoices Approved Today
        #region Invoices Approved Today Page
        /// <summary>
        /// View invoice approved today
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult InvoiceApprovedToday()
        {
            return View("~/Views/AnchorCompany/DiscInvoiceApprovedToday.cshtml");
        }
        #endregion

        #region Get Invoice Approved Today List
        /// <summary>
        /// Get invoice approved today list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetInvoiceApprovedTodayList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoices Approved Today", "Invoice Approved Today List", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var VendorName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var TotalAppInvAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<VendorInvoiceApprovedTodayModel> lstInvoiceApprovedToday = _AnchorCompanyRepository.GetInvoiceApprovedToday(CompanyID, sortBy, pageSize, skip, VendorName, TotalAppInvAmt);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstInvoiceApprovedToday != null && lstInvoiceApprovedToday.Count() != 0)
                {
                    recordsFiltered = lstInvoiceApprovedToday.ElementAt(0).FilteredRecord;
                    recordsTotal = lstInvoiceApprovedToday.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstInvoiceApprovedToday });
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

        #region Invoice Approved Today View Page
        /// <summary>
        /// Invoice approved today by vendor
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IActionResult InvoiceApprovedTodayView(int VendorID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            VendorInvoiceApprovedTodayModel vendorInvoiceApprovedToday = new VendorInvoiceApprovedTodayModel();

            try
            {
                vendorInvoiceApprovedToday.VendorID = VendorID;
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }

            return View("~/Views/AnchorCompany/DiscInvoiceApprovedTodayView.cshtml", vendorInvoiceApprovedToday);
        }
        #endregion

        #region  Get Invoice Approved Today View List      
        /// <summary>
        /// Get incoice approved today view list
        /// </summary>
        /// <param name="vendorInvoiceApprovedToday"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetInvoiceApprovedTodayViewList(VendorInvoiceApprovedTodayModel vendorInvoiceApprovedToday)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoices Approved Today", "Invoice Approved Today View List", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var BucketID = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var TotalAppInvAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<VendorInvApproveTodayViewModel> lstAwaitedInvVendorsView = _AnchorCompanyRepository.GetInvoiceApprovedTodayView(CompanyID, vendorInvoiceApprovedToday.VendorID, sortBy, pageSize, skip, BucketID, TotalAppInvAmt);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstAwaitedInvVendorsView != null && lstAwaitedInvVendorsView.Count() != 0)
                {
                    recordsFiltered = lstAwaitedInvVendorsView.ElementAt(0).FilteredRecord;
                    recordsTotal = lstAwaitedInvVendorsView.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAwaitedInvVendorsView });
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

        #region Bucket Wise Discounted Invoice Approved Today Page
        /// <summary>
        /// Bucket wise invoice view
        /// </summary>
        /// <param name="BucketID"></param>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IActionResult BucketWiseDiscInvViewList(Int64 BucketID, int VendorID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            VendorInvApproveTodayViewModel vendorInvApproveTodayView = new VendorInvApproveTodayViewModel();
            try
            {
                int id = Convert.ToInt32(VendorID);
                int Bid = Convert.ToInt32(BucketID);
                var Res = _CommonRepository.AuditTrailLog("Invoice Approved Today ", "Invoice Approved TodayView ,BucketWise Discount Invoice View", id,Bid);
                vendorInvApproveTodayView.VendorID = VendorID;
                vendorInvApproveTodayView.BucketID = BucketID;
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return View("~/Views/AnchorCompany/BucketWiseApprovedTodayInv.cshtml", vendorInvApproveTodayView);
        }
        #endregion

        #region Bucket Wise Discounted Invoice Approved Today View List
        /// <summary>
        /// Action on bucket discount
        /// </summary>
        /// <param name="vendorBucketInvoiceView"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBucketWiseDiscInvViewList(VendorInvApproveTodayViewModel vendorBucketInvoiceView)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoice Approved Today ", "Invoice Approved TodayView ,BucketWise Discount Invoice View", UserID,0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var InvoiceID = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var PurchaseOrder = Request.Form["columns[0][search][value]"].FirstOrDefault();

                IEnumerable<VendorBucketWiseDiscInvViewModel> lstBucketInvVendorsView = _AnchorCompanyRepository.GetBucketWiseDiscInvView(CompanyID, vendorBucketInvoiceView.VendorID, vendorBucketInvoiceView.BucketID, sortBy, pageSize, skip, InvoiceID, PurchaseOrder);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstBucketInvVendorsView != null && lstBucketInvVendorsView.Count() != 0)
                {
                    recordsFiltered = lstBucketInvVendorsView.ElementAt(0).FilteredRecord;
                    recordsTotal = lstBucketInvVendorsView.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstBucketInvVendorsView });
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
        #region
        #endregion
        #endregion
        #endregion

        #region  Anchor Notification List
        /// <summary>
        /// Get Notification List in Layout
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult AnchorNotificationList()
        {
            return View("~/Views/AnchorCompany/AnchorNotification.cshtml");
        }

        /// <summary>
        /// Get Anchor Notifications List in Page
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getAnchorNotificationList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Notification", "Anchor Notification List", UserID, 0);
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var Notification = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var UserId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<NotificationModel> lstNotification = _AnchorCompanyRepository.getAnchorNotificationList(sortBy, pageSize, UserId, skip, Notification);

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
                throw ex;
            }

        }
        //  Update all Notification as Read
        /// <summary>
        /// All anchor notification read
        /// </summary>
        /// <returns></returns>
        public IActionResult AllAnchorNotificationsRead()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Notification ", "Marked all read", UserID, 0);
                var UserId = HttpContext.Session.GetInt32("UserID");
                var Result = _AnchorCompanyRepository.AnchorUpdateUser(UserId);
                TempData["UpdateResult"] = Result;
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");

            }
            return RedirectToAction("AnchorNotificationList", "AnchorCompDashboard");
        }
        #endregion

        #region Get Anchor Company Graph Details
        /// <summary>
        /// Getting graph details by month
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGraphDetails(string month)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Graph details", UserID, 0);
                Int64? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<GraphDetailsModel> lstGraph = _AnchorCompanyRepository.GetGraphDetails(month, AnchorCompId);
                return Json(lstGraph);
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }
        }
        #endregion


        /// <summary>
        /// Invoice history view
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult InvoiceHistory()
        {

            return View("~/Views/AnchorCompany/InvoiceHistory.cshtml");
        }

        /// <summary>
        /// Getting innvoice history
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetInvoiceHistory()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoices Status", "Invoice History List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                
                var VendorName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var Invoicetotatalamt = Request.Form["columns[7][search][value]"].FirstOrDefault();

                IEnumerable<InvoiceStatus> invoiceStatus = _AnchorCompanyRepository.GetInvoiceStatuses(VendorId, AnchorCompId, sortBy, pageSize, skip,VendorName,Invoicetotatalamt);

                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (invoiceStatus != null && invoiceStatus.Count()!=0)
                {
                    recordsFiltered = invoiceStatus.ElementAt(0).FilteredRecord;
                    recordsTotal = invoiceStatus.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw , recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data=invoiceStatus});
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
        /// Vendor wise earning view
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult VenderwiseEarning()
        {
            return View("~/Views/AnchorCompany/Earning_VendorWiseListing.cshtml");

        }

        #region Get Vender wise Earning

        /// <summary>
        /// Getting vendor wise earning
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetVenderwiseEarning(int companyID, DateTime fromDate, DateTime toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Earning", "Vendor Wise Earning List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var FromDate = fromDate;
                var ToDate = toDate;

                var VendorName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var TotalInvoiceAmmount = Request.Form["columns[5][search][value]"].FirstOrDefault();
                IEnumerable<Earning_Vendorwise> earning_s = _AnchorCompanyRepository.GetEarning_Vendorwises(VendorId, sortBy, pageSize, skip, FromDate, toDate);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (earning_s != null && earning_s.Count() != 0)
                {
                    recordsFiltered = earning_s.ElementAt(0).FilteredRecord;
                    recordsTotal = earning_s.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = earning_s });
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
       //export to excel of earning vendorwise
        public IActionResult ExportRegisterToCSV(DateTime fromDate, DateTime toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Earning", "Earing List CSV Export", UserID, 0);
            Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
            var sortBy = "CompanyID asc";
            int pageSize = 100000;
            int skip = 0;
            IEnumerable<Earning_Vendorwise> earning_s = _AnchorCompanyRepository.GetEarning_Vendorwises(VendorId, sortBy, pageSize, skip, fromDate, toDate);

            string csv = ExportRegister(earning_s);

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Earning.csv");
        }
        //list earning vendorwise
        public string ExportRegister(IEnumerable<Earning_Vendorwise> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord");
        }

        #endregion

        #region Get Amount Discount Rate List

        /// <summary>
        /// Getting amount discount rate wise
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAmountDiscountRatewiseList(int companyID, string fromDate, string toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Earing", "Vendor wise Earning View ", UserID, 0);
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int? anc = AnchorCompId;
                Int32? VendorId = companyID;
                var FromDate = fromDate;
                var ToDate = toDate;

                var VendorName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var TotalInvoiceAmmount = Request.Form["columns[5][search][value]"].FirstOrDefault();
                IEnumerable<Eearning_discountRateWise> discountRateWises = _AnchorCompanyRepository.eearning_DiscountRateWises(VendorId,anc, sortBy, pageSize, skip, fromDate, toDate);

                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (discountRateWises != null && discountRateWises.Count() != 0)
                {
                    recordsFiltered = discountRateWises.ElementAt(0).FilteredRecord;
                    recordsTotal = discountRateWises.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = discountRateWises });
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

        #region Get Percent Amount Discount List

        /// <summary>
        /// Getting percentage amount discount rate wise
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public JsonResult GetPercentAmountDiscountRatewiseList(int companyID, string fromDate, string toDate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Earning", "Percent Amount Disount Rate Wise List ", UserID, 0);
                int? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int? anc = AnchorCompId;
                Int32? VendorId = companyID;
                var FromDate = fromDate;
                var ToDate = toDate;

                var VendorName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var TotalInvoiceAmmount = Request.Form["columns[5][search][value]"].FirstOrDefault();
                IEnumerable<Eearning_discountRateWise> eearning_DiscountRateWises = _AnchorCompanyRepository.discountRateWisesinPercent(VendorId,anc, sortBy, pageSize, skip, fromDate, toDate);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (eearning_DiscountRateWises != null && eearning_DiscountRateWises.Count() != 0)
                {
                    recordsFiltered = eearning_DiscountRateWises.ElementAt(0).FilteredRecord;
                    recordsTotal = eearning_DiscountRateWises.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = eearning_DiscountRateWises });
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

        #region Rule of Engine

        public string RuleofEngine(string rule_percentage = "", string rule_limit = "", string Internalfund = "", string FromBank = "", string chkUnlimited = "", string AnchorRate = "")
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Modify", "set Rule of Engine", UserID, 0);
                var Result = _AnchorCompanyRepository.UpdateRuleOfEngine(CompanyID, rule_percentage, rule_limit, Internalfund, FromBank, chkUnlimited, AnchorRate);
            }

            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }

            return "Success";

        }
        #endregion

        #region list Get Rule of EngineValue
        public IActionResult GetRuleOfEngineData()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home/Modify", "Rule of Engine Data ", UserID, 0);
                RuleofEngineDetails objDatawithSP = _AnchorCompanyRepository.GetRuleofEngineData(CompanyID);
                return Json(objDatawithSP);
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }
            
        }
        #endregion

        #region Export to excel get wait for action by Invoice
      
        public IActionResult ExportInvoice(string Bucket, string TotalInvoiceAmt, string VendorId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                Int32 VendorID = Convert.ToInt32(VendorId);
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<VendorBucketAwaitedInvViewModel> lstAwaitedInvVendorsView = _AnchorCompanyRepository.GetAwaitedInvoiceApprovalView(CompanyID, VendorID, sortBy, pageSize, skip, Bucket, TotalInvoiceAmt);

                string csv = ExportInvoiceList(lstAwaitedInvVendorsView);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "AnchorInvoice.csv");
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
       
        public string ExportInvoiceList(IEnumerable<VendorBucketAwaitedInvViewModel> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord");

        }

        #endregion


        #region Export to excel get wait for action by Buckets Wise Invoice

        public IActionResult ExportBucketsWiseInvoice(Int64 BucketID, string InvoiceID, string POID, string VendorId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                Int32 VendorID = Convert.ToInt32(VendorId);
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<VendorBucketInvoiceViewModel> lstBucketInvVendorsView = _AnchorCompanyRepository.GetBucketWiseInvoiceView(CompanyID, VendorID, BucketID, sortBy, pageSize, skip, InvoiceID, POID);

                string csv = ExportBucketsWiseInvoiceList(lstBucketInvVendorsView);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "AnchorBucketsInvoice.csv");
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

        public string ExportBucketsWiseInvoiceList(IEnumerable<VendorBucketInvoiceViewModel> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord");

        }

        #endregion

        [HttpPost]
        public JsonResult UpdateUTRDetails(InvoiceUTRDetails invoiceUTRDetails)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Result = 0;

            try
            {
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                //disbursementData.CreatedBy = UserId;
                // disbursementData.UpdatedBy = UserId;
                Result = _AnchorCompanyRepository.UpdateUTRDetails(invoiceUTRDetails);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }
            return Json(Result); ;
        }

        #region Get Anchor Company Upcoming Invoice Payable Graph Details
        /// <summary>
        /// Get Anchor Company Upcoming Invoice Payable Graph Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUpcomingPayableData(int DataTypeId,int vendorId = 0)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Graph details", UserID, 0);
                Int64? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<UpcomingPayableGraphModel> lstGraph = _AnchorCompanyRepository.GetUpcomingPayableGraphData(AnchorCompId, vendorId, DataTypeId);
                return Json(lstGraph.Select(x=>new { label=x.PaymentDate.ToString("dd MMM yyyy"),value=x.Amount}));
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

        #region Get Vendors For Anchors
        /// <summary>
        /// Get Vendors For Anchors
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetVendorsForAnchor(int DataTypeId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Graph details", UserID, 0);
                Int64? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<VendorDropdownModel> lstVendors = _AnchorCompanyRepository.GetVendorsForAnchor(AnchorCompId, DataTypeId);
                return Json(lstVendors);
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

    }


}