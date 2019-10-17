using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class LookupDetailsService : ILookupDetails
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
        public LookupDetailsService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion

        #region Method defination

        /// <summary>
        /// Get lookup detail by key
        /// </summary>
        /// <param name="LookupKey"></param>
        /// <returns></returns>
        public IEnumerable<LookupDetails> getLookupDetailByKey(string LookupKey)
        {
            RepositoryService<LookupDetails> objLookupDetails = new RepositoryService<LookupDetails>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@LookupFor", LookupKey, System.Data.SqlDbType.VarChar));

            var data = objLookupDetails.ExecWithStoreProcedure("Proc_getLookupDetailsByGroup @LookupFor", parameters.ToArray());
            IEnumerable<LookupDetails> lookupDetails = data.ToList();

            return lookupDetails;
        }

        #endregion

        #region Get User Mail  Template

        /// <summary>
        /// Get user email template
        /// </summary>
        /// <param name="LookupKey"></param>
        /// <returns></returns>
        public IEnumerable<GetUserMailTemplate> getUserMailTemplate(int? AccessViewId = 0)
        {
            try
            {

                RepositoryService<GetUserMailTemplate> objGetUserMailTemplate = new RepositoryService<GetUserMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                //parameters.Add(SQLHelper.SqlInputParam("@LookupFor", LookupKey, System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@AccessViewId", AccessViewId, System.Data.SqlDbType.BigInt));
                var data = objGetUserMailTemplate.ExecWithStoreProcedure("Proc_getUserEmailTemplate @AccessViewId", parameters.ToArray());
                IEnumerable<GetUserMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region Get Forgot Password Mail  Template

        /// <summary>
        /// Get forgot password template
        /// </summary>
        /// <param name="LookupKey"></param>
        /// <returns></returns>
        public IEnumerable<GetForgetPasswordMailTemplate> getForgetPasswordMailTemplate()
        {
            try
            {

                RepositoryService<GetForgetPasswordMailTemplate> objGetForgetPasswordMailTemplate = new RepositoryService<GetForgetPasswordMailTemplate>(_vContext);
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

                var data = objGetForgetPasswordMailTemplate.ExecWithStoreProcedure("Proc_getForgetPasswordEmailTemplate");
                IEnumerable<GetForgetPasswordMailTemplate> lookupDetails = data.ToList();
                return lookupDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        public IEnumerable<GetEmailId> GetEmailId(int ID,string RoleName)
        {
            RepositoryService<GetEmailId> obj = new RepositoryService<GetEmailId>(_vContext);
            IEnumerable<GetEmailId> drawfundsfrombank;
            try
            {
                ICollection<SqlParameter> parameters = new List<SqlParameter>();

             
                parameters.Add(SQLHelper.SqlInputParam("@UserID", ID.ToString(), System.Data.SqlDbType.VarChar));
                parameters.Add(SQLHelper.SqlInputParam("@Role", "" + RoleName + "", System.Data.SqlDbType.VarChar));


                drawfundsfrombank = (obj.ExecWithStoreProcedure("proc_GetUserEmail @UserID,@Role", parameters.ToArray())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return drawfundsfrombank;

        }
    }
}
