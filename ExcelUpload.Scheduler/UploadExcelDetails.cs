using Finocart.CustomModel;
using Finocart.Services;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ExcelUpload.Scheduler
{
    public class UploadExcelDetails
    {
        Finocart_V1Entities8 finocartEntities1 = new Finocart_V1Entities8();
        SecurityHelperService SecurityHelperService = new SecurityHelperService();
        //private readonly IHostingEnvironment _hostingEnvironment;

        //public UploadExcelDetails()
        //{

        //}
        //public UploadExcelDetails(IHostingEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //}        

        public void UploadVendors(string Path, Int64? CompanyID, string FileName, string CompanyName)
        {
            try
            {

                DataTable dt = new DataTable();
                string JSONString = string.Empty;
                var memory = new MemoryStream();
                string sFileExtension = FileName.Split('.')[1];
                //    var FileName = CompanyName + DateTime.Now.ToString("yyyyMMddhhmmss");
                ISheet sheet;
                //string fullPath = Path.Combine(FileName + sFileExtension);
                using (var stream = new FileStream(Path, FileMode.Open))
                {
                    stream.CopyTo(memory);
                    memory.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(memory); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(memory); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }

                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        dt.Columns.Add(headerRow.GetCell(j).ToString());
                    }
                    dt.Columns.Add("Message");

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        DataRow dr = dt.NewRow();
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                dr[j] = row.GetCell(j).ToString();

                        }

                        dt.Rows.Add(dr);

                        if (dt.Rows[i - 1]["Vendor Name"].ToString() != "" && dt.Rows[i - 1]["Pan Number"].ToString() != "" && dt.Rows[i - 1]["Contact Person Name"].ToString() != "" && dt.Rows[i - 1]["Email ID"].ToString() != "" && dt.Rows[i - 1]["Contact Number"].ToString() != "")
                        {

                            if (!Regex.IsMatch(dt.Rows[i - 1]["Pan Number"].ToString(), @"^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$"))
                            {
                                dt.Rows[i - 1]["Message"] = "Pan Number is not valid";
                                continue;
                            }
                            if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() != "")
                            {
                                if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() == "yes")
                                {
                                    if (dt.Rows[i - 1]["UAM Number"].ToString() == "")
                                    {
                                        dt.Rows[i - 1]["Message"] = "UAM Number should not be blank";
                                        continue;
                                    }
                                }
                            }
                            if (dt.Rows[i - 1]["UAM Number"].ToString() != "")
                            {
                                if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() == "")
                                {
                                    dt.Rows[i - 1]["Message"] = "MSME should not be blank";
                                    continue;
                                }
                            }
                            if (!Regex.IsMatch(dt.Rows[i - 1]["Email ID"].ToString(), @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                            {
                                dt.Rows[i - 1]["Message"] = "Email ID is not valid";
                                continue;
                            }

                            else
                            {
                                dt.Rows[i - 1]["Message"] = "Success";
                            }

                        }
                        else
                        {
                            if (dt.Rows[i - 1]["Vendor Name"].ToString() == "")
                            {
                                dt.Rows[i - 1]["Message"] = "Vendor Name should not be blank";
                                continue;
                            }
                            if (dt.Rows[i - 1]["Pan Number"].ToString() == "")
                            {
                                dt.Rows[i - 1]["Message"] = "Pan Number should not be blank";
                                continue;
                            }
                            if (dt.Rows[i - 1]["Contact Person Name"].ToString() == "")
                            {
                                dt.Rows[i - 1]["Message"] = "Contact Person Name should not be blank";
                                continue;
                            }
                            if (dt.Rows[i - 1]["Email ID"].ToString() == "")
                            {
                                dt.Rows[i - 1]["Message"] = "Email ID should not be blank";
                                continue;
                            }
                            if (dt.Rows[i - 1]["Contact Number"].ToString() == "")
                            {
                                dt.Rows[i - 1]["Message"] = "Contact Number should not be blank";
                                continue;
                            }
                        }

                        string randomPassword = GeneratePassword();
                        string Password = SecurityHelperService.Encrypt(randomPassword);
                        var Result = InsertVendorRecord(dr, CompanyID, Password);

                        if (Convert.ToInt32(Result.Value) > 0)
                        {
                            //string Template = string.Empty;
                            string Template = GetVendorRegisterMailTemplate();
                            string path = Template;
                            string EMAIL_TOKEN_PAYMENT_LINK = "##$$PAYMENT_LINK$$##";
                            string paymentLink = "http://dotnet.brainvire.com/Finocart/Account/AdminLogin";///change url
                            //string MailStatus = string.Empty;
                            string emailToAddress = dr[6].ToString();
                            string subject = "Vendor registration";
                            WebClient client = new WebClient();
                            string startupPath = Environment.CurrentDirectory;

                            string body = path;
                            // string body = client.DownloadString(startupPath + "/Views/Template/EmailTemplate.cshtml");
                            body = body.Replace("@@User@@", dr[0].ToString());
                            body = body.Replace("@@PanNumber@@", dr[1].ToString());
                            body = body.Replace("@@ProjectName@@", "Finocart");
                            body = body.Replace("@@VendorName@@", dt.Rows[i - 1]["Vendor Name"].ToString());
                            body = body.Replace("@@AnchorCompanyname@@", CompanyName);
                            body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);
                            body = body.Replace("@@PanNumber@@", dt.Rows[i - 1]["Pan Number"].ToString());
                            body = body.Replace("@@Password@@", randomPassword);
                            IEnumerable<LookupDetail> lookupDetails = getLookupDetailByKey("SMTPInfo");
                            SendEmail(lookupDetails, emailToAddress, subject, body, true);
                        }

                        if (Convert.ToInt32(Result.Value) == -1)
                        {
                            dt.Rows[i - 1]["Message"] = "Pan Number already exists";
                            continue;
                        }

                    }
                    //GetLog(dt);
                }
                //}

                JSONString = JsonConvert.SerializeObject(dt);
                GetLog(JSONString, "Vendor", CompanyID, CompanyName, FileName);
                //HttpContext.Session.SetString("Excel", JSONString);
                //return Json(new { result = dt });

              


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<UploadExcelPath> GetExcelPathsDetails()
        {
            try
            {
                var maxValue = finocartEntities1.UploadExcelPaths.Max(x => x.CreatedDate);
                var query = (from data in finocartEntities1.UploadExcelPaths where data.CreatedDate == maxValue orderby data.CreatedDate select data).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObjectParameter InsertVendorRecord(DataRow VendorDetails, Int64? AnchorCompId, string randomPassword)
        {
            int result = 0;
            ObjectParameter ReturnedValue = new ObjectParameter("ReturnValue", typeof(int));
            try
            {
                
                finocartEntities1.proc_InsertVendorData(
                    AnchorCompId, VendorDetails[0].ToString(), randomPassword, VendorDetails[1].ToString(), VendorDetails[2].ToString(), VendorDetails[3].ToString(), VendorDetails[4].ToString(), VendorDetails[5].ToString(), VendorDetails[6].ToString(), VendorDetails[7].ToString(), ReturnedValue
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ReturnedValue;
        }

        public string GeneratePassword()
        {
            string Password = "";
            try
            {
                string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
                string numbers = "1234567890";
                string SpecialCharacters = "!@#$%^&*?_-";

                string characters = "";
                //if (rbType.SelectedItem.Value == "1")
                {
                    characters += alphabets + small_alphabets + numbers + SpecialCharacters;
                }

                for (int i = 0; i < 8; i++)
                {
                    string character = string.Empty;
                    do
                    {
                        int index = new Random().Next(0, characters.Length);
                        character = characters.ToCharArray()[index].ToString();
                    } while (Password.IndexOf(character) != -1);
                    Password += character;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Password;
        }

        public void GetLog(string LogDetails, string Log, Int64? CompanyID, string CompanyName, string FileName)
        {
            string ErrorMessage = string.Empty;
            try
            {
                dynamic key = JsonConvert.DeserializeObject(LogDetails);
                string folderName = "UploadLogs";
                //string sWebRootFolder = AppDomain.CurrentDomain.BaseDirectory;
                string sWebRootFolder = "D:\\Publish\\Finocart\\wwwroot";
                //string sWebRootFolder = "D:\\Rashmi\\Projects\\Finocart\\Development_V1\\Finocart\\Finocart.Web\\wwwroot";
                //sWebRootFolder = sWebRootFolder.ToString().Replace("ExcelUpload.Scheduler\\bin\\Debug\\", "");
                //sWebRootFolder = sWebRootFolder + "\\Finocart.Web\\wwwroot\\";
                string newPath = Path.Combine(sWebRootFolder, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string sFileName = "";
                if (Log == "Vendor")
                { sFileName = @"VendorLog"+ DateTime.Now.ToString("yyyyMMddhhmmss")+".xlsx"; }
                else { sFileName = @"InvoiceLog" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx"; }
                FileInfo file = new FileInfo(Path.Combine(newPath, sFileName));
                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    var index = 1;
                    ExcelWorksheet worksheet;
                    // add a new worksheet to the empty workbook
                    if (Log == "Vendor")
                    {
                        // add a new worksheet to the empty workbook
                        worksheet = package.Workbook.Worksheets.Add("Vendors");
                        //First add the headers                    
                        foreach (VendorHeaderModel item in Enum.GetValues(typeof(VendorHeaderModel)))
                        {
                            var Headers = item.ToString().Replace("_", " ").Replace("00", " (").Replace("1", ")").Replace("0", "/");
                            worksheet.Cells[1, index].Value = Headers;
                            worksheet.Cells[1, index].Style.Font.Size = 12;
                            worksheet.Cells[1, index].Style.Font.Bold = true;
                            worksheet.Cells[1, index].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                            index++;
                        }
                        for (int i = 0; i < key.Count; i++)
                        {
                            worksheet.Cells["A" + (i + 2)].Value = key[i]["Vendor Name"].Value;
                            worksheet.Cells["B" + (i + 2)].Value = key[i]["Pan Number"].Value;
                            worksheet.Cells["C" + (i + 2)].Value = key[i]["MSME  (Yes/No)"].Value;
                            worksheet.Cells["D" + (i + 2)].Value = key[i]["UAM Number"].Value;
                            worksheet.Cells["E" + (i + 2)].Value = key[i]["Contact Person Name"].Value;
                            worksheet.Cells["F" + (i + 2)].Value = key[i]["Contact Person Designation"].Value;
                            worksheet.Cells["G" + (i + 2)].Value = key[i]["Email ID"].Value;
                            worksheet.Cells["H" + (i + 2)].Value = key[i]["Contact Number"].Value;
                            worksheet.Cells["I" + (i + 2)].Value = key[i]["Message"].Value;
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
                            index++;
                        }
                        for (int i = 0; i < key.Count; i++)
                        {
                            worksheet.Cells["A" + (i + 2)].Value = key[i]["PO Number"].Value;
                            worksheet.Cells["B" + (i + 2)].Value = key[i]["Invoice Date (DD/MM/YYYY)"].Value;
                            worksheet.Cells["C" + (i + 2)].Value = key[i]["Invoice Number"].Value;
                            worksheet.Cells["D" + (i + 2)].Value = key[i]["Invoice Amount"].Value;
                            worksheet.Cells["E" + (i + 2)].Value = key[i]["Payment Due Date (DD/MM/YYYY)"].Value;
                            worksheet.Cells["F" + (i + 2)].Value = key[i]["Approved Amount"].Value;
                            //worksheet.Cells["G" + (i + 2)].Value = key[i]["Payment Days"].Value;
                            worksheet.Cells["G" + (i + 2)].Value = key[i]["Pan Number"].Value;
                            worksheet.Cells["H" + (i + 2)].Value = key[i]["Message"].Value;
                        }
                    }
                    worksheet.Cells[1, index].Value = "Message";
                    worksheet.Cells[1, index].Style.Font.Size = 12;
                    worksheet.Cells[1, index].Style.Font.Bold = true;
                    worksheet.Cells[1, index].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells.AutoFitColumns();
                    package.Save(); //Save the workbook.
                }

                UpdateExcelLogPath(Path.Combine(newPath, sFileName), CompanyID, CompanyName, FileName, Log);

            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                //var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                //return RedirectToAction("ErrorPage", "Common");

            }
        }

        public ObjectParameter UpdateExcelLogPath(string sWebRootFolder, Int64? CompanyID, string CompanyName, string FileName, string Log)
        {
            int result = 0;
            ObjectParameter ReturnedValue = new ObjectParameter("ReturnValue", typeof(int));
            try
            {

                finocartEntities1.proc_InsertExcelData(
                    null, FileName, Log, CompanyID, CompanyName, sWebRootFolder, ReturnedValue
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ReturnedValue;
        }

        public void UploadInvoice(string Path, Int16? CompanyID, string FileName, string AnchCompanyName)
        {
                string ErrorMessage = string.Empty;
                try
                    {
                    DataTable dt = new DataTable();
                    string JSONString = string.Empty;
                    var memory = new MemoryStream();
                    string sFileExtension = FileName.Split('.')[1];
                    var EmailID = "";
                    var VendorName = "";
                    bool sendMail = false;
                    ISheet sheet;
                    using (var stream = new FileStream(Path, FileMode.Open))
                    {

                       stream.CopyTo(memory);
                       memory.Position = 0;
                       if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(memory); //This will read the Excel 97-2000 formats  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(memory); //This will read 2007 Excel format  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }

                        IRow headerRow = sheet.GetRow(0); //Get Header Row
                        int cellCount = headerRow.LastCellNum;

                        for (int j = 0; j < cellCount; j++)
                        {
                            NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                            dt.Columns.Add(headerRow.GetCell(j).ToString());
                        }
                        dt.Columns.Add("Message");
                        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                        {
                            DataRow dr = dt.NewRow();
                            IRow row = sheet.GetRow(i);
                            if (row == null) continue;
                            if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                    dr[j] = row.GetCell(j).ToString();

                            }
                            dt.Rows.Add(dr);
                            DateTime date;
                            if (dt.Rows[i - 1]["PO Number"].ToString() != "" && dt.Rows[i - 1]["Invoice Date (DD/MM/YYYY)"].ToString() != "" && dt.Rows[i - 1]["Invoice Number"].ToString() != "" && dt.Rows[i - 1]["Invoice Amount"].ToString() != "" && dt.Rows[i - 1]["Payment Due Date (DD/MM/YYYY)"].ToString() != "" && dt.Rows[i - 1]["Approved Amount"].ToString() != "")
                            {
                                if (!DateTime.TryParseExact(dt.Rows[i - 1]["Invoice Date (DD/MM/YYYY)"].ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    dt.Rows[i - 1]["Message"] = "Invoice Date should be in DD/MM/YYYY format";
                                    continue;
                                }
                                if (Convert.ToInt64(dt.Rows[i - 1]["Invoice Amount"].ToString()) < Convert.ToInt64(dt.Rows[i - 1]["Approved Amount"].ToString()))
                                {
                                    dt.Rows[i - 1]["Message"] = "Approved amount should be less than invoice amount";
                                    continue;
                                }
                                if (!DateTime.TryParseExact(dt.Rows[i - 1]["Payment Due Date (DD/MM/YYYY)"].ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out date))
                                {
                                    dt.Rows[i - 1]["Message"] = "Payment Due Date should be in DD/MM/YYYY format";
                                    continue;
                                }    
                                else
                                {
                                    dt.Rows[i - 1]["Message"] = "Success";
                                }

                            }
                            else
                            {
                                if (dt.Rows[i - 1]["PO Number"].ToString() == "")
                                {
                                    dt.Rows[i - 1]["Message"] = "PO Number should not be blank";
                                    continue;
                                }
                                if (dt.Rows[i - 1]["Invoice Date (DD/MM/YYYY)"].ToString() == "")
                                {
                                    dt.Rows[i - 1]["Message"] = "Invoice Date should not be blank";
                                    continue;
                                }
                                if (dt.Rows[i - 1]["Invoice Number"].ToString() == "")
                                {
                                    dt.Rows[i - 1]["Message"] = "Invoice Number should not be blank";
                                    continue;
                                }
                                if (dt.Rows[i - 1]["Invoice Amount"].ToString() == "")
                                {
                                    dt.Rows[i - 1]["Message"] = "Invoice Amount should not be blank";
                                    continue;
                                }
                                if (dt.Rows[i - 1]["Payment Due Date (DD/MM/YYYY)"].ToString() == "")
                                {
                                    dt.Rows[i - 1]["Message"] = "Payment Due Date should not be blank";
                                    continue;
                                }
                                if (dt.Rows[i - 1]["Approved Amount"].ToString() == "")
                                {
                                    dt.Rows[i - 1]["Message"] = "Approved Amount should not be blank";
                                    continue;
                                }             
                            }

                            Company objDatawithSP = CheckEmailID(row.Cells[6].ToString());
                            if (objDatawithSP != null)
                            {
                                EmailID = objDatawithSP.Contact_email;
                                VendorName = objDatawithSP.Company_name;
                            }
                            else
                            {
                                dt.Rows[i - 1]["Message"] = "Pan Number is not valid";
                                continue;
                            }

                        var Result = InsertInvoiceRecord(row, CompanyID);


                        if (Convert.ToInt32(Result.Value) == -1)
                        {
                            dt.Rows[i - 1]["Message"] = "Invoice Number already Exists";
                            continue;
                        }
                        sendMail = true;
                        }

                    //sending email
                    if (sendMail)
                    {

                        string Template = GetInvoiceRegisterMailTemplate();
                        string path = Template;
                        //string tag = lstAwaitedInvVendorsView.ElementAt(0).TableTag;
                        string EMAIL_TOKEN_PAYMENT_LINK = "##$$PAYMENT_LINK$$##";
                        string paymentLink = "http://dotnet.brainvire.com/Finocart/Account/AdminLogin";///change url
                        string MailStatus = string.Empty;
                        string subject = "Vendor registration";
                        WebClient client = new WebClient();
                        string startupPath = Environment.CurrentDirectory;
                        string body = path;
                        StringBuilder sb = new StringBuilder();
                        //IEnumerable<GetUploadVendorListModel1> lstAwaitedInvVendorsView1 = _companyRepository.GetUploadVendorList1(VendorID);
                        //for (int i = 1; i < 5; i++)
                        //{
                        body = body.Replace("@@VendorName@@", VendorName);
                        body = body.Replace("@@AnchorName@@", AnchCompanyName);
                        body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);
                        //body = body.Replace("@@PODate(MM/DD/YYYY)@@", "Abc");
                        //body = body.Replace("@@InvoiceNumber@@", "Abc");
                        //body = body.Replace("@@InvoiceAmount@@", "Abc");
                        //body = body.Replace("@@PaymentDueDate(MM/DD/YYYY)@@", "Abc");
                        //body = body.Replace("@@ApprovedAmount@@", "Abc");
                        //body = body.Replace("@@PaymentDays@@", "Abc");


                        IEnumerable<LookupDetail> lookupDetails = getLookupDetailByKey("SMTPInfo");
                        SendEmail(lookupDetails, EmailID, subject, body, true);

                    }
                }
           
                   JSONString = JsonConvert.SerializeObject(dt);
                   GetLog(JSONString, "Invoice", CompanyID, AnchCompanyName, FileName);
                //HttpContext.Session.SetString("Excel", JSONString);
                //if (file.FileName == "InvoiceTemplate.xlsx")
                //{

                //    file.FileName.Remove(1);

                //}
                //return Json(new { result = dt });
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                int ErrorLine = frame.GetFileLineNumber();
                //var Result = _CommonRepository.LogManagement(ControllerName, ActionName, ex.Message, ErrorLine, UserID);
                throw ex;
            }
        }

        public Company CheckEmailID(string PanNumber)
        {
            var query = (from data in finocartEntities1.Companies where data.Pan_number == PanNumber select data).FirstOrDefault();
            return query;
        }

        public ObjectParameter InsertInvoiceRecord(IRow InvoiceDetails, Int16? AnchorCompId)
        {
            int result = 0;
            ObjectParameter ReturnedValue = new ObjectParameter("ReturnValue", typeof(int));
            try
            {
                string Pan_Number = InvoiceDetails.Cells[6].ToString();
                var VendorID = (from data in finocartEntities1.Companies where data.Pan_number == Pan_Number select data.CompanyID).FirstOrDefault();

                finocartEntities1.proc_InsertInvoiceData(
                    InvoiceDetails.Cells[0].ToString(), Convert.ToDateTime(InvoiceDetails.Cells[1].ToString()), InvoiceDetails.Cells[2].ToString(), AnchorCompId, Convert.ToDecimal(InvoiceDetails.Cells[3].ToString()), Convert.ToDateTime(InvoiceDetails.Cells[4].ToString()), Convert.ToDecimal(InvoiceDetails.Cells[5].ToString()), ReturnedValue, VendorID

                    //null, null, null, 2, null, null, null, null, ReturnedValue, VendorID
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ReturnedValue;
        }

        public void SendEmail(IEnumerable<LookupDetail> lookupDetails, string emailToAddress, string subject, string body, bool isBodyHtml)
        {

            string gmailUserName = lookupDetails.ElementAt(0).LookupValue;
            string gmailUserPassword = lookupDetails.ElementAt(1).LookupValue;
            string SMTPSERVER = lookupDetails.ElementAt(2).LookupValue;
            int PORTNO = Convert.ToInt32(lookupDetails.ElementAt(3).LookupValue);

            SmtpClient smtpClient = new SmtpClient(SMTPSERVER, PORTNO);
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(gmailUserName, gmailUserPassword);
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(gmailUserName);
                    message.Subject = subject == null ? "" : subject;
                    message.Body = body == null ? "" : body;
                    message.IsBodyHtml = isBodyHtml;
                    message.To.Add(emailToAddress);

                    try
                    {
                        smtpClient.Send(message);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public string GetVendorRegisterMailTemplate()
        {
            try
            {
                var query = (from data in finocartEntities1.VendorRegisterMailTemplates select data.Template).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<LookupDetail> getLookupDetailByKey(string LookupKey)
        {
            try
            {
                var query = (from data in finocartEntities1.LookupDetails where data.LookupFor == LookupKey select data).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetInvoiceRegisterMailTemplate()
        {
            try
            {
                var query = (from data in finocartEntities1.InvoiceMailTemplates where data.TableTag == "InvoiceUploadEmailToVendor" select data.Template).FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
  
}
