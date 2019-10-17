using Finocart.Model;
using System;
using System.Collections.Generic;

namespace Finocart.Interface
{
    /// <summary>
    /// Employee Interface
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Get count
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Get Employees 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> getEmployees();

        /// <summary>
        /// Get Employees By Id
        /// </summary>
        /// <param name="iEmployeeId"></param>
        /// <returns></returns>
        Employee getEmployeesById(int iEmployeeId);

        /// <summary>
        /// Get Employees Info
        /// </summary>
        /// <param name="iEmployeeId"></param>
        /// <returns></returns>
        Int64 getEMployeesInfo(int iEmployeeId);

        /// <summary>
        /// Get Employee Data information
        /// </summary>
        /// <returns></returns>
        Int64 getEmployeesDataInfo();

    }
}

