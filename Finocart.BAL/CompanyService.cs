using Finocart.CustomModel;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class CompanyService : ICompany
    {
        #region Declartion 

        /// <summary>
        /// Context
        /// </summary>
        private readonly VMSContext _vContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CompanyService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion

        #region Insert Company Record
        /// <summary>
        /// Register anchor company
        /// </summary>
        /// <param name="objCompany"></param>
        /// <returns></returns>
        public int InsertAnchorCompanyRegister(Company objCompany)
        {
            int result = 0;

            try
            {
                RepositoryService<Company> objComp = new RepositoryService<Company>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", objCompany.CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@Company_Name", objCompany.Company_name, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Login_Id", objCompany.Login_id, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Password", SecurityHelperService.Encrypt(objCompany.Password), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Address", objCompany.Address, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InterestedAS", objCompany.InterestedAs, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyType", objCompany.CompanyType, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Pan_Number", objCompany.Pan_number, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Title", objCompany.Contact_Title, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Name", objCompany.Contact_Name, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Designation", objCompany.Contact_Designation, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Email", objCompany.Contact_email, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Mobile", objCompany.Contact_mobile, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Comment", objCompany.Contact_Comments, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_CIN", objCompany.Contact_CIN, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CreatedBy", objCompany.CreatedBy, System.Data.SqlDbType.VarChar));//set CreatedBy dynamically
                parameters.Add(SQLHelper.SqlInputParam("@UpdatedBy", objCompany.UpdatedBy, System.Data.SqlDbType.VarChar));//set UpdatedBy dynamically
                parameters.Add(SQLHelper.SqlInputParam("@FactoryOrReverseFactory", objCompany.FactoryOrReverseFactory, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@Status", objCompany.status, System.Data.SqlDbType.Int));
                result = objComp.ExecuteSqlCommand("proc_AddUpdateCompany  @CompanyID,@Company_Name, @Login_Id, @Password,@Address, @InterestedAS,@CompanyType,@Pan_Number,@Contact_CIN,@Contact_Title,@Contact_Name,@Contact_Designation,@Contact_Email,@Contact_Mobile,@Contact_Comment,@CreatedBy,@UpdatedBy,@FactoryOrReverseFactory,@Status", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        public IEnumerable<GetSumOfAmountPaid> UpdateCompanyRate_Days(int? CompanyID, decimal PercentageRate, int PaymentDays, string InternalFundLimits, bool chkUnlimited)
        {

            try
            {
                RepositoryService<GetSumOfAmountPaid> objComp = new RepositoryService<GetSumOfAmountPaid>(_vContext);
                
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@PercentageRate", PercentageRate, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@PaymentDays", PaymentDays, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@InternalFundLimit", InternalFundLimits, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@IsLimitUnlimited", chkUnlimited, System.Data.SqlDbType.Bit));

                var result = objComp.ExecWithStoreProcedure("proc_updateRate_Days  @CompanyID,@PercentageRate,@PaymentDays,@IsLimitUnlimited,@InternalFundLimit", parameters.ToArray());
                IEnumerable<GetSumOfAmountPaid> maxAmountPaid = result.ToList();
                return maxAmountPaid;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Check if pan number exists
        /// <summary>
        /// Check if pan number exists
        /// </summary>
        /// <param name="pan"></param>
        /// <returns></returns>
        public string CheckPan(string pan)
        {
            int? isExist = 0;
            string rolename = "";
            try
            {
                RepositoryService<CompanyCredentialsModel> objCompanyCredentialsModel = new RepositoryService<CompanyCredentialsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@PanNumber", pan, System.Data.SqlDbType.VarChar));
                var result = objCompanyCredentialsModel.ExecWithStoreProcedure("proc_GetPan  @PanNumber", parameters.ToArray());
                IEnumerable<CompanyCredentialsModel> PanCheck = result.ToList();

                if (PanCheck.Count() != 0)
                {
                    //isExist = PanCheck.FirstOrDefault().IsExistPan;
                    rolename = PanCheck.FirstOrDefault().RoleAccess;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rolename;
        }


        #endregion

        #region Check if Email Id exists
        /// <summary>
        /// Check if pan number exists
        /// </summary>
        /// <param name="pan"></param>
        /// <returns></returns>
        public int? CheckEmail(string email)
        {
            int? isExist = 0;
            try
            {
                RepositoryService<CompanyCredentialsModel> objCompanyCredentialsModel = new RepositoryService<CompanyCredentialsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@EmailId", email, System.Data.SqlDbType.VarChar));
                var result = objCompanyCredentialsModel.ExecWithStoreProcedure("proc_GetEmail  @EmailId", parameters.ToArray());
                IEnumerable<CompanyCredentialsModel> EmailCheck = result.ToList();

                if (EmailCheck.Count() != 0)
                {
                    isExist = EmailCheck.FirstOrDefault().IsExistEmailId;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return isExist;
        }


        #endregion        

        #region Get company details by ID
        /// <summary>
        /// Get company details by ID 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Company GetCompany(Int32? ID)
        {
            Company company = new Company();
            RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
            company = objCompany.GetByID(ID);
            return company;
        }
        #endregion

        #region Get Company Listing
        /// <summary>
        /// Getting company listing
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="CompanyName"></param>
        /// <param name="ContactPerson"></param>
        /// <returns></returns>
        public IEnumerable<GetCompanyModel> GetCompanyListing(string sortBy, int pageSize, Int64 skip, string CompanyName, string InterestedAs, string name)
        {

            try
            {
                RepositoryService<GetCompanyModel> obj = new RepositoryService<GetCompanyModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@pageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
                if (InterestedAs == "")
                {
                    parameters.Add(SQLHelper.SqlInputParam("@InterestedAs", "" + 0 + "", System.Data.SqlDbType.Int));
                }
                else
                {
                    parameters.Add(SQLHelper.SqlInputParam("@InterestedAs", "" + Convert.ToInt16(InterestedAs) + "", System.Data.SqlDbType.Int));
                }
                parameters.Add(SQLHelper.SqlInputParam("@name", "" + name + "", System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("proc_GetCompanyList @sortBy,@pageSize,@skip,@CompanyName,@name,@InterestedAs", parameters.ToArray());
                IEnumerable<GetCompanyModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region EDIT details of Company
        /// <summary>
        /// Edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Company EditPage(int id)
        {
            Company objCompanyModels = new Company();
            try
            {
                RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", id, System.Data.SqlDbType.Int));
                var data = objCompany.ExecWithStoreProcedure("proc_EditCompany  @CompanyID", parameters.ToArray());
                IEnumerable<Company> lstCompany = data.ToList();
                foreach (var v in lstCompany)
                {
                    objCompanyModels.CompanyID = lstCompany.ElementAt(0).CompanyID;
                    objCompanyModels.Company_name = lstCompany.ElementAt(0).Company_name;
                    objCompanyModels.Contact_CIN = lstCompany.ElementAt(0).Contact_CIN;
                    objCompanyModels.Contact_Comments = lstCompany.ElementAt(0).Contact_Comments;
                    objCompanyModels.Pan_number = lstCompany.ElementAt(0).Pan_number;
                    objCompanyModels.Contact_Name = lstCompany.ElementAt(0).Contact_Name;
                    objCompanyModels.Contact_Designation = lstCompany.ElementAt(0).Contact_Designation;
                    objCompanyModels.Contact_email = lstCompany.ElementAt(0).Contact_email;
                    objCompanyModels.Contact_mobile = lstCompany.ElementAt(0).Contact_mobile;
                    objCompanyModels.InterestedAs = lstCompany.ElementAt(0).InterestedAs;
                    objCompanyModels.Address = lstCompany.ElementAt(0).Address;
                    objCompanyModels.Contact_Title = lstCompany.ElementAt(0).Contact_Title;
                    objCompanyModels.FactoryOrReverseFactory = lstCompany.ElementAt(0).FactoryOrReverseFactory;
                    objCompanyModels.status = lstCompany.ElementAt(0).status;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCompanyModels;
        }


        #endregion

        #region Get look Up Details
        /// <summary>
        /// Get lookup dropdown
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GetLookUpModel> GetLookUpDropDown()
        {
            RepositoryService<GetLookUpModel> objGetLookUpModel = new RepositoryService<GetLookUpModel>(_vContext);
            IEnumerable<GetLookUpModel> lstLookUp;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SPType", "GetLookUpDropDown", System.Data.SqlDbType.VarChar));
                lstLookUp = (objGetLookUpModel.ExecWithStoreProcedure("proc_GetLookUpDropDown  @SPType", parameters.ToArray())).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            return lstLookUp;
        }
        public IEnumerable<GetLookUpModel> GetFactoryOrReverse()
        {

            RepositoryService<GetLookUpModel> objGetLookUpModel = new RepositoryService<GetLookUpModel>(_vContext);
            IEnumerable<GetLookUpModel> lstLookUp;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SPType", "GetLOOKUFACTORINGORREV", System.Data.SqlDbType.VarChar));
                lstLookUp = (objGetLookUpModel.ExecWithStoreProcedure("proc_GetLOOKUFACTORINGORREV  @SPType", parameters.ToArray())).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            return lstLookUp;
        }

        public IEnumerable<GetStatus> GetCuurentStatus()
        {
            RepositoryService<GetStatus> objGetLookUpModel = new RepositoryService<GetStatus>(_vContext);
            IEnumerable<GetStatus> lstLookUp;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@SPType", "GetActiveInactive", System.Data.SqlDbType.VarChar));
                lstLookUp = (objGetLookUpModel.ExecWithStoreProcedure("proc_GetActiveInactive  @SPType", parameters.ToArray())).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            return lstLookUp;
        }
        #endregion

        #region Get Uplaod Vendor Listing
        /// <summary>
        /// Get upload vendor list
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="VendorId"></param>
        /// <param name="POName"></param>
        /// <param name="InvoiveNo"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IEnumerable<GetUploadVendorListModel> GetUploadVendorList(Int64? CompanyID, string VendorID, Int64? AnchorCompID, string POName, string InvoiceDate, string INVOICENO, string InvoiceAmt, string ApprovedAmt, string InvoiceStatus, string PaymentDays, string sortBy, int pageSize, Int64 skip)
        {
            RepositoryService<GetUploadVendorListModel> objGetUploadVendorListModel = new RepositoryService<GetUploadVendorListModel>(_vContext);
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorId", VendorID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorCompID", AnchorCompID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@pageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@POName", POName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceDate", InvoiceDate, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceNO", INVOICENO, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceAmt", InvoiceAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ApprovedAmt", ApprovedAmt, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@InvoiceStatus", InvoiceStatus, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PaymentDays", PaymentDays, System.Data.SqlDbType.VarChar));
               
                var data = objGetUploadVendorListModel.ExecWithStoreProcedure("proc_UploadAllVendorListing @VendorId,@CompanyID,@AnchorCompID,@sortBy,@pageSize,@skip,@POName,@InvoiceDate," +
                    "@InvoiceNO,@InvoiceAmt,@ApprovedAmt,@InvoiceStatus,@PaymentDays", parameters.ToArray());
                IEnumerable<GetUploadVendorListModel> lstComapany = data.ToList();
                return lstComapany;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


        #region Get Register Mail  Template
        /// <summary>
        /// Get register mail template
        /// </summary>
        /// <param name="LookupKey"></param>
        /// <returns></returns>
        public IEnumerable<GetRegisterMailTemplate> getRestraterMailTemplate()
        {
            try
            {

                RepositoryService<GetRegisterMailTemplate> objGetRegisterMailTemplate = new RepositoryService<GetRegisterMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                var data = objGetRegisterMailTemplate.ExecWithStoreProcedure("Proc_getRegisterEmailTemplate");
                IEnumerable<GetRegisterMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region Get Vendor Register Mail  Template

        public IEnumerable<GetVendorRegisterMailTemplate> GetVendorRegisterMailTemplate(string LookupKey)
        {
            try
            {

                RepositoryService<GetVendorRegisterMailTemplate> obj = new RepositoryService<GetVendorRegisterMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@LookupFor", LookupKey, System.Data.SqlDbType.VarChar));

                var data = obj.ExecWithStoreProcedure("Proc_getVendorRegisterEmailTemplate @LookupFor", parameters.ToArray());
                IEnumerable<GetVendorRegisterMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region Get Invoice Register Mail  Template

        public IEnumerable<GetInvoiceRegisterMailTemplate> GetInvoiceRegisterMailTemplate(string LookupKey)
        {
            try
            {

                RepositoryService<GetInvoiceRegisterMailTemplate> obj = new RepositoryService<GetInvoiceRegisterMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@LookupFor", LookupKey, System.Data.SqlDbType.VarChar));

                var data = obj.ExecWithStoreProcedure("Proc_getInvoiceRegisterEmailTemplate @LookupFor", parameters.ToArray());
                IEnumerable<GetInvoiceRegisterMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region Get Uplaod Vendor Listing
        public IEnumerable<GetUploadVendorListModel1> GetUploadVendorList1(Int64? VendorId)
        {
            RepositoryService<GetUploadVendorListModel1> obj = new RepositoryService<GetUploadVendorListModel1>(_vContext);
            IEnumerable<GetUploadVendorListModel1> lstComapany;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@VendorId", VendorId, System.Data.SqlDbType.VarChar));
                lstComapany = (obj.ExecWithStoreProcedure("proc_VendorListing @VendorId", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstComapany;
        }
        #endregion

        #region Get Audit Trail Log Listing

        public IEnumerable<GetAuditTrailLogModel> GetAuditTrailLogListing(string sortBy, int pageSize, Int64 skip, string CompanyName, string ContactPerson)
        {

            try
            {
                RepositoryService<GetAuditTrailLogModel> obj = new RepositoryService<GetAuditTrailLogModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", "" + sortBy + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@pageSize", "" + pageSize + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@skip", "" + skip + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + CompanyName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@ContactPerson", "" + ContactPerson + "", System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("proc_GetAuditTrailLogList @sortBy,@pageSize,@skip,@CompanyName,@ContactPerson", parameters.ToArray());
                IEnumerable<GetAuditTrailLogModel> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region EDIT details of Company

        public EditSendGridDetailsModel SendGridEditPage()
        {
            EditSendGridDetailsModel objCompanyModels = new EditSendGridDetailsModel();
            EditSendGridDetails objCompanyModelss = new EditSendGridDetails();
            try
            {
                RepositoryService<EditSendGridDetailsModel> objCompany = new RepositoryService<EditSendGridDetailsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", 0, System.Data.SqlDbType.Int));
                var data = objCompany.ExecWithStoreProcedure("proc_EditSendGridDetails  @CompanyID", parameters.ToArray());
                IEnumerable<EditSendGridDetailsModel> lstCompany = data.ToList();
                if (lstCompany != null && lstCompany.Count() != 0)
                {
                    objCompanyModels.UserId = lstCompany.ElementAt(0).LookupValue;
                    objCompanyModels.Password = lstCompany.ElementAt(1).LookupValue;
                    objCompanyModels.HostName = lstCompany.ElementAt(2).LookupValue;
                    objCompanyModels.PortNO = lstCompany.ElementAt(3).LookupValue;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCompanyModels;
        }


        #endregion

        #region Insert Company Record
        /// <summary>
        /// Register anchor company
        /// </summary>
        /// <param name="objCompany"></param>
        /// <returns></returns>
        public int UpdateSendGrid(EditSendGridDetailsModel objSendGridModel)
        {
            int result = 0;

            try
            {
                RepositoryService<EditSendGridDetailsModel> objComp = new RepositoryService<EditSendGridDetailsModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserId", objSendGridModel.UserId, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Password", objSendGridModel.Password, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@HostName", objSendGridModel.HostName, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PortNO", objSendGridModel.PortNO, System.Data.SqlDbType.VarChar));
                result = objComp.ExecuteSqlCommand("proc_AddUpdateSendGrid  @UserId,@Password, @HostName, @PortNO", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion
        public int DeleteCompanyById(string companyName, string panNumber)
        {
            int result = 0;
            try
            {
                RepositoryService<Company> objComp = new RepositoryService<Company>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@CompanyName", "" + companyName + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PanNumber", "" + panNumber + "", System.Data.SqlDbType.VarChar));
                result = objComp.ExecuteSqlCommand("proc_SoftdeleteCompnay @CompanyName,@PanNumber", parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #region Check if pan number exists
        /// <summary>
        /// Check if pan number exists
        /// </summary>
        /// <param name="pan"></param>
        /// <returns></returns>
        public int? CheckAnchorRate(Int32? CompanyID, decimal AnchorRate)
        {
            int? isExist = 0;
            try
            {
                RepositoryService<CompanyAnchorRateModel> objCompanyCredentialsModel = new RepositoryService<CompanyAnchorRateModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@AnchorRate", AnchorRate, System.Data.SqlDbType.Decimal));
                var result = objCompanyCredentialsModel.ExecWithStoreProcedure("proc_CheckAnchorRate @CompanyID,@AnchorRate", parameters.ToArray());
                IEnumerable<CompanyAnchorRateModel> PanCheck = result.ToList();

                isExist = PanCheck.FirstOrDefault().IsExistPan;
            }
            catch (Exception)
            {

                throw;
            }

            return isExist;
        }
        #endregion

        #region EDIT details of Company
        /// <summary>
        /// Edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Company ProfileEditPage(Int32? id)
        {
            Company objCompanyModels = new Company();
            try
            {
                RepositoryService<Company> objCompany = new RepositoryService<Company>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", id, System.Data.SqlDbType.Int));
                var data = objCompany.ExecWithStoreProcedure("proc_EditCompany  @CompanyID", parameters.ToArray());
                IEnumerable<Company> lstCompany = data.ToList();
                foreach (var v in lstCompany)
                {
                    objCompanyModels.Company_name = lstCompany.ElementAt(0).Company_name;
                    objCompanyModels.Contact_CIN = lstCompany.ElementAt(0).Contact_CIN;
                    objCompanyModels.Contact_Comments = lstCompany.ElementAt(0).Contact_Comments;
                    objCompanyModels.Pan_number = lstCompany.ElementAt(0).Pan_number;
                    objCompanyModels.Contact_Name = lstCompany.ElementAt(0).Contact_Name;
                    objCompanyModels.Contact_Designation = lstCompany.ElementAt(0).Contact_Designation;
                    objCompanyModels.Contact_email = lstCompany.ElementAt(0).Contact_email;
                    objCompanyModels.Contact_mobile = lstCompany.ElementAt(0).Contact_mobile;
                    objCompanyModels.InterestedAs = lstCompany.ElementAt(0).InterestedAs;
                    objCompanyModels.Address = lstCompany.ElementAt(0).Address;
                    objCompanyModels.Contact_Title = lstCompany.ElementAt(0).Contact_Title;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCompanyModels;
        }


        #endregion

        #region Insert Company Record
        /// <summary>
        /// Register anchor company
        /// </summary>
        /// <param name="objCompany"></param>
        /// <returns></returns>
        public int UpdateProfile(Company objCompany)
        {
            int result = 0;

            try
            {
                RepositoryService<Company> objComp = new RepositoryService<Company>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", objCompany.CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@Address", objCompany.Address, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Title", objCompany.Contact_Title, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Name", objCompany.Contact_Name, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Designation", objCompany.Contact_Designation, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Mobile", objCompany.Contact_mobile, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Contact_Comment", objCompany.Contact_Comments, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@UpdatedBy", objCompany.UpdatedBy, System.Data.SqlDbType.VarChar));//set UpdatedBy dynamically
                result = objComp.ExecuteSqlCommand("proc_UpdateProfileAdminMaster  @CompanyID,@Address,@Contact_Title,@Contact_Name,@Contact_Designation,@Contact_Mobile,@Contact_Comment,@UpdatedBy", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region EDIT User details of Company
        /// <summary>
        /// Edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel ProfileUserEditPage(Int32? id)
        {
            UserModel objUserModels = new UserModel();
            try
            {
                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserID", id, System.Data.SqlDbType.Int));
                var data = objUserModel.ExecWithStoreProcedure("proc_EditUser  @UserID", parameters.ToArray());
                IEnumerable<UserModel> lstUser = data.ToList();
                foreach (var v in lstUser)
                {
                    objUserModels.UserID = lstUser.ElementAt(0).UserID;
                    objUserModels.Name = lstUser.ElementAt(0).Name;
                    objUserModels.Mobile = lstUser.ElementAt(0).Mobile;
                    objUserModels.Email = lstUser.ElementAt(0).Email;
                    objUserModels.Designation = lstUser.ElementAt(0).Designation;
                    objUserModels.RolesAccess = lstUser.ElementAt(0).RolesAccess;
                    objUserModels.RoleAccessID = lstUser.ElementAt(0).RoleAccessID;
                    objUserModels.AccessViewID = lstUser.ElementAt(0).AccessViewID;
                    objUserModels.LookupValue = lstUser.ElementAt(0).LookupValue;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objUserModels;
        }


        #endregion

        #region Insert Company Record
        /// <summary>
        /// Register anchor company
        /// </summary>
        /// <param name="objCompany"></param>
        /// <returns></returns>
        public int UpdateUserProfile(UserModel objUserPage)
        {
            int result = 0;

            try
            {
                RepositoryService<UserModel> objComp = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", objUserPage.CompanyID, System.Data.SqlDbType.BigInt));
                parameters.Add(SQLHelper.SqlInputParam("@Name", objUserPage.Name, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Designation", objUserPage.Designation, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Mobile", objUserPage.Mobile, System.Data.SqlDbType.VarChar));
                result = objComp.ExecuteSqlCommand("proc_UpdateProfileUserMaster  @CompanyID,@Name,@Designation,@Mobile", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        public IEnumerable<CompanyBankView> GetCompanyBankListing(string sortBy, int pageSize, Int64 skip, Int64 CompanyID, string Search)
        {

            try
            {
                RepositoryService<CompanyBankView> obj = new RepositoryService<CompanyBankView>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@CompanyID", CompanyID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Search", Search, System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("proc_GetCompanyBankViewList @sortBy,@PageSize,@Skip,@CompanyID,@Search", parameters.ToArray());
                IEnumerable<CompanyBankView> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<BankCompanyView> GetBankCompanyListing(string sortBy, int pageSize, Int64 skip, Int64 BankID, string Search)
        {

            try
            {
                RepositoryService<BankCompanyView> obj = new RepositoryService<BankCompanyView>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@sortBy", sortBy, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@BankID", BankID, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Search", Search, System.Data.SqlDbType.VarChar));
                var data = obj.ExecWithStoreProcedure("proc_GetBankCompanyViewList @sortBy,@PageSize,@Skip,@BankID,@Search", parameters.ToArray());
                IEnumerable<BankCompanyView> lstAnchorCompListing = data.ToList();
                return lstAnchorCompListing;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public void updateCompanyRate_Days(int? companyID, string percentageRate, int paymentDays)
        //{
        //    throw new NotImplementedException();
        //}

        //public void updateCompanyRate_Days(string percentage, int paymentDays)
        //{
        //    throw new NotImplementedException();
        //}

        public int InsertHolidateReason(int? UserID, DateTime? holidate, string reason)
        {
            int result = 0;
            try
            {
                RepositoryService<InsertHoli_Reason> objComp = new RepositoryService<InsertHoli_Reason>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@UserID", "" + UserID + "", System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Holidate", "" + holidate + "", System.Data.SqlDbType.DateTime));
                parameters.Add(SQLHelper.SqlInputParam("@Reason", "" + reason + "", System.Data.SqlDbType.VarChar));
                result = objComp.ExecuteSqlCommand("InsertHolidate_Reason @UserID,@Holidate,@Reason", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int DeleteHolidateReason(int ID)
        {
            int result = 0;
            try
            {
                RepositoryService<InsertHoli_Reason> objComp = new RepositoryService<InsertHoli_Reason>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ID", "" + ID + "", System.Data.SqlDbType.Int));
                result = objComp.ExecuteSqlCommand("DeleteHolidate_Reason @ID", parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
             return result;
        }
        public int EditHolidateReason(int ID, string Holidate, string Reason)
        {
            int result = 0;
            try
            {
                RepositoryService<InsertHoli_Reason> objComp = new RepositoryService<InsertHoli_Reason>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ID", "" + ID + "", System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Holidate", "" + Holidate + "", System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Reason", "" + Reason + "", System.Data.SqlDbType.VarChar));
                result = objComp.ExecuteSqlCommand("UpdateHolidate_Reason @ID ,@Holidate ,@Reason", parameters.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }   
    }
}

