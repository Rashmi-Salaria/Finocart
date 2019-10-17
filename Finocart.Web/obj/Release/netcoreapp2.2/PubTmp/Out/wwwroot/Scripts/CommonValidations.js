
//For integer validation
function isNumberKeyValue(event) {
    //
    //alert(event.charCode);
    if ((event.charCode != 46 && (event.charCode >= 48 && event.charCode <= 57)) || (event.charCode == 0))
        return
    else
        event.preventDefault();
}

//For getting JQgrid New rowid for add
function CreateJQgridRowIdForAdd(TableId) {

    var lastRowId = $("#" + TableId).find(">tbody>tr.jqgrow").filter(":last").attr('id');

    if (lastRowId == undefined)
        lastRowId = "0";

    lastRowId = lastRowId.replace("jqg", "");

    return parseFloat(lastRowId) + 1;

}



function isNumberKey(evt) {
    
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode == 0 || charCode == 9 || charCode == "undefined" || charCode == 37 || charCode == 38 || charCode == 39)
        return true
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 35 && charCode != 36)
        return false;
    return true;
}

//For decimal validation
function isDecimalKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode == 0 || charCode == 9 || charCode == "undefined" || charCode == 37 || charCode == 38 || charCode == 39)
        return true
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 8 && charCode != 35 && charCode != 36)
        return false;
    return true;
}

//for alphanumeric validation
function isAlphaNumericKey(evt) {

    var charCode = (evt.which) ? evt.which : evt.keyCode
    //37,38,39
    if (charCode == 0 || charCode == 9 || charCode == "undefined" || charCode == 37 || charCode == 38 || charCode == 39)
        return true
    if (!(charCode > 47 && charCode < 58) && // numeric (0-9)
        !(charCode > 64 && charCode < 91) && // upper alpha (A-Z)
        !(charCode > 96 && charCode < 123) && charCode != 46 && charCode != 45 && charCode != 95 && charCode != 32 && charCode != 8 && charCode != 35 && charCode != 36 && charCode != 47 && charCode != 92)
        return false;

    return true;
}

function checkSpcialChar(event) {

    // Handling navigation keys for Firefox
    var keyChar = event.key;
    if (keyChar != null && keyChar != undefined && (keyChar == "ArrowLeft" || keyChar == "ArrowUp" || keyChar == "ArrowDown" || keyChar == "ArrowRight" || keyChar == "Home" || keyChar == "End" || keyChar == "Delete"))
        return true;

    var keyCode = event.keyCode;
    var charCode = keyCode != "" && keyCode != null && keyCode != undefined ? keyCode : event.which;
    if (charCode == 8 || charCode == 9 || charCode == 32 || charCode == undefined)
        return true;
    else if (!(charCode >= 65 && charCode <= 90 || charCode >= 97 && charCode <= 122 || charCode >= 48 && charCode <= 57))
        return false;
    else
        return true;
}

function CheckIndianZipCode(zipCode) {
    var CheckZipCode = /(^\d{6}$)/;
    if (CheckZipCode.test(zipCode))
        return true;
    else
        return false;
}


//for alphanumeric validation (space not allowed)
function isonlyAlphaNumericKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode == 0 || charCode == 9 || charCode == "undefined" || charCode == 37 || charCode == 38 || charCode == 39)
        return true
    if (!(charCode > 47 && charCode < 58) && // numeric (0-9)
        !(charCode > 64 && charCode < 91) && // upper alpha (A-Z)
        !(charCode > 96 && charCode < 123) && charCode != 46 && charCode != 45 && charCode != 95 && charCode != 8 && charCode != 47 && charCode != 92 && charCode != 35 && charCode != 36)
        return false;

    return true;
}


var format = function (num) {
    if (num == undefined || num == null)
    {
        num = 0;
    }
    var str = num.toString(), parts = false, output = [], i = 1, formatted = null;
    if (str.indexOf(".") != -1) {
        parts = str.split(".");
        str = parts[0];
    }
    str = str.split("").reverse();
    for (var j = 0, len = str.length; j < len; j++) {
        if (str[j] != ",") {
            output.push(str[j]);
            if (i % 3 == 0 && j < (len - 1)) {
                output.push(",");
            }
            i++;
        }
    }
    formatted = output.reverse().join("");
    return (formatted + ((parts) ? "." + parts[1].substr(0, 2) : ""));
};


function FormatTest(num) {
    var str = num.toString(), parts = false, output = [], i = 1, formatted = null;
    if (str.indexOf(".") != -1) {
        parts = str.split(".");
        str = parts[0];
    }
    str = str.split("").reverse();
    for (var j = 0, len = str.length; j < len; j++) {
        if (str[j] != ",") {
            output.push(str[j]);
            if (i % 3 == 0 && j < (len - 1)) {
                output.push(",");
            }
            i++;
        }
    }
    formatted = output.reverse().join("");
    return (formatted + ((parts) ? "." + parts[1].substr(0, 2) : ""));
};


function validateEmail(email) {

    var re = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return re.test(email);
}

function allowDecimals(e) {
    // Handling navigation keys for Firefox
    var keyChar = e.key;
    if (keyChar != null && keyChar != undefined && (keyChar == "ArrowLeft" || keyChar == "ArrowUp" || keyChar == "ArrowDown" || keyChar == "ArrowRight" || keyChar == "Home" || keyChar == "End" || keyChar == "Delete"))
        return true;

    var charCode = (e.which) ? e.which : e.keyCode;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else
        return true;
}

function FormatDecimals(e, target) {

    if (!allowDecimals(e)) {
        return false;
    }

    var keyChar = e.key;
    if (keyChar != null && keyChar != undefined && keyChar == "Delete") {
        return true;
    }

    //just one dot
    var charCode = (e.which) ? e.which : e.keyCode;
    var number = $(target).val().split('.');
    if (number.length > 1 && charCode == 46) {
        return false;
    }

    if ($(target).val().length == 15 && number.length == 1 && charCode != 46) {
        var keyChar = e.key;
        if (keyChar != null && keyChar != undefined && (keyChar == "ArrowLeft" || keyChar == "ArrowUp" || keyChar == "ArrowDown" || keyChar == "ArrowRight" || keyChar == "Home" || keyChar == "End" || keyChar == "Delete" || keyChar == "Backspace" || keyChar == "Tab"))
            return true;
        else
            return false;
    }

    var cursorPosition = $(target).getCursorPosition();
    var oldLength = $(target).val().length;
    $(target).val(format($(target).val()));
    var newLength = $(target).val().length;

    var keyChar = e.key;
    if ((keyChar != "Backspace" || charCode != 8) && (oldLength != newLength))
        cursorPosition = cursorPosition + 1;

    $(target).setCursorPosition(cursorPosition);
    return true;
}


function FormatDecimalsTest(e, target) {

    if (!allowDecimals(e)) {
        return false;
    }

    var keyChar = e.key;
    if (keyChar != null && keyChar != undefined && keyChar == "Delete") {
        return true;
    }

    //just one dot
    var charCode = (e.which) ? e.which : e.keyCode;
    var number = $(target).val().split('.');
    if (number.length > 1 && charCode == 46) {
        return false;
    }

    if ($(target).val().length == 15 && number.length == 1 && charCode != 46) {
        var keyChar = e.key;
        if (keyChar != null && keyChar != undefined && (keyChar == "ArrowLeft" || keyChar == "ArrowUp" || keyChar == "ArrowDown" || keyChar == "ArrowRight" || keyChar == "Home" || keyChar == "End" || keyChar == "Delete" || keyChar == "Backspace" || keyChar == "Tab"))
            return true;
        else
            return false;
    }

    var cursorPosition = $(target).getCursorPosition();
    var oldLength = $(target).val().length;
    $(target).val(formatTest($(target).val()));
    var newLength = $(target).val().length;

    var keyChar = e.key;
    if ((keyChar != "Backspace" || charCode != 8) && (oldLength != newLength))
        cursorPosition = cursorPosition + 1;

    $(target).setCursorPosition(cursorPosition);
    return true;
}

function DecimalKeyUp(e, target) {

    var number = $(target).val().split('.');

    if ($(target).val().length > 15 && number.length == 1) {
        $(target).val($(target).val().substring(0, 15));
    }

    var charCode = (e.which) ? e.which : e.keyCode;

    var cursorPosition = $(target).getCursorPosition();
    var oldLength = $(target).val().length;
    $(target).val(format($(target).val()));
    var newLength = $(target).val().length;

    var keyChar = e.key;
    if ((keyChar != "Backspace" || charCode != 8) && (oldLength != newLength))
        cursorPosition = cursorPosition + 1;

    $(target).setCursorPosition(cursorPosition);
}

function DecimalKeyUpTest(e, target) {

    var number = $(target).val().split('.');

    if ($(target).val().length > 15 && number.length == 1) {
        $(target).val($(target).val().substring(0, 15));
    }

    var charCode = (e.which) ? e.which : e.keyCode;

    var cursorPosition = $(target).getCursorPosition();
    var oldLength = $(target).val().length;
    $(target).val(FormatTest($(target).val()));
    var newLength = $(target).val().length;

    var keyChar = e.key;
    if ((keyChar != "Backspace" || charCode != 8) && (oldLength != newLength))
        cursorPosition = cursorPosition + 1;

    $(target).setCursorPosition(cursorPosition);
}


function allowNumeric(e) {
    // Handling navigation keys for Firefox
    var keyChar = e.key;
    if (keyChar != null && keyChar != undefined && (keyChar == "ArrowLeft" || keyChar == "ArrowUp" || keyChar == "ArrowDown" || keyChar == "ArrowRight" || keyChar == "Home" || keyChar == "End" || keyChar == "Delete"))
        return true;

    var charCode = (e.which) ? e.which : e.keyCode;

    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else
        return true;
}

function FormatNumeric(e, target) {

    if (!allowNumeric(e)) {
        return false;
    }

    var keyChar = e.key;
    if (keyChar != null && keyChar != undefined && keyChar == "Delete") {
        return true;
    }
    var charCode = (e.which) ? e.which : e.keyCode;

    if ($(target).val().length == 13) {
        var keyChar = e.key;
        if (keyChar != null && keyChar != undefined && (keyChar == "ArrowLeft" || keyChar == "ArrowUp" || keyChar == "ArrowDown" || keyChar == "ArrowRight" || keyChar == "Home" || keyChar == "End" || keyChar == "Delete" || keyChar == "Backspace" || keyChar == "Tab"))
            return true;
        else
            return false;
    }

    var cursorPosition = $(target).getCursorPosition();
    var oldLength = $(target).val().length;
    $(target).val(format($(target).val()));
    var newLength = $(target).val().length;

    var keyChar = e.key;
    if ((keyChar != "Backspace" || charCode != 8) && (oldLength != newLength))
        cursorPosition = cursorPosition + 1;

    $(target).setCursorPosition(cursorPosition);
    return true;
}

function NumericKeyUp(e, target) {

    if ($(target).val().length > 13) {
        $(target).val($(target).val().substring(0, 13));
    }

    var charCode = (e.which) ? e.which : e.keyCode;

    var cursorPosition = $(target).getCursorPosition();
    var oldLength = $(target).val().length;
    $(target).val(format($(target).val()));
    var newLength = $(target).val().length;

    var keyChar = e.key;
    if ((keyChar != "Backspace" || charCode != 8) && (oldLength != newLength))
        cursorPosition = cursorPosition + 1;

    $(target).setCursorPosition(cursorPosition);
}

//SET CURSOR POSITION
$.fn.setCursorPosition = function (pos) {
    this.each(function (index, elem) {
        if (elem.setSelectionRange) {
            elem.setSelectionRange(pos, pos);
        } else if (elem.createTextRange) {
            var range = elem.createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    });
    return this;
};

$.fn.getCursorPosition = function () {
    var input = this.get(0);
    if (!input) return; // No (input) element found
    if ('selectionStart' in input) {
        // Standard-compliant browsers
        return input.selectionStart;
    } else if (document.selection) {
        // IE
        input.focus();
        var sel = document.selection.createRange();
        var selLen = document.selection.createRange().text.length;
        sel.moveStart('character', -input.value.length);
        return sel.text.length - selLen;
    }
}

function CheckAlphabet(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode == 8 || charCode == 9 || charCode == 32)
        return true;
    else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
        return true;
    else
        return false;
    //(!(charCode >= 65 && charCode <= 90 || charCode >= 97 && charCode <= 122 || charCode >= 48 && charCode <= 57))
    //return false;

}