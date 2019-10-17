using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Model;
using Finocart.Services;
using Finocart.Web.Views.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Finocart.Web.Controllers
{
    //[BreadCrumb(UseDefaultRouteUrl = true, Order = 0)]
    public class AnchorCompanyController : BaseController
    {
      
        #region Interface declaration

        private readonly ICompany _companyRepository;
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

       

        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="UserRepository"></param>
        public AnchorCompanyController(ILookupDetails lookupDetailsRepository, ICommon CommonRepository, IConfiguration ConfigurationRepository, IAnchorCompany anchorCompanyRepository, ICompany CompanyRepository, IHostingEnvironment hostingEnvironment)
        {
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
            _configuration = ConfigurationRepository;
            _AnchorCompanyRepository = anchorCompanyRepository;
            _companyRepository = CompanyRepository;
            _hostingEnvironment = hostingEnvironment;
        }


        #endregion

        #region Definitions

        /// <summary>
        /// returning anchor dashboard
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult AnchorDashboard()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                var Res = _CommonRepository.AuditTrailLog("Home", "Anchor Company DashBorad", UserID, 0);
                Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                ViewBag.CompanyID = CompanyID;
                var start = "0";
                var length = "10";
                var sortBy = "CompanyID";
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;


                var VendorName = "";
                var TotalAppInvAmt = "";

                /// <summary>
                /// PaymentDueToday
                /// </summary>
                IEnumerable<VendorPaymentDueModel> lstPDT = _AnchorCompanyRepository.GetPaymentDueToday(CompanyID, sortBy, pageSize, skip, VendorName, TotalAppInvAmt);
                 Res = _CommonRepository.AuditTrailLog("Home", "Anchor Payment Due date", UserID, 0);
                decimal? sumAvailableFundComp = 0;
                for (int i = 0; i < lstPDT.Count(); i++)
                {
                    sumAvailableFundComp = Sum(sumAvailableFundComp, lstPDT.ElementAt(i).AmountPaymenttoday);

                }
                String temp0 = String.Format("{0:C0}", sumAvailableFundComp);
                temp0 = temp0.Replace("$", "");
                if (Convert.ToString(sumAvailableFundComp).Contains("."))
                {
                    string[] values0 = Convert.ToString(sumAvailableFundComp).Split('.');
                    //TempData["AVAILABLEFORFUNDINGTODAY"] = temp0 + "." + values0[1];
                    ViewData["PAYABLEDUETODAY"] = temp0;
                }
                else
                {
                    ViewData["PAYABLEDUETODAY"] = temp0;
                }
                decimal? sumAvailableForFundingToday = 0;

                String temp2 = String.Format("{0:C0}", sumAvailableForFundingToday);
                temp2 = temp2.Replace("$", "");
                if (Convert.ToString(sumAvailableForFundingToday).Contains("."))
                {
                    string[] values1 = Convert.ToString(sumAvailableForFundingToday).Split('.');

                    ViewData["AMOUNTAPPROVEDTODAY"] = temp2;
                }
                else
                {
                    ViewData["AMOUNTAPPROVEDTODAY"] = temp2;
                }
                /// <summary>
                /// Invoice Pending For Action
                /// </summary>
                IEnumerable<VendorAwaitedInvApprovalModel> lstAwaitedInvoiceVendors = _AnchorCompanyRepository.GetAwaitedInvoiceApproval(CompanyID, sortBy, pageSize, skip, VendorName, TotalAppInvAmt);
                 Res = _CommonRepository.AuditTrailLog("Home", "Anchor Awaited Invoice Approval", UserID, 0);
                decimal? SumAWAITEDAPPROVALININR = 0;
                for (int i = 0; i < lstAwaitedInvoiceVendors.Count(); i++)
                {
                    SumAWAITEDAPPROVALININR = Sum(SumAWAITEDAPPROVALININR, lstAwaitedInvoiceVendors.ElementAt(i).TotalApprovedAmt);

                }

                String temp1 = String.Format("{0:C0}", SumAWAITEDAPPROVALININR);
                temp1 = temp1.Replace("$", "");
                if (Convert.ToString(SumAWAITEDAPPROVALININR).Contains("."))
                {
                    string[] values = Convert.ToString(SumAWAITEDAPPROVALININR).Split('.');
                    //TempData["AWAITEDAPPROVALININR"] = temp1 + "." + values[1];
                    ViewData["AWAITEDAPPROVALININR"] = temp1;
                }
                else
                {
                    ViewData["AWAITEDAPPROVALININR"] = temp1;
                }

                IEnumerable<VendorInvoiceApprovedTodayModel> lstInvoiceApprovedToday = _AnchorCompanyRepository.GetInvoiceApprovedToday(CompanyID, sortBy, pageSize, skip, VendorName, TotalAppInvAmt);
                 Res = _CommonRepository.AuditTrailLog("Home", "Invoice Approved Today", UserID, 0);
                decimal? sumReceivableDueTodayInInr = 0;
                for (int i = 0; i < lstInvoiceApprovedToday.Count(); i++)
                {
                    sumReceivableDueTodayInInr = Sum(sumReceivableDueTodayInInr, lstInvoiceApprovedToday.ElementAt(i).TotalApprovedAmt);

                }

                String temp3 = String.Format("{0:C0}", sumReceivableDueTodayInInr);
                temp3 = temp3.Replace("$", "");
                if (temp3.Contains("."))
                {
                    string[] values2 = Convert.ToString(sumReceivableDueTodayInInr).Split('.');

                    ViewData["AMOUNTDUETODAYININR"] = temp3;
                }
                else
                {
                    ViewData["AMOUNTDUETODAYININR"] = temp3;
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
            return View("Dashboard");
        }
        public static decimal? Sum(decimal? num1, decimal? num2)
        {
            decimal? total;
            total = num1 + num2;
            return total;
        }
        #region Register a company
        /// <summary>
        /// Register a company
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "SuperAdminLogin")]
        [CustomFilter]
        public IActionResult Register()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var Res = _CommonRepository.AuditTrailLog("User Listing", "Update", UserID, 0);
            Company company = new Company();

            try
            {
                Int32? CompanyId = HttpContext.Session.GetInt32("CompanyID");
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                string Role = HttpContext.Session.GetString("Role");
                ViewBag.Role = TempData["Role"] == null ? Role : TempData["Role"];
                ViewBag.LookUp = (_companyRepository.GetLookUpDropDown()).ToList();
                ViewBag.look = (_companyRepository.GetFactoryOrReverse()).ToList();
                ViewBag.Details= (_companyRepository.GetCuurentStatus()).ToList();
                string InternalRole = TempData["InternalRole"] == null ? "" : TempData["InternalRole"].ToString();
                if (InternalRole.ToLower() == "admin")
                {
                    company = _companyRepository.GetCompany(CompanyId);
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

            return View("Register", company);
        }
        #endregion

        #region Get view to upload documents
        /// <summary>
        /// Upload Documents
        /// </summary>
        /// <returns></returns>
        /// 
       
        public IActionResult UploadDocuments()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("Documenttype");
                return View("UploadDocuments", lookupDetails);
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
        public async Task<JsonResult> UploadFileDatatoServer(IFormFile AnchorDocument, int iDocumentTypeID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            string ReturnMessage = "";
            bool bResult = false;
            string documentFolderPath = _configuration["AnchorDocumentsFolder"];
            try
            {
                string exportFileName = "Anchor_PAN_" + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                bResult = _CommonRepository.UploadFileToServer(AnchorDocument, documentFolderPath, exportFileName);
                //int iAnchorCompId = 1;//set it dynamic
                Int32? iAnchorCompId = HttpContext.Session.GetInt32("CompanyID");
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                if (bResult == true)
                {
                    ReturnMessage = "File uploaded successfully";

                    _AnchorCompanyRepository.InsertDocumentDet(AnchorDocument, iAnchorCompId, iDocumentTypeID, exportFileName + Path.GetExtension(AnchorDocument.FileName), UserId);
                }
                else
                {
                    ReturnMessage = "There is some error in fileupload";
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
            var result = new { bResult = bResult, ReturnMessage = ReturnMessage };
            return Json(result);
        }
        #endregion

        #region Get anchor document listing
        /// <summary>
        /// Get users' listing
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAnchorDocList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Anchor Company", "Anchor Documnet List", UserID, 0);
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                var draw = Request.Form["draw"].FirstOrDefault();
                int? AnchorCompID = UserId;
                IEnumerable<DocumentModel> lstAnchorCompDoc = _AnchorCompanyRepository.GetAnchorDocList(AnchorCompID);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;


                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAnchorCompDoc });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }

            // return View();
        }

        /// <summary>
        /// Checking PAN number
        /// </summary>
        /// <param name="pan"></param>
        /// <returns></returns>
        public ActionResult CheckPan(string pan)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var result = _companyRepository.CheckPan(pan);
                return Json(result);
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
        /// Checking if email exist
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ActionResult CheckEmail(string email)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var result = _companyRepository.CheckEmail(email);
                return Json(result);
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
        /// Registering a company
        /// </summary>
        /// <param name="objCompany"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Company objCompany, IFormCollection formCollection)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try

            {
                objCompany.Password = _CommonRepository.GeneratePassword();
                string Role = HttpContext.Session.GetString("Role");
                Int32? UserId = HttpContext.Session.GetInt32("UserID");
                objCompany.CreatedBy = UserId;
                objCompany.UpdatedBy = UserId;
                var value = formCollection["InterestedAs"];
                var text = formCollection["hidText"];
                var Result = _companyRepository.InsertAnchorCompanyRegister(objCompany);
                if (Result > 0 && objCompany.CompanyID == 0)
                {
                    IEnumerable<GetRegisterMailTemplate> lstAwaitedInvVendorsView = _companyRepository.getRestraterMailTemplate();
                    string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                    string EMAIL_TOKEN_PAYMENT_LINK = "##$$PAYMENT_LINK$$##";
                    string paymentLink = "http://dotnet.brainvire.com/Finocart/Account/AdminLogin";///change url
                    string MailStatus = string.Empty;
                    string emailToAddress = objCompany.Contact_email;
                    string InterestedAsRoles = text.ToString().ToUpper();
                    string subject = "User registration";
                    string body = path;
                    body = body.Replace("@@CompanyName@@", objCompany.Company_name);
                    body = body.Replace("@@InterestedAsRoles@@", InterestedAsRoles);
                    body = body.Replace("@@User@@", objCompany.Pan_number);
                    body = body.Replace("@@ProjectName@@", "Finocart");
                    body = body.Replace("@@Password@@", objCompany.Password);
                    body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);
                    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                    _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);
                    //}
                }
                Company objDatawithSP = _CommonRepository.CheckAdminByEmail(objCompany.Contact_email, false);
                if (objDatawithSP != null)
                {
                    string DescriptionMessage = "Congratulations your registered on the Finocart Portal. Now you can access the portal and create your users";
                    var Result1 = _AnchorCompanyRepository.AddNotificationMessage(objDatawithSP.CompanyID, DescriptionMessage, null, UserId);
                }

                TempData["success"] = "success";
                ViewBag.Role = Role;
                 
                if (objCompany.FactoryOrReverseFactory == 28)
                {
                    objCompany = null;
                    return RedirectToAction("Factoring", "AnchorCompany");
                }
                else
                {
                    objCompany = null;
                    return RedirectToAction("ReverseFactoring", "AnchorCompany");
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
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateRecord(decimal PercentageRate, int PaymentDays, string InternalFundLimits, bool chkUnlimited)
        {
            //long objCmp = 0;
            //decimal? AmountPaid;
             Int32? CompanyID =Convert.ToInt32(HttpContext.Session.GetInt32("CompanyID"));
             var objCmp =_companyRepository.UpdateCompanyRate_Days(CompanyID,PercentageRate, PaymentDays, InternalFundLimits, chkUnlimited);
            //if (objCmp.Count() != 0)
            //{
            //    AmountPaid = objCmp.ElementAt(0).AmountPaid;
            //}
            //else
            //{
            //    AmountPaid = null;
            //}
            return Json(objCmp.ElementAt(0).AmountPaid);
        }
        #endregion

        #region Remove Anchor Comppany Document
        /// <summary>
        /// Remove anchor company document
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteAnchorCompDoc(Int64 ID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            AnchorCompanyDocument anchorCompanyDocument = _AnchorCompanyRepository.GetFileNameById(ID);

            bool bResult = false;
            string documentFolderPath = _configuration["AnchorDocumentsFolder"];
            string exportFileName = anchorCompanyDocument.LocalFolderFileName;
            try
            {
                bResult = _CommonRepository.DeleteFile(documentFolderPath, exportFileName);
                if (bResult == true)
                {
                    _AnchorCompanyRepository.DeleteAnchorCompDocRecord(anchorCompanyDocument);
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

        /// <summary>
        /// Returning view of anchor company
        /// </summary>
        /// <returns></returns>
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public ActionResult AnchorCompListByVendorID()
        {
            return View("AnchorCompListing");
        }

        #region Get anchor companies list by vendor id
        /// <summary>
        ///  Get anchor companies list by vendor id
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
                var Res = _CommonRepository.AuditTrailLog("Anchor Company", "Anchor Company List", UserID,0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyID = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();

                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(VendorId, sortBy, pageSize, skip, CompanyID, CompanyName, "", "", 1);
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
        #endregion

        #region Get Vendor-Invoice List By CompanyID
        public ActionResult GetVendorInvoiceDetailsViewByVendorID(int icompanyID)
        {
            CompanyInvoiceListModel objCompanyInvoice = new CompanyInvoiceListModel();
            objCompanyInvoice.CompanyID = icompanyID;
            TempData["SelCompanyID"] = icompanyID;
            return View("CompanyInvoiceVendorList", objCompanyInvoice);
        }

        /// <summary>
        /// Getting vendor invoice list by id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetVendorInvoiceListByCompanyID(CompanyInvoiceListModel objCompanyInvoice)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                var Res = _CommonRepository.AuditTrailLog("Anchor Company", "Vendor Invoice List By Company Id", UserID, 0);
                int iSelCompanyId = Convert.ToInt32(objCompanyInvoice.CompanyID);
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var UserName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var UserRole = Request.Form["columns[6][search][value]"].FirstOrDefault() == "0" ? "" : Request.Form["columns[6][search][value]"].FirstOrDefault();

                IEnumerable<CompanyInvoiceListModel> lstVendorInvoiceList = _AnchorCompanyRepository.GetVendorInvoiceListByCompanyID(iSelCompanyId, sortBy, pageSize, skip);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstVendorInvoiceList != null && lstVendorInvoiceList.Count() != 0)
                {
                    recordsFiltered = lstVendorInvoiceList.ElementAt(0).FilteredRecord;
                    recordsTotal = lstVendorInvoiceList.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstVendorInvoiceList });
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

        /// <summary>
        /// Exporting anchor company list by vendor id
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public IActionResult ExportAnchorCompListByVendorID(string CompanyName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            string CompanyID="";
            try
            {
                var Res = _CommonRepository.AuditTrailLog("select Invoice", "Eligible Invoices Export CSV", UserID, 0);
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchorCompListingByVendorId(VendorId, sortBy, pageSize, skip, CompanyID, CompanyName, "", "", 1);
                string csv = ExportAnchorCompDatastr(lstAnchorCompany);
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
        /// Exporting anchor company data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportAnchorCompDatastr(IEnumerable<AnchorCompListingModel> objData)
        {
            return CommonService.ListToCSV(objData);
        }

        /// <summary>
        /// Exporting vendor invoice by company id
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public IActionResult ExportVendorInvoiceListByCompanyID(int CompanyID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var sortBy = "InvoiceNo asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<CompanyInvoiceListModel> lstVendorInvoiceList = _AnchorCompanyRepository.GetVendorInvoiceListByCompanyID(CompanyID, sortBy, pageSize, skip);

                string csv = ExportVendorInvoiceDatastr(lstVendorInvoiceList);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "VendorInvoiceByCompany.csv");
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
        /// Exporting vendor invoice
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportVendorInvoiceDatastr(IEnumerable<CompanyInvoiceListModel> objData)
        {
            return CommonService.ListToCSV(objData);
        }

        /// <summary>
        /// Get Discount anchor company list by vendor id
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
                var Res = _CommonRepository.AuditTrailLog("Home", "Discount Anchor Comapny List Verdor", UserID, 0);
                Int32? CompanyId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var TotalInvoiceAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<AnchorCompListingModel> lstAnchorCompany = _AnchorCompanyRepository.GetAnchordashboardCompListingByVendorId(CompanyId, sortBy, pageSize, skip, "", CompanyName, TotalInvoiceAmt, "Discount", 2);
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
        /// View for vendor listing
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult VendorListing()
        {
            return View("~/Views/AnchorCompany/VendorListing.cshtml");
        }

        /// <summary>
        /// Getting list of vendors
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetVendorListing()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Vendor", "Vendor Listing", UserID, 0);
                var iSelCompanyId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var UserName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var UserRole = Request.Form["columns[6][search][value]"].FirstOrDefault() == "0" ? "" : Request.Form["columns[6][search][value]"].FirstOrDefault();

                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var ContactNo = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var EmailId = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var VendorStatus = Request.Form["columns[4][search][value]"].FirstOrDefault();

                IEnumerable<VendorlistModel> lstVendorInvoiceList = _AnchorCompanyRepository.GetVendorListing(Convert.ToInt32(iSelCompanyId), sortBy, pageSize, skip, CompanyName, ContactNo, EmailId, VendorStatus);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstVendorInvoiceList != null && lstVendorInvoiceList.Count() != 0)
                {
                    recordsFiltered = lstVendorInvoiceList.ElementAt(0).FilteredRecord;
                    recordsTotal = lstVendorInvoiceList.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstVendorInvoiceList });
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

        #region Export To CSV VendorListing

        /// <summary>
        /// Exporting vendor to CSV
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="CompanyName"></param>
        /// <param name="InAmount"></param>
        /// <returns></returns>
        public IActionResult ExportVendorListToCSV(int CompanyId, string CompanyName, string InAmount)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Vendor", "CSV Export Vendor List ", UserID, 0);
              
                var ContactNo = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var EmailId = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var VendorStatus = Request.Form["columns[4][search][value]"].FirstOrDefault();
                Int32? VendorId = HttpContext.Session.GetInt32("UserID");
                var iSelCompanyId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "CompanyID asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<VendorlistModel> lstVendorList = _AnchorCompanyRepository.GetVendorListing(Convert.ToInt32(iSelCompanyId), sortBy, pageSize, skip, CompanyName, ContactNo, EmailId, VendorStatus);

                string csv = ExportVendorlist(lstVendorList);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Vendor.csv");
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
        /// Exporting vendor list
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportVendorlist(IEnumerable<VendorlistModel> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord,InvoiceLimitAmt");
        }

        #endregion

        // download the template for excel formate

        public IActionResult DownloadTemplate(string Tempate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Vendor", "Template Download", UserID, 0);
                string sWebRootFolder = _hostingEnvironment.WebRootPath;

                string sFileName = "";
                if (Tempate == "Vendor")
                { sFileName = @"VendorTemplate.xlsx"; }
                else { sFileName = @"InvoiceTemplate.xlsx"; }
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    var index = 1;
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet;
                    if (Tempate == "Vendor")
                    {
                        worksheet = package.Workbook.Worksheets.Add("Vendors");
                        //First add the headers                    
                        foreach (VendorHeaderModel item in Enum.GetValues(typeof(VendorHeaderModel)))
                        {
                            var Headers = item.ToString().Replace("_", " ").Replace("00", " (").Replace("1", ")").Replace("0", "/");
                            worksheet.Cells[1, index].Value = Headers;
                            worksheet.Cells[1, index].Style.Font.Size = 12;
                            worksheet.Cells[1, index].Style.Font.Bold = true;
                            worksheet.Cells[1, index].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                            worksheet.Cells[1, index].AutoFitColumns();
                            index++;
                        }
                    }
                    else
                    {
                        worksheet = package.Workbook.Worksheets.Add("Invoice");
                        foreach (InvoiceHeaderModel item in Enum.GetValues(typeof(InvoiceHeaderModel)))
                        {
                            //First add the headers
                            var Headers = item.ToString().Replace("_", " ").Replace("00", " (").Replace("1", ")").Replace("0", "/");
                            worksheet.Cells[1, index].Value = Headers;
                            worksheet.Cells[1, index].Style.Font.Size = 12;
                            worksheet.Cells[1, index].Style.Font.Bold = true;
                            worksheet.Cells[1, index].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                            worksheet.Cells[1, index].AutoFitColumns();
                            index++;
                        }
                    }
                    worksheet.Cells.AutoFitColumns();
                    package.Save(); //Save the workbook.
                }
                var result = PhysicalFile(Path.Combine(sWebRootFolder, sFileName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = file.Name
                }.ToString();

                return result;
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
        /// Uploading vendor
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadVendors()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Vendor", "Upload vendor", UserID, 0);
                var AnchCompanyId = HttpContext.Session.GetInt32("CompanyID");
                string CompanyName = HttpContext.Session.GetString("Companyname");
                IFormFile file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                DataTable dt = new DataTable();
                string JSONString = string.Empty;
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                    var FileName = "Vendor_"+CompanyName+"_"+ DateTime.Now.ToString("yyyyMMddhhmmss")+"_"+ AnchCompanyId;
                    ISheet sheet;
                    string fullPath = Path.Combine(newPath, FileName + sFileExtension);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;
                        if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }

                        //var Res = _AnchorCompanyRepository.InsertExcelPath(fullPath, FileName + sFileExtension, AnchCompanyId, CompanyName, "Vendor");

                        //    IRow headerRow = sheet.GetRow(0); //Get Header Row
                        //    int cellCount = headerRow.LastCellNum;
                        //    for (int j = 0; j < cellCount; j++)
                        //    {
                        //        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        //        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        //        dt.Columns.Add(headerRow.GetCell(j).ToString());
                        //    }
                        //    dt.Columns.Add("Message");

                        //    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                        //    {
                        //        DataRow dr = dt.NewRow();
                        //        IRow row = sheet.GetRow(i);
                        //        if (row == null) continue;
                        //        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        //        for (int j = row.FirstCellNum; j < cellCount; j++)
                        //        {
                        //            if (row.GetCell(j) != null)
                        //                dr[j] = row.GetCell(j).ToString();

                        //        }

                        //        dt.Rows.Add(dr);

                        //        if (dt.Rows[i - 1]["Vendor Name"].ToString() != "" && dt.Rows[i - 1]["Pan Number"].ToString() != "" && dt.Rows[i - 1]["Contact Person Name"].ToString() != "" && dt.Rows[i - 1]["Email ID"].ToString() != "" && dt.Rows[i - 1]["Contact Number"].ToString() != "")
                        //        {

                        //            if (!Regex.IsMatch(dt.Rows[i - 1]["Pan Number"].ToString(), @"^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$"))
                        //            {
                        //                dt.Rows[i - 1]["Message"] = "Pan Number is not valid";
                        //                continue;
                        //            }
                        //            else if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() != "")
                        //            {
                        //                if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() == "yes")
                        //                {
                        //                    if (dt.Rows[i - 1]["UAN Number"].ToString() == "")
                        //                    {
                        //                        dt.Rows[i - 1]["Message"] = "UAN Number should not be blank";
                        //                        continue;
                        //                    }
                        //                }
                        //            }
                        //            else if (dt.Rows[i - 1]["UAN Number"].ToString() != "")
                        //            {
                        //                if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() == "")
                        //                {
                        //                    dt.Rows[i - 1]["Message"] = "MSME should not be blank";
                        //                    continue;
                        //                }
                        //            }
                        //            else if (!Regex.IsMatch(dt.Rows[i - 1]["Email ID"].ToString(), @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                        //            {
                        //                dt.Rows[i - 1]["Message"] = "Email ID is not valid";
                        //                continue;
                        //            }

                        //            else
                        //            {
                        //                dt.Rows[i - 1]["Message"] = "Success";
                        //            }



                        //        }
                        //        else
                        //        {
                        //            if (dt.Rows[i - 1]["Vendor Name"].ToString() == "")
                        //            {
                        //                dt.Rows[i - 1]["Message"] = "Vendor Name should not be blank";
                        //                continue;
                        //            }
                        //            if (dt.Rows[i - 1]["Pan Number"].ToString() == "")
                        //            {
                        //                dt.Rows[i - 1]["Message"] = "Pan Number should not be blank";
                        //                continue;
                        //            }
                        //            if (dt.Rows[i - 1]["Contact Person Name"].ToString() == "")
                        //            {
                        //                dt.Rows[i - 1]["Message"] = "Contact Person Name should not be blank";
                        //                continue;
                        //            }
                        //            if (dt.Rows[i - 1]["Email ID"].ToString() == "")
                        //            {
                        //                dt.Rows[i - 1]["Message"] = "Email ID should not be blank";
                        //                continue;
                        //            }
                        //            if (dt.Rows[i - 1]["Contact Number"].ToString() == "")
                        //            {
                        //                dt.Rows[i - 1]["Message"] = "Contact Number should not be blank";
                        //                continue;
                        //            }
                        //        }
                        //        int Result = 0;
                        //        string randomPassword = _CommonRepository.GeneratePassword();
                        //        string Password = SecurityHelperService.Encrypt(randomPassword);
                        //        //var Result = _AnchorCompanyRepository.InsertVendorRecord(dr, AnchCompanyId, Password);
                        //        if (Result > 0)
                        //        {
                        //            string Template = string.Empty;
                        //            IEnumerable<GetVendorRegisterMailTemplate> lstAwaitedInvVendorsView = _companyRepository.GetVendorRegisterMailTemplate(Template);
                        //            string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                        //            string EMAIL_TOKEN_PAYMENT_LINK = "##$$PAYMENT_LINK$$##";
                        //            string paymentLink = "http://dotnet.brainvire.com/Finocart/Account/AdminLogin";///change url
                        //            //string MailStatus = string.Empty;
                        //            string emailToAddress = dr[6].ToString();
                        //            string subject = "Vendor registration";
                        //            WebClient client = new WebClient();
                        //            string startupPath = Environment.CurrentDirectory;

                        //            string body = path;
                        //            // string body = client.DownloadString(startupPath + "/Views/Template/EmailTemplate.cshtml");
                        //            body = body.Replace("@@User@@", dr[0].ToString());
                        //            body = body.Replace("@@PanNumber@@", dr[1].ToString());
                        //            body = body.Replace("@@ProjectName@@", "Finocart");
                        //            body = body.Replace("@@VendorName@@", dt.Rows[i - 1]["Vendor Name"].ToString());
                        //            body = body.Replace("@@AnchorCompanyname@@", CompanyName);
                        //            body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);
                        //            body = body.Replace("@@PanNumber@@", dt.Rows[i - 1]["Pan Number"].ToString());
                        //            body = body.Replace("@@Password@@", randomPassword);
                        //            IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                        //            _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);
                        //        }

                        //        if (Result == -1)
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "Pan Number already exists";
                        //            continue;
                        //        }

                        //    }
                        //    //    //GetLog(dt);
                    }
                }

                //JSONString = JsonConvert.SerializeObject(dt);
                //HttpContext.Session.SetString("Excel", JSONString);
                //return Json(new { result = dt });
                return Json(new { result = 1 });


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
        /// Get Log
        /// </summary>
        /// <param name="Log"></param>
        /// <returns></returns>
        public IActionResult GetLogs(Int64 ID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Vendor", "Upload Invoice", UserID, 0);
                //var keys = HttpContext.Session.GetString("Excel");
                //dynamic key = JsonConvert.DeserializeObject(keys);
                UploadExcelPath uploadExcelPaths = _AnchorCompanyRepository.GetUploadDetils(ID);
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = uploadExcelPaths.Logs;
                //if (Log == "Vendor")
                //{ sFileName = @"VendorLog.xlsx"; }
                //else { sFileName = @"InvoiceLog.xlsx"; }
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(sFileName);
                //if (file.Exists)
                //{
                //    file.Delete();
                //    file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                //}
                //using (ExcelPackage package = new ExcelPackage(file))
                //{

                //    var index = 1;
                //    ExcelWorksheet worksheet;
                //    // add a new worksheet to the empty workbook
                //    if (Log == "Vendor")
                //    {
                //        // add a new worksheet to the empty workbook
                //        worksheet = package.Workbook.Worksheets.Add("Vendors");
                //        //First add the headers                    
                //        foreach (VendorHeaderModel item in Enum.GetValues(typeof(VendorHeaderModel)))
                //        {
                //            var Headers = item.ToString().Replace("_", " ").Replace("00", " (").Replace("1", ")").Replace("0", "/");
                //            worksheet.Cells[1, index].Value = Headers;
                //            worksheet.Cells[1, index].Style.Font.Size = 12;
                //            worksheet.Cells[1, index].Style.Font.Bold = true;
                //            worksheet.Cells[1, index].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                //            index++;
                //        }
                //        for (int i = 0; i < key.Count; i++)
                //        {
                //            worksheet.Cells["A" + (i + 2)].Value = key[i]["Vendor Name"].Value;
                //            worksheet.Cells["B" + (i + 2)].Value = key[i]["Pan Number"].Value;
                //            worksheet.Cells["C" + (i + 2)].Value = key[i]["MSME  (Yes/No)"].Value;
                //            worksheet.Cells["D" + (i + 2)].Value = key[i]["UAN Number"].Value;
                //            worksheet.Cells["E" + (i + 2)].Value = key[i]["Contact Person Name"].Value;
                //            worksheet.Cells["F" + (i + 2)].Value = key[i]["Contact Person Designation"].Value;
                //            worksheet.Cells["G" + (i + 2)].Value = key[i]["Email ID"].Value;
                //            worksheet.Cells["H" + (i + 2)].Value = key[i]["Contact Number"].Value;
                //            worksheet.Cells["I" + (i + 2)].Value = key[i]["Message"].Value;
                //        }
                //    }
                //    else
                //    {
                //        worksheet = package.Workbook.Worksheets.Add("Invoice");
                //        foreach (InvoiceHeaderModel item in Enum.GetValues(typeof(InvoiceHeaderModel)))
                //        {
                //            //First add the headers
                //            var Headers = item.ToString().Replace("_", " ").Replace("00", " (").Replace("1", ")").Replace("0", "/");
                //            worksheet.Cells[1, index].Value = Headers;
                //            worksheet.Cells[1, index].Style.Font.Size = 12;
                //            worksheet.Cells[1, index].Style.Font.Bold = true;
                //            worksheet.Cells[1, index].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                //            index++;
                //        }
                //        for (int i = 0; i < key.Count; i++)
                //        {
                //            worksheet.Cells["A" + (i + 2)].Value = key[i]["PO Number"].Value;
                //            worksheet.Cells["B" + (i + 2)].Value = key[i]["PO Date (MM/DD/YYYY)"].Value;
                //            worksheet.Cells["C" + (i + 2)].Value = key[i]["Invoice Number"].Value;
                //            worksheet.Cells["D" + (i + 2)].Value = key[i]["Invoice Amount"].Value;
                //            worksheet.Cells["E" + (i + 2)].Value = key[i]["Payment Due Date (MM/DD/YYYY)"].Value;
                //            worksheet.Cells["F" + (i + 2)].Value = key[i]["Approved Amount"].Value;
                //            worksheet.Cells["G" + (i + 2)].Value = key[i]["Payment Days"].Value;
                //            worksheet.Cells["H" + (i + 2)].Value = key[i]["Pan Number"].Value;
                //            worksheet.Cells["I" + (i + 2)].Value = key[i]["Message"].Value;
                //        }
                //    }
                //    worksheet.Cells[1, index].Value = "Message";
                //    worksheet.Cells[1, index].Style.Font.Size = 12;
                //    worksheet.Cells[1, index].Style.Font.Bold = true;
                //    worksheet.Cells[1, index].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                //    worksheet.Cells.AutoFitColumns();
                //    package.Save(); //Save the workbook.
                //}
                var result = PhysicalFile(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = file.Name
                }.ToString();

                return result;

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
        /// Upload invoice by vendor id
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public JsonResult UploadInvoice()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Vendor", "Upload Invoice  ", UserID, 0);
                var AnchCompanyId = HttpContext.Session.GetInt32("CompanyID");
            var AnchCompanyName = HttpContext.Session.GetString("Companyname");
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            DataTable dt = new DataTable();
            string JSONString = string.Empty;
            var EmailID = "";
            var VendorName = "";
            bool sendMail = false;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                var FileName = "Invoice_" + AnchCompanyName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + AnchCompanyId;
                ISheet sheet;
                string fullPath = Path.Combine(newPath, FileName + sFileExtension);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {

                        file.CopyTo(stream);
                        stream.Position = 0;
                        if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }

                        //var Res = _AnchorCompanyRepository.InsertExcelPath(fullPath, FileName + sFileExtension, AnchCompanyId, AnchCompanyName, "Invoice");

                        //IRow headerRow = sheet.GetRow(0); //Get Header Row
                        //int cellCount = headerRow.LastCellNum;

                        //for (int j = 0; j < cellCount; j++)
                        //{
                        //    NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        //    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        //    dt.Columns.Add(headerRow.GetCell(j).ToString());
                        //}
                        //dt.Columns.Add("Message");
                        //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                        //{
                        //    DataRow dr = dt.NewRow();
                        //    IRow row = sheet.GetRow(i);
                        //    if (row == null) continue;
                        //    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        //    for (int j = row.FirstCellNum; j < cellCount; j++)
                        //    {
                        //        if (row.GetCell(j) != null)
                        //            dr[j] = row.GetCell(j).ToString();

                        //    }
                        //    dt.Rows.Add(dr);
                        //    DateTime date;
                        //    if (dt.Rows[i - 1]["PO Number"].ToString() != "" && dt.Rows[i - 1]["PO Date (DD/MM/YYYY)"].ToString() != "" && dt.Rows[i - 1]["Invoice Number"].ToString() != "" && dt.Rows[i - 1]["Invoice Amount"].ToString() != "" && dt.Rows[i - 1]["Payment Due Date (DD/MM/YYYY)"].ToString() != "" && dt.Rows[i - 1]["Approved Amount"].ToString() != "" && dt.Rows[i - 1]["Payment Days"].ToString() != "")
                        //    {
                        //        if (!DateTime.TryParseExact(dt.Rows[i - 1]["PO Date (DD/MM/YYYY)"].ToString(), "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "PO Date should be in DD/MM/YYYY format";
                        //            continue;
                        //        }
                        //        else if (!DateTime.TryParseExact(dt.Rows[i - 1]["Payment Due Date (DD/MM/YYYY)"].ToString(), "dd/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out date))
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "Payment Due Date should be in DD/MM/YYYY format";
                        //            continue;
                        //        }
                        //        else
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "Success";
                        //        }

                        //    }
                        //    else
                        //    {
                        //        if (dt.Rows[i - 1]["PO Number"].ToString() == "")
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "PO Number should not be blank";
                        //            continue;
                        //        }
                        //        if (dt.Rows[i - 1]["PO Date (DD/MM/YYYY)"].ToString() == "")
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "PO Date should not be blank";
                        //            continue;
                        //        }
                        //        if (dt.Rows[i - 1]["Invoice Number"].ToString() == "")
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "Invoice Number should not be blank";
                        //            continue;
                        //        }
                        //        if (dt.Rows[i - 1]["Invoice Amount"].ToString() == "")
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "Invoice Amount should not be blank";
                        //            continue;
                        //        }
                        //        if (dt.Rows[i - 1]["Payment Due Date (DD/MM/YYYY)"].ToString() == "")
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "Payment Due Date should not be blank";
                        //            continue;
                        //        }
                        //        if (dt.Rows[i - 1]["Approved Amount"].ToString() == "")
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "Approved Amount should not be blank";
                        //            continue;
                        //        }
                        //        if (dt.Rows[i - 1]["Payment Days"].ToString() == "")
                        //        {
                        //            dt.Rows[i - 1]["Message"] = "Payment Days should not be blank";
                        //            continue;
                        //        }
                        //    }

                        //    Company objDatawithSP = _AnchorCompanyRepository.CheckEmailID(row.Cells[7].ToString());
                        //    if (objDatawithSP != null)
                        //    {
                        //        EmailID = objDatawithSP.Contact_email;
                        //        VendorName = objDatawithSP.Company_name;
                        //    }
                        //    else
                        //    {
                        //        dt.Rows[i - 1]["Message"] = "Pan Number is not valid";
                        //        continue;
                        //    }
                        //    var Result = _AnchorCompanyRepository.InsertInvoiceRecord(row, AnchCompanyId);


                        //    if (Result == -1)
                        //    {
                        //        dt.Rows[i - 1]["Message"] = "Invoice Number and PO Number already Exists";
                        //        continue;
                        //    }
                        //    sendMail = true;
                        //}

                        ////sending email
                        //if (sendMail)
                        //{

                        //    IEnumerable<GetInvoiceRegisterMailTemplate> lstAwaitedInvVendorsView = _companyRepository.GetInvoiceRegisterMailTemplate("InvoiceUploadEmailToVendor");
                        //    string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
                        //    string tag = lstAwaitedInvVendorsView.ElementAt(0).TableTag;
                        //    string EMAIL_TOKEN_PAYMENT_LINK = "##$$PAYMENT_LINK$$##";
                        //    string paymentLink = "http://dotnet.brainvire.com/Finocart/Account/AdminLogin";///change url
                        //    string MailStatus = string.Empty;
                        //    string subject = "Vendor registration";
                        //    WebClient client = new WebClient();
                        //    string startupPath = Environment.CurrentDirectory;
                        //    string body = path;
                        //    StringBuilder sb = new StringBuilder();
                        //    //IEnumerable<GetUploadVendorListModel1> lstAwaitedInvVendorsView1 = _companyRepository.GetUploadVendorList1(VendorID);
                        //    //for (int i = 1; i < 5; i++)
                        //    //{
                        //    body = body.Replace("@@VendorName@@", VendorName);
                        //    body = body.Replace("@@AnchorName@@", AnchCompanyName);
                        //    body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);
                        //    //body = body.Replace("@@PODate(MM/DD/YYYY)@@", "Abc");
                        //    //body = body.Replace("@@InvoiceNumber@@", "Abc");
                        //    //body = body.Replace("@@InvoiceAmount@@", "Abc");
                        //    //body = body.Replace("@@PaymentDueDate(MM/DD/YYYY)@@", "Abc");
                        //    //body = body.Replace("@@ApprovedAmount@@", "Abc");
                        //    //body = body.Replace("@@PaymentDays@@", "Abc");


                        //    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
                        //    _CommonRepository.SendEmail(lookupDetails, EmailID, subject, body, true);

                        //}
                    }
                }
                //JSONString = JsonConvert.SerializeObject(dt);
                //HttpContext.Session.SetString("Excel", JSONString);
                //if (file.FileName == "InvoiceTemplate.xlsx")
                //{

                //    file.FileName.Remove(1);

                //}
                return Json(new { result = 1 });
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

        //#region Get Company Result
        ///// <summary>
        ///// Company View
        ///// </summary>
        ///// <returns></returns>
        //[JWTAuthorize("Account", "SuperAdminLogin")]
        //[CustomFilter]
        //public IActionResult CompanyList()
        //{
        //    //if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == "SuperAdmin")
        //    //{
        //    ViewBag.LookUp = (_companyRepository.GetLookUpDropDown()).ToList();
        //    return View();
        //    //}
        //    //else
        //    //{
        //    //    return RedirectToAction("SuperAdminLogin", "Account");
        //    //}
        //}
        //#endregion
        #region Get Company Result
        [JWTAuthorize("Account", "SuperAdminLogin")]
        [CustomFilter]
        public IActionResult Factoring()
        {
            //if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == "SuperAdmin")
            //{
            ViewBag.LookUp = (_companyRepository.GetLookUpDropDown()).ToList();
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("SuperAdminLogin", "Account");
            //}
        }
        #endregion
        #region Get Register List

        /// <summary>
        /// Saving company data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCompanyData()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            var Res = _CommonRepository.AuditTrailLog("User Listing", "Listing Page", UserID, 0);
            string ErrorMessage = string.Empty;
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
                var InterestedAs = Request.Form["columns[6][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                string name = "factoring";
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<GetCompanyModel> cm = _companyRepository.GetCompanyListing(sortBy, pageSize, skip, CompanyName, InterestedAs, name);
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
                throw ex;
            }
          

        }
        #endregion
        /// <summary>
        ///view of resversefactory user
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public ActionResult ReverseFactoring()
        {
            return View();

        }

        /// <summary>
        /// list of reverse userlisting 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetReverseCompanyData()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            var Res = _CommonRepository.AuditTrailLog("User Listing", "Listing Page", UserID, 0);
            string ErrorMessage = string.Empty;
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
                var InterestedAs = Request.Form["columns[6][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                string name = "Reversefactoring";
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<GetCompanyModel> cm = _companyRepository.GetCompanyListing(sortBy, pageSize, skip, CompanyName, InterestedAs, name);
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
                throw ex;
            }


        }
        #region Get Invoice Pending For Action View Page
        /// <summary>
        /// Company view by id
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public IActionResult ComapanyView(int CompanyID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            VendorAwaitedInvApprovalModel vendorAwaitedInvApproval = new VendorAwaitedInvApprovalModel();
            try
            {

                vendorAwaitedInvApproval.VendorID = CompanyID;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");

            }
            return View("~/Views/AnchorCompany/DiscApprovalAwaitedInvoiceView.cshtml", vendorAwaitedInvApproval);
        }
        #endregion

        #region EDIT details of user

        /// <summary>
        /// Edit company page by company id
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public ActionResult EditCompanyPage(string CompanyIDD)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
        

            Company objComapnyModel = new Company();
           
                var data = IsBase64Encoded(CompanyIDD);
                if (data == true)
                {
                 
                    var result = Convert.FromBase64String(CompanyIDD);
                    int CompanyIDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                    ViewBag.LookUp = (_companyRepository.GetLookUpDropDown()).ToList();
                    ViewBag.look = (_companyRepository.GetFactoryOrReverse()).ToList();
                    ViewBag.Details = (_companyRepository.GetCuurentStatus()).ToList();
                    objComapnyModel = _companyRepository.EditPage(CompanyIDs);
                    var Res = _CommonRepository.AuditTrailLog("User Listing", "Edit", UserID,CompanyIDs);
                    return View("Register", objComapnyModel);
                }
                else
                {
                    return View("~/Views/User/PageNotFound.cshtml");
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
        /// <summary>
        /// soft delete of company data
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="panNumber"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCompany(string companyName,string panNumber,int id)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {


                Company objComapnyModel = new Company();
                objComapnyModel.Company_name = companyName;
                objComapnyModel.Pan_number = panNumber;
                var result = _companyRepository.DeleteCompanyById(companyName, panNumber);

                return RedirectToAction("Factoring", "AnchorCompany");
               
                
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

        #region Export To CSV Register

        /// <summary>
        /// Exporting data to CSV
        /// </summary>
        /// <param name="ContactPerson"></param>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public IActionResult ExportRegisterToCSV(string InterestedAs, string CompanyName)
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
                string name = "factoring";

                IEnumerable<GetCompanyModel> lstAnchorCompany = _companyRepository.GetCompanyListing(sortBy, pageSize, skip, CompanyName, InterestedAs, name);

                string csv = ExportRegister(lstAnchorCompany);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Register.csv");
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
        public IActionResult ExportReverseFactoring(string InterestedAs, string CompanyName)
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
                string name = "Reversefactoring";

                IEnumerable<GetCompanyModel> lstAnchorCompany = _companyRepository.GetCompanyListing(sortBy, pageSize, skip, CompanyName, InterestedAs, name);

                string csv = ExportRegister(lstAnchorCompany);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Register.csv");
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
        /// Export register
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportRegister(IEnumerable<GetCompanyModel> objData)
        {
            return CommonService.ListToCSV(objData);
        }

        #endregion

        #region Upload Vendor List 

        /// <summary>
        /// Uploading vendor listing
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IActionResult UploadVendorListing(string VendorID, string VendorCompany)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            VendorAwaitedInvApprovalModel vendorAwaitedInvApproval = new VendorAwaitedInvApprovalModel();
            try
            {
                
                var Res = _CommonRepository.AuditTrailLog("Vendor", "Vendor Listing View Action List", UserID, 0);
                var data = IsBase64Encoded(VendorID);
                var data1 = IsBase64Encoded(VendorCompany);
                if (data == true && data1 == true)
                {
                    var result = Convert.FromBase64String(VendorID);
                    int VendorIDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                    var resultVendorCom = Convert.FromBase64String(VendorCompany);
                    string VendorComs = Convert.ToString(Encoding.UTF8.GetString(resultVendorCom));
                    vendorAwaitedInvApproval.VendorID = VendorIDs;
                    vendorAwaitedInvApproval.VendorName = VendorComs;
                    return View("~/Views/AnchorCompany/UploadVendorListing.cshtml", vendorAwaitedInvApproval);
                }
                else
                {
                    return View("~/Views/User/PageNotFound.cshtml");
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

        #region Get Upload Vendor View List
        /// <summary>
        /// Getting upload vendor list
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUploadVendorList(string VendorID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var data = IsBase64Encoded(VendorID);
                if (data == true)
                {
                    var result = Convert.FromBase64String(VendorID);
                    string VendorIDs = Encoding.UTF8.GetString(result);
                    Int64? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                    Int64? AnchorCompID = HttpContext.Session.GetInt32("AnchorCompID");
                    var draw = Request.Form["draw"].FirstOrDefault();
                    var start = Request.Form["start"].FirstOrDefault();
                    var length = Request.Form["length"].FirstOrDefault();
                    //Get Sort columns value
                    var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    var POName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                    var InvoiceDate = Request.Form["columns[2][search][value]"].FirstOrDefault();
                    var INVOICENO = Request.Form["columns[3][search][value]"].FirstOrDefault();
                    var InvoiceAmt = Request.Form["columns[4][search][value]"].FirstOrDefault();
                    var ApprovedAmt= Request.Form["columns[6][search][value]"].FirstOrDefault();
                    var InvoiceStatus = Request.Form["columns[7][search][value]"].FirstOrDefault();
                    var PaymentDays = Request.Form["columns[8][search][value]"].FirstOrDefault();
                    IEnumerable<GetUploadVendorListModel> lstAwaitedInvVendorsView = _companyRepository.GetUploadVendorList(CompanyID, VendorIDs, AnchorCompID, POName, InvoiceDate, INVOICENO, InvoiceAmt, ApprovedAmt, InvoiceStatus, PaymentDays, sortBy, pageSize, skip);
                    int? recordsFiltered = 0;
                    int? recordsTotal = 0;

                    if (lstAwaitedInvVendorsView != null && lstAwaitedInvVendorsView.Count() != 0)
                    {
                        recordsFiltered = lstAwaitedInvVendorsView.ElementAt(0).FilteredRecord;
                        recordsTotal = lstAwaitedInvVendorsView.ElementAt(0).TotalRecord;
                    }
                    return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAwaitedInvVendorsView });
                }
                else
                {
                    return Json(null);
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
        #endregion

        #region Export To CSV Upload Vendor
        /// <summary>
        /// Exporting vendor to CSV
        /// </summary>
        /// <param name="VendorID"></param>
        /// <param name="CompanyID"></param>
        /// <param name="POName"></param>
        /// <param name="INVOICENO"></param>
        /// <returns></returns>
        public IActionResult ExportUploadVendorToCSV(string VendorID, string POName, string INVOICENO)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            string csv = "";
            try
            {
                var data = IsBase64Encoded(VendorID);
                if (data == true)
                {
                    var result = Convert.FromBase64String(VendorID);
                    string VendorIDs = Encoding.UTF8.GetString(result);
                    var Res = _CommonRepository.AuditTrailLog("Vedor", "Vendor Listing,Export CSV", UserID, 0);
                    Int64? VendorId = HttpContext.Session.GetInt32("UserID");
                    Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                    Int64? AnchorCompID = HttpContext.Session.GetInt32("AnchorCompID");
                    var sortBy = "CompanyID asc";
                    int pageSize = 100000;
                    int skip = 0;
                   
                    var InvoiceDate = Request.Form["columns[2][search][value]"].FirstOrDefault();
                    var InvoiceAmt = Request.Form["columns[4][search][value]"].FirstOrDefault();
                    var ApprovedAmt = Request.Form["columns[6][search][value]"].FirstOrDefault();
                    var InvoiceStatus = Request.Form["columns[7][search][value]"].FirstOrDefault();
                    var PaymentDays = Request.Form["columns[8][search][value]"].FirstOrDefault();
                    IEnumerable<GetUploadVendorListModel> lstAwaitedInvVendorsView = _companyRepository.GetUploadVendorList(CompanyID, VendorID, AnchorCompID,POName,InvoiceDate, INVOICENO, InvoiceAmt,ApprovedAmt,InvoiceStatus,PaymentDays,sortBy,pageSize,skip);

                    csv = ExportUploadVendor(lstAwaitedInvVendorsView);
                    

                }
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Upload-Vendor.csv");
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


        public string ExportUploadVendor(IEnumerable<GetUploadVendorListModel> objData)
        {
            return CommonService.ListToCSV(objData);
        }

        #endregion

        #region Audit Trail Log List Details
        /// <summary>
        /// view audit log
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "SuperAdminLogin")]
        [CustomFilter]
        public IActionResult AuditTrailLogList()
        {
            return View("~/Views/Account/AuditTrailLog.cshtml");
        }
        #endregion

        #region Get Register List
        /// <summary>
        /// listing of AuditTrail
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAuditTrailList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Audit Log Listing", "Audit Log Listing", UserID, 0);

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
                recordsFiltered =0;
                recordsTotal = 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<GetAuditTrailLogModel> cm = _companyRepository.GetAuditTrailLogListing(sortBy, pageSize, skip, CompanyName, ContactPerson);
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
                throw ex;
            }


        }
        #endregion

        #region Send Grid VIEW AND EDIT
        /// <summary>
        /// edit audit trail log
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "SuperAdminLogin")]
        [CustomFilter]
        public IActionResult SendGridEdit()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            EditSendGridDetailsModel objComapnyModel = new EditSendGridDetailsModel();
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Control Panel", "SendGridEdit", UserID, 0);

                objComapnyModel = _companyRepository.SendGridEditPage();
                    return View("~/Views/AnchorCompany/SendGridAddUpdate.cshtml", objComapnyModel);
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

        #region Update Send Grid 
        /// <summary>
        /// Update audit traillog
        /// </summary>
        /// <param name="objSendGridModel"></param>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult UpdateSendGrid(EditSendGridDetailsModel objSendGridModel, IFormCollection formCollection)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Control Panel", "SendGridUpdate", UserID, 0);
                var Result = _companyRepository.UpdateSendGrid(objSendGridModel);
                TempData["success"] = "success";
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return RedirectToAction("SendGridEdit", "AnchorCompany");
        }

        #endregion

        #region Profile
        /// <summary>
        /// dislpay  user profile
        /// </summary>
        /// <returns></returns>
        public ActionResult Profile()
        {

            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            var RoleName = HttpContext.Session.GetString("Role");
            Company objComapnyModel = new Company();
            UserModel objUserModel = new UserModel();
            string ErrorMessage = string.Empty;
            try
            {
                if (RoleName == "MasterAdmin")
                {
                    var Res1 = _CommonRepository.AuditTrailLog("Profile", "Admin Master", UserID, 0);
                    ViewBag.LookUp = (_companyRepository.GetLookUpDropDown()).ToList();
                    objComapnyModel = _companyRepository.ProfileEditPage(UserID);
                    var Res = _CommonRepository.AuditTrailLog("Profile", "Edit", UserID, 0);
                    return View("AdminMasterProfile", objComapnyModel);
                }
                else
                {

                    objUserModel = _companyRepository.ProfileUserEditPage(UserID);
                    IEnumerable<LookupDetails> lstUserStatus = _lookUpRepository.getLookupDetailByKey("UserStatus");
                    IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("AccessView");
                    IEnumerable<LookupDetails> lstRoleType = _lookUpRepository.getLookupDetailByKey("RoleType");
                    Tuple<UserModel, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>> objUserModelData =
                    new Tuple<UserModel, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>, IEnumerable<LookupDetails>>(objUserModel, lstUserStatus, lookupDetails, lstRoleType);
                    return View("UserMasterProfile", objUserModelData);
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

        #region Update Admin Master Profile
        /// <summary>
        /// update admin profile
        /// </summary>
        /// <param name="objCompany"></param>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(Company objCompany, IFormCollection formCollection)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res1 = _CommonRepository.AuditTrailLog("Profile", "User Master", UserID, 0);
                objCompany.CompanyID = Convert.ToInt32(UserID);
                var Result = _companyRepository.UpdateProfile(objCompany);
                TempData["success"] = "success";
               
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
            return RedirectToAction("UserList", "User");
        }

        #endregion

        #region Update User Profile
       /// <summary>
       /// Update user profile
       /// </summary>
       /// <param name="objUserPage"></param>
       /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProfileUser(UserModel objUserPage)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            var RoleName = HttpContext.Session.GetString("RoleName");

            try
            {
                objUserPage.CompanyID = Convert.ToInt32(UserID);
                var Result = _companyRepository.UpdateUserProfile(objUserPage);
                
                if(objUserPage.AccessViewID == 9)
                {
                    return RedirectToAction("VendorDashboardMain", "Vendor");
                }

                else if(objUserPage.AccessViewID == 25)
                {

                    return RedirectToAction("BankDashboard", "BankCompany");
                }

               else
                {
                    return RedirectToAction("AnchorDashboard", "AnchorCompany");
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

            //return RedirectToAction("UserList", "User");
        }



        #endregion


        #region Check Anchor Rate
        /// <summary>
        /// check anchor rate
        /// </summary>
        /// <param name="AnchorRate"></param>
        /// <returns></returns>
        public ActionResult CheckAnchorRate(string AnchorRate)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            var AnchCompanyId = HttpContext.Session.GetInt32("CompanyID");
            try
            {
                decimal arate = Convert.ToDecimal(AnchorRate);
                var result = _companyRepository.CheckAnchorRate(AnchCompanyId, arate);
                return Json(result);
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

        /// <summary>
        /// Diplsy view logs
        /// </summary>
        /// <returns></returns>
        /// 
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult VendorInvoiceLogsListing()
        {
            return View("VendorInvoiceLogsList");
        }
        /// <summary>
        /// listing of InvoiceLogs
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetVendorInvoiceLogsList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Res = _CommonRepository.AuditTrailLog("Upload Excel log List", "Vendor Invoice Log List", UserID, 0);
                Int32? CompanyId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                //var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                var sortBy = "CreatedDate desc";
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var Upload = Request.Form["columns[2][search][value]"].FirstOrDefault();
                //var TotalInvoiceAmt = Request.Form["columns[3][search][value]"].FirstOrDefault();

                IEnumerable<UploadLogsModel> lstUploadLogs = _AnchorCompanyRepository.GetVendorInvoiceLogs(sortBy, pageSize, skip, CompanyId, Upload);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstUploadLogs != null && lstUploadLogs.Count() != 0)
                {
                    recordsFiltered = lstUploadLogs.ElementAt(0).FilteredRecord;
                    recordsTotal = lstUploadLogs.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstUploadLogs });
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

        #region Get Company Bank View Result
        /// <summary>
        /// Display company view
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public ActionResult CompanyBankViewList(string CompanyID)
        {
            var data = IsBase64Encoded(CompanyID);
            if (data == true)
            {

                var result = Convert.FromBase64String(CompanyID);
                int CompanyIDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                CompanyBankView companyBankView = new CompanyBankView();
                companyBankView.CompanyID = CompanyIDs;
                return View(companyBankView);
            }
            else
            {
                return View("~/Views/User/PageNotFound.cshtml");
            }
        }
        #endregion

        #region Get Company Bank View List

        /// <summary>
        /// Saving company data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCompanyBankViewList(CompanyBankView companyBankView)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            var Res = _CommonRepository.AuditTrailLog("User Listing", "Listing Page", UserID, 0);
            string ErrorMessage = string.Empty;
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
                var Search = Request.Form["columns[0][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<CompanyBankView> cm = _companyRepository.GetCompanyBankListing(sortBy, pageSize, skip, companyBankView.CompanyID, Search);
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
                throw ex;
            }


        }
        /// <summary>
        /// Export to Excel companylist
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public IActionResult ExportCompanyBankViewToCSV(string Search, Int64 CompanyID)
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
                IEnumerable<CompanyBankView> lstCompanyBankView = _companyRepository.GetCompanyBankListing(sortBy, pageSize, skip, CompanyID, Search);

                string csv = ExportCompanyBankView(lstCompanyBankView);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "AnchorCompanyBanks.csv");
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
        /// Export register
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportCompanyBankView(IEnumerable<CompanyBankView> objData)
        {
            return CommonService.ListToCSV(objData,"FilteredRecord,TotalRecord");
        }
        [CustomFilter]

        [JWTAuthorize("Account", "SuperAdminLogin")]
        public IActionResult SuperAdminDashBoard()
        {
           
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            string vendorlist = "Vendor";
            IEnumerable<AnchorVendorBankCount> vendor = _AnchorCompanyRepository.GetVendorAnchorBankCount(vendorlist);
            ViewBag.vendercount = vendor.Count();

            string AnchorCompanylist = "Anchor Company";
            IEnumerable<AnchorVendorBankCount> Anchorlist = _AnchorCompanyRepository.GetVendorAnchorBankCount(AnchorCompanylist);
            ViewBag.AnchorCompanylist = Anchorlist.Count();


            string BankCompany = "Bank";
            IEnumerable<AnchorVendorBankCount> Banklist = _AnchorCompanyRepository.GetVendorAnchorBankCount(BankCompany);
            ViewBag.BankCompany = Banklist.Count();


            string AnchorVendorcount = "Both";
            IEnumerable<AnchorVendorBankCount> AnchorVendorlist = _AnchorCompanyRepository.GetVendorAnchorBankCount(AnchorVendorcount);
            ViewBag.AnchorVendorcount = AnchorVendorlist.Count();




            return View("~/Views/SuperAdmin/SuperAdminDashBoard.cshtml");


        }
        #endregion

        /// <summary>
        /// View for vendor listing
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyVendorListing(string CompanyIDD,string CompanyName)
        {
            var data = IsBase64Encoded(CompanyIDD);
            var data1 = IsBase64Encoded(CompanyName);
            if (data == true && data1 == true)
            {

                var result = Convert.FromBase64String(CompanyIDD);
                var result1 = Convert.FromBase64String(CompanyName);
                int CompanyIDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                string CompanyNames = Convert.ToString(Encoding.UTF8.GetString(result1));
                VendorlistModel vendorlistModel = new VendorlistModel();
                vendorlistModel.CompanyID = CompanyIDs;
                vendorlistModel.Company_name = CompanyNames;
                return View(vendorlistModel);
            }
            else
            {
                return View("~/Views/User/PageNotFound.cshtml");
            }
        }

        /// <summary>
        /// Getting list of vendors
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCompanyVendorListing(VendorlistModel vendorlistModel)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var iSelCompanyId = vendorlistModel.CompanyID;
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var UserName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var UserRole = Request.Form["columns[6][search][value]"].FirstOrDefault() == "0" ? "" : Request.Form["columns[6][search][value]"].FirstOrDefault();

                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var ContactNo = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var EmailId = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var VendorStatus = Request.Form["columns[4][search][value]"].FirstOrDefault();

                IEnumerable<VendorlistModel> lstVendorInvoiceList = _AnchorCompanyRepository.GetVendorListing(Convert.ToInt32(iSelCompanyId), sortBy, pageSize, skip, CompanyName, ContactNo, EmailId, VendorStatus);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstVendorInvoiceList != null && lstVendorInvoiceList.Count() != 0)
                {
                    recordsFiltered = lstVendorInvoiceList.ElementAt(0).FilteredRecord;
                    recordsTotal = lstVendorInvoiceList.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstVendorInvoiceList });
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
        /// Vendor Company View
        /// </summary>
        /// <param name="CompanyIDD"></param>
        /// <returns></returns>

        public IActionResult VendorCompanyListing(string CompanyIDD , string CompanyName)
        {
            var data = IsBase64Encoded(CompanyIDD);
            var data1 = IsBase64Encoded(CompanyName);
            if (data == true && data1 == true)
            {

                var result = Convert.FromBase64String(CompanyIDD);
                var result1 = Convert.FromBase64String(CompanyName);
                int CompanyIDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                string CompanyNames = Convert.ToString(Encoding.UTF8.GetString(result1));
                VendorlistModel vendorlistModel = new VendorlistModel();
                vendorlistModel.VendorId = CompanyIDs;
                vendorlistModel.Company_name = CompanyNames;
                return View(vendorlistModel);
            }
            else
            {
                return View("~/Views/User/PageNotFound.cshtml");
            }
        }

        /// <summary>
        /// listing of vendor company
        /// </summary>
        /// <param name="anchorListModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetVendorCompanyListing(AnchorListModel anchorListModel)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var iSelCompanyId = anchorListModel.VendorId;
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var UserName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var UserRole = Request.Form["columns[6][search][value]"].FirstOrDefault() == "0" ? "" : Request.Form["columns[6][search][value]"].FirstOrDefault();

                var CompanyName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var InAmount = Request.Form["columns[3][search][value]"].FirstOrDefault();


                IEnumerable<AnchorListModel> lstVendorInvoiceList = _AnchorCompanyRepository.GetVendorAnchorListing(Convert.ToInt32(iSelCompanyId), sortBy, pageSize, skip, CompanyName, InAmount);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstVendorInvoiceList != null && lstVendorInvoiceList.Count() != 0)
                {
                    recordsFiltered = lstVendorInvoiceList.ElementAt(0).FilteredRecord;
                    recordsTotal = lstVendorInvoiceList.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstVendorInvoiceList });
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

        public IActionResult ExportVendorCompanyToCSV(Int32 CompanyIDD, string companyName, string InAmount)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            string csv = "";
            try
            {
                //var data = IsBase64Encoded(CompanyIDD);
                //if (data == true)
                //{

                    //var result = Convert.FromBase64String(CompanyIDD);
                    //int CompanyIDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                    var Res = _CommonRepository.AuditTrailLog("Vedor", "Vendor Listing,Export CSV", UserID, 0);
                    //Int64? VendorId = HttpContext.Session.GetInt32("UserID");
                    //Int32? CompanyID = HttpContext.Session.GetInt32("CompanyID");
                    //Int64? AnchorCompID = HttpContext.Session.GetInt32("AnchorCompID");
                    var sortBy = "CompanyID asc";
                    int pageSize = 100000;
                    int skip = 0;
                    IEnumerable<AnchorListModel> lstVendorInvoiceList = _AnchorCompanyRepository.GetVendorAnchorListing(CompanyIDD, sortBy, pageSize, skip, companyName, InAmount);

                    csv = ExportVendorCompany(lstVendorInvoiceList);


                //}
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "AnchorCompany.csv");
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


        public string ExportVendorCompany(IEnumerable<AnchorListModel> objData)
        {
            return CommonService.ListToCSV(objData, "FilteredRecord,TotalRecord");
        }


        /// <summary>
        /// View of bank
        /// </summary>
        /// <param name="CompanyIDD"></param>
        /// <returns></returns>
        public ActionResult BankCompanyViewList(string CompanyIDD)
        {
            var data = IsBase64Encoded(CompanyIDD);
            if (data == true)
            {

                var result = Convert.FromBase64String(CompanyIDD);
                int CompanyIDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                BankCompanyView bankCompanyView = new BankCompanyView();
                bankCompanyView.AnchorCompId = CompanyIDs;
                return View(bankCompanyView);
            }
            else
            {
                return View("~/Views/User/PageNotFound.cshtml");
            }
        }

        /// <summary>
        /// listing of banks
        /// </summary>
        /// <param name="bankCompanyView"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBankCompanyViewList(BankCompanyView bankCompanyView)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            var Res = _CommonRepository.AuditTrailLog("User Listing", "Listing Page", UserID, 0);
            string ErrorMessage = string.Empty;
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
                var Search = Request.Form["columns[0][search][value]"].FirstOrDefault();
                recordsFiltered = 0;
                recordsTotal = 0;
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                IEnumerable<BankCompanyView> cm = _companyRepository.GetBankCompanyListing(sortBy, pageSize, skip, bankCompanyView.AnchorCompId, Search);
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
                throw ex;
            }


        }


        public IActionResult SetLimit()
        {
            return View("SetLimit");
        }

        public IActionResult BothVendorAnchorList(string CompanyIDD, string CompanyName)
        {
            var data = IsBase64Encoded(CompanyIDD);
            var data1 = IsBase64Encoded(CompanyName);
            //var data2 = IsBase64Encoded(CompanyType);
            if (data == true && data1 == true)
            {

                var result = Convert.FromBase64String(CompanyIDD);
                var result1 = Convert.FromBase64String(CompanyName);
                //var result2 = Convert.FromBase64String(CompanyType);

                int CompanyIDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                string CompanyNames = Convert.ToString(Encoding.UTF8.GetString(result1));
               // string CompanyTypes = Convert.ToString(Encoding.UTF8.GetString(result2));

                VendorlistModel vendorlistModel = new VendorlistModel();
                vendorlistModel.VendorId = CompanyIDs;
                vendorlistModel.Company_name = CompanyNames;
                //vendorlistModel.Company_type = CompanyTypes;
                return View(vendorlistModel);
            }
            else
            {
                return View("~/Views/User/PageNotFound.cshtml");
            }
        }
       
        
        //[JWTAuthorize("Account", "UserLogin")]
        public ActionResult HolidayList()
        {
            return View();
        }
        public JsonResult GetHolidayListing()
        {


            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            int?  UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                var draw = Request.Form["draw"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();
                var Reason = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                recordsFiltered = 0;
                recordsTotal = 0;
                IEnumerable<HolidayListModel> lstAnchorHolidayList = _AnchorCompanyRepository.GetAnchorHolidayListing(sortBy, pageSize, skip,Reason,UserID);

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

        [HttpPost]
        public JsonResult InsertHolidate(int ID, string holidate, string reason)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                int objCmp = 0;
                objCmp = _companyRepository.InsertHolidateReason(UserID,Convert.ToDateTime(holidate),reason);
                return Json(new { data = objCmp });
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

        [HttpPost]
        public ActionResult EditHolidate(int ID, string Holidate, string Reason)
        {
            //DateTime Holidate2 = Convert.ToDateTime("2019-01-01"); 
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                int objCmp = 0;
                objCmp = _companyRepository.EditHolidateReason(ID, Holidate, Reason);
                return View("HolidayList");
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

        public ActionResult DeleteHolidate(string ID)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var data = IsBase64Encoded(ID);
                if (data == true)
                {

                    var result = Convert.FromBase64String(ID);
                    int IDs = Convert.ToInt16(Encoding.UTF8.GetString(result));
                    int objCmp = 0;
                    objCmp = _companyRepository.DeleteHolidateReason(IDs);
                    return View("HolidayList");
                }
                else
                {
                    return View("~/Views/User/PageNotFound.cshtml");
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
        [JWTAuthorize("Account", "UserLogin")]
        [CustomFilter]
        public IActionResult AnchorHolidayList()
        {
           
            return View();
        }

        #region Get Company Result
        
        public IActionResult Notification()
        {
            return View();
        }
        #endregion

        #region Get Register List

        /// <summary>
        /// Saving company data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertNotification(string BugType, string BugDetail,string CompanyName,int CompanyId)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            var Res = _CommonRepository.AuditTrailLog("User Listing", "Listing Page", UserID, 0);
            string ErrorMessage = string.Empty;
            try
            {
                string Role = HttpContext.Session.GetString("Role");
                int objData = 0;
                objData = _AnchorCompanyRepository.InsertNotificationDetail(BugType, BugDetail, CompanyName, CompanyId);

                return View("Dashboard");
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
        [HttpPost]
        public JsonResult GetNotificationData()
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
                var SearchFieldName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                recordsFiltered = 0;
                recordsTotal = 0;

                IEnumerable<GetNotificationDetail> lstNotificationDetail = _AnchorCompanyRepository.GetNotificationList(sortBy, pageSize, skip, SearchFieldName);

                if (lstNotificationDetail != null && lstNotificationDetail.Count() != 0)
                {
                    recordsFiltered = lstNotificationDetail.ElementAt(0).FilteredRecord;
                    recordsTotal = lstNotificationDetail.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstNotificationDetail });
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

        public ActionResult AuditTrailHistory()
        {
            
            IEnumerable<TablesName> lstGetTableName = _AnchorCompanyRepository.GetTablesName();
            ViewBag.TableNameList = lstGetTableName;
            return View();
        }

        [HttpPost]
        public JsonResult GetColumnList(string TableName)
        {
            try
            {
                IEnumerable<TablesName> lstGetColumnName = _AnchorCompanyRepository.GetColumnsName(TableName);
               
                return Json(data: lstGetColumnName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult GetAuditTrailHistoryList(string TableName,string ColumnName,DateTime? FormDate,DateTime? ToDate)
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
                var SearchFieldName = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var FormDate1 = Convert.ToDateTime(FormDate).Date;
                var ToDate1 = Convert.ToDateTime(ToDate).Date;

                string FormDate2 = Convert.ToString(FormDate1);
                string ToDate2 = Convert.ToString(ToDate1);
                recordsFiltered = 0;
                recordsTotal = 0;

                IEnumerable<AuditTrailHistory> lstAuditTrailHistory = _AnchorCompanyRepository.GetAuditTrailHistory(sortBy, pageSize, skip, SearchFieldName,TableName,ColumnName,FormDate2,ToDate2);

                if (lstAuditTrailHistory != null && lstAuditTrailHistory.Count() != 0)
                {
                    recordsFiltered = lstAuditTrailHistory.ElementAt(0).FilteredRecord;
                    recordsTotal = lstAuditTrailHistory.ElementAt(0).TotalRecord;
                }
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstAuditTrailHistory });
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
       
    }


}

