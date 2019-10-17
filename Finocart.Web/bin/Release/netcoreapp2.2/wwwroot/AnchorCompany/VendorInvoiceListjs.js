$(document).ready(function () {
    $('#tbl_CompanyInvoiceList').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetVendorInvoiceList,
            "data": {CompanyID: $("#CompanyID").val()},
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "invoiceID", "name": "InvoiceID", "autoWidth": true, "visible": false }, //index 1
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true }, //index 2
            { "data": "vendorName", "name": "VendorName", "autoWidth": true }, //index 3
            { "data": "companyID", "name": "CompanyID", "autoWidth": true },
            { "data": "poNumber", "name": "PONumber", "autoWidth": true },
            {
                "data": "poDate", "name": "PODate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                } },
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            {
                "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                } },
            { "data": "discount", "name": "Discount", "autoWidth": true },
            { "data": "invStatus", "name": "INVStatus", "autoWidth": true },
            { "data": "rejectionReason", "name": "RejectionReason", "autoWidth": true },
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },


            {
                "data": function (data, type, row) {
                    return "<a href='#' class='actions-ico'><img src='/Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                }
            }

        ],
    });

    oTable1 = $('#tbl_CompanyInvoiceList').DataTable();
        $('#btnInvoiceFilter').click(function () {
            oTable1.columns(1).search($('#txt_CompanyName').val().trim());
            oTable1.draw();
        });

        
});

