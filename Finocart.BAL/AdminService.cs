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
    public class AdminService : IAdmin
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
        public AdminService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion

        /// <summary>
        /// Finding name
        /// </summary>
        /// <param name="InputCredential"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public AdminLoginModel FindName(string InputCredential, string Password)
        {
            RepositoryService<AdminLoginModel> objAdminLoginModel = new RepositoryService<AdminLoginModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            

            parameters.Add(SQLHelper.SqlInputParam("@InputCredential", InputCredential, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@Password", Password, System.Data.SqlDbType.VarChar));
            var data = objAdminLoginModel.ExecWithStoreProcedure("proc_CheckAdminLogin @InputCredential, @Password", parameters.ToArray());
            AdminLoginModel user = data.SingleOrDefault();
            return user;
        }

        #region Invoice Approval Order List
        /// <summary>
        /// Getting invoice approved listing
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceApprovalOrderModel> GetInvoiceApprovedListing(string sortBy, int pageSize, Int64 skip)
        {
            RepositoryService<InvoiceApprovalOrderModel> objInvoiceApprovalOrderModel = new RepositoryService<InvoiceApprovalOrderModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@SortBy", sortBy, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@PageSize", pageSize, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@Skip", skip, System.Data.SqlDbType.VarChar));
            var data = objInvoiceApprovalOrderModel.ExecWithStoreProcedure("proc_GetInvoiceApprovalDet  @Skip, @PageSize, @sortBy", parameters.ToArray());
            IEnumerable<InvoiceApprovalOrderModel> lstInvoiceApproval = data.ToList();
            return lstInvoiceApproval;
        }
        #endregion

        #region User List For Dropdown
        /// <summary>
        /// Get user list for dropdown
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserList()
        {
            RepositoryService<User> objUser = new RepositoryService<User>(_vContext);
            IEnumerable<User> lstUsers = objUser.SelectAll();
            
            return lstUsers;
        }
        #endregion

        #region Add and Update Invoice Approval
        /// <summary>
        /// save and update invoice approval record
        /// </summary>
        /// <param name="objUserPage"></param>
        /// <returns></returns>
        public int InsertUpdateApprovalRecord(InvoiceApprovalOrderModel objUserPage)
        {
            int result = 0;

            try
            {
                RepositoryService<InvoiceApprovalOrderModel> objInvoiceApprovalOrderModel = new RepositoryService<InvoiceApprovalOrderModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ApprovalID", objUserPage.ApprovalID, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@FromAmount", objUserPage.FromAmount, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@ToAmount", objUserPage.ToAmount, System.Data.SqlDbType.Decimal));
                parameters.Add(SQLHelper.SqlInputParam("@ApprovedBy", objUserPage.ApprovedBy, System.Data.SqlDbType.Int));
                parameters.Add(SQLHelper.SqlInputParam("@Users", objUserPage.Users, System.Data.SqlDbType.VarChar));
                result = objInvoiceApprovalOrderModel.ExecuteSqlCommand("proc_AddUpdateInvoiceApproved  @ApprovalID,@FromAmount, @ToAmount, @ApprovedBy, @Users", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Edit Invoice Approval
        /// <summary>
        /// get invoice approval record on edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InvoiceApprovalOrderModel EditPage(int id)
        {
            InvoiceApprovalOrderModel objInvoiceApprovalModels = new InvoiceApprovalOrderModel();
            try
            {
                RepositoryService<InvoiceApprovalOrderModel> objInvoiceApprovalOrderModel = new RepositoryService<InvoiceApprovalOrderModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(SQLHelper.SqlInputParam("@ApprovalID", id, System.Data.SqlDbType.Int));
                var data = objInvoiceApprovalOrderModel.ExecWithStoreProcedure("proc_EditInvoiceApproved  @ApprovalID", parameters.ToArray());
                IEnumerable<InvoiceApprovalOrderModel> lstInvoiceApproval = data.ToList();
                foreach (var v in lstInvoiceApproval)
                {
                    objInvoiceApprovalModels.ApprovalID = lstInvoiceApproval.ElementAt(0).ApprovalID;
                    objInvoiceApprovalModels.FromAmount = lstInvoiceApproval.ElementAt(0).FromAmount;
                    objInvoiceApprovalModels.ToAmount = lstInvoiceApproval.ElementAt(0).ToAmount;
                    objInvoiceApprovalModels.ApprovedBy = lstInvoiceApproval.ElementAt(0).ApprovedBy;
                    objInvoiceApprovalModels.ApprovedByName = lstInvoiceApproval.ElementAt(0).ApprovedByName;
                    objInvoiceApprovalModels.Users = lstInvoiceApproval.ElementAt(0).Users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objInvoiceApprovalModels;
        }
        #endregion

        #region Delete Invoice Approval
        /// <summary>
        /// delete invoice approval record by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteInvoiceApprovalPage(int id)
        {
            int result = 0;

            try
            {

                RepositoryService<UserModel> objUserModel = new RepositoryService<UserModel>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(SQLHelper.SqlInputParam("@ApprovalID", id, System.Data.SqlDbType.Int));
                result = objUserModel.ExecuteSqlCommand("proc_deleteInvoiceApproval  @ApprovalID", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Check Amount Duplication
        /// <summary>
        /// check from amount and to amount duplication
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InvoiceApprovalOrder> CheckAmountDuplicate(decimal fromAmount, decimal toAmount)
        {
            RepositoryService<InvoiceApprovalOrder> objInvoiceApprovalOrder = new RepositoryService<InvoiceApprovalOrder>(_vContext);
            return objInvoiceApprovalOrder.SelectAll().Where(x=>x.FromAmount == fromAmount && x.ToAmount == toAmount).ToList();
        }
        #endregion

        /// <summary>
        /// Finding name
        /// </summary>
        /// <param name="panNumber"></param>
        /// <returns></returns>
        public string LockedAdminUser(string panNumber)
        {
            RepositoryService<UserLockedResponse> strResponse = new RepositoryService<UserLockedResponse>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SQLHelper.SqlInputParam("@PanNumber", panNumber, System.Data.SqlDbType.VarChar));
            var data = strResponse.ExecWithStoreProcedure("proc_AdminLoginLocked @PanNumber", parameters.ToArray());
            return data.FirstOrDefault().Message;
        }

        /// <summary>
        /// Finding name
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string LockedUser(string email)
        {
            RepositoryService<UserLockedResponse> strResponse = new RepositoryService<UserLockedResponse>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SQLHelper.SqlInputParam("@EmailID", email, System.Data.SqlDbType.VarChar));
            var data = strResponse.ExecWithStoreProcedure("proc_UserLoginLocked @EmailID", parameters.ToArray());
            return data.FirstOrDefault().Message;
        }
    }
}
