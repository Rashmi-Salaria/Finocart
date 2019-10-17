$(document).ready(function () {

    $('#tbl_InvoiceHistory').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        
        "ajax": {
            "url": GetInvoiceHistory,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [

            //{
            //    "data": function (data, type, row) {
            //        //index 0
            //        return "<input type='checkbox' class='select-checkbox clsCompanyCheck_" + data.companyID + "'/>";
            //    }
            //},
            /*{ "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true },*/ //index 2 
            { "data": "poNumber", "name": "PONumber", "autoWidth": true, "class": "clsPONumber" },
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true, "class": "clsTotalInvoice" },
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsTotalInvoice", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, "class": "clsinvoiceAmt", render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "approvedAmt", "name": "approvedAmt", "autoWidth": true, "class": "clsapprovedAmt", render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "discount", "name": "Discount", "autoWidth": true, "class": "clsDiscount" }, 
            {
                "data": "discountDate", "name": "DiscountDate", "autoWidth": true, "class": "clsDiscountDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            }, 
            //{ "data": "invoiceStatus", "name": "InvoiceStatus", "autoWidth": true, "class": "clsDiscount" }, 
            {
                "data": function (data, type, row) {
                    if (data.invoiceStatus == "Rejected") {
                        return "<td class='clsDiscount'><span class='red'>Rejected</span></td>";
                    }
                    if (data.invoiceStatus == "Approved") {
                        return "<td class='clsDiscount'><span class='green'>Approved</span></td>";
                    }
                    if (data.invoiceStatus == "Pending") {
                        return "<td class='clsDiscount'><span class='yellow'>Pending</span></td>";
                    }
                }},
            {
                "data": "invoiceApprovalDate", "name": "InvoiceApprovalDate", "autoWidth": true, "class": "clsDiscountDate", "format": "dd/MM/YYYY"
            }, 
            { "data": "disbursementAmt", "name": "DisbursementAmt", "autoWidth": true, "class": "clsDiscountDate", render: $.fn.dataTable.render.number(',', '.', 0) }, 
            { "data": "discountAmt", "name": "DiscountAmt", "autoWidth": true, "class": "clsDiscountDate", render: $.fn.dataTable.render.number(',', '.', 0) }, 
            //{ "data": "discountBucketBucketName", "name": "DiscountBucketBucketName", "autoWidth": true, "class": "clsDiscountDate" }, 
            { "data": "utrDetails", "name": "UTRDetails", "autoWidth": true, "class": "clsDiscountDate" },
            { "data": "finoLimUnLim", "name": "FinoLimUnLim", "autoWidth": true, "class": "clsDiscountDate" }
                
           

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

        
            var TotalInvoiceAmt = api.column('.clsinvoiceAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Invoice_Amt = $.fn.dataTable.render.number(',', '').display(TotalInvoiceAmt);
            $("#lbTotalInvoicesAmt").text(Total_Invoice_Amt);

            var TotalInvoiceAppAmt = api.column('.clsapprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Invoice_App_Amt = $.fn.dataTable.render.number(',', '').display(TotalInvoiceAppAmt);
            $("#lbTotalInvoicesAppAmt").text(Total_Invoice_App_Amt);
            
        }
    });

    oTable1 = $('#tbl_InvoiceHistory').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(2).search($('#txt_CompanyName').val().trim());
        oTable1.columns(7).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });
    $('.searchTerm').on('keyup', function () {
        oTable1.columns(2).search($('#txt_CompanyName').val().trim());
        oTable1.columns(7).search($('#txt_TotalApprovedINV').val().trim());
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
        url = "../Vendor/ExportInvoiceHistory?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
        alert(1);
    });


    // for dropdown page selection
    $('#pagechange5').change(function () {
        
        var newTFvalue = $("#pagechange5").val();
        if (newTFvalue == "1") {
         url = "../Vendor/InvoiceHistory";
            window.location.href = url;
        }
        else if (newTFvalue == "2") {
         url = "../Vendor/GetReceivableDue";
            window.location.href = url;
        }
        else if (newTFvalue == "3") {
            url = "../VendorDashboard/InvoicesAwaitedApprovals";
            window.location.href = url;
        }
        else if (newTFvalue == "4") {
        url = "../Vendor/InvoiceListBytodaysdate";
         window.location.href = url;
        }
        else if (newTFvalue == "5") {
            url = "../Vendor/Availableforfunding";
            window.location.href = url;
        }

    });

});