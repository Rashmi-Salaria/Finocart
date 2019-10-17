using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Finocart.SAPService.Common
{
    public class SQLDBHelper
    {

        #region ExecuteDataSet
        /// <summary>
        /// To get dataset from db 
        /// </summary>
        /// <param name="strProcedureName"></param>
        /// <param name="sqlParameterList"></param>
        /// <param name="isWithTransaction"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string strProcedureName, SqlParameter[] sqlParameterList, bool isWithTransaction)
        {
            var ds = new DataSet();
            SqlTransaction trans = null;
            try
            {
                if (objSqlConnection.State == ConnectionState.Closed)
                    objSqlConnection.Open();

                if (isWithTransaction)
                {
                    trans = objSqlConnection.BeginTransaction();
                    ds = SqlHelper.ExecuteDataset(trans, CommandType.StoredProcedure, strProcedureName, sqlParameterList);
                }
                else
                    ds = SqlHelper.ExecuteDataset(objSqlConnection, CommandType.StoredProcedure, strProcedureName, sqlParameterList);
                if (isWithTransaction)
                    trans.Commit();
            }
            catch (Exception ex)
            {
                if (isWithTransaction)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                objSqlConnection.Close();
            }
            return ds;
        }
        #endregion ExecuteDataSet

        #region ExecuteNonQuery
        /// <summary>
        /// Execute non query
        /// </summary>
        /// <param name="strProcedureName"></param>
        /// <param name="sqlParameterList"></param>
        /// <param name="isWithTransaction"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string strProcedureName, SqlParameter[] sqlParameterList, bool isWithTransaction)
        {
            var NoofRowEffected = 0;
            SqlTransaction trans = null;
            try
            {
                if (objSqlConnection.State == ConnectionState.Closed)
                    objSqlConnection.Open();

                if (isWithTransaction)
                {
                    trans = objSqlConnection.BeginTransaction();
                    NoofRowEffected = SqlHelper.ExecuteNonQuery(trans, strProcedureName, sqlParameterList);
                }
                else
                    NoofRowEffected = SqlHelper.ExecuteNonQuery(objSqlConnection, strProcedureName, sqlParameterList);
                if (isWithTransaction)
                    trans.Commit();
            }
            catch (Exception ex)
            {
                if (isWithTransaction)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                objSqlConnection.Close();
            }
            return NoofRowEffected;
        }
        #endregion ExecuteNonQuery

        #region ExecuteScalar
        /// <summary>
        /// Execute scalar query
        /// </summary>
        /// <param name="strProcedureName"></param>
        /// <param name="sqlParameterList"></param>
        /// <param name="isWithTransaction"></param>
        /// <returns></returns>
        public object ExecuteScalar(string strProcedureName, SqlParameter[] sqlParameterList, bool isWithTransaction)
        {
            object obj = null;
            SqlTransaction trans = null;
            try
            {
                if (objSqlConnection.State == ConnectionState.Closed)
                    objSqlConnection.Open();

                if (isWithTransaction)
                {
                    trans = objSqlConnection.BeginTransaction();
                    obj = SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure, strProcedureName, sqlParameterList);
                }
                else
                    obj = SqlHelper.ExecuteScalar(objSqlConnection, CommandType.StoredProcedure, strProcedureName, sqlParameterList);
                if (isWithTransaction)
                    trans.Commit();
            }
            catch (Exception ex)
            {
                if (isWithTransaction)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                objSqlConnection.Close();
            }
            return obj;
        }
        #endregion ExecuteScalar

        #region ExecuteDataTable
        /// <summary>
        /// Execute data table
        /// </summary>
        /// <param name="strProcedureName"></param>
        /// <param name="sqlParameterList"></param>
        /// <param name="isWithTransaction"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string strProcedureName, SqlParameter[] sqlParameterList, bool isWithTransaction)
        {
            var dt = new DataTable();
            SqlDataReader dr = null;
            SqlTransaction trans = null;
            try
            {
                if (objSqlConnection.State == ConnectionState.Closed)
                    objSqlConnection.Open();

                if (isWithTransaction)
                {
                    trans = objSqlConnection.BeginTransaction();
                    dr = SqlHelper.ExecuteReader(trans, CommandType.StoredProcedure, strProcedureName, sqlParameterList);
                    dt.Load(dr);
                }
                else
                {
                    dr = SqlHelper.ExecuteReader(objSqlConnection, CommandType.StoredProcedure, strProcedureName, sqlParameterList);
                    dt.Load(dr);
                }
                if (isWithTransaction)
                    trans.Commit();
            }
            catch (Exception ex)
            {
                if (isWithTransaction)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                objSqlConnection.Close();
            }
            return dt;
        }
        #endregion ExecuteDataTable


        #region[Private Variable]
        private string _ConnectionString;
        private SqlCommand objSqlCommand;
        private SqlConnection objSqlConnection;

        #endregion

        #region[Public Properties]
        private String ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }

        }
        #endregion

        public SQLDBHelper()
           //   : this("Data source = BVILAP-048; database = Prestige; User ID =sa; password =abc@123;")
           : this(ConfigurationManager.AppSettings["DBQueryString"])
        //: this(@"Data source = DMNTGSTNPS1D\MSSQL09; database = NPS-DEMO; User ID =sa; password =sql@12345;")
        // : this("Data source = 10.201.96.55; database = NPS-DEMO ; User ID =npsadmin; password =nps@123;")
        {
            SqlConnection.ClearAllPools();
            objSqlConnection = new SqlConnection();
            objSqlConnection.ConnectionString = ConnectionString;
            objSqlCommand = new SqlCommand();
            objSqlCommand.Connection = objSqlConnection;
            objSqlCommand.CommandTimeout = 5000000;
            objSqlConnection.Open();
        }

        SQLDBHelper(string strConnectionString)
        {
            ConnectionString = strConnectionString;
        }

        /// <summary>
        /// Adding parameter
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public void AddParameter(string Name, object Value)
        {
            SqlParameter objSqlParameter = objSqlCommand.CreateParameter();
            objSqlParameter.ParameterName = Name;
            objSqlParameter.Value = Value;

            objSqlCommand.Parameters.Add(objSqlParameter);
        }

        /// <summary>
        /// Clear parameter
        /// </summary>
        /// <param name="objSQLDBHelper"></param>
        public void ClearParameter(SQLDBHelper objSQLDBHelper)
        {
            objSQLDBHelper.objSqlCommand.Parameters.Clear();
        }

        /// <summary>
        /// Add parameter
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <param name="SqlParameterDirection"></param>
        public void AddParameter(string Name, object Value, ParameterDirection SqlParameterDirection)
        {

            SqlParameter objSqlParameter = objSqlCommand.CreateParameter();
            objSqlParameter.ParameterName = Name;
            objSqlParameter.Value = Value;
            objSqlParameter.Direction = SqlParameterDirection;
            objSqlCommand.Parameters.Add(objSqlParameter);
        }

        /// <summary>
        /// Add parameter
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <param name="dbType"></param>
        public void AddParameter(string Name, object Value, SqlDbType dbType)
        {
            SqlParameter objSqlParameter = objSqlCommand.CreateParameter();
            objSqlParameter.ParameterName = Name;
            objSqlParameter.Value = Value;
            objSqlParameter.SqlDbType = dbType;
            objSqlCommand.Parameters.Add(objSqlParameter);
        }

        /// <summary>
        /// Add parameter
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <param name="dbType"></param>
        /// <param name="SqlParameterDirection"></param>
        public void AddParameter(string Name, object Value, SqlDbType dbType, ParameterDirection SqlParameterDirection)
        {

            SqlParameter objSqlParameter = objSqlCommand.CreateParameter();
            objSqlParameter.ParameterName = Name;
            objSqlParameter.Value = Value;
            objSqlParameter.SqlDbType = dbType;
            objSqlParameter.Direction = SqlParameterDirection;
            objSqlCommand.Parameters.Add(objSqlParameter);
        }

        /// <summary>
        /// Add parameter
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <param name="dbType"></param>
        /// <param name="SqlParameterDirection"></param>
        /// <param name="IsNull"></param>
        public void AddParameter(string Name, object Value, SqlDbType dbType, ParameterDirection SqlParameterDirection, bool IsNull)
        {

            SqlParameter objSqlParameter = objSqlCommand.CreateParameter();
            objSqlParameter.ParameterName = Name;
            objSqlParameter.Value = Value;
            objSqlParameter.SqlDbType = dbType;
            objSqlParameter.IsNullable = IsNull;
            objSqlParameter.Direction = SqlParameterDirection;
            objSqlCommand.Parameters.Add(objSqlParameter);
        }

        /// <summary>
        /// Add parameter
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="dbType"></param>
        /// <param name="SqlParameterDirection"></param>
        /// <param name="IsNull"></param>
        public void AddParameter(string Name, SqlDbType dbType, ParameterDirection SqlParameterDirection, bool IsNull)
        {

            SqlParameter objSqlParameter = objSqlCommand.CreateParameter();
            objSqlParameter.ParameterName = Name;
            objSqlParameter.SqlDbType = dbType;
            objSqlParameter.IsNullable = IsNull;
            objSqlParameter.Direction = SqlParameterDirection;
            objSqlCommand.Parameters.Add(objSqlParameter);
        }
        
        /// <summary>
        /// Execute non query
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Commandtyp"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string Query, CommandType Commandtyp)
        {
            int i = 0;
            try
            {
                objSqlCommand.CommandText = Query;
                objSqlCommand.CommandType = Commandtyp;
                if (objSqlConnection.State == ConnectionState.Closed) objSqlConnection.Open();
                i = objSqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (objSqlConnection.State == ConnectionState.Open) objSqlConnection.Close();
                throw ex;
            }
            finally
            {
                objSqlCommand.Parameters.Clear();
                objSqlConnection.Close();
            }

            return i;

        }

        /// <summary>
        /// Execute non query
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Commandtyp"></param>
        /// <param name="parameterdirection"></param>
        public void ExecuteNonQuery(string Query, CommandType Commandtyp, ParameterDirection parameterdirection)
        {

            try
            {

                objSqlCommand.CommandText = Query;
                objSqlCommand.CommandType = Commandtyp;
                if (objSqlConnection.State == ConnectionState.Closed) objSqlConnection.Open();
                objSqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                if (objSqlConnection.State == ConnectionState.Open) objSqlConnection.Close();
                throw ex;
            }
            finally
            {
                objSqlCommand.Parameters.Clear();
                objSqlConnection.Close();
            }



        }

        /// <summary>
        /// Execute non query
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Commandtyp"></param>
        /// <param name="parameterdirection"></param>
        /// <param name="OutPutParameter"></param>
        /// <returns></returns>
        public long ExecuteNonQuery(string Query, CommandType Commandtyp, ParameterDirection parameterdirection, string OutPutParameter)
        {
            long ReturnValue;
            try
            {

                objSqlCommand.CommandText = Query;
                objSqlCommand.CommandType = Commandtyp;
                if (objSqlConnection.State == ConnectionState.Closed) objSqlConnection.Open();
                int i = objSqlCommand.ExecuteNonQuery();
                ReturnValue = (!string.IsNullOrEmpty(OutPutParameter)) ? Convert.ToInt64(objSqlCommand.Parameters[OutPutParameter].Value) : 0;
            }
            catch (Exception ex)
            {
                if (objSqlConnection.State == ConnectionState.Open) objSqlConnection.Close();
                throw ex;
            }
            finally
            {
                objSqlCommand.Parameters.Clear();
                objSqlConnection.Close();
            }


            return ReturnValue;
        }
        
        /// <summary>
        /// Execute data set
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Commandtyp"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string Query, CommandType Commandtyp)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter();
                objSqlCommand.CommandText = Query;
                objSqlCommand.CommandType = Commandtyp;
                adpt.SelectCommand = objSqlCommand;
                if (objSqlConnection.State == ConnectionState.Closed) objSqlConnection.Open();
                adpt.Fill(ds);

            }
            catch (Exception ex)
            {
                if (objSqlConnection.State == ConnectionState.Open) objSqlConnection.Close();
                throw ex;
            }
            finally
            {
                objSqlCommand.Parameters.Clear();
                objSqlConnection.Close();
            }

            return ds;

        }

        /// <summary>
        /// Execute data reader
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Commandtyp"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteDataReader(string Query, CommandType Commandtyp)
        {
            SqlDataReader rdr = null;
            SqlCommand SqlCommand = new SqlCommand();
            try
            {

                objSqlCommand.CommandText = Query;
                objSqlCommand.CommandType = Commandtyp;
                objSqlCommand.CommandTimeout = 900000;
                rdr = objSqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                objSqlConnection.Close();
                throw ex;
            }
            finally
            {
                //objSqlCommand.Parameters.Clear();
                //objSqlConnection.Close();
            }
            return rdr;
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Commandtyp"></param>
        /// <returns></returns>
        public object ExecuteScalar(string Query, CommandType Commandtyp)
        {
            object objValue;
            try
            {
                objSqlCommand.CommandText = Query;
                objSqlCommand.CommandType = Commandtyp;
                if (objSqlConnection.State == ConnectionState.Closed) objSqlConnection.Open();
                objValue = objSqlCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                if (objSqlConnection.State == ConnectionState.Open) objSqlConnection.Close();
                throw ex;
            }
            finally
            {
                objSqlCommand.Parameters.Clear();
                objSqlConnection.Close();
            }

            return objValue;

        }

        /// <summary>
        /// Execute data table
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Commandtyp"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string Query, CommandType Commandtyp)
        {
            DataTable da = new DataTable();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter();
                objSqlCommand.CommandText = Query;
                objSqlCommand.CommandType = Commandtyp;
                adpt.SelectCommand = objSqlCommand;
                if (objSqlConnection.State == ConnectionState.Closed) objSqlConnection.Open();
                adpt.Fill(da);

            }
            catch (Exception ex)
            {
                if (objSqlConnection.State == ConnectionState.Open) objSqlConnection.Close();
                throw ex;
            }
            finally
            {
                objSqlCommand.Parameters.Clear();
                objSqlConnection.Close();
            }

            return da;

        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            objSqlConnection.Dispose();
            objSqlConnection = null;
            objSqlCommand.Dispose();
        }
    }
    }

