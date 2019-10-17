$(document).ready(function () {
    $('#DrawFundsDocumentView').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": DrawFundsDocumentView,
            "data": { documentTypeID: $("#ID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "id", "name": "ID", "autoWidth": true },
            { "data": "filePath", "name": "FilePath", "autoWidth": true },
            //{ "data": "company_name", "name": "Company_name", "autoWidth": true },
            //{ "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },
            //{ "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            //{ "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true },
            //{ "data": "available_Limits", "name": "Available_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            //{ "data": "utilized_Limits", "name": "Utilized_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            //{
            //    "data": null,
            //    "render": function (data, type, full) {
            //        console.log(data.interest_rate);
            //        if (data.interest_rate === null && data.interest_rate_month === null)
            //            return '';
            //        else
            //            return data.interest_rate + '% for ' + data.interest_rate_month + ' Month';

            //    }
            //},
            //{
            //    "data": "validityFromto", "name": "ValidityFromto", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
            //        var date = new Date(data);
            //        var month = date.getMonth() + 1;
            //        return /*date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear()*/"";
            //    }
            //},
            //{
            //    "data": "validity_upto", "name": "Validity_upto", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
            //        var date = new Date(data);
            //        var month = date.getMonth() + 1;
            //        return /*date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear()*/ "";
            //    }
            //},
            {
                "data": function (data, type, row) {
                    return "<a href='#' onclick=DownloadHere(" + data.id + ")>Download Here</a>";
                    //return "<a onclick=GetBankLimits(" + data.companyID + ") class='actions - ico getInvoice clsEligibleInv' > <img src='../ Content / images / shape - 4.png' class='img - responsive' /></a > ";
                }
            }
        ],
        columnDefs: [
            {
                targets: [0],
                className: "hide_column"
            }]
    });

    oTable1 = $('#tbl_BankAnchorList').DataTable();
    $('#btnComapnyFilter').click(function () {
        oTable1.columns(0).search($('#txt_AnchorNameSearch').val().trim());
        oTable1.draw();
    });
    $('#ExportAnchCompCSV').click(function () {
        var AnchorName = $('#txt_AnchorNameSearch').val().trim();
        url = "../BankCompany/ExportBankSetLimitList?CompanyName=" + AnchorName;
        window.location.href = url;

    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(0).search($('#txt_AnchorNameSearch').val().trim());
        oTable1.draw();
    });

   

});

function DownloadHere(DocumentId) {
    url = Document_download + "?DocumentTypeID=" + DocumentId;
    window.location.href = url;
}