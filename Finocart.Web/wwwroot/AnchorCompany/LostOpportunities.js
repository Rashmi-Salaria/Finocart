var InvoiceID = []; var CompInvID = []; var AnchCompanyID = []; var TotalSelectedInvAmt = 0; var TotalInvoice = 0;
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
        showButtonPanel: true,
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

    $('#btnInvoiceFilter').off().click(function () {
        debugger
         var fromdate = $("#FromDate").val();
        var toDate = $("#ToDate").val();
        var AchrCmp = $("#ddl_Vendors").val();
        //if (AchrCmp != 0) {
         //   $("#lblAchrCmpny").css("display", "none")
            if (fromdate != "") {
                $("#lblErrMsg").css("display", "none");
                $("#lblAchrCmpny").css("display", "none")
                if (toDate != "") {
                    $("#lblErrMsgToDate").css("display", "none");
                    $("#lblErrMsg").css("display", "none");
                    $("#lblAchrCmpny").css("display", "none")

                    $("#dvLostOpportunities").show();
                    $('#tbl_LostOpportunitiesList').DataTable().destroy();
                    $('#tbl_LostOpportunitiesList').dataTable({
                        "processing": true,
                        "serverSide": true,
                        "searching": true,
                        "dom": '"ltipr"',
                        "ajax": {
                            "url": GetAnchorLostOpportunities,
                            "data": { companyID: $('#ddl_Vendors').val(), fromDate: $('#FromDate').val(), toDate: $('#ToDate').val() },
                            "type": "POST",
                            "datatype": "json",
                            "async": false
                        },
                        "columns": [
                            { "data": "noOfDiscInvoice", "name": "NoOfDiscInvoice", "autoWidth": true }, //index 1
                            { "data": "totalDiscAppAmt", "name": "TotalDiscAppAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0)  }, //index 2   
                            { "data": "totalAppCount", "name": "TotalAppCount", "autoWidth": true },   //index 3           
                            { "data": "approvedDiscInvAmt", "name": "ApprovedDiscInvAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }, //index 4
                            { "data": "discOffPercent", "name": "DiscOffPercent", "autoWidth": true }, //index 5
                            { "data": "amountPaid", "name": "AmountPaid", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
                            { "data": "amountEarned", "name": "AmountEarned", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
                            { "data": "oppLostOnAmt", "name": "OppLostOnAmt", "autoWidth": true },
                            { "data": "perOppLost", "name": "PerOppLost", "autoWidth": true },
                        ]});
                }
                else {
                    $("#lblErrMsgToDate").css("display", "block");
                    if (AchrCmp == '') {
                        $("#lblAchrCmpny").css("display", "block")
                    }
                    else {
                        $("#lblAchrCmpny").css("display", "none")
                    }

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
                if (AchrCmp == '') {
                    $("#lblAchrCmpny").css("display", "block")
                }
                else {
                    $("#lblAchrCmpny").css("display", "none")
                }
            }
        //}
        //else {
        //    $("#lblAchrCmpny").css("display", "block")
        //    if (toDate == '') {
        //        $("#lblErrMsgToDate").css("display", "block");
        //    }
        //    else {
        //        $("#lblErrMsgToDate").css("display", "none");
        //    }
        //    if (fromdate == '') {
        //        $("#lblErrMsg").css("display", "block");
        //    }
        //    else {
        //        $("#lblErrMsg").css("display", "none");
        //    }
        //}
    });

});

function GetInvoice(id) {
    
    $.ajax({
        type: "Get",
        dataType: "html",
        url: "../Vendor/GetIndividualReceiveableDueToday?id=" + id,
        success: function (data) {

        },
        error: function () {
            alert("Error occured!!")
        }
    });
}

function BackInvoice() {
    $("#eAnchorCompInvoices").show();
    $("#eEligibleInvoices").hide();
}
function BackToDiscOfferedInv() {
    $("#dOfferedInvoices").show();
    $("#iDiscOfferedInv").hide();
}

