
$(document).ready(function () {
    debugger;
    $('#tbl_BankSetLimitList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetBankSetLimitList,
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "companyID", "name": "companyID", "autoWidth": true },
            { "data": "company_name", "name": "Company_name", "autoWidth": true },
            { "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            { "data": "available_Limits", "name": "Available_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "utilized_Limits", "name": "Utilized_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            //{ "data": "interest_rate", "name": "Interest_rate", "autoWidth": true },
            {
                "data": null,
                "render": function (data, type, full) {
                    if (data.interest_rate === null && data.interest_rate_month === null)
                        return '';
                    else
                        return data.interest_rate + '% for ' + data.interest_rate_month + ' Month';
                        
                }
            },
            {
                "data": "validityFromto", "name": "ValidityFromto", "autoWidth": true, "class": "clsCreatedDate", "render":""
                //    function (data) {
                //    var date = new Date(data);
                //    var month = date.getMonth() + 1;
                //    return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                //}
            },
            {
                "data": "validity_upto", "name": "Validity_upto", "autoWidth": true, "class": "clsCreatedDate", "render":""
                //    function (data) {
                //    var date = new Date(data);
                //    var month = date.getMonth() + 1;
                //    return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                //}
            },

            {
                "data": function (data, type, row) {
                    return "<a href='#' id='btnModify' class='actions-ico' data-email=" + data.contact_email + " data-anchorname='" + data.company_name + "' data-id=" + data.companyID + " ><img  src='/Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                }
            }
        ],
        //"columnDefs": [{
        //    "targets": '_all',
        //    "defaultContent": "-"
        //}],
    });

    oTable2 = $('#tbl_BankSetLimitList').DataTable();

    $('#btnComapnyFilter').click(function () {
        oTable2.columns(1).search($('#txt_CompanyNameSearch').val().trim());
        oTable2.draw();
    });
    $('.searchTerm').on('keyup', function () {

        oTable2.columns(0).search($('#txt_CompanyNameSearch').val().trim());
        oTable2.draw();
    });
    $('#ExportAnchCompCSV').click(function () {
        var AnchorName = $('#txt_CompanyNameSearch').val().trim();
        url = "../BankCompany/ExportBankSetLimitList?CompanyName=" + AnchorName;
        window.location.href = url;

    });
   
   });
$(document).on("click", "#btnModify", function (e) {
    debugger;
    var TeamDetailPostBackURL = '../BankCompany/EditBankAnchorDetail';
    var $buttonClicked = $(this);
    var id = $buttonClicked.attr('data-id');
    var email = $buttonClicked.attr('data-email');
    var anchorName = $buttonClicked.attr('data-anchorname');
    var options = { "backdrop": "static", keyboard: true };
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

