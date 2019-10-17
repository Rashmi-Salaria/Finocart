$(document).ready(function () {
    $('#tbl_FundsRequestListView').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": FundRequestView,
            "data": { Anchor_id: $("#Anchor_id").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "bankName", "name": "BankName", "autoWidth": true }, //index 1
            { "data": "available_Limits", "name": "Available_Limits", "autoWidth": true }, //index 2   
          
            { "data": "rateAndMonth", "name": "RateAndMonth", "autoWidth": true },
            { "data": "validity_upto", "name": "Validity_upto", "autoWidth": true },
            { "data": "requestStatus", "name": "RequestStatus", "autoWidth": true },
        ],
    });

    oTable1 = $('#tbl_VendorPaymentDueListView').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(1).search($('#txt_InvoiceID').val().trim());
        oTable1.columns(3).search($('#txt_InvoiceAmt').val().trim());
        oTable1.draw();
    });

    $('#ExportAnchCompCSV').click(function () {
        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalInvoiceAmt').val().trim();
        url = "../Vendor/ExportAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });
})
