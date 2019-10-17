$(document).ready(function () {
    $('#tbl_BankLimitAnchorViewList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetBankLimitAnchorViewList,
            "data": { CompanyID: $("#hdnCompanyID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' value='" + data.setLimitId + "'/>";
                }
            },
            //{ "data": "company_name", "name": "Company_name", "autoWidth": true },
            //{ "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },
            //{ "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            //{ "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true },
            { "data": "available_Limits", "name": "Available_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "utilized_Limits", "name": "Utilized_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "odbd", "name": "ODBD", "autoWidth": true },
            {
                "data": null,
                "render": function (data, type, full) {
                    console.log(data.interest_rate);
                    if (data.interest_rate === null && data.interest_rate_month === null)
                        return '';
                    else
                        return data.interest_rate + '%';

                }
            },
            {
                "data": "validityFromto", "name": "ValidityFromto", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            },
            {
                "data": "validity_upto", "name": "Validity_upto", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            },
            {
                "data": function (data, type, row) {
                    if (data.utilized_Limits != null && data.utilized_Limits != 0) {
                        return "<a href='#' id='btnModify' class='actions-ico' data-email=" + data.contact_email + " data-anchorname=" + data.company_name + " data-id=" + data.setLimitId + " style='display:none'><img  src='/Finocart/Content/images/edit.png' title='Edit' class='img-responsive' /></a>" +
                            "<a href='#' onclick='GetLogView(" + data.setLimitId + ")' class='actions-ico' data-toggle='modal' data-target='#frview' ><img  src='/Finocart/Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                    }
                    else {
                        return "<a href='#' id='btnModify' class='actions-ico' data-email=" + data.contact_email + " data-anchorname=" + data.company_name + " data-id=" + data.setLimitId + " ><img  src='/Finocart/Content/images/edit.png' title='Edit' class='img-responsive' /></a>" +
                            "<a href='#' onclick='GetLogView(" + data.setLimitId + ")' class='actions-ico' data-toggle='modal' data-target='#frview' ><img  src='/Finocart/Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                    }
                }
            }
        ],
        'columnDefs': [
        {
            'targets': 0,
            'className': 'hide_column',
        }]
    });

    oTable1 = $('#tbl_BankLimitAnchorViewList').DataTable();
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
        //var TeamDetailPostBackURL = '../BankCompany/EditBankAnchorDetail';
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var email = $buttonClicked.attr('data-email');
        var anchorName = $buttonClicked.attr('data-anchorname');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "SetLimitID": id, "CompanyID": $("#hdnCompanyID").val(), "CompanyEmail": email, "anchorName": anchorName, "PageName": "Limits" },
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

    $("#btnAddNewSetLimit").click(function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var email = $buttonClicked.attr('data-email');
        var anchorName = $buttonClicked.attr('data-anchorname');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "CompanyID": $("#hdnCompanyID").val(), "CompanyEmail": $("#hdnCompanyEmail").val(), "anchorName": $("#hdnCompanyName").val(), "PageName":"Limits", "Status":"Approved" },
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

function GetLogView(setLimitId) {
    $('#tblBankSetLimitLogView').DataTable().destroy();
    $('#tblBankSetLimitLogView').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        //"scrollX": true,
        "ajax": {
            "url": GetBankLimitLogList,
            "data": { setLimitId: setLimitId },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            //{
            //    "data": function (data, type, row) {
            //        debugger;
            //        return "<input type='hidden' value='" + data.setLimitId + "'/>";
            //    }
            //},
            //{ "data": "company_name", "name": "Company_name", "autoWidth": true },
            //{ "data": "contact_Name", "name": "Contact_Name", "autoWidth": true },
            //{ "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            //{ "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true },
            { "data": "available_Limits", "name": "Available_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "utilized_Limits", "name": "Utilized_Limits", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0),"visible":false },
            { "data": "odbd", "name": "ODBD", "autoWidth": true },
            {
                "data": null,
                "render": function (data, type, full) {
                    console.log(data.interest_rate);
                    if (data.interest_rate === null && data.interest_rate_month === null)
                        return '';
                    else
                        return data.interest_rate + '%';

                }
            },
            {
                "data": "validityFromto", "name": "ValidityFromto", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            },
            {
                "data": "validity_upto", "name": "Validity_upto", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            },
            {
                "data": "modifiedDate", "name": "Validity_upto", "autoWidth": true, "class": "clsModifiedDate"
            }
            //{
            //    "data": function (data, type, row) {
            //        return "<a href='#' id='btnModify' class='actions-ico' data-email=" + data.contact_email + " data-anchorname=" + data.company_name + " data-id=" + data.setLimitId + " ><img  src='../Content/images/edit.png' title='Edit' class='img-responsive' /></a>" +
            //            "<a href='#' id='btnView' class='actions-ico' data-email=" + data.contact_email + " data-anchorname=" + data.company_name + " data-id=" + data.setLimitId + " ><img  src='../Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
            //    }
            //}
        ]
        //'columnDefs': [
        //    {
        //        'targets': 0,
        //        'className': 'hide_column',
        //    }]
    });
}
