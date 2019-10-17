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
            { "data": "company_name", "name": "Company_name", "autoWidth": true },
            { "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },
            { "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true },
            { "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            
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

        oTable1.columns(0).search($('#txt_CompanyNameSearch').val().trim());
        oTable1.columns(6).search($('#InterestedAs').val().trim());
        //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
        oTable1.draw();
    });

});
