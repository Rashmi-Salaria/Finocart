$(document).ready(function () {
   
    $("#selUpDoc").change(function () {
        $("#lblDocTypeError").hide();
    })
    $('#Anchoruploaddocuments').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',

        "ajax": {
            "url": "../GetAnchorDocList",
            "type": "POST",
            "datatype": "json",

        },
        "columns": [
            { "data": "docID", "name": "DocID", "autoWidth": true, "visible": false }, //index 1
            { "data": "documentType", "name": "DocumentType", "autoWidth": true }, //index 2
            { "data": "fileName", "name": "FileName", "autoWidth": true }, //index 3
            
            { "data": "Status", "defaultContent": "<i>Uploaded</i>"},
            {
                "data": function (data, type, row) {
                    return "<a href='../AnchorCompany/DeleteAnchorCompDoc/" + data.docID + "' class='actions-ico'><img src='/Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                }
            }

        ],
    });

    $('#tbl_AnchorCompList').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": "../AnchorCompany/GetAnchorCompListByVendorID",
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "companyID", "name": "CompanyID", "autoWidth": true, "visible": false }, //index 1
            { "data": "company_name", "name": "Company_name", "autoWidth": true }, //index 2
            //{ "data": "contact_Name", "name": "Contact_Name", "autoWidth": true }, //index 3
            //{ "data": "contact_mobile", "name": "Contact_mobile", "autoWidth": true },
            //{ "data": "contact_email", "name": "Contact_email", "autoWidth": true },
            //{ "data": "location", "name": "Location", "autoWidth": true },
            { "data": "totalInvoice", "name": "TotalInvoice", "autoWidth": true },
            { "data": "totalInvoiceAmount", "name": "TotalInvoiceAmount", "autoWidth": true, render: $.fn.dataTable.render.number(',', '.', 0) },
            { "data": "approvedInv", "name": "ApprovedInv", "autoWidth": true },           
            { "data": "pendingInv", "name": "PendingInv", "autoWidth": true },
            { "data": "rejectedInv", "name": "RejectedInv", "autoWidth": true },
            //{
            //    "data": function (data, type, row) {
            //        return "<a href='../AnchorCompany/GetVendorInvoiceDetailsViewByVendorID/?icompanyID=" + data.companyID + "' class='actions-ico'><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>";
            //    }
            //}
      
        ],
        "order": [
            [1, "asc"],
        ],
        
    });

    oTable1 = $('#tbl_AnchorCompList').DataTable();
    $('#btnInvoiceFilter').click(function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.draw();
    });

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(1).search($('#txt_CompanyName').val().trim());
        oTable1.draw();
    });

    $('#ExportInvoiceCSV').click(function () {
        debugger;
        //var CompanyID = $('#txt_CompanyID').val().trim();
        var CompanyName = $('#txt_CompanyName').val().trim();
        url = "../AnchorCompany/ExportAnchorCompListByVendorID?CompanyName=" + CompanyName;
        window.location.href = url;
    });
});


/////////////////////////////////////Ancghor register Method/////////////////////////////////




$(document).ready(function () {
    Numeric();
});

function Numeric() {
    jQuery.fn.ForceNumericOnly =
        function () {
            return this.each(function () {
                $(this).keydown(function (e) {
                    var key = e.charCode || e.keyCode || 0;
                    // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
                    // home, end, period, and numpad decimal
                    return (
                        key == 8 ||
                        key == 9 ||
                        key == 13 ||
                        key == 46 ||
                        key == 110 ||
                        key == 190 ||
                        (key >= 35 && key <= 40) ||
                        (key >= 48 && key <= 57) ||
                        (key >= 96 && key <= 105));
                });
            });
        };
}
$('#Contact_mobile').focus(function () {
    $('#Contact_mobile').ForceNumericOnly();
});

$("#Pan_number").on('change', function () {
    debugger
    $.ajax({
        type: "get",
        url: CheckPan,
        data: { pan: $('#Pan_number').val() },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data == 0) {
                $('#lblPan').text('');
            }
            else {
                $('#lblPan').show();
                $('#Pan_number').val('');
                $('#lblPan').text(data+" "+'PanNumber already exist');
            }
        },
        error: function () {
            alert("Error occured!!")
        }
    }); 
});

// for validity check of pancard
$('#Pan_number').change(function (event) {

    var count = 0;
    var regExp = /[a-zA-z]{5}\d{4}[a-zA-Z]{1}/;
    var txtpan = $(this).val();
    if (txtpan.length == 10) {
        if (txtpan.match(regExp)) {
            
        }
        else {
            $('#lblPan').show();
            $('#lblPan').text('Not a Valid Pan');
            count++;
            return false;
        }
    }
    else {
        $('#lblPan').show();
        $('#lblPan').text('Please enter 10 digits for a valid PAN numberlid Pan');
        count++;
        return false;
    }

});
$("#Contact_email").on('change', function () {
    $.ajax({
        type: "get",
        url: CheckEmail,
        data: { email: $('#Contact_email').val() },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data == 0) {
                $('#lblEmail').text('');
            }
            else {
                $('#lblEmail').show();
                $('#Contact_email').val('');
                $('#lblEmail').text('Email Id already exists!');
            }
        },
        error: function () {
            alert("Error occured!!")
        }
    });
});

$('#Confirm_Password').focusout(function () {
    ValidatePassword();
});

function ValidatePassword() {
    if ($('#Confirm_Password').val() == '' && $('#Password').val()!='') {
        $('#lblconfirmPassword').text('Confirm password can not be empty');
    }
    else {
        if ($('#Password').val() != $('#Confirm_Password').val()) {
            $('#lblconfirmPassword').text('Confirm password should match with password')
        }
        else {
            $('#lblconfirmPassword').text('')
        }
    }
}

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}
function CheckIfEmailIdExists(EmailID) {
    
    $.ajax({
        type: "get",
        url: "../Common/CheckIfEmailIdExists",
        data: { EmailID: $('#Contact_email').val(), Module:'Company' },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data == 0) {
                $('#lblEmail').text('');
            }
            else {
                $('#lblEmail').show();
                $('#lblEmail').text('Email id already exists!');
            }
        },
        error: function () {
            alert("Error occured!!")
        }
    });
}
$("#Company_name").keypress(function () {
    $('#lblCompanyName').hide();
});
$("#Contact_CIN").keypress(function () {
    $('#lblCIN').hide();
});
$("#Password").keypress(function () {
    $('#lblPassword').hide();
});
$("#Contact_Name").keypress(function () {
    $('#lblContactPerson').hide();
});
$("#Confirm_Password").keypress(function () {
    $('#lblconfirmPassword').hide();
});
$("#Contact_Designation").keypress(function () {
    $('#lblDesignation').hide();
});
$("#Contact_email").keypress(function () {
    $('#lblEmail').hide();
});
$("#Contact_mobile").keypress(function () {
    $('#lblMobileNo').hide();
});
$("#Pan_number").keypress(function () {
    $('#lblPan').hide();
});
$("#InterestedAs").change(function () {
    $("#lblInterestedas").hide();
});
