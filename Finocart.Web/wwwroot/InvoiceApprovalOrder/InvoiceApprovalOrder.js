$(document).ready(function () {

    //Get Invoice Approval List(start)
    $('#tbl_InvoiceApprovedList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "scrollX": true,
        "ajax": {
            "url": GetInvoiceApprovedList,
            "type": "POST",
            "datatype": "json",

        },
        "columns": [
            { "data": "approvalID", "name": "ApprovalID", "autoWidth": true, "visible": false }, //index 1
            { "data": "fromAmount", "name": "FromAmount", "autoWidth": true }, //index 2
            { "data": "toAmount", "name": "ToAmount", "autoWidth": true }, //index 3
            { "data": "approvedByName", "name": "ApprovedByName", "autoWidth": true }, //index 4
            { "data": "usersName", "name": "usersName", "autoWidth": true }, //index 5
            {
                "data": function (data, type, row) {
                    return "<a href='../Admin/EditInvoiceApprovalPage/" + data.approvalID + "'' class='actions-ico'><img src='../Content/images/edit.png' class='img-responsive' title='Edit' /><a href='../Admin/DeleteInvoiceApprovalPage/" + data.approvalID + "'' class='actions-ico'><img src='../Content/images/delete.png' title='Delete' class='img-responsive' /></a>";
                }
            }

        ],
    });
    //(end)

    $('#btnAddNew').click(function () {
        url = "../Admin/AddInvoiceApproval";
        window.location.href = url;
    });


    var ApprovedByName = $("#ApprovedByName").val().toLowerCase();

    // set value of approved by dropdown on edit (start)
    $("#ddl_ApprovedBy option").filter(function () {
        return $(this).val() == $("input[name=ApprovedBy]").val();
    }).prop('selected', true);
    //(end)

    // set users dropdown by selected approvedby value on edit (start)
    if (ApprovedByName == "group-anyone" || ApprovedByName == "group-all") {
        var data = $("#Users").val();
        var dataarray = data.split(",");
        $("#ddl_User").hide();
        $("#ddl_MultiUser").val(dataarray);
    }
    //(end)

    // get multiselect dropdown (start)
    $('#ddl_MultiUser').multiselect({
        buttonContainer: '<span class="multiUserDropDown" />',
        buttonWidth: '479px',
        templates: {
            button: '<button type="button" class="multiselect dropdown-toggle selectstatus form-control" data-toggle="dropdown"><span class="multiselect-selected-text"></span> <b class="caret"></b></button>',
            ul: '<ul class="multiselect-container dropdown-menu"></ul>',
            filter: '<li class="multiselect-item filter"><div class="input-group"><span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span><input class="form-control multiselect-search" type="text"></div></li>',
            filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" type="button"><i class="glyphicon glyphicon-remove-circle"></i></button></span>',
            li: '<li><a tabindex="0"><label></label></a></li>',
            divider: '<li class="multiselect-item divider"></li>',
            liGroup: '<li class="multiselect-item multiselect-group"><label></label></li>'
        },
        buttonText: function (options) {
            if (options.length == 0) {
                if (ApprovedByName != "single") {
                    $("#Users").val('')
                }
                return 'Select User';
            }
            if (options.length > 6) {
                return options.length + ' selected';
            }
            else {
                var selected = '';
                var Users = '';
                options.each(function () {
                    selected += $(this).text() + ', ';
                    Users += $(this).val() + ',';
                    $("#Users").val(Users.substr(0, Users.length - 1))
                });
                return selected.substr(0, selected.length - 2);
            }
        }

    });
    //(end)

    // set users dropdown by selected approvedby value on edit (start)
    if (ApprovedByName == "anyone") {
        $(".multiUserDropDown").hide();
        $("#ddl_User").hide();
    }
    else if (ApprovedByName == "single") {
        $(".multiUserDropDown").hide();
        $("#ddl_User").show();
        $("#ddl_User option").filter(function () {
            return $(this).val() == $("input[name=Users]").val();
        }).prop('selected', true);
    }
    else if ($("#ApprovedBy").val() == "0") {
        $(".multiUserDropDown").hide();
    }
    //(end)

    // set users dropdown on change approvedby value in dropdown (start)
    $("#ddl_ApprovedBy").change(function () {
        var ApprovedBy = $("#ddl_ApprovedBy option:selected").text().toLowerCase();
        $("#ApprovedBy").val($(this).val())
        $("#Users").val('');
        $("input[name=Users]").val('')
        if (ApprovedBy == "anyone") {
            $("#ddl_User").hide();
            $(".multiUserDropDown").hide();
        }
        else if (ApprovedBy == "single") {
            $("#ddl_User").show();
            $(".multiUserDropDown").hide();
            $("#ddl_User option[value='0']").attr("selected", true);
        }
        else if (ApprovedBy == "group-anyone" || ApprovedBy == "group-all") {
            $("#ddl_User").hide();
            $('#ddl_MultiUser').attr('multiple', 'multiple');
            $(".multiUserDropDown").show();
        }
        else {

        }
    });
    //(end)

    $("#ddl_User").change(function () {
        $("#Users").val($(this).val());
    });

    // validations on save and update data (start)
        $("#btnSave").click(function (e) {
            var errormsg = false;

            var FromAmount = parseFloat($("#FromAmount").val());
            var ToAmount = parseFloat($("#ToAmount").val());
            var ApprovedBy = $("#ddl_ApprovedBy").val();
            var Users = $("#Users").val();

            if (FromAmount < 1) {
                $("#lblFromAmountError").show();
                $("#lblFromAmountError").html('From Amount cannot be less than one');
                errormsg = true;
            }
            if (ToAmount < 1) {
                $("#lblToAmountError").show();
                $("#lblToAmountError").html('To Amount cannot be less than one');
                errormsg = true;
            }
            if (ToAmount <= FromAmount && ToAmount > 1) {
                $("#lblToAmountError").show();
                $("#lblToAmountError").html('To Amount cannot be equal or less than from amount');
                errormsg = true;
            }
            if (ApprovedBy == "0") {
                $("#lblApprovedByError").show();
                $("#lblApprovedByError").html('Please select approved by');
                errormsg = true;
            }
            if (ApprovedBy != "12" && ApprovedBy != "0") {
                if (Users == "" || Users == "0") {
                    $("#lblUserError").show();
                    $("#lblUserError").html('Please select user');
                    errormsg = true;
                }
            }

            if ($("#lblFromAmountError").is(":visible") == true) {
                errormsg = true;
            }

            if (errormsg == true) {
                return false;
            }
        });
    //(end)
    
});

//check from amount and to amount validation on focusout
$(document).on('focusout', function () {

    var FromAmount = parseFloat($("#FromAmount").val());
    var ToAmount = parseFloat($("#ToAmount").val());
    $.ajax({
        type: "POST",
        datatype: "json",
        async: false,
        url: "/Admin/CheckDuplicateAmount?FromAmount=" + FromAmount + "&ToAmount=" + ToAmount+"",
        success: function (data) {

            if (data != 0) {
                if ($('#ApprovalID').val() != data) {
                    $('#lblFromAmountError').show();
                    $('#lblFromAmountError').text('From amount and to amount already exist');
                    return false;
                }

            }
            else {
                $('#lblFromAmountError').hide();
            }
        },
        error: function (result) {
        }
    });

});
//(end)