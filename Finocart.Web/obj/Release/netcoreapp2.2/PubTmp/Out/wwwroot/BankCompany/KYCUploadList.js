$(document).ready(function () {
    $('#tbl_kyculoadList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetKycUploadListata,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "companyID", "name": "companyID", "autoWidth": true, "visible": false, "searchable": false },
            { "data": "company_name", "name": "Company_name", "autoWidth": true },
            { "data": "status", "name": "Company_KYC_Status", "autoWidth": true },
            {
                "data": function (data, type, row) {
                    return "<a href='../BankCompany/UploadDocumentListing/?CompanyID=" + data.companyID + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }
        ],
    });

    oTable1 = $('#tbl_kyculoadList').DataTable();
    $('#btnComapnyFilter').click(function () {
        oTable1.columns(0).search($('#txt_AnchorNameSearch').val().trim());
        oTable1.draw();
    });
    $('#ExportAnchCompCSV').click(function () {
        var AnchorName = $('#txt_AnchorNameSearch').val().trim();
        url = "../ExportRegisterToCSV?AnchorName=" + AnchorName;
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
})
