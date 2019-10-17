$(document).ready(function () {

    
    var dash = $("#datefieldvalidator").val();
    var CurrentDate = new Date();
    if (dash <= CurrentDate) {
        $("#messagelbl").show();
        errormsg = true;
    }
    else
    {
        $("#messagelbl").hide();        
    }

    if ($('#chkswitch').prop('checked') == true) {
        $('#Redirect_finoAssist').show();
        $('#Redirect_SelectInvoice').show();
    }
    $('#chkswitch').on('click', function () {
    
        if ($('#chkswitch').prop('checked') == true) {
            
            $('#Redirect_finoAssist').show();
            $('#Redirect_SelectInvoice').show();
        }
        else {
            $('#Redirect_finoAssist').hide();
            $('#Redirect_SelectInvoice').hide();
            var url = "../Vendor/AutomaticFunding";
            window.open(url, "_self")
        }
    });

    //alert("");
    $('#DashboardMain').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetDashboardlist,
            "type": "POST",
            "datatype": "json",
            "async": false           
        },
        "columns": [

            //{
            //    "data": function (data, type, row) {
            //        //index 0
            //        return "<input type='checkbox' class='select-checkbox clsCompanyCheck_" + data.companyID + "'/>";
            //    }
            //},
            { "data": "companyId", "name": "CompanyId", "autoWidth": true, "class": "clsCompanyId" }, //index 1 
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2   
                       
            {
                "data": "totalInvoice", "name": "TotalInvoice", "autoWidth": true, "class": "clsTotalInvoice"

            },
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, "class": "clsTotalInvoiceAmount", render: $.fn.dataTable.render.number(',', '.', 0) }

            

        ],
        "columnDefs": [{
            //"targets": 0,
            "searchable": false,
            "orderable": false,
            "className": "dt-body-center",

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
            $("#lbTotalInvoices").text(api.column('.clsTotalInvoice').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
            $("#lbTotalInvoicesAmt").text(api.column('.clsTotalInvoiceAmount').data().reduce(function (a, b) { return Number(a) + Number(b); }, 0));
        }
    });
    $('#Redirect_SelectInvoice').on('click', function () {
        
        var url = "../Vendor/VendorDashboard";
        window.open(url, "_self")
    });

    $('#viewSelectedAmount').click(function () {
       
        var val = $('#lblfundingamountError').val();
        if (val != '') {
            $('#Invoices').modal('show');
         
        }   
    });
    $('#ClosePopSeletedInvc').click(function () {

        $('#Invoices').modal('hide');
    });


    $('#Redirect_finoAssist').on('click', function () {   
     
        $('.rangeslider__fill').css("width", "10px !impotant")
        $('.rangeslider__handle').css("left", "0px !impotant")
        
        $('#lblfundingamountError').hide();  
        $('#lblDiscountError').hide();  
        $('#lbldateError').hide();  
        $('#lblamountError').hide();  
        $("#lblInvldNum").hide();  
       

        $('#txtFunding').val('');
        $('#txtDiscount').val('');
        $('#txtActualAmount').val('');
        $('#txtDiscountAmount').val('');
        $("#txtdate").val('');
        $('#txtRequireFund').val(''); 
        $('#tabName').val('');
        $('#txtFundingDate').val('');
    })

    $('#byAnchorCmpny').on('click', function () {
        $('.rangeslider__fill').css("width", "10px !impotant")
        $('.rangeslider__handle').css("left", "0px !impotant")
        $('#lblfundingamountError').hide();
        $('#lblDiscountError').hide();
        $('#lbldateError').hide();
        $('#lblamountError').hide();
        $("#lblInvldNum").hide();
        $("#viewSelectedAmount").hide();
        $("#ProceedSelAnchor").show();
        $("#ProceedSelInvoice").hide();

        $('#txtFunding').val('');
        $('#txtDiscount').val('');
        $('#txtActualAmount').val('');
        $('#txtDiscountAmount').val('');
        $('#txtdate').val('');
        $('#txtRequireFund').val('');
        $('#tabName').val('ByAnchorComp');
        $('#txtFundingDate').val('');
    })


    $('#ByAmnt').on('click', function () {

        $.ajax({
            type: "get",
            datatype: "json",
            async: false,
            url: GetMaximumAvailAmtByAmount,
            data: '',
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
        $('#tabName').val('');
        $('#txtFundingDate').val('');
    })

    $('#viewSelectedAmount').click(function () {

        var val = $('#txtFundingDate').val();
        if (val != '') {
            $('#lbldateError').hide();
        }
        else {
            $('#lbldateError').show();
        }

        if ($('#txtRequireFund').val() != '') {
            $('#lblamountError').hide(); 
        }
        else {
            $('#lblamountError').show();
        }

        if ($('#txtFunding').val() != '') {
            $('#lblfundingamountError').hide();
        }
        else {
            $('#lblfundingamountError').show();
        }

        if ($('#txtDiscount').val() != '') {
            $('#lblDiscountError').hide();
        }
        else {
            $('#lblDiscountError').show();
        }
    });
});