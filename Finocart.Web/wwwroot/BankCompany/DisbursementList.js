$(document).ready(function () {
    $('#tbl_Disbursements').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetDisbursementList,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "id", "name": "id", "autoWidth": true, "visible": false, "searchable": false},
            { "data": "drawRequestId", "name": "DrawRequestId", "autoWidth": true },
            { "data": "anchorName", "name": "AnchorName", "autoWidth": true },
            { "data": "fundsRequested", "name": "FundsRequested", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0)},
            {
                "data": "requestDate", "name": "RequestDate", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            },
            { "data": "paidAmount", "name": "PaidAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0)},
            {
                "data": "paymentDate", "name": "PaymentDate", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            },
            { "data": "paymentStatus", "name": "PaymentStatus", "autoWidth": true },
            { "data": "utrDetails", "name": "UTRDetails", "autoWidth": true },
           
            {
                "data": function (data, type, row) {
                    return "<a href='#' id='btnModify' class='actions-ico' data-id=" + data.id + " ><img  src='../Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                }
            }
        ],
        "order": [
            [0, "desc"],
        ],
    });

    oTable1 = $('#tbl_Disbursements').DataTable();
    $('#btnComapnyFilter').click(function () {
        oTable1.columns(0).search($('#txt_AnchorNameSearch').val().trim());
        oTable1.draw();
    });
    $('#ExportAnchCompCSV').click(function () {
        var AnchorName = $('#txt_AnchorNameSearch').val().trim();
        url = "../BankCompany/ExportDisbursementToCSV?CompanyName=" + AnchorName;
        window.location.href = url;

    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(0).search($('#txt_AnchorNameSearch').val().trim());
        oTable1.draw();
    });

    $(document).on("click", "#btnModify", function (e) {
        var TeamDetailPostBackURL = '../BankCompany/EditDisbursementDetail';
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "CompanyID": id },
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
})
