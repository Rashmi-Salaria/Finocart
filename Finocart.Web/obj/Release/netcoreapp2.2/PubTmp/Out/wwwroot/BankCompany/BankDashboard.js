$(document).on("click", "#disbursements_model_popup", function (e) {
    var TeamDetailPostBackURL = '../BankCompany/AddEditBankDisbursements';
    var $buttonClicked = $(this);
    var options = { "backdrop": "static", keyboard: true };
    $.ajax({
        type: "GET",
        url: TeamDetailPostBackURL,
        contentType: "application/json; charset=utf-8",
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