using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finocart.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Finocart.Web;
using Finocart.CustomModel;
using Finocart.Services;
using System.IO;
using System.Net.Mail;
using Microsoft.IdentityModel.Protocols;
using System.Net;
using System.Configuration;
using Finocart.Model;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Finocart.Web.Views.Common;
using static Finocart.Web.Controllers.BaseController;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace Finocart.Web.Controllers
{


    public class BankCompanyController : Controller
    {

        #region Interface declaration

        private readonly IBank _companyRepository;
        private readonly IAnchorCompany _AnchorCompanyRepository;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Common Repository
        /// </summary>
        private readonly ICommon _CommonRepository;
        /// <summary>
        /// Lookup data Repository
        /// </summary>
        private readonly ILookupDetails _lookUpRepository;

        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        public BankCompanyController(IBank companyRepository, IAnchorCompany anchorCompanyRepository, ICommon CommonRepository, ILookupDetails lookupDetailsRepository, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _companyRepository = companyRepository;
            _AnchorCompanyRepository = anchorCompanyRepository;
            _CommonRepository = CommonRepository;
            _lookUpRepository = lookupDetailsRepository;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Bank Bashboard
        /// <summary>
        /// bank dashboard View
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult BankDashboard()
        {

            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "BankDashboard", UserID, 0);
                string requestAmountReceiveToday = _companyRepository.GetRequestAmountReceivedToday();
                ViewBag.requestAmountReceiveToday = requestAmountReceiveToday;
                var start = "0";
                var length = "500";
                var sortBy = "CompanyID";
                var page = "";
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var lstEmailIDofBank = _companyRepository.GetBankId(UserID);
                int Bankid = 0;
                var CompanyName = "";
                IEnumerable<BankAnchorListModel> cm = _companyRepository.GetBankSetLimitList(sortBy, pageSize, skip, CompanyName, "", 0);
                ViewData["TOTALNEWANCHOR"] = cm.Count();

                IEnumerable<BankLimitAnchorList> objexistingAnchor = _companyRepository.TotalExistingAnchor(sortBy, pageSize, skip, CompanyName, "").Distinct();
                ViewData["TOTALEXISTINGANCHOR"] = objexistingAnchor.Count();

                IEnumerable<BankLimitAnchorListView> objTotalBankLimitAnchorLists = _companyRepository.GetTotalBankLimitAnchorLists(sortBy, pageSize, skip, CompanyName, page, UserID);
                decimal? TotalSetBankLimit = 0;
                for (int i = 0; i < objTotalBankLimitAnchorLists.Count(); i++)
                {
                    TotalSetBankLimit = Sum(TotalSetBankLimit, objTotalBankLimitAnchorLists.ElementAt(i).Available_Limits);

                }
                String temp3 = String.Format("{0:C0}", TotalSetBankLimit);
                temp3 = temp3.Replace("$", "");
                if (Convert.ToString(TotalSetBankLimit).Contains("."))
                {
                    string[] values2 = Convert.ToString(TotalSetBankLimit).Split('.');

                    ViewData["TOTALBANKLIMITANCHORLIST"] = temp3;
                }
                else
                {
                    ViewData["TOTALBANKLIMITANCHORLIST"] = temp3;
                }

                IEnumerable<BankLimitAnchorListView> objUtilizeBankLimitAnchorLists = _companyRepository.GetTotalBankLimitAnchorLists(sortBy, pageSize, skip, CompanyName, page, UserID);
                decimal? objTotalUtilizeBankLimit = 0;
                for (int i = 0; i < objTotalBankLimitAnchorLists.Count(); i++)
                {
                    objTotalUtilizeBankLimit = Sum(objTotalUtilizeBankLimit, objUtilizeBankLimitAnchorLists.ElementAt(i).Utilized_Limits);

                }
                String temp2 = String.Format("{0:C0}", objTotalUtilizeBankLimit);
                temp2 = temp2.Replace("$", "");
                if (temp2.Contains("."))
                {
                    string[] values2 = Convert.ToString(objTotalUtilizeBankLimit).Split('.');

                    ViewData["TOTALUTILIZEDBANKLIMIT"] = temp2;
                }
                else
                {
                    ViewData["TOTALUTILIZEDBANKLIMIT"] = temp2;
                }
                IEnumerable<TotalPendingKYC> objpendingKyc = _companyRepository.GetTotalCompanyPendingKYCs();
                ViewData["TOTALPENDINGKYC"] = objpendingKyc.Count();
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
        public static decimal? Sum(decimal? num1, decimal? num2)
        {
            decimal? total;
            total = num1 + num2;
            return total;
        }
        #endregion

        #region Get Top Anchor Company
        /// <summary>
        /// list display top anchor
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTopAnchorData()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Top Anchor Data", UserID, 0);
                int BankID = Convert.ToInt32(UserID);
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<TopAnchorCompaniesInBankDashboardModel> cm = _companyRepository.GetTopAnchor(sortBy, pageSize, skip, BankID);

                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }
        #endregion

        #region Bank Anchor List Page
        /// <summary>
        /// Bank listing view
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult BankAnchorList()
        {
            return View("BankAnchorListingPage");
        }
        #endregion

        #region Bank SetLimit View
        /// <summary>
        /// listing of BankSetLimit
        /// </summary>
        /// <returns></returns>
        public IActionResult BankSetLimitListing()
        {
            return View("BankSetLimitList");
        }

        #endregion

        #region Bank Limit Anchor List
        /// <summary>
        /// Bank Limit AnchorListing
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult BankLimitAnchorListing()
        {
            return View("BankLimitAnchorListing");
        }

        #endregion

        #region  Get Bank Limit Anchor List
        /// <summary>
        /// listing of limit anchors
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBankLimitAnchorList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            //Int32? UserID = HttpContext.Session.GetInt32("UserID");
            Int32? UserID = HttpContext.Session.GetInt32("CompanyID");
            string ErrorMessage = string.Empty;


            try
            {
                var Res = _CommonRepository.AuditTrailLog("Anchor Listing", "Limit", UserID, 0);
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var ContactPerson = Request.Form["columns[2][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                if (sortBy == " asc" || sortBy == " desc")
                {
                    sortBy = "interest_rate" + sortBy;
                }
                IEnumerable<BankLimitAnchorList> cm = _companyRepository.GetBankSetLimitAnchorLists(sortBy, pageSize, skip, CompanyName, "", UserID).Distinct();

                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;

                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        #endregion

        #region Exoprt Bank Set Limit
        /// <summary>
        /// Export to excel Limit Anchors
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public IActionResult ExportBankSetLimitAnchorList(string CompanyName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Limits", "Export CSV Set Limits List ", UserID, 0);
            Int32? VendorId = HttpContext.Session.GetInt32("UserID");
            var sortBy = "CompanyID asc";
            int pageSize = 100000;
            int skip = 0;
            IEnumerable<BankLimitAnchorList> cm = _companyRepository.GetBankSetLimitAnchorLists(sortBy, pageSize, skip, CompanyName, "", VendorId);

            string csv = ExportBankSetLimitAnchorListDatastr(cm);

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "BankSetLimitAnchorList.csv");
        }

        public string ExportBankSetLimitAnchorListDatastr(IEnumerable<BankLimitAnchorList> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord");
        }

        #endregion

        #region Edit Bank Set Limit by CompanyID
        /// <summary>
        /// Modify Limit By company ID
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public IActionResult BankModifyLimit(int CompanyID, string[] Additional_document)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            SetBankAmountLimit objComapnyModel = new SetBankAmountLimit();
            try
            {
                ViewBag.ApprovalDays = _companyRepository.GetApprovalDays().ToList();
                objComapnyModel.Anchor_id = CompanyID;
                objComapnyModel.Anchor_id = CompanyID;

            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return View("ModifyLimit", objComapnyModel);
        }

        #endregion

        #region Disbursements
        /// <summary>
        /// View of Disbursements
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult DisbursementsListing()
        {
            return View("Disbursements");
        }

        #endregion

        #region Modify Bank SetLimit
        /// <summary>
        /// Save bank and limit and email fuctionality
        /// </summary>
        /// <param name="setBankAmountLimit"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult BankModifyLimit(SetBankAmountLimit setBankAmountLimit, Int64[] DocumentType_ID, string[] Document_Name, string[] Document_Description)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("CompanyID");
            //setBankAmountLimit.Id = UserID.Value;
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Set Limit", "Update", UserID, setBankAmountLimit.Anchor_id);

            try
            {
                var lstEmailIDofBank = _companyRepository.GetBankId(UserID);

                setBankAmountLimit.ModifiedUserName = Convert.ToString(lstEmailIDofBank.ElementAt(0).CompanyId);
                string BankName = lstEmailIDofBank.ElementAt(0).Name;
                Int64 DocumentTypeID = 0;
                for (int i = 0; i < Document_Name.Length; i++)
                {
                    if (Document_Name.ElementAt(i) != null)
                    {
                        DocumentTypeID = DocumentType_ID.ElementAt(i);

                        var Result1 = _companyRepository.InsertDocumentDetails(DocumentTypeID, Document_Name.ElementAt(i), Document_Description.ElementAt(i), UserID, setBankAmountLimit.Anchor_id);
                    }
                }
                var Result = _companyRepository.InsertUpdateLimitAmount(setBankAmountLimit, UserID);


                if (Result > 0)
                {
                    string Template = string.Empty;
                    IEnumerable<GetBankMailTemplate> lstAwaitedInvVendorsView = _companyRepository.GetBankMailTemplate(Template);
                    string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                    string EMAIL_TOKEN_PAYMENT_LINK = "##$$LOGIN_LINK$$##";
                    string paymentLink = "http://localhost:4670/Account/AdminLogin";///change url
                    string emailToAddress = setBankAmountLimit.AnchorEmail.ToString();
                    string subject = "Bank Approval";
                    string body = path;
                    body = body.Replace("@@User@@", BankName);
                    body = body.Replace("@@validamount@@", setBankAmountLimit.Utilized_Limits.ToString());
                    body = body.Replace("@@limit@@", setBankAmountLimit.Available_Limits.ToString());
                    body = body.Replace("@@rate@@", setBankAmountLimit.Interest_rate.ToString());
                    //body = body.Replace("@@rate@@", setBankAmountLimit.Interest_rate.ToString() + "% For" + setBankAmountLimit.Interest_rate_month.ToString() + " Month");
                    body = body.Replace("@@ProjectName@@", "Finocart");
                    body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);

                    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                    _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);

                    string Validity_upto = setBankAmountLimit.Validity_upto.Value.ToShortDateString();

                    //string DescriptionMessage = "The " + BankName + " Bank have provided you the Amount Of Rs " + setBankAmountLimit.Utilized_Limits + " with validity date of " + Validity_upto + "";
                    string DescriptionMessage = "The " + BankName + " has set INR "+setBankAmountLimit.Available_Limits+" limit.";
                    Int32? UserId = HttpContext.Session.GetInt32("UserID");
                    var Result1 = _AnchorCompanyRepository.AddNotificationMessage(setBankAmountLimit.Anchor_id, DescriptionMessage, null, UserId);
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
            if (setBankAmountLimit.PageName == "Limits")
            {
                return RedirectToAction("BankLimitAnchorViewList", "BankCompany", new { id = setBankAmountLimit.Anchor_id });
            }
            else
            {
                //return View("~/Views/BankCompany/BankSetLimitList.cshtml");
                return RedirectToAction("BankAnchorList", "BankCompany");

            }
        }

        #endregion

        public JsonResult DeleteDocumentDetail(string documentTypeId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var data = IsBase64Encoded(documentTypeId);
                if (data == true)
                {

                    var result = Convert.FromBase64String(documentTypeId);
                    int IDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                    int objCmp = 0;
                    objCmp = _companyRepository.DeleteDocumentDetail(IDs);
                    return Json(objCmp);
                }
                else
                {
                    return Json(0);
                }

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

        #region Fund Request List
        /// <summary>
        /// View of FundRequest
        /// </summary>
        /// <returns></returns>
        public IActionResult FundRequest()
        {
            return View("~/Views/BankCompany/FundRequest.cshtml");
        }

        #endregion

        #region Fund Request List Post Event
        /// <summary>
        /// list of Funds Withdrawn History
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFundWithdrawHistory()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                IEnumerable<FundsRequestHistory> lstPaymentDueVendors = _companyRepository.GetFundRequestHistoryList(sortBy, pageSize, skip);
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
                return Json(null);
            }

        }

        #endregion

        #region Fund Request List View Get Event
        /// <summary>
        /// Fund request View
        /// </summary>
        /// <returns></returns>
        public IActionResult FundRequestView()
        {
            return View("~/Views/BankCompany/FundRequestView.cshtml");
        }

        #endregion

        #region Fund Request List View Post Event
        /// <summary>
        /// listing of Fund Request
        /// </summary>
        /// <param name="fundsRequestHistory"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult FundRequestView(FundsRequestHistory fundsRequestHistory)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var InvoiceID = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var InvoiceAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<FundsRequestHistoryView> lstPaymentDueVendorView = _companyRepository.GetFundsRequestHistoryView(fundsRequestHistory.Anchor_id, sortBy, pageSize, skip);
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
                return RedirectToAction("ErrorPage", "Common");
            }

        }

        #endregion

        #region Get BankSetLimit List
        /// <summary>
        /// listing of Set Limit 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBankSetLimitList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Set Limit", "Listing", UserID, 0);

            try
            {
                var lstEmailIDofBank = _companyRepository.GetBankId(UserID);
                int Bankid = Convert.ToInt32(lstEmailIDofBank.ElementAt(0).CompanyId);
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var ContactPerson = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                if (sortBy == " asc" || sortBy == " desc")
                {
                    sortBy = "interest_rate" + sortBy;
                }
                IEnumerable<BankAnchorListModel> cm = _companyRepository.GetBankSetLimitList(sortBy, pageSize, skip, CompanyName, "", Bankid);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        #endregion

        #region Get Bank Anchor  List

        [HttpPost]
        public JsonResult GetBankAnchorListata()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Anchor Listing", "New Anchor", UserID, 0);
                var lstEmailIDofBank = _companyRepository.GetBankId(UserID);
                int Bankid = Convert.ToInt32(lstEmailIDofBank.ElementAt(0).CompanyId);
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var ContactPerson = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                if (sortBy == " asc" || sortBy == " desc")
                {
                    sortBy = "interest_rate" + sortBy;
                }
                IEnumerable<BankAnchorListModel> cm = _companyRepository.GetBankSetLimitList(sortBy, pageSize, skip, CompanyName, "", Bankid);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                ViewData["TOTALNEWANCHOR"] = cm.Count();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        #endregion

        #region DrawFundsDocumentList
        /// <summary>
        /// view of draw funds
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult DrawFundsList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FundsList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Draw Funds", "Anchor Draw Fund List", UserID, 0);
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var NameofBank = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var KYCStatus = Request.Form["columns[3][search][value]"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                recordsFiltered = 9;
                recordsTotal = 10;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<UploadKYC> cm = _companyRepository.getDrawFundsList(sortBy, pageSize, skip, AnchorCompId, NameofBank, KYCStatus);

                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }

        }
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult DrawFundsHistoryList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FundsHistoryList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Draw Fund Request History", "Draw Fund Request history List", UserID, 0);
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                //recordsFiltered = 9;
                //recordsTotal = 10;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<DrawFundsFromBank> cm = _companyRepository.getFundsHistoryList(sortBy, pageSize, skip, AnchorCompId);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }

        }
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult FundsWithdrawnHistory()
        {
            return View();

        }

        [HttpPost]
        public JsonResult FundsWithdrawnHistorylist()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Funds History", "Fund Width Draw History List", UserID, 0);
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;


                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<FundsWithdrwanHistory> cm = _companyRepository.fundsWithdrwanHistories(sortBy, pageSize, skip, AnchorCompId);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }



        public IActionResult FundsWithdrawnHistoryView(int ID, string bankName)
        {
            ViewBag.name = bankName;
            TempData["bankName"] = bankName;
            TempData["ID"] = ID;

            TempData.Keep("bankName");
            TempData.Keep("ID");


            return View();

        }

        [HttpPost]
        public JsonResult FundsWithdrawnHistoryViewList(FundsWithdrwanHistory withdrwanHistory)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            //object bankname = TempData["bankName"];
            //object Id = TempData["ID"];


            try
            {
                var Res = _CommonRepository.AuditTrailLog("Funds History", "Fund Width Draw History  View List", UserID, 0);
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var BankName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var Drawfunds = Request.Form["columns[2][search][value]"].FirstOrDefault();

                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<FundsWithdrwanHistory> cm = _companyRepository.fundsWithdrwanHistoriesview(sortBy, pageSize, skip, AnchorCompId, withdrwanHistory.id, withdrwanHistory.BankName);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }



        }
        public IActionResult DrawFundsHistoryview()
        {
            return View();
        }


        [HttpPost]
        public JsonResult FundsHistoryviewList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Fund Request", "Fund Request History View List", UserID, 0);
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var BankName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var Drawfunds = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<DrawFundsFromBank> cm = _companyRepository.getFundsHistoryview(sortBy, pageSize, skip, AnchorCompId, BankName, Drawfunds);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
                //return Json(new
                //{
                //    redirectUrl = Url.Action("ErrorPage", "Common"),
                //    isRedirect = true
                //});
            }

        }

        public IActionResult DrawFundsDocumentList(int ID, string BankName, string Status)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {

                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");
                IEnumerable<BankDocumnet> model = _companyRepository.BankDocumnet(AnchorCompId, ID);
                HttpContext.Session.SetString("Name", BankName); //For getBank Id 
                ViewBag.Bank = ID;
                ViewBag.BankName = BankName;
                ViewBag.Status = Status;
                return View("DrawFundsDocumentList", model);
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

        //[BreadCrumb(Title = "Client Details", Order = 3)]
        public IActionResult DrawFundsDocumentView(int DocumentTypeID)
        {
            //this.AddBreadCrumb(new BreadCrumb
            //{
            //    Title = "Upload KYC",
            //    Url = string.Format("{0}", Url.Action("DrawFundsList", "BankCompany")),
            //    Order = 1
            //});

            //this.AddBreadCrumb(new BreadCrumb
            //{
            //    Title = "Download KYC",
            //    Url = string.Format("{0}", Url.Action("DrawFundsDocumentView", "BankCompany")),
            //    Order = 2
            //});

            GetDocument_Download getDocument_Download = new GetDocument_Download();
            getDocument_Download.ID = DocumentTypeID;
            return View(getDocument_Download);
        }

        [HttpPost]
        public JsonResult DrawFundsDocumentViewList(int documentTypeID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Fund Request", "Fund Request History View List", UserID, 0);
                Int64? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                //var BankName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                //var Drawfunds = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<GetDocument_Download> cm = _companyRepository.DrawFundsDocumentView(sortBy, pageSize, skip, documentTypeID, AnchorCompId);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
                //return Json(new
                //{
                //    redirectUrl = Url.Action("ErrorPage", "Common"),
                //    isRedirect = true
                //});
            }

        }

        #endregion

        #region  Edit BankAnchor Detail
        /// <summary>
        /// Update bank Anchor Details
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyEmail"></param>
        /// <param name="anchorName"></param>
        /// <returns></returns>
        public ActionResult EditBankAnchorDetail(int SetLimitID, int CompanyID, string CompanyEmail, string anchorName, string PageName, string Status)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("CompanyID");
            string ErrorMessage = string.Empty;


            SetBankAmountLimit objComapnyModel = new SetBankAmountLimit();
            try
            {

                var lstEmailIDofBank = _companyRepository.GetBankId(UserID);
                int Bankid = Convert.ToInt32(lstEmailIDofBank.ElementAt(0).CompanyId);
                var Res = _CommonRepository.AuditTrailLog("Set Limit", "Edit", UserID, SetLimitID);
                ViewBag.ApprovalDays = _companyRepository.GetApprovalDays().ToList();
                objComapnyModel = _companyRepository.EditPage(SetLimitID, Bankid);
                IEnumerable<BankDocumnet> bankDocumnet = _companyRepository.GetDocumentsList(CompanyID, Bankid);
                objComapnyModel.Id = SetLimitID;
                objComapnyModel.Anchor_id = CompanyID;
                objComapnyModel.AnchorEmail = CompanyEmail;
                objComapnyModel.Anchor_Name = anchorName;
                objComapnyModel.ModifiedUserName = Bankid.ToString();
                objComapnyModel.PageName = PageName;
                ViewBag.AnchorName = anchorName;
                ViewBag.KYCStatus = Status;
                IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("BankLimit");
                List<string> od = new List<string>()
                {"OD","BD" };
                ViewBag.ODBD = od.ToList();
                Tuple<SetBankAmountLimit, IEnumerable<BankDocumnet>, IEnumerable<LookupDetails>> objData = new Tuple<SetBankAmountLimit, IEnumerable<BankDocumnet>, IEnumerable<LookupDetails>>(objComapnyModel, bankDocumnet, lookupDetails);
                return PartialView("_ModifyLimitPartial", objData);
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

        #region Exoprt Bank Set Limit
        /// <summary>
        /// Export to excel SetLimit List
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public IActionResult ExportBankSetLimitList(string CompanyName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("New Anchor List", "Bank Anchor List", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("UserID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<BankAnchorListModel> lstAnchorCompany = _companyRepository.GetBankSetLimitList(sortBy, pageSize, skip, CompanyName, "", UserID);

                string csv = ExportBankSetLimitListDatastr(lstAnchorCompany);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "BankSetLimitList.csv");
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

        public string ExportBankSetLimitListDatastr(IEnumerable<BankAnchorListModel> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord");
        }

        #endregion

        #region Display Disbursement Popup
        /// <summary>
        /// edit disbursements details
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEditBankDisbursements()
        {
            DisbursementsModel objComapnyModel = new DisbursementsModel();
            return PartialView("_AddEditDisbursementPartial", objComapnyModel);
        }

        #endregion

        #region Insert edit Disbursement data
        /// <summary>
        /// update disbursements 
        /// </summary>
        /// <param name="disbursementData"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddEditDisbursementData(DisbursementsModel disbursementData)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Disbursement", "Update and Save", UserID, disbursementData.id);

            try
            {
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                disbursementData.CreatedBy = UserId;
                disbursementData.UpdatedBy = UserId;
                var Result = _companyRepository.AddEditDisbursementData(disbursementData);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return RedirectToAction("DisbursementsListing", "BankCompany");
        }

        #endregion

        #region Get Disbursement Listing Details
        /// <summary>
        /// Listing Of Disbursements
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDisbursementList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Disbursement", "DisbursementList", UserID, 0);

            try
            {
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var ContactPerson = Request.Form["columns[1][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<DisbursementListModel> cm = _companyRepository.GetDisbursementList(sortBy, pageSize, skip, CompanyName, "");
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        #endregion

        #region  Edit Disbursement Detail
        /// <summary>
        /// Modifiy Details 
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public ActionResult EditDisbursementDetail(int CompanyID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Disbursement", "Edit", UserID, CompanyID);

            DisbursementsModel objComapnyModel = new DisbursementsModel();
            try
            {
                objComapnyModel = _companyRepository.EditDisbursementDetail(CompanyID);
                objComapnyModel.id = CompanyID;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }

            return PartialView("_AddEditDisbursementPartial", objComapnyModel);
        }

        #endregion

        #region  Edit Funds From Bank Detail
        /// <summary>
        /// 
        /// Update fund details
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 
        public ActionResult EditFundsFromBankDetail(int ID, bool isFromHistory = false)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
           


            SetBankAmountLimit objsetBankAmountLimit = new SetBankAmountLimit();
            objsetBankAmountLimit.Id = ID;
            try
            {
                objsetBankAmountLimit = _companyRepository.EditFundRequestFromBank(ID);
                objsetBankAmountLimit.Id = ID;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            ViewBag.isFromHistory = isFromHistory;
            return PartialView("_EditFundsRequestFromBank", objsetBankAmountLimit);
        }

        #endregion

        #region KYC UPLOAD List
        /// <summary>
        /// View of KYC data
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult KycUploadListdata()
        {
            return View("KYCUploadList");
        }

        #endregion

        #region Get KYC Upload Listing Details
        /// <summary>
        /// listing of KYC Data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetKycUploadListata()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            Int64? BankID = HttpContext.Session.GetInt32("CompanyID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Anchor Listing", "Kyc Documents", UserID, 0);
            try
            {
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var ContactPerson = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<KycUploadModel> cm = _companyRepository.GetKycUploadList(sortBy, pageSize, skip, CompanyName, "", BankID);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }
        #endregion

        #region View Upload Document List
        /// <summary>
        /// Upload KYC Documents
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public IActionResult UploadDocumentListing(int CompanyID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Kyc Documents", "View Page", UserID, CompanyID);

            KycUploadModel vendorAwaitedInvApproval = new KycUploadModel();
            try
            {
                vendorAwaitedInvApproval.CompanyID = CompanyID;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return View("~/Views/BankCompany/UploadDocumentDetailsList.cshtml", vendorAwaitedInvApproval);
        }

        #endregion

        #region Get Bank Company Graph Details
        /// <summary>
        /// Bank company details Graph
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
                var Res = _CommonRepository.AuditTrailLog("Home", "Graph Details", UserID, 0);
                Int64? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                //string BankName = HttpContext.Session.GetString("LoginName");
                var lstEmailIDofBank = _companyRepository.GetBankId(UserID);
                string BankName = Convert.ToString(lstEmailIDofBank.ElementAt(0).CompanyId);
                IEnumerable<BankGraphDetailsModel> lstGraph = _companyRepository.GetGraphDetails(month, BankName);
                return Json(lstGraph);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        #endregion

        #region Download Document 
        /// <summary>
        /// Download KYC Documents
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public IActionResult Download(string FilePath)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                GetUploadDocumentListModel st_Addclientlist = new GetUploadDocumentListModel();
                string documentFolderPath = _configuration["BankDocumentsFolder"];
                string UploadedFileFolder = "UploadedDocuments";
                string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), UploadedFileFolder, documentFolderPath));
                string Path1 = filePath;
                string filepath = Path1 + "\\" + FilePath;
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FilePath);
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

        #region Get Upload Vendor View List
        /// <summary>
        /// Upload Vendor Listing
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUploadDocumentList(int CompanyID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                Int64? AnchorCompID = HttpContext.Session.GetInt32("AnchorCompID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                IEnumerable<GetUploadDocumentListModel> lstAwaitedInvVendorsView = _companyRepository.GetUploadDocumentList(CompanyID, sortBy, pageSize, skip);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;

                if (lstAwaitedInvVendorsView != null && lstAwaitedInvVendorsView.Count() != 0)
                {
                    recordsFiltered = lstAwaitedInvVendorsView.Count();
                    recordsTotal = lstAwaitedInvVendorsView.Count();
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAwaitedInvVendorsView });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }

        }

        #endregion

        #region Exoprt Bank Set Limit
        /// <summary>
        /// Export to Excel Disbursements Amounts
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public IActionResult ExportDisbursementToCSV(string CompanyName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            var Res = _CommonRepository.AuditTrailLog("Distributment", "Export CSV Distributment", UserID, 0);
            Int32? VendorId = HttpContext.Session.GetInt32("UserID");
            var sortBy = "id asc";
            int pageSize = 100000;
            int skip = 0;
            IEnumerable<DisbursementListModel> lstAnchorCompany = _companyRepository.GetDisbursementList(sortBy, pageSize, skip, CompanyName, "");

            string csv = ExportDisbursementDatastr(lstAnchorCompany);

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Disbursement.csv");
        }

        public string ExportDisbursementDatastr(IEnumerable<DisbursementListModel> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord");
        }

        #endregion

        #region Upload Document Details
        /// <summary>
        /// Upload Anchor Documents
        /// </summary>
        /// <param name="AnchorDocument"></param>
        /// <param name="BankName"></param>
        /// <returns></returns>
        /// 

        public async Task<IActionResult> UploadDocument(IFormFile AnchorDocument, int BankID, string BankName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                bool bResult = false;
                string ReturnMessage = "";
                int result1 = 0;
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string CompanyName = HttpContext.Session.GetString("Companyname");
                Int32? UserId = HttpContext.Session.GetInt32("UserID");

                string documentFolderPath = _configuration["BankDocumentsFolder"];
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    var DocumentType_ID = Path.GetFileName(file.Name);
                    var filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(fileName);
                    string newname = filenamewithoutext + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;

                    bResult = _CommonRepository.UploadFileToServer(file, documentFolderPath, newname);
                    if (bResult == true)
                    {
                        BankDocumnet_list BankDocumnet_list = new BankDocumnet_list();
                        BankDocumnet_list.DocumentType_ID = int.Parse(DocumentType_ID);
                        BankDocumnet_list.AnchorID = AnchorCompId;
                        BankDocumnet_list.FilePath = newname + extension;
                        BankDocumnet_list.BankName = BankID.ToString();
                        BankDocumnet_list.Status = 0;
                        BankDocumnet_list.CreatedBy = UserId;
                        BankDocumnet_list.ModifiedBy = UserId;
                        result1 = _companyRepository.Documnet_Submit(BankDocumnet_list);
                    }
                    else
                    {
                        ReturnMessage = "There is some error in fileupload";
                    }
                }
                if (result1 > 0)
                {
                    var lstEmailIDofBank = _companyRepository.GetEmail(BankID.ToString());

                    string Template = string.Empty;
                    IEnumerable<GetBankKYCMailTemplate> lstAwaitedInvVendorsView = _companyRepository.GetBankKYCMailTemplate(Template);
                    string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                    string EMAIL_TOKEN_PAYMENT_LINK = "##$$LOGIN_LINK$$##";
                    string paymentLink = "http://localhost:4670/Account/AdminLogin";///change url
                    string emailToAddress = lstEmailIDofBank.ElementAt(0).BankEmail;
                    string subject = "Bank Approval";
                    WebClient client = new WebClient();
                    string startupPath = Environment.CurrentDirectory;
                    string body = path;
                    body = body.Replace("@@User@@", BankName);
                    body = body.Replace("@@ProjectName@@", "Finocart");
                    body = body.Replace("@@CompanyName@@", CompanyName);
                    body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);

                    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                    _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);
                    TempData["successmsg"] = "success";

                    Company objDatawithSP = _CommonRepository.CheckAdminByEmail(emailToAddress, false);
                    if (objDatawithSP != null)
                    {                        
                        string DescriptionMessage = CompanyName + "  has submitted KYC documents. Please review.";

                        var Result1 = _AnchorCompanyRepository.AddNotificationMessageForBank(null, DescriptionMessage, 25, UserId);
                    }

                }
                return RedirectToAction("DrawFundsDocumentList", "BankCompany", new { ID = BankID, BankName = BankName });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            //return RedirectToAction("DrawFundsDocumentList", "BankCompany", new { ID = 0, BankName = BankName });
        }

        #endregion

        #region Approved Status
        /// <summary>
        /// Display Company Status
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public IActionResult ApprovedStatus(string CompanyName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Kyc Documents", "Update Approved Status", UserID, Convert.ToInt32(CompanyName));

            try
            {
                Int32? BankID = HttpContext.Session.GetInt32("CompanyID");
                var Result = _companyRepository.ApprovedStatus(CompanyName, BankID);
                if (Result > 0)
                {
                    var lstEmailIDofBank = _companyRepository.GetAnchorEmail(CompanyName);
                    string emailToAddress = lstEmailIDofBank.ElementAt(0).Contact_email;
                    string AnchorName = lstEmailIDofBank.ElementAt(0).Contact_Name;
                    string BankName = HttpContext.Session.GetString("Companyname");
                    string Template = string.Empty;
                    int Id = 1;
                    IEnumerable<GetBankApprovedMailTemplate> lstAwaitedInvVendorsView = _companyRepository.GetApprovedMailTemplate(Template, Id);
                    string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                    string subject = "Bank Approval";
                    string body = path;
                    body = body.Replace("@@User@@", AnchorName);
                    body = body.Replace("@@BankName@@", BankName);
                    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                    _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);


                    Company objDatawithSP = _CommonRepository.CheckAdminByEmail(emailToAddress, false);
                    if (objDatawithSP != null)
                    {

                        string DescriptionMessage = BankName + "  has approved your documents. You can now draw funds.";

                        var Result1 = _AnchorCompanyRepository.AddNotificationMessage(objDatawithSP.CompanyID, DescriptionMessage, null, UserID);
                    }
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
            return View("KYCUploadList");
        }

        #endregion

        #region Reject Status
        /// <summary>
        /// Update Rejected Status
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public IActionResult RejectStatus(string CompanyName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("Kyc Documents", "Update Reject Status", UserID, Convert.ToInt32(CompanyName));

            try
            {
                var Result = _companyRepository.RejectStatus(CompanyName);
                if (Result > 0)
                {
                    var lstEmailIDofBank = _companyRepository.GetAnchorEmail(CompanyName);
                    string emailToAddress = lstEmailIDofBank.ElementAt(0).Contact_email;
                    string AnchorName = lstEmailIDofBank.ElementAt(0).Contact_Name;
                    string BankName = HttpContext.Session.GetString("Companyname");
                    string Template = string.Empty;
                    int Id = 2;
                    IEnumerable<GetBankApprovedMailTemplate> lstAwaitedInvVendorsView = _companyRepository.GetApprovedMailTemplate(Template, Id);
                    string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                    string subject = "Bank Reject";
                    string body = path;
                    body = body.Replace("@@User@@", AnchorName);
                    body = body.Replace("@@BankName@@", BankName);
                    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                    _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);

                    Company objDatawithSP = _CommonRepository.CheckAdminByEmail(emailToAddress, false);
                    if (objDatawithSP != null)
                    {

                        string DescriptionMessage = BankName + " has not approved your documents. Please resubmit KYC.";

                        var Result1 = _AnchorCompanyRepository.AddNotificationMessage(objDatawithSP.CompanyID, DescriptionMessage, null, UserID);
                    }

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
            return View("KYCUploadList");
        }

        #endregion

        #region
        /// <summary>
        /// Fund Request From bank
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult DrawFunds()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetFundsFromBank()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Fund Request From Bank", "Fund Request From Bank List", UserID, 0);
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var BankName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var Drawfunds = Request.Form["columns[2][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<DrawFundsFromBank> cm = _companyRepository.getFundsFromBank(sortBy, pageSize, skip, AnchorCompId);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        [HttpGet]
        public ActionResult GetAvailableLimit(int ID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                string Role = HttpContext.Session.GetString("Role");

                var length = 1;
                var start = 0;

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                //var BankName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                //var Drawfunds = Request.Form["columns[2][search][value]"].FirstOrDefault();

                var sortBy = "Anchor_id";
                IEnumerable<DrawFundsFromBank> cm = _companyRepository.getFundsFromBank(sortBy, pageSize, skip, AnchorCompId);

                var resutl = (from am in cm where am.id == ID select am.Available_Limits).FirstOrDefault();
                return View("_EditFundsRequestFromBank", resutl);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }

        }



        #region Insert/Update - Funds Requested From Bank
        [HttpPost]
        public IActionResult EditFundsRequestFromBank(SetBankAmountLimit setBankAmountLimitdata)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                //disbursementData.CreatedBy = UserId;
                // disbursementData.UpdatedBy = UserId;
                var Result = _companyRepository.UpdateFundsRequestFromBank(setBankAmountLimitdata);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return View("FundsRequestFromBank");
        }

        #endregion

        public JsonResult GetBankTotalFundLimits(Int64 companyID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("finassist", "Invoice by Amount List", UserID, 0);

                int TotalAvailableLimits = _companyRepository.getBankTotalFundLimits(companyID);

                return Json(new { data = TotalAvailableLimits });

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
        /// for 
        /// </summary>
        [HttpPost]
        public JsonResult GetFundWithDrawDetailsperBank(int ID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                recordsFiltered = 0;
                recordsTotal = 0;
                IEnumerable<FundRequestFromBank> cm = _companyRepository.getFundWithDrawDetailsperBank(sortBy, pageSize, skip,ID);
                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        //public JsonResult GetLeastAvilable_Bal(int ID)
        //{
        //    string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
        //    string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
        //    Int32? UserID = HttpContext.Session.GetInt32("UserID");
        //    string ErrorMessage = string.Empty;

        //    try
        //    {
        //        IEnumerable<GetLeastAvail_bal> cm = _companyRepository.GetListAvaial_bal(int ID);
        //        return Json(new { data = cm });
        //    }
        //    catch (Exception ex)
        //    {
        //        var st = new StackTrace(ex, true);
        //        var frame = st.GetFrame(0);
        //        int ErrorLine = frame.GetFileLineNumber();
        //        var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
        //        return Json(null);
        //    }
        //}

        public IActionResult BankLimitAnchorViewList(int id)
        {
            BankLimitAnchorListView bankLimitAnchorList = new BankLimitAnchorListView();
            var lstEmailIDofBank = _companyRepository.GetAnchorEmail(id.ToString());
            bankLimitAnchorList.CompanyID = id;
            bankLimitAnchorList.Company_name = lstEmailIDofBank.ElementAt(0).CompanyName;
            bankLimitAnchorList.Contact_email = lstEmailIDofBank.ElementAt(0).Contact_email;
            return View("BankLimitAnchorViewList", bankLimitAnchorList);
        }

        [HttpPost]
        public JsonResult GetBankLimitAnchorViewList(BankLimitAnchorListView bankLimitAnchorList)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            //Int32? UserID = HttpContext.Session.GetInt32("UserID");
            Int32? UserID = HttpContext.Session.GetInt32("CompanyID");
            string ErrorMessage = string.Empty;


            try
            {
                var Res = _CommonRepository.AuditTrailLog("Anchor Listing", "Limit", UserID, 0);
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var ContactPerson = Request.Form["columns[2][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                if (sortBy == " asc" || sortBy == " desc")
                {
                    sortBy = "interest_rate" + sortBy;
                }
                IEnumerable<BankLimitAnchorListView> cm = _companyRepository.GetBankLimitAnchorLists(sortBy, pageSize, skip, CompanyName, bankLimitAnchorList.CompanyID, "", UserID);

                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult GetBankLimitLogList(Int64 setLimitId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            //Int32? UserID = HttpContext.Session.GetInt32("UserID");
            Int32? UserID = HttpContext.Session.GetInt32("CompanyID");
            string ErrorMessage = string.Empty;


            try
            {
                var Res = _CommonRepository.AuditTrailLog("Anchor Listing", "Limit", UserID, 0);
                string Role = HttpContext.Session.GetString("Role");
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var ContactPerson = Request.Form["columns[2][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                if (sortBy == " asc" || sortBy == " desc")
                {
                    sortBy = "interest_rate" + sortBy;
                }
                //IEnumerable<BankLimitAnchorListView> cm = _companyRepository.GetBankLimitAnchorLists(sortBy, pageSize, skip, CompanyName, bankLimitAnchorList.CompanyID, "", UserID);
                IEnumerable<BankLimitLogView> cm = _companyRepository.GetBankLimitLogLists(sortBy, pageSize, skip, setLimitId);

                if (cm != null && cm.Count() != 0)
                {
                    recordsFiltered = cm.ElementAt(0).FilteredRecord;
                    recordsTotal = cm.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = cm });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        public IActionResult Document_download(int DocumentTypeID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            try
            {
                Int32? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");



                IEnumerable<GetDocument_Download> gdd = _companyRepository.getDocumentDownload(DocumentTypeID, AnchorCompId);
                GetDocument_Download cm = new GetDocument_Download();
                cm = gdd.FirstOrDefault();
                if (cm.FilePath != "")
                {
                    string FileName = cm.FilePath;
                    string documentFolderPath = _configuration["BankDocumentsFolder"];
                    string UploadedFileFolder = "UploadedDocuments";
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string filePath = Path.GetFullPath(Path.Combine(webRootPath, UploadedFileFolder, documentFolderPath));
                    string Path1 = filePath;
                    string filepath = Path1 + "\\" + FileName;
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);

                }
                else
                {
                    return RedirectToAction("DrawFundsDocumentList");
                }

            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }

        }

        public JsonResult CheckFromToDate(DateTime fromDate, DateTime toDate, Int64 anchorCompId, Int64 setLimitId)
        {

            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            var BankId = HttpContext.Session.GetInt32("CompanyID");
            try
            {
                var result = _companyRepository.CheckFromToDate(BankId, fromDate, toDate, anchorCompId, setLimitId);
                return Json(result);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return Json(null);
            }
        }

        public IActionResult SetLimitHistory()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetSetLimitHistory()
        {

            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            int? CompanyID = HttpContext.Session.GetInt32("CompanyID");
            string ErrorMessage = string.Empty;

            try
            {
                var length = Request.Form["length"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var SearchFieldName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                recordsFiltered = 0;
                recordsTotal = 0;
                IEnumerable<SetLimitHistory> lstSetLimitHistory = _AnchorCompanyRepository.GetSetLimitHistory(sortBy,pageSize,skip,SearchFieldName,CompanyID);
                if (lstSetLimitHistory != null && lstSetLimitHistory.Count() != 0)
                {
                    recordsFiltered = lstSetLimitHistory.ElementAt(0).FilteredRecord;
                    recordsTotal = lstSetLimitHistory.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstSetLimitHistory });
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

        #region Get Vendors For Anchors
        /// <summary>
        /// Get Vendors For Anchors
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSetLimitForAnchorComp()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Graph details", UserID, 0);
                Int64? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<SetLimitForAnchor> lstSetLimitforAnchor = _companyRepository.GetSetLimitForAnchor();
                return Json(lstSetLimitforAnchor);
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
        public JsonResult GetAnchorSetLimitData(int AnchorCompID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Graph details", UserID, 0);
               // Int64? AnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                IEnumerable<GetUpcomingSetLimitChartData> lstGraph = _companyRepository.GetSetLimitforAnchorCompany(AnchorCompID);
                return Json(lstGraph.Select(x=>new { label=x.CreatedDate.ToString("dd MMM yyyy"),value=x.Available_Limits}));
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
        public IActionResult BankHolidayList()
        {

            return View();
        }

        public JsonResult GetHolidayListing()
        {


            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            int? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var Reason = Request.Form["columns[1][search][value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                recordsFiltered = 0;
                recordsTotal = 0;
                IEnumerable<HolidayListModel> lstAnchorHolidayList = _companyRepository.GetHolidayListing(sortBy, pageSize, skip,Reason,UserID);

                if (lstAnchorHolidayList != null && lstAnchorHolidayList.Count() != 0)
                {
                    recordsFiltered = lstAnchorHolidayList.ElementAt(0).FilteredRecord;
                    recordsTotal = lstAnchorHolidayList.ElementAt(0).TotalRecord;
                }
                //int? recordsFiltered = 0;
                ////int? recordsTotal = 0;
                //if (lstAnchorHolidayList != null && lstAnchorHolidayList.Count() != 0)
                //{
                //    //recordsFiltered = lstAnchorHolidayList.ElementAt(0).FilteredRecord;
                //    //recordsTotal = lstAnchorHolidayList.ElementAt(0).TotalRecord;
                //}

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAnchorHolidayList });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;

            }

            // return View("~/Views/AnchorCompany/HolidayList.cshtml");
        }
    }
}

