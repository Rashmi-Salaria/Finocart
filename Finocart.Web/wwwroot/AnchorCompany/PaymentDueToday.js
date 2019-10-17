var InvoiceID = []; var CompInvID = []; var AnchCompanyID = []; var TotalSelectedInvAmt = 0; var TotalInvoice = 0;
$(document).ready(function () {
    debugger;
    $('#tbl_VendorPaymentDueList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetPaymentDueTodayList,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "vendorID", "name": "VendorID", "autoWidth": true, "visible": false, "searchable": false}, //index 1
            { "data": "vendorName", "name": "VendorName", "autoWidth": true }, //index 2   
            { "data": "noOfInvoices", "name": "NoOfInvoices", "autoWidth": true, "class": "clsTotalInvoiceNo"},
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, "class": "clsTotalInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "totalInvoiceAppAmt", "name": "TotalInvoiceAppAmt", "autoWidth": true, "class": "clsApprovedAmt",render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "amountPaymenttoday", "name": "AmountPaymenttoday", "autoWidth": true, "class": "clsTotalInvoiceAmountPaytoday",render: $.fn.dataTable.render.number(',', '.', 0)},
            { "data": "earning", "name": "Earning", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            {
                "data": function (data, type, row) {
                    return "<a href='../AnchorCompDashboard/PaymentDueTodayView/?VendorID=" + data.vendorID + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }

        ],  
        "columnDefs": [{
            "targets": 0,
            "searchable": false,
            "orderable": false,
            "className": "dt-body-center",
            //"render": function (data, type, full, meta) {
            //    return '<input type="checkbox" name="id[]" value="'
            //        + $('<div/>').text(data).html() + '">';
            //}
        },
        {
            "targets": 3,
            "createdCell": function (td, row, data, index) {
               

                var APPROVED_AMOUNT = $.fn.dataTable.render.number(',', '', 0, '').display(data.totalInvoiceAppAmt);
                $("#lblapprovedamount").text(APPROVED_AMOUNT);
                //alert(APPROVED_AMOUNT);

                var AmountPayableToday = $.fn.dataTable.render.number(',', '', 0, '').display(data.amountPaymenttoday);
                $("#lblamountpayabletoday").text(AmountPayableToday);
                //alert(AmountPayableToday);


                $(td).addClass('clsTotalInvoice_' + data.anchorID);
            }
        }
        ],
        "order": [
            [1, "asc"],
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lblapprovedinvices").text(api.column('.clsTotalInvoiceNo').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));

            var TotalAppAmt = api.column('.clsApprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_App_Amt = $.fn.dataTable.render.number(',', '').display(TotalAppAmt);
            $("#lblapprovedamount").text(Total_App_Amt);
          
            var TotalInvoiceAmtPayToday = api.column('.clsTotalInvoiceAmountPaytoday').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Invoice_AmtPayToday = $.fn.dataTable.render.number(',', '').display(TotalInvoiceAmtPayToday);
            $("#lblamountpayabletoday").text(Total_Invoice_AmtPayToday);

            //$("#lblapprovedamount").text(api.column('.clsApprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
           // $("#lblamountpayabletoday").text(api.column('.clsTotalInvoiceAmountPaytoday').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
        }
    });

    oTable1 = $('#tbl_VendorPaymentDueList').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(3).search($('#txt_TotalInvoiceAmt').val().trim());
        oTable1.draw();
    });
    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(3).search($('#txt_TotalInvoiceAmt').val().trim());
        oTable1.draw();
    });
    $('#ExportAnchCompCSV').click(function ()
    {
        var VendorName = $('#txt_VendorName') || '';
        var TotalAppInvAmt = $('#txt_TotalAppInvAmt') || '';
        url = "../AnchorCompDashboard/ExportPaymentDueTodayListToCSV?VendorName=" + VendorName + "&TotalAppInvAmt=" + TotalAppInvAmt;
        window.location.href = url;
    });
})
