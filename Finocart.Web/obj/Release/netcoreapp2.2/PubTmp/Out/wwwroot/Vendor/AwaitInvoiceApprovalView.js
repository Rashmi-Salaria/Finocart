
$(document).ready(function () {
    $('#tbl_ViewAvailableforfunding').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetViewAvailableforfunding,
            "data": { fundid: $("#hdncompany").val() },
            "type": "POST",
            "datatype": "json",
            "async": false,
            
        },
        "columns": [

            //{
            //    "data": function (data, type, row) {
            //        //index 0
            //        return "<input type='checkbox' class='select-checkbox clsCompanyCheck_" + data.companyID + "'/>";
            //    }
            //},
            { "data": "poNumber", "name": "PONumber", "autoWidth": true, "class": "clsAnchCompanyID" }, //index 1
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true, "class": "clsTotalInvoice" },
            //{ "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true }, //index 2   

            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsTotalCreatedDate", "format": "dd/MM/YYYY"
                //"render": function (data) {
                //    var date = new Date(data);
                //    var month = date.getMonth() + 1;
                //    return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                //}
                },

            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, "class": "clsTotalInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "approvedAmt", "name": "approvedAmt", "autoWidth": true, "class": "clsTotalInvoiceAmount1", render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "discount", "name": "Discount", "autoWidth": true, "class": "clsDiscount" },
            //{
            //    "data": "validUptoDate", "name": "ValidUptoDate", "autoWidth": true, "class": "clsValidUptoDate", "render": function (data) {
            //        var date = new Date(data);
            //        var month = date.getMonth() + 1;
            //        return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
            //    } },
            {
                "data": "bucketCreatedDate", "name": "BucketCreatedDate", "autoWidth": true, "class": "clsbucketCreatedDate", "format": "dd/MM/YYYY"
                    //function (data) {
                    //var date = new Date(data);
                    //var month = date.getMonth() + 1;
                    //return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                    //}
            },
            {
                "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true, "class": "clsPaymentDueDate", "format": "dd/MM/YYYY"
                //"render": function (data) {
                //    var date = new Date(data);
                //    var month = date.getMonth() + 1;
                //    return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                //}
            },
            { "data": "Status", "defaultContent": "<i>Pending @ Anchor Company</i>", "autoWidth": true, "class": "clsInvoiceStatus" }, //index 8
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
            //$("#lbTotalInvoicesAppAmt1").text(api.column('.clsTotalInvoiceAmount1').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            var Totalaomunt = api.column('.clsTotalInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_amount = $.fn.dataTable.render.number(',', '').display(Totalaomunt);
            $("#lbTotalInvoicesAmt").text(Total_amount); 
        }
    });

    oTable1 = $('#tbl_ViewAvailableforfunding').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(3).search($('#txt_CompanyName').val().trim());
        oTable1.columns(5).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });


    $('#ExportAnchCompCSV').click(function () {
        
        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalApprovedINV').val().trim();
        var CompanyId = $('#hdncompany').val().trim();
        url = "../VendorDashboard/ExportViewgetAvailableForFunding?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt + "&CompanyId=" + CompanyId;
        window.location.href = url;
    });

    $('#AnchorComp-select-all').on('click', function () {
        // Check/uncheck all checkboxes in the table
        var rows = oTable1.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });


   
 


});

function GetInvoiceJourney(InvoiceId, data) {
    var PageName = "EligibleInvoice";
    $.ajax({
        type: "Post",
        dataType: "html",
        url: "../VendorDashboard/GetInvoiceJourneyHistory?InvoiceId=" + InvoiceId + "&PageName=" + PageName,
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            $("#dvMaintainEligibleInvJourney").html('');

            var data = JSON.parse(data);
            var PageName = data.pageName;
            var successdata = data.data;            
            var htmlRowLastSrch = "";
            if (PageName == "EligibleInvoice") {
                for (var i = 0; i < successdata.length; i++) {
                    var paymentDueDate = new Date(successdata[i].paymentDueDate);
                    var paymentDueMonth = paymentDueDate.getMonth() + 1;
                    successdata[i].paymentDueDate = paymentDueDate.getDate().toString() + "/" + (paymentDueMonth.length > 1 ? paymentDueMonth : "" + paymentDueMonth) + "/" + paymentDueDate.getFullYear();
                    var invoiceCreatedDate = new Date(successdata[i].invoiceCreatedDate);
                    var invoiceCreatedMonth = paymentDueDate.getMonth() + 1;
                    successdata[i].invoiceCreatedDate = invoiceCreatedDate.getDate().toString() + "/" + (invoiceCreatedMonth.length > 1 ? invoiceCreatedMonth : "" + invoiceCreatedMonth) + "/" + invoiceCreatedDate.getFullYear();
                    htmlRowLastSrch = '<div class="cont"><div id="div_' + i + '_' + successdata[i].invoicestatusvalue + '"><div><p>Invoice Creation Date</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].invoiceCreatedDate + '">' + successdata[i].invoiceCreatedDate + '</label></div>'
                        + '<div><p>Invoice Due Date</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].paymentDueDate + '">' + successdata[i].paymentDueDate + '</span></label></div>'
                        + '<div><p>Invoice Status</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].invoiceStatusText + '">' + successdata[i].invoiceStatusText + '</span></label></div>'
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