$(document).ready(function () {
    debugger;
    $('#DrawFundsList_tb').dataTable({

        "processing": true,
        "serverSide": true,
        "dom": '"ltipr"',

        "ajax": {
            "url": "../BankCompany/GetFundsFromBank",
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            //{ "data": "id", "name": "id", "autoWidth": true, "class": "index" }, //index 0
            { "data": "bankID", "name": "bankID", "autoWidth": true, "class": "index", "visible": false,"searchable": false}, //index 0
            { "data": "bankName", "name": "bankName", "autoWidth": true, "class": "index" }, //index 1
            { "data": "odbd", "name": "odbd", "autoWidth": true, "class": "index" }, //index 2
            { "data": "available_Limits", "name": "available_Limits", "autoWidth": true, "class": "index", render: $.fn.dataTable.render.number(',', '.', 0) },//index 3
            { "data": "interest_rate", "name": "Interest_rate", "autoWidth": true, "class": "index" }, //index 4
            {
                "data": "validityFromto", "name": "validityFromto", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            },
            {
                "data": "validity_upto", "name": "validity_upto", "autoWidth": true, "class": "clsCreatedDate", "format": "dd/MM/YYYY"
            },
            { "data": "draw_funds", "name": "Draw_funds", "autoWidth": true, "class": "clsTotalAmt", render: $.fn.dataTable.render.number(',', '.', 0) },//index 6
            { "data": "remaining_avail_bal", "name": "Remaining_avail_bal", "autoWidth": true, "class": "index", render: $.fn.dataTable.render.number(',', '.', 0) },
            
            {
                "data": function (data, type, row) {
                    var Available_bal = $.fn.dataTable.render.number(',','').display(data.remaining_avail_bal);              

                    return data.isVisibleAction?"<a href='#' id='btnModify'  data-bankName='" + data.bankName + "'  data-remaining_avail_bal='" + Available_bal + "' class='actions-ico' data-id=" + data.id + "><img src='../Content/images/edit.png' title='Edit' class='img-responsive' /></a>":"";
                    //return "<a href='#' id='btnModify'  data-bankName='" + data.bankName + "'  data-remaining_avail_bal='" + Available_bal + "' class='actions-ico' data-id=" + data.id + "><img  src='../Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                }
            }
        ],
        "columnDefs": [{
            "targets": 0,
            "searchable": false,
            "orderable": false,
            "className": "dt-body-center",
           
        },
        {
            "targets": 3,
            "createdCell": function (td, row, data, index) {
                $(td).addClass('clsTotalInvoice_' + data.anchorID);
            }
        }
        ],
        "order": [
            [1, "asc"],
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            //$("#lblapprovedamount").text(api.column('.clsTotalAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            var TotalRequestedDrawFunds = api.column('.clsTotalAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_RequestedDrawFunds = $.fn.dataTable.render.number(',', '').display(TotalRequestedDrawFunds);
            $("#lblapprovedamount").text(Total_RequestedDrawFunds);

        }

    });





    $("tbody").sortable({
        placeholder: "bg-warning",
        stop: function (event, tr) {
            $("tr").each(function (index, element) {
                var hiddenInput = $(element).children(".index").first();
                hiddenInput.text(index)

            });
        }

    });

    $(document).on("click", "#btnModify", function (e) {
        debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var bankName = $buttonClicked.attr('data-bankName');
        var available_bal = $buttonClicked.attr('data-remaining_avail_bal');
        
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "POST",
            url: '../BankCompany/EditFundsFromBankDetail',
            //contentType: "application/json; charset=utf-8",
            data: { "ID": id },
            //datatype: "json",
            success: function (data) {
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');

                $("#lblBankName").text(bankName);
                $("#lblAvailableBal").text(available_bal);
          

            },

            error: function () {
                alert("Dynamic content load failed.");
            }
        });


    });
});
