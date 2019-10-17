var Count = 0;
$(document).ready(function () {
    
    $("#Name").keypress(function () {
        $('#lblNameError').hide();
    });

    $("#Email").keypress(function () {
        $('#lblEmailError').hide();
        $("#lblEmailInvalidError").hide();
        $('#lblDuplicateEmailError').hide();
    });

    $("#Mobile").keypress(function () {
        $('#lblMobileError').hide();
        $("#lblMobileInvalidError").hide();
    });

    $('#Mobile').on('keypress', function (e) {

        var $this = $(this);
        var regex = new RegExp("^[0-9\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        // for 10 digit number only
        if ($this.val().length > 9) {
            e.preventDefault();
            $("#lblMobilelengtherror").show();
            return false;
        }
        if (e.charCode < 54 && e.charCode > 47) {
            if ($this.val().length == 0) {
                e.preventDefault();
                return false;
            } else {
                return true;
            }

        }
        if (regex.test(str)) {
            return true;
        }
        e.preventDefault();
        return false;
    });

  
    $(function () {
        $("#btnSave").click(function (e) {
            
            var errormsg = false;
            var Name = $("#Name").val();
            if (Name == "") {
                $("#lblNameError").show();
                errormsg = true;
            }

            var Email = $("#Email").val();

            if (Email == "") {
                $("#lblEmailError").show();
                errormsg = true;
            }
            
            var Mobile = $("#Mobile").val();
            if (Mobile == "") {
                $("#lblMobileError").show();
                errormsg = true;
            }

            var Designation = $("#Designation").val();
            if (Designation == "") {
                $("#lblDesignationError").show();
                errormsg = true;
            }

            var SelRoleType = $("#ddl_RoleType").val();
            if (SelRoleType == 0) {
                $("#lblSelRoleTypeError").show();
                errormsg = true;
            }
            var SelRoleAccess = $("#ddl_Access").val();
            if (SelRoleAccess == 0) {
                $("#lblSelRoleAccessError").show();
                errormsg = true;
            }
            var SelStatus = $("#ddl_Status").val();
            if (SelStatus == 0) {
                $("#lblSelStatusErr").show();
                errormsg = true;
            }
            if (Count == 1) {
                errormsg = true;
            }
            if (errormsg == true) {
                return false;
            }
        });
    });

    // Save End    


    $('#btnCancel').click(function (e) {
        

    });
   
    $("#Email").on('focusout', function () {
        Count = 0;
        
        if ($("#Email").val() != '') {
            var result = isEmail($("#Email").val());
            if (result == false) {
                $("#lblEmailInvalidError").show();
                Count++;
            }
            else {
                CheckIfEmailIdExists($("#Email").val());
            }
        }
    });

    $("#Mobile").on('focusout', function () {
        Count = 0;
        var result = IsNumeric($("#Mobile").val());
        if (result == false) {
            $("#lblMobileInvalidError").show();
            Count++;
        }
    });

  

});

function isEmail(emailaddress) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(emailaddress))
        return false;
    
}

function IsNumeric(mobileno) {
    var filter = /^[0-9-+]+$/;
    if (mobileno != "") {
        if (filter.test(mobileno)) {

        }
        else {
            return false;
        }
    }

}

function CheckIfEmailIdExists(EmailID) {
    $.ajax({
        type: "get",
        url: "../Common/CheckIfEmailIdExists",
        data: { EmailID: $("#Email").val(), Module: 'User' },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
           
            if (data == 0) {
                $('#lblDuplicateEmailError').hide();
            }
            else {
                $('#lblDuplicateEmailError').show();
                $("#Email").val('');
                Count++;
            }
        },
        //error: function () {
        //    alert("Error occured!!")
        //}
    });
}