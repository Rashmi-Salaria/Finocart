$(document).ready(function () {
        document.getElementById("Btn_Submit").focus();
   
    $('#panNumber').keypress(function (event) {
        if (event.keyCode == 13) {
            $('#Btn_Submit').click();
        }
    });
    $('#paswd').keypress(function (event) {
        if (event.keyCode == 13) {
            $('#Btn_Submit').click();
        }
    });

    $("#Email").keypress(function () {
        $('#lblEmailError').hide();
        $("#lblEmailInvalidError").hide();
        $('#lblDuplicateEmailError').hide();
    });
    $("#Email").on('focusout', function () {
        debugger
        Count = 0;
        var emailaddress = $("#Email").val();
        if (emailaddress!= null) {
            var validat = isEmail(emailaddress);
            if (validat == false) {
                $("#lblEmailInvalidError").show();
                Count++;
            }
            else {
                //CheckIfEmailIdExists($("#Email").val());
            }
        }
    });
    function isEmail(emailaddress) {      
        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test(emailaddress))
            return false;

    }


    $('#Email').keypress(function () {
        $('#lblEmailInvalidError').hide();
        $('#lblEmailError').hide();
    });
    $('#Password').keyup(function () {
        $('#ErrPassword').hide();
    });

    $('#Btn_Submit').click(function (event) {             
  
        SaveGenerateQuote(event);        
    });

});

function SaveGenerateQuote(event) {
  
    var errMsg = Validation(event);
    if (errMsg == false) {
        return errMsg;
    }
    else {

        $('#FormSubmitLogin').submit();
    }
}

function Validation() {
    
    var errMsg = true;
    if ($('#Email').val() == "" || $('#EmailID').val() == "") {
        $('#ErrEmailId').show();
        errMsg = false;

    }
    if ($('#Password').val() == "" || $('#Password').val() == "") {
        $('#ErrPassword').show();
        errMsg = false;
    }
    if ($('#PANNumber').val() == "") {
        $('#ErrEmailId').show();
        errMsg = false;

    }

    var VSheight = document.documentElement.clientHeight;
    var VSwidth = document.documentElement.clientWidth;
    //Commented below lines as it was producing error
    //document.getElementById('login-left').style.height = VSheight -225 + 'px';
    //document.getElementById('login-left').style.width = VSwidth -225 + 'px';
    return errMsg;
}
function ClearAllControls(formid) {
    //Common.ClearAllControls(formid)
    var url = "./SuperAdminLogin";
    window.location = url; 
}
$(document).on('keypress', function (e) {
    if (e.which == 13) {
        document.getElementById("Btn_Submit").focus();
    }
});
//function CheckIfEmailIdExists(EmailID) {
//    debugger
//    $.ajax({
//        type: "get",
//        url: "../Common/CheckIfEmailIdExists",
//        data: { EmailID: $("#Email").val(), Module: 'User' },
//        contentType: 'application/json; charset=utf-8',
//        success: function (data) {

//            if (data == 0) {
//                $('#lblDuplicateEmailError').hide();
//            }
//            else {
//                $('#lblDuplicateEmailError').show();
//                $("#Email").val('');
//                Count++;
//            }
//        },
//        //error: function () {
//        //    alert("Error occured!!")
//        //}
//    });
//}






