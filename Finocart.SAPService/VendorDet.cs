using SAP.Middleware.Connector;


namespace Finocart.SAPService
{
    public class VendorDet
    {
        public VendorDetModel GetVendorsList()
        {

            //SAPCommonService objSAPCommonService = new SAPCommonService();
            SAPCommonService objSAPCommon = new SAPCommonService();
            //RfcDestination rfcDest = null;

            RfcDestination rfcDest = RfcDestinationManager.TryGetDestination("accelyides");

            if (rfcDest == null)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(objSAPCommon);
                rfcDest = RfcDestinationManager.GetDestination("accelyides");
            }

            //RfcDestinationManager.UnregisterDestinationConfiguration(objSAPCommon);
            //RfcDestinationManager.RegisterDestinationConfiguration(objSAPCommon);
            //RfcDestination rfcDest = null;
            //rfcDest = RfcDestinationManager.GetDestination("accelyides");
            VendorDetModel vendorDetModel = GetVendorDetails(rfcDest);
            return vendorDetModel;
        }
        public VendorDetModel GetVendorDetails(RfcDestination rfcDest)
        {
            VendorDetModel vendorDetModel = new VendorDetModel();
            RfcDestination SAPRfcDestination = RfcDestinationManager.GetDestination("accelyides");

            RfcRepository rfcrep = SAPRfcDestination.Repository;
            IRfcFunction BapiGetCompanyDetail = null;


            BapiGetCompanyDetail = rfcrep.CreateFunction("BAPI_VENDOR_GETDETAIL");
            BapiGetCompanyDetail.SetValue("VENDORNO", "VBX");
            BapiGetCompanyDetail.SetValue("COMPANYCODE", "0001");
            BapiGetCompanyDetail.Invoke(rfcDest);
          
            IRfcStructure IRS_OS_GeneralDet= BapiGetCompanyDetail.GetStructure("GENERALDETAIL");
            vendorDetModel.VENDOR = IRS_OS_GeneralDet.GetValue("VENDOR").ToString();
            vendorDetModel.NAME = IRS_OS_GeneralDet.GetValue("NAME").ToString();
            vendorDetModel.NAME_2 = IRS_OS_GeneralDet.GetValue("NAME_2").ToString();
            vendorDetModel.NAME_3 = IRS_OS_GeneralDet.GetValue("NAME_3").ToString();
            vendorDetModel.NAME_4 = IRS_OS_GeneralDet.GetValue("NAME_4").ToString();
            vendorDetModel.CITY = IRS_OS_GeneralDet.GetValue("CITY").ToString();
            vendorDetModel.DISTRICT = IRS_OS_GeneralDet.GetValue("DISTRICT").ToString();
            vendorDetModel.PO_BOX = IRS_OS_GeneralDet.GetValue("PO_BOX").ToString();
            vendorDetModel.POBX_PCD = IRS_OS_GeneralDet.GetValue("POBX_PCD").ToString();
            vendorDetModel.POSTL_CODE = IRS_OS_GeneralDet.GetValue("POSTL_CODE").ToString();
            vendorDetModel.REGION = IRS_OS_GeneralDet.GetValue("REGION").ToString();
            vendorDetModel.STREET = IRS_OS_GeneralDet.GetValue("STREET").ToString();
            vendorDetModel.COUNTRY = IRS_OS_GeneralDet.GetValue("STREET").ToString();
            vendorDetModel.COUNTRYISO = IRS_OS_GeneralDet.GetValue("COUNTRYISO").ToString();
            vendorDetModel.POBX_CTY = IRS_OS_GeneralDet.GetValue("POBX_CTY").ToString();
            vendorDetModel.LANGU = IRS_OS_GeneralDet.GetValue("LANGU").ToString();
            vendorDetModel.LANGU_ISO = IRS_OS_GeneralDet.GetValue("LANGU_ISO").ToString();
            vendorDetModel.TELEPHONE = IRS_OS_GeneralDet.GetValue("TELEPHONE").ToString();
            vendorDetModel.FORMOFADDR = IRS_OS_GeneralDet.GetValue("FORMOFADDR").ToString();
            vendorDetModel.TELEPHONE2 = IRS_OS_GeneralDet.GetValue("TELEPHONE2").ToString();

            //Console.WriteLine(vendorDetModel.Value);
            //Console.ReadKey();
            RfcSessionManager.EndContext(rfcDest);

            return vendorDetModel;
        }
    }
}

