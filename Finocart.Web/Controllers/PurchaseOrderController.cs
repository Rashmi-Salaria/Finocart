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
using Microsoft.AspNetCore.Routing;

namespace Finocart.Web.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        #region Interface declaration

        /// <summary>
        /// Invoice Data Repository
        /// </summary>
        private readonly IPurchaseOrder _empRepository;

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
        public PurchaseOrderController(IPurchaseOrder invoiceRepository, ILookupDetails lookupDetailsRepository, ICommon CommonRepository)
        {
            _empRepository = invoiceRepository;
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
        }

        #endregion

        #region Purchase Order Listing
        /// <summary>
        /// Purchase order list view
        /// </summary>
        /// <returns></returns>
        public IActionResult PurchaseOrderList()
        {
            return View("~/Views/PurchaseOrder/PurchaseOrderList.cshtml");
        }

        /// <summary>
        /// Get Purchase Order Data List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getPurchaseOrderList()
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
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var PurchaseOrderNo = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var VendorName = Request.Form["columns[2][search][value]"].FirstOrDefault();

                IEnumerable<PurchaseOrderModel> lstPO = _empRepository.getPurchaseOrderList(sortBy, pageSize, skip, PurchaseOrderNo, VendorName);

                int? recordsFiltered = 0;
                int? recordsTotal = 0;
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

        #endregion

        #region Export Purchase Order Data To CSV
        /// <summary>
        /// Export Purchase Order Data List To CSV
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportPurchaseOrderData(string PurchaseOrderNo, string VendorName)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {

                var sortBy = "PONumber asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<PurchaseOrderModel> lstPO = _empRepository.getPurchaseOrderList(sortBy, pageSize, skip, PurchaseOrderNo, VendorName);

                string csv = ExportPODatastr(lstPO);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "PurchaseOrder.csv");
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
        /// Export PO data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportPODatastr(IEnumerable<PurchaseOrderModel> objData)
        {
            return CommonService.ListToCSV(objData, "TotalRecord,FilteredRecord");
        }
        #endregion

        #region Invoice Listing By Purchase Order

        /// <summary>
        /// Getting invoice list by PO number
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        public IActionResult InvoiceListByPOId(string PONumber)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {
                IEnumerable<ModuleMaster> objData = _empRepository.getModuleStatus();
                objData = objData.Where(x => x.ModuleName == "Invoice");
                InvoiceModel objInvoice = new InvoiceModel();
                objInvoice.PONumber = PONumber;
                Tuple<InvoiceModel, IEnumerable<ModuleMaster>> objInvoiceData = new Tuple<InvoiceModel, IEnumerable<ModuleMaster>>(objInvoice, objData);
                return View("~/Views/PurchaseOrder/POInvoiceList.cshtml", objInvoiceData);
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
        /// Get Invoice Data List By PO Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getInvoiceListByPOId(InvoiceModel objInvoiceModel)
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
                //Get Sort columns value
                var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() + " " + Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var InvoiceNo = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var InvoiceStatus = Request.Form["columns[8][search][value]"].FirstOrDefault();

                IEnumerable<InvoiceModel> lstInvoice = _empRepository.getInvoiceListByPOId(sortBy, pageSize, skip, InvoiceNo, InvoiceStatus, objInvoiceModel.PONumber);

                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstInvoice != null && lstInvoice.Count() != 0)
                {
                    recordsFiltered = lstInvoice.ElementAt(0).FilteredRecord;
                    recordsTotal = lstInvoice.ElementAt(0).TotalRecord;
                }

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

        #region Export Invoice Data By Purchase Order Number To CSV
        /// <summary>
        /// Export Invoice Data By Purchase Order Number To CSV
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportInvoiceByPOIdData(string InvoiceNo, string InvoiceStatus, string PONumber)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            try
            {

                var sortBy = "PONumber asc";
                int pageSize = 100000;
                int skip = 0;
                IEnumerable<InvoiceModel> lstInvoice = _empRepository.getInvoiceListByPOId(sortBy, pageSize, skip, InvoiceNo, InvoiceStatus, PONumber);

                string csv = ExportInvoiceDatastr(lstInvoice);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "InvoiceByPO.csv");
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
        /// Exporting invoice data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportInvoiceDatastr(IEnumerable<InvoiceModel> objData)
        {
            return CommonService.ListToCSV(objData, "InvoiceID,AnchorCompName,TotalRecord,FilteredRecord");
        }
        #endregion

        #region Export records to CSV format by PO Id
        /// <summary>
        /// Export records to CSV format by PO Id
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        public ActionResult ExportRecordByPOId(string PONumber)
        {
            IEnumerable<PurchaseOrderModel> lstPurchaseOrderList = _empRepository.GetRecordByPONumber(PONumber);
            string VendorName = "";
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                if (lstPurchaseOrderList != null)
                {
                    VendorName = lstPurchaseOrderList.ElementAt(0).VendorName;
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
            return RedirectToAction("ExportPurchaseOrderData", new RouteValueDictionary(
            new { controller = "PurchaseOrder", action = "Main", PONumber = PONumber,VendorName=VendorName }));
           
        }
        #endregion
    }
}