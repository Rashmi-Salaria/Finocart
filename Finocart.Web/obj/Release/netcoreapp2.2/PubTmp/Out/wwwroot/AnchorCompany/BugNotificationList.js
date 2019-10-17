$(document).ready(function () {
    $('#tbl_NotificationList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetNotificationData,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "id", "name": "id", "autoWidth": true, "visible": false, "searchable": false },
            { "data": "date", "name": "date", "autoWidth": true },
            { "data": "companyName", "name": "CompanyName", "autoWidth": true },
            { "data": "bugType", "name": "BugType", "autoWidth": true },
            { "data": "bugMessage", "name": "BugMessage", "autoWidth": true },

        ],
    });

    oTable1 = $('#tbl_NotificationList').DataTable();
    //$('#btnComapnyFilter').click(function () {
    //    oTable1.search($('#txt_CompanyNameSearch').val().trim());
    //    //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
    //    oTable1.draw();
    //});
    $('#ExportAnchCompCSV').click(function () {

        var ContactPerson = "";
        var CompanyName = $('#txt_CompanyNameSearch').val().trim();
        url = "../AnchorCompany/ExportRegisterToCSV?ContactPerson=" + ContactPerson + "&CompanyName=" + CompanyName;
        window.location.href = url;

    });

    $('.searchTerm').on('keyup change', function () {

        oTable1.columns(1).search($('#txt_CompanyNameSearch').val().trim());
        oTable1.columns(2).search($('#txt_CompanyNameSearch').val().trim());
        oTable1.columns(3).search($('#txt_CompanyNameSearch').val().trim());
        oTable1.columns(4).search($('#txt_CompanyNameSearch').val().trim());
        //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
        oTable1.draw();
    });

});
