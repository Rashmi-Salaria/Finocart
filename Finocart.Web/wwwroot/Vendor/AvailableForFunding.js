
$(document).ready(function () {

    $('#tbl_Availableforfunding').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetAvailableforfunding,
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
            { "data": "companyID", "name": "CompanyID", "autoWidth": true, "class": "clsAnchCompanyID", "visible": false, "searchable": false}, //index 1
            { "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true }, //index 2   
            {
                "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true, "class": "clsTotalInvoice"

            },
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, "class": "clsTotalInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "approvedAmt", "name": "approvedAmt", "autoWidth": true, "class": "clsTotalappInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) },
            {
                "data": function (data, type, row) {
                    return "<a onclick=GetInvoice(" + data.companyID + ",'" + data.anchorCompName +"') class='actions - ico getInvoice clsEligibleInv' > <img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a > ";
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
                $(td).addClass('clsTotalInvoice_' + data.companyID);
            }
        }
        ],
        "order": [
            [1, "asc"],
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lbTotalInvoices").text(api.column('.clsTotalInvoice').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));

            var TotalInvoiceAmt = api.column('.clsTotalInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_Invoice_Amt = $.fn.dataTable.render.number(',', '').display(TotalInvoiceAmt);
            $("#lbTotalInvoicesAmt").text(Total_Invoice_Amt);

            var TotalAppInvoiceAmt = api.column('.clsTotalappInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_App_Invoice_Amt = $.fn.dataTable.render.number(',', '').display(TotalAppInvoiceAmt);
            $("#lbTotalInvoicesAppAmt").text(Total_App_Invoice_Amt);
        }
    });

    oTable1 = $('#tbl_Availableforfunding').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(3).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(3).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });

    $('#ExportAnchCompCSV').click(function () {
        
        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalApprovedINV').val().trim();
        url = "../Vendor/ExportgetAvailableForFunding?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });




   
    $('#AnchorComp-select-all').on('click', function () {
        // Check/uncheck all checkboxes in the table
        var rows = oTable1.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    $('#pagechange2').change(function () {
        
        var newTFvalue = $("#pagechange2").val();
        if (newTFvalue == "1") {

            url = "../Vendor/Availableforfunding";
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
            url = "../Vendor/InvoiceHistory";
            window.location.href = url;
        }

    });



 
});

function GetInvoice(id,name) {

    debugger;
    var PageName = "";

    url = "../Vendor/GetviewAvailableforfunding?id=" + id + "&companyName="+name;
    window.location.href = url;
   
}