using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Finocart.Services
{
    /// <summary>
    /// Common service class
    /// </summary>
    public class CommonService : ICommon
    {
        #region Declartion 

        /// <summary>
        /// Context
        /// </summary>
        private readonly VMSContext _vContext;
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CommonService(VMSContext context, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _vContext = context;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion
        #region Common Methods S

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="signatureImagePath">The signature image path.</param>
        /// <param name="path">The path.</param>
        public bool DeleteFile(string DocumentPath, string FileName)
        {
            bool bResult = false;
            string deletePath = string.Empty;

            if (!string.IsNullOrWhiteSpace(DocumentPath))
            {
                string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot", DocumentPath));
                deletePath = Path.Combine(filePath, FileName);

                if (File.Exists(deletePath))
                {
                    try
                    {
                        File.Delete(deletePath);
                        bResult = true; ;
                    }
                    catch(Exception)
                    {
                        bResult = false;
                    }
                }
            }
            return bResult;
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="lookupDetails"></param>
        /// <param name="emailToAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        public void SendEmail(IEnumerable<LookupDetails> lookupDetails, string emailToAddress, string subject, string body, bool isBodyHtml)
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
                    message.From = new MailAddress(gmailUserName,"Finocart");
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

        //public void SendEmailBrainvire1(IEnumerable<LookupDetails> lookupDetails, string emailToAddress, string subject, string body, bool isBodyHtml)
        //{

        //    string gmailUserName = lookupDetails.ElementAt(0).LookupValue;
        //    string gmailUserPassword = lookupDetails.ElementAt(1).LookupValue;
        //    string SMTPSERVER = lookupDetails.ElementAt(2).LookupValue;
        //    int PORTNO = Convert.ToInt32(lookupDetails.ElementAt(3).LookupValue);

        //    SmtpClient smtpClient = new SmtpClient(SMTPSERVER, PORTNO);
        //    smtpClient.EnableSsl = true;
        //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpClient.UseDefaultCredentials = false;
        //    smtpClient.Credentials = new NetworkCredential(gmailUserName, gmailUserPassword);
        //    //foreach (var address in emailToAddress.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        using (MailMessage message = new MailMessage())
        //        {
        //            message.From = new MailAddress(gmailUserName);
        //            message.Subject = subject == null ? "" : subject;
        //            foreach (var bd in body)
        //            {
        //                message.Body = body == null ? "" : body;

        //            }
                   
        //            message.IsBodyHtml = isBodyHtml;
        //            message.To.Add(emailToAddress);

        //            try
        //            {
        //                smtpClient.Send(message);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //    }
        //}


        /// <summary>
        /// Export List as CSV
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="ExcludeColumns">Pass comma separated column names</param>
        /// <returns></returns>
        public static string ListToCSV<T>(IEnumerable<T> list, string ExcludeColumns = "")
        {
            StringBuilder sList = new StringBuilder();

            Type type = typeof(T);
            var props = type.GetProperties();
          

            if (string.IsNullOrEmpty(ExcludeColumns))
            {
                //string newLine = string.Format("{0},{1}", string.Join(",", props.Select(p => p.Name)));
                //sList.Append(newLine);
                sList.Append(string.Join(",", props.Select(p => p.Name)));
            }
            else
            {
                foreach (var propertyInfo in props)
                {
                    if (!string.IsNullOrEmpty(ExcludeColumns))
                    {
                        if (!ExcludeColumns.ToLower().Contains(propertyInfo.Name.ToLower()))
                        {
                            if (sList.Length == 0)
                            {
                                //string newLine = string.Format("{0},{1}", propertyInfo.Name);
                                sList.Append(propertyInfo.Name);
                                //sList.Append(newLine);
                            }
                            else
                            {
                                //string newLine = string.Format("{0},{1}", propertyInfo.Name);
                                sList.Append("," + propertyInfo.Name);
                                //sList.Append("," + newLine);
                            }
                        }
                    }
                }
            }
            string customCOlumns = sList.ToString();

            sList.Append(Environment.NewLine);


            foreach (var element in list)
            {
                if (string.IsNullOrEmpty(ExcludeColumns))
                {
                    sList.Append(string.Join(",", props.Select(p => p.GetValue(element, null))));
                }
                else
                {
                    int j = 0;
                    foreach (var propertyInfo in props)
                    {
                        if (!ExcludeColumns.ToLower().Contains(propertyInfo.Name.ToLower()))
                        {
                            if (j == 0)
                            {
                                sList.Append(propertyInfo.GetValue(element, null));
                                j = j + 1;
                            }
                            else
                            {
                                sList.Append("," + propertyInfo.GetValue(element, null));
                            }
                        }
                    }
                }

                sList.Append(Environment.NewLine);
            }
            return sList.ToString();
        }

        //public bool uploadFiletoServer(IConfiguration _configuration) //string strFiles, IFormFile fromFile)
        //{
        //    try
        //    {
        //        // First way  
        //        //string conectionString = _configuration.GetSection("ConnectionStrings").GetSection("EmployeeDatabase").Value;
        //        string aalowExtension = _configuration.GetSection("Extension").GetSection("AllowFileExtension").Value;
        //        string preventExtension = _configuration.GetSection("Extension").GetSection("PreventFileExtension").Value;

        //        // Second way  
        //        //string value2 = _configuration.GetValue<string>("Data:ConnectionString");
        //        string VendorDocumentsFolder = _configuration["VendorDocumentsFolder"];
        //        string AnchorDocumentsFolder = _configuration["AnchorDocumentsFolder"];





        //    }
        //    catch (Exception)
        //    {

        //        //throw
        //    }
        //    return false;
        //}
        #endregion

        /// <summary>
        /// Check User Old Password
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> CheckUserPassword()
        {
            RepositoryService<User> objUser = new RepositoryService<User>(_vContext);
            return objUser.SelectAll();
        }

        /// <summary>
        /// Checking if user exist by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public User CheckUserByEmail(string email, bool isDelete)
        {
            RepositoryService<User> objUser = new RepositoryService<User>(_vContext);
            User user = objUser.SelectAll().Where(x => x.Email == email && x.IsDelete == isDelete).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// checking if user exist by email
        /// and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User CheckUser(string email, string password)
        {
            RepositoryService<User> objUser = new RepositoryService<User>(_vContext);
            User user = objUser.SelectAll().Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// Check Admin Old Password
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Company> CheckAdminPassword()
        {
            RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
            //obj.SelectAll();
            return objCompany.SelectAll();
        }

        /// <summary>
        /// Checking if Admin exist by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public Company CheckAdminByEmail(string email, bool isDelete)
        {
            RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
            Company company = objCompany.SelectAll().Where(x=>x.Contact_email == email && x.IsDelete == isDelete).FirstOrDefault();
            return company;
        }

        /// <summary>
        /// Checking if Admin exist by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Company CheckAdminByEmailExists(string email)
        {
            RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
            Company company = objCompany.SelectAll().Where(x => x.Contact_email == email).FirstOrDefault();
            return company;
        }

        /// <summary>
        /// Checking if Admin exist by email
        /// and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public Company CheckAdmin(string email, string password)
        {
            RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
            Company company = objCompany.SelectAll().Where(x => x.Pan_number == email && x.Password == password).FirstOrDefault();
            return company;
        }

        /// <summary>
        /// Check Super Admin Old Password
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FinocartMaster> CheckSuperAdminPassword()
        {
            RepositoryService<FinocartMaster> objFinocartMaster = new RepositoryService<FinocartMaster>(_vContext);
            //obj.SelectAll();
            return objFinocartMaster.SelectAll();
        }

        /// <summary>
        /// Checking if super admin exist by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public FinocartMaster CheckSuperAdminByEmail(string email, bool isDelete)
        {
            RepositoryService<FinocartMaster> objFinocartMaster = new RepositoryService<FinocartMaster>(_vContext);
            FinocartMaster finocartMaster = objFinocartMaster.SelectAll().Where(x=>x.EmailId == email && x.IsDelete == isDelete).FirstOrDefault();
            return finocartMaster;
        }

        /// <summary>
        /// Checking if super admin exist by email
        /// and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public FinocartMaster CheckSuperAdmin(string email, string password)
        {
            RepositoryService<FinocartMaster> objFinocartMaster = new RepositoryService<FinocartMaster>(_vContext);
            FinocartMaster finocartMaster = objFinocartMaster.SelectAll().Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
            return finocartMaster;
        }

        /// <summary>
        /// Update new password
        /// </summary>
        /// <param name="objChangePassword"></param>
        /// <returns></returns>
        public int UpdateNewPassword(ChangePasswordModel objChangePassword)
        {
            int result = 0;

            try
            {
                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Role", objChangePassword.Role, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserId", objChangePassword.UserId, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Password", objChangePassword.NewPassword, System.Data.SqlDbType.VarChar));
                result = objUserModel.ExecuteSqlCommand("proc_ChangePassword  @Role,@UserId,@Password", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #region Upload file 
        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="documentFolderPath"></param>
        /// <param name="exportFileName"></param>
        /// <returns></returns>
        public bool UploadFileToServer(IFormFile formFile, string documentFolderPath, string exportFileName)
        {
            string errorMessge = string.Empty;
            bool bResult = false;
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
                    string UploadedFileFolder ="UploadedDocuments";
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    //string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(UploadedFileFolder))
                    {
                        Directory.CreateDirectory(UploadedFileFolder);
                    }
                    string filePath = Path.GetFullPath(Path.Combine(webRootPath,
                            UploadedFileFolder, documentFolderPath));

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
                    bResult = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
                errorMessge = "Error during upload file. Please contact administrator.";
                throw ex;
            }
            return bResult;
        }
        #endregion

        /// <summary>
        /// Getting user list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserList()
        {
            RepositoryService<User> objUser = new RepositoryService<User>(_vContext);
            //obj.SelectAll();
            return objUser.SelectAll();
        }
        

        #region Validate vendor user
        /// <summary>
        /// GetFileNameById
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool ValidateVendorUser(Int32? UserID,string FileName)
        {
            bool IsValidUser = false;
            try
            {
                VendorDocument vendorDocument = new VendorDocument();
                RepositoryService<VendorDocument> objVendorDocument = new RepositoryService<VendorDocument>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@FileName", FileName, System.Data.SqlDbType.VarChar));
                var data = objVendorDocument.ExecWithStoreProcedure("proc_GetVendorDocUser  @UserID,@FileName", parameters.ToArray());
                IEnumerable<VendorDocument> lstVendorDocUser = data.ToList();
                if (lstVendorDocUser.Count()>0)
                {
                    IsValidUser = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValidUser;
        }
        #endregion

        #region
        /// <summary>
        /// Validate AnchorComp User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool ValidateAnchorCompUser(Int32? UserID,string FileName)
        {
            bool IsValidUser = false;
            try
            {
                AnchorCompanyDocument vendorDocument = new AnchorCompanyDocument();
                RepositoryService<AnchorCompanyDocument> objAnchorCompanyDocument = new RepositoryService<AnchorCompanyDocument>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@FileName", FileName, System.Data.SqlDbType.VarChar));

                var data = objAnchorCompanyDocument.ExecWithStoreProcedure("proc_GetAnchorCompDocUser  @UserID,@FileName", parameters.ToArray());
                IEnumerable<AnchorCompanyDocument> lstAnchorCompDocUser = data.ToList();
                if (lstAnchorCompDocUser.Count() > 0)
                {
                    IsValidUser = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValidUser;
        }
        #endregion

        #region Generate random password
        /// <summary>
        /// Generating random password
        /// </summary>
        /// <returns></returns>
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
        #endregion

        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="EmailID"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public int UpdatePassword(string Password, string EmailID, string Role)
        {
            int result = 0;

            try
            {
                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Role", Role, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Email", EmailID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Password", Password, System.Data.SqlDbType.VarChar));
                result = objUserModel.ExecuteSqlCommand("proc_UpdatePassword  @Role, @Email, @Password", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #region
        /// <summary>
        /// Check if email id exists
        /// </summary>
        /// <param name="EmailID"></param>
        /// <returns></returns>
        public int? CheckIfEmailIdExists(string EmailID, string Type)
        {
            int? isExist = 0;
            try
            {
                RepositoryService<CompanyCredentialsModel> objCompanyCredentialsModel = new RepositoryService<CompanyCredentialsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@Type", Type, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@EmailId", EmailID, System.Data.SqlDbType.VarChar));
                var result = objCompanyCredentialsModel.ExecWithStoreProcedure("proc_IfEmailIdExists  @EmailId,@Type", parameters.ToArray());
                IEnumerable<CompanyCredentialsModel> lstEmailId = result.ToList();
                isExist = lstEmailId.FirstOrDefault().IsExistEmailId;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return isExist;

        }
        #endregion

        #region Insert Log Management 

        public int LogManagement(string ControllerName, string ActionName, string ErrorMessage, int ErrorLine, Int32? UserID)
        {
            int result = 0;

            try
            {
                RepositoryService<LogManagement> obj = new RepositoryService<LogManagement>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ControllerName", ControllerName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ActionName", ActionName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Message", ErrorMessage, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ErrorLine", ErrorLine, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.VarChar));
                result = obj.ExecuteSqlCommand("SP_SaveLogManagement @ControllerName,@ActionName,@Message,@ErrorLine,@UserID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Insert Audit Trail

        public int AuditTrailLog(string ModuleName, string PageName, Int32? UserID, int CompanyID)
        {
            int result = 0;

            try
            {
                RepositoryService<LogManagement> obj = new RepositoryService<LogManagement>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ModuleName", ModuleName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageName", PageName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UserID", UserID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.VarChar));
                result = obj.ExecuteSqlCommand("SP_SaveAuditTrailLog @ModuleName,@PageName,@UserID,@CompanyID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion


        #region
        /// <summary>
        /// Check if email id exists
        /// </summary>
        /// <param name="CustomFilter"></param>
        /// <returns></returns>
        
        public string CustomUserFilter(string controllername, string actionname, string RoleAccess, string Role)
        {
            string result = "0";
           
            try
            {
                RepositoryService<ActionFilter> objCompanyCredentialsModel = new RepositoryService<ActionFilter>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ControllerName", controllername, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ActionName", actionname, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@RoleAccess", RoleAccess, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Role", Role, System.Data.SqlDbType.VarChar));
                var data = objCompanyCredentialsModel.ExecWithStoreProcedure("Proc_RoleAccessFilter  @ControllerName,@ActionName,@RoleAccess,@Role", parameters.ToArray());
                IEnumerable<ActionFilter> checkdata = data.ToList();
                if(checkdata.Count()>=1)
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return result;
            throw new NotImplementedException();
        }
        #endregion
    }


}
