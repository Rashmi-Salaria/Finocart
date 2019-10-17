$(document).ready(function () {
    debugger;
    $('#tbl_UploadVendorList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        //"scrollX": true,
        "ajax": {
            "url": GetUploadData,
            "data": { VendorID: $("#VendorID").val(), VendorCompany: $("#lblVendorCompany").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            //{ "data": "vendorCompany", "name": "VendorCompany", "autoWidth": true },
            { "data": "poNumber", "name": "PONumber", "autoWidth": true },
            {
                "data": "poDate", "name": "PODate", "autoWidth": true, "format": "dd/MM/YYYY"
            },
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true },
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true, "format": "dd/MM/YYYY" },
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            //{ "data": "invoiceStatus", "name": "InvoiceStatus", "autoWidth": true },
            {
                "data": function (data, type, row) {
                    if (data.invoiceStatus == "Rejected") {
                        return "<td><span class='red'>Rejected</span></td>";
                    }
                    if (data.invoiceStatus == "Approved") {
                        return "<td><span class='green'>Approved</span></td>";
                    }
                    if (data.invoiceStatus == "Pending") {
                        return "<td><span class='yellow'>Pending</span></td>";
                    }
                    else {
                        return "<td><span>Eligible for Discounting</span></td>";
                    }
                }
            },
            //{ "data": "intresetedAs", "name": "IntresetedAs", "autoWidth": true },
            { "data": "invoiceApprovePayDays", "name": "InvoiceApprovePayDays", "autoWidth": true },
            //{ "data": "paymentDate", "name": "paymentDate", "autoWidth": true, "format": "dd/MM/YYYY"}
            {"data": "paymentDate", "name": "paymentDate", "autoWidth": true, "format": "dd/MM/YYYY"}

        ],

    });

    oTable1 = $('#tbl_UploadVendorList').DataTable();

    $('#btnComapnyFilter').click(function () {
        oTable1.columns(1).search($('#txt_PONumber').val().trim());
        oTable1.columns(3).search($('#txt_InvoiceNo').val().trim());
        oTable1.draw();
    });

    $('.searchTerm').on('keyup', function () {
        debugger;
        oTable1.columns(1).search($('#txt_PONumber').val().trim());
        oTable1.columns(2).search($('#txt_PONumber').val().trim());
        oTable1.columns(3).search($('#txt_PONumber').val().trim());
        oTable1.columns(4).search($('#txt_PONumber').val().trim());
        oTable1.columns(6).search($('#txt_PONumber').val().trim());
        oTable1.columns(7).search($('#txt_PONumber').val().trim());
        oTable1.columns(8).search($('#txt_PONumber').val().trim());
        oTable1.draw();
    });

    $('#ExportAnchCompCSV').click(function () {

        var VendorID = $('#VendorID').val();
        url = "../ExportUploadVendorToCSV?VendorID=" + VendorID;
        window.location.href = url;

    });

    $("#lblcompanyName").text($("#lblVendorCompany").val());
   
});
