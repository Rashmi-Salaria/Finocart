using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SAP.Middleware.Connector;
using System.Data;
using Finocart.WebAPI.Models;


namespace Finocart.WebAPI.Controllers.Common
{
    public class CommonAPIController : ApiController
    {
        //private readonly IInvoiceModule _invoiceRepository;
        //public CommonAPIController(IInvoiceModule invoiceRepository)
        //{
        //    _invoiceRepository = invoiceRepository;
        //}
        [Route("api/Invovice/Get")]
        public InvoiceValueModel GetInvoiceValue()
        {

            //SAPCommonService objSAPCommonService = new SAPCommonService();
            SAPCommon objSAPCommon = new SAPCommon();
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
            InvoiceValueModel invoiceValueModel = GetInvoiceValue(rfcDest);
            return invoiceValueModel;
        }

        public InvoiceValueModel GetInvoiceValue(RfcDestination rfcDest)
        {
            InvoiceValueModel invoiceValueModel = new InvoiceValueModel();
            RfcDestination SAPRfcDestination = RfcDestinationManager.GetDestination("accelyides");

            RfcRepository rfcrep = SAPRfcDestination.Repository;
            IRfcFunction BapiGetCompanyDetail = null;


            BapiGetCompanyDetail = rfcrep.CreateFunction("BAPI_INCOMINGINVOICE_GETDETAIL");
            BapiGetCompanyDetail.SetValue("INVOICEDOCNUMBER", "1000000020");
            BapiGetCompanyDetail.SetValue("FISCALYEAR", "2013");
            BapiGetCompanyDetail.Invoke(rfcDest);
            IRfcTable tblReturn = BapiGetCompanyDetail.GetTable("ITEMDATA");
            DataTable TBL = tblReturn.ToDataTable("TBL");
            for (int i = 0; i < TBL.Rows.Count; i++)
            {
                InvoiceValueModel invoiceValueModel1 = new InvoiceValueModel();

                invoiceValueModel1.REF_DOC = TBL.Rows[i]["REF_DOC"].ToString();
               // invoiceValueModel.lstInvoiceValue.Add(invoiceValueModel1);
            }

            IRfcStructure IRS_OS_HEADER = BapiGetCompanyDetail.GetStructure("HEADERDATA");
            invoiceValueModel.Value = IRS_OS_HEADER.GetValue("USERNAME").ToString();
            //Console.WriteLine(invoiceValueModel.Value);
            //Console.ReadKey();
            RfcSessionManager.EndContext(rfcDest);

            return invoiceValueModel;
        }
    }
}
