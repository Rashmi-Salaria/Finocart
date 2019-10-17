$(document).ready(function () {
    $('#tbl_FundsRequstHistoryList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {

            "url": GetFundWithdrawHistory,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
           
            { "data": "anchor_id", "name": "Anchor_id", "autoWidth": true }, //index 1
           // { "data": "anchor_Name", "name": "Anchor_Name", "autoWidth": true }, //index 2   
            { "data": "requestAmount", "name": "RequestAmount", "autoWidth": true },
            { "data": "validity_upto", "name": "Validity_upto", "autoWidth": true },
            
            {
                "data": function (data, type, row) {
                    return "<a href='../BankCompany/FundRequestView/?Anchor_id=" + data.anchor_id + "'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
                }
            }

        ],
    });

    oTable1 = $('#tbl_FundsRequstHistoryList').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(2).search($('#Txt_FundsHitoryAmount').val().trim());
        oTable1.columns(3).search($('#txt_FundHistoryRequestDate').val().trim());
        oTable1.draw();
    });


    $(function () {
        var dtToday = new Date();

        var month = dtToday.getMonth() + 1;
        var day = dtToday.getDate();
        var year = dtToday.getFullYear();
        if (month < 10)
            month = '0' + month.toString();
        if (day < 10)
            day = '0' + day.toString();

        var maxDate = year + '-' + month + '-' + day;
        
        $('#IntrestVldUpto').attr('min', maxDate);
    });

  


    $("#TxtAvlblelLimit").on('focusout', function () {
        if ($("#TxtAvlblelLimit").val() == 0 || $("#TxtAvlblelLimit").val() == '') {
            $("#TxtAvlblelLimitError").show();
        }
        else {
            $("#TxtAvlblelLimitError").hide();
        }
    });

    $("#TxtMdifyLimit").on('focusout', function () {
        if ($("#TxtMdifyLimit").val() == 0 || $("#TxtMdifyLimit").val() == '') {
            $("#TxtMdifyLimitError").show();
        }
        else {
            $("#TxtMdifyLimitError").hide();
        }
    });

    $("#TxtSetIntrestFor").on('focusout', function () {
        if ($("#TxtSetIntrestFor").val() == 0 || $("#TxtSetIntrestFor").val() == '') {
            $("#TxtSetIntrestError").show();
        }
        else {
            $("#TxtSetIntrestError").hide();
        }
    });

    $("#TxtSetIntrestFor").on('focusout', function () {
        if ($("#TxtSetIntrestFor").val() == 0 || $("#TxtSetIntrestFor").val() == '' ) {
            $("#TxtSetIntrestError").show();
        }
        else {
            $("#TxtSetIntrestError").hide();
        }
    });
    $("#TxtSetIntrestMonth").on('focusout', function () {
        if ($("#TxtSetIntrestMonth").val() == 0 || $("#TxtSetIntrestMonth").val() == '') {
            $("#TxtSetIntrestError").show();
        }
        else {
            $("#TxtSetIntrestError").hide();
        }
    });

    $("#IntrestVldUpto").on('focusout', function () {
        if ($("#IntrestVldUpto").val() == 0 || $("#IntrestVldUpto").val() == '') {
            $("#IntrestVldUptoErro").show();
        }
        else {
            $("#IntrestVldUptoErro").hide();
        }
    });

    $("#DrpNoOfDaysReq").on('focusout', function () {
        if ($("#DrpNoOfDaysReq").val() == "0" || $("#DrpNoOfDaysReq").val() == '') {
            $("#DrpNoOfDaysReqErr").show();
        }
        else {
            $("#DrpNoOfDaysReqErr").hide();
        }
    });

    $('#btnSave').click(function (e) {
        if ($("#TxtAvlblelLimit").val() == 0 || $("#TxtAvlblelLimit").val() == '') {
            $("#TxtAvlblelLimitError").show();
            e.preventDefault();
        }
        else {
            $("#TxtAvlblelLimitError").hide();
        }

        if ($("#TxtMdifyLimit").val() == 0 || $("#TxtMdifyLimit").val() == '') {
            $("#TxtMdifyLimitError").show();
            e.preventDefault();
        }
        else {
            $("#TxtMdifyLimitError").hide();
        }

        //if ($("#TxtSetIntrestFor").val() == 0 || $("#TxtSetIntrestFor").val() == '') {
        //    $("#TxtSetIntrestError").show();
        //    e.preventDefault();
        //}
        //else {
        //    $("#TxtMdifyLimitError").hide();
        //}
        if ($("#TxtSetIntrestFor").val() == 0 || $("#TxtSetIntrestFor").val() == '') {
            $("#TxtSetIntrestError").show();
            e.preventDefault();
        }
        else {
            $("#TxtSetIntrestError").hide();
        }

        if ($("#TxtSetIntrestMonth").val() == 0 || $("#TxtSetIntrestMonth").val() == '') {
            $("#TxtSetIntrestError").show();
            e.preventDefault();
        }
        else {
            $("#TxtSetIntrestError").hide();
        }

        if ($("#IntrestVldUpto").val() == 0 || $("#IntrestVldUpto").val() == '') {
            $("#IntrestVldUptoErro").show();
            e.preventDefault();
        }
        else {
            $("#IntrestVldUptoErro").hide();
        }

        if ($("#DrpNoOfDaysReq").val() == "0" || $("#DrpNoOfDaysReq").val() == '') {
            $("#DrpNoOfDaysReqErr").show();
            e.preventDefault();
        }
        else {
            $("#DrpNoOfDaysReqErr").hide();
        }

        
    });
    });