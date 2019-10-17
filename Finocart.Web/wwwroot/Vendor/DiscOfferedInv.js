$(document).ready(function () {
   
    $('#tbl_DiscountOfferedInvDashboard').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            //"url": GetDiscountOfferedInvList,
            "url": "../Vendor/GetDiscountOfferedInvList",
            "data": { CompanyId: $("#Item3_CompanyId").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [

            //{ "data": function (data, type, row) {return  } },
            { "data": "poNumber", "name": "PONumber", "autoWidth": true, "class": "clsPONumber" }, //index 1
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true, "class": "clsInvoiceNo" }, //index 2
            { "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true, "class": "clsCompanyName" }, //index 3
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"}, //index 4
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, "class": "clsInvoiceAmt", render: $.fn.dataTable.render.number(',', '.', 0)}, //index 5
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, "class": "clsApprovedAmt", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 6
            { "data": "discount", "name": "Discount", "autoWidth": true, "class": "clsDiscount" }, //index 7
            {
                "data": "bucketCreatedDate", "name": "BucketCreatedDate", "autoWidth": true, "class": "clsBucketCreatedDate", "format": "dd/MM/YYYY"}, //index 8
            //{
                //"data": "validToDate", "name": "ValidToDate", "autoWidth": true, "class": "clsDiscValidUptoDate", "render": function (data) {
                //    var date = new Date(data);
                //    var month = date.getMonth() + 1;
                //    return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                //}}, //index 9
            {
                "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true, "class": "clsPaymentDueDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            }, //index 10
            //{ "data": "invStatus", "name":"InvStatus", "autoWidth": true, "class": "clsInvoiceStatus" }, //index 11
            {
                "data": function (data, type, row) {
                    if (data.invStatus == "Rejected") {
                        return "<td class='clsInvoiceStatus'><span class='red'>Rejected</span></td>";
                    }
                    if (data.invStatus == "Approved") {
                        return "<td class='clsInvoiceStatus'><span class='green'>Approved</span></td>";
                    }
                    if (data.invStatus == "Pending") {
                        return "<td class='clsInvoiceStatus'><span class='yellow'>Pending</span></td>";
                    }
                }
            },
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsRowInvoiceID' value='" + data.invoiceID + "'/>";
                }
            },//index 12
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsInvApprovePayDay' value='" + data.invoiceApprovePayDays + "'/>";
                }
            },//index 13
            {
                "data": function (data, type, row) {
                    return "<input type='text' class='clsInvApprovedDate' value='" + data.invoiceApprovalDate + "' />";
                }
            },//index 14
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsFormulaRow_" + data.invoiceID + "'/>";
                }
            },//index 15
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsCompanyId' value='" + data.companyId + "'/>";
                }
            },//index 16
            {
                "data": function (data, type, row) {
                    //return "<a href='#' onclick='DownloadInvoice(" + data.companyId + ")' class='btn'><img src='../Content/images/download.png' title='Download' class='img-responsive' title='Download invoice'/></a>" +
                     return "<a href='#' onclick='GetInvoiceJourney(" + data.invoiceID + ",this)' data-para1='DiscountOffered' class='actions-ico'><img src='../Content/images/New folder/shape-29.png' title='Invoice Journey' class='img-responsive' title='Invoice Journey'/></a>";
                }
            }//index 17
        ],
        columnDefs: [
            {
                targets: [10,11, 12, 13, 14],
                className: "hide_column"
            }
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
           
            $("#TotalDiscInvCount").text(api.rows().count());
            $("#TotalDiscInvAmount").text(api.column('.clsInvoiceAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
        }
    });

    oTable = $('#tbl_DiscountOfferedInvDashboard').DataTable();

    $('#btnDiscInvFilter').click(function () {
        oTable.columns(3).search($('#txt_AnchorName').val().trim());
        oTable.columns(5).search($('#txt_TotalInvAmount').val().trim());
        oTable.draw();
        //GetLastSearchValue();
    });
    $('.searchTerm').on('keyup', function () {
        oTable.columns(3).search($('#txt_AnchorName').val().trim());
        oTable.columns(5).search($('#txt_TotalInvAmount').val().trim());
        oTable.draw();
    });
    $('#ExportDiscInvoiceCSV').click(function () {
        var AnchorName = $('#txt_AnchorName').val().trim();
        var TotalInvAmount = $('#txt_TotalInvAmount').val().trim();
        url = "../Invoice/ExportInvoiceData?AnchorCompanyName=" + AnchorName + "&InvoiceStatus=" + TotalInvAmount;
        window.location.href = url;
    });

   
});



function DownloadInvoice(CompanyId) {

    var AnchorCompName = $('#txt_AnchorName').val().trim();
    var TotalInvAmount = $('#txt_TotalInvAmount').val().trim();
    var CompanyID = CompanyId;
    url = "../Vendor/ExportRecordOfDiscInvoice?AnchorCompanyName=" + AnchorCompName + "&TotalInvAmount=" + TotalInvAmount + "&CompanyId=" + CompanyID;
    window.location.href = url;
}

function GetInvoiceJourney(InvoiceId, data) {
    debugger
    var PageName = data.dataset.para1;
    $("#dvMaintainEligibleInvJourney").html('');
    //$("#DiscInvoiceJourney").empty();
    $.ajax({
        type: "Post",
        dataType: "html",
        url: "../Vendor/GetInvoiceJourneyHistory?InvoiceId=" + InvoiceId + "&PageName=" + PageName,
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            $("#dvMaintainDiscInvJourney").html('');

            var data = JSON.parse(data);
            var PageName = data.pageName;
            var successdata = data.data;

            var htmlRowLastSrch = "";
            if (PageName == "DiscountOffered") {
                for (var i = 0; i < successdata.length; i++) {
                    htmlRowLastSrch = '<div class="cont"><div id="div_' + i + '_' + successdata[i].invoicestatusvalue + '">'
                        + '<div class="row"><div class="col-xs-12 col-sm-8"></div ></div > '
                        + '<div><label>PERSON NAME : <span id="lbl_' + i + '_' + successdata[i].createdUser + '">' + successdata[i].createdUser + '</span></label></div>'
                        + '<div><label>EMAIL ID : <span id="lbl_' + i + '_' + successdata[i].email + '">' + successdata[i].email + '</span></label></div>'
                        + '<div><label>REMARK : <span id="lbl_' + i + '_' + successdata[i].remark + '">' + successdata[i].remark + '</span></label></div>'
                        + '<div><label>SENT TO : <span id="lbl_' + i + '_' + successdata[i].sentToPerson + '">' + successdata[i].sentToPerson + '</span></label></div>'
                        + '</div><div>'
                        + '<div><hr></div>'
                        + '<div class="row"><div class="col-xs-12 col-sm-8"><div><label>INVOICE STATUS : <span id="lbl_' + i + '_' + successdata[i].invoiceStatusText + '">' + successdata[i].invoiceStatusText + '</span></label></div ></div > <div class="col-xs-12 col-sm-4" style="padding: 0;"><div><label>DATE : <span id="lbl_' + i + '_' + successdata[i].createdDate + '">' + successdata[i].createdDate + '</span></label></div></div></div>'
                    $("#dvMaintainDiscInvJourney").append(htmlRowLastSrch);
                }
                $("#CompanyName").text(successdata[0].Company_name);
                $("#lblINVO").text(successdata[0].InvoiceNo)
                $("#lblPOID").text(successdata[0].poNumber);
                $("#lblInvCreateDate").text(successdata[0].invoiceCreatedDate);
                $("#lblDiscOfferedDate").text(successdata[0].createdDate);
                $("#DiscInvoiceJourney").modal('show');
            }
            else if (PageName == "EligibleInvoice") {
                for (var i = 0; i < successdata.length; i++) {
                    var date = new Date(successdata[i].invoiceCreatedDate);
                    var month = date.getMonth() + 1;

                    var date1 = new Date(successdata[i].paymentDueDate);
                    var month1 = date1.getMonth() + 1;

                    htmlRowLastSrch = '<div class="cont"><div id="div_' + i + '_' + successdata[i].invoicestatusvalue + '"><div><p>Invoice Creation Date</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].invoiceCreatedDate + '">' + date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear() + '</label></div>'
                        + '<div><p>Invoice Due  Date</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].paymentDueDate + '">' + date1.getDate().toString() + "/" + (month1 > 9 ? month1 : "0" + month1) + "/" + date1.getFullYear() + '</label></div>'
                        + '<div><p>Invoice Status</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].invoiceStatusText + '">' + successdata[i].invoiceStatusText + '</label></div>'
                        + '</div><div>';
                    $("#dvMaintainEligibleInvJourney").append(htmlRowLastSrch);
                }
                $("#ElgibleInvCompanyName").text(successdata[0].company_name);
                $("#lblElgInvPOID").text(successdata[0].poNumber);
                // $("#lblElgInvCreateDate").text(successdata[0].invoiceCreatedDate);

                $("#EligibleInvoiceJourney").modal('show');
            }

        },
        error: function () {
            alert("Error occured!!")
        }
    });
}