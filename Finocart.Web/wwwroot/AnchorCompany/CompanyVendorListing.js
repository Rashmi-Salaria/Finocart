$(document).ready(function () {

    $('#tbl_CompanyVendorList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',

        "ajax": {
            "url": GetVendorListing,
            "data": { CompanyID: $("#hdnCompanyID").val(), CompanyName: $("#hdnCompanyName1").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            /*{ "data": "anchorName", "name": "AnchorName", "autoWidth": true }, *///index 2
            { "data": "vendorId", "name": "VendorId", "autoWidth": true, "class": "clsVendorId", "visible": false, "searchable": false }, //index 1
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2   
            { "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true, "class": "clsContact_mobile" },
            { "data": "contact_email", "name": "Contact_email", "autoWidth": true, "class": "clsContact_email" },
            { "data": "invoiceLimitAmt", "name": "InvoiceLimitAmt", "autoWidth": true, "class": "clsInvoiceLimitAmt" },
            { "data": "invAmtLimitStatusValues", "name": "InvAmtLimitStatusValues", "autoWidth": true, "class": "clsInvAmtLimitStatus" },
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },
            //{
            //    "data": function (data, type, row) {
            //        return "<a href='../AnchorCompany/UploadVendorListing/?VendorId=" + btoa(data.vendorId) + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
            //    }
            //}

        ],

    });

    oTable2 = $('#tbl_CompanyVendorList').DataTable();

    //$('#btnInvoiceFilter').click(function () {

    //    oTable1.columns(1).search($('#txt_CompanyName').val().trim());
    //    oTable1.columns(3).search($('#txt_TotalApprovedINV').val().trim());
    //    oTable1.draw();
    //});

    $('.searchTerm').on('keyup', function () {
        oTable2.columns(1).search($('#txt_Search').val().trim());
        //oTable1.columns(3).search($('#txt_TotalApprovedINV').val().trim());
        oTable2.draw();
    });

    //$("#ExportAnchCompCSVVendor").click(function () {
    //    url = "/AnchorCompany/DownloadTemplate?Tempate=Vendor";
    //    window.location.href = url;

    //});
    //$("#ExportAnchCompCSVAnchor").click(function () {
    //    url = "/AnchorCompany/DownloadTemplate?Tempate=Anchor";
    //    window.location.href = url;

    //});
    $("#ExportAnchCompCSV").click(function () {
        url = "../ExportVendorCompanyToCSV?CompanyIDD=" + $("#hdnCompanyID").val() + "&companyName" + $('#txt_CompanyName').val().trim();
        window.location.href = url;
    });
   
    $('#lblcompanyName').text($("#hdnCompanyName").val());
    $('#lblcompanyName1').text($("#hdnCompanyName1").val());

    $('#tbl_CompanyBankViewList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetCompanyBank,
            "data": { CompanyID: $("#hdnCompanyID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "anchCompName", "name": "AnchCompName", "autoWidth": true },
            { "data": "bankName", "name": "BankName", "autoWidth": true },
            { "data": "availableLimits", "name": "AvailableLimits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            //{ "data": "utilizedLimits", "name": "UtilizedLimits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "interestrate", "name": "Interestrate", "autoWidth": true },
            //{
            //    "data": "validityUpto", "name": "ValidityUpto", "autoWidth": true, "render": function (data) {
            //        var date = new Date(data);
            //        var month = date.getMonth() + 1;
            //        return date.getDate().toString() + "/" + (month> 9 ? month : "0" + month) + "/" + date.getFullYear();
            //    }
            //},
            { "data": "drawFunds", "name": "DrawFunds", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) }
        ],
    });

    oTable3 = $('#tbl_CompanyBankViewList').DataTable();
    //$('#btnComapnyFilter').click(function () {
    //    oTable1.search($('#txt_CompanyNameSearch').val().trim());
    //    //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
    //    oTable1.draw();
    //});
    $('#ExportAnchCompCSV').click(function () {
        var Search = $('#txt_CompanyNameSearch').val().trim();
        url = "../ExportCompanyBankViewToCSV?Search=" + Search + "&CompanyID=" + $("#hdnCompanyID").val();
        window.location.href = url;

    });

    $('.searchTerm').on('keyup', function () {

        oTable3.columns(0).search($('#txt_CompanyNameSearch').val().trim());
        //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
        oTable3.draw();
    });
    
});