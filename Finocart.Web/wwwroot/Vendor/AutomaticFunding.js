$(document).ready(function () {
    //$('#DiscuntVlidDate').datepicker();
    $("#DiscuntRate").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            //$("#lblRateMsgErr").html("Digits Only").show();
            return false;
        }
        
    });

     var date_input = $('#DiscuntVlidDate');
        var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
        var dateToday = new Date();
        date_input.datepicker({
            format: 'mm/dd/yyyy',
            container: container,
            todayHighlight: true,
            autoclose: true,
            numberOfMonths: 2,
            showButtonPanel: true
            //minDate: dateToday,
            //'startDate': dateToday
        })

   
    $('#yesdiscuntRate').on('click', function () {
        $("#DicntRateValdDiv").css("display", "block");
       
    });

    $('#NodiscuntRate').on('click', function () {

        var url = "../Vendor/VendorDashboardMain";
        window.open(url, "_self")
    });


    $('#BtnCofirm').on('click', function () {
        
        var rates = $("#DiscuntRate").val();
        var dates = $("#DiscuntVlidDate").val();
        var ids = $("#autoid").val();
        //var date = $("#DiscuntVlidDate").val();
        //var datestr = (new Date(DiscuntVlidDate)).toUTCString();
        $("#DiscuntVlidDate").datepicker({ dateformat: 'dd/MM/yyyy' });

        if (rates == 0 || rates == '') {
            $("#lblRateMsgErr").html("Please Enter valid discount rate").show();
            return;
        }
        if (rates > 100 || rates < 0)
        {
            $("#lblRateMsgErr").html("Please Enter valid discount rate(0-100)").show();
            return;

        }
        
        else if (dates == "")
        {
            $("#lblDateErr").show();
            return;
        }
      else
            {
                $("#lblErrMsg").css("display", "none");
            }

        var objfub = { 'id': ids, 'rate': rates, 'date': dates };
        
        $.ajax({
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            url: "../Vendor/UpdateFunding",
            data: objfub,
            success: function (data) {
                alert("Data Save Successfully");
                var url = "../Vendor/VendorDashboardMain";
                window.open(url, "_self")
            },
            error: function (result) {
                alert("Error");
            }
        });
        Common.CustomSuccessNotify("Details Updated");
    });


   
    $('#BtnCancel').on('click', function () {
         
        
        var url = "../Vendor/AutomaticFunding";
        window.open(url, "_self")
       
    });

    $('#BtnCancelBefrChng').on('click', function () {
      
        $("#DicntRateValdDiv").css("display", "none");

    });

    $('#cal2').click(function () {
    $("#DiscuntVlidDate").datepicker().focus();
        
    });


    $('#btnchange').on('click', function () {
        
        $("#DiscuntRate").val("");
        $("#DiscuntVlidDate").val("");

    });

    

});