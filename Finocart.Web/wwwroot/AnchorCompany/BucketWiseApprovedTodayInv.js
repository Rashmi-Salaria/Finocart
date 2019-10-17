$(document).ready(function () {
    $('#tbl_BucketsWiseApprovedTodayInvoice').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetBucketWiseDiscInvViewList,
            "data": { VendorID: $("#VendorID").val(), BucketID: $("#BucketID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "poNumber", "name": "PONumber", "autoWidth": true }, //index 0
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true }, //index 1   
            { "data": "vendorName", "name": "VendorName", "autoWidth": true }, //index 2
            {
                "data": "invoiceCreatedDate", "name": "InvoiceCreatedDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month > 9 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },//index 3
            { "data": "invoiceAmount", "name": "InvoiceAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 4
            { "data": "approvedAmount", "name": "ApprovedAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 5
            { "data": "discountRate", "name": "DiscountRate", "autoWidth": true, },//index 6
            {
                "data": "discountOfferedDate", "name": "DiscountOfferedDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month > 9 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },//index 7
            {
                "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month > 9 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },//index 8
            {
                "data": "paymentDate", "name": "PaymentDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },//index 9
            { "data": "payableAmount", "name": "PayableAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 9
            { "data": "earning", "name": "Earning", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }//index 9
            //{
            //    "data": function (data, type, row) {
            //        return "<a href='../BucketWiseInvoiceViewList/?BucketID=" + data.bucketID + "&VendorID=" + data.vendorID + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
            //    }
            //}

        ],
    });

    oTable1 = $('#tbl_BucketsWiseApprovedTodayInvoice').DataTable();
    $('#btnBucketFilter').click(function () {
        oTable1.columns(0).search($('#txt_POID').val().trim());
        oTable1.columns(1).search($('#txt_InvoiceID').val().trim());
        oTable1.draw();
    });


    $('.searchTerm').on('keyup', function () {
        oTable1.columns(0).search($('#txt_POID').val().trim());
        oTable1.columns(1).search($('#txt_InvoiceID').val().trim());
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