$(document).ready(function () {

    $('#tbl_VendorAnchorList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',

        "ajax": {
            "url": GetAnchorListing,
            "data": { VendorId: $("#hdnCompanyID").val(), AnchorName: $("#hdnCompanyName").val()  },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            /*{ "data": "vendorName", "name": "VendorName", "autoWidth": true },*/ //index 2 
            { "data": "companyId", "name": "CompanyId", "autoWidth": true, "class": "clsVendorId", "visible": false, "searchable": false }, //index 1
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2   
            { "data": "contact_Name", "name": "Contact_Name", "autoWidth": true }, //index 2   
            { "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true, "class": "clsContact_mobile" },
            { "data": "contact_email", "name": "Contact_email", "autoWidth": true, "class": "clsContact_email" },
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

    oTable1 = $('#tbl_VendorAnchorList').DataTable();

    //$('#btnInvoiceFilter').click(function () {

    //    oTable1.columns(1).search($('#txt_CompanyName').val().trim());
    //    oTable1.columns(3).search($('#txt_TotalApprovedINV').val().trim());
    //    oTable1.draw();
    //});

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        //oTable1.columns(3).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });

    $("#ExportAnchCompCSV").click(function () {
        url = "../ExportVendorCompanyToCSV?CompanyIDD="+$("#hdnCompanyID").val()+"&companyName="+$("#hdnCompanyName").val() ;
        window.location.href = url;

    });
    $("#ExportAnchCompCSVVendor").click(function () {
        url = "../ExportVendorCompanyToCSV?CompanyIDD="+$("#hdnCompanyID").val()+"&companyName="+$('#txt_CompanyName').val();
        window.location.href = url;

    });
    //alert($("#hdnCompanyName").val());
    $('#lblcompanyName').text($("#hdnCompanyName").val());
    $('#lblcompanyName1').text($("#hdnCompanyName").val());
});