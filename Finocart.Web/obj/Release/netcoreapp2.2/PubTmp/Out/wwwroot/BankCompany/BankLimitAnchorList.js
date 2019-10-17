$(document).ready(function () {
    $('#tbl_BankLimitAnchorList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        //"scrollX": true,
        "ajax": {
            "url": GetBankLimitAnchorList,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "companyID", "name": "companyID", "autoWidth": true, "visible": false, "searchable": false},
            { "data": "company_name", "name": "Company_name", "autoWidth": true },
            { "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },
            { "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            { "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true },
            //{ "data": "available_Limits", "name": "Available_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            //{ "data": "utilized_Limits", "name": "Utilized_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            //{ "data": "odbd", "name": "ODBD", "autoWidth": true },
            //{
            //    "data": null,
            //    "render": function (data, type, full) {
            //        debugger;
            //        console.log(data.interest_rate);
            //        if (data.interest_rate === null && data.interest_rate_month === null)
            //            return '';
            //        else
            //            return data.interest_rate + '% for ' + data.interest_rate_month + ' Month';

            //    }
            //},
            //{
            //    "data": "validityFromto", "name": "ValidityFromto", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            //},
            //{
            //    "data": "validity_upto", "name": "Validity_upto", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            //},
            {
                "data": function (data, type, row) {
                    return "<a href='#' id='btnModify' class='actions-ico' data-email=" + data.contact_email + " data-anchorname=" + data.company_name + " data-id=" + data.companyID + " ><img  src='/Finocart/Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                    //return "<a onclick=GetBankLimits(" + data.companyID + ") class='actions - ico getInvoice clsEligibleInv' data-email=" + data.contact_email + " data-anchorname=" + data.company_name + " > <img src='../ Content / images / shape - 4.png' class='img - responsive' /></a > ";
                }
            }
        ],
    });

    oTable1 = $('#tbl_BankLimitAnchorList').DataTable();
    $('#btnComapnyFilter').click(function () {
        oTable1.columns(1).search($('#txt_AnchorNameSearch').val().trim());
        oTable1.draw();
    });
    $('#ExportAnchCompCSV').click(function () {
        var AnchorName = $('#txt_AnchorNameSearch').val().trim();
        url = "../BankCompany/ExportBankSetLimitAnchorList?CompanyName=" + AnchorName;
        window.location.href = url;

    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_AnchorNameSearch').val().trim());
        oTable1.draw();
    });

    $(document).on("click", "#btnModify", function (e) {
        debugger;
        var TeamDetailPostBackURL = '../BankCompany/EditBankAnchorDetail';
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var email = $buttonClicked.attr('data-email');
        var anchorName = $buttonClicked.attr('data-anchorname');
        var options = { "backdrop": "static", keyboard: true };
        //$.ajax({
        //    type: "GET",
        //    url: TeamDetailPostBackURL,
        //    contentType: "application/json; charset=utf-8",
        //    data: { "CompanyID": id, "CompanyEmail": email, "anchorName": anchorName },
        //    datatype: "json",
        //    success: function (data) {
        //        $('#myModalContent').html(data);
        //        $('#myModal').modal(options);
        //        $('#myModal').modal('show');

        //    },
        //    error: function () {
        //        alert("Dynamic content load failed.");
        //    }
        //});
        url = "../BankCompany/BankLimitAnchorViewList?id=" + id;
        window.location.href = url;
    });
});

//function GetBankLimits(id) {
//    debugger;
//    var PageName = "";
//    var email = $buttonClicked.attr('data-email');
//    var anchorName = $buttonClicked.attr('data-anchorname');
//    url = "../BankCompany/BankLimitAnchorViewList?id=" + id;
//    window.location.href = url;

//}
