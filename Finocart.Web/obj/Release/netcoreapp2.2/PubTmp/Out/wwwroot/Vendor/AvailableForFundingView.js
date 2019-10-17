
$(document).ready(function () {
    $('#tbl_ViewAvailableforfunding').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetViewAvailableforfunding,
            "type": "POST",
            "datatype": "json",
            "async": false,
            "data": { "id": "" + $("#hdncompany").val() + "", "anchorCompanyName": "" + $("#hdnAnchorcompany")+"" }
        },
        "columns": [
      
            //{
            //    "data": function (data, type, row) {
            //        //index 0
            //        return "<input type='checkbox' class='select-checkbox clsCompanyCheck_" + data.companyID + "'/>";
            //    }
            //},
            { "data": "poNumber", "name": "PONumber", "autoWidth": true, "class": "clsAnchCompanyID" }, //index 1
            /*{ "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true },*/ //index 2   
            {
                "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true, "class": "clsTotalInvoice"

            },
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsTotalCreatedDate","render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },

            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, "class": "clsTotalInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "approvedAmt", "name": "approvedAmt", "autoWidth": true, "class": "clsTotalappInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) },
            {
                "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true, "class": "clsPaymentDueDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }},
            { "data": "Status", "defaultContent": "<i>Eligible for Discounting</i>", "autoWidth": true, "class": "clsInvoiceStatus" }, //index 8
            {
                "data": function (data, type, row) {
                    return "<a href='#' onclick='GetInvoiceJourney(" + data.invoiceID + ",this)' data-para1='EligibleInvoice' class='actions-ico'><img src='../Content/images/New folder/shape-29.png' title='Invoice Journey' class='img-responsive' title='Invoice Journey'/></a>" 
                }//index 14
            },

        ],
        "columnDefs": [{
            "targets": 0,
            "searchable": false,
            "orderable": false,
            "className": "dt-body-center",
         
        },
        {
            "targets": 3,
            "createdCell": function (td, row, data, index) {
                $(td).addClass('clsTotalInvoice_' + data.companyID);
            }
        }
        ],
        "order": [
            [1, "asc"],
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lbTotalInvoices").text(api.rows().count());
            //$("#lbTotalInvoicesAmt").text(api.column('.clsTotalInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            //$("#lbTotalInvoicesAppAmt").text(api.column('.clsTotalappInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            // var TotalRequestedDrawFunds = api.column('.clsTotalAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            //var Total_RequestedDrawFunds = $.fn.dataTable.render.number(',', '').display(TotalRequestedDrawFunds);
            //$("#lblapprovedamount").text(Total_RequestedDrawFunds);

            var TotalInvoiceamount = api.column('.clsTotalInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Invoice_amount = $.fn.dataTable.render.number(',', '').display(TotalInvoiceamount);
            $("#lbTotalInvoicesAmt").text(Total_Invoice_amount);

            var TotalInvoiceAppamount = api.column('.clsTotalappInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Invoice_App_amount = $.fn.dataTable.render.number(',', '').display(TotalInvoiceAppamount);
            $("#lbTotalInvoicesAppAmt").text(Total_Invoice_App_amount); 
        }
    });

    oTable1 = $('#tbl_ViewAvailableforfunding').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(3).search($('#txt_CompanyName').val().trim());
        oTable1.columns(5).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });
    $('#AnchorComp-select-all').on('click', function () {
        // Check/uncheck all checkboxes in the table
        var rows = oTable1.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });


    $('#ExportAnchCompCSV').click(function () {
        
        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalApprovedINV').val().trim();
        var CompanyId = $('#hdncompany').val().trim();
        url = "../Vendor/ExportViewgetAvailableForFunding?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt + "&CompanyId=" + CompanyId;
        window.location.href = url;
    });
   
    $("#lblcompanyName").text($("#hdnAnchorcompany").val());
});

function GetInvoiceJourney(InvoiceId, data) {
    debugger;
    var PageName = "EligibleInvoice";
    $.ajax({
        type: "Post",
        dataType: "html",
        url: "../Vendor/GetInvoiceJourneyHistory?InvoiceId=" + InvoiceId + "&PageName=" + PageName,
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            debugger;
            $("#dvMaintainEligibleInvJourney").html('');

            var data = JSON.parse(data);
            var PageName = data.pageName;
            var successdata = data.data;

            var htmlRowLastSrch = "";
            if (PageName == "EligibleInvoice") {
                for (var i = 0; i < successdata.length; i++) {
                     //convert for payment due Date
                    //var invoicedate = new Date(successdata[i].invoiceCreatedDate);
                    //var invoiceCreatedDate = (invoicedate.getFullYear() + '-' + (invoicedate.getMonth() + 1) + '-' + invoicedate.getDate());
                    var date1 = new Date(successdata[i].invoiceCreatedDate);
                    var month1 = date1.getMonth() + 1;
                    var invoiceCreatedDate = date1.getFullYear() + "-" + (month1 > 9 ? month1 : "0" + month1) + "-" + date1.getDate().toString();
                    //convert for payment due Date
                    //var date = new Date(successdata[i].paymentDueDate);
                    //var paymentDueDate = (date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
                    var date = new Date(successdata[i].paymentDueDate);
                    var month = date.getMonth() + 1;
                    var paymentDueDate = date.getFullYear() + "-" + (month > 9 ? month : "0" + month) + "-" + date.getDate().toString();

                    htmlRowLastSrch = '<div class="cont"><div id="div_' + i + '_' + successdata[i].invoicestatusvalue + '"><div><p>Invoice Creation Date</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].invoiceCreatedDate + '">' + invoiceCreatedDate + '</label></div>'
                        + '<div><p>Invoice Due Date</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].paymentDueDate + '">' + paymentDueDate + '</label></div>'
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

function BackBtn() {
    history.back();
}