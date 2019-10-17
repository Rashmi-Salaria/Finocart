$(document).ready(function () {
    $('#tbl_BucketsWiseInvoiceView').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetBucketWiseInvoiceViewList,
            "data": { VendorID: $("#VendorID").val(), BucketID: $("#BucketID").val(), CompanyName: $("#hdnCompanyName").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "poid", "name": "POID", "autoWidth": true }, //index 0
            { "data": "invoiceID", "name": "InvoiceID", "autoWidth": true }, //index 1   
          /*  { "data": "vendorName", "name": "VendorName", "autoWidth": true }, *///index 2
            {
                "data": "invoiceCreationDate", "name": "InvoiceCreationDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },//index 3
            { "data": "invoiceAmount", "name": "InvoiceAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 4
            { "data": "approvedAmount", "name": "ApprovedAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 5
            { "data": "discount", "name": "Discount", "autoWidth": true },//index 6
            {
                "data": "discountOfferedDate", "name": "DiscountOfferedDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },//index 7
            {
                "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            }//index 8
            //{ "data": "discountValidUpto", "name": "DiscountValidUpto", "autoWidth": true },//index 9
            //{
            //    "data": function (data, type, row) {
            //        return "<a href='../BucketWiseInvoiceViewList/?BucketID=" + data.bucketID + "&VendorID=" + data.vendorID + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
            //    }
            //}

        ],
    });

    oTable1 = $('#tbl_BucketsWiseInvoiceView').DataTable();

    $('#btnBucketFilter').click(function () {
        oTable1.columns(0).search($('#txt_InvoiceID').val().trim());
        oTable1.columns(3).search($('#txt_POID').val().trim());
        oTable1.draw();
    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(0).search($('#txt_InvoiceID').val().trim());
        oTable1.columns(3).search($('#txt_POID').val().trim());
        oTable1.draw();
    });

    $('#ExportInvoiceCSV').click(function () {
        var InvoiceID = $('#txt_InvoiceID').val().trim();
        var POID = $('#txt_POID').val().trim();
        var VendorId = $("#VendorID").val().trim();
        var BucketID = $("#BucketID").val().trim();
        url = "../ExportBucketsWiseInvoice?InvoiceID=" + InvoiceID + "&POID=" + POID + "&VendorId=" + VendorId + "&BucketID=" + BucketID;
        window.location.href = url;
    });
    $('#lblcompanyName').text($("#hdnCompanyName").val());
});

function BackBtn() {
    history.back();
}