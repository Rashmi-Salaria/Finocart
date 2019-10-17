var InvoiceID = []; var CompInvID = []; var AnchCompanyID = []; var TotalSelectedInvAmt = 0; var TotalInvoice = 0;
$(document).ready(function () {
    
    window.history.pushState(null, "", window.location.href);
    window.onpopstate = function () {
        window.history.pushState(null, "", window.location.href);
    };

    $("#lbTotalSelectInvoices").text(0);
    $('#tbl_AnchorCompList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetRecieableDueAmount,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            //{
            //    "data": function (data, type, row) {            //index 0
            //      return "<input type='checkbox' class='select-checkbox clsCompanyCheck_" + data.anchorID+"'/>";
            //    }
            //},
            { "data": "anchorID", "name": "anchorID", "autoWidth": true, "class": "clsAnchCompanyID", "visible": false, "searchable": false }, //index 1

            { "data": "anchorname", "name": "Anchorname", "autoWidth": true }, //index 2   
            {
                "data": "noOfInvoices", "name": "NoOfInvoices", "autoWidth": true,"class": "clsTotalInvoiceNo"
                
            },
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, "class": "clsTotalInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) },

            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, "class": "clsApprovedAmt", render: $.fn.dataTable.render.number(',', '.', 0) },

            { "data": "amountReceiabletoday", "name": "AmountReceiabletoday", "autoWidth": true, "class": "clsTotalInvoiceAmountRec", render: $.fn.dataTable.render.number(',', '.', 0) },
            {
                "data": function (data, type, row) {
                    //return "<a onclick='GetInvoice("+data.anchorID +")' class='actions-ico getInvoice clsEligibleInv'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                    //return "<a href='../Vendor/GetIndividualReceiveableDueToday/?icompanyID=" + data.anchorID + + "' class='actions-ico getInvoice clsEligibleInv'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                    return "<a href='../Vendor/GetIndividualReceiveableDueToday/?anchorID=" + btoa(data.anchorID) +"'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }

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
            "createdCell": function (td,row, data, index) {
                $(td).addClass('clsTotalInvoice_' + data.anchorID);
            }
        }
        ],
        "order": [
            [1, "asc"],
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lblapprovedinvices").text(api.column('.clsTotalInvoiceNo').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));

            var Approvedamount = api.column('.clsApprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Approved_amount = $.fn.dataTable.render.number(',', '').display(Approvedamount);
            $("#lblapprovedamount").text(Approved_amount); 

            var AmountRecievabletoday = api.column('.clsTotalInvoiceAmountRec').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0);
            var Amount_Recievable_today = $.fn.dataTable.render.number(',', '').display(AmountRecievabletoday);
            $("#lblamountrecievabletoday").text(Amount_Recievable_today);
        }
    });

    oTable1 = $('#tbl_AnchorCompList').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(2).search($('#txt_CompanyName').val().trim());
        oTable1.columns(4).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(2).search($('#txt_CompanyName').val().trim());
        oTable1.columns(4).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });


    $('#ExportAnchCompCSV').click(function () {
        var Anchorname = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmount = $('#txt_TotalApprovedINV').val().trim();
        url = "../Vendor/ExportGetRecieableDueAmount?Anchorname=" + Anchorname + "&TotalInvoiceAmount=" + TotalInvoiceAmount;
        window.location.href = url;
    });

    $('#AnchorComp-select-all').on('click', function () {
        // Check/uncheck all checkboxes in the table
        var rows = oTable1.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    $('#tbl_AnchorCompList').off().on("change", "input[type='checkbox']", function () {
        //$('#tbl_AnchorCompList [type="checkbox"]').each(function (i, chk) {
        var DiscountInvID = []
        if ($(this).prop("checked") == true) {
            if (AnchCompanyID.indexOf(parseInt($(this).closest("tr").find('.clsAnchCompanyID').text())) == -1) {
                $("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) + parseInt($(this).closest("tr").find('.clsTotalInvoice').text()));
            }
        }
        else {
            var CompanyID = parseInt($(this).closest("tr").find('.clsAnchCompanyID').text())
            CompInvID = JSON.parse($("#companyInvoicesID").val());
            for (var i = 0; i < InvoiceID.length;) {
                if (InvoiceID[i][1] == CompanyID) {                    
                    InvoiceID.splice(i, 1);
                    CompInvID.splice(i, 1)
                }
                else {
                    i++;
                }
            }           
            $("#companyInvoicesID").val(JSON.stringify(CompInvID));
            AnchCompanyID.splice($.inArray(parseInt($(this).closest("tr").find('.clsCompanyId').val()), AnchCompanyID), 1);
            var TotalInvoiceCount = $(this).closest("tr").find('.clsTotalInvoice').text();
            if (TotalInvoiceCount.indexOf('(') != -1) {
                $("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) - parseInt(TotalInvoiceCount.split('(')[1].split(')')[0]));
            }
            else {
                $("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) - parseInt(TotalInvoiceCount));
            }
            $(".clsTotalInvoice_" + CompanyID).text($(this).closest("tr").find('.clsTotalInvoice').text().split('(')[0]);
        }
            
            //if (chk.checked) {

            //}
        //});       
    });

    $('#btnOfferDiscount').on('click', function () {
        $("#txt_TotalSelectInvoices").val($("#lbTotalSelectInvoices").text())
    });

    //$('#tbl_DiscountAnchorCompList').dataTable({
    //    "processing": true,
    //    "serverSide": true,
    //    "searching": true,
    //    "dom": '"ltipr"',
    //    "ajax": {
    //        "url": GetDiscAnchorCompListByVendorID,
    //        "type": "POST",
    //        "datatype": "json",
    //    },
    //    "columns": [
    //        { "data": "companyID", "name": "CompanyID", "autoWidth": true }, //index 1
    //        { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2   
    //        { "data": "totalInvoice", "name": "TotalInvoice", "autoWidth": true, "class": "clsTotalInvoice" },
    //        { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, "class": "clsTotalInvoiceAmount" },
    //        {
    //            "data": function (data, type, row) {
    //                //return "<a href='../AnchorCompany/GetVendorInvoiceDetailsViewByVendorID/?icompanyID=" + data.companyID + "' class='actions-ico'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
    //                return "<a onclick='GetInvoice(" + data.companyID + ",this)' class='actions-ico getInvoice clsDiscountOfferedInv'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
    //            }
    //        }

    //    ],
    //    "footerCallback": function (row, data, start, end, display) {
    //        var api = this.api();
    //        $("#lbTotalInvoices1").text(api.column('.clsTotalInvoice').data().reduce(function (a, b) {return Number(a) + Number(b);}, 0));
    //        $("#lbTotalInvoicesAmt1").text(api.column('.clsTotalInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));           
    //    }
    //});

    oTable2 = $('#tbl_DiscountAnchorCompList').DataTable();
    $('#btnDiscCompFilter').click(function () {
        oTable2.columns(1).search($('#txt_DiscCompanyName').val().trim());
        oTable2.columns(3).search($('#txt_DiscTotalApprovedINV').val().trim());
        oTable2.draw();
    });

    $('#ExportDiscAnchCompCSV').click(function () {
        var CompanyName = $('#txt_DiscCompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_DiscTotalApprovedINV').val().trim();
        url = "../Vendor/ExportDiscAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });

    
    oTable3 = $('#tbl_Buckets').DataTable();
    $("#lbTotalDiscBucket").text(oTable3.rows().count());
    $('#btnDiscBuckFilter').click(function () {
        oTable3.columns(2).search($('#txt_AnchorName').val().trim());
        oTable3.columns(8).search($('#ddl_Status').val().trim());
        oTable3.draw();
        GetLastSearchValue();
    });

    $('#ExportDiscBucketWiseCSV').click(function () {
        var AnchorName = $('#txt_AnchorName').val().trim();
        var InvoiceStatus = $('#ddl_Status').val().trim();
        url = "../Invoice/ExportInvoiceData?AnchorCompanyName=" + AnchorName + "&InvoiceStatus=" + InvoiceStatus;
        window.location.href = url;
    });

    $('#pagechange2').change(function () {
        
        var newTFvalue = $("#pagechange2").val();
        if (newTFvalue == "1") {
            url = "../Vendor/GetReceivableDue";
            window.location.href = url;

        }
        else if (newTFvalue == "2") {
            url = "../VendorDashboard/InvoicesAwaitedApprovals";
            window.location.href = url;
        }
        else if (newTFvalue == "3") {
            url = "../Vendor/InvoiceListBytodaysdate";
            window.location.href = url;

        }
        else if (newTFvalue == "4") {
            url = "../Vendor/Availableforfunding";
            window.location.href = url;

        }
        else if (newTFvalue == "5") {
            url = "../Vendor/InvoiceHistory";
            window.location.href = url;
        }

    });

});

function GetInvoice(id) {
    
    $.ajax({
        type: "Get",
        dataType: "html",
        url: "../Vendor/GetIndividualReceiveableDueToday?id=" +id,
        //data: { EmailID: $("#Email").val(), Module: 'User' },
        //contentType: 'application/json; charset=utf-8',
        //async: false,
        success: function (data) {
            
        },  
        error: function () {
            alert("Error occured!!")
        }
    });
}

function BackInvoice() {
    $("#eAnchorCompInvoices").show();
    $("#eEligibleInvoices").hide();
}
function BackToDiscOfferedInv() {
    $("#dOfferedInvoices").show();
    $("#iDiscOfferedInv").hide();
}

