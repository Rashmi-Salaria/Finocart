$(document).ready(function () {
    $('#tbl_VendorPaymentDueListView').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetPaymentDueTodayViewList,
            "data": { VendorID: $("#VendorID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "poid", "name": "POID", "autoWidth": true }, //index 1
            { "data": "invoiceID", "name": "InvoiceID", "autoWidth": true }, //index 2   
            {
                "data": "invoiceDate", "name": "InvoiceDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month.length > 1 ? month :  month) + "/" + date.getFullYear();
                } },
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "earning", "name": "Earning", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "netPayableAmt", "name": "NetPayableAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },

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
});

function BackBtn() {
    history.back();
}
