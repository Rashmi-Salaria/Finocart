using System;
using System.Collections.Generic;
using System.Linq;
using SAP.Middleware.Connector;
using System.Data;

namespace Finocart.SAPService
{
    public class InvoiceDet
    {
        public InvoiceValueModel GetInvoiceValue(string InvoiceDocNumber, string FISCALYEAR)
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
            InvoiceValueModel invoiceValueModel = GetInvoiceValue(rfcDest, InvoiceDocNumber, FISCALYEAR);
            return invoiceValueModel;
        }

        public InvoiceValueModel GetInvoiceValue(RfcDestination rfcDest, string InvoiceDocNumber, string FISCALYEAR)
        {
            List<InvoiceValueModel> lstInvoiceValueDet = new List<InvoiceValueModel>();
            InvoiceValueModel invoiceValueModel = new InvoiceValueModel();
            RfcDestination SAPRfcDestination = RfcDestinationManager.GetDestination("accelyides");

            RfcRepository rfcrep = SAPRfcDestination.Repository;
            IRfcFunction BapiGetCompanyDetail = null;


            BapiGetCompanyDetail = rfcrep.CreateFunction("BAPI_INCOMINGINVOICE_GETDETAIL");
            BapiGetCompanyDetail.SetValue("INVOICEDOCNUMBER", InvoiceDocNumber);
            BapiGetCompanyDetail.SetValue("FISCALYEAR", FISCALYEAR);
            BapiGetCompanyDetail.Invoke(rfcDest);
            IRfcTable tblReturn = BapiGetCompanyDetail.GetTable("ITEMDATA");
            DataTable TBL = tblReturn.ToDataTable("TBL");

            IRfcTable tblTAXReturn = BapiGetCompanyDetail.GetTable("TAXDATA");
            DataTable TBLTaxReturn = tblTAXReturn.ToDataTable("TBL");

            IRfcTable tblWithTAXReturn = BapiGetCompanyDetail.GetTable("WITHTAXDATA");
            DataTable DtWithTAXReturn = tblWithTAXReturn.ToDataTable("TBL");

            IRfcTable tblVendorSplitData = BapiGetCompanyDetail.GetTable("VENDORITEMSPLITDATA");
            DataTable DtVendorSplitData = tblVendorSplitData.ToDataTable("TBL");
            //for (int i = 0; i < TBL.Rows.Count; i++)
            //{
            //    InvoiceValueModel InvoiceValueModel = new InvoiceValueModel();

            //    InvoiceValueModel.REF_DOC = TBL.Rows[i]["REF_DOC"].ToString();
            //   // invoiceValueModel.lstInvoiceValue.Add(InvoiceValueModel);
            //}
            invoiceValueModel.lstItemInvoiceDet = (from DataRow row in TBL.Rows
                                                   select new InvoiceValueModel
                                                   {
                                                       INVOICE_DOC_ITEM = row["INVOICE_DOC_ITEM"].ToString(),
                                                       PO_NUMBER = row["INVOICE_DOC_ITEM"].ToString(),
                                                       PO_ITEM = row["PO_ITEM"].ToString(),
                                                       SERIAL_NO = row["SERIAL_NO"].ToString(),
                                                       REF_DOC = row["REF_DOC"].ToString(),
                                                       REF_DOC_YEAR = row["REF_DOC_YEAR"].ToString(),
                                                       REF_DOC_IT = row["REF_DOC_IT"].ToString(),
                                                       TAX_CODE = row["TAX_CODE"].ToString(),
                                                       ITEM_AMOUNT = row["ITEM_AMOUNT"].ToString(),
                                                       QUANTITY = row["QUANTITY"].ToString(),
                                                       PO_UNIT = row["PO_UNIT"].ToString(),
                                                       PO_UNIT_ISO = row["PO_UNIT_ISO"].ToString()
                                                   }).ToList();

            invoiceValueModel.lstInvoiceTAXDet = (from DataRow row in TBLTaxReturn.Rows
                                                  select new InvoiceValueModel
                                                  {
                                                      TAX_CODE = row["TAX_CODE"].ToString(),
                                                      TAX_AMOUNT = row["TAX_AMOUNT"].ToString(),
                                                      VEND_ERROR = row["VEND_ERROR"].ToString(),
                                                      TAX_ERROR = row["TAX_ERROR"].ToString(),
                                                  }).ToList();

            invoiceValueModel.lstWithTAXInvoiceDet = (from DataRow row in DtWithTAXReturn.Rows
                                                      select new InvoiceValueModel
                                                      {
                                                          SPLIT_KEY = row["SPLIT_KEY"].ToString(),
                                                          WI_TAX_TYPE = row["WI_TAX_TYPE"].ToString()
                                                      }).ToList();

            invoiceValueModel.lstVEndorItemSolitData = (from DataRow row in DtVendorSplitData.Rows
                                                        select new InvoiceValueModel
                                                        {
                                                            SPLIT_KEY = row["SPLIT_KEY"].ToString(),
                                                            SPLIT_AMOUNT = row["SPLIT_AMOUNT"].ToString()
                                                        }).ToList();

            IRfcStructure IRS_OS_HEADER = BapiGetCompanyDetail.GetStructure("HEADERDATA");
            invoiceValueModel.INV_DOC_NO = IRS_OS_HEADER.GetValue("INV_DOC_NO").ToString();
            invoiceValueModel.USERNAME = IRS_OS_HEADER.GetValue("FISC_YEAR").ToString();
            invoiceValueModel.FISC_YEAR = IRS_OS_HEADER.GetValue("USERNAME").ToString();
            invoiceValueModel.INVOICEE_IND = IRS_OS_HEADER.GetValue("INVOICE_IND").ToString();
            invoiceValueModel.DOC_TYPE = IRS_OS_HEADER.GetValue("DOC_TYPE").ToString();
            invoiceValueModel.DOC_DATE = DateTime.Parse(IRS_OS_HEADER.GetValue("DOC_DATE").ToString());
            invoiceValueModel.PSTNG_DATE = DateTime.Parse(IRS_OS_HEADER.GetValue("PSTNG_DATE").ToString());
            invoiceValueModel.USERNAME = IRS_OS_HEADER.GetValue("USERNAME").ToString();
            invoiceValueModel.REF_DOC_NO = IRS_OS_HEADER.GetValue("REF_DOC_NO").ToString();
            invoiceValueModel.COMP_CODE = IRS_OS_HEADER.GetValue("COMP_CODE").ToString();
            invoiceValueModel.DIFF_INV = IRS_OS_HEADER.GetValue("DIFF_INV").ToString();
            invoiceValueModel.CURRENCY = IRS_OS_HEADER.GetValue("CURRENCY").ToString();
            invoiceValueModel.CURRENCY_ISO = IRS_OS_HEADER.GetValue("CURRENCY_ISO").ToString();
            invoiceValueModel.EXCH_RATE = IRS_OS_HEADER.GetValue("EXCH_RATE").ToString();
            invoiceValueModel.EXCH_RATE_V = IRS_OS_HEADER.GetValue("EXCH_RATE_V").ToString();
            invoiceValueModel.GROSS_AMT = IRS_OS_HEADER.GetValue("GROSS_AMNT").ToString();
            invoiceValueModel.BLINE_DATE = DateTime.Parse(IRS_OS_HEADER.GetValue("BLINE_DATE").ToString());
            invoiceValueModel.ENTRY_DATE = DateTime.Parse(IRS_OS_HEADER.GetValue("ENTRY_DATE").ToString());
            invoiceValueModel.ENTRY_TIME = DateTime.Parse(IRS_OS_HEADER.GetValue("ENTRY_TIME").ToString());
            invoiceValueModel.DISCNT = IRS_OS_HEADER.GetValue("DISCNT").ToString();
            invoiceValueModel.INVOICE_STATUS = IRS_OS_HEADER.GetValue("INVOICE_STATUS").ToString();

            //Console.WriteLine(invoiceValueModel.Value);
            //Console.ReadKey();
            RfcSessionManager.EndContext(rfcDest);

            return invoiceValueModel;
        }
    }
}
