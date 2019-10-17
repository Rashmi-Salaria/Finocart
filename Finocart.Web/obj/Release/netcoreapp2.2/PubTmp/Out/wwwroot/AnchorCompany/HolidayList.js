$(document).ready(function () {
    $('#txtSearchfield').val('');
    debugger;
    BindDatepicker('lstOprtFrmdat');
    BindDatepicker('lstOprtTodat');
    BindDatepicker('FromClender');
    //BindDatepicker('Toclender');

    $('#tbl_HolidayList').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetHolidayListData,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "id", "name": "id", "autoWidth": true, "visible": false },
            { "data": "date", "name": "date", "autoWidth": true },
            { "data": "reason", "name": "Reason", "autoWidth": true },
            { "data": "createdBy", "name": "createdBy", "autoWidth": true },
            { "data": "createdDate", "name": "createdDate", "autoWidth": true },
            { "data": "updatedBy", "name": "	updatedBy", "autoWidth": true },
            { "data": "updatedDate", "name": "updateddate", "autoWidth": true },
            {
                "data": function (data, type, row) {
                    return "<a class='edit' href='#' data-id =" + data.id + " data-date ='" + data.date + "' data-reason ='" + data.reason +"' ><img src='../Content/images/edit.png' title='Edit' class='img-responsive' style='margin-left: 38px;position: absolute;' /></a>" +
                        "<a class='delete' href='#' data-id ="+ btoa(data.id)+"><img src='../Content/images/deletebox.png' title='Delete' class='img-responsive' style='margin-left: 70px;'/></a>";
                }
            }
        ],
        "order": [
            [1, "desc"],
        ],
    });

    oTable1 = $('#tbl_HolidayList').DataTable();
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

    $('.searchTerm').on('keyup change', function () {

        oTable1.columns(1).search($('#txtSearchfield').val().trim());
        //oTable1.columns(3).search($('#txt_ContactNameSearch').val().trim());
        //oTable1.columns(4).search($('#txt_ContactNameSearch').val().trim());
        oTable1.draw();
        
    });

    //Binding datepicker to ID
    function BindDatepicker(datepickerId) {

        var date_input = $('#' + datepickerId);
        var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
        var dateToday = new Date();
        date_input.datepicker({
            format: 'dd/mm/yyyy',
            container: container,
            todayHighlight: true,
            autoclose: true,
            numberOfMonths: 2,
            showButtonPanel: true
        })
    }

    $('.edit').click(function (ev) {
        debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var holidate = $buttonClicked.attr('data-date');
        var reason = $buttonClicked.attr('data-reason');

        $("#hdnRecordId").val(id);
        $("#txtDate").val(holidate);
        $("#txtReason").val(reason);
        $("#holidayModal").modal('show');
        
    });
    $('.delete').click(function () {
        debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        $.ajax({
            type: "GET",
            url: '../AnchorCompany/DeleteHolidate/',
            contentType: "application/json; charset=utf-8",
            data: { ID: id },
            datatype: "json",
            success: function () {
                debugger;
                if (confirm('Are you sure want delete record?')) {
                    var url = $(this).attr('href');
                    $('#content').load(url);
                    window.location.reload();
                }
                else {
                    return false;
                }
       
            },

            error: function () {
                alert("Dynamic content load failed.");
            }
        })
        
    });

});
