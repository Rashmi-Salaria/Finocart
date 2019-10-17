using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Web.Mvc;
using System.Data.Entity;

namespace ExcelUpload.Scheduler
{
    public interface IUploadExcel
    {

    }

    public class UploadExcel: IUploadExcel
    {
        AnchorCompanyService
        Finocart_V1Entities2 finocartEntities1 = new Finocart_V1Entities2();
        
        //public bool UploadVendors()
        //{
        //    try
        //    {
        //        var AnchCompanyId = HttpContext.Session.GetInt32("CompanyID");
        //        string CompanyName = HttpContext.Session.GetString("Companyname");
        //        IFormFile file = Request.Form.Files[0];
        //        string folderName = "Upload";
        //        string webRootPath = _hostingEnvironment.WebRootPath;
        //        string newPath = Path.Combine(webRootPath, folderName);
        //        DataTable dt = new DataTable();
        //        string JSONString = string.Empty;
        //        if (!Directory.Exists(newPath))
        //        {
        //            Directory.CreateDirectory(newPath);
        //        }
        //        if (file.Length > 0)
        //        {
        //            string sFileExtension = Path.GetExtension(file.FileName).ToLower();
        //            var FileName = CompanyName + DateTime.Now.ToString("yyyyMMddhhmmss");
        //            ISheet sheet;
        //            string fullPath = Path.Combine(newPath, FileName + sFileExtension);
        //            using (var stream = new FileStream("E:\\project\\Finocart_Web\\Development_V1\\Finocart\\Finocart.Web\\wwwroot\\Upload\\Nestle 20190723123945.xlsx", FileMode.Open))
        //            {
        //                file.CopyTo(stream);
        //                stream.Position = 0;
        //                if (sFileExtension == ".xls")
        //                {
        //                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
        //                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
        //                }
        //                else
        //                {
        //                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
        //                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
        //                }

        //                var Res = _AnchorCompanyRepository.InsertVendorExcelPath(fullPath, FileName);

        //                IRow headerRow = sheet.GetRow(0); //Get Header Row
        //                int cellCount = headerRow.LastCellNum;
        //                for (int j = 0; j < cellCount; j++)
        //                {
        //                    NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
        //                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
        //                    dt.Columns.Add(headerRow.GetCell(j).ToString());
        //                }
        //                dt.Columns.Add("Message");

        //                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
        //                {
        //                    DataRow dr = dt.NewRow();
        //                    IRow row = sheet.GetRow(i);
        //                    if (row == null) continue;
        //                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
        //                    for (int j = row.FirstCellNum; j < cellCount; j++)
        //                    {
        //                        if (row.GetCell(j) != null)
        //                            dr[j] = row.GetCell(j).ToString();

        //                    }

        //                    dt.Rows.Add(dr);

        //                    if (dt.Rows[i - 1]["Vendor Name"].ToString() != "" && dt.Rows[i - 1]["Pan Number"].ToString() != "" && dt.Rows[i - 1]["Contact Person Name"].ToString() != "" && dt.Rows[i - 1]["Email ID"].ToString() != "" && dt.Rows[i - 1]["Contact Number"].ToString() != "")
        //                    {

        //                        if (!Regex.IsMatch(dt.Rows[i - 1]["Pan Number"].ToString(), @"^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$"))
        //                        {
        //                            dt.Rows[i - 1]["Message"] = "Pan Number is not valid";
        //                            continue;
        //                        }
        //                        else if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() != "")
        //                        {
        //                            if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() == "yes")
        //                            {
        //                                if (dt.Rows[i - 1]["UAN Number"].ToString() == "")
        //                                {
        //                                    dt.Rows[i - 1]["Message"] = "UAN Number should not be blank";
        //                                    continue;
        //                                }
        //                            }
        //                        }
        //                        else if (dt.Rows[i - 1]["UAN Number"].ToString() != "")
        //                        {
        //                            if (dt.Rows[i - 1]["MSME  (Yes/No)"].ToString().ToLower() == "")
        //                            {
        //                                dt.Rows[i - 1]["Message"] = "MSME should not be blank";
        //                                continue;
        //                            }
        //                        }
        //                        else if (!Regex.IsMatch(dt.Rows[i - 1]["Email ID"].ToString(), @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
        //                        {
        //                            dt.Rows[i - 1]["Message"] = "Email ID is not valid";
        //                            continue;
        //                        }

        //                        else
        //                        {
        //                            dt.Rows[i - 1]["Message"] = "Success";
        //                        }



        //                    }
        //                    else
        //                    {
        //                        if (dt.Rows[i - 1]["Vendor Name"].ToString() == "")
        //                        {
        //                            dt.Rows[i - 1]["Message"] = "Vendor Name should not be blank";
        //                            continue;
        //                        }
        //                        if (dt.Rows[i - 1]["Pan Number"].ToString() == "")
        //                        {
        //                            dt.Rows[i - 1]["Message"] = "Pan Number should not be blank";
        //                            continue;
        //                        }
        //                        if (dt.Rows[i - 1]["Contact Person Name"].ToString() == "")
        //                        {
        //                            dt.Rows[i - 1]["Message"] = "Contact Person Name should not be blank";
        //                            continue;
        //                        }
        //                        if (dt.Rows[i - 1]["Email ID"].ToString() == "")
        //                        {
        //                            dt.Rows[i - 1]["Message"] = "Email ID should not be blank";
        //                            continue;
        //                        }
        //                        if (dt.Rows[i - 1]["Contact Number"].ToString() == "")
        //                        {
        //                            dt.Rows[i - 1]["Message"] = "Contact Number should not be blank";
        //                            continue;
        //                        }
        //                    }

        //                    string randomPassword = _CommonRepository.GeneratePassword();
        //                    string Password = SecurityHelperService.Encrypt(randomPassword);
        //                    var Result = _AnchorCompanyRepository.InsertVendorRecord(dr, AnchCompanyId, Password);
        //                    if (Result > 0)
        //                    {
        //                        string Template = string.Empty;
        //                        IEnumerable<GetVendorRegisterMailTemplate> lstAwaitedInvVendorsView = _companyRepository.GetVendorRegisterMailTemplate(Template);
        //                        string path = lstAwaitedInvVendorsView.ElementAt(0).Template;
        //                        string EMAIL_TOKEN_PAYMENT_LINK = "##$$PAYMENT_LINK$$##";
        //                        string paymentLink = "http://dotnet.brainvire.com/Finocart/Account/AdminLogin";///change url
        //                        //string MailStatus = string.Empty;
        //                        string emailToAddress = dr[6].ToString();
        //                        string subject = "Vendor registration";
        //                        WebClient client = new WebClient();
        //                        string startupPath = Environment.CurrentDirectory;

        //                        string body = path;
        //                        // string body = client.DownloadString(startupPath + "/Views/Template/EmailTemplate.cshtml");
        //                        body = body.Replace("@@User@@", dr[0].ToString());
        //                        body = body.Replace("@@PanNumber@@", dr[1].ToString());
        //                        body = body.Replace("@@ProjectName@@", "Finocart");
        //                        body = body.Replace("@@VendorName@@", dt.Rows[i - 1]["Vendor Name"].ToString());
        //                        body = body.Replace("@@AnchorCompanyname@@", CompanyName);
        //                        body = body.Replace(EMAIL_TOKEN_PAYMENT_LINK, paymentLink);
        //                        body = body.Replace("@@PanNumber@@", dt.Rows[i - 1]["Pan Number"].ToString());
        //                        body = body.Replace("@@Password@@", randomPassword);
        //                        IEnumerable<LookupDetails> lookupDetails = _lookUpRepository.getLookupDetailByKey("SMTPInfo");
        //                        _CommonRepository.SendEmail(lookupDetails, emailToAddress, subject, body, true);
        //                    }

        //                    if (Result == -1)
        //                    {
        //                        dt.Rows[i - 1]["Message"] = "Pan Number already exists";
        //                        continue;
        //                    }

        //                }
        //                //GetLog(dt);
        //            }
        //        }

        //        JSONString = JsonConvert.SerializeObject(dt);
        //        HttpContext.Session.SetString("Excel", JSONString);
        //        return Json(new { result = dt });


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public void SubmitJobCostingExcel(string reqs)
        {
            try
            {
                //if (HttpC == null)
                //{
                //    //return Json(new { code = 1 });

                //}
                //else
                //{
                    int User = 0;
                    var result1 = "";

                    //ForSalesDTO objForSalesDTO = JsonConvert.DeserializeObject<ForSalesDTO>(reqs);
                    //for (int i = 0; i < Request.Files.Count; i++)
                    //{
                    //    var file = Request.Files[i];
                    //    string filename, extension, filenamewithoutext;
                    //    //fname = file.FileName;
                    //    string dt = DateTime.Now.ToString("yyyyMMddhhmmss");
                    //    filename = Path.GetFileName(file.FileName);

                    //    extension = Path.GetExtension(filename);
                    //    filenamewithoutext = Path.GetFileNameWithoutExtension(filename);
                    //filenamewithoutext = filenamewithoutext + dt;
                    //filename = SaleID + filenamewithoutext + extension;

                    //string pathForSaving = Microsoft.SqlServer.Server.MapPath("~/ExcelFileUpload");
                    //bool exists = Directory.Exists(pathForSaving);
                    //if (!exists)
                    //{
                    //    Directory.CreateDirectory(pathForSaving);
                    //}

                    var location = "E:\\project\\Finocart_Web\\Development_V1\\Finocart\\Finocart.Web\\wwwroot\\Upload\\Nestle 20190723123945.xlsx";
                    //file.SaveAs(location);
                    string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + location + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=0;TypeGuessRows=0;ImportMixedTypes=Text\"";

                    User = Import_To_Grid(location, strConn);
                    //if ((DataTable)Session["Export"] != null)
                    //{
                    //    DataTable Filter = (DataTable)Session["Export"];
                    //    if (Convert.ToInt16(Session["Validation"]) == Filter.Rows.Count)
                    //    {
                    //        result1 = "Success";
                    //    }
                    //    else
                    //    {
                    //        result1 = "UnSuccess";
                    //    }
                    //}

                //}
                //var result = new { Result = result1, data = User };

                //return jsonResult;
                //}
            }
            catch (Exception ex)
            {
                //dbErrorLogging.LogError(ex);
                throw ex;
            }
        }
            public int Import_To_Grid(string FilePath, string conStr)
            {
                //DBHelper _db = new DBHelper();
                int RecordCount = 0;
                int result = 0;
                DataSet result1 = new DataSet();
                var Category = "";
                var Vendor = "";
                var SaleID = "";

                try
                {
                    OleDbConnection connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dt = new DataTable();
                    cmdExcel.Connection = connExcel;
                    connExcel.Open();
                    DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(dt);
                    //if (dt.Rows.Count == 0)
                    //{
                    //    return -2;
                    //}
                    //else
                    //{
                        DataTable filteredRows = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        //DataTable ColumnDataTable = objForSalesBal.GetTableColumnName();
                        RecordCount = filteredRows.Rows.Count;
                        List<string> newlist = new List<string>();
                        string[] ExcelColumn = filteredRows.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                        //string[] Column = ColumnDataTable.AsEnumerable().Select(r => r.Field<string>("COLUMNNAME")).ToArray();
                        //var diff = ExcelColumn.Except(Column);
                        //var remove = Column.Except(ExcelColumn);
                        filteredRows.Columns.Add("Message");
                    //string saleid = Session["SaleID"].ToString();

                    //if (diff.Count() == 0 && remove.Count() == 0)
                    //{
                        for (int i = 0; i < filteredRows.Rows.Count; i++)
                        {

                        //        _db.AddParameter("@SP_TYPE", "Message", SqlDbType.VarChar, ParameterDirection.Input);
                        //        _db.AddParameter("@SaleID", filteredRows.Rows[i]["Ref#"], SqlDbType.VarChar, ParameterDirection.Input);
                        //        _db.AddParameter("@Category", filteredRows.Rows[i]["Category"], SqlDbType.VarChar, ParameterDirection.Input);
                        //        _db.AddParameter("@Vendor", filteredRows.Rows[i]["Vendor"], SqlDbType.VarChar, ParameterDirection.Input);
                        //        //_db.AddParameter("@PaymentStatus", filteredRows.Rows[i]["Payment Status"], SqlDbType.VarChar, ParameterDirection.Input);
                        //        result1 = _db.ExecuteDataSet("USP_JobCosting", CommandType.StoredProcedure);

                        //        if (result1.Tables[0].Rows.Count > 0)
                        //        {
                        //            foreach (DataRow objNotes in result1.Tables[0].Rows)
                        //            {
                        //                Category = objNotes["returnValue"].ToString();

                        //            }
                        //        }
                        //        if (result1.Tables[1].Rows.Count > 0)
                        //        {
                        //            foreach (DataRow objNotes in result1.Tables[1].Rows)
                        //            {

                        //                Vendor = objNotes["returnValue1"].ToString();
                        //            }
                        //        }
                        //        if (result1.Tables[2].Rows.Count > 0)
                        //        {
                        //            foreach (DataRow objNotes in result1.Tables[2].Rows)
                        //            {

                        //                SaleID = objNotes["returnValue2"].ToString();
                        //            }
                        //        }


                        if (filteredRows.Rows[i]["Vendor Name"].ToString() != "" && filteredRows.Rows[i]["Pan Number"].ToString() != "" && filteredRows.Rows[i]["Contact Person Name"].ToString() != "" && filteredRows.Rows[i]["Email ID"].ToString() != "" && filteredRows.Rows[i]["Contact Number"].ToString() != "")
                        {

                            if (!Regex.IsMatch(filteredRows.Rows[i]["Pan Number"].ToString(), @"^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$"))
                            {
                                filteredRows.Rows[i]["Message"] = "Pan Number is not valid";
                                continue;
                            }
                            else if (filteredRows.Rows[i]["MSME  (Yes/No)"].ToString().ToLower() != "")
                            {
                                if (filteredRows.Rows[i]["MSME  (Yes/No)"].ToString().ToLower() == "yes")
                                {
                                    if (filteredRows.Rows[i]["UAN Number"].ToString() == "")
                                    {
                                        filteredRows.Rows[i]["Message"] = "UAN Number should not be blank";
                                        continue;
                                    }
                                }
                            }
                            else if (filteredRows.Rows[i]["UAN Number"].ToString() != "")
                            {
                                if (filteredRows.Rows[i]["MSME  (Yes/No)"].ToString().ToLower() == "")
                                {
                                    filteredRows.Rows[i]["Message"] = "MSME should not be blank";
                                    continue;
                                }
                            }
                            else if (!Regex.IsMatch(filteredRows.Rows[i]["Email ID"].ToString(), @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                            {
                                filteredRows.Rows[i]["Message"] = "Email ID is not valid";
                                continue;
                            }

                            else
                            {
                                filteredRows.Rows[i]["Message"] = "Success";
                            }



                        }
                        else
                        {
                            if (filteredRows.Rows[i]["Vendor Name"].ToString() == "")
                            {
                                filteredRows.Rows[i]["Message"] = "Vendor Name should not be blank";
                                continue;
                            }
                            if (filteredRows.Rows[i]["Pan Number"].ToString() == "")
                            {
                                filteredRows.Rows[i]["Message"] = "Pan Number should not be blank";
                                continue;
                            }
                            if (filteredRows.Rows[i]["Contact Person Name"].ToString() == "")
                            {
                                dt.Rows[i - 1]["Message"] = "Contact Person Name should not be blank";
                                continue;
                            }
                            if (filteredRows.Rows[i]["Email ID"].ToString() == "")
                            {
                                filteredRows.Rows[i]["Message"] = "Email ID should not be blank";
                                continue;
                            }
                            if (filteredRows.Rows[i]["Contact Number"].ToString() == "")
                            {
                                filteredRows.Rows[i]["Message"] = "Contact Number should not be blank";
                                continue;
                            }
                        }
                    //}
                    var Result = InsertVendorRecord(filteredRows);
                }



                return 1;

                }
                catch (Exception ex)
                {
                    //dbErrorLogging.LogError(ex);
                    throw ex;
                }

            }

        //public int InsertVendorRecord(DataRow VendorDetails, Int64? AnchorCompId, string randomPassword)
        //{
        //    int result = 0;

        //    try
        //    {
        //        RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
        //        ICollection<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", AnchorCompId, System.Data.SqlDbType.BigInt));
        //        parameters.Add(SQLHelper.SqlInputParam("@VendorName", VendorDetails[0].ToString(), System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@Password", randomPassword, System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@PanNumber", VendorDetails[1].ToString(), System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@MIME", VendorDetails[2].ToString(), System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@UANNumber", VendorDetails[3].ToString(), System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@ContactPersonName", VendorDetails[4].ToString(), System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@ContactPersonDesignation", VendorDetails[5].ToString(), System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@EmailID", VendorDetails[6].ToString(), System.Data.SqlDbType.VarChar));
        //        parameters.Add(SQLHelper.SqlInputParam("@ContactNumber", VendorDetails[7].ToString(), System.Data.SqlDbType.VarChar));
        //        result = objCompany.ExecuteSqlCommand("proc_InsertVendorData  @AnchorCompID, @VendorName, @Password, @PanNumber, @MIME, @UANNumber, @ContactPersonName, @ContactPersonDesignation, @EmailID, @ContactNumber", parameters.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return result;
        //}

        private bool InsertVendorRecord(DataTable filteredRows)
        {
            try
            {

                //var invoice = (from data in finocartEntities1.Invoice where data.InvoiceID == invoiceId select data).FirstOrDefault();
                //invoice.InvoiceStatus = Convert.ToInt32(ClsConstants.Status.Approved);
                //invoice.PaymentDate = paymentdate;
                //invoice.AmountPaid = amountPaid;
                //invoice.Earning = discountAmount;
                finocartEntities1.proc_InsertVendorData();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
