$(document).ready(function () {
    BindDatepicker('lstOprtFrmdat');
    BindDatepicker('lstOprtTodat');
    BindDatepicker('FromClender');
    BindDatepicker('Toclender');
    debugger;

    allDataBind();
    $('#btnComapnyFilter').click(function () {
        var TableName = $('#ddlTableList').val();
        var ColumnName = $('#ddlColumnList').val();
        var FormDate = $('#FundReqFromDate').val();
        var ToDate = $('#FundReqToDate').val();

        loadDataTable(TableName, ColumnName, FormDate, ToDate);
    });
  
    //loadDataTable();
    //$('#tbl_AuditTrailHistoryList').dataTable({
        
    //    "processing": true,
    //    "serverSide": true,
    //    "searching": true,
    //    "dom": '"ltipr"',
    //    "scrollX": true,
        
    //    "ajax": {
    //        "url": GetAuditTrailHistoryList,
    //        "type": "POST",
    //        "datatype": "json",
            
    //        "async": false
    //    },
    //    "columns": [
    //        { "data": "auditId", "name": "AuditId", "autoWidth": true, "visible": false, "searchable": false},
    //        { "data": "tableName", "name": "TableName", "autoWidth": true },
    //        { "data": "columnName", "name": "ColumnName", "autoWidth": true },
    //        { "data": "oldValue", "name": "OldValue", "autoWidth": true },
    //        { "data": "newValue", "name": "NewValue", "autoWidth": true },
    //        { "data": "createdBy", "name": "CreatedBy", "autoWidth": true },
    //        { "data": "createdOn", "name": "CreatedOn", "autoWidth": true },
    //    ],
    //    "order": [
    //        [1, "desc"],
    //    ],
    //});

    //oTable1 = $('#tbl_AuditTrailHistoryList').DataTable();
   
    //$('.searchTerm').on('keyup change', function () {

    //    oTable1.columns(1).search($('#txt_CompanyNameSearch').val().trim());
       
    //    oTable1.draw();

    //});


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
function loadDataTable(TableName ,ColumnName, FormDate, ToDate) {
    debugger;
    $('#tbl_AuditTrailHistoryList').dataTable({
        
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "scrollX": true,
        "destroy": true,
        "ajax": {
            "url": GetAuditTrailHistoryList,
            "type": "POST",
            "datatype": "json",
            "data": {
                TableName: TableName ,
                ColumnName: ColumnName,
                FormDate: FormDate,
                ToDate: ToDate

            },
            "async": false
        },
        "columns": [
            { "data": "auditId", "name": "AuditId", "autoWidth": true, "visible": false, "searchable": false },
            { "data": "tableName", "name": "TableName", "autoWidth": true },
            { "data": "columnName", "name": "ColumnName", "autoWidth": true },
            { "data": "oldValue", "name": "OldValue", "autoWidth": true },
            { "data": "newValue", "name": "NewValue", "autoWidth": true },
            { "data": "createdBy", "name": "CreatedBy", "autoWidth": true },
            { "data": "createdOn", "name": "CreatedOn", "autoWidth": true }
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

}
function allDataBind() {
    debugger;
    $('#tbl_AuditTrailHistoryList').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "scrollX": true,
        "destroy": true,
        "ajax": {
            "url": GetAuditTrailHistoryList,
            "type": "POST",
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
            { "data": "createdOn", "name": "CreatedOn", "autoWidth": true }
        ],
        "order": [
            [1, "desc"],
        ],
    });

    oTable1 = $('#tbl_AuditTrailHistoryList').DataTable();

    $('.searchTerm').on('keyup', function () {

        oTable1.columns(1).search($('#txt_CompanyNameSearch').val().trim());
        
        oTable1.draw();

    });

}