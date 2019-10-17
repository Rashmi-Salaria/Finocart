$(document).ready(function () {

    debugger;
    $('#tbl_VendorAwaitedInvApprovalList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetAwaitedInvoiceApprovalList,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "vendorID", "name": "VendorID", "autoWidth": true, "visible": false, "searchable": false }, //index 0
            { "data": "vendorName", "name": "VendorName", "autoWidth": true }, //index 1   
            { "data": "totalInvoiceCount", "name": "TotalInvoiceCount", "autoWidth": true, "class": "clsInvoiceCount" },//index 2
            { "data": "totalApprovedAmt", "name": "TotalApprovedAmt", "autoWidth": true, "class": "clstotalApprovedAmt" ,render: $.fn.dataTable.render.number(',', '.', 0) },//index 3
            {
                "data": function (data, type, row) {
                    return "<a href='../AnchorCompDashboard/AwaitedInvoiceApprovalView/?VendorIDs=" + btoa(data.vendorID) + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
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

                var Total_ApprovedAmt = $.fn.dataTable.render.number(',', '', 0, '').display(data.totalApprovedAmt);
                $("#lbAwaitTotalApprovedAmt").text(Total_ApprovedAmt);


                $(td).addClass('clsTotalInvoice_' + data.companyID);


            }
        }
        ],
        "order": [
            [1, "asc"],
        ],
        "footerCallback": function (row, data, start, end, display) {

            var api = this.api();
            $("#lbAwaitTotalInvoices").text(api.column('.clsInvoiceCount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));

            var TotalAppAmt = api.column('.clstotalApprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Total_App_Amt = $.fn.dataTable.render.number(',', '').display(TotalAppAmt);
            $("#lbAwaitTotalApprovedAmt").text(Total_App_Amt);
        }
    });

    oTable1 = $('#tbl_VendorAwaitedInvApprovalList').DataTable();
    $('#btnAwaitedVendorFilter').click(function () {

        oTable1.columns(1).search($('#txt_VendorName').val().trim());
        oTable1.columns(3).search($('#txt_TotalApprovedAmt').val().trim());
        oTable1.draw();
    });
    $('.searchTerm').on('keyup', function () {

        oTable1.columns(1).search($('#txt_VendorName').val().trim());
        oTable1.draw();
    });
    $('#ExportAnchCompCSV').click(function () {
        var CompanyName = $('#txt_CompanyName').val() || '';
        var TotalInvoiceAmt = $('#txt_TotalInvoiceAmt').val() || '';
        url = "../AnchorCompDashboard/ExportAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });



})
