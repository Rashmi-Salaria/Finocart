$(document).ready(function () {
    BindDatepicker('lstOprtFrmdat');
    BindDatepicker('lstOprtTodat');
    BindDatepicker('FromClender');
    BindDatepicker('Toclender');

    $('#tbl_AuditTrailList').dataTable({
       
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetAuditTrailLogData,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "moduleName", "name": "ModuleName", "autoWidth": true },
            { "data": "pageName", "name": "PageName", "autoWidth": true },
            {
                "data": "logDate", "name": "LogDate", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            { "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },
        ],
         "order": [
            [2, "desc"],
        ],
    });

    oTable1 = $('#tbl_AuditTrailList').DataTable();
    $('#btnComapnyFilter').click(function () {
        oTable1.columns(0).search($('#FundReqFromDate').val().trim());
        oTable1.columns(1).search($('#FundReqToDate').val().trim());
        oTable1.draw();
    });
    $('#ExportAnchCompCSV').click(function () {

        var ContactPerson = $('#txt_ContactNameSearch').val().trim();
        var CompanyName = $('#txt_CompanyNameSearch').val().trim();
        url = "../ExportRegisterToCSV?ContactPerson=" + ContactPerson + "&CompanyName=" + CompanyName;
        window.location.href = url;

    });

    //$('.searchTerm').on('keyup', function () {

    //    oTable1.columns(0).search($('#txt_CompanyNameSearch').val().trim());
    //    oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
    //    oTable1.draw();
    //});

    //Binding datepicker to ID
    function BindDatepicker(datepickerId) {

        var date_input = $('#' + datepickerId);
        var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
        var dateToday = new Date();
        date_input.datepicker({
            format: 'mm/dd/yyyy',
            container: container,
            todayHighlight: true,
            autoclose: true,
            numberOfMonths: 2,
            showButtonPanel: true
        })
    }

})
