using Finocart.Interface;
using Finocart.Model;
using Finocart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Finocart.CustomModel;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Finocart.Web.Views.Common;

namespace Finocart.Web.Controllers
{

    
    public class VendorController : BaseController
    {
        #region Interface declaration

        /// <summary>
        /// Lookup data Repository
        /// </summary>
        private readonly ILookupDetails _lookUpRepository;
        /// <summary>
        /// Common Repository
        /// </summary>
        private readonly ICommon _CommonRepository;
        /// <summary>
        ///Vendor Repository
        /// </summary>
        private readonly IVendor _VendorRepository;
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configurationRepository;
        /// <summary>
        ///Anchor Company Repository
        /// </summary>
        private readonly IAnchorCompany _AnchorCompanyRepository;
        /// <summary>
        /// Invoice Repository
        /// </summary>
        private readonly IInvoice _invoiceRepository;

        /// <summary>
        /// User data Repository
        /// </summary>
        //private readonly IUser _Userepository;
        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="UserRepository"></param>
        public VendorController(ILookupDetails lookupDetailsRepository, ICommon CommonRepository, IVendor vendorRepository, IConfiguration configurationRepository, IInvoice invoiceRepository, IAnchorCompany AnchorCompanyRepository)
        {
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
            _VendorRepository = vendorRepository;
            _configurationRepository = configurationRepository;
            _AnchorCompanyRepository = AnchorCompanyRepository;
            _invoiceRepository = invoiceRepository;
        }


        #endregion
        /// <summary>
        /// Vendor dashboard view
        /// </summary>
        /// <returns></returns>
        public IActionResult VendorDashboard()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Select Invoice", "Eligibal Invoice ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                ViewBag.MaxAvailableAmount = _invoiceRepository.GetMaxAvailableAmount(VendorId);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return View("Dashboard");
          

        }

        /// <summary>
        /// Vendor dashboard main view
        /// </summary>
        /// <returns></returns>
        [JWTAuthorize("Account","UserLogin")]
        [CustomFilter]
        public IActionResult VendorDashboardMain()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Vendor Dashbord main", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                string amount= _invoiceRepository.GetMaxAvailableAmount(VendorId);
                // ViewBag.MaxAvailableAmount = _invoiceRepository.GetMaxAvailableAmount(VendorId);

                ViewBag.MaxAvailableAmount = Math.Round(Convert.ToDecimal(amount), 0).ToString("N0");
                var start = "0";
                var length = "10";
                //Get Sort columns value
                var sortBy = "CompanyID asc";
                var receieablesortBy = "anchorID asc";
                var sortByawait = "CompanyName asc";
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var Companyname = "";
                var Approvedamt = "";

                IEnumerable<AvailableFundCompModel> lstPO = _VendorRepository.getAvailableforfundingList(sortBy, pageSize, skip, Companyname, Approvedamt, "Eligible", VendorId);
                 Res = _CommonRepository.AuditTrailLog("Home", "Vendor Dashbord main Top Anchor Comapny", UserID, 0);
                decimal? sumAvailableFundComp = 0;
                for (int i = 0; i < lstPO.Count(); i++)
                {
                    sumAvailableFundComp = Sum(sumAvailableFundComp, lstPO.ElementAt(i).ApprovedAmt);

                }
                String temp0 = String.Format("{0:C0}", sumAvailableFundComp);
                temp0 = temp0.Replace("$", "");
                if (Convert.ToString(sumAvailableFundComp).Contains("."))
                {
                    string[] values0 = Convert.ToString(sumAvailableFundComp).Split('.');
                  
                    TempData["AVAILABLEFORFUNDINGTODAY"] = temp0;
                }
                else
                {
                    TempData["AVAILABLEFORFUNDINGTODAY"] = temp0;
                }
                IEnumerable<DiscountOfferedInvModel> lstPO1 = _VendorRepository.getInvoicetodaysList(sortBy, pageSize, skip, Companyname, Approvedamt, VendorId);
                Res = _CommonRepository.AuditTrailLog("Home", "Vendor Dashbord main Amount Approve today", UserID, 0);
                decimal? SumAVAILABLEFORFUNDINGTODAY = 0;
                for (int i = 0; i < lstPO1.Count(); i++)
                {
                    SumAVAILABLEFORFUNDINGTODAY = Sum(SumAVAILABLEFORFUNDINGTODAY, lstPO1.ElementAt(i).ApprovedAmt);

                }
                String temp2 = String.Format("{0:C0}", SumAVAILABLEFORFUNDINGTODAY);
                temp2 = temp2.Replace("$", "");
                if (Convert.ToString(SumAVAILABLEFORFUNDINGTODAY).Contains("."))
                {
                    string[] values1 = Convert.ToString(SumAVAILABLEFORFUNDINGTODAY).Split('.');
                   
                    TempData["AMOUNTAPPROVEDTODAYININR"] = temp2;
                }
                else
                {
                    TempData["AMOUNTAPPROVEDTODAYININR"] = temp2;
                }

                IEnumerable<AnchorCompListingModel> lstawaitforapproval = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(VendorId, sortByawait, pageSize, skip, "", Companyname, Approvedamt, "DiscInvoicesStatus", 3);
                Res = _CommonRepository.AuditTrailLog("Home", "Vendor Dashbord main Awaited Approval ", UserID, 0);
                decimal? SumAWAITEDAPPROVALININR = 0;
                for (int i = 0; i < lstawaitforapproval.Count(); i++)
                {
                    SumAWAITEDAPPROVALININR = Sum(SumAWAITEDAPPROVALININR, lstawaitforapproval.ElementAt(i).AwaitTotalInvoiceAppAmount);

                }

                String temp1 = String.Format("{0:C0}", SumAWAITEDAPPROVALININR);
                temp1 = temp1.Replace("$", "");
                if (Convert.ToString(SumAWAITEDAPPROVALININR).Contains("."))
                {
                    string[] values = Convert.ToString(SumAWAITEDAPPROVALININR).Split('.');
                    
                    TempData["AWAITEDAPPROVALININR"] = temp1;
                }
                else
                {
                    TempData["AWAITEDAPPROVALININR"] = temp1;
                }


                IEnumerable<RecievableDue> receieabledue = _VendorRepository.GetRecieableduestoday(VendorId, receieablesortBy, pageSize, skip, 0, Companyname, Approvedamt, "Eligible", 2);
                Res = _CommonRepository.AuditTrailLog("Home", "Vendor Dashbord main Reciveable Today  ", UserID, 0);
                decimal? SumRECEIVABLESDUETODAYININR = 0;
                for (int i = 0; i < receieabledue.Count(); i++)
                {
                    SumRECEIVABLESDUETODAYININR = Sum(SumRECEIVABLESDUETODAYININR, receieabledue.ElementAt(i).AmountReceiabletoday);

                }

                
                String temp3 = String.Format("{0:C0}", SumRECEIVABLESDUETODAYININR);
                temp3 = temp3.Replace("$", "");
                if (temp3.Contains("."))
                {
                    string[] values2 = Convert.ToString(SumRECEIVABLESDUETODAYININR).Split('.');
                   
                    TempData["RECEIVABLESDUETODAYININR"] = temp3;
                }
                else
                {
                    TempData["RECEIVABLESDUETODAYININR"] = temp3;
                }

                var lstHolidays = _VendorRepository.GetHolidayDates();
                if (lstHolidays.Count() > 0)
                {
                    ViewBag.Holidays = string.Join(',',lstHolidays.Select(x => Convert.ToDateTime(x.HolidayDates).ToString("MM/dd/yyyy")).ToArray());
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
            return View("DashboardMain");

        }

        /// <summary>
        /// Getting sum of 2 numbers
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static decimal? Sum(decimal? num1, decimal? num2)
        {
            decimal? total;
            total = num1 + num2;
            return total;
        }

        /// <summary>
        /// Automatic funding view
        /// </summary>
        /// <returns></returns>
        public IActionResult AutomaticFunding()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            AutoFunding ObjAutoFunding = new AutoFunding();
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Automatic Funding", "Automatic Funding Page", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                ViewBag.MaxAvailableAmount = _invoiceRepository.GetMaxAvailableAmount(VendorId);

                IEnumerable<AutoFunding> fundings = _VendorRepository.GetAutoFundings(VendorId);
                ViewBag.DiscuntRate = fundings.FirstOrDefault();
               
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return View("AutomaticFunding", ObjAutoFunding);

        }

        /// <summary>
        /// Getting anchor company list
        /// by vendor id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAnchorCompListByVendorID()
       {

            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Top Anchor Company List Dashborad", UserID, 0);
                Int32? CompanyId = HttpContext.Session.GetInt32("CompanyID");
             
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var TotalInvoiceAmt = Request.Form["columns[6][search][value]"].FirstOrDefault();

                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(CompanyId, sortBy, pageSize, skip, "", CompanyName, TotalInvoiceAmt, "Eligible", 2);
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
        /// Export anchor company list by vendor id
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <returns></returns>
        public IActionResult ExportAnchorCompListByVendorID(string CompanyName, string TotalInvoiceAmt)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Anchor Company", "Export CSV File  ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                CompanyName = "";
                TotalInvoiceAmt = "";
                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(VendorId, sortBy, pageSize, skip, "", CompanyName, TotalInvoiceAmt, "Eligible", 2);

                string csv = ExportAnchorCompListDatastr(lstAnchorCompany);

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

        /// <summary>
        /// Exporting anchor company list data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportAnchorCompListDatastr(IEnumerable<AnchorCompListingModel> objData)
        {
            return CommonService.ListToCSV(objData, "ApprovedInv,RejectedInv,PendingInv,Location,TotalRecord,FilteredRecord");
        }


        /// <summary>
        /// Getting Discount anchor by vendor
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDiscAnchorCompListByVendorID()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                var Res = _CommonRepository.AuditTrailLog("select Inovice", "Discount Anchor Company List", UserID, 0);
                Int32? CompanyId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var TotalInvoiceAmt = Request.Form["columns[4][search][value]"].FirstOrDefault();

                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(CompanyId, sortBy, pageSize, skip, "", CompanyName, TotalInvoiceAmt, "Discount", 2);
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
        /// Export disc anchor by vendor view
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <returns></returns>
        public IActionResult ExportDiscAnchorCompListByVendorID(string CompanyName, string TotalInvoiceAmt)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                var Res = _CommonRepository.AuditTrailLog("select Invoice", "Discount Invoice Export CSV List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(VendorId, sortBy, pageSize, skip, "", CompanyName, TotalInvoiceAmt, "Discount", 2);

                string csv = ExportDiscAnchorCompListDatastr(lstAnchorCompany);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "DiscountAnchorCompanyByVendor.csv");
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
        /// Export data of discount anchor 
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportDiscAnchorCompListDatastr(IEnumerable<AnchorCompListingModel> objData)
        {
            return CommonService.ListToCSV(objData, "ApprovedInv,RejectedInv,PendingInv,Location,TotalRecord,FilteredRecord");
        }

        #region Get view to upload documents
        /// <summary>
        /// Upload Documents
        /// </summary>
        /// <returns></returns>
        public IActionResult UploadVendorDocuments()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("Documenttype");
                return View("UploadVendorDocuments", lookupDetails);
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

        #region Upload File Data to Server
        /// <summary>
        /// Upload File Data to Server
        /// </summary>
        /// <param name="AnchorDocument"></param>
        /// <param name="iDocumentTypeID"></param>
        /// <returns></returns>
        public async Task<JsonResult> UploadFileDatatoServer(IFormFile VendorDocument, int iDocumentTypeID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                string ReturnMessage = "";
                bool bResult = false;
                string documentFolderPath = _configurationRepository["VendorDocumentsFolder"];
                string exportFileName = "Vendor_PAN_" + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                bResult = _CommonRepository.UploadFileToServer(VendorDocument, documentFolderPath, exportFileName);

                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                Int32? iVendorID = UserId;
                if (bResult == true)
                {
                    ReturnMessage = "File uploaded successfully";
                    _VendorRepository.InsertDocumentDet(VendorDocument, iVendorID, iDocumentTypeID, exportFileName + Path.GetExtension(VendorDocument.FileName), UserId);
                }
                else
                {
                    ReturnMessage = "There is some error in fileupload";
                }

                var result = new { bResult = bResult, ReturnMessage = ReturnMessage };
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
        #endregion

        #region Get vendor document listing
        /// <summary>
        /// Get users' listing
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetVendorDocList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                var draw = Request.Form["draw"].FirstOrDefault();
                int? VendorID = UserId;
                IEnumerable<DocumentModel> lstVendorDoc = _VendorRepository.GetVendorDocList(VendorID);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstVendorDoc });
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
        /// Get receivable due view
        /// </summary>
        /// <returns></returns>
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public ActionResult GetReceivableDue()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Reciveable", "Reciveable  Due Today  ", UserID, 0);
                return View("ReceivableDue");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Getting receivable due amount
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRecieableDueAmount()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Receivables Due Today", "Receivables Due Today List ", UserID, 0);

                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
               
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var Anchorname = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var TotalInvoiceAmount = Request.Form["columns[4][search][value]"].FirstOrDefault();


                IEnumerable<RecievableDue> receieabledue = _VendorRepository.GetRecieableduestoday(VendorId, sortBy, pageSize, skip, 0, Anchorname, TotalInvoiceAmount, "Eligible", 2);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (receieabledue != null && receieabledue.Count() != 0)
                {
                    recordsFiltered = receieabledue.ElementAt(0).FilteredRecord;
                    recordsTotal = receieabledue.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = receieabledue });

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
         /// Export Recievable Due Amount
         /// </summary>
         /// <param name="Anchorname"></param>
         /// <param name="TotalInvoiceAmount"></param>
         /// <returns></returns>
        public IActionResult ExportGetRecieableDueAmount(string Anchorname, string TotalInvoiceAmount)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Reciveable Due Amount", "Export CSV  Export Get Recieable DueAmount ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<RecievableDue> receieabledue = _VendorRepository.GetRecieableduestoday(VendorId, sortBy, pageSize, skip, 0, Anchorname, TotalInvoiceAmount, "Eligible", 2);

                string csv = ExportGetRecieableDueAmountDatastr(receieabledue);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "RecievableDueAmount.csv");
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
        /// Export Recievable Due Amount List Data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportGetRecieableDueAmountDatastr(IEnumerable<RecievableDue> objData)
        {
            return CommonService.ListToCSV(objData, "TotalRecord,FilteredRecord");
        }

        /// <summary>
        /// Get recievable today of anchor
        /// </summary>
        /// <param name="anchorID"></param>
        /// <returns></returns>
        public ActionResult GetIndividualReceiveableDueToday(string anchorID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var data = IsBase64Encoded(anchorID);
            if (data == true)
            {
               
                var Res = _CommonRepository.AuditTrailLog("Get Reciveable Today", "Reciveable View List ", UserID, 0);
                var result = Convert.FromBase64String(anchorID);
                int companyID = Convert.ToInt16(Encoding.UTF8.GetString(result));
                TempData["id"] = companyID.ToString();
                ViewBag.VendorId = HttpContext.Session.GetInt32("CompanyID");
                ViewBag.VendorName = HttpContext.Session.GetString("UserName");             
                return View("tIndividualReceiveableDueToday");
               
            }
            else
            {
                return View("~/Views/User/PageNotFound.cshtml");
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

        /// <summary>
        /// Getting amount to receive
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTodayRecAmount()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var id = TempData["id"];
                TempData["id"] = id;
                var IDD = id;
                var Res = _CommonRepository.AuditTrailLog("Recievables Due Today", "Recieveable tody List", UserID, 0);
                Int64? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var Anchorname = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var TotalInvoiceAmount = Request.Form["columns[4][search][value]"].FirstOrDefault();
                IEnumerable<ReceivabledueIndividual> receivabledueIndividuals = _VendorRepository.GetReceivabledueIndividuals(VendorId, Convert.ToInt32(IDD), skip, pageSize, sortBy, "", "Eligible", 2);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (receivabledueIndividuals != null && receivabledueIndividuals.Count() != 0)
                {
                    recordsFiltered = receivabledueIndividuals.ElementAt(0).FilteredRecord;
                    recordsTotal = receivabledueIndividuals.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = receivabledueIndividuals });
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
        #region Remove Anchor Comppany Document
        /// <summary>
        /// Remove anchor company document
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteVendorDoc(Int64 ID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            VendorDocument vendorDocument = _VendorRepository.GetFileNameById(ID);

            bool bResult = false;
            string documentFolderPath = _configurationRepository["VendorDocumentsFolder"];
            string exportFileName = vendorDocument.LocalFolderFileName;
            try
            {
                bResult = _CommonRepository.DeleteFile(documentFolderPath, exportFileName);
                if (bResult == true)
                {
                    _VendorRepository.DeleteVendorDocRecord(vendorDocument);
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
            return Json(bResult);
        }
        #endregion

        #region
        /// <summary>
        /// Getting eligible invoice view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="PageName"></param>
        /// <returns></returns>
        public ActionResult GetEligibleInvoicesView(int id, string PageName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {


                var Res = _CommonRepository.AuditTrailLog("Select Invoice", "Eligible invoice View ", UserID, id);
                ViewBag.VendorId = HttpContext.Session.GetInt32("UserID");
                ViewBag.VendorName = HttpContext.Session.GetString("UserName");
                InvoiceModel objInvoiceModel = new InvoiceModel();
                objInvoiceModel.CompanyId = id;
                IEnumerable<ModuleMaster> objData = _invoiceRepository.getModuleStatus();
                objData = objData.Where(x => x.ModuleName == "Invoice");
                IEnumerable<SearchHistory> lstLastSearchParams = _invoiceRepository.GetSearchHistories();
                IEnumerable<LookupDetails> lstlookupDetails = _lookUpRepository.getLookupDetailByKey("Last Search Limit");
                TempData["MaxSearchCount"] = lstlookupDetails.ElementAt(0).LookupValue;
                
                Tuple<IEnumerable<ModuleMaster>, IEnumerable<SearchHistory>, InvoiceModel> objUserModelData =
                        new Tuple<IEnumerable<ModuleMaster>, IEnumerable<SearchHistory>, InvoiceModel>(objData, lstLastSearchParams, objInvoiceModel);
                if (PageName.ToLower().Contains("eligible"))
                {
                    return PartialView("EligibleInvDashboard", objUserModelData);
                    
                }
                else
                {
                    return PartialView("DiscountOfferedInvDashboard", objUserModelData);

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
        /// GetEligibleInvoicesList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetEligibleInvoicesList(InvoiceModel objInvoiceModel)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Select Invoice", "Eligible invoice List ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var AnchorCompanyName = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var InvoiceStatus = Request.Form["columns[8][search][value]"].FirstOrDefault();

                IEnumerable<InvoiceModel> lstInvoice = _invoiceRepository.getInvoiceList(sortBy, pageSize, skip, AnchorCompanyName, InvoiceStatus, "VDashboardScreen", objInvoiceModel.CompanyId, VendorId);
               
                decimal? ApprovedInvAmount = 0;
                foreach (var item in lstInvoice)
                {
                    item.ApprovedAmt = item.ApprovedAmt == null ? 0 : item.ApprovedAmt;
                    ApprovedInvAmount += item.ApprovedAmt;
                }
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstInvoice != null && lstInvoice.Count() != 0)
                {
                    recordsFiltered = lstInvoice.ElementAt(0).FilteredRecord;
                    recordsTotal = lstInvoice.ElementAt(0).TotalRecord;
                }
                ViewBag.ApprovedInvAmount = ApprovedInvAmount;
                ViewBag.TotalApprovedInvoices = lstInvoice.Count();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstInvoice });
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

        #region Export Invoice Data To CSV
        /// <summary>
        /// Export Invoice Data List To CSV
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportRecordOfInvoice(string AnchorCompanyName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Select Invoice", "Eligible invoice View ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "PONumber asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<InvoiceModel> lstInvoice = _invoiceRepository.getInvoiceList(sortBy, pageSize, skip, AnchorCompanyName, "", "Discount is null", 0, VendorId);

                string csv = ExportDatastr(lstInvoice);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "EligibleInvoice.csv");
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
        /// Exporting data of invoice
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportDatastr(IEnumerable<InvoiceModel> objData)
        {
            return CommonService.ListToCSV(objData, "InvoiceID,VendorName,TotalRecord,FilteredRecord,CompanyID");
        }
        #endregion
        /// <summary>
        /// Export to Excel of list of eligible invoices
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="AnchorCompanyName"></param>
        /// <param name="InvoiceStatus"></param>
        /// <returns></returns>
        public IActionResult ExportEligibleInvoice(string CompanyID, string AnchorCompanyName, string InvoiceStatus)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Select Invoice", "Export CSV Eligible invoice List ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "PONumber asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<InvoiceModel> lstInvoice = _invoiceRepository.getInvoiceList(sortBy, pageSize, skip, AnchorCompanyName, InvoiceStatus, "VDashboardScreen", Convert.ToInt16(CompanyID), VendorId);

                string csv = ExportEligibleDatastr(lstInvoice);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "EligibleInvoice.csv");
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
        /// listing of invoices eligible
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>

        public string ExportEligibleDatastr(IEnumerable<InvoiceModel> objData)
        {
            return CommonService.ListToCSV(objData, "Discount,Days,RejectionReason,InvStatus,DiscountAmt,DisbursementAmt,InvoiceApprovePayDays,InvoiceApprovalDate,TotalRecord,FilteredRecord,CompanyID");
        }

        #region
        /// <summary>
        /// Insert invoice bucket details
        /// </summary>
        /// <param name="bucketInvoiceModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult InsertBucketDetails(BucketInvoiceModel bucketInvoiceModel, string vendorPage)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            int result = 0;
            bucketInvoiceModel.UserId= HttpContext.Session.GetInt32("UserID");
            var UserName= HttpContext.Session.GetString("UserName");
            bucketInvoiceModel.BucketName = HttpContext.Session.GetString("Companyname") + bucketInvoiceModel.BucketName;
            Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
            try
            {
                if (vendorPage == "finoassist")
                {
                    string[] _date = bucketInvoiceModel.ValidToDate.Split('/');
                    bucketInvoiceModel.ValidToDate = _date[1] + "/" + _date[0] + "/" + _date[2];
                    result = _VendorRepository.InsertFinoInvoiceBucketDiscountDet(bucketInvoiceModel);
                }
                else
                {
                    result = _VendorRepository.InsertInvoiceBucketDiscountDet(bucketInvoiceModel);
                     _VendorRepository.SendNotification(bucketInvoiceModel, UserID, VendorId);
                }

                //var iCount = 0;
                //string emailToAddress = "";
                //List<string> lstMultipleemailToAddress = new List<string>();
                //List<string> lstMultipleDistincctemailToAddress = new List<string>();
                //string subject = "";
                //string body = "";
                //string sTextBody = "";
                //var Role = "";
                //string Company_ContactName = "";
                //if (result >= 1)
                //{
                //    var EmailIdList = _VendorRepository.GetEmailByInvoiceId(bucketInvoiceModel.InvoiceIDStr.TrimEnd(','));
                //    User objuser = _VendorRepository.GetEmailIdByVendorId(bucketInvoiceModel.UserId);
                //    foreach (var EmailIditem in EmailIdList)
                //    {
                //        lstMultipleemailToAddress.Add(EmailIditem.EmailId + "_Anchor");

                //    }
                //    lstMultipleemailToAddress.Add(objuser.Email + "_Vendor");
                //    lstMultipleDistincctemailToAddress = lstMultipleemailToAddress.Distinct().ToList();
                //    string startupPath = Environment.CurrentDirectory;
                //    IEnumerable<GetRegisterMailTemplate> lstAwaitedInvVendorsView = _VendorRepository.getRestraterMailTemplate();
                //    foreach (var emailid in lstMultipleDistincctemailToAddress)
                //    {
                //        //foreach (var item in EmailIdList)
                //        //{
                //        //    iCount++;
                //            var CompanyName = _VendorRepository.GetCompany(VendorId);
                //            int idx = emailid.LastIndexOf('_');
                //            Role = emailid.Substring(idx + 1);
                //            //Company_ContactName = item.Company_ContactName;
                //            //WebClient client = new WebClient();
                //            string Template = string.Empty;
                //            IEnumerable<GetBankMailTemplate> lstDiscInv = _VendorRepository.GetDiscInvMailTemplate(Template);
                //            string path = lstDiscInv.ElementAt(0).Template;
                //            string EMAIL_TOKEN_PAYMENT_LINK = "##$$LOGIN_LINK$$##";
                //            string paymentLink = "http://localhost:4670/Account/AdminLogin";///change url
                //            emailToAddress = emailid.Substring(0, idx);
                //            subject = "Discount Acknowledgement";
                //            body = path;
                //            body = body.Replace("@@User@@", UserName);
                //            body = body.Replace("@@CompanyName@@", CompanyName.Company_name);
                //            body = body.Replace("@@Discount@@", bucketInvoiceModel.DiscountPercentage.ToString());
                //            body = body.Replace("@@Amount@@", 200000.ToString());
                //            //body = body.Replace("@@rate@@", setBankAmountLimit.Interest_rate.ToString() + "% For" + setBankAmountLimit.Interest_rate_month.ToString() + " Month");
                //            body = body.Replace("@@ProjectName@@", "Finocart");
                //            body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);

                //            IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                //            _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);
                //        }
                //    }
                //}
                    //var iCount = 0;
                    //string emailToAddress = "";
                    //List<string> lstMultipleemailToAddress = new List<string>();
                    //List<string> lstMultipleDistincctemailToAddress = new List<string>();
                    //string subject = "";
                    //string body = "";
                    //string sTextBody = "";
                    //var Role = "";
                    //string Company_ContactName = "";
                    //if (result >= 1)
                    //{

                    //    var EmailIdList = _VendorRepository.GetEmailByInvoiceId(bucketInvoiceModel.InvoiceIDStr.TrimEnd(','));
                    //    User objuser = _VendorRepository.GetEmailIdByVendorId(bucketInvoiceModel.UserId);
                    //    foreach (var EmailIditem in EmailIdList)
                    //    {
                    //        lstMultipleemailToAddress.Add(EmailIditem.EmailId + "_Anchor");

                    //    }
                    //    lstMultipleemailToAddress.Add(objuser.Email + "_Vendor");
                    //    lstMultipleDistincctemailToAddress = lstMultipleemailToAddress.Distinct().ToList();
                    //    string startupPath = Environment.CurrentDirectory;
                    //    IEnumerable<GetRegisterMailTemplate> lstAwaitedInvVendorsView = _VendorRepository.getRestraterMailTemplate();
                    //    string path = "~/Views/Template/BucketAcknowlwdgement/BucketAcknowledgement.cshtml";
                    //    body = System.IO.File.ReadAllText(path);
                    //    foreach (var emailid in lstMultipleDistincctemailToAddress)
                    //    {
                    //        foreach (var item in EmailIdList)
                    //        {
                    //            iCount++;

                    //            int idx = emailid.LastIndexOf('_');
                    //            Role = emailid.Substring(idx + 1);
                    //            emailToAddress = emailid.Substring(0, idx);
                    //            subject = "Discount Acknowledgement";
                    //            Company_ContactName = item.Company_ContactName;
                    //            WebClient client = new WebClient();

                    //            sTextBody += "<tr><td>" + iCount.ToString() + "</td>" +
                    //                "<td>" + item.InvoiceNo + "</td><td>" + item.BucketID.ToString() + "</td><td>" + item.BucketName + "</td>" +
                    //                "<td>" + item.DiscountPercentage.ToString() + "</td><td>" + item.ValidToDate + "</td><td> Pending at anchor company </td></tr>";
                    //        }
                    //        if (Role.ToLower() == "vendor")
                    //        {
                    //            body = body.Replace("@@User@@", UserName);
                    //        }
                    //        else if (Role.ToLower() == "anchor")
                    //        {
                    //            body = body.Replace("@@User@@", Company_ContactName);
                    //        }
                    //        body = body.Replace("@@sTextBody@@", sTextBody);

                    //        IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                    //        _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);
                    //    }
                    //}
                    return Json(new { Successresult = result });
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
        /// <summary>
        /// Get Discount Offered Invoice List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDiscountOfferedInvList(OfferedDiscountInvModel objInvoiceModel)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("select Invoice", "Discount Invoice list", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var AnchorCompanyName = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var InvoiceAmount = Request.Form["columns[5][search][value]"].FirstOrDefault();

                IEnumerable<OfferedDiscountInvModel> lstDiscInvoice = _VendorRepository.GetDiscountOfferedInvoiceList(sortBy, pageSize, skip, AnchorCompanyName, InvoiceAmount, "VDashboardScreen", objInvoiceModel.CompanyID, VendorId);
                decimal? ApprovedInvAmount = 0;
                foreach (var item in lstDiscInvoice)
                {
                    item.ApprovedAmt = item.ApprovedAmt == null ? 0 : item.ApprovedAmt;
                    ApprovedInvAmount += item.ApprovedAmt;
                }
                int? recordsFiltered = 0;
                long? recordsTotal = 0;
                if (lstDiscInvoice != null && lstDiscInvoice.Count() != 0)
                {
                    recordsFiltered = lstDiscInvoice.ElementAt(0).FilteredRecord;
                    recordsTotal = lstDiscInvoice.ElementAt(0).TotalRecord;
                }
                ViewBag.ApprovedInvAmount = ApprovedInvAmount;
                ViewBag.TotalApprovedInvoices = lstDiscInvoice.Count();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstDiscInvoice, InvoiceList = PartialView("") });
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

        #region  Get journey of invoice
        /// <summary>
        /// Get Invoice Journey History
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <param name="PageName"></param>
        /// <returns></returns>
        public JsonResult GetInvoiceJourneyHistory(Int64 InvoiceId, string PageName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Available For funding", "View Invoice Journey History", UserID, 0);
                IEnumerable<InvoiceJourneyHistoryModel> lstInvoiceJourneyHistory = _VendorRepository.GetInvoiceJourneyHistory(InvoiceId, PageName);
                return Json(new { data = lstInvoiceJourneyHistory, PageName = PageName });
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

        #region Export Disounted Invoice's Data To CSV
        /// <summary>
        /// Export Invoice Data List To CSV
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportRecordOfDiscInvoice(string AnchorCompanyName, string InvoiceAmount, int CompanyId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Select Invoice", "Export CSV  Discount Invoice List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "PONumber asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<OfferedDiscountInvModel> lstInvoice = _VendorRepository.GetDiscountOfferedInvoiceList(sortBy, pageSize, skip, AnchorCompanyName, InvoiceAmount, "VDashboardScreen", CompanyId, VendorId);

                string csv = ExportDatastr(lstInvoice);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "DiscountOfferedInvoice.csv");
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
        /// Export data of invoice
        /// on which discount offered
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportDatastr(IEnumerable<OfferedDiscountInvModel> objData)
        {
            return CommonService.ListToCSV(objData, "InvoiceNo,VendorName,TotalRecord,FilteredRecord,Days,InvoiceApprovePayDays,InvoiceApprovalDate,CompanyID," +
                "InvoiceIDStr,BucketName,DiscountPercentage,TotalInvoiceCount,TotalInvoiceAmount,AfterDiscountAmount,DiscountAmount,BucketID,BucketStatus,UserId,CompanyID");
        }
        #endregion

        #region Finoassist get invoice by amount
        /// <summary>
        /// Getting invoice by amount
        /// </summary>
        /// <param name="sumAssuredAmount"></param>
        /// <param name="fundingDate"></param>
        /// <param name="discount"></param>
        /// <param name="finassistType"></param>
        /// <param name="anchoCompany"></param>
        /// <returns></returns>
        public JsonResult GetInvoicesByAmount(decimal sumAssuredAmount, string fundingDate, decimal discount, string finassistType, string anchoCompany)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("finassist", "Invoice by Amount List", UserID, 0);
                IEnumerable<InvoiceModel> lstInvoiceByAmount;
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");


                string[] _date = fundingDate.Split('/');
                fundingDate = _date[1] + "/" + _date[0] + "/" + _date[2];
                if (finassistType == "ByAnchorComp")
                {
                    lstInvoiceByAmount = _VendorRepository.GetInvoicesByAnchor(sumAssuredAmount, VendorId, 0, 1000000, "PONumber desc",fundingDate, discount, anchoCompany);
                }
                else
                {
                    lstInvoiceByAmount = _VendorRepository.GetInvoicesByAmount(sumAssuredAmount, VendorId, 0, 1000000, "PONumber desc", fundingDate, discount);
                }
                return Json(new { data = lstInvoiceByAmount });
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
        /// View selected invoice
        /// </summary>
        /// <param name="sumAssuredAmount"></param>
        /// <param name="fundingDate"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetInvoicesListByAmount(decimal sumAssuredAmount, string fundingDate, decimal discount, string finassistType, string anchoCompany)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("finassist", "Invoice by Amount List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var AnchorCompanyName = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var InvoiceStatus = Request.Form["columns[8][search][value]"].FirstOrDefault();

                IEnumerable<InvoiceModel> lstInvoiceByAmount;
                string[] _date = fundingDate.Split('/');
                fundingDate = _date[1] + "/" + _date[0] + "/" + _date[2];
                if (finassistType == "ByAnchorComp")
                {
                    lstInvoiceByAmount = _VendorRepository.GetInvoicesByAnchor(sumAssuredAmount, VendorId, skip, pageSize, sortBy, fundingDate, discount, anchoCompany);
                }
                else
                {
                    lstInvoiceByAmount = _VendorRepository.GetInvoicesByAmount(sumAssuredAmount, VendorId, skip, pageSize, sortBy, fundingDate, discount);
                }
                
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstInvoiceByAmount != null && lstInvoiceByAmount.Count() != 0)
                {
                    recordsFiltered = lstInvoiceByAmount.ElementAt(0).FilteredRecord;
                    recordsTotal = lstInvoiceByAmount.ElementAt(0).TotalRecord;
                }
               
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstInvoiceByAmount });
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

        #region Finoassist Maximum Available Amount By Anchor Company
        /// <summary>
        /// Getting maximum available amount of anchor
        /// </summary>
        /// <param name="anchoCompany"></param>
        /// <returns></returns>
        public JsonResult GetMaximumAvailAmtForAnchComp(string anchoCompany)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", " Max Amount For Anchor Comapny", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var result = _VendorRepository.GetMaxAvailableAmount(VendorId, anchoCompany.TrimEnd(','));
                return Json(new { data = result });
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

        #region Finoassist 
        /// <summary>
        /// Getting min fund payment date
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMinFundPaymentDate()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", " Min Fund Payment date", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var result = _VendorRepository.GetMinFundPaymentDate(VendorId);
                return Json(new { data = result });
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

        #region Finoassist Maximum Available Amount By Anchor Company
        /// <summary>
        /// Getting max available amount
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMaximumAvailAmtByAmount()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", " Max Available For Funding ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var result = _invoiceRepository.GetMaxAvailableAmount(VendorId);
                return Json(new { data = result });
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

        #region Get Eligible Invoices For Offer Discount

        /// <summary>
        /// Getting eligible invoice
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetEligibleInvoices(int CompanyId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            int? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("select invoice", "Eligibale Invoice List", UserID, 0);
                //var Reason = Request.Form["columns[1][search][value]"].FirstOrDefault();
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                //var length = Request.Form["length"].FirstOrDefault();
                //var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                //var start = Request.Form["start"].FirstOrDefault();
                //int pageSize = length != null ? Convert.ToInt32(length) : 0;
                //int skip = start != null ? Convert.ToInt32(start) : 0;
                IEnumerable<InvoiceModel> lstInvoice = _invoiceRepository.getInvoiceList("", 10000000, 0, "", "", "VDashboardScreen", CompanyId, VendorId);
                IEnumerable<HolidayListModel> lstAnchorHolidayList = _AnchorCompanyRepository.GetAnchorHolidayListing("Id asc", 10000000, 0,"", UserID);
                return Json(new { data = lstInvoice, data1 = lstAnchorHolidayList });
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

        #region Get Graph Details
        /// <summary>
        /// Getting graph details
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
                var Res = _CommonRepository.AuditTrailLog("Home", " Graph Details", UserID, 0);
                Int64? VendorId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<GraphDetailsModel> lstGraph = _VendorRepository.GetGraphDetails(month, VendorId);
                return Json(lstGraph);
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

        #region Approved Invoice
        /// <summary>
        /// Invoice list by today date view
        /// </summary>
        /// <returns></returns>
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult InvoiceListBytodaysdate()
        {
            return View("~/Views/Vendor/InvoiceAmmountApproved.cshtml");
        }

        /// <summary>
        /// Getting invoice list by PO id 
        /// for today
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getInvoiceListByPOIdtodayes()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Amount Approved Today", "Invoice List By PO Id Todaye List", UserID, 0);

                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var Companyname = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var Approvedamt = Request.Form["columns[6][search][value]"].FirstOrDefault();

                IEnumerable<DiscountOfferedInvModel> lstPO = _VendorRepository.getInvoicetodaysList(sortBy, pageSize, skip, Companyname, Approvedamt, VendorId);

                int? recordsFiltered = 0;
                long? recordsTotal = 0;
                if (lstPO != null && lstPO.Count() != 0)
                {
                    recordsFiltered = lstPO.ElementAt(0).FilteredRecord;
                    recordsTotal = lstPO.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstPO });
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
        /// Exporting today's invoice by po
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <returns></returns>
        public IActionResult ExportgetInvoiceListByPOIdtodayes(string CompanyName, string TotalInvoiceAmt)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Amount Approve Today ", "Export Invoice List By PO ID Today   ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("UserID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;

                var PurchaseOrderNo = "";
                var VendorName = "";
                //IEnumerable<DiscountOfferedInvModel> lstAnchorCompany = _VendorRepository.getInvoicetodaysList(sortBy, pageSize, skip, PurchaseOrderNo, VendorName, VendorId);
                IEnumerable<DiscountOfferedInvModel> lstAnchorCompany = _VendorRepository.getInvoicetodaysList(sortBy, pageSize, skip, CompanyName, TotalInvoiceAmt, VendorId);
                string csv = Exportgettoday(lstAnchorCompany);

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
        /// Export get today
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string Exportgettoday(IEnumerable<DiscountOfferedInvModel> objData)
        {
            return CommonService.ListToCSV(objData, "InvoiceNo,Days,PaymentDueDate,RejectionReason, InvStatus, InvoiceApprovePayDays,VendorName,ValidToDate,TotalInvoiceCount,TotalInvoiceAmount,BucketStatus,BucketID,UserId,TotalRecord,FilteredRecord,InvoiceApprovalDate,InvoiceIDStr");
        }
        #endregion

        #region Available For Funding
        /// <summary>
        /// Available for funding - view
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult Availableforfunding()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("AVAILABLE FOR FUNDING TODAY", "Available for funding List", UserID, 0);
                int vendorid = 0;
                IEnumerable<AutoFunding> fundings = _VendorRepository.GetAutoFundings(vendorid);
                if (fundings.Count() != 0)
                {
                    ViewBag.DiscuntRate = fundings.FirstOrDefault();
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
            return View("~/Views/Vendor/AvailableForFunding.cshtml");
        }

        /// <summary>
        /// Getting available for funding
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAvailableforfunding()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("AVAILABLE FOR FUNDING TODAY", "Available for funding List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var Companyname = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var Approvedamt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<AvailableFundCompModel> lstPO = _VendorRepository.getAvailableforfundingList(sortBy, pageSize, skip, Companyname, Approvedamt, "Eligible", VendorId);



                int? recordsFiltered = 0;
                long? recordsTotal = 0;
                if (lstPO != null && lstPO.Count() != 0)
                {
                    recordsFiltered = lstPO.ElementAt(0).FilteredRecord;
                    recordsTotal = lstPO.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstPO });
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
        /// Export available for funding
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <returns></returns>
        public IActionResult ExportgetAvailableForFunding(string CompanyName, string TotalInvoiceAmt)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Available For Funding", "Export CSV Available For Funding ", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;

            
            IEnumerable<AvailableFundCompModel> lstPO = _VendorRepository.getAvailableforfundingList(sortBy, pageSize, skip, CompanyName, TotalInvoiceAmt, "Eligible", VendorId);

                string csv = ExportgetAvailable(lstPO);

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
        /// Exporting data of Available
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportgetAvailable(IEnumerable<AvailableFundCompModel> objData)
        {
            return CommonService.ListToCSV(objData, "");
        }

        /// <summary>
        /// Getting available for funding by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetviewAvailableforfunding(string id,string companyName)
        {
            TempData["ID"] = id;
            TempData["CompanyName"] = companyName;
            return View("~/Views/Vendor/Availbleforfundingview.cshtml");
        }

        /// <summary>
        /// View available for funding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetViewAvailableforfunding(string id)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                int Avid = Convert.ToInt32(id);
                var Res = _CommonRepository.AuditTrailLog("Available For Funding", "Available for funding View List  ", UserID, Avid);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int CompanyId = Convert.ToInt32(id);
                var AnchorCompanyName = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var TotalInvoiceAmmount = Request.Form["columns[5][search][value]"].FirstOrDefault();
                InvoiceModel objInvoiceModel = new InvoiceModel();
                objInvoiceModel.InvStatus = "Eligible for Discounting";
                IEnumerable<InvoiceModel> lstInvoice = _invoiceRepository.ViewAvailableforfunding(sortBy, pageSize, skip, AnchorCompanyName, TotalInvoiceAmmount, "VDashboardScreen", CompanyId, VendorId);
              
                decimal? ApprovedInvAmount = 0;
                
                foreach (var item in lstInvoice)
                {
                    item.ApprovedAmt = item.ApprovedAmt == null ? 0 : item.ApprovedAmt;
                    ApprovedInvAmount += item.ApprovedAmt;
                }
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstInvoice != null && lstInvoice.Count() != 0)
                {
                    recordsFiltered = lstInvoice.ElementAt(0).FilteredRecord;
                    recordsTotal = lstInvoice.ElementAt(0).TotalRecord;
                }
                ViewBag.ApprovedInvAmount = ApprovedInvAmount;
                ViewBag.TotalApprovedInvoices = lstInvoice.Count();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstInvoice });
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
        /// Export available for funding
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public IActionResult ExportViewgetAvailableForFunding(string CompanyName, string TotalInvoiceAmt, int CompanyId)
        {     
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Available For Funding", "Export CSV Available For Funding  List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;


                IEnumerable<InvoiceModel> lstInvoice = _invoiceRepository.ViewAvailableforfunding(sortBy, pageSize, skip, CompanyName, TotalInvoiceAmt, "VDashboardScreen", CompanyId, VendorId);

                string csv = ExportViewgetAvailable(lstInvoice);

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
        /// Getting data to export for 
        /// available funding
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportViewgetAvailable(IEnumerable<InvoiceModel> objData)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Available For Funding", "Available View Export ", UserID,0);
            return CommonService.ListToCSV(objData, "InvoiceID,VendorName,Discount,Days,RejectionReason,ApprovedAmt,TotalRecord,FilteredRecord,InvoiceApprovePayDays,InvoiceApprovalDate");
        }
        #endregion



        #region Insert Funding Rate And Date
        #endregion
        /// <summary>
        /// Adding funding
        /// </summary>
        /// <param name="ObjAutoFunding"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddFundingRateAndDate(AutoFunding ObjAutoFunding)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                Int32? CompanyId = HttpContext.Session.GetInt32("CompanyID");
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                
                var Result = _VendorRepository.InsertFundingRateAndDate(ObjAutoFunding);

                return RedirectToAction("VendorDashboardMain", "Vendor");

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

        #region Invoice History
        /// <summary>
        /// Invoice history view
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult InvoiceHistory()
        {
            return View("~/Views/Vendor/InvoiceHistory.cshtml");
        }

        /// <summary>
        /// Getting invoice history
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
                var Res = _CommonRepository.AuditTrailLog("INVOICES STATUS (HISTORY)", "Invoice History List", UserID, 0);

                Int32? VendorID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var AnchorCompanyName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var TotalInvoiceAmmount = Request.Form["columns[7][search][value]"].FirstOrDefault();
                InvoiceModel objInvoiceModel = new InvoiceModel();
                objInvoiceModel.InvStatus = "Eligible for Discounting";
                IEnumerable<InvoiceHistoryModel> lstinvoicehistory = _VendorRepository.InvoiceHistorylist(sortBy, pageSize, skip, AnchorCompanyName, TotalInvoiceAmmount, VendorID);
                //lstInvoice = lstInvoice.Where(f => f.Discount == null);
                decimal? ApprovedInvAmount = 0;
                //lstInvoice[0].InvStatus = "Eligible for Discounting";
                foreach (var item in lstinvoicehistory)
                {
                    item.ApprovedAmt = item.ApprovedAmt == null ? 0 : item.ApprovedAmt;
                    ApprovedInvAmount += item.ApprovedAmt;
                }
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstinvoicehistory != null && lstinvoicehistory.Count() != 0)
                {
                    recordsFiltered = lstinvoicehistory.ElementAt(0).FilteredRecord;
                    recordsTotal = lstinvoicehistory.ElementAt(0).TotalRecord;
                }
                ViewBag.ApprovedInvAmount = ApprovedInvAmount;
                ViewBag.TotalApprovedInvoices = lstinvoicehistory.Count();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstinvoicehistory });
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
        /// Export invoice history view
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="TotalInvoiceAmt"></param>
        /// <returns></returns>
        public IActionResult ExportInvoiceHistory(string CompanyName, string TotalInvoiceAmt)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoice Status (History) ", "Export CSV Invoice History Today", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "AnchorCompName asc";
                int pageSize = 100000;
                int skip = 0;


                IEnumerable<InvoiceHistoryModel> lstinvoicehistory = _VendorRepository.InvoiceHistorylist(sortBy, pageSize, skip, CompanyName, TotalInvoiceAmt, VendorId);

                string csv = Exportexcludeinvoicehistory(lstinvoicehistory);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "InvoiceHistory.csv");
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
        /// Export invoice history data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string Exportexcludeinvoicehistory(IEnumerable<InvoiceHistoryModel> objData)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Invoice Status (History) ", "Export Exclude Invoice History    ", UserID, 0);

                return CommonService.ListToCSV(objData, "");
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

        /// <summary>
        /// Update funding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateFunding(string id, string rate, string date)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {

                var Res = _CommonRepository.AuditTrailLog("Auto Finding Update Confirm", "Update Auto Finding Data", UserID, 0);
                Int32? CompanyId = HttpContext.Session.GetInt32("CompanyID");
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                var Result = _VendorRepository.Updatefunding(id, rate, date);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return RedirectToAction("VendorDashboardMain", "Vendor");
        }


    }
}