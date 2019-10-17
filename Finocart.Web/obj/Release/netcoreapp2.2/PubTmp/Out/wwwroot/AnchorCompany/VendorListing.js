
$(document).ready(function () {
    $('#tbl_VendorList').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',

        "ajax": {
            "url": "GetVendorListing",
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        
        "columns": [
            { "data": "vendorId", "name": "VendorId", "autoWidth": true, "class": "clsVendorId", "visible": false, "searchable": false }, //index 1
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2   
            { "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true, "class": "clsContact_mobile" },
            { "data": "contact_email", "name": "Contact_email", "autoWidth": true, "class": "clsContact_email" },
            //{ "data": "invoiceLimitAmt", "name": "InvoiceLimitAmt", "autoWidth": true, "class": "clsInvoiceLimitAmt" },
            //{ "data": "invAmtLimitStatus", "name": "InvAmtLimitStatus", "autoWidth": true, "class": "clsInvAmtLimitStatus" },
            //{ "data": "invAmtLimitStatusValues", "name": "invAmtLimitStatusValues", "autoWidth": true },

            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },
            {
                "data": function (data, type, row) {
                    return "<a href='../AnchorCompany/UploadVendorListing/?VendorId=" + btoa(data.vendorId) + "&VendorCompany=" + btoa(data.company_name) + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }
            

        ],
        
    });

    oTable1 = $('#tbl_VendorList').DataTable();

    $('#btnInvoiceFilter').click(function () {

        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(3).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.columns(2).search($('#txt_CompanyName').val().trim());
        oTable1.columns(3).search($('#txt_CompanyName').val().trim());
        oTable1.columns(4).search($('#txt_CompanyName').val().trim());
        oTable1.draw();
    });

    $("#ExcelVendorTemplate").click(function () {
        url = "../AnchorCompany/DownloadTemplate?Tempate=Vendor";
        window.location.href = url;

    });

    $('#btnUpload').on('click', function () {
        debugger;
        var fileExtension = ['xls', 'xlsx'];
        var filename = $('#fUpload').val();
        if (filename.length == 0) {
            alert("Please select a file.");
            return false;
        }
        else {
            var extension = filename.replace(/^.*\./, '');
            if ($.inArray(extension, fileExtension) == -1) {
                alert("Please select only excel files.");
                return false;
            }
        }
        var fdata = new FormData();
        var fileUpload = $("#fUpload").get(0);
        var files = fileUpload.files;
        var FileName =
            fdata.append(files[0].name, files[0]);
        $.ajax({
            type: "POST",
            url: "../AnchorCompany/UploadVendors",
            data: fdata,
            contentType: false,
            processData: false,
            success: function (response) {
                debugger;
                alert("File Uploaded Successfully");
                $("#fUpload").val('');
                //var url = "../AnchorCompany/GetLog?Log=Vendor";
                //window.location.href = url;
            },
            error: function (e) {

            }
        });
    });

    $('#ExportAnchCompCSV').click(function () {

        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalApprovedINV').val().trim();
        url = "../AnchorCompany/ExportVendorListToCSV?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });

    $('#btnUploadInv').on('click', function () {
        debugger;
        var fileExtension = ['xls', 'xlsx'];
        var filename = $('#fUploadInv').val();
        if (filename.length == 0) {
            alert("Please select a file.");
            return false;
        }
        else {
            var extension = filename.replace(/^.*\./, '');
            if ($.inArray(extension, fileExtension) == -1) {
                alert("Please select only excel files.");
                return false;
            }
        }
        var fdata = new FormData();
        var fileUpload = $("#fUploadInv").get(0);
        var files = fileUpload.files;
        fdata.append(files[0].name, files[0]);
        $.ajax({
            type: "POST",
            url: "../AnchorCompany/UploadInvoice",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (response) {
                alert("File Uploaded Successfully");
                $("#fUploadInv").val('');
                //var url = "../AnchorCompany/GetLog?Log=Invoice";
                //window.location.href = url;
            },
            error: function (e) {
                $('#dvData').html(e.responseText);
            }
        });
    });
});

function InvoiceExcelTemplate() {
    url = "../AnchorCompany/DownloadTemplate?Tempate=Invoice";
    window.location.href = url;
}

function InvoiceUpload(Id, Email) {
    debugger;
    var fileExtension = ['xls', 'xlsx'];
    var filename = $('#fUpload' + Id).val();
    if (filename.length == 0) {
        alert("Please select a file.");
        return false;
    }
    else {
        var extension = filename.replace(/^.*\./, '');
        if ($.inArray(extension, fileExtension) == -1) {
            alert("Please select only excel files.");
            return false;
        }
    }
    var fdata = new FormData();
    var fileUpload = $("#fUpload" + Id).get(0);
    var files = fileUpload.files;
    fdata.append(files[0].name, files[0]);
    $.ajax({
        type: "POST",
        url: "../AnchorCompany/UploadInvoice?VendorID=" + Id + "&contact_email=" + Email,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        contentType: false,
        processData: false,
        success: function (response) {
            var url = "../AnchorCompany/GetLog?Log=Invoice";
            window.location.href = url;
        },
        error: function (e) {
            $('#dvData').html(e.responseText);
        }
    });
}


