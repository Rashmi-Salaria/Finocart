using Finocart.CustomModel;
using Finocart.Model;
using System;
using System.Collections.Generic;
namespace Finocart.Interface
{
    public interface IAdmin
    {
        AdminLoginModel FindName(string InputCredential, string Password);

        /// <summary>
        /// get user list for dropdown
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUserList();

        /// <summary>
        /// get invoice approval list
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        IEnumerable<InvoiceApprovalOrderModel> GetInvoiceApprovedListing(string sortBy, int pageSize, Int64 skip);

        /// <summary>
        /// save and update invoice approval record
        /// </summary>
        /// <param name="objUserModel"></param>
        /// <returns></returns>
        int InsertUpdateApprovalRecord(InvoiceApprovalOrderModel objUserModel);

        /// <summary>
        /// get invoice approval record on edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        InvoiceApprovalOrderModel EditPage(int id);

        /// <summary>
        /// delete invoice approval record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteInvoiceApprovalPage(int id);

        /// <summary>
        /// check from amount and to amount duplication
        /// </summary>
        /// <returns></returns>
        IEnumerable<InvoiceApprovalOrder> CheckAmountDuplicate(decimal fromAmount, decimal toAmount);

        /// <summary>
        /// Lock Admin Accounts after multiple attempts
        /// </summary>
        /// <returns></returns>
        string LockedAdminUser(string panNumber);

        /// <summary>
        /// Lock User Accounts after multiple attempts
        /// </summary>
        /// <returns></returns>
        string LockedUser(string email);
    }
}
