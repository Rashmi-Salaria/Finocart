$(document).ready(function () {
    $('#tbl_VendorInvoiceApprovedToday').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetInvoiceApprovedTodayList,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "vendorID", "name": "VendorID", "autoWidth": true, "visible": false, "searchable": false}, //index 0
            { "data": "vendorName", "name": "VendorName", "autoWidth": true }, //index 1   
            { "data": "totalDiscAppToday", "name": "TotalDiscAppToday", "autoWidth": true },//index 2
            { "data": "totalApprovedAmt", "name": "TotalApprovedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 3
            { "data": "totalDisbAmt", "name": "TotalDisbAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },//index 4
            { "data": "earning", "name": "Earning", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0)},//index 5
            {
                "data": function (data, type, row) {
                    return "<a href='../AnchorCompDashboard/InvoiceApprovedTodayView/?VendorID=" + data.vendorID + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }

        ],

    });

    oTable1 = $('#tbl_VendorInvoiceApprovedToday').DataTable();
    $('#btnFilter').click(function () {
        oTable1.columns(1).search($('#txt_VendorName').val().trim());
        oTable1.columns(3).search($('#txt_TotalApprovedAmt').val().trim());
        oTable1.draw();
    });


    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_VendorName').val().trim());
        oTable1.columns(3).search($('#txt_TotalApprovedAmt').val().trim());
        oTable1.draw();
    });

    $('#ExportAnchCompCSV').click(function () {
       // alert(1);
        //alert(2);
        //var CompanyName = $('#txt_CompanyName').val().trim();
        //alert(2);
        //var TotalInvoiceAmt = $('#txt_TotalInvoiceAmt').val().trim();
        //url = "../Vendor/ExportAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;

        var TotalInvoiceAmt = "";
        var CompanyName = "";
        url = "/Vendor/ExportAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });
})