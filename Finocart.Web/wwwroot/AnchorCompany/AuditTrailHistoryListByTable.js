﻿$('#ddlTableList').change(function () {
        BindDatepicker('lstOprtFrmdat');
        BindDatepicker('lstOprtTodat');
        BindDatepicker('FromClender');
        BindDatepicker('Toclender');
        debugger;
        $('#tbl_AuditTrailHistoryList').dataTable({

            "processing": true,
            "serverSide": true,
            "searching": true,
            "dom": '"ltipr"',
            "scrollX": true,
            "ajax": {
                "url": "/AnchorCompany/GetAuditTrailHistoryListByTable",
                "type": "POST",
                "data": { TableName: $("#ddlTableList").val() },
                "datatype": "json",
                "async": false
            },
            "columns": [
                { "data": "auditId", "name": "AuditId", "autoWidth": true, "visible": false, "searchable": false },
                { "data": "tableName", "name": "TableName", "autoWidth": true },
                { "data": "columnName", "name": "ColumnName", "autoWidth": true },
                { "data": "oldValue", "name": "OldValue", "autoWidth": true },
                { "data": "newValue", "name": "NewValue", "autoWidth": true },
                { "data": "createdBy", "name": "CreatedBy", "autoWidth": true },
                { "data": "createdOn", "name": "CreatedOn", "autoWidth": true },
            ],
            "order": [
                [1, "desc"],
            ],
        });
   
    oTable1 = $('#tbl_AuditTrailHistoryList').DataTable();

    $('.searchTerm').on('keyup change', function () {

        oTable1.columns(1).search($('#txt_CompanyNameSearch').val().trim());

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
});
