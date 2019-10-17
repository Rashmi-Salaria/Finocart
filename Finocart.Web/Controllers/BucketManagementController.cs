using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Finocart.CustomModel;
using Finocart.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finocart.Web.Controllers
{
    public class BucketManagementController : BaseController
    {
        #region Interface declaration

        /// <summary>
        /// Invoice Data Repository
        /// </summary>
        private readonly IBucketManagement _bucketRepository;

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
        public BucketManagementController(IBucketManagement bucketRepository, ILookupDetails lookupDetailsRepository, ICommon CommonRepository)
        {
            _bucketRepository = bucketRepository;
            _lookUpRepository = lookupDetailsRepository;
            _CommonRepository = CommonRepository;
        }

        #endregion

        /// <summary>
        /// Bucket list view
        /// </summary>
        /// <returns></returns>
        public IActionResult BucketList()
        {
            return View("BucketList");
        }

        /// <summary>
        /// Getting bucket list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getBucketList()
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

                var PurchaseOrderNo = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var VendorName = Request.Form["columns[2][search][value]"].FirstOrDefault();

                IEnumerable<BucketManagementModel> lstBucket = _bucketRepository.getBucketList(sortBy, pageSize, skip, VendorId);

                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstBucket != null && lstBucket.Count() != 0)
                {
                    recordsFiltered = lstBucket.ElementAt(0).FilteredRecord;
                    recordsTotal = lstBucket.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstBucket });
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
        /// Bucket list view
        /// </summary>
        /// <param name="bucketInvoiceModel"></param>
        /// <returns></returns>
        public IActionResult BucketListView(BucketManagementModel bucketInvoiceModel)
        {
            return View("BucketListView", bucketInvoiceModel);
        }

        /// <summary>
        /// Getting bucket list view
        /// </summary>
        /// <param name="bucketInvoiceModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getBucketListView(BucketManagementModel bucketInvoiceModel)
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

                var PurchaseOrderNo = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var VendorName = Request.Form["columns[2][search][value]"].FirstOrDefault();

                IEnumerable<BucketManagementModel> lstBucket = _bucketRepository.getBucketListView(sortBy, pageSize, skip, VendorId, bucketInvoiceModel.BucketID);

                int? recordsFiltered = 0;
                int? recordsTotal = 0;
                if (lstBucket != null && lstBucket.Count() != 0)
                {
                    recordsFiltered = lstBucket.ElementAt(0).FilteredRecord;
                    recordsTotal = lstBucket.ElementAt(0).TotalRecord;
                }

                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = lstBucket });
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