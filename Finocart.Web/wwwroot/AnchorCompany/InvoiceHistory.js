$(document).ready(function () {
    $('#tbl_InvoiceStatus').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        //"scrollX": "100%",
        "ajax": {
            "url": GetInvoiceHistory,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [

            { "data": "poNumber", "name": "PONumber", "class": "clsPONumber" },
            { "data": "invoiceNo", "name": "InvoiceNo", "class": "clsTotalInvoice" },
            {
                "data": "createdDate", "name": "CreatedDate", "class": "clsTotalInvoice", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }},
            { "data": "invoiceAmt", "name": "InvoiceAmt", "class": "clsinvoiceAmt", render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "invoiceApprovedAmount", "name": "InvoiceApprovedAmount", "class": "clsapprovedAmt", render: $.fn.dataTable.render.number(',', '.', 0) },
          
            {
                "data": function (data, type, row) {
                    if (data.status == "Rejected") {
                        return "<td class='clsStatusRejected'><span class='red'>Rejected</span></td>";
                    }
                    if (data.status == "Approved") {
                        return "<td class='clsStatusApproved'><span class='green'>Approved</span></td>";
                    }
                    if (data.status == "Pending") {
                        return "<td class='clsStatusPending'><span class='yellow'>Pending</span></td>";
                    }
                }
            },
            //{ "data": "discountBucketBucketName", "name": "DiscountBucketBucketName", "autoWidth": true, "class": "clsDiscountBucketName" },
            { "data": "amountPaid", "name": "AmountPaid", "class": "clsAmountPaid",render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "earnings", "name": "Earning", "class": "clsEarning", render: $.fn.dataTable.render.number(',', '.', 0) },
            {
                "data": "paymentDate", "name": "PaymentDate", "class": "clsPaymentDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                } },
            { "data": "utrDetails", "name": "UTRDetails", "class": "clsUTRDetails" },
            {
                "data": "discountDate", "name": "DiscountDate", "class": "clsDiscountDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                } },
            { "data": "finoLimUnLim", "name": "FinoLimUnLim", "class": "clsDiscountDate" },
            { "data": "limits", "name": "Limits", "class": "clsLimits", render: $.fn.dataTable.render.number(',', '.', 0) },
            {
                "data": function (data, type, row) {
                    var date = new Date(data.paymentDate);
                    //var month = date.getMonth() + 1;
                    //date = date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                    var date1 = new Date();
                    date1 = new Date(date1.getFullYear(), date1.getMonth(), date1.getDate());
                    //var month1 = date1.getMonth() + 1;
                    //date1 = date1.getDate().toString() + "/" + (month1.length > 1 ? month1 : "0" + month1) + "/" + date1.getFullYear();
                    if (data.status == "Approved" && date1 >= date) {
                        return "<a href='#' id='btnModify' class='actions-ico' data-id=" + data.invoiceID + " data-UTRDetails=" + data.utrDetails + " data-toggle='modal' data-target='#InvoiceUTRDetails'><img  src='../Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                    }
                    else {
                        return null;
                    }
                }
            }
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

            $("#lbTotalInvoicesAmt").text(api.column('.clsinvoiceAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
             var Remaining_bal = $.fn.dataTable.render.number(',', '', 0, '').display($("#lbTotalInvoicesAmt").text());
            $("#lbTotalInvoicesAmt").text(Remaining_bal);
           

            $("#lbTotalInvoicesAppAmt").text(api.column('.clsapprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            var TotalInvoicesAppAmt = $.fn.dataTable.render.number(',', '', 0, '').display($("#lbTotalInvoicesAppAmt").text());
            $("#lbTotalInvoicesAppAmt").text(TotalInvoicesAppAmt);


            $("#lbTotalSelectInvoices").text(api.column('.clsStatusApprovedStatus').rows().count());
            //$("#lbTotalSelectInvoices").text(api.rows().count());
            $("#lbTotalPaidAmt").text(api.column('.clsAmountPaid').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            var TotalPaidAmt = $.fn.dataTable.render.number(',', '', 0, '').display($("#lbTotalPaidAmt").text());
            $("#lbTotalPaidAmt").text(TotalPaidAmt);

            $("#lbTotalEarningAmt").text(api.column('.clsEarning').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            var TotalEarningAmt = $.fn.dataTable.render.number(',', '', 0, '').display($("#lbTotalEarningAmt").text());
            $("#lbTotalEarningAmt").text(TotalEarningAmt);
        }
    });

    oTable1 = $('#tbl_InvoiceStatus').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(2).search($('#txt_VendorName').val().trim());
        oTable1.columns(7).search($('#txt_InvoiceApprovedAmount').val().trim());
        oTable1.draw();
    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(2).search($('#txt_VendorName').val().trim());
        oTable1.columns(7).search($('#txt_InvoiceApprovedAmount').val().trim());
        oTable1.draw();
    });

    $('#AnchorComp-select-all').on('click', function () {
        // Check/uncheck all checkboxes in the table
        var rows = oTable1.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });


    $('#ExportAnchCompCSV').click(function () {
        //alert(1);
        //var CompanyName = $('#txt_CompanyName').val().trim();
        //var TotalInvoiceAmt = $('#txt_TotalApprovedINV').val().trim();
        //url = "../Vendor/ExportInvoiceHistory?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        //window.location.href = url;
        url = "../Vendor/ExportInvoiceHistory?CompanyName=";
        window.location.href = url;
    });

    $("#btnSubmit").click(function (e) {
        debugger
        //var cheklimit = '../BankCompany/GetAvailableLimit';

        //var $buttonClicked = $(this);
        var InvoiceID = $('#InvoiceID').val()
        var UTRDetails = $('#UTRDetails').val();

        if (UTRDetails === "") {
            $('#lblAvailabeLimit').show();
            return false;
        }
        $.ajax({

            url: '../AnchorCompDashboard/UpdateUTRDetails',
            type: "POST",
            data: { InvoiceID: InvoiceID, UTRDetails: UTRDetails },
            success: function (data) {
                url = "../AnchorCompDashboard/InvoiceHistory";
                window.location.href = url;

            },
        });
    });

    $("#btnModify").click(function (e) {
        var $buttonClicked = $(this);
        $("#InvoiceID").val($buttonClicked.attr('data-id'));
        $("#UTRDetails").val($buttonClicked.attr('data-UTRDetails'));
    });
});