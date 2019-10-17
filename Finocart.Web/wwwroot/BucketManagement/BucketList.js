$(document).ready(function () {
    $('#tbl_Buckets').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetBucketList,
            "type": "POST",
            "datatype": "json",
            "async": "false"
        },
        "columns": [
            {
                "data": function (data, type, row) {            //index 0
                    return '<input type="checkbox"/>';
                }
            },
            { "data": "bucketID", "name": "BucketID", "autoWidth": true }, //index 0
            { "data": "bucketName", "name": "BucketName", "autoWidth": true }, //index 1
            { "data": "totalInvoiceCount", "name": "TotalInvoiceCount", "autoWidth": true },
            { "data": "totalInvoiceAmt", "name": "TotalInvoiceAmt", "autoWidth": true },
            { "data": "bucketCreatedDate", "name": "BucketCreatedDate", "autoWidth": true },
            { "data": "validUptoDate", "name": "ValidUptoDate", "autoWidth": true },
            { "data": "anchorCompStatus", "name": "AnchorCompStatus", "autoWidth": true },
            {
                "data": function (data, type, row) {
                    return "<a href='../BucketManagement/BucketListView?BucketID=" + data.bucketID + "' class='btn'><img src='../Content/images/view.png' title='View' class='img-responsive' /></a>"
                } //index 7
            }
        ],
    });

    oTable = $('#tbl_Buckets').DataTable();
    $('#btnFilter').click(function () {
        oTable.columns(2).search($('#txt_AnchorName').val().trim());
        oTable.columns(8).search($('#ddl_Status').val().trim());
        oTable.draw();
        GetLastSearchValue();
    });

    $('#ExportInvoiceCSV').click(function () {
        var AnchorName = $('#txt_AnchorName').val().trim();
        var InvoiceStatus = $('#ddl_Status').val().trim();
        url = "../Invoice/ExportInvoiceData?AnchorCompanyName=" + AnchorName + "&InvoiceStatus=" + InvoiceStatus;
        window.location.href = url;
    });

    $('#tbl_BucketsView').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetBucketListView,
            "data": { BucketID: $("#BucketID").val() },
            "type": "POST",
            "datatype": "json",
            "async": "false"
        },
        "columns": [
            { "data": "bucketID", "name": "BucketID", "autoWidth": true }, //index 0
            { "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true }, //index 1
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true },
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true },
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true },
            { "data": "discount", "name": "Discount", "autoWidth": true },
            { "data": "anchorCompStatus", "name": "AnchorCompStatus", "autoWidth": true },
            { "data": null, "autoWidth": true },
            { "data": null, "autoWidth": true },
        ],
    });

    oTable = $('#tbl_Buckets').DataTable();
    $('#btnFilter').click(function () {
        oTable.columns(2).search($('#txt_AnchorName').val().trim());
        oTable.columns(8).search($('#ddl_Status').val().trim());
        oTable.draw();
        GetLastSearchValue();
    });

    $('#ExportInvoiceCSV').click(function () {
        var AnchorName = $('#txt_AnchorName').val().trim();
        var InvoiceStatus = $('#ddl_Status').val().trim();
        url = "../Invoice/ExportInvoiceData?AnchorCompanyName=" + AnchorName + "&InvoiceStatus=" + InvoiceStatus;
        window.location.href = url;
    });
});