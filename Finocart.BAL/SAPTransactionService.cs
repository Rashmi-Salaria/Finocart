using System;
using System.Collections.Generic;
using System.Text;
using Finocart.Interface;


using System.Data;

namespace Finocart.Services
{
    //public class SAPTransactionService:IInvoiceModule
    //{
    //    public InvoiceValueModel GetInvoiceValue(RfcDestination rfcDest)
    //    {
    //        InvoiceValueModel invoiceValueModel = new InvoiceValueModel();
    //        RfcDestination SAPRfcDestination = RfcDestinationManager.GetDestination("accelyides");

    //        RfcRepository rfcrep = SAPRfcDestination.Repository;
    //        IRfcFunction BapiGetCompanyDetail = null;


    //        BapiGetCompanyDetail = rfcrep.CreateFunction("BAPI_INCOMINGINVOICE_GETDETAIL");
    //        BapiGetCompanyDetail.SetValue("INVOICEDOCNUMBER", "1000000020");
    //        BapiGetCompanyDetail.SetValue("FISCALYEAR", "2013");
    //        BapiGetCompanyDetail.Invoke(rfcDest);
    //        IRfcTable tblReturn = BapiGetCompanyDetail.GetTable("ITEMDATA");
    //        DataTable TBL = tblReturn.ToDataTable("TBL");
    //        for (int i = 0; i < TBL.Rows.Count; i++)
    //        {
    //            InvoiceValueModel invoiceValueModel1 = new InvoiceValueModel();

    //            invoiceValueModel1.REF_DOC = TBL.Rows[i]["REF_DOC"].ToString();
    //            invoiceValueModel.lstInvoiceValue.Add(invoiceValueModel1);
    //        }

    //        IRfcStructure IRS_OS_HEADER = BapiGetCompanyDetail.GetStructure("HEADERDATA");
    //        invoiceValueModel.Value = IRS_OS_HEADER.GetValue("USERNAME").ToString();
    //        Console.WriteLine(invoiceValueModel.Value);
    //        Console.ReadKey();
    //        RfcSessionManager.EndContext(rfcDest);

    //        return invoiceValueModel;
    //    }
    //}
}
