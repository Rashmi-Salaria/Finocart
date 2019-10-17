$(document).ready(function () {
    $('#tbl_BucketsWiseAwaitedInvoiceView').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetAwaitedInvoiceApprovalViewList,
            "data": { VendorID: $("#VendorID").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "bucketID", "name": "BucketID", "autoWidth": true }, //index 0   
            { "data": "vendorID", "name": "VendorID", "autoWidth": true }, //index 2
            { "data": "totalInvoiceCount", "name": "TotalInvoiceCount", "autoWidth": true, "class": "clsInvoiceCount" },//index 3
            { "data": "totalApprovedAmt", "name": "TotalApprovedAmt", "autoWidth": true, "class": "clstotalApprovedAmt",render: $.fn.dataTable.render.number(',', '.', 0) },//index 4
            {
                "data": "discOfferedDate", "name": "DiscOfferedDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month > 9 ? month : "0" + month) + "/" + date.getFullYear();
                }},//index 5
            //{ "data": "discValidUpto", "name": "DiscValidUpto", "autoWidth": true },//index 6
            { "data": "bucketName", "name": "BucketName", "autoWidth": true }, //index 1
            {
                "data": function (data, type, row) {
                    return "<a href='../BucketWiseInvoiceViewList/?BuckID=" + btoa(data.bucketID) + "&VendID=" + btoa(data.vendorID) + "&VendorCompany=" + btoa(data.bucketName) + "'><img src='http://dotnet.brainvire.com/Finocart/Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }

        ],
        "columnDefs": [{
            "targets": [1],
            "searchable": false,
            "orderable": false,
            "className": "dt-body-center hide_column",
        },
        {
            "targets": 3,
            "createdCell": function (td, row, data, index) {
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
        //columnDefs: [
        //    {
        //        targets: [2],
        //        className: "hide_column"
        //    }
       
    });

    oTable1 = $('#tbl_BucketsWiseAwaitedInvoiceView').DataTable();
    $('#btnBucketFilter').click(function () {
        oTable1.columns(0).search($('#txt_BucketID').val().trim());
        oTable1.columns(3).search($('#txt_ApprovedAmt').val().trim());
        oTable1.draw();
    });


    $('.searchTerm').on('keyup', function () {
        oTable1.columns(0).search($('#txt_BucketID').val().trim());
        oTable1.columns(3).search($('#txt_ApprovedAmt').val().trim());
        oTable1.draw();
    });

    $('#ExportAnchCompCSV').click(function () {
        debugger
        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalInvoiceAmt').val().trim();
        var VendorId = $("#VendorID").val().trim();
        url = "../AnchorCompDashboard/ExportAwaitedInvoiceApprovalView?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt + "&VendorId" + VendorId;
        window.location.href = url;
    });

    $('#ExportInvoiceCSV').click(function () {
        debugger;
        var BucketId = $('#txt_BucketID').val().trim();
        var ApprovedAmt = $('#txt_ApprovedAmt').val().trim();
        var VendorId = $("#VendorID").val().trim();
        url = "../ExportInvoice?Bucket=" + BucketId + "&TotalInvoiceAmt=" + ApprovedAmt + "&VendorId=" + VendorId;
        window.location.href = url;
    });
})

function BackBtn() {
    history.back();
}

//function HomeBtn() {
//    url = "../AnchorDashboard";
//    window.location.href = url;
//}