using System.Data;

namespace Finocart.SAPService.Common.SQLTransaction
{
    public class SQLServerTransaction
    {
        public SQLDBHelper _db;

        /// <summary>
        /// Inserting vendor details in sql table
        /// </summary>
        /// <param name="objvendorDetModel"></param>
        /// <returns></returns>
        public int InsertVendorDetailsInSQLTable(VendorDetModel objvendorDetModel)
        {
            int result = 0;
            _db.AddParameter("@VendorName", objvendorDetModel.VENDOR, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.NAME, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.NAME_2, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.NAME_3, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.NAME_4, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.CITY, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.DISTRICT, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.PO_BOX, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.POBX_PCD, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.POSTL_CODE, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.REGION, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.STREET, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.COUNTRY, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.COUNTRYISO, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.LANGU, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.LANGU_ISO, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.TELEPHONE, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.FORMOFADDR, SqlDbType.VarChar, ParameterDirection.Input);
            _db.AddParameter("@VendorName", objvendorDetModel.TELEPHONE2, SqlDbType.VarChar, ParameterDirection.Input);

            result = _db.ExecuteNonQuery("proc_AddVendorByERP", CommandType.StoredProcedure);
            return result;
           
        }
    }
}
