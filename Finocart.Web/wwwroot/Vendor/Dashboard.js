var InvoiceID = []; var CompInvID = []; var AnchCompanyID = []; var CompanyIDs = []; var TotalSelectedInvAmt = 0; var TotalInvoice = 0; var arrPaymentDate = [];
$(document).ready(function () {
    debugger
    //window.history.pushState(null, "", window.location.href);
    //window.onpopstate = function () {
    //    window.history.pushState(null, "", window.location.href);
    //};

    //$('#tbl_AnchorCompList').dataTable({
    //    "language": {
    //        "decimal": ",",
    //        "thousands": "."
    //    }
    //});

    $("#lbTotalSelectInvoices").text(0);
    $('#tbl_AnchorCompList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetAnchorCompListByVendorID,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            {
                "data": function (data, type, row) {
                    //AnchCompanyID//index 0
                    //CompanyIDs

                    if (CompanyIDs.length != 0) {
                        if ($.inArray(data.companyID, CompanyIDs) >= 0) {
                            return "<input type='checkbox' checked class='select-checkbox clsCompanyCheck_" + data.companyID +"'/>"
                        }
                    }
                    //return "<input type='checkbox' class='clsRowCheckbox'/>"
                    return "<input type='checkbox' class='select-checkbox clsCompanyCheck_" + data.companyID+"'/>";
                }
            },
            { "data": "companyID", "name": "CompanyID", "autoWidth": true, "class": "clsAnchCompanyID", "searchable": false }, //index 1
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2   
            {
                "data": "totalInvoice", "name": "TotalInvoice", "autoWidth": true, "class": "clsTotalInvoice"    //index 3            
            },
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, "class": "clsTotalInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 4
            { "data": "totalInvoiceAppAmount", "name": "TotalInvoiceAppAmount", "autoWidth": true, "class": "clsTotalInvoiceAppAmount", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 5
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsInvTotalAmt clsTotalInvoiceAmt_" + data.companyID +"' value=''/>"; //index 6  //Get Total Selected Invoice Amount
                }
            },
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsAppInvTotalAmt clsAppTotalInvoiceAmt_" + data.companyID + "' value=''/>"; //index 7  //Get Total Selected Invoice Amount
                }
            },
            {
                "data": function (data, type, row) {
                    return "<input type='hidden' class='clsInvCalAmt clsTotalInvoiceCalAmt_" + data.companyID + "' value=''/>"; //index 8  //Get Total Invoice Amount After Calculate
                }
            },
            {
                "data": function (data, type, row) {
                    return "<a onclick='GetInvoice(" + data.companyID + ",this," + data.totalInvoice + ")' class='actions-ico getInvoice clsEligibleInv'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
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
                $(td).addClass('clsTotalInvoice_' + data.companyID);
            }
        },
        {
                "targets": [6,7,8],
                "className": "hide-column"
        }
        ],
        "order": [
            [1, "asc"],
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lbTotalInvoices").text(api.column('.clsTotalInvoice').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            $("#lbTotalInvoicesAmt").text(api.column('.clsTotalInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));            
        }
    });

    oTable1 = $('#tbl_AnchorCompList').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(3).search($('#txt_CompanyName').val().trim());
        oTable1.columns(6).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });
    $('.searchTerm').on('keyup', function () {
        debugger;
        oTable1.columns(3).search($('#txt_CompanyName').val().trim());
        oTable1.columns(6).search($('#txt_TotalApprovedINV').val().trim());
        oTable1.draw();
    });
    $('#ExportAnchCompCSV').click(function () {
        var CompanyName = $('#txt_CompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_TotalApprovedINV').val().trim();
        url = "../Vendor/ExportAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });

    $('#AnchorComp-select-all').on('click', function () {
        // Check/uncheck all checkboxes in the table
        var rows = oTable1.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    $('#tbl_AnchorCompList').off().on("change", "input[type='checkbox']", function () {
        var CompanyID = parseInt($(this).closest("tr").find('.clsAnchCompanyID').text())
        if ($(this).prop("checked") == true) {
            //if (AnchCompanyID.indexOf(parseInt($(this).closest("tr").find('.clsAnchCompanyID').text())) == -1) {
            $("#lbTotalSelectInvoices").text(parseInt($("#lbTotalSelectInvoices").text()) + parseInt($(this).closest("tr").find('.clsTotalInvoice').text()));
            $(".clsTotalInvoiceAmt_" + CompanyID).val(Number($(this).closest("tr").find('.clsTotalInvoiceAmount').text().replace(/\,/g, '')));
            $(".clsAppTotalInvoiceAmt_" + CompanyID).val(Number($(this).closest("tr").find('.clsTotalInvoiceAppAmount').text().replace(/\,/g, '')));
            //}
            var InvoicecalculateAmt = 0;
            $.ajax({
                type: "Post",
                dataType: "json",
                url: "../Vendor/GetEligibleInvoices?CompanyId=" + CompanyID,
                //data: { CompanyId: CompanyID },
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function (data) {
                    debugger;
                    $.each(data.data, function (i) {
                        /*TotalInvAmount += Number($(this).closest("tr").find('.clsInvoiceAmt').text());*///Invoice amount column
                        id = data.data[i].invoiceID;
                        //var InvApprovedDate = data.data[i].invoiceApprovalDate;
                        //var dateinNewFormat = new Date(InvApprovedDate);
                        //var dateinNewFormat = new Date();
                        var daysToApprove = data.data[i].invoiceApprovePayDays;
                        //dateinNewFormat.setDate(dateinNewFormat.getDate() + parseInt(daysToApprove));
                        var startDate = new Date();
                        var holidayDate = new Date();
                        //startDate = new Date(startDate.replace(/-/g, "/"));
                        var ApprovalDate = "", noOfDaysToAdd = parseInt(daysToApprove), count = 0;
                        while (count < noOfDaysToAdd) {
                            ApprovalDate = new Date(startDate.setDate(startDate.getDate() + 1));
                            if (ApprovalDate.getDay() != 0 && ApprovalDate.getDay() != 6) {
                                count++;
                            }
                        }
                        //while (count < noOfDaysToAdd) {
                        //    ApprovalDate = new Date(startDate.setDate(startDate.getDate() + 1));
                        //    if (ApprovalDate.getDay() != 0 && ApprovalDate.getDay() != 6) {
                        //        $.each(data.data1, function (i) {
                        //            if (data.data1[i].date != (ApprovalDate.getDate() + '/' + (ApprovalDate.getMonth() + 1) + '/' + ApprovalDate.getFullYear())) {
                        //                count++;
                        //            }
                        //            return false;
                        //        });
                        //    }
                        //}
                        var dd = ApprovalDate.getDate();
                        var mm = ApprovalDate.getMonth() + 1;
                        var y = ApprovalDate.getFullYear();
                        var ApproxDateOfApproval = y + '-' + mm + '-' + dd;
                        var Days = Common.DiffrenceBetweenDays(data.data[i].paymentDueDate, ApproxDateOfApproval);

                        var CalCulatedValueByFormula = ((Days / 365) * data.data[i].approvedAmt).toFixed(2);
                        $('.clsFormulaRow_' + id + '').val(CalCulatedValueByFormula);

                        InvoicecalculateAmt += Number(CalCulatedValueByFormula);
                        InvoiceID.push([id, data.data[i].companyId]);
                        CompInvID.push(id)
                        arrPaymentDate.push(id+'_'+ApproxDateOfApproval);
                    });
                    $(".clsTotalInvoiceCalAmt_" + CompanyID).val(InvoicecalculateAmt);
                    var Value = JSON.stringify(CompInvID);
                    $("#companyInvoicesID").val(Value);
                    if (jQuery.inArray(parseInt(CompanyID), CompanyIDs) == -1) {
                        CompanyIDs.push(CompanyID);
                    }

                },
                error: function () {
                    alert("Error occured!!")
                }
            });
        }
        else {
            if ($("#companyInvoicesID").val() != "") {
                CompInvID = JSON.parse($("#companyInvoicesID").val());
            }
            for (var i = 0; i < InvoiceID.length;) {
                if (InvoiceID[i][1] == CompanyID) {                    
                    InvoiceID.splice(i, 1);
                    CompInvID.splice(i, 1);
                    arrPaymentDate.splice(i, 1);
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
            $(".clsTotalInvoiceAmt_" + CompanyID).val('');
            $(".clsAppTotalInvoiceAmt_" + CompanyID).val('');
            $(".clsTotalInvoiceCalAmt_" + CompanyID).val('');
            CompanyIDs.splice($.inArray(parseInt(CompanyID), CompanyIDs), 1);
        }  
        $("#lbTotalSelectInvoicesAmt").text(0);
        $("#lbTotalSelectInvoicesAppAmt").text(0);
        $('#tbl_AnchorCompList [type="checkbox"]').each(function (i, chk) {
            if (chk.checked) {
                $("#lbTotalSelectInvoicesAmt").text(parseFloat($("#lbTotalSelectInvoicesAmt").text()) + Number($(this).closest("tr").find('.clsInvTotalAmt').val().replace(/\,/g, '')));
                $("#lbTotalSelectInvoicesAppAmt").text(parseFloat($("#lbTotalSelectInvoicesAppAmt").text()) + Number($(this).closest("tr").find('.clsAppInvTotalAmt').val().replace(/\,/g, '')));
            }
        });
    });

    $('#btnOfferDiscount').on('click', function () {
        $("#lblVldDateMsgErr").hide();
        $("#lblDiscuntMsgErr").hide();
        $("#lblInvldAmt").hide();

        $("#txtValidDateTime").val('')
        $("#txt_OfferDiscount").val('') 
        $("#txt_afterDiscount").html('')      
        $("#txt_OfferDiscountAmount").html('')

        var TotalCalInvAmt = 0;
        $("#txt_TotalSelectInvoices").val($("#lbTotalSelectInvoices").text())
        $("#txt_TotalSelInvoicesAmount").text(parseInt($("#lbTotalSelectInvoicesAppAmt").text()).toLocaleString('en'))
        $('#tbl_AnchorCompList [type="checkbox"]').each(function (i, chk) {
            if (chk.checked) {
                TotalCalInvAmt += Number($(this).closest("tr").find('.clsInvCalAmt').val().replace(/\,/g, ''));
                if (this.id == 'AnchorComp-select-all') {
                    $('#OfferDiscount').modal('hide');
                    $('#lblOfferDiscount').show();
                    $('#lblOfferDiscount').hide();
                   // return false;
                }
                else
                {                  
                    $('#OfferDiscount').modal('show');
                    $('#lblOfferDiscount').hide();
                    return false;
                }
                
            }
            else {

                $('#lblOfferDiscount').show();
              //  $('#OfferDiscount').show();
            }

        });
        $("#TotalCalcInvAmt").val(TotalCalInvAmt);
        
    });

    $("#OfferDiscountCompute").on().click(function () {
        debugger;
        var val = $("#txt_OfferDiscount").val();
        var valdate = $("#txtValidDateTime").val();
        var valdiscunt = $("#txt_afterDiscount").val();
        if (val >100 || val < 0) {
            $("#lblInvldAmt").show();
            if (val == "" || val == 0) {
                $("#lblDiscuntMsgErr").hide();
            }
        }
        else {
            if (val == "" || val == 0) {
                $("#lblDiscuntMsgErr").show();

                if (valdate == "") {
                    $("#lblVldDateMsgErr").css("display", "block");;
                }
                else {
                    var InvoicecalAmt = parseFloat($("#TotalCalcInvAmt").val());
                    $("#txt_OfferDiscountAmount").text(Math.round((InvoicecalAmt * $("#txt_OfferDiscount").val()) / 100).toLocaleString('en'));
                    $("#txt_afterDiscount").text(Math.round($("#txt_TotalSelInvoicesAmount").text().replace(/,/g, '') - $("#txt_OfferDiscountAmount").text().replace(/,/g, '')).toLocaleString('en'));
                    if ($("#txt_afterDiscount").val() >= 0) {
                        $("#lblVldDateMsgErr").hide();
                        $("#lblDiscuntMsgErr").hide();
                        $("#lblInvldAmt").hide();
                    }
                }
            }
            else {
                if (valdate == "") {
                    $("#lblVldDateMsgErr").css("display", "block");;
                }
                else {
                    var InvoicecalAmt = parseFloat($("#TotalCalcInvAmt").val());
                    $("#txt_OfferDiscountAmount").text(Math.round((InvoicecalAmt * $("#txt_OfferDiscount").val()) / 100).toLocaleString('en'));
                    $("#txt_afterDiscount").text(Math.round($("#txt_TotalSelInvoicesAmount").text().replace(/,/g, '') - $("#txt_OfferDiscountAmount").text().replace(/,/g, '')).toLocaleString('en'));
                    if ($("#txt_afterDiscount").val() >= 0) {
                        $("#lblVldDateMsgErr").hide();
                        $("#lblDiscuntMsgErr").hide();
                        $("#lblInvldAmt").hide();
                    }
                }
            }
        }
    });

    $('#liElgibleInvoice').click(function () {
        $('#lblOfferDiscount').hide();
       
    });


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
            { "data": "companyID", "name": "CompanyID", "autoWidth": true }, //index 1
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2   
            { "data": "totalInvoice", "name": "TotalInvoice", "autoWidth": true, "class": "clsTotalInvoice" },
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, "class": "clsTotalDiscInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0)  },
            {
                "data": function (data, type, row) {
                    //return "<a href='../AnchorCompany/GetVendorInvoiceDetailsViewByVendorID/?icompanyID=" + data.companyID + "' class='actions-ico'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                    return "<a onclick='GetInvoice(" + data.companyID + ",this)' class='actions-ico getInvoice clsDiscountOfferedInv'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }

        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $("#lbTotalInvoices1").text(api.column('.clsTotalInvoice').data().reduce(function (a, b) {return Number(a) + Number(b);}, 0));
            $("#lbTotalInvoicesAmt1").text(api.column('.clsTotalDiscInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));           
        }
    });

    oTable2 = $('#tbl_DiscountAnchorCompList').DataTable();
    $('#btnDiscCompFilter').click(function () {
        oTable2.columns(2).search($('#txt_DiscCompanyName').val().trim());
        oTable2.columns(4).search($('#txt_DiscTotalApprovedINV').val().trim());
        oTable2.draw();
    });
    $('.searchTerm').on('keyup', function () {
        debugger;
        oTable2.columns(2).search($('#txt_DiscCompanyName').val().trim());
        oTable2.columns(4).search($('#txt_DiscTotalApprovedINV').val().trim());
        oTable2.draw();
    });
    $('#ExportDiscAnchCompCSV').click(function () {
        var CompanyName = $('#txt_DiscCompanyName').val().trim();
        var TotalInvoiceAmt = $('#txt_DiscTotalApprovedINV').val().trim();
        url = "../Vendor/ExportDiscAnchorCompListByVendorID?CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
        window.location.href = url;
    });

    

});

function ExportEligInvoiceCSV() {
    var CompanyID = $("#Item3_CompanyId").val()
    var CompanyName = $('#txt_DiscCompanyName').val().trim();
    var TotalInvoiceAmt = $('#txt_DiscTotalApprovedINV').val().trim();
    url = "../Vendor/ExportEligibleInvoice?CompanyID=" + CompanyID + "&CompanyName=" + CompanyName + "&TotalInvoiceAmt=" + TotalInvoiceAmt;
    window.location.href = url;
}

function GetInvoice(id, data, total) {
    
    TotalInvoice = total;
    var classname = data.classList[2];
    var PageName = "";
    if ((classname.toLowerCase().indexOf('eligibleinv')>0)) {
        PageName = "EligibleInv";
    }
    else if ((classname.toLowerCase().indexOf('discountofferedinv')>0)) {
        PageName = "DiscountOfferedInv";
    }
    $.ajax({
        type: "Post",
        dataType: "html",
        url: "../Vendor/GetEligibleInvoicesView?id=" + id + "&PageName=" + PageName,
        //data: { EmailID: $("#Email").val(), Module: 'User' },
        //contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (PageName == "EligibleInv") {
                $("#eEligibleInvoices").remove();
                $("#eAnchorCompInvoices").hide();
                $("#eInvoices").append(data);
            }
            else if (PageName == "DiscountOfferedInv") {
                $("#iDiscOfferedInv").remove();
                $("#dOfferedInvoices").hide();
                $("#dInvoices").append(data);
            }
            
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

function GetInvoiceJourney(InvoiceId,data) {
    var PageName = data.dataset.para1;
    $("#dvMaintainEligibleInvJourney").empty();
    $.ajax({
        type: "Post",
        dataType: "html",
        url: "../Vendor/GetInvoiceJourneyHistory?InvoiceId=" + InvoiceId + "&PageName=" + PageName,
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            $("#dvMaintainInvJourney").html('');
            
            var data = JSON.parse(data);
            var PageName = data.pageName;
            var successdata = data.data;

            var htmlRowLastSrch = "";
            if (PageName == "DiscountOffered") {
                for (var i = 0; i < successdata.length; i++) {
                    htmlRowLastSrch = '<div class="cont"><div id="div_' + i + '_' + successdata[i].invoicestatusvalue + '">'
                        + '<div class="row"><div class="col-xs-12 col-sm-8"><div><label>INVOICE STATUS : <span id="lbl_' + i + '_' + successdata[i].invoiceStatusText + '">' + successdata[i].invoiceStatusText + '</span></label></div ></div ><div class="col-xs-12 col-sm-4" style="padding: 0;"><div><label>DATE : <span id="lbl_' + i + '_' + successdata[i].createdDate + '">' + successdata[i].createdDate + '</span></label></div></div ></div > '
                        + '<div><hr></div>'
                        + '<div><label>PERSON NAME : <span id="lbl_' + i + '_' + successdata[i].createdUser + '">' + successdata[i].createdUser + '</span></label></div>'
                        + '<div><label>EMAIL ID : <span id="lbl_' + i + '_' + successdata[i].email + '">' + successdata[i].email + '</span></label></div>'
                        + '<div><label>REMARK : <span id="lbl_' + i + '_' + successdata[i].remark + '">' + successdata[i].remark + '</span></label></div>'
                        + '<div><label>SENT TO : <span id="lbl_' + i + '_' + successdata[i].sentToPerson + '">' + successdata[i].sentToPerson + '</span></label></div>'
                        + '</div><div>';
                    $("#dvMaintainDiscInvJourney").append(htmlRowLastSrch);
                }
                $("#CompanyName").text(successdata[0].company_name);
                $("#lblPOID").text(successdata[0].poNumber);
                $("#lblInvCreateDate").text(successdata[0].invoiceCreatedDate);
                $("#lblDiscOfferedDate").text(successdata[0].createdDate);

                $("#DiscInvoiceJourney").modal('show');
            }
            else if (PageName == "EligibleInvoice") {
                for (var i = 0; i < successdata.length; i++) {
                    var date = new Date(successdata[i].invoiceCreatedDate );
                    var month = date.getMonth() + 1;
                    
                    var date1 = new Date(successdata[i].paymentDueDate);
                    var month1 = date.getMonth() + 1;
                    
                    htmlRowLastSrch = '<div class="cont"><div id="div_' + i + '_' + successdata[i].invoicestatusvalue + '"><div><p>Invoice Creation Date</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].invoiceCreatedDate + '">' + date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear() + '</label></div>'
                        + '<div><p>Invoice Due  Date</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].paymentDueDate + '">' + date1.getDate().toString() + "/" + (month1.length > 1 ? month1 : "0" + month1) + "/" + date1.getFullYear() + '</label></div>'
                        + '<div><p>Invoice Status</p><span>:</span><label id="lbl_' + i + '_' + successdata[i].invoiceStatusText + '">' + successdata[i].invoiceStatusText + '</label></div>'
                        + '</div><div>';
                    $("#dvMaintainEligibleInvJourney").append(htmlRowLastSrch);
                }
                $("#ElgibleInvCompanyName").text(successdata[0].company_name);
                $("#lblElgInvPOID").text(successdata[0].poNumber);
               // $("#lblElgInvCreateDate").text(successdata[0].invoiceCreatedDate);
                
                $("#EligibleInvoiceJourney").modal('show');
            }

        },
        error: function () {
            alert("Error occured!!")
        }
    });
}
function GetTodayDate() {
    var tdate = new Date();
    var currentDate = tdate.getDate().toString() + tdate.getMonth().toString() + tdate.getUTCFullYear().toString() + tdate.getHours() + tdate.getMinutes() + tdate.getSeconds();

    return currentDate;
}
function CompanyOfferDiscount() {
    debugger;
    var val = $("#txt_OfferDiscount").val();
    var valdate = $("#txtValidDateTime").val();
    var valdiscunt = $("#txt_afterDiscount").val();
    
    var BucketName = GetTodayDate();

    if (val == "" || val == 0) {
        $("#lblDiscuntMsgErr").show();
        if (valdate == "") {
            $("#lblVldDateMsgErr").css("display", "block");;
        }
    }
    else {
        if (valdate == "") {
            $("#lblVldDateMsgErr").css("display", "block");;
        }
        var InvoiceIdstr = ""; var PaymentDate = "";

        var TotalInvoiceCount = $("#txt_TotalSelectInvoices").val();
       

    
        for (var i = 0; i < CompInvID.length; i++) {
            InvoiceIdstr += CompInvID[i] + ",";
            PaymentDate += arrPaymentDate[i] + ",";
        }
        var BucketName = $("#txt_BucketName").val();
        var DiscountValue = $("#txt_OfferDiscount").val();
        var ValidToDateTime = $("#txtValidDateTime").val();
        var AfterDiscountAmount = $("#txt_afterDiscount").text().replace(/,/g, '');
        var TotalInvAmount = $("#txt_TotalSelInvoicesAmount").text().replace(/,/g, '');
        var TotalDiscountAmount = $("#txt_OfferDiscountAmount").text().replace(/,/g, '');

        //var bucketInvoiceModel = {
        //    InvoiceIDStr: InvoiceIdstr, BucketName: BucketName, DiscountPercentage: DiscountValue, ValidToDate: PaymentDate,
        //    TotalInvoiceCount: TotalInvoiceCount, TotalInvoiceAmount: TotalInvAmount, AfterDiscountAmount: AfterDiscountAmount, DiscountAmount: TotalDiscountAmount
        //}  

        //$.ajax({
        //    type: "POST",
        //    data: JSON.stringify({
        //        "bucketInvoiceModel": bucketInvoiceModel
        //    }),
        //    url: InsertbucketDetails,
            
        //    contentType: 'application/json; charset=utf-8',
        //    datatype: "json",
        //    cache: false,  
        //    async: false,
        //    success: function (data) {
        //        debugger;
        //        if (data.successresult > 0) {
        //            $("#myModalDialog").hide();
        //            Common.CustomSuccessNotify("Invoices added to bucket");
        //            alert("Invoices added to bucket");
        //            location.reload();
        //        }
        //    },
        //    error: function () {
        //        alert("Error occured!!")
        //    }
        //}); 
        var url = InsertbucketDetails;

        $.post(url, {
            InvoiceIDStr: InvoiceIdstr, BucketName: BucketName, DiscountPercentage: DiscountValue, ValidToDate: PaymentDate,
            TotalInvoiceCount: TotalInvoiceCount, TotalInvoiceAmount: TotalInvAmount, AfterDiscountAmount: AfterDiscountAmount, DiscountAmount: TotalDiscountAmount
        }, function (data) {

            if (data.successresult > 0) {
                $("#myModalDialog").hide();
                Common.CustomSuccessNotify("Invoices added to bucket");
                alert("Invoices added to bucket");
                location.reload();
            }
        });
    
    }
    
}