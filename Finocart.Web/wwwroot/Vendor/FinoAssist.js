

$(document).ready(function () {

    var CompanyIDstr = "";
    var date_input = $('#txtdate'); //our date input has the name "date"
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    

    $.ajax({
        type: "get",
        datatype: "json",
        async: false,
        url: GetMinFundPaymentDate,
        data: '',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var dateToday = new Date(data.data);
            date_input.datepicker({
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                numberOfMonths: 2,
                showButtonPanel: true,
                minDate: dateToday,
                'startDate': dateToday,
                daysOfWeekDisabled: [0, 6],
                beforeShowDay: function (Date) {
                    var forbidden = GetHolidays();
                    var curr_day = Date.getDate();
                    var curr_month = Date.getMonth() + 1;
                    var curr_year = Date.getFullYear();
                    var curr_date = curr_month + '/' + curr_day + '/' + curr_year;
                    if (forbidden.indexOf(curr_date) > -1) return false;
                }
            })
        }
    });
  //  $("#txtFundingDate").datepicker({ minDate: 0 });
    $("#DiscountCompute").off().click(function () {
        debugger
        var errormsg = false;
        if ($("#txtFundingDate").val() == "")
        {
            $("#lbldateError").show();
            errormsg = true;
        }
        if ($("#txtRequireFund").val() == "") {
            $("#lblamountError").show();
            errormsg = true;
        }
        if ($("#txtFunding").val() == "") {
            $("#lblfundingamountError").show();
            errormsg = true;
        }
        if ($("#txtDiscount").val() == "") {
            $("#lblDiscountError").show();
            errormsg = true;
        }
        var fundingDate = $("#txtFundingDate").val();
        if ($("#txtFundingDate").val() != "" && $("#txtRequireFund").val() != "" && $("#txtFunding").val() != "" && $("#txtDiscount").val() != "") {
            $.ajax({
                type: "get",
                url: GetInvoicesByAmount,
                data: { sumAssuredAmount: $("#txtFunding").val(), fundingDate:fundingDate, discount: $("#txtDiscount").val(), finassistType: $("#tabName").val(), anchoCompany: CompanyIDstr },
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function (data) {
                    var InvoiceDiscountAmt = 0; var InvoiceDisbAmt = 0; var ApprovedAmt = 0;

                    $.each(data.data, function (i) {
                       
                        InvoiceDiscountAmt += data.data[i].discountAmt;
                        InvoiceDisbAmt += data.data[i].disbursementAmt;
                        ApprovedAmt += data.data[i].approvedAmt;
                       
                    });
                    $("#txtDiscountAmount").val(Math.round(InvoiceDiscountAmt).toLocaleString('en'));
                    $("#txtActualAmount").val(Math.round(InvoiceDisbAmt).toLocaleString('en'));
                    $("#txtTotalApprovedAmount").val(Math.round(ApprovedAmt).toLocaleString('en'));

                },
                error: function () {
                    alert("Error occured!!")
                }
            });
        }

        if (errormsg == true) {
            return false;
        }
    });

    $('#txtFunding').keyup(function () {
        $('#txtRequireFund').val($('#txtFunding').val());
        $('input[type="range"]').val($('#txtRequireFund').val()).change();
    });
    $('#txtRequireFund').keyup(function () {
        $('#txtFunding').val($('#txtRequireFund').val());
        $('input[type="range"]').val($('#txtRequireFund').val()).change();
    });
    $('#txtRequireFund').keyup(function () {
        var val = $('#txtRequireFund').val();
        var rslt = val.includes('-')
        if (rslt == true) {
            $("#lblInvldNum").show();
        }
        else {
            $("#lblInvldNum").hide();
        }

    });


    SelectBox();

    $('#chkSelectALL').click(function () {
        SelectBox();
        if ($(this).is(":checked")) {
            $('.cls-checkbox').prop('checked','checked');
        } else {
            $('.cls-checkbox').prop('checked', '');
        }
    });

    $("#viewSelectedAmount").off().click(function () {
        var errormsg = false;
        if ($("#txtFundingDate").val() == "") {
            $("#lbldateError").show();
            errormsg = true;
        }
        if ($("#txtRequireFund").val() == "") {
            $("#lblamountError").show();
            errormsg = true;
        }
        if ($("#txtFunding").val() == "") {
            $("#lblfundingamountError").show();
            errormsg = true;
        }
        if ($("#txtDiscount").val() == "") {
            $("#lblDiscountError").show();
            errormsg = true;
        }
        if ($("#txtFundingDate").val() != "" && $("#txtRequireFund").val() != "" && $("#txtFunding").val() != "" && $("#txtDiscount").val() != "") {
            $('#tbl_AnchorCompInvoices').DataTable().destroy();
            $('#tbl_AnchorCompInvoices').dataTable({
                "processing": true,
                "serverSide": true,
                "searching": true,
                "dom": '"ltipr"',
                "ajax": {
                    "url": GetInvoicesListByAmount,
                    "data": { sumAssuredAmount: $("#txtFunding").val(), fundingDate: $("#txtFundingDate").val(), discount: $("#txtDiscount").val(), finassistType: $("#tabName").val(), anchoCompany: CompanyIDstr },
                    "type": "POST",
                    "datatype": "json",
                    "async": false
                },
                "columns": [
                    { "data": "poNumber", "name": "PONumber", "autoWidth": true, "class": "clsPONumber" }, //index 1
                    { "data": "invoiceNo", "name": "InvoiceNo", "autoWidth": true, "class": "clsInvoiceNo" }, //index 2
                    { "data": "anchorCompName", "name": "AnchorCompName", "autoWidth": true, "class": "clsCompanyName" }, //index 3
                    {
                        "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "class": "clsCreatedDate", "render": function (data) {
                            var date = new Date(data);
                            var month = date.getMonth() + 1;
                            return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                        }}, //index 4
                    { "data": "invoiceAmt", "name": "InvoiceAmt", "autoWidth": true, "class": "clsInvoiceAmt", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 5
                    { "data": "approvedAmt", "name": "ApprovedAmt", "autoWidth": true, "class": "clsApprovedAmt", render: $.fn.dataTable.render.number(',', '.', 0) }, //index 6
                    {
                        "data": "paymentDueDate", "name": "PaymentDueDate", "autoWidth": true, "class": "clsPaymentDueDate", "render": function (data) {
                            var date = new Date(data);
                            var month = date.getMonth() + 1;
                            return date.getDate().toString() + "/" + (month.length > 1 ? month :month) + "/" + date.getFullYear();
                        }}, //index 7
                    { "data": "Status", "defaultContent": "<i>Eligible for Discounting</i>", "autoWidth": true }, //index 8
                    { "data": "discount", "defaultContent": $("#txtDiscount").val(), "autoWidth": true }, //index 9
                    {
                        "data": "discountAmt", "name": "DiscountAmt", "autoWidth": true, "class": "clsDiscountAmt", render: function (data) { return parseInt(data.toFixed()).toLocaleString('en') }
                    }, //index 10  
                    { "data": "disbursementAmt", "name": "DisbursementAmt", "autoWidth": true, "class": "clsDisbursementAmt", render: function (data) { return parseInt(data.toFixed()).toLocaleString('en') } }, //index 10  
                ],
                "footerCallback": function (row, data, start, end, display) {
                    var api = this.api();
                    $("#lbTotalInvoices").text(parseInt(api.column('.clsApprovedAmt').data().reduce(function (a, b) { return (Number(a) + Number(b)).toFixed(); }, 0)).toLocaleString('en'));
                    $("#lbTotalInvoicesAmt").text(parseInt(api.column('.clsDiscountAmt').data().reduce(function (a, b) { return (Number(a) + Number(b)).toFixed(); }, 0)).toLocaleString('en'));
                    $("#lbTotalInvoicesAppAmt").text(parseInt(api.column('.clsDisbursementAmt').data().reduce(function (a, b) { return (Number(a) + Number(b)).toFixed(); }, 0)).toLocaleString('en'));
                }
            });
        }
        if (errormsg == true) {
            return false;
        }
    });



    $('#tbl_DiscountByInvoice').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": "../Vendor/GetAnchorCompListByVendorID",
            "type": "POST",
            "datatype": "json",
        },            
        "columns": [
            { "data":null },
            { "data": "companyID", "name": "CompanyID", "autoWidth": true }, //index 1
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2   
            { "data": "totalInvoice", "name": "TotalInvoice", "autoWidth": true },
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 2) },
            {
                "data": function (data, type, row) {
                    return "<input class='clsCompanyID' value='" + data.companyID + "'/>";
                }
            }
        ],
        'columnDefs': [{
            'targets': 0,
            //'targets': [5],
            'searchable': false,
            'orderable': false,
            //'className': 'dt-body-center',
            'className': 'dt-body-center',
            //'className': 'hide_column',
            'render': function (data, type, full, meta) {
                return '<input class="cls-checkbox" type="checkbox" name='+data.companyID+' value="' + $('<div/>').text(data).html() + '">';
            }

        },
        {
            'targets': 5,
            'className': 'hide_column',
        }

        ]
    });

    $('input[type="range"]').rangeslider({

        // Feature detection the default is `true`.
        // Set this to `false` if you want to use
        // the polyfill also in Browsers which support
        // the native <input type="range"> element.
        polyfill: false,

       

        // Callback function
        onSlide: function (position, value) {
            $("#txtRequireFund").val(value.toLocaleString('en'));
            $("#txtFunding").val(value.toLocaleString('en'));

        },

        // Callback function
        //onSlideEnd: function (position, value) {  }
    });

    $('input[type="range"]').attr({
        min: 0,
        max: $('#range').attr('max').replace(/,/g, ''),
        step: 1,
        value: 0
    });
    $('input[type="range"]').rangeslider('update', true);

    $("#ProceedSelInvoice").off().click(function () {
        var errormsg = false;
        if ($("#txtFundingDate").val() == "") {
            $("#lbldateError").show();
            errormsg = true;
        }
        if ($("#txtRequireFund").val() == "") {
            $("#lblamountError").show();
            errormsg = true;
        }
        if ($("#txtFunding").val() == "") {
            $("#lblfundingamountError").show();
            errormsg = true;
        }
        if ($("#txtDiscount").val() == "") {
            $("#lblDiscountError").show();
            errormsg = true;
        }
        if (errormsg == true) {
            return false;
        }
    
    });

    $("#ProceedSelAnchor").off().click(function () {
        if (CompanyIDstr != "") {
            if ($("#tabName").val() == "ByAnchorComp") {
              
                $("#byamount").addClass("active in");
                $("#byinvoice").removeClass("active in");
              
                $("#ProceedSelInvoice").show();
                $("#ProceedSelAnchor").hide();
                $("#viewSelectedAmount").show();

                $.ajax({
                    type: "get",
                    datatype: "json",
                    async: false,
                    url: MaxAvailableAmount,
                    data: { anchoCompany: CompanyIDstr },
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        $("#maxAvailableAmount").text(data.data);
                        $('input[type="range"]').attr({
                            min: 0,
                            max: data.data,
                            step: 1,
                            value: 0
                        });
                        $('input[type="range"]').rangeslider('update', true);
                    }
                });
                $('.rangeslider__fill').css("width", "10px !impotant")
                $('.rangeslider__handle').css("left", "0px !impotant")

                $('#lblfundingamountError').hide();
                $('#lblDiscountError').hide();
                $('#lbldateError').hide();
                $('#lblamountError').hide();
                $("#lblInvldNum").hide();
                $("#viewSelectedAmount").show();
                $("#ProceedSelAnchor").hide();
                $("#ProceedSelInvoice").show();

                $('#txtFunding').val('');
                $('#txtDiscount').val('');
                $('#txtActualAmount').val('');
                $('#txtDiscountAmount').val('');
                $('#txtdate').val('');
                $('#txtRequireFund').val('');
                //$('#tabName').val('');
                $('#txtFundingDate').val('');
            }
        }
        else {
            $("#lblanchorCompError").show();
        }
    });

    $("#yesSaveInvoice").off().click(function () {
        var InvoiceID = ""; var SelInvoiceCount = 0;
        $.ajax({
            type: "get",
            url: GetInvoicesByAmount,
            data: { sumAssuredAmount: $("#txtFunding").val(), fundingDate: $("#txtFundingDate").val(), discount: $("#txtDiscount").val(), finassistType: $("#tabName").val(), anchoCompany: CompanyIDstr },
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                
               
                $.each(data.data, function (i) {
                    InvoiceID += data.data[i].invoiceID + ",";
                })
                SelInvoiceCount = data.length;
                CompanyOfferDiscount(InvoiceID, SelInvoiceCount);
            },
            error: function () {
                alert("Error occured!!")
            }
        });
    });

    $('#tbl_DiscountByInvoice').off().on("change", "input[type='checkbox']", function () {
        
        CompanyIDstr = "";
        $('#tbl_DiscountByInvoice [type="checkbox"]').each(function (i, chk) {
            if (chk.checked) {
                var id = $(this).closest("tr").find('.clsCompanyID').val();
                CompanyIDstr += id + ",";
            }
        });
        $("#companyID").val(CompanyIDstr);
        $("#lblanchorCompError").hide();
    });
});

function GetDiscountAmount(data) {
    
    var InvApprovedDate = data.invoiceApprovalDate;
    var dateinNewFormat = new Date(InvApprovedDate);
    var daysToPay = data.invoiceApprovePayDays;
    dateinNewFormat.setDate(dateinNewFormat.getDate() + parseInt(daysToPay));
    var dd = dateinNewFormat.getDate();
    var mm = dateinNewFormat.getMonth() + 1;
    var y = dateinNewFormat.getFullYear();
    var ApproxDateOfPay = y + '-' + mm + '-' + dd;
    var Days = Common.DiffrenceBetweenDays(data.paymentDueDate, ApproxDateOfPay);
    var CalCulatedValueByFormula = ((Days / 365) * data.invoiceAmt).toFixed(2);
    return (Number(CalCulatedValueByFormula) * $("#txtDiscount").val()) / 100;
}

function SelectBox() {
    $('.cls-checkbox').click(function () {
        
        if ($(this).is(":checked")) {
            $('.cls-checkbox').each(function () {
                if ($(this).is(":checked")) {
                    $('#chkSelectALL').prop('checked', 'checked');
                } else {
                    $('#chkSelectALL').prop('checked', '');
                    return false;
                }
            });
        } else {
            $('.cls-checkbox').each(function () {
                if ($(this).is(":checked")) {
                    $('#chkSelectALL').prop('checked', 'checked');
                } else {
                    $('#chkSelectALL').prop('checked', '');
                    return false;
                }
            });
        }


    });
}

function CompanyOfferDiscount(InvoiceID, SelInvoiceCount) {
    var InvoiceIdstr = InvoiceID;
    debugger
    var TotalInvoiceCount = SelInvoiceCount;
    var BucketName = $("#txt_BucketName").val();
    var DiscountValue = $("#txtDiscount").val();
    var ValidToDateTime = $("#txtFundingDate").val();
    var AfterDiscountAmount = $("#txt_afterDiscount").text();
    var TotalInvAmount = $("#txt_TotalSelInvoicesAmount").text();
    var TotalDiscountAmount = $("#txt_OfferDiscountAmount").text();




    var url = InsertbucketDetails;

    $.post(url, {
        InvoiceIDStr: InvoiceIdstr, BucketName: BucketName, DiscountPercentage: DiscountValue, ValidToDate: ValidToDateTime,
        TotalInvoiceCount: TotalInvoiceCount, TotalInvoiceAmount: TotalInvAmount, AfterDiscountAmount: AfterDiscountAmount, DiscountAmount: TotalDiscountAmount, vendorPage:"finoassist"
    }, function (data) {

        if (data.successresult > 0) {
            $("#InvoicesConfirm").hide();
            Common.CustomSuccessNotify("Invoices added to bucket");
            location.reload();
        }
    });

}
