using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Model;
using Finocart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finocart.Web.Controllers
{
    public class InvoiceController : BaseController
    {
        #region Interface declaration

        /// <summary>
        /// Invoice Data Repository
        /// </summary>
        private readonly IInvoice _empRepository;

        /// <summary>
        /// Lookup data Repository
        /// </summary>
        private readonly ILookupDetails _lookUpRepository;
        private readonly ICommon _CommonRepository;

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        public InvoiceController(IInvoice invoiceRepository, ILookupDetails lookupDetailsRepository, ICommon CommonRepository)
        {
            _empRepository = invoiceRepository;
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
        }

        #endregion

        #region Get Invoice Listing

        /// <summary>
        /// Invoice Listing Page
        /// </summary>
        /// <returns></returns>
        public IActionResult InvoiceList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                ViewBag.VendorId = HttpContext.Session.GetInt32("UserID");
                ViewBag.VendorName = HttpContext.Session.GetString("UserName");
                IEnumerable<ModuleMaster> objData = _empRepository.getModuleStatus();
                objData = objData.Where(x => x.ModuleName == "Invoice");
                IEnumerable<SearchHistory> lstLastSearchParams = _empRepository.GetSearchHistories();
                IEnumerable<LookupDetails> lstlookupDetails = _lookUpRepository.getLookupDetailByKey("Last Search Limit");
                ViewData["MaxSearchCount"] = lstlookupDetails.ElementAt(0).LookupValue;
                
                Tuple<IEnumerable<ModuleMaster>, IEnumerable<SearchHistory>> objUserModelData =
                new Tuple<IEnumerable<ModuleMaster>, IEnumerable<SearchHistory>>(objData, lstLastSearchParams);
                return View("~/Views/Invoice/InvoiceList.cshtml", objUserModelData);
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
        /// Get Invoice Data List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getInvoiceList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var AnchorCompanyName = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var InvoiceStatus =  Request.Form["columns[9][search][value]"].FirstOrDefault();

                IEnumerable<InvoiceModel> lstInvoice = _empRepository.getInvoiceList(sortBy, pageSize, skip, AnchorCompanyName, InvoiceStatus,"",0, VendorId);
               
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstInvoice!=null && lstInvoice.Count()!=0)
                {
                    recordsFiltered = lstInvoice.ElementAt(0).FilteredRecord;
                    recordsTotal = lstInvoice.ElementAt(0).TotalRecord;
                }
                
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstInvoice});
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
        public IActionResult ExportInvoiceData(string AnchorCompanyName, string InvoiceStatus)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                Int32? VendorId = HttpContext.Session.GetInt32("CompanyID");
                var sortBy = "PONumber asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<InvoiceModel> lstInvoice = _empRepository.getInvoiceList(sortBy, pageSize, skip, AnchorCompanyName, InvoiceStatus, "", 0, VendorId);

                string csv = ExportDatastr(lstInvoice);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Invoice.csv");
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
        /// Export data 
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportDatastr(IEnumerable<InvoiceModel> objData)
        {
            return CommonService.ListToCSV(objData, "InvoiceID,VendorName,TotalRecord,FilteredRecord");
        }
        #endregion

        #region 
        /// <summary>
        /// Getting last search history
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLastSearchhistory()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                IEnumerable<SearchHistory> lstLastSearchParams = _empRepository.GetSearchHistories();
                return Json(new { data = lstLastSearchParams });
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

        #region GetMaxVendorAvailableAmount
        /// <summary>
        /// getting max available amount
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMaxAvailableAmount()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            string maxAmount = string.Empty;
            try
            {
                maxAmount = _empRepository.GetMaxAvailableAmount(Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }
            return Json(new { MaxAvailableAmount = maxAmount });
        }
        #endregion 
    }
}