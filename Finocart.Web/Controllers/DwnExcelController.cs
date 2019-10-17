using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using Finocart.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Finocart.Web.Controllers
{
    public class DwnExcelController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICommon _CommonRepository;

        public DwnExcelController(IHostingEnvironment hostingEnvironment, ICommon CommonRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _CommonRepository = CommonRepository;

        }



        public IActionResult DwnExceli()
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            Int32? UserID = HttpContext.Session.GetInt32("UserID");
            string ErrorMessage = string.Empty;
            try
            {
               
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = @"demo.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee");
                    //First add the headers
                    worksheet.Cells[1, 1].Value = "Vendor ID";
                    worksheet.Cells[1, 2].Value = "Vendor Name";
                    worksheet.Cells[1, 3].Value = "Contact Number";
                    worksheet.Cells[1, 4].Value = "Email ID";
                    worksheet.Cells[1, 5].Value = "Maximum Limit";
                    worksheet.Cells[1, 6].Value = "Vendor Status";
                    worksheet.Cells[1, 7].Value = "Date of Creation";



                    package.Save(); //Save the workbook.
                }
                var result = PhysicalFile(Path.Combine(sWebRootFolder, sFileName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = file.Name
                }.ToString();

                return result;

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
    }
}