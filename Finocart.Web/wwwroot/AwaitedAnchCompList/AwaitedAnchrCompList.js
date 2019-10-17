$(document).ready(function () {
    
    $('#tbl_AwaitedInvoice').dataTable({    
    "processing": true,
    "serverSide": true,
    "searching": true,
        "dom": '"ltipr"',
    "ajax": {
        "url": GetAnchorCompListByVendorID,
        "type": "POST",
        "datatype": "json",
        "async": false
    },
    "columns": [
        { "data": "companyID", "name": "CompanyID", "autoWidth": true, "class": "clsAnchCompanyID", "visible": false, "searchable": false }, //index 0
        { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 1   
        
        {
            "data": "pendingDiscInv", "name": "PendingDiscInv", "autoWidth": true, "class": "clsPendingDiscInv" //index2

        },
        { "data": "awaitTotalInvoiceAmount", "name": "AwaitTotalInvoiceAmount", "autoWidth": true, "class": "clsAwaitTotalInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0)}, //index3
        { "data": "awaitTotalInvoiceAppAmount", "name": "AwaitTotalInvoiceAppAmount", "autoWidth": true, "class": "clsAwaitTotalInvoiceAppAmount", render: $.fn.dataTable.render.number(',', '.', 0) }, //index4

       
     
        {
            "data": function (data, type, row) {

                return "<a class='actions-ico' id='btnView' data-id=" + data.companyID + " data-name=" + data.company_name + "><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
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
        $("#lbAwaitTotalInvoices").text(api.column('.clsPendingDiscInv').data().reduce(function (a, b) { return Number(a) + Number(b); },0));
        var Totalaomunt = api.column('.clsAwaitTotalInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
        var Total_amount = $.fn.dataTable.render.number(',', '').display(Totalaomunt);
        $("#lbAwaitTotalInvoicesAmt").text(Total_amount);       
      }
});

 oTable1 = $('#tbl_AwaitedInvoice').DataTable();
    $('#btnFilter').click(function () {    
        oTable1.columns(1).search($('#txt_AnchorCompanyName').val().trim());
        oTable1.columns(4).search($('#txt_AppInvoiceAmnt').val().trim());
    oTable1.draw();
    });


    $('.searchTerm').on('keyup', function () {
        debugger;
        oTable1.columns(1).search($('#txt_AnchorCompanyName').val().trim());
        oTable1.columns(4).search($('#txt_AppInvoiceAmnt').val().trim());
        oTable1.draw();
    });

    //$('#ExportAwtedAppInvoiceCSV').click(function () {
    
    //    var CompanyName = $('#txt_AnchorCompanyName').val().trim();
    //    var TotalInvoiceAmt = $('#txt_AppInvoiceAmnt').val().trim();
     
    //    url = "../VendorDashboard/ExportDiscAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
    //window.location.href = url;
    //});

    $('#ExportAwtedAppInvoiceCSV').click(function () {

        var CompanyName = $('#txt_AnchorCompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_AppInvoiceAmnt').val().trim();

        url = "../VendorDashboard/ExportDiscAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });

    $('#AnchorComp-select-all').on('click', function () {
        // Check/uncheck all checkboxes in the table
        var rows = oTable1.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });


    $('#pagechange').change(function () {
        
        var newTFvalue = $("#pagechange").val();
        if (newTFvalue == "1") {
            url = "../VendorDashboard/InvoicesAwaitedApprovals";
            window.location.href = url;
        }
        else if (newTFvalue == "2") {
            url = "../Vendor/GetReceivableDue";
           window.location.href = url;
        }
        else if (newTFvalue == "3") {
            url = "../Vendor/InvoiceListBytodaysdate";
            window.location.href = url;
          
        }
        else if (newTFvalue == "4") {
            url = "../Vendor/Availableforfunding";
            window.location.href = url;

        }
        else if (newTFvalue == "5") {
            url = "../Vendor/InvoiceHistory";
            window.location.href = url;
        }



    });
})

$(document).on("click", "#btnView", function (e) {
    debugger;
    var $buttonClicked = $(this);
    var CompanyID = $buttonClicked.attr('data-id');
    var CompanyName = $buttonClicked.attr('data-name');
    url = "../VendorDashboard/GetviewAvaitInvoiceApproval?id=" + btoa(CompanyID) + "&AnchorCompanyName=" + btoa(CompanyName);
    window.location.href = url;
});

function GetInvoice(id) {
    var $buttonClicked = $(this);
    var CompanyName = $buttonClicked.attr('data-name');
    url = "../VendorDashboard/GetviewAvaitInvoiceApproval?id=" + btoa(id) + "&AnchorCompanyName=" + CompanyName;
    window.location.href = url;

}



