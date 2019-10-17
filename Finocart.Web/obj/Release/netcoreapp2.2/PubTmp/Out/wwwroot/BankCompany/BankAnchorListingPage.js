$(document).ready(function () {
    $('#tbl_BankAnchorList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetBankAnchorListata,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "companyID", "name": "companyID", "autoWidth": true, "visible": false, "searchable": false },
            { "data": "company_name", "name": "Company_name", "autoWidth": true },
            { "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },
            { "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            { "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true },
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
                    return "<a href='#' id='btnModify' class='actions-ico' data-email=" + data.contact_email + " data-anchorname=" + data.company_name + " data-id=" + data.companyID + " ><img  src='../Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                    //return "<a onclick=GetBankLimits(" + data.companyID + ") class='actions - ico getInvoice clsEligibleInv' > <img src='../ Content / images / shape - 4.png' class='img - responsive' /></a > ";
                }
            }
        ],
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

    $(document).on("click", "#btnModify", function (e) {
        var TeamDetailPostBackURL = '../BankCompany/EditBankAnchorDetail';
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var email = $buttonClicked.attr('data-email');
        var anchorName = $buttonClicked.attr('data-anchorname');
        var options = { "backdrop": "static", keyboard: true };
        debugger;
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "CompanyID": id, "CompanyEmail": email, "anchorName": anchorName },
            datatype: "json",
            success: function (data) {
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });


});
