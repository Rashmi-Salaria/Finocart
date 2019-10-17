$(document).ready(function () {
    debugger;

    $('#tbl_setLimitHistory').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetSetLimitistory,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
           
            { "data": "date", "name": "date", "autoWidth": true },
            { "data": "initialAmount", "name": "InitialAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', 0)},
            { "data": "initialRate", "name": "InitialRate", "autoWidth": true },
            { "data": "updatedAmount", "name": "UpdatedAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', 0)},
            { "data": "updatedRate", "name": "UpdatedRate", "autoWidth": true },
            { "data": "personUpdated", "name": "PersonUpdated", "autoWidth": true },
            
        ],
        "order": [
            [1, "desc"],
        ],
    });

    oTable1 = $('#tbl_setLimitHistory').DataTable();
    //$('#btnComapnyFilter').click(function () {
    //    oTable1.columns(0).search($('#FundReqFromDate').val().trim());
    //    oTable1.columns(1).search($('#FundReqToDate').val().trim());
    //    oTable1.draw();
    //});
    //$('#ExportAnchCompCSV').click(function () {

    //    var ContactPerson = $('#txt_ContactNameSearch').val().trim();
    //    var CompanyName = $('#txt_CompanyNameSearch').val().trim();
    //    url = "../ExportRegisterToCSV?ContactPerson=" + ContactPerson + "&CompanyName=" + CompanyName;
    //    window.location.href = url;

    //});

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_SearchField').val().trim());
        oTable1.columns(2).search($('#txt_SearchField').val().trim());
        oTable1.draw();
    });

});
