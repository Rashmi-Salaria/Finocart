$(document).ready(function () {

    $('#DrawFundsHistoryperBank').dataTable({

        "processing": true,
        "serverSide": true,
        "dom": '"ltipr"',


        "ajax": {
            "url": "../BankCompany/GetFundsFromBank",
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            {
                "data": "date", "name": "date", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },
            { "data": "draw_funds", "name": "Draw_funds", "autoWidth": true, "class": "clsTotalAmt", render: $.fn.dataTable.render.number(',', '.', 0) },//index 6
            { "data": "remaining_avail_bal", "name": "Remaining_avail_bal", "autoWidth": true, "class": "index", render: $.fn.dataTable.render.number(',', '.', 0) }
        ],

        "columnDefs": [{
            "targets": 0,
            "searchable": false,
            "orderable": false,
            "className": "dt-body-center",
            //"render": function (data, type, full, meta) {
            //    return '<input type="checkbox" name="id[]" value="'
            //        + $('<div/>').text(data).html() + '">';
            //}
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
            $("#lblapprovedamount").text(api.column('.clsTotalAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
        }
    });

    oTable1 = $('#DrawFundsHistoryperBank').DataTable();
    $("tbody").sortable({
        placeholder: "bg-warning",
        stop: function (event, tr) {
            $("tr").each(function (index, element) {
                var hiddenInput = $(element).children(".index").first();
                hiddenInput.text(index)
               
            });
        }

    });
});