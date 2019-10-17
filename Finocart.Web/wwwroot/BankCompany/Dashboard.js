
$(document).ready(function () {
    $('#tbl_DiscountAnchorCompList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetDiscAnchorCompListByVendorID,
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "company_name", "name": "company_name", "autoWidth": true }, //index 1
            { "data": "companyID", "name": "companyID", "autoWidth": true }, //index 2   
            { "data": "totalInvoice", "name": "TotalInvoice", "autoWidth": true, "class": "clsamountPaid" },
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, "class": "clsearning", render: $.fn.dataTable.render.number(',', '.', 2) }

        ],
    });

    oTable2 = $('#tbl_DiscountAnchorCompList').DataTable();
    $('#btnDiscCompFilter').click(function () {
        oTable2.columns(1).search($('#txt_DiscCompanyName').val().trim());
        oTable2.columns(3).search($('#txt_DiscTotalApprovedINV').val().trim());
        oTable2.draw();
    });
});