using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Finocart.CustomModel.SAP
{
   public class InvoiceValueModel
    {
        [Key]
        #region Header data
        public string Value { get; set; }
        public string REF_DOC { get; set; }
        public string INV_DOC_NO { get; set; }
        public string FISC_YEAR { get; set; }
        public string INVOICEE_IND { get; set; }
        public string DOC_TYPE { get; set; }
        public DateTime DOC_DATE { get; set; }
        public DateTime PSTNG_DATE { get; set; }
        public string USERNAME { get; set; }
        public string REF_DOC_NO { get; set; }
        public string COMP_CODE { get; set; }
        public string DIFF_INV { get; set; }
        public string CURRENCY { get; set; }
        public string CURRENCY_ISO { get; set; }
        public string EXCH_RATE { get; set; }
        public string EXCH_RATE_V { get; set; }
        public string GROSS_AMT { get; set; }
        public string CALC_TAX_IND { get; set; }
        public string PMNTTRMS { get; set; }
        public DateTime BLINE_DATE { get; set; }
        public string DSCT_DAYS1 { get; set; }
        public string DSCT_DAYS2 { get; set; }
        public string NETTERMS { get; set; }
        public string DSCT_PCT1 { get; set; }
        public string DSCT_PCT2 { get; set; }
        public string DEL_COSTS { get; set; }
        public DateTime ENTRY_DATE { get; set; }
        public DateTime ENTRY_TIME { get; set; }
        public string DISCNT { get; set; }
        public string INVOICE_STATUS { get; set; }
        #endregion

        #region Item data
        public string INVOICE_DOC_ITEM { get; set; }
        public string PO_NUMBER { get; set; }
        public string PO_ITEM { get; set; }
        public string SERIAL_NO { get; set; }
        public string REF_DOC_YEAR { get; set; }
        public string REF_DOC_IT { get; set; }
        public string TAX_CODE { get; set; }
        public string ITEM_AMOUNT { get; set; }
        public string QUANTITY { get; set; }
        public string PO_UNIT { get; set; }
        public string PO_UNIT_ISO { get; set; }
        #endregion

        #region Tax_data
        public string TAX_AMOUNT { get; set; }
        public string VEND_ERROR { get; set; }
        public string TAX_ERROR { get; set; }
        #endregion

        #region With tax data
        public string SPLIT_KEY { get; set; }
        public string WI_TAX_TYPE { get; set; }
        public string WI_TAX_CODE { get; set; }
        public string WI_TAX_BASE { get; set; }
        public string WI_TAX_AMT { get; set; }
        public string WI_TAX_WITHHELD_AMT { get; set; }
        #endregion

        #region
        public string SPLIT_AMOUNT { get; set; }
        public string PYMT_METH { get; set; }
        #endregion
        public List<InvoiceValueModel> lstItemInvoiceDet { get; set; }
        public List<InvoiceValueModel> lstInvoiceTAXDet { get; set; }
        public List<InvoiceValueModel> lstWithTAXInvoiceDet { get; set; }
        public List<InvoiceValueModel> lstVEndorItemSolitData { get; set; }
    }
}
