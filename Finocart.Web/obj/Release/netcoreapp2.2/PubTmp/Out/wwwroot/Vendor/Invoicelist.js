
$(document).ready(function () {
    
    $('#tbl_InvoiceListByPO').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": "../Vendor/getInvoiceListByPOIdtodayes",
            "type": "POST",
            "datatype": "json"
         
        },
        "columns": [
       
            { "data": "companyId", "name": "CompanyId", "autoWidth": true, "visible": false, "searchable": false}, //index 0
            { "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true, "class": "clsAnchorCompName"}, //index 1
            { "data": "poNumber", "name": "PONumber", "autoWidth": true }, //index 3
            { "data": "invoiceID", "name": "InvoiceID", "autoWidth": true, "class": "clsInvoiceID" }, //index 4
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "format": "dd/MM/YYYY"}, //index 4
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0), "class": "clsInvoiceAmt"}, //index 4
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0), "class": "clsApprovedAmt" }, //index 4
            {
                "data": "bucketCreatedDate", "name": "BucketCreatedDate", "autoWidth": true, "format": "dd/MM/YYYY"}, //index 4
            { "data": "discount", "name": "Discount", "autoWidth": true }, //index 4
            { "data": "discountAmt", "name": "DiscountAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }, //index 4
            { "data": "disbursementAmt", "name": "DisbursementAmt", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0)}, //index 4
            {
                "data": "paymentDate", "name": "PaymentDate", "autoWidth": true, "format": "dd/MM/YYYY"}, //index 4
            
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lbTotalInvoices").text(api.column('.clsInvoiceID').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));

            $("#lbTotalSelectInvoices").text(api.column('.clsInvoiceID').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));

            var TotalInvoiceAmount = api.column('.clsInvoiceAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Invoice_Amount = $.fn.dataTable.render.number(',', '').display(TotalInvoiceAmount);
            $("#lbTotalInvoicesAmt").text(Total_Invoice_Amount);

            var TotalInvoiceAppAmt = api.column('.clsApprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Invoice_App_Amt = $.fn.dataTable.render.number(',', '').display(TotalInvoiceAppAmt);
            $("#lbTotalInvoicesAppAmt").text(Total_Invoice_App_Amt);

            var TotalSelectedInvoiceAmount = api.column('.clsInvoiceAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Selected_Invoice_Amount = $.fn.dataTable.render.number(',', '').display(TotalSelectedInvoiceAmount);
            $("#lbTotalSelectInvoicesAmt").text(Total_Selected_Invoice_Amount);

            var TotalSelectedInvoiceAppAmt = api.column('.clsApprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Selected_Invoice_App_Amt = $.fn.dataTable.render.number(',', '').display(TotalSelectedInvoiceAppAmt);
            $("#lbTotalSelectInvoicesAppAmt").text(Total_Selected_Invoice_App_Amt);
        }
    });

    $('#ExportAnchCompCSV').click(function () {
        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalApprovedINV').val().trim();
        url = "../Vendor/ExportgetInvoiceListByPOIdtodayes?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });

    oTable1 = $('#tbl_InvoiceListByPO').DataTable();
    $('#btnInvoiceFilter').click(function () {
        
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(6).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });
    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(6).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });

    //created dropdown selection 
    $('#pagechange4').change(function () {
        
        var newTFvalue = $("#pagechange4").val();
        if (newTFvalue == "1") {

            url = "../Vendor/InvoiceListBytodaysdate";
            window.location.href = url;
        }
        else if (newTFvalue == "2") {

            url = "../Vendor/InvoiceHistory";
            window.location.href = url;
        }
        else if (newTFvalue == "3") {
            url = "../Vendor/GetReceivableDue";
            window.location.href = url;
        }
        else if (newTFvalue == "4") {

            url = "../VendorDashboard/InvoicesAwaitedApprovals";
            window.location.href = url;
        }
        else if (newTFvalue == "5") {
            url = "../Vendor/Availableforfunding";
            window.location.href = url;
        }

    });
});