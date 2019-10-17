$(document).ready(function () {

    $("#txt_ValidDateTime").datepicker({
        dateFormat: "yy-mm-dd"
    });

    $('#tbl_Invoice').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetInvoiceList,
            "type": "POST",
            "datatype": "json",
            "async":"false"
        },
        "columns": [
            {
                "data": function (data, type, row) {
                    return "<input type='checkbox' class='clsRowCheckbox'/>"
                },
            { "data": "poNumber", "name": "PONumber", "autoWidth": true }, //index 1
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true }, //index 2
            { "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true }, //index 3
            { "data": "createdDate", "name": "CreatedDate", "autoWidth": true }, //index 4
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true }, //index 5
            { "data": "discount", "name": "Discount", "autoWidth": true }, //index 6
            { "data": "days", "name": "Days", "autoWidth": true }, //index 7
            { "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true }, //index 8
            { "data": "invStatus", "name": "InvStatus", "autoWidth": true }, //index 9
            { "data": "rejectionReason", "name": "RejectionReason", "autoWidth": true }, //index 10
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true }, //index 11
            { "data": "invoiceID", "name": "InvoiceID", "autoWidth": true, "visible": true,"class":"clsRowInvoiceID" }, //index 12
        ],
    });

    oTable = $('#tbl_Invoice').DataTable();
    $('#btnFilter').click(function () {
        oTable.columns(3).search($('#txt_AnchorName').val().trim());
        oTable.columns(9).search($('#ddl_Status').val().trim());
        oTable.draw();
        GetLastSearchValue();
    });

    $('#ExportInvoiceCSV').click(function () {
        var AnchorName = $('#txt_AnchorName').val().trim();
        var InvoiceStatus = $('#ddl_Status').val().trim();
        url = "../Invoice/ExportInvoiceData?AnchorCompanyName=" + AnchorName + "&InvoiceStatus=" + InvoiceStatus;
        window.location.href = url;
    });
});

function GetLastSearchValue() {

    $.ajax({
        type: "get",
        url: GetLastSearchhistory,
        data: '',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            
            var totaltextboxcount = $('.clsLastSearch').length;
            var htmlRowLastSrch = "";
            var list = data.data;
            if (totaltextboxcount < $("#MaxSearchLimit").val() && $("#txt_AnchorName").val() != "") {
                htmlRowLastSrch += '<input type="text" class="searchTerm clsLastSearch" id="txt_LastSearch_' + (totaltextboxcount) + '" placeholder="Search by Name" value="' + list[(list.length - 1)].searchKeyValue.split(':')[1] + '" />';
                $("#dvlstSrch").append(htmlRowLastSrch);
            }
            else if (totaltextboxcount == $("#MaxSearchLimit").val() && $("#txt_AnchorName").val() != "") {
                $("#txt_LastSearch_0").remove();
                htmlRowLastSrch += '<input type="text" class="searchTerm clsLastSearch" id="txt_LastSearch_' + (totaltextboxcount) + '" placeholder="Search by Name" value="' + list[(list.length - 1)].searchKeyValue.split(':')[1] + '" />';
                $("#dvlstSrch").append(htmlRowLastSrch);
            }
            for (var i = 0; i < list.length; i++) {

            }
            
        },
        error: function () {
            alert("Error occured!!")
        }
    })
}

var MaxAvailableAmount = '@Url.Action("GetMaxAvailableAmount", "Invoice")';
$(document).ready(function () {
    
    $.ajax({
        type: "get",
        url: MaxAvailableAmount,
        data: '',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            alert(data);
        }
    });
});