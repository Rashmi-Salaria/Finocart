using System;
using SAP.Middleware.Connector;
using System.Data;


namespace Finocart.SAPService
{
    public class SAPCommonService:IDestinationConfiguration
    {
        /// <summary>
        /// Getting paramete name
        /// </summary>
        /// <param name="destinationName"></param>
        /// <returns></returns>
        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters connectto = new RfcConfigParameters();
            if ("accelyides".Equals(destinationName))
            {
                connectto.Add(RfcConfigParameters.Client, "800");
                connectto.Add(RfcConfigParameters.SystemNumber, "00");
                connectto.Add(RfcConfigParameters.AppServerHost, "10.50.250.19");
                connectto.Add(RfcConfigParameters.Name, "accelyides");
                connectto.Add(RfcConfigParameters.User, "abap");
                connectto.Add(RfcConfigParameters.Password, "abap123");
                connectto.Add(RfcConfigParameters.Language, "EN");
            }
            return connectto;
        }

        /// <summary>
        /// Change event supported
        /// </summary>
        /// <returns></returns>
        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;
    }

    public static class Multihandler
    {
        /// <summary>
        /// converting sap table to sql table
        /// </summary>
        /// <param name="sapTable"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IRfcTable sapTable, string name)
        {
            DataTable adoTable = new DataTable(name);
            //... Create ADO.Net table.
            for (int liElement = 0; liElement < sapTable.ElementCount; liElement++)
            {
                RfcElementMetadata metadata = sapTable.GetElementMetadata(liElement);
                adoTable.Columns.Add(metadata.Name, GetDataType(metadata.DataType));
            }

            //Transfer rows from SAP Table ADO.Net table.
            foreach (IRfcStructure row in sapTable)
            {
                DataRow ldr = adoTable.NewRow();
                for (int liElement = 0; liElement < sapTable.ElementCount; liElement++)
                {
                    RfcElementMetadata metadata = sapTable.GetElementMetadata(liElement);

                    switch (metadata.DataType)
                    {
                        case RfcDataType.DATE:

                            ldr[metadata.Name] = row.GetString(metadata.Name).Substring(0, 4) + row.GetString(metadata.Name).Substring(5, 2) + row.GetString(metadata.Name).Substring(8, 2);
                            break;
                        case RfcDataType.BCD:
                            ldr[metadata.Name] = row.GetDecimal(metadata.Name);
                            break;
                        case RfcDataType.CHAR:
                            ldr[metadata.Name] = row.GetString(metadata.Name);
                            break;
                        case RfcDataType.STRING:
                            ldr[metadata.Name] = row.GetString(metadata.Name);
                            break;
                        case RfcDataType.INT2:
                            ldr[metadata.Name] = row.GetInt(metadata.Name);
                            break;
                        case RfcDataType.INT4:
                            ldr[metadata.Name] = row.GetInt(metadata.Name);
                            break;
                        case RfcDataType.FLOAT:
                            ldr[metadata.Name] = row.GetDouble(metadata.Name);
                            break;
                        default:
                            ldr[metadata.Name] = row.GetString(metadata.Name);
                            break;
                    }
                }
                adoTable.Rows.Add(ldr);
            }
            return adoTable;
        }

        /// <summary>
        /// Get data by type
        /// </summary>
        /// <param name="rfcDataType"></param>
        /// <returns></returns>
        private static Type GetDataType(RfcDataType rfcDataType)
        {
            switch (rfcDataType)
            {
                case RfcDataType.DATE:
                    return typeof(string);
                case RfcDataType.CHAR:
                    return typeof(string);
                case RfcDataType.STRING:
                    return typeof(string);
                case RfcDataType.BCD:
                    return typeof(decimal);
                case RfcDataType.INT2:
                    return typeof(int);
                case RfcDataType.INT4:
                    return typeof(int);
                case RfcDataType.FLOAT:
                    return typeof(double);
                default:
                    return typeof(string);
            }
        }
    }

    public class CustomClass
    {
        public string Value { get; set; }
    }

}

