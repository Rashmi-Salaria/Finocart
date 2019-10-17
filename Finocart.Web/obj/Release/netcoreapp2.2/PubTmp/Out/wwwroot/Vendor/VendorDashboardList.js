//================================================================Declarations=========================================================//

//================================================================Declarations=========================================================//
$(document).ready(function () {
    var index = 0;
    $("#TotalSelApprovedInvAmt").text(0);
    $("#TotalSelInvCount").text(0);
   
    $('#tbl_EligibleInvDashboard').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetEligibleInvoicesList,
            "data": { CompanyId: $("#Item3_CompanyId").val() },
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [

            //{ "data": function (data, type, row) {return  } },
            {
                "data": function (data, type, row) {
                    
                    if ($("#companyInvoicesID").val() != "") {
                        if ((JSON.parse($("#companyInvoicesID").val()).indexOf(data.invoiceID)) >= 0) {
                                return "<input type='checkbox' checked class='clsRowCheckbox'/>"
                            }
                    }
                    return "<input type='checkbox' class='clsRowCheckbox'/>"
                }
            },//index 0
            { "data": "poNumber", "name": "PONumber", "autoWidth": true, "class": "clsPONumber" }, //index 1
            { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true, "class": "clsInvoiceNo" }, //index 2
            { "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true, "class": "clsCompanyName" }, //index 3
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }}, //index 4
            { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, "class": "clsInvoiceAmt", render: $.fn.dataTable.render.number(',', '.', 2) }, //index 5
            { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, "class": "clsApprovedAmt", render: $.fn.dataTable.render.number(',', '.', 2)}, //index 6
            {
                "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }
            }, //index 7
           
            { "data": "Status", "defaultContent": "<i>Eligible for Discounting</i>", "autoWidth": true, "class": "clsInvoiceStatus" }, //index 8
           
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsRowInvoiceID' value='" + data.invoiceID +"'/>";
                }
            },//index 9
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsInvApprovePayDay' value='" + data.invoiceApprovePayDays +"'/>";
                }
            },//index 10
            {
                "data": function (data, type, row) {
                    return "<input type='text' class='clsInvApprovedDate' value='" + data.invoiceApprovalDate +"' />";
                }
            },//index 11
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsFormulaRow_" + data.invoiceID + "'/>";
                }
            },//index 12
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsCompanyId' value='" + data.companyId + "'/>";
                }
            },//index 13
            {
                "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true
            },
            {
                "data": function (data, type, row) {
                    //return "<a href='#' onclick='DownloadInvoice()' class='actions-ico'><img src='../Content/images/download.png' title='Download' class='img-responsive' title='Download invoice'/></a>" +
                    return "<a href='#' onclick='GetInvoiceJourney(" + data.invoiceID + ",this)' data-para1='EligibleInvoice' class='actions-ico'><img src='../Content/images/New folder/shape-29.png' title='Invoice Journey' class='img-responsive' title='Invoice Journey'/></a>"
                            //"<a href='#hrefApplyDiscount' class='actions-ico' id='hrefApplyDiscount'><img src='../Content/images/rectangle-1.png' title='' class='img-responsive' title='Apply Discount'/></a>";
                }//index 14
            }
            
        ],
        columnDefs: [
            {
                targets: [9,10,11,12,13],
                className: "hide_column"
            },
            {
                targets: [14],
                className: "clsPaymentDueDate hide_column"
            }
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            
            $("#lbTotalEligibleInvoices").text(api.rows().count());
            $("#lbTotalApprovedInvoicesAmt").text(api.column('.clsApprovedAmt').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));   
        }
    });

    oTable = $('#tbl_EligibleInvDashboard').DataTable();
    var result = [];
  
    $('#btnFilter').click(function () {
        oTable.columns(3).search($('#txt_AnchorName').val().trim());
        oTable.columns(8).search($('#ddl_Status').val().trim());
        oTable.draw();
        GetLastSearchValue();
    });
    $('.searchTerm').on('keyup', function () {
        debugger;
        oTable1.columns(3).search($('#txt_AnchorName').val().trim());
        oTable1.columns(8).search($('#ddl_Status').val().trim());
        oTable1.draw();
    });
    $('#ExportInvoiceCSV').click(function () {
        var AnchorName = $('#txt_AnchorName').val().trim();
        var InvoiceStatus = $('#ddl_Status').val().trim();
        url = "../Invoice/ExportInvoiceData?AnchorCompanyName=" + AnchorName + "&InvoiceStatus=" + InvoiceStatus;
        window.location.href = url;
    });
});

function GetLastSearchValue() {

    $.ajax({
        type: "get",
        url: GetLastSearchhistory,
        data: '',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            
            var totaltextboxcount = $('.clsLastSearch').length;
            var htmlRowLastSrch = "";
            var list = data.data;
            if (totaltextboxcount < $("#MaxSearchLimit").val() && $("#txt_AnchorName").val() != "") {
                htmlRowLastSrch += '<input type="text" class="searchTerm clsLastSearch" id="txt_LastSearch_' + (totaltextboxcount) + '" placeholder="Search by Name" value="' + list[(list.length - 1)].searchKeyValue.split(':')[1] + '" />';
                $("#dvlstSrch").append(htmlRowLastSrch);
            }
            else if (totaltextboxcount == $("#MaxSearchLimit").val() && $("#txt_AnchorName").val() != "") {
                $("#txt_LastSearch_0").remove();
                htmlRowLastSrch += '<input type="text" class="searchTerm clsLastSearch" id="txt_LastSearch_' + (totaltextboxcount) + '" placeholder="Search by Name" value="' + list[(list.length - 1)].searchKeyValue.split(':')[1] + '" />';
                $("#dvlstSrch").append(htmlRowLastSrch);
            }
            for (var i = 0; i < list.length; i++) {

            }
            
        },
        error: function () {
            alert("Error occured!!")
        }
    })
}
function funAddToBucket() {
    var chkCheckboxCnt = $('#tbl_EligibleInvDashboard').find('input[type="checkbox"]:checked');
    var TotalInvAmount = 0;
    if (chkCheckboxCnt.length == 0) {
        $("#lblAddToBucketError").show();
        return;
    }
    else {
        //$("#txt_TotalSelInvoices").val(chkCheckboxCnt.length);
        $('table [type="checkbox"]').each(function (i, chk) {
            if (chk.checked) {
                TotalInvAmount += Number($(this).closest("tr").find('td:eq(6)').text());//Invoice amount column
                
            }
        });
        $("#txt_TotalInvoicesAmount").text($("#TotalSelApprovedInvAmt").text());
        $("#txt_TotalSelInvoices").val($("#TotalSelInvCount").text());

       // $("#myModal").modal("show");
    }
}
function GetTodayDate() {
    var tdate = new Date();
    var currentDate = tdate.getDate().toString() + tdate.getMonth().toString() + tdate.getUTCFullYear().toString() + tdate.getHours() + tdate.getMinutes() + tdate.getSeconds();

    return currentDate;
}
function SetBucket() {
    var BketName = $("#txt_BucketName").val()
    var discount = $("#txt_Discount").val()
    var date = $("#txt_ValidDateTime").val()
  
    $("#lblErrMsgDate").show();

    if (BketName != '' && discount != '' && date != '') {
        var InvoiceIDArr = []; var InvoiceIdstr = "";

        var TotalInvoiceCount = $("#txt_TotalSelInvoices").val();
        $('#tbl_EligibleInvDashboard [type="checkbox"]').each(function (i, chk) {
            if (chk.checked) {
                var id = $(this).closest("tr").find('.clsRowInvoiceID').val();
                InvoiceIdstr += id + ",";
            }
        });
   //     var BucketName = $("#txt_BucketName").val();
        var BucketName = GetTodayDate();
        var DiscountValue = $("#txt_Discount").val();
        var ValidToDateTime = $("#txt_ValidDateTime").val();
        var AfterDiscountAmount = $("#txt_afterDiscountAmount").text();
        var TotalInvAmount = $("#txt_TotalInvoicesAmount").text();
        var TotalDiscountAmount = $("#txt_DiscountAmountForInv").text();

        var url = InsertbucketDetails;

        $.post(url, {
            InvoiceIDStr: InvoiceIdstr, BucketName: BucketName, DiscountPercentage: DiscountValue, ValidToDate: ValidToDateTime,
            TotalInvoiceCount: TotalInvoiceCount, TotalInvoiceAmount: TotalInvAmount, AfterDiscountAmount: AfterDiscountAmount, DiscountAmount: TotalDiscountAmount
        }, function (data) {

            if (data.successresult > 0) {
                $("#myModalDialog").hide();
                Common.CustomSuccessNotify("Invoices added to bucket");
                location.reload();
            }
        });

    }

   
    else { 
        if (BketName == '') {
            $("#lblErrMsgAssign").show();
        }
        else {
            $("#lblErrMsgAssign").hide();
        }

        if (discount == '') {
            $("#lblErrMsgDiscnt").show();
        }
        else {
            $("#lblErrMsgDiscnt").hide();
        }
        if (date == '') {
            $("#lblErrMsgDate").show();
        }
        else {
            $("#lblErrMsgDate").hide();
        }
    }
}

$('#btnAddtobucket').click(function () {
    $("#lblErrMsgDiscnt").hide();
    $("#lblErrMsgAssign").hide();
    $("#lblErrMsgDate").hide();

    $("#txt_BucketName").val('');
    $("#txt_Discount").val('');
    $("#txt_ValidDateTime").val('')

    $("#txt_afterDiscountAmount").html('')
    $("#txt_DiscountAmountForInv").html('')
});

    //$('.clsRowCheckbox').change(function () {
$('#tbl_EligibleInvDashboard').off().on("change", "input[type='checkbox']", function () {
    debugger;
         var TotalInvAmount = 0; var InvoicecalculateAmt = 0; var id; var CompanyID = 0; var row = 0; var SelInvoicesCount = ""; var TotalInvAppAmount = 0; var ApproxDateOfApproval =""
         CompanyID = $(this).closest("tr").find('.clsCompanyId').val();
         $(".clsTotalInvoiceCalAmt_" + CompanyID).val('');
         $('#tbl_EligibleInvDashboard [type="checkbox"]').each(function (i, chk) {
            if (chk.checked) {

                TotalInvAmount += Number($(this).closest("tr").find('.clsInvoiceAmt').text().replace(/\,/g, ''));//Invoice amount column
                TotalInvAppAmount += Number($(this).closest("tr").find('.clsApprovedAmt').text().replace(/\,/g, ''));
                id = $(this).closest("tr").find('.clsRowInvoiceID').val();//td:eq(9)
                var daysToApprove = $(this).closest("tr").find('.clsInvApprovePayDay').val();
                //dateinNewFormat.setDate(dateinNewFormat.getDate() + parseInt(daysToApprove));
                var startDate = new Date();
                var ApprovalDate = "", noOfDaysToAdd = parseInt(daysToApprove), count = 0;
                while (count < noOfDaysToAdd) {
                    ApprovalDate = new Date(startDate.setDate(startDate.getDate() + 1));
                    if (ApprovalDate.getDay() != 0 && ApprovalDate.getDay() != 6) {
                        count++;
                    }
                }
                var dd = ApprovalDate.getDate();
                var mm = ApprovalDate.getMonth() + 1;
                var y = ApprovalDate.getFullYear();
                ApproxDateOfApproval = y + '-' + mm + '-' + dd;
                var Days = Common.DiffrenceBetweenDays($(this).closest("tr").find('.clsPaymentDueDate').text(), ApproxDateOfApproval);

                var CalCulatedValueByFormula = ((Days / 365) * $(this).closest("tr").find('.clsApprovedAmt').text().replace(/\,/g, '')).toFixed(2);
                $('.clsFormulaRow_' + id + '').val(CalCulatedValueByFormula);

                InvoicecalculateAmt += Number($('.clsFormulaRow_' + id + '').val());
                $("#hdnValuesForTX").val(InvoicecalculateAmt);
                $(".clsTotalInvoiceCalAmt_" + CompanyID).val(InvoicecalculateAmt);
                ++row;
            }
           
         });
         $(".clsTotalInvoice_" + CompanyID).text('');
         $(".clsTotalInvoice_" + CompanyID).text(TotalInvoice + '(' + row + ')');
         $(".clsTotalInvoiceAmt_" + CompanyID).val(TotalInvAmount)
         $(".clsAppTotalInvoiceAmt_" + CompanyID).val(TotalInvAppAmount)
         if ($(this).prop("checked") == true) {
             InvoiceID.push([parseInt($(this).closest("tr").find('.clsRowInvoiceID').val()), parseInt(CompanyID)]);
             CompInvID.push(parseInt($(this).closest("tr").find('.clsRowInvoiceID').val()));
             arrPaymentDate.push(parseInt($(this).closest("tr").find('.clsRowInvoiceID').val())+'_'+ApproxDateOfApproval);
        }
         else {
             for (var i = 0; i < InvoiceID.length;) {
                 if (InvoiceID[i][0] == parseInt($(this).closest("tr").find('.clsRowInvoiceID').val())) {
                     InvoiceID.splice(i, 1);
                     CompInvID.splice(i, 1);
                     arrPaymentDate.splice(i, 1);
                 }
                 else {
                     i++;
                 }
             }
         }
         
         var Value = JSON.stringify(CompInvID);
         $("#companyInvoicesID").val(Value);
         if ($('#tbl_EligibleInvDashboard').find('input[type="checkbox"]:checked')[0] != undefined) {
             $('.clsCompanyCheck_' + CompanyID).prop('checked', true);
             if (jQuery.inArray(parseInt(CompanyID), CompanyIDs) == -1) {
                 CompanyIDs.push(parseInt(CompanyID))
             }
          
         }
         else {
             $('.clsCompanyCheck_' + $(this).closest("tr").find('.clsCompanyId').val()).prop('checked', false);
             CompanyIDs.splice($.inArray(parseInt(CompanyID), CompanyIDs), 1);
         }

         $("#lbTotalSelectInvoices").text(0);
         $("#lbTotalSelectInvoicesAmt").text(0);
         $("#lbTotalSelectInvoicesAppAmt").text(0);
         $('#tbl_AnchorCompList [type="checkbox"]').each(function (i, chk) {
             if (chk.checked) {
                 if ($(this).closest("tr").find('.clsTotalInvoice').text().indexOf('(') != -1) {
                     $("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) + Number($(this).closest("tr").find('.clsTotalInvoice').text().split('(')[1].split(')')[0]));
                 }
                 else {
                     $("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) + Number($(this).closest("tr").find('.clsTotalInvoice').text()))
                 }
                 $("#lbTotalSelectInvoicesAmt").text(parseFloat($("#lbTotalSelectInvoicesAmt").text()) + Number($(this).closest("tr").find('.clsInvTotalAmt').val().replace(/\,/g, '')));
                 $("#lbTotalSelectInvoicesAppAmt").text(parseFloat($("#lbTotalSelectInvoicesAppAmt").text()) + Number($(this).closest("tr").find('.clsAppInvTotalAmt').val().replace(/\,/g, '')));
             }
         });

         $("#TotalSelApprovedInvAmt").text(TotalInvAppAmount);
        
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
$(document).on("focusout", "input[id='txt_Discount'],input[id='txt_DiscountPerInv']", function () {
    
    if ($("#txt_ValidDateTime").val() != "" || $("#txt_ValidDateTimePerInv").val() != "") {
        AssignDiscountValue();
    }
    if ($("#txt_Discount").val() == "") {
        $("#txt_afterDiscountAmount").text('');
        $("#txt_DiscountAmountForInv").text('');
    }
});
$(document).on("change", "input[id='txt_ValidDateTime'],input[id='txt_ValidDateTimePerInv']", function () {
    
    if ($("#txt_Discount").val() != "" || $("#txt_DiscountPerInv").val() != "") {
        AssignDiscountValue();
    }
    if ($("#txt_ValidDateTime").val() == "") {
        $("#txt_afterDiscountAmount").text('');
        $("#txt_DiscountAmountForInv").text('');
    }
});

function AssignDiscountValue() {
    var DiscountValue = $("#txt_Discount").val();
    var TotalInvAmt = $("#txt_TotalInvoicesAmount").text();
    var DiscountAmount = $("#hdnValuesForTX").val() * ($("#txt_Discount").val()/100);
    var AmountAfterDiscount = TotalInvAmt - DiscountAmount
    $("#txt_afterDiscountAmount").text(AmountAfterDiscount);
    $("#txt_DiscountAmountForInv").text(DiscountAmount);

    var DiscountValuePerInv = $("#txt_DiscountPerInv").val();
    var TotalInvAmtPerInv = $("#txt_TotalInvoicesAmountPerInv").text();
    var DiscountAmountPerInv = $("#hdnValuesForTXPerInv").val();
    var AmountAfterDiscountPerInv = TotalInvAmtPerInv - DiscountAmountPerInv
    $("#txt_afterDiscountAmountPerInv").text(AmountAfterDiscountPerInv);
    $("#txt_DiscountAmountPerInv").text(DiscountAmountPerInv);
}

function DownloadInvoice() {
    
    var AnchorCompName = $('#lblCompanyName').val().trim();
   
    url = "../Vendor/ExportRecordOfInvoice?AnchorCompanyName=" + AnchorCompName;
    window.location.href = url;
}

$(document).on("click", "a[href='#hrefApplyDiscount']", function () {
    var TotalInvAmount = 0; var InvoicecalculateAmt = 0;
   
    $("#lbl_InvPOIDPerInv").text($(event.target).closest("tr").find('.clsPONumber').text());
    $("#lblInvoiceId").text($(event.target).closest("tr").find('.clsInvoiceNo').text());
    $("#lbl_AnchorCompName").text($(event.target).closest("tr").find('.clsCompanyName').text());
    $("#lblInvCreationDate").text($(event.target).closest("tr").find('.clsCreatedDate').text());
    $("#lblInvAmount").text($(event.target).closest("tr").find('.clsInvoiceAmt').text().replace(/\,/g, ''));
    $("#lbl_TotalSelInvoicesPerInv").text(1);
    $("#txt_TotalInvoicesAmountPerInv").text($(event.target).closest("tr").find('.clsInvoiceAmt').text().replace(/\,/g, ''));
    var id = $(event.target).closest("tr").find('.clsRowInvoiceID').text()
  
    var daysToApprove = $(event.target).closest("tr").find('.clsInvApprovePayDay').val();
    var InvApprovedDate = new Date($(event.target).closest("tr").find('.clsInvApprovedDate').val());
    var numberOfDaysToAdd = parseInt(daysToApprove);
    InvApprovedDate.setDate(InvApprovedDate.getDate() + numberOfDaysToAdd); 

    var dd = InvApprovedDate.getDate();
    var mm = InvApprovedDate.getMonth() + 1;
    var y = InvApprovedDate.getFullYear();

    var ApproxDateOfApproval = y + '-' + mm + '-' + dd;

    var dtPaymentDueDate = new Date($(event.target).closest("tr").find('.clsPaymentDueDate').text())
    var PaymentdueDateinNewFormat = dtPaymentDueDate.getFullYear() + '-' + (dtPaymentDueDate.getMonth() + 1) + '-' + dtPaymentDueDate.getDate()
    console.log(dtPaymentDueDate);
    console.log(ApproxDateOfApproval);
    var Days = Common.DiffrenceBetweenDays(PaymentdueDateinNewFormat, ApproxDateOfApproval);
    console.log(Days);
    var CalCulatedValueByFormula = ((Days / 365) * $(event.target).closest("tr").find('td:eq(5)').text().replace(/\,/g, '')).toFixed(2);

    $("#hdnValuesForTXPerInv").val(CalCulatedValueByFormula);

    $("#DiscountByInvoice").modal('show');

});
function SetDiscountForInv() {
    var InvoiceIDArr = []; var InvoiceIdstr = "";
    
    var TotalInvoiceCount = 1;
    
    var BucketName = $("#txt_BucketNamePerInv").val();
    var DiscountValue = $("#txt_DiscountPerInv").val();
    var ValidToDateTime = $("#txt_ValidDateTimePerInv").val();
    var AfterDiscountAmount = $("#txt_afterDiscountAmountPerInv").text();
    var TotalInvAmount = $("#txt_TotalInvoicesAmountPerInv").text();
    var TotalDiscountAmount = $("#txt_DiscountAmountPerInv").text();




    var url = InsertbucketDetails;

    $.post(url, {
        InvoiceIDStr: 1, BucketName: BucketName, DiscountPercentage: DiscountValue, ValidToDate: ValidToDateTime,
        TotalInvoiceCount: TotalInvoiceCount, TotalInvoiceAmount: TotalInvAmount, AfterDiscountAmount: AfterDiscountAmount, DiscountAmount: TotalDiscountAmount
    }, function (data) {

        if (data.successresult > 0) {
            $("#myModalDialog").modal('hide');
            Common.CustomMessageShow("Discount Offered Successfully to Selected Invoices, You can check status of Invoices in  Discount Offered Invoices Section");
            //location.reload();
        }
    });
}