$(document).ready(function () {
    debugger;

    $('#tbl_HolidayRecord').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetHolidayReasonListData,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
           
            { "data": "date", "name": "date", "autoWidth": true },
            { "data": "reason", "name": "Reason", "autoWidth": true },
        ],
        "order": [
            [1, "desc"],
        ],
    });

    oTable1 = $('#tbl_HolidayRecord').DataTable();
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

        oTable1.columns(2).search($('#txt_CompanyNameSearch').val().trim());
        //oTable1.columns(3).search($('#txt_ContactNameSearch').val().trim());
        //oTable1.columns(4).search($('#txt_ContactNameSearch').val().trim());
        oTable1.draw();
    });

});
