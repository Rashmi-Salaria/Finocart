using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Model;
using Finocart.Services;
using Finocart.Web.Views.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static Finocart.Web.Controllers.BaseController;

namespace Finocart.Web.Controllers
{

    
    public class VendorDashboardController : Controller
    {
        #region Interface declaration

        private readonly ICompany _companyRepository;
        #region Interface declaration

        /// <summary>
        /// Invoice Data Repository
        /// </summary>

        private readonly IInvoice _invoiceRepository;

        /// <summary>
        /// Lookup data Repository
        /// </summary>
        private readonly ILookupDetails _lookUpRepository;

        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Common Repository
        /// </summary>
        private readonly ICommon _CommonRepository;
        /// <summary>
        ///Anchor Company Repository
        /// </summary>
        private readonly IAnchorCompany _AnchorCompanyRepository;

        private readonly IVendor _VendorRepository;


        #endregion
        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        #endregion
        public VendorDashboardController(ILookupDetails lookupDetailsRepository, ICommon CommonRepository, IConfiguration ConfigurationRepository, IAnchorCompany anchorCompanyRepository, ICompany CompanyRepository,IInvoice invoiceRepository,IVendor VendorRepository)
        {
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
            _configuration = ConfigurationRepository;
            _AnchorCompanyRepository = anchorCompanyRepository;
            _companyRepository = CompanyRepository;
            _invoiceRepository = invoiceRepository;
            _VendorRepository = VendorRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Invoice await apporval view
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult InvoicesAwaitedApprovals()
        {
            
            return View("~/Views/Vendor/AwaitedInvoicesApprovals.cshtml");
        }

        /// <summary>
        /// Get awaited invoice list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getAwaitedInvoiceList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Awaited Approval", "Awaited Invoice List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var InvoiveAppTotlAmnt = Request.Form["columns[4][search][value]"].FirstOrDefault();
               

                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(VendorId, sortBy, pageSize, skip, "", CompanyName, InvoiveAppTotlAmnt, "DiscInvoicesStatus", 3);
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
        /// Export discount anchor list
        /// by vendor id view
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
                Int32? VendorId = HttpContext.Session.GetInt32("UserID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(VendorId, sortBy, pageSize, skip, "", CompanyName, TotalInvoiceAmt, "", 2);

                string csv = ExportAnchorCompDatastr(lstAnchorCompany);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "AwaitedInvoiceApproval.csv");
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
        /// Export anchor data 
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportAnchorCompDatastr(IEnumerable<AnchorCompListingModel> objData)
        {
            try
            {
                return CommonService.ListToCSV(objData, "Contact_Name,Contact_mobile,Contact_email,ApprovedInv,RejectedInv,PendingInv,Location,TotalRecord,FilteredRecord,PendingDiscInv");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get await Invoice approval view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetviewAvaitInvoiceApproval(string id, string AnchorCompanyName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var data = IsBase64Encoded(id);
                var anchorCompName = IsBase64Encoded(AnchorCompanyName);
                if (data == true && anchorCompName == true)
                    //if (data == true)
                {
                    var Res = _CommonRepository.AuditTrailLog("Awaited Approval", "View Avait Invoice Approval", UserID,0);
                    var result = Convert.FromBase64String(id);
                    var resultAnchotCompName = Convert.FromBase64String(AnchorCompanyName);
                    int IDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                    string AnchorCompNames = Encoding.UTF8.GetString(resultAnchotCompName);
                    TempData["ID"] = IDs;
                    TempData["AnchorCompNames"] = AnchorCompNames;
                }
                else
                {
                    return RedirectToAction("ErrorPage", "Common");
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
            return View("~/Views/Vendor/AwaitedInvoicesApprovalsView.cshtml");
        }
          

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
        /// Get available for funding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetViewAvailableforfunding(string fundid)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Available Funding Today", "Available funding Today View List", UserID, 0);
                Int64? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int CompanyId = Convert.ToInt32(fundid);
                var AnchorCompanyName = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var TotalInvoiceAmmount = Request.Form["columns[5][search][value]"].FirstOrDefault();
                AwaitInvoiceApprovalModel objInvoiceModel = new AwaitInvoiceApprovalModel();
                objInvoiceModel.InvStatus = "Eligible for Discounting";
                IEnumerable<AwaitInvoiceApprovalModel> lstInvoice = _invoiceRepository.ViewAwaitInvoiceApproval(sortBy, pageSize, skip, AnchorCompanyName, TotalInvoiceAmmount, "VDashboardScreen", CompanyId, VendorId);
               
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
        /// Export available for funding view
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
                var Res = _CommonRepository.AuditTrailLog("Available Funding Today", "Available Funding Today View List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;


                IEnumerable<AwaitInvoiceApprovalModel> lstInvoice = _invoiceRepository.ViewAwaitInvoiceApproval(sortBy, pageSize, skip, CompanyName, TotalInvoiceAmt, "VDashboardScreen", CompanyId, VendorId);

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
        /// Export available for funding data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportViewgetAvailable(IEnumerable<AwaitInvoiceApprovalModel> objData)
        {
            try
            {
                return CommonService.ListToCSV(objData, "InvoiceID,VendorName,Discount,Days,RejectionReason,ApprovedAmt,TotalRecord,FilteredRecord,InvoiceApprovePayDays,InvoiceApprovalDate");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get invoice journey history view
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

        /// <summary>
        /// Get dashboard list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDashboardlist()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Int32? VendorId = HttpContext.Session.GetInt32("UserID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var InvoiveAppTotlAmnt = Request.Form["columns[4][search][value]"].FirstOrDefault();


                IEnumerable<DashboardListModel> lstDashboard = _AnchorCompanyRepository.GetDashboardList(VendorId, sortBy, pageSize, skip, "", CompanyName, InvoiveAppTotlAmnt, "ApproveTotalInvoice", 3);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;

                if (lstDashboard != null && lstDashboard.Count() != 0)
                {
                    recordsFiltered = lstDashboard.ElementAt(0).FilteredRecord;
                    recordsTotal = lstDashboard.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstDashboard });
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

        #region Get Anchor Company Upcoming Invoice Payable Graph Details
        /// <summary>
        /// Get Anchor Company Upcoming Invoice Payable Graph Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUpcomingPayableData(int DataTypeId, int anchorCompId = 0)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Graph details", UserID, 0);
                Int64? VendorId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<UpcomingPayableGraphModel> lstGraph = _VendorRepository.GetReceivableReceivedGraphData(anchorCompId, VendorId, DataTypeId);
                return Json(lstGraph.Select(x => new { label = x.PaymentDate.ToString("dd MMM yyyy"), value = x.Amount }));
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
        public JsonResult GetAnchorCompanyForVendor(int DataTypeId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Graph details", UserID, 0);
                Int64? VendorId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<AnchorCompanyDropdownModel> lstAnchors= _VendorRepository.GetAnchorCompanyForVendor(VendorId, DataTypeId);
                return Json(lstAnchors);
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
