using System;
using Finocart_Scheduler.Microsoft;
using Finocart_Scheduler.Model;
using SAP.Middleware.Connector;

namespace Finocart_Scheduler.SAP
{
    public class SAPVendor : ISAPVendor
    {
        Logger logger = new Logger();
        /// <summary>
        /// Getting single vendor from SAP
        /// Date: 20 June 2019
        /// Developer: Shreyans Khandelwal(0538)
        /// </summary>
        /// <returns></returns>
        public VendorDetail GetVendor()
        {
            VendorDetail vendorDetModel = new VendorDetail();
            try
            {
                SAPCommonService objSAPCommon = new SAPCommonService();
                RfcDestination rfcDest = RfcDestinationManager.TryGetDestination("accelyides");

                if (rfcDest == null)
                {
                    RfcDestinationManager.RegisterDestinationConfiguration(objSAPCommon);
                    rfcDest = RfcDestinationManager.GetDestination("accelyides");
                }

                vendorDetModel = GetVendorDetail(rfcDest);
                return vendorDetModel;
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Getting vendor details from SAP
        /// Date: 20 June 2019
        /// Developer: Shreyans Khandelwal (0538)
        /// </summary>
        /// <param name="rfcDest"></param>
        /// <returns></returns>
        public VendorDetail GetVendorDetail(RfcDestination rfcDest)
        {
            try
            {
                VendorDetail vendorDetModel = new VendorDetail();

                RfcDestination SAPRfcDestination = RfcDestinationManager.GetDestination("accelyides");

                RfcRepository rfcrep = SAPRfcDestination.Repository;
                IRfcFunction BapiGetCompanyDetail = null;


                BapiGetCompanyDetail = rfcrep.CreateFunction("BAPI_VENDOR_GETDETAIL");
                BapiGetCompanyDetail.SetValue("VENDORNO", "VBX");
                BapiGetCompanyDetail.SetValue("COMPANYCODE", "0001");
                BapiGetCompanyDetail.Invoke(rfcDest);

                IRfcStructure IRS_OS_GeneralDet = BapiGetCompanyDetail.GetStructure("GENERALDETAIL");
                vendorDetModel.CompanyName = IRS_OS_GeneralDet.GetValue("VENDOR").ToString();
                vendorDetModel.ContactName = IRS_OS_GeneralDet.GetValue("NAME").ToString();
                vendorDetModel.ContactMobile = IRS_OS_GeneralDet.GetValue("TELEPHONE").ToString();
                vendorDetModel.Address = IRS_OS_GeneralDet.GetValue("FORMOFADDR").ToString();
                //vendorDetModel.NAME_2 = IRS_OS_GeneralDet.GetValue("NAME_2").ToString();
                //vendorDetModel.NAME_3 = IRS_OS_GeneralDet.GetValue("NAME_3").ToString();
                //vendorDetModel.NAME_4 = IRS_OS_GeneralDet.GetValue("NAME_4").ToString();
                //vendorDetModel.CITY = IRS_OS_GeneralDet.GetValue("CITY").ToString();
                //vendorDetModel.DISTRICT = IRS_OS_GeneralDet.GetValue("DISTRICT").ToString();
                //vendorDetModel.PO_BOX = IRS_OS_GeneralDet.GetValue("PO_BOX").ToString();
                //vendorDetModel.POBX_PCD = IRS_OS_GeneralDet.GetValue("POBX_PCD").ToString();
                //vendorDetModel.POSTL_CODE = IRS_OS_GeneralDet.GetValue("POSTL_CODE").ToString();
                //vendorDetModel.REGION = IRS_OS_GeneralDet.GetValue("REGION").ToString();
                ////vendorDetModel.Address = IRS_OS_GeneralDet.GetValue("STREET").ToString();
                //vendorDetModel.COUNTRY = IRS_OS_GeneralDet.GetValue("STREET").ToString();
                //vendorDetModel.COUNTRYISO = IRS_OS_GeneralDet.GetValue("COUNTRYISO").ToString();
                //vendorDetModel.POBX_CTY = IRS_OS_GeneralDet.GetValue("POBX_CTY").ToString();
                //vendorDetModel.LANGU = IRS_OS_GeneralDet.GetValue("LANGU").ToString();
                //vendorDetModel.LANGU_ISO = IRS_OS_GeneralDet.GetValue("LANGU_ISO").ToString();
                //vendorDetModel.TELEPHONE2 = IRS_OS_GeneralDet.GetValue("TELEPHONE2").ToString();
                
                RfcSessionManager.EndContext(rfcDest);

                return vendorDetModel;
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                throw ex;
            }
        }
    }
}
