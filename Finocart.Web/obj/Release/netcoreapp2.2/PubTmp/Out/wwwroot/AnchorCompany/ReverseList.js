$(document).ready(function () {
       $('#tbl_CompanyList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetReverseCompanyData,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [  
            { "data": "company_name", "name": "Company_name", "autoWidth": true },
            { "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },  
            { "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true },
            { "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            { "data": "pan_number", "name": "Pan_number", "autoWidth": true },
            { "data": "contact_CIN", "name": "Contact_CIN", "autoWidth": true },
            { "data": "lookUp", "name": "LookUp", "autoWidth": true },
            {
                "data": function (data, type, row) {
                    if (data.lookUp == "Anchor Company") {
                        return "<a href='../AnchorCompany/EditCompanyPage/?CompanyIDD=" + btoa(data.companyID) + "'><img src='../Content/images/shape-4.png' title='View' style='display: inline-block' class='img-responsive' /></a>" +
                            //"</a><a href='../AnchorCompany/CompanyBankViewList/?CompanyID=" + btoa(data.companyID) + "'><img src='../Content/images/Bnkimage.png' title='Bank' style='display: inline-block; width: 20px;' class='img-responsive' /></a>" +
                            "<a href='../AnchorCompany/CompanyVendorListing/?CompanyIDD=" + btoa(data.companyID) + "&CompanyName=" + btoa(data.company_name) + "'><img src='../Content/images/av.png' title='AV' style='display: inline-block; width: 20px;' class='img-responsive' /></a>";
                    }
                    if (data.lookUp == "Bank") {
                        return "<a href='../AnchorCompany/EditCompanyPage/?CompanyIDD=" + btoa(data.companyID) + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' />"
                            + "<a href='../AnchorCompany/BankCompanyViewList/?CompanyIDD=" + btoa(data.companyID) + "&CompanyName=" + btoa(data.company_name) + "'><img src='../Content/images/av.png' title='AV' style='display: inline-block; width:20px;' class='img-responsive' /></a>"
                    }
                    if (data.lookUp == "Vendor Company") {
                        return "<a href='../AnchorCompany/EditCompanyPage/?CompanyIDD=" + btoa(data.companyID) + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>" +
                            "<a href='../AnchorCompany/VendorCompanyListing/?CompanyIDD=" + btoa(data.companyID) + "&CompanyName=" + btoa(data.company_name) + "'><img src='../Content/images/av.png' title='AV' style='display: inline-block; width: 20px;' class='img-responsive' /></a>"
                    }
                    if (data.lookUp == "Both") {
                        return "<a href='../AnchorCompany/EditCompanyPage/?CompanyIDD=" + btoa(data.companyID) + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' />"
                            + "</a><a href='../AnchorCompany/BothVendorAnchorList/?CompanyIDD=" + btoa(data.companyID) + "&CompanyName=" + btoa(data.company_name) + "'><img src='../Content/images/av.png' title='AV' style='display: inline-block; width:20px;' class='img-responsive' /></a>"
                    }
                }
            }
        ],
    });

    oTable1 = $('#tbl_CompanyList').DataTable();
    //$('#btnComapnyFilter').click(function () {
    //    oTable1.search($('#txt_CompanyNameSearch').val().trim());
    //    //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
    //    oTable1.draw();
    //});
    //$('#ExportReverseFactoring').click(function () {
        
    //    var ContactPerson = "";
    //    var CompanyName = $('#txt_CompanyNameSearch').val().trim();
    //    url = "../ExportRegisterToCSV?ContactPerson=" + ContactPerson + "&CompanyName=" + CompanyName;
    //    window.location.href = url;
       
    //});
    $('#ExportReverseFactoring').click(function () {

        var ContactPerson = "";
        var CompanyName = "";
        url = "/AnchorCompany/ExportReverseFactoring?ContactPerson=" + ContactPerson + "&CompanyName=" + CompanyName;
        window.location.href = url;

    });

    $('.searchTerm').on('keyup', function () {
        
        oTable1.columns(0).search($('#txt_CompanyNameSearch').val().trim());
        //oTable1.columns(1).search($('#txt_ContactNameSearch').val().trim());
        oTable1.draw();
    });
})
