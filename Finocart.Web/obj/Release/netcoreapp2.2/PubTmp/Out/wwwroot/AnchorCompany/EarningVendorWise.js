
$(document).ready(function () {

    var date_input = $('#lstOprtFrmdat');
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

    var date_input = $('#lstOprtTodat');
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    var dateToday = new Date();
    date_input.datepicker({
        format: 'mm/dd/yyyy',
        container: container,
        todayHighlight: true,
        autoclose: true,
        numberOfMonths: 2,
        showButtonPanel: true,
    })
    var date_input = $('#FromClender');
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


    var date_input = $('#Toclender');
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

    $('#ViewData').click(function () {
        debugger
        var fromdate = $("#FundReqFromDate").val();
        var toDate = $("#FundReqToDate").val();
        if (fromdate != "") {
            if (toDate != "") {
                $("#lblErrMsgToDate").css("display", "none");
                $("#lblErrMsg").css("display", "none");
                $("#dvFundingRequest").show();
                $('#tbl_EarningVenderwise').DataTable().destroy();
                $('#tbl_EarningVenderwise').dataTable({
                    "processing": true,
                    "serverSide": true,
                    "searching": true,
                    "dom": '"ltipr"',
                    "ajax": {
                        "url": GetVenderwiseEarning,
                        "data": { fromDate: $('#FundReqFromDate').val(), toDate: $('#FundReqToDate').val() },
                        "type": "POST",
                        "datatype": "json",
                    },
                    "columns": [

                        {
                            "data": function (data, type, row) {
                                return "<input type='checkbox' class='select-checkbox clsCompanyCheck_" + data.companyID + "'/>";
                            }
                        },
                        { "data": "vendorName", "name": "VendorName", "autoWidth": true, "class": "clsVendorName" }, //index 1
                        { "data": "invoiceCount", "name": "NoofOfferedDiscount", "autoWidth": true,"class":"clsInvoiceCount" }, //index 2   
                        { "data": "invoiceAmount", "name": "InvoiceAmount", "autoWidth": true, "class": "clsInvoiceAmt", render: $.fn.dataTable.render.number(',', '.', 0) },
                        { "data": "approvedAmount", "name": "ApprovedAmount", "autoWidth": true, "class": "clsInvoiceApprovedAmt", render: $.fn.dataTable.render.number(',', '.', 0) },
                        { "data": "amountpaid", "name": "AmountPaid", "autoWidth": true, "class": "clsAmountPaid", render: $.fn.dataTable.render.number(',', '.', 0)},
                        { "data": "earning", "name": "Earning", "autoWidth": true, "class": "clsEarning", render: $.fn.dataTable.render.number(',', '.', 0) },
                        {
                            "data": function (data, type, row) {
                                return "<a onclick='GetView(" + data.companyID + ")' class='actions-ico getInvoice clsDiscountOfferedInv' data-toggle='modal' data-target='#frview'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                            }
                        }

                    ],

                    "footerCallback": function (row, data, start, end, display) {
                        var api = this.api();
                        $("#lbltotalnoinvoices").text(api.column('.clsInvoiceCount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
                        $("#lbltotalinvoiceamount").text(api.column('.clsInvoiceAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
                        $("#lblapprovedinvoiceamount").text(api.column('.clsInvoiceApprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
                        $("#lbltotalamountpaid").text(api.column('.clsAmountPaid').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
                        $("#lbltotalearning").text(api.column('.clsEarning').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
                    }
                });
            }
            else {
                $("#lblErrMsgToDate").css("display", "block");
                if (fromdate == '') {
                    $("#lblErrMsg").css("display", "block");
                }
                else {
                    $("#lblErrMsg").css("display", "none");
                }
            }
        }
        else {
            $("#lblErrMsg").css("display", "block");
            if (toDate == '') {
                $("#lblErrMsgToDate").css("display", "block");
            }
            else {
                $("#lblErrMsgToDate").css("display", "none");
            }
        }
    });
    oTable1 = $('#tbl_InvoiceHistory').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(5).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });
    $('#AnchorComp-select-all').on('click', function () {
        var rows = oTable1.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });
    $('#ExportEarningCSV').click(function () {
        debugger;

        var fromDate = $('#FundReqFromDate').val().trim();
        var toDate = $('#FundReqToDate').val().trim();
        if (fromDate == "" && toDate == "")
        {
        alert("Pls Select Date");
        return;
         }
        url = "../AnchorCompDashboard/ExportRegisterToCSV?fromDate=" + fromDate + "&toDate=" + toDate;
        window.location.href = url;
    });

    $("#spSlider").click(function () {
        if ($("#spSlider").hasClass("clsAmount")) {
            $("#dvFundingReqAmount").show();
            $("#dvFundingReqPercentage").hide();
            $("#spSlider").removeClass("clsAmount");
            $("#spSlider").addClass("clsPercentage");
        }
        else {
            $("#dvFundingReqAmount").hide();
            $("#dvFundingReqPercentage").show();
            $("#spSlider").addClass("clsAmount");
            $("#spSlider").removeClass("clsPercentage");
        }
    });
});
function GetView(companyID) {
    $('#tblFundingReqAmount').DataTable().destroy();
    $('#tblFundingReqPercentage').DataTable().destroy();
    $('#tblFundingReqAmount').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetAmountDiscountRatewiseList,
            "data": { companyID: companyID, fromDate: $('#FundReqFromDate').val(), toDate: $('#FundReqToDate').val() },
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "discount", "name": "Discount", "autoWidth": true, "class": "clsDiscount" }, //index 1  
            { "data": "invoiceCount", "name": "InvoiceCount", "autoWidth": true, "class": "clsInvoiceCount" }, //index 2
            { "data": "amountPaid", "name": "AmountPaid", "autoWidth": true, "class": "clsAmountPaid", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 3
            { "data": "earning", "name": "Earning", "autoWidth": true, "class": "clsEarning", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 4

        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lbldiscount").text(api.column('.clsInvoiceCount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            $("#lblpaidamount").text(api.column('.clsAmountPaid').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            $("#lblearning").text(api.column('.clsEarning').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
        }
    });
    $('#tblFundingReqPercentage').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetPercentAmountDiscountRatewiseList,
            "data": { companyID: companyID, fromDate: $('#FundReqFromDate').val(), toDate: $('#FundReqToDate').val() },
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "discount", "name": "Discount", "autoWidth": true }, //index 1  
            { "data": "invoiceCount", "name": "InvoiceCount", "autoWidth": true, "class": "cslPercentiIvoiceCount" }, //index 2
            { "data": "amountPaid", "name": "AmountPaid", "autoWidth": true, "class": "clsPercentAmountPaid", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 3
            { "data": "earning", "name": "Earning", "autoWidth": true, "class": "clsPercentEarning", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 4

        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lblpercentinvoicenumer").text(api.column('.cslPercentiIvoiceCount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            $("#lblamountpaid").text(api.column('.clsPercentAmountPaid').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            $("#lblpercentearning").text(api.column('.clsPercentEarning').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
        }
    });
}