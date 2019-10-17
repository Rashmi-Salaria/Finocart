$(document).ready(function () {
    $('#tbl_UploadDocumentList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetUploadData,
            "data": { CompanyID: $("#CompanyID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            //{ "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },
            { "data": "document_Name", "name": "Focument_Name", "autoWidth": true },
            { "data": "filePath", "name": "FilePath", "autoWidth": true, "visible": false },
            {
                "data": function (data, type, row) {
                    return "<a href='../Download/?FilePath=" + data.filePath + "'>Download Here</a>";
                }
            }
        ],
    });

    oTable1 = $('#tbl_UploadDocumentList').DataTable();

    $('#btnComapnyFilter').click(function () {
        oTable1.columns(0).search($('#txt_PONameSearch').val().trim());
        oTable1.columns(1).search($('#txt_InvoiceNoSearch').val().trim());
        oTable1.draw();
    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(0).search($('#txt_PONameSearch').val().trim());
        oTable1.columns(1).search($('#txt_InvoiceNoSearch').val().trim());
        oTable1.draw();
    });

    $('#ExportAnchCompCSV').click(function () {

        var VendorID = $('#VendorID').val();
        url = "../ExportUploadVendorToCSV?VendorID=" + VendorID;
        window.location.href = url;

    });

    $('#btnApproved').click(function () {
        var CompanyName = $("#CompanyID").val()
        url = "../ApprovedStatus?CompanyName=" + CompanyName;
        window.location.href = url;
    });


    $('#btnReject').click(function () {
        var CompanyName = $("#CompanyID").val()
        url = "../RejectStatus?CompanyName=" + CompanyName;
        window.location.href = url;
    });


})
