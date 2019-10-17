$(document).ready(function () {
    $('#tbl_CompanyBankViewList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetCompanyBank,
            "data": { CompanyID: $("#hdnCompanyID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "anchCompName", "name": "AnchCompName", "autoWidth": true },
            { "data": "bankName", "name": "BankName", "autoWidth": true },
            { "data": "availableLimits", "name": "AvailableLimits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "utilizedLimits", "name": "UtilizedLimits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "interestrate", "name": "Interestrate", "autoWidth": true },
            {
                "data": "validityUpto", "name": "ValidityUpto", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },
            { "data": "drawFunds", "name": "DrawFunds", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }
        ],
    });

    oTable1 = $('#tbl_CompanyBankViewList').DataTable();
    //$('#btnComapnyFilter').click(function () {
    //    oTable1.search($('#txt_CompanyNameSearch').val().trim());
    //    //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
    //    oTable1.draw();
    //});
    $('#ExportAnchCompCSV').click(function () {
        var Search = $('#txt_CompanyNameSearch').val().trim();
        url = "../ExportCompanyBankViewToCSV?Search=" + Search + "&CompanyID=" + $("#hdnCompanyID").val();
        window.location.href = url;

    });

    $('.searchTerm').on('keyup', function () {

        oTable1.columns(0).search($('#txt_CompanyNameSearch').val().trim());
        //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
        oTable1.draw();
    });
})