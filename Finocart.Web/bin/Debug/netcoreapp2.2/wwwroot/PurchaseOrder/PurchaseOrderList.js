$(document).ready(function () {

    $('#tbl_PurchaseOrder').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": "../PurchaseOrder/getPurchaseOrderList",
            "type": "POST",
            "datatype": "json",

        },
        "columns": [
            { "data": "poNumber", "name": "PONumber", "autoWidth": true }, //index 0
            { "data": "noOfInvoices", "name": "NoOfInvoices", "autoWidth": true }, //index 1
            { "data": "vendorName", "name": "VendorName", "autoWidth": true }, //index 3
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 2) }, //index 4
            {
                "data": function (data, type, row) {
                    return "<a href='../PurchaseOrder/InvoiceListByPOId?PONumber=" + data.poNumber + "' class='btn'><img src='../Content/images/view.png' title='View' class='img-responsive' /></a>" +
                        "<a href='../PurchaseOrder/ExportRecordByPOId?PONumber=" + data.poNumber +"' class='btn'><img src='../Content/images/download.png' title='Download' class='img-responsive' /></a>";
                }
            }
        ],
    });

    oTable = $('#tbl_PurchaseOrder').DataTable();
    $('#btnPOFilter').click(function () {
        oTable.columns(2).search($('#txt_VendorName').val().trim());
        oTable.columns(0).search($('#txt_PurchaseOrder').val().trim());
        oTable.draw();
    });

    $('#ExportPOCSV').click(function () {
        var VendorName = $('#txt_VendorName').val().trim();
        var PurchaseOrderNo = $('#txt_PurchaseOrder').val().trim();
        url = "../PurchaseOrder/ExportPurchaseOrderData?PurchaseOrderNo=" + PurchaseOrderNo + "&VendorName=" + VendorName;
        window.location.href = url;
    });

    $('#tbl_InvoiceListByPO').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetInvoiceListByPOId,
            "data": { PONumber: $("#PONumber").val()},
            "type": "POST",
            "datatype": "json",

        },
        "columns": [
           
            { "data": "poNumber", "name": "PONumber", "autoWidth": true }, //index 0
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true }, //index 1
            { "data": "vendorName", "name": "VendorName", "autoWidth": true }, //index 2
            { "data": "createdDate", "name": "CreatedDate", "autoWidth": true }, //index 3
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 2) }, //index 4
            { "data": "discount", "name": "Discount", "autoWidth": true }, //index 5
            { "data": "days", "name": "Days", "autoWidth": true }, //index 6
            { "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true }, //index 7
            { "data": "invStatus", "name": "InvStatus", "autoWidth": true }, //index 8
            { "data": "rejectionReason", "name": "RejectionReason", "autoWidth": true }, //index 9
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 2)}, //index 10
            
        ],
    });

    oTable1 = $('#tbl_InvoiceListByPO').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(1).search($('#txt_InvoiceName').val().trim());
        oTable1.columns(8).search($('#ddl_Status').val().trim());
        oTable1.draw();
    });

    $('#ExportInvoiceCSV').click(function () {
        var InvoiceName = $('#txt_InvoiceName').val().trim();
        var InvoiceStatus = $('#ddl_Status').val().trim();
        var PurchaseOrderNo = $('#PONumber').val().trim();
        url = "../PurchaseOrder/ExportInvoiceByPOIdData?InvoiceNo=" + InvoiceName + "&InvoiceStatus=" + InvoiceStatus + "&PONumber=" + PurchaseOrderNo;
        window.location.href = url;
    });
});

