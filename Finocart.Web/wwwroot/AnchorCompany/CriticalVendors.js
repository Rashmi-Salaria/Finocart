$(document).ready(function () {
    $("#ddl_Vendors").on('focusout', function () {
        if ($("#ddl_Vendors").val() == 0 || $("#ddl_Vendors").val() == '') {
            $("#lblVnameErr").show();
        }
        else {
            $("#lblVnameErr").hide();
        }
    });
    $("#PercentageRate").on('focusout', function () {    
        if ($("#PercentageRate").val() == 0 || $("#PercentageRate").val() == '') {
            $("#lblDisCntErr").show();
        }
            else {
            $("#lblDisCntErr").hide();
            }      
    });
    $("#ddl_InvoiceLimit").on('focusout', function () {
        if ($("#ddl_InvoiceLimit").val() == 0 || $("#ddl_InvoiceLimit").val() == '') {
            $("#lblInvcLimitErr").show();
        }
        else {
            $("#lblInvcLimitErr").hide();
        }
    });
    $("#InvoiceLimitAmt").on('focusout', function () {
        if ($("#InvoiceLimitAmt").val() == 0 || $("#InvoiceLimitAmt").val() == '') {
            $("#lblInvcAmntlmitErr").show();
        }
        else {
            $("#lblInvcAmntlmitErr").hide();
        }
    });

    $('#tbl_CriticalVendors').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetCriticalVendorsList,
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 1
            { "data": "companyID", "name": "CompanyID", "autoWidth": true }, //index 2   
            { "data": "percentageRate", "name": "PercentageRate", "autoWidth": true, "class": "clsTotalInvoice" },
            { "data": "invAmtLimitStatus", "name": "InvAmtLimitStatus", "autoWidth": true, "class": "clsTotalDiscInvoiceAmount" },
            { "data": "invoiceLimitAmt", "name": "InvoiceLimitAmt", "autoWidth": true, "class": "clsTotalDiscInvoiceAmount" },
            {
                "data": function (data, type, row) {
                    return "<a href='#' data-toggle='modal' data-target='#CriticalVendors' name='EditCriticalVendors' class='actions-ico EditCriticalVendors'><img src='../Content/images/edit.png' title='Edit' class='img-responsive' /><a href='#' class='actions-ico' onclick='DeleteCriticalVendors(" + data.companyID + ")'><img src='../Content/images/delete.png' title='Delete' class='img-responsive' /></a>";
                }
            }

        ],
       
    });

    oTable2 = $('#tbl_CriticalVendors').DataTable();
    $('#btnVendorsFilter').click(function () {
        oTable2.columns(0).search($('#txt_VendorCompanyName').val().trim());
        oTable2.columns(4).search($('#txt_InvoiceLimitValue').val().trim());
        oTable2.draw();
    });


    $('.searchTerm').on('keyup', function () {
        oTable2.columns(0).search($('#txt_VendorCompanyName').val().trim());
        oTable2.columns(4).search($('#txt_InvoiceLimitValue').val().trim());
        oTable2.draw();
    });

    $('#ExportCriticalVendorsCSV').click(function () {
        var CompanyName = $('#txt_VendorCompanyName').val().trim();
        var InvoiceLimitValue = $('#txt_InvoiceLimitValue').val().trim();
        url = "../AnchorCompDashboard/ExportCriticalVendors?VendorName=" + CompanyName + "&TotalInvAmtLimit=" + InvoiceLimitValue;
        window.location.href = url;
    });

    $('#tbl_CriticalVendors').on('click', 'tr', function () {
        $("#lblInvcAmntlmitErr").hide();
        $("#lblInvcLimitErr").hide();
        $("#lblDisCntErr").hide();
        $("#lblVnameErr").hide();
        $("#ddl_Vendors").val(oTable2.row(this).data().companyID);
        $("#CompanyID").val(oTable2.row(this).data().companyID);
        $("#PercentageRate").val(oTable2.row(this).data().percentageRate);
        $("#ddl_InvoiceLimit").val(oTable2.row(this).data().status);
        $("#Status").val(oTable2.row(this).data().status);
        $("#InvoiceLimitAmt").val(oTable2.row(this).data().invoiceLimitAmt);
    });

   

    $('#BtnClose').click(function () {
        
        $("#myModalDialog").modal('close');
    });

    $('#btnAddNew').click(function () {
        $("#lblInvcAmntlmitErr").hide();
        $("#lblInvcLimitErr").hide();
        $("#lblDisCntErr").hide();
        $("#lblVnameErr").hide();

        $("#PercentageRate").val('');
        $("#InvoiceLimitAmt").val('');
        $("#ddl_InvoiceLimit").val('');
        $('select').val('0');
    });

    $('#BtnSave').click(function (e) {
        if ($("#ddl_Vendors").val() == 0 || $("#ddl_Vendors").val() == '') {
            $("#lblVnameErr").show();
            if ($("#PercentageRate").val() == 0 || $("#PercentageRate").val() == '') {
                $("#lblDisCntErr").show();
            }
            if ($("#ddl_InvoiceLimit").val() == 0 || $("#ddl_InvoiceLimit").val() == '') {
                $("#lblInvcLimitErr").show();
            }
            if ($("#InvoiceLimitAmt").val() == 0 || $("#InvoiceLimitAmt").val() == '') {
                $("#lblInvcAmntlmitErr").show();
            }
            e.preventDefault();
            return false;   
        }
      
       
    });
});

function DeleteCriticalVendors(CompanyId) {

    if (confirm("Are you sure you want to delete this User ?")) {

        var url = DeleteCriticalVendor;
        $.post(url, { ID: CompanyId }, function (data) {
            if (data == 1) {
                url = "../AnchorCompDashboard/CriticalVendors?result=" + data;
                window.location.href = url;
            }
            else {
                alert("We are sorry, something went wrong. Please try again later");
            }
        });
    }
    else {
        return false;
    }
}