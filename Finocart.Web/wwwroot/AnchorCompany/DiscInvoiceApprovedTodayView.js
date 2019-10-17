$(document).ready(function () {
    $('#tbl_InvoiceApprovedTodayView').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetInvoiceApprovedTodayViewList,
            "data": { VendorID: $("#VendorID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "bucketID", "name": "BucketID", "autoWidth": true }, //index 0
            { "data": "bucketName", "name": "BucketName", "autoWidth": true }, //index 1   
            { "data": "vendorID", "name": "VendorID", "autoWidth": true }, //index 2
            { "data": "totalInvoiceCount", "name": "TotalInvoiceCount", "autoWidth": true },//index 3
            { "data": "totalApprovedAmt", "name": "TotalApprovedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 4
            {
                "data": "discOfferedDate", "name": "DiscOfferedDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },//index 5
            { "data": "totalDisbAmt", "name": "TotalDisbAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 6
            { "data": "earning", "name": "Earning", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 7
            {
                "data": function (data, type, row) {
                    return "<a href='../BucketWiseDiscInvViewList/?BucketID=" + data.bucketID + "&VendorID=" + data.vendorID + "'><img src='/Finocart/Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }
        ],
        columnDefs: [
            {
                targets: [2],
                className: "hide_column"
            }
        ],
    });

    oTable1 = $('#tbl_InvoiceApprovedTodayView').DataTable();
    $('#btnBucketFilter').click(function () {
        oTable1.columns(0).search($('#txt_BucketID').val().trim());
        oTable1.columns(3).search($('#txt_ApprovedAmt').val().trim());
        oTable1.draw();
    });


    $('.searchTerm').on('keyup', function () {
        oTable1.columns(0).search($('#txt_BucketID').val().trim());
        oTable1.columns(3).search($('#txt_ApprovedAmt').val().trim());
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