namespace Finocart.CustomModel
{
    public enum VendorHeaderModel
    {
            Vendor_Name,
            Pan_Number,
            MSME_00Yes0No1,
            UAM_Number,
            Contact_Person_Name,
            Contact_Person_Designation,
            Email_ID,
            Contact_Number
    }

    public enum InvoiceHeaderModel
    {
            PO_Number,
            Invoice_Date00DD0MM0YYYY1,
            Invoice_Number,
            Invoice_Amount,
            Payment_Due_Date00DD0MM0YYYY1,
            Approved_Amount,
            //Payment_Days,
            Pan_Number
    }
}
