using Finocart.Interface;
using Finocart.Model;
using Finocart.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Finocart.Web.Controllers
{
    /// <summary>
    /// Employee Controller
    /// </summary>
    /// 
    
    public class EmployeeController : BaseController
    {

        #region Interface declaration

        /// <summary>
        /// Employee Data Repository
        /// </summary>
        private readonly IEmployee _empRepository;

        /// <summary>
        /// Lookup data Repository
        /// </summary>
        private readonly ILookupDetails _lookUpRepository;

        private readonly IConfiguration _configuration;
        private readonly ICommon _CommonRepository;
        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository"></param>
        public EmployeeController(IEmployee employeeRepository, ILookupDetails lookupDetailsRepository
            ,IConfiguration configuration, ICommon CommonRepository)
        {
            _empRepository = employeeRepository;
            _lookUpRepository = lookupDetailsRepository;
            _configuration = configuration;
            _CommonRepository = CommonRepository;
        }

        #endregion

        #region Action Results

        /// <summary>
        /// Get Employee List
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ///For testing purpose
            HttpContext.Session.SetInt32("UserID", 123455);
            ViewBag.VendorId = HttpContext.Session.GetInt32("UserID");

            //ViewBag.EmployeeCount = GetEmployeeCount();
            //GetEmployees();
            return View();
        }

        /// <summary>
        /// Get Employee List
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportData()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                IEnumerable<Employee> objData = GetEmployees();
                string csv = ExportDatastr(objData);

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "report.csv");
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
        /// Upload vendor document
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadVendorDocuemnts(IFormFile formFile)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
                //Get folder name from the appsetting.json
                string documentFolderPath = _configuration["VendorDocumentsFolder"];
                string exportFileName = "Vendor_PAN_" + DateTime.Now.ToShortDateString().Replace("-", "") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;

                uploadFiletoServer(documentFolderPath, formFile, exportFileName);
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                return RedirectToAction("ErrorPage", "Common");
            }
                return View("Index");
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get Employee data count
        /// </summary>
        /// <returns></returns>
        public string GetEmployeeCount()
        {
            int i = _empRepository.Count();
            Int64 jEmployeeID = _empRepository.getEMployeesInfo(1);
            return i.ToString();
        }

        /// <summary>
        /// Get employee view
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployees()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            { 

            IEnumerable<Employee> objData = _empRepository.getEmployees();
            Employee objDatawithSP = _empRepository.getEmployeesById(1);
            Int64 i = _empRepository.getEmployeesDataInfo();

                return objData;
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

        /// <summary>
        /// Export employee data
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ExportDatastr(IEnumerable<Employee> objData)
        {
            return CommonService.ListToCSV(objData,"Id");
        }

        /// <summary>
        /// Upload File to server
        /// </summary>
        /// <param name="documentFolderPath">Document folder name where need to upload/save documents </param>
        /// <param name="formFile">Object of file</param>
        /// <param name="exportFileName">Export file name without extension</param>
        /// <returns></returns>
        public  string uploadFiletoServer(string documentFolderPath, IFormFile formFile,string exportFileName) //string strFiles, IFormFile fromFile)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            string errorMessge = "";
            try
            {
 
                //1 check if the file length is greater than 0 bytes 
                if (formFile == null || formFile.Length == 0)
                    errorMessge = "file not selected";

                //2 Get the extension of the file
                string fileName = formFile.FileName;
                string extension = Path.GetExtension(fileName);

                //Check with prevent extension(s)
                string preventExtension = _configuration.GetSection("Extension").GetSection("PreventFileExtension").Value;
                if (preventExtension.ToLower().Contains(extension))
                {
                    errorMessge = "file type is not allowed to upload";
                }
                else
                {
                    string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot", documentFolderPath));

                    if (!Directory.Exists(documentFolderPath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var fileStream = new FileStream(
                                        Path.Combine(filePath, exportFileName + extension),FileMode.Create))
                    {
                        formFile.CopyToAsync(fileStream);
                    }
                    errorMessge = "file uploaded successfully";
                }

            }
            catch (Exception ex)
            {
                errorMessge = "Error during upload file. Please contact administrator.";
                //throw
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }

            return errorMessge;
        }

        /// <summary>
        /// Upload file to server
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<JsonResult> UploadFileDatatoServer(IFormFile formFile)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            // do something here
            //string errors = "No data";
            //return Json(errors);
            string errorMessge = "";
            try
            {

                //Get folder name from the appsetting.json
                string documentFolderPath = _configuration["VendorDocumentsFolder"];
                string exportFileName = "Vendor_PAN_" + DateTime.Now.ToShortDateString().Replace("-", "") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;


                //1 check if the file length is greater than 0 bytes 
                if (formFile == null || formFile.Length == 0)
                    errorMessge = "file not selected";

                //2 Get the extension of the file
                string fileName = formFile.FileName;
                string extension = Path.GetExtension(fileName);

                //Check with prevent extension(s)
                string preventExtension = _configuration.GetSection("Extension").GetSection("PreventFileExtension").Value;
                if (preventExtension.ToLower().Contains(extension))
                {
                    errorMessge = "file type is not allowed to upload";
                }
                else
                {
                    string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot", documentFolderPath));

                    if (!Directory.Exists(documentFolderPath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var fileStream = new FileStream(
                                        Path.Combine(filePath, exportFileName + extension), FileMode.Create))
                    {
                        formFile.CopyToAsync(fileStream);
                    }
                    errorMessge = "file uploaded successfully";
                }

            }
            catch (Exception ex)
            {
                errorMessge = "Error during upload file. Please contact administrator.";
                //throw
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }

            return Json(errorMessge);
        }

        /// <summary>
        /// User view
        /// </summary>
        /// <returns></returns>
        public ActionResult Users()
        {
            return View("InternalUserManagement");
        }

        /// <summary>
        /// View emailer
        /// </summary>
        /// <returns></returns>
        public ActionResult Emailer()
        {
            return View("Emailer");
        }

        #endregion
    }
}