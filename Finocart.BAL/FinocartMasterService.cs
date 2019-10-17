using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Finocart.Services
{
    public class FinocartMasterService : IFinoCartMaster
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
        public FinocartMasterService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion
        /// <summary>
        /// Validate login
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public FinocartMaster ValidateLogin(string EmailId, string Password)
        {
            
            RepositoryService<FinocartMaster> objFinocartMaster = new RepositoryService<FinocartMaster>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@EmailId", EmailId, System.Data.SqlDbType.VarChar));
            parameters.Add(SQLHelper.SqlInputParam("@Password", Password, System.Data.SqlDbType.VarChar));
            var data = objFinocartMaster.ExecWithStoreProcedure("proc_CheckSuperAdminLogin @EmailId, @Password", parameters.ToArray());
            FinocartMaster SuperAdmin = data.SingleOrDefault();
            return SuperAdmin;
        }
    }
}
