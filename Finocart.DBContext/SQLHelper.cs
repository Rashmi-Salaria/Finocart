using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Finocart.DBContext
{
    public static class SQLHelper
    {
        /// <summary>
        /// SQLs the input parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="sqlType">Type of the SQL.</param>
        /// <returns>Returns Input Parameter</returns>
        public static SqlParameter SqlInputParam(string name, object value, SqlDbType sqlType)
        {
            SqlParameter input = new SqlParameter(name, ToDBNull(value));
            input.SqlDbType = sqlType;
            input.Direction = ParameterDirection.Input;

            return input;
        }

        /// <summary>
        /// SQLs the output parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sqlType">Type of the SQL.</param>
        /// <param name="size">The size.</param>
        /// <returns>
        /// Returns Input Parameter
        /// </returns>
        public static SqlParameter SqlOutputParam(string name, SqlDbType sqlType, int? size = null)
        {
            SqlParameter output = new SqlParameter(name, sqlType);
            output.Direction = ParameterDirection.Output;

            if (size.HasValue)
            {
                output.Size = size.Value;
            }

            return output;
        }
        
        /// <summary>
        /// to check passed parameter is null or not. is null then return db null value
        /// </summary>
        /// <param name="value">Object values</param>
        /// <returns>Returns Null values</returns>
        public static object ToDBNull(object value)
        {
            if (value != null)
            {
                return value;
            }
            return DBNull.Value;
        }

    }
}
