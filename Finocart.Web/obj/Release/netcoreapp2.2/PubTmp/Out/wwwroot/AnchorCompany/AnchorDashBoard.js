$(document).ready(function () {
    $("#chkswitch").prop("checked", true);
    
   

    $('#chkswitch').on('click', function () {
        if ($('#chkswitch').prop('checked') == false) {
            var url = "../AnchorCompDashboard/CriticalVendors";
            window.open(url, "_self")
        }
    });

    /*Rule of Engine Valiadtion and Form Submit Start */
    $('input[type=radio][name=status]').change(function () {
        if (this.value == 'yes') {
            document.getElementById("rule_limit").readOnly = false;
            $("#lblrule_limitError_radio").hide();
        }
        else if (this.value == 'no') {
            document.getElementById("rule_limit").readOnly = true;
            $("#lblrule_limitError_radio").show();
            $("#lblrule_limitError").hide();
        }
    });

    $('#submit_rule_engine').on("click", function () {
        var errormsg = false;
        var per = $("#AnchorPercentage").val();
        if (per > 100) {
            alert("No numbers above 100");
            return false;

        }
        if ($('#chkUnlimited').is(":checked")) {
            // it is checked\
            chkUnlimited = true;
        }
        else {
            chkUnlimited = false;

            if ($("#Internal_fund").val() == "" || $("#Internal_fund").val() == "0") {
                $("#lblrule_limitError").show();
                errormsg = true;
            }
        }

        if ($("#AnchorPercentage").val() == "" || $("#AnchorPercentage").val() == "0") {
            $("#lblrule_percentageError").show();
            errormsg = true;
        }
        if (errormsg == true) {
            return false;
        }
        //if (document.getElementById('AnchorPercentage').value == '') {
        //    $("#lblrule_percentageError").show();
        //}
        //else if (document.getElementById('Internal_fund').value == '') {
        //    //$("#lblrule_percentageError").show();
        //    $("#lblrule_limitError").show();
        //}
        //else {
            document.getElementById('percentage_data').value = document.getElementById('AnchorPercentage').value;
            document.getElementById('limit_data').value = document.getElementById('rule_limit').value;
            var rule_percentage = $("#AnchorPercentage").val();
            var rule_limit = $("#rule_limit").val();

            if (rule_limit == 'Unlimited') {
                rule_limit = '';
                
            }
            sessionStorage.setItem("MyId", rule_percentage);
            sessionStorage.setItem("MyId1", rule_limit);

            var Internalfund = $('#Internal_fund').val();
            var FromBank = $('#FromBank').val();
            var AnchorRate = $('#AnchorPercentage').val();
            var chkUnlimited = null;
            if ($('#chkUnlimited').is(":checked")) {
                // it is checked\
                chkUnlimited = "1";
                $("#lblTotalAmount").text('Unlimited');
            }
            else {
                chkUnlimited = "0";
            }
            $.ajax({
                type: "get",
                url: "../AnchorCompDashboard/RuleofEngine",
                data: { rule_percentage: rule_percentage, rule_limit: rule_limit, Internalfund: Internalfund, FromBank: FromBank, chkUnlimited: chkUnlimited, AnchorRate: AnchorRate },
                contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    if (data.UserExist == 1) {
                        $("#lblDuplicateEmailError").show();

                    }
                    else {
                        $('#lblTotalLimit').text('');
                        alert("Data Updated Sucessfully");
                        $('#myModal').modal('hide');
                    }

                },

            });
        //}
        window.location.reload();
    });

    $('[data-dismiss=modal]').on('click', function (e) {
        $("input[name='model_popup']").val('');
        $("#lblrule_percentageError").hide();
        $("#lblrule_limitError").hide();
        $("#lblrule_limitError_radio").hide();
        $("#chkUnlimited").prop('checked', false);
        $("#Internal_fund").prop("disabled", false);
        $("#FromBank").prop("disabled", false);
        $("#lblAnchorPercentage").hide();
    })

    $('#model_engine').on("click", function () {
        $("#status_no").prop("checked", true);
        $("input[name='model_popup']").val('');

        $("#lblrule_percentageError").hide();
        $("#lblrule_limitError").hide();
        $("#lblrule_limitError_radio").hide();
        document.getElementById("rule_limit").readOnly = true;
        GetRuleofEngineData();
    });

    $("#AnchorPercentage").keyup(function (e) {
        $("#lblrule_percentageError").hide();
    })
    $("#rule_limit").keyup(function (e) {
        $("#lblrule_limitError").hide();
    })
    $('input:radio[name=status]').change(function () {
        $("#lblrule_limitError_radio").hide();
    });

    $("#btnBankEdit").click(function () {
        //debugger;
        //if ($("#FromBank").val() == "") {
        //    return false;
        //}
        //else {
        //    return true;
        //}
        debugger;
        $.ajax({
            type: "get",
            url: GetBankTotalFundLimits,
            data: { companyID: $("#hdnCompanyID").val() },
            contentType: 'application/json; charset=utf-8',
            async: false,
            
            success: function (TotalAvailableLimits) {
                debugger;
                if (TotalAvailableLimits.data == "0") {
                    alert("Sorry No Bank currently available!!!")
                   
                    
                    //$("#lblTotalAvailLimitError").show();
                    //$("#lblTotalAvailLimitError").html('Please enter bank limit less or equal to ' + parseInt(data.data));
                }
                else {
                    url = "../BankCompany/FundsRequestFromBank";
                    window.location.href = url;
                }

            },
            error: function () {
                alert("Sorry")
            }
        });
    });

    //$("#submit_set_limits").click(function () {
    //    debugger;
    //    var errormsg = false;
    //    var PercentageRate = $("#Anchor_Percentage").val();
    //    var PaymentDays = $("#anchorPaymentDays").val();
    //    var InternalFundLimits = $("#Internalfund").val().replace(/\,/g, '');
    //    var chkUnlimited = false;
    //    if ($('#IschkUnlimited').is(":checked")) {
    //        // it is checked\
    //        chkUnlimited = true;
    //    }
    //    else {
    //        chkUnlimited = false;

    //        if ($("#Internalfund").val() == "" || $("#Internalfund").val() == "0") {
    //            $("#lblAnchorInternalFund").html('Please enter internal fund');
    //            errormsg = true;
    //        }
    //    }

    //    if ($("#Anchor_Percentage").val() == "" || $("#Anchor_Percentage").val() == "0") {
    //        $("#lblAnchorPercentageError").html('Please enter percent');
    //        errormsg = true;
    //    }

    //    if ($("#anchorPaymentDays").val() == "" || $("#anchorPaymentDays").val() == "0") {
    //        $("#lblAnchorPaymentDays").html('Please enter payment days');
    //        errormsg = true;
    //    }
    //    if (errormsg == true) {
    //        return false;
    //    }
    //    $.ajax({
    //        url: "../AnchorCompany/UpdateRecord",
    //        type: "POST",
    //        dataType: "json",
    //        data: { PercentageRate: PercentageRate, PaymentDays: PaymentDays, InternalFundLimits: InternalFundLimits, chkUnlimited: chkUnlimited },
    //        success: function () {
    //            alert("Record updated successfully");
    //            url = "../AnchorCompany/AnchorDashboard";
    //            window.location.href = url;
    //        }

    //    });
        
    //});

    //GetSetLimitsData();

    //$("#closed").click(function () {
    //    var TeamDetailPostBackURL = '../AnchorCompDashboard/GetRuleOfEngineData';
    //    $.ajax({
    //        type: "GET",
    //        url: TeamDetailPostBackURL,
    //        contentType: "application/json; charset=utf-8",
    //        datatype: "json",
    //        success: function (data) {
    //            $('#Anchor_Percentage').val(data.percentageRate);
    //            $("#Internalfund").val(data.internalFundLimit);
    //            $("#chkUnlimited").prop('checked', data.isLimitUnlimited);
    //            $("#anchorPaymentDays").val(data.paymentDays);

    //            if (data.percentageRate == null || (data.internalFundLimit == null && data.isLimitUnlimited == false) || data.paymentDays == null) {
    //                alert("You need to fill the detail in order to proceed.");
    //                url = "../Account/UserLogin";
    //                window.location.href = url;
    //            }
    //            else {
    //                url = "../AnchorCompany/AnchorDashboard";
    //                window.location.href = url;
    //            }
    //        },
    //        error: function () {
    //            alert("Dynamic content load failed.");
    //        }
    //    });
    //});

    //$("#Internalfund").keyup(function () {
    //    $("#lblAnchorInternalFund").html('');
    //});

    //$("#Anchor_Percentage").keyup(function () {
    //    $("#lblAnchorPercentageError").html('');
    //});

    //$("#anchorPaymentDays").keyup(function () {
    //    $("#lblAnchorPaymentDays").html('');
    //});

    //$("#IschkUnlimited").change(function () {
    //    $("#lblAnchorInternalFund").html('');
    //    if ($('#IschkUnlimited').prop('checked') == true) {
    //        $("#Internalfund").prop("disabled", true);
    //    }
    //    else {
    //        $("#Internalfund").prop("disabled", false);
    //    }

    //});

    //$("#menuSetLimites").click(function () {
    //    GetSetLimitsData();
    //});
});

function isNumberKey(evt) {
    $("#lblTotalAvailLimitError").hide();
    //if ($("#FromBank").val() == "") 
        //$('#btnBankEdit').prop('disabled', true);
    //else 
        //$('#btnBankEdit').prop('disabled', false);
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))

        return false;

    return true;
}

check = function (e, value) {
    if (!e.target.validity.valid) {
        e.target.value = value.substring(0, value.length - 1);
        return false;
    }
    var idx = value.indexOf('.');
    if (idx >= 0) {
        if (value.length - idx > 3) {
            e.target.value = value.substring(0, value.length - 1);
            return false;
        }
    }
    return true;
}

$(window).load(function () {
    var value = sessionStorage.getItem("MyId");
    var value1 = sessionStorage.getItem("MyId1");
    document.getElementById('percentage_data').value = value;
    document.getElementById('limit_data').value = value1;
    GetRuleofEngineData();
    //if ($("#FromBank").val() == "")
    //    $('#btnBankEdit').prop('disabled', true);
});
$('#AnchorPercentage').focus(function () {
    $('#AnchorPercentage').ForceNumericOnly();
});
$(document).on("focusout", "#AnchorPercentage", function () {
    if ($('#AnchorPercentage').val() != '') {
        $.ajax({
            type: "get",
            url: CheckAnchorRate,
            data: { AnchorRate: $('#AnchorPercentage').val() },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                debugger;
                if (data == 1) {
                    $('#lblAnchorPercentage').show();
                    $('#lblAnchorPercentage').text('Anchor rate is too low.');                    
                }
                else if (data == 0) {
                    $('#lblAnchorPercentage').show();
                    $('#lblAnchorPercentage').text('Anchor rate is too high.');
                }
                else {
                    $('#lblAnchorPercentage').text('');
                    $('#lblAnchorPercentage').hide();
                }
            },
            error: function () {
                alert("Error occured!!")
            }
        });
    }

});
$('#AnchorPercentage').change(function (event) {
    //$("#AnchorPercentage").on('change', function () {
    $.ajax({
        type: "get",
        url: CheckAnchorRate,
        data: { AnchorRate: $('#AnchorPercentage').val() },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            debugger;
            if (data == 1) {
                $('#lblAnchorPercentage').show();
                $('#lblAnchorPercentage').text('Anchor rate is too low.');
            }
            else if (data == 0) {
                $('#lblAnchorPercentage').show();
                $('#lblAnchorPercentage').text('Anchor rate is too high.');
            }
            else {
                $('#lblAnchorPercentage').text('');
                $('#lblAnchorPercentage').hide();
            }
        },
        error: function () {
            alert("Error occured!!")
        }
    });
});

$(document).on("click", "#chkUnlimited", function () {
    
    if ($('#chkUnlimited').prop('checked') == true) {
        $("#Internal_fund").val('');
        //$("#FromBank").val('');
        $("#Internal_fund").prop("disabled", true);
        //$("#FromBank").prop("disabled", true);
        //$('#btnBankEdit').prop('disabled', true);
        $("#rule_limit").val('Unlimited');

    } else {
        $("#Internal_fund").prop("disabled", false);
        //$("#FromBank").prop("disabled", false);
        //$('#btnBankEdit').prop('disabled', false);
    }

    //if ($("#FromBank").val() == "")
    //    $('#btnBankEdit').prop('disabled', true);
});

$(document).on("keyup", ".sumtotal", function () {
    update_amounts();
});
function update_amounts() {
    //if ($('#FromBank').val() != '') {
    //    $('#btnBankEdit').prop('disabled', false);
    //}
    //else {
    //    $('#btnBankEdit').prop('disabled', true);
    //}
    var value1 = parseFloat($('#Internal_fund').val()) || 0;
    var value2 = parseFloat($('#FromBank').val()) || 0;
    $('#rule_limit').val(value1 + value2);
}
/*Rule of Engine Valiadtion and Form Submit Start */
function GetRuleofEngineData() {
    var TeamDetailPostBackURL = '../AnchorCompDashboard/GetRuleOfEngineData';
    $.ajax({
        type: "GET",
        url: TeamDetailPostBackURL,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            $('#AnchorPercentage').val(data.percentageRate);            
            if (data.isLimitUnlimited == true) {
                $("#limit_data").val('Unlimited');
                $("#Internal_fund").prop("disabled", true);
                $("#rule_limit").val('Unlimited');
            }
            else {
                if (data.invoiceLimitAmt != null) {
                    document.getElementById("rule_limit").readOnly = false;
                    if (data.totalDraw_funds != null) {
                        $("#rule_limit").val((parseInt(data.internalFundLimit) + parseInt(data.totalDraw_funds)).toLocaleString('en'));
                    }
                    else {
                        $("#rule_limit").val((parseInt(data.internalFundLimit)).toLocaleString('en'));
                    }
                }

                if (data.totalDraw_funds != null) {
                    $("#limit_data").val((parseInt(data.internalFundLimit) + parseInt(data.totalDraw_funds)).toLocaleString('en'));

                }
                else {
                    $("#limit_data").val((parseInt(data.internalFundLimit)).toLocaleString('en'));
                }
            }
         
            $("#Internal_fund").val(data.internalFundLimit);
            //$("#FromBank").val(data.bankLimit);
            //$("#AnchorPercentage").val(data.anchorRate);
            $("#chkUnlimited").prop('checked', data.isLimitUnlimited);
            $("#percentage_data").val(data.percentageRate);
            $("#FromBank").val(data.totalDraw_funds);
           
            var Available_bal = $.fn.dataTable.render.number(',', '').display(data.avalableLimits);
            var Remaining_limits = $.fn.dataTable.render.number(',', '').display(data.remainingLimits);
            if (Available_bal != null) {
                $("#lblTotalAmount").text(Available_bal);
            }
            else {
                $("#lblTotalAmount").text("");
            }
           
            if (data.remainingLimits != null) {
                $("#lblTotalRemainAmount").text(Remaining_limits);
            }
            else {
                $("#lblTotalRemainAmount").text("");
            }
            //if ($('#chkUnlimited').is(":checked")) {
            //    // it is checked\
            //    chkUnlimited = "1";
            //    $("#lblTotalAmount").text('Unlimited');
            //}
            //else {
            //    chkUnlimited = "0";
            //    $("#lblTotalAmount").text(data.invoiceLimitAmt);
            //}
            
           
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
        
    });
   
}

function GetSetLimitsData() {
    var TeamDetailPostBackURL = '../AnchorCompDashboard/GetRuleOfEngineData';

    $.ajax({
        type: "GET",
        url: TeamDetailPostBackURL,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            $('#Anchor_Percentage').val(data.percentageRate);
            if (data.internalFundLimit != null) {
                $("#Internalfund").val(data.internalFundLimit.toLocaleString('en'));
            }
            $("#IschkUnlimited").prop('checked', data.isLimitUnlimited);
            $("#anchorPaymentDays").val(data.paymentDays);

            if ($('#Anchor_Percentage').val() == "" || ($("#Internalfund").val() == "" && $('#IschkUnlimited').is(":checked") == false) || $("#anchorPaymentDays").val() == "") {
                $("#myModalDialog").modal("show");
            }
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
}
