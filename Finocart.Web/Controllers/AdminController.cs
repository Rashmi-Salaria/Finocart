using Finocart.CustomModel;
using Finocart.Interface;
using Finocart.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Finocart.Web.Controllers
{
    public class AdminController : BaseController
    {
        #region Interface declaration

        /// <summary>
        /// User data Repository
        /// </summary>
        private readonly IAdmin _adminRepository;
        private readonly ILookupDetails _lookUpRepository;
        private readonly ICommon _CommonRepository;
        #endregion


        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AdminRepository"></param>
        public AdminController(IAdmin adminRepository, ILookupDetails lookupDetailsRepository, ICommon CommonRepository)
        {
            _adminRepository = adminRepository;
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
        }


        #endregion
        public IActionResult Index()
        {
            return View();
        }

        #region Invoice Approval Order List

        /// <summary>
        /// Invoice List Page
        /// </summary>
        /// <returns></returns>
        public IActionResult InvoiceApprovedList()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                return View("~/Views/Admin/InvoiceApprovalOrderList.cshtml");
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
        /// Get Invoice Approval List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetInvoiceApprovedList()
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
                var UserName = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var UserRole = Request.Form["columns[6][search][value]"].FirstOrDefault() == "0" ? "" : Request.Form["columns[6][search][value]"].FirstOrDefault();

                IEnumerable<InvoiceApprovalOrderModel> lstInvoiceApproval = _adminRepository.GetInvoiceApprovedListing(sortBy, pageSize, skip);
                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstInvoiceApproval != null && lstInvoiceApproval.Count() != 0)
                {
                    recordsFiltered = lstInvoiceApproval.ElementAt(0).FilteredRecord;
                    recordsTotal = lstInvoiceApproval.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstInvoiceApproval });
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

        #region Add and Update Invoice Approval

        /// <summary>
        /// Invoice Approval Add Page
        /// </summary>
        /// <returns></returns>
        public IActionResult AddInvoiceApproval()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                InvoiceApprovalOrderModel objInvoiceApprovalOrder = new InvoiceApprovalOrderModel();
                IEnumerable<User> lstUsers = _adminRepository.GetUserList();
                IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("InvoiceApproved");
                Tuple<InvoiceApprovalOrderModel, IEnumerable<LookupDetails>, IEnumerable<User>> objAdminModelData = new Tuple<InvoiceApprovalOrderModel, IEnumerable<LookupDetails>, IEnumerable<User>>(objInvoiceApprovalOrder, lookupDetails, lstUsers);
                return View("AddInvoiceApproval", objAdminModelData);
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
        /// Save Invoice Approval Record
        /// </summary>
        /// <param name="objInvoiceApprovalPage"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddInvoiceApproval(InvoiceApprovalOrderModel objInvoiceApprovalPage)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                var Result = _adminRepository.InsertUpdateApprovalRecord(objInvoiceApprovalPage);
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

            return RedirectToAction("InvoiceApprovedList", "Admin");
        }

        /// <summary>
        /// Edit and get Invoice Approval record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditInvoiceApprovalPage(int id)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;

            InvoiceApprovalOrderModel objInvoiceApprovalModel = new InvoiceApprovalOrderModel();
            try
            {
                objInvoiceApprovalModel = _adminRepository.EditPage(id);
                IEnumerable<User> lstUsers = _adminRepository.GetUserList();
                IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("InvoiceApproved");
                Tuple<InvoiceApprovalOrderModel, IEnumerable<LookupDetails>, IEnumerable<User>> objAdminModelData = new Tuple<InvoiceApprovalOrderModel, IEnumerable<LookupDetails>, IEnumerable<User>>(objInvoiceApprovalModel, lookupDetails, lstUsers);
                return View("AddInvoiceApproval", objAdminModelData);
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

        #region Delete Invoice Approval

        /// <summary>
        /// Delete invoice on the basis of id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteInvoiceApprovalPage(int id)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {

                int result = 0;
                result = _adminRepository.DeleteInvoiceApprovalPage(id);
                TempData["DeleteResult"] = result;
                return RedirectToAction("InvoiceApprovedList", "Admin");
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

        #region Check Amount Duplication
        /// <summary>
        /// check from amount and to amount duplication
        /// </summary>
        /// <param name="FromAmount"></param>
        /// <param name="ToAmount"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckDuplicateAmount(decimal FromAmount, decimal ToAmount)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                long Json1 = 0;
                IEnumerable<InvoiceApprovalOrder> objInvoiceApprovalModel = _adminRepository.CheckAmountDuplicate(FromAmount, ToAmount);
              
                if (objInvoiceApprovalModel.Any())
                {
                    Json1 = objInvoiceApprovalModel.ElementAt(0).ID;
                }
                return Json(Json1);
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

    }
}