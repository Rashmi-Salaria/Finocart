$(document).ready(function () {
    BindDatepicker('lstOprtFrmdat');
    BindDatepicker('lstOprtTodat');
    BindDatepicker('FromClender');
    BindDatepicker('Toclender');
    
    $('#btnInvoiceFilter').click(function () {
        
        var fromdate = $("#FromDate").val();
        var toDate = $("#ToDate").val();
        var AchrCmp = $("#ddl_AnchorComp").val();
        if (AchrCmp != 0) {
            $("#lblAchrCmpny").css("display", "none")
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
                            "url": GetLostOpportunitiesList,
                            "data": { companyID: $('#ddl_AnchorComp').val(), fromDate: $('#FromDate').val(), toDate: $('#ToDate').val() },
                            "type": "POST",
                            "datatype": "json",
                            "async": false
                        },
                        "columns": [
                            //{ "data": "companyID", "name": "CompanyID", "autoWidth": true }, //index 1
                            { "data": "noOfInvoice", "name": "NoOfInvoice", "autoWidth": true }, //index 1
                            { "data": "totalAppAmt", "name": "TotalAppAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }, //index 2   
                            { "data": "noOfDiscInvoices", "name": "NoOfDiscInvoices", "autoWidth": true },   //index 3           
                            { "data": "totalDiscAppAmt", "name": "TotalDiscAppAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }, //index 4
                            { "data": "discOffPercent", "name": "DiscOffPercent", "autoWidth": true }, //index 5
                            { "data": "approvedInv", "name": "ApprovedInv", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
                            { "data": "succAppAmtPercent", "name": "SuccAppAmtPercent", "autoWidth": true },
                            { "data": "succDiscAmtPercent", "name": "SuccDiscAmtPercent", "autoWidth": true },
                           
                        ]
                      

                    });
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
        }
        else {
            $("#lblAchrCmpny").css("display", "block")
            if (toDate == '') {
                $("#lblErrMsgToDate").css("display", "block");
            }
            else {
                $("#lblErrMsgToDate").css("display", "none");
            }
            if (fromdate == '') {
                $("#lblErrMsg").css("display", "block");
            }
            else {
                $("#lblErrMsg").css("display", "none");
            }
        }
    });

    $('#ExportAnchCompCSV').click(function () {
        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalApprovedINV').val().trim();
        url = "../Vendor/ExportAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });
    $('#btnFundingReq').click(function () {
        var fromdate = $("#FundReqFromDate").val();
        var toDate = $("#FundReqToDate").val();
        if (fromdate != "") {
            if (toDate != "") {
                $("#lblErrMsgToDate").css("display", "none");
                $("#lblErrMsg").css("display", "none");
                $("#dvFundingRequest").show();
                $('#tbl_FundingRequestList').DataTable().destroy();
                $('#tbl_FundingRequestList').dataTable({
                    "processing": true,
                    "serverSide": true,
                    "searching": true,
                    "dom": '"ltipr"',
                    "ajax": {
                        "url": GetFundingRequestList,
                        "data": { fromDate: $('#FundReqFromDate').val(), toDate: $('#FundReqToDate').val() },
                        "type": "POST",
                        "datatype": "json",
                    },
                    "columns": [
                        { "data": "companyID", "name": "CompanyID", "autoWidth": true }, //index 1
                        { "data": "companyName", "name": "CompanyName", "autoWidth": true, "class": "clsAnchCompanyName" }, //index 2   
                        { "data": "discOfferInvoice", "name": "DiscOfferInvoice", "autoWidth": true },
                        { "data": "discOfferInvoiceAmount", "name": "DiscOfferInvoiceAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
                        { "data": "discOfferAppInvoiceAmount", "name": "DiscOfferAppInvoiceAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
                        //{
                        //    "data": function (data, type, row) {
                        //        return "<lable class='clsAnchCompanyName'>" + data.companyName +"</lable>";
                        //    }
                        //},
                        {
                            "data": function (data, type, row) {
                                //return "<a href='../AnchorCompany/GetVendorInvoiceDetailsViewByVendorID/?icompanyID=" + data.companyID + "' class='actions-ico'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                                return "<a onclick='GetView(" + data.companyID + ")' class='actions-ico getInvoice clsDiscountOfferedInv' data-toggle='modal' data-target='#frview'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                            }
                        }

                    ],
                    //columnDefs: [
                    //    {
                    //        targets: [5],
                    //        className: "hide_column"
                    //    }
                    //]

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

   

    $('#ExportInvoiceCSV').click(function () {
        
        var fromDate = $('#FundReqFromDate').val().trim();
        var toDate = $('#FundReqToDate').val().trim();
        url = "../VendorAnalytics/ExportAnalyticsFundingRequest?fromDate=" + fromDate + "&toDate=" + toDate;
        window.location.href = url;
    });



});

//Binding datepicker to ID
function BindDatepicker(datepickerId) {

    var date_input = $('#'+datepickerId);
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


function GetView(companyID)
{
    $("#anchCompName").text($(event.target).closest("tr").find('.clsAnchCompanyName').text());
    $("#fromDate").text($('#FundReqFromDate').val());
    $("#toDate").text($('#FundReqToDate').val());
    $('#tblFundingReqAmount').DataTable().destroy();
    $('#tblFundingReqPercentage').DataTable().destroy();
    $('#tblFundingReqAmount').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetFundingRequestAmountList,
            "data": { companyID: companyID, fromDate: $('#FundReqFromDate').val(), toDate: $('#FundReqToDate').val() },
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "discount", "name": "Discount", "autoWidth": true }, //index 1  
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }, //index 2
            { "data": "rejectedAmt", "name": "RejectedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }, //index 3
            { "data": "pendingAmt", "name": "PendingAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }, //index 4
            { "data": "total", "name": "Total", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) } //index 4
        ],
  
    });

    $('#tblFundingReqPercentage').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetFundingRequestPercentList,
            "data": { companyID: companyID, fromDate: $('#FundReqFromDate').val(), toDate: $('#FundReqToDate').val() },
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "discount", "name": "Discount", "autoWidth": true }, //index 1  
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true }, //index 2
            { "data": "rejectedAmt", "name": "RejectedAmt", "autoWidth": true }, //index 3
            { "data": "pendingAmt", "name": "PendingAmt", "autoWidth": true }, //index 4
            { "data": "total", "name": "Total", "autoWidth": true } //index 4
        ],
     
    });
}
