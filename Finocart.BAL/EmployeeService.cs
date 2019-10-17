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
    public class EmployeeService : IEmployee
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
        public EmployeeService(VMSContext context)
        {
            _vContext = context;
        }

        #endregion

        #region Method defination

        /// <summary>
        /// Employee Count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _vContext.Employee.Count();
        }

        /// <summary>
        /// Getting list of employees
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> getEmployees()
        {
            RepositoryService<Employee> objEmployee = new RepositoryService<Employee>(_vContext);
            
            return objEmployee.SelectAll();
        }

        /// <summary>
        /// Get employees by Id - Procedure with single parameter 
        /// </summary>
        /// <param name="iEmployeeId"></param>
        /// <returns></returns>
        public Employee getEmployeesById(int iEmployeeId)
        {
            RepositoryService<Employee> objEmployee = new RepositoryService<Employee>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@EmployeeID", iEmployeeId, System.Data.SqlDbType.Int));

            var data = objEmployee.ExecWithStoreProcedure("proc_getEmployeeData @EmployeeID", parameters.ToArray());
            Employee employee = data.SingleOrDefault();

            return employee;
        }

        /// <summary>
        /// Get employees infor - Procedure with single parameter which return custommodel
        /// </summary>
        /// <param name="iEmployeeId"></param>
        /// <returns></returns>
        public Int64 getEMployeesInfo(int iEmployeeId)
        {
            Int64 i = 0;
            RepositoryService<EmployeeModel> objEmployeeModel = new RepositoryService<EmployeeModel>(_vContext);
            ICollection<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(SQLHelper.SqlInputParam("@EmployeeID", iEmployeeId, System.Data.SqlDbType.Int));

            var data = objEmployeeModel.ExecWithStoreProcedure("proc_getEmployeeData @EmployeeID", parameters.ToArray());
            EmployeeModel employee = data.SingleOrDefault();
            
            return i;
        }

        /// <summary>
        /// Get Employeedatainfor - Procedure without any parameters
        /// </summary>
        /// <returns></returns>
        public Int64 getEmployeesDataInfo()
        {
            Int64 i = 0;
            RepositoryService<EmployeeModel> objEmployeeModel = new RepositoryService<EmployeeModel>(_vContext);

            var data = objEmployeeModel.ExecWithStoreProcedure("proc_getEmployeeData_Demo");
            List<EmployeeModel> employee = data.ToList();

          
            return i;
        }

        #endregion
    }
}
