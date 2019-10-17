//================================================================Declarations=========================================================//

//================================================================Declarations=========================================================//
$(document).ready(function () {
    var index = 0;
    $("#TotalSelApprovedInvAmt").text(0);
    $("#TotalSelInvCount").text(0);
   
    $('#tbl_AnchorCompList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetTodayRecAmount,
            "data": { CompanyId: $("#Item3_CompanyId").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [

            //{ "data": function (data, type, row) {return  } },
            //{
            //    "data": function (data, type, row) {            //index 0
            //        return "<input type='checkbox' class='select-checkbox clsCompanyCheck_" + data.InvoiceNo + "'/>";
            //    }
            //    },
            //index 0
            { "data": "poNumber", "name": "PONumber", "autoWidth": true, "class": "clsPONumber" }, //index 1
            { "data": "invoiceNO", "name": "InvoiceNO", "autoWidth": true, "class": "clsInvoiceNo" }, //index 2
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month.length > 1 ? month : month) + "/" + date.getFullYear();
                }}, //index 3
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, "class": "clsInvoiceAmt", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 4
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, "class": "clsApprovedAmt", render: $.fn.dataTable.render.number(',', '.', 0) }, //5
            { "data": "discount", "name": "Discount", "autoWidth": true, "class": "clsDiscount" }, //index 6
            { "data": "netReceivableAmount", "name": "NetReceivableAmount", "autoWidth": true, "class": "NetReceivableAmount", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 7
            
           
          
           
        ],
        
    });

    oTable = $('#tbl_EligibleInvDashboard').DataTable();
    var result = [];
  
    $('#btnFilter').click(function () {
        oTable.columns(3).search($('#txt_AnchorName').val().trim());
        oTable.columns(7).search($('#ddl_Status').val().trim());
        oTable.draw();
        GetLastSearchValue();
    });

  
});




    //$('.clsRowCheckbox').change(function () {
     $('#tbl_EligibleInvDashboard').off().on("change", "input[type='checkbox']", function () {
         
         var TotalInvAmount = 0; var InvoicecalculateAmt = 0; var id; var CompanyID = 0; var row = 0; var SelInvoicesCount = "";
         $('#tbl_EligibleInvDashboard [type="checkbox"]').each(function (i, chk) {
            if (chk.checked) {

                TotalInvAmount += Number($(this).closest("tr").find('.clsInvoiceAmt').text());//Invoice amount column
                id = $(this).closest("tr").find('.clsRowInvoiceID').val();//td:eq(9)
                var InvApprovedDate = $(this).closest("tr").find('.clsInvApprovedDate').val()
                var dateinNewFormat = new Date(InvApprovedDate);
                var daysToApprove = $(this).closest("tr").find('.clsInvApprovePayDay').val();
                dateinNewFormat.setDate(dateinNewFormat.getDate() + parseInt(daysToApprove));
                var dd = dateinNewFormat.getDate();
                var mm = dateinNewFormat.getMonth() + 1;
                var y = dateinNewFormat.getFullYear();
                var ApproxDateOfApproval = y + '-' + mm + '-' + dd;
                var Days = Common.DiffrenceBetweenDays($(this).closest("tr").find('.clsPaymentDueDate').text(), ApproxDateOfApproval);

                var CalCulatedValueByFormula = ((Days / 365) * $(this).closest("tr").find('td:eq(5)').text()).toFixed(2);
                $('.clsFormulaRow_' + id + '').val(CalCulatedValueByFormula);

                InvoicecalculateAmt += Number($('.clsFormulaRow_' + id + '').val());
                $("#hdnValuesForTX").val(InvoicecalculateAmt);

                CompanyID = $(this).closest("tr").find('.clsCompanyId').val();
                ++row;
            }
           
         });
         $(".clsTotalInvoice_" + CompanyID).text('');
         $(".clsTotalInvoice_" + CompanyID).text(TotalInvoice + '(' + row + ')');
         if ($(this).prop("checked") == true) {
             InvoiceID.push([parseInt(id), parseInt(CompanyID)]);
             CompInvID.push(parseInt(id));
              
            TotalSelectedInvAmt = parseInt($("#lbTotalSelectInvoices").text()) + Number($(this).closest("tr").find('.clsInvoiceAmt').text());
        }
        else {
            InvoiceID.splice($.inArray(parseInt($(this).closest("tr").find('.clsRowInvoiceID').val()), InvoiceID), 1);
            
            TotalSelectedInvAmt -= Number($(this).closest("tr").find('.clsInvoiceAmt').text());
         }
         $("#lbTotalSelectInvoices").text(0);
         $('#tbl_AnchorCompList [type="checkbox"]').each(function (i, chk) {
             if (chk.checked) {
                 if ($(this).closest("tr").find('.clsTotalInvoice').text().indexOf('(') != -1) {
                     $("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) + Number($(this).closest("tr").find('.clsTotalInvoice').text().split('(')[1].split(')')[0]));
                 }
                 else {
                     $("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) + Number($(this).closest("tr").find('.clsTotalInvoice').text()))
                 }
             }
         });

         var Value = JSON.stringify(CompInvID);
         $("#companyInvoicesID").val(Value);
         
         //$("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) + InvoiceID.length);
         $("#lbTotalSelectInvoicesAmt").text(TotalSelectedInvAmt);

         if ($('#tbl_EligibleInvDashboard').find('input[type="checkbox"]:checked')[0] != undefined) {
             $('.clsCompanyCheck_' + CompanyID).prop('checked', true);
             if (($.inArray(parseInt(CompanyID), AnchCompanyID)) == -1) {
                 if (parseInt(CompanyID) != 0) {
                     AnchCompanyID.push(parseInt(CompanyID))
                 }
             }
         }
         else {
             $('.clsCompanyCheck_' + $(this).closest("tr").find('.clsCompanyId').val()).prop('checked', false);
             AnchCompanyID.splice($.inArray(parseInt($(this).closest("tr").find('.clsCompanyId').val()), AnchCompanyID), 1);
         }

        $("#TotalSelApprovedInvAmt").text(TotalInvAmount);
        
        var chkCheckboxCnt = $('#tbl_EligibleInvDashboard').find('input[type="checkbox"]:checked')
        if (chkCheckboxCnt.length == 0) {
            $('#btnAddtobucket').attr("disabled", true);
        }
        else {
            $('#btnAddtobucket').attr("disabled", false);
        }
        $("#TotalSelInvCount").text(chkCheckboxCnt.length);
});
$(document).on("keydown", "input[id='txt_Discount'],input[id='txt_DiscountPerInv']", function () {
    if (event.shiftKey == true) {
        event.preventDefault();
    }

    if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 8 || event.keyCode == 9 ||
        event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {

    } else {
        event.preventDefault();
    }

    if ($(this).val().indexOf('.') !== -1 && event.keyCode == 190)
        event.preventDefault();
});

function BackBtn() {
    history.back();
}






