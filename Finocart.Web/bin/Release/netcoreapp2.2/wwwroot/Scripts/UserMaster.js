/*=====================================Declaration=======================================*/
var lstUserType = "", lstRole = '';
/*=====================================End Declaration=======================================*/

$(document).ready(function () {
    GetUserMasterDLL();     //To get the ddl on page load
    //$(document).on("click", "[id^=BtnEdit]", function () {
    //URL: UpdateDetails;
    
    //Hide label controls on load.
    $('#lblErrLoginAddUser').hide();
    $('#lblErrLoginId').hide();
    $('#lblErrLoginName').hide();
    $('#lblErrPassword').hide();
    $('#lblErrLoginConPw').hide();
    $('#lblErrLoginRole').hide();
    $('#lblErrContactNo').hide();
    $('#lblErrCorrectContactNo').hide();
    $('#lblErrLoginMatchPw').hide();
    $('#lblErrLoginPwLength').hide();
    $('#lblErrLoginIdLenth').hide();
    $('#lblErrLoginInvalidId').hide();
    $('#lblErrLoginIdNumeric').hide();
    //$('#lblDataUpdate').hide();

    // Retrive Current Rows Values
    $("#tblUserList").on('click', '#BtnEdit', function () {
        
        $('#lblErrEditLoginName').hide();
        $('#lblErrEditRoleName').hide();
        $('#lblErrEditContactNum').hide();
        $('#lblErrEditCorrectContactNum').hide();
        var currentRow = $(this).closest("tr");
        var LoginID = $("#txt_LoginId").val(currentRow.find("td:eq(1)").text().trim());
        var LoginName = $("#txt_LoginName").val(currentRow.find("td:eq(0)").text().trim());
        var ContactNumber = $("#txt_ContactNumber").val(currentRow.find("td:eq(6)").text().trim());
        BindDropDown($("#IsWebTerminalUpdateUser"), lstUserType, '', $(currentRow.find("[aria-describedby=tblUserList_UserCode]")).html());
        BindDropDown($("#RoleName"), lstRole, '', $(currentRow.find("[aria-describedby=tblUserList_RoleID]")).html());
        if (currentRow.find("td:eq(7)").text().trim() == "true") {
            $("#IsActive").attr('checked', true);
        }
        else {
            $("#IsActive").attr('checked', false);
        }
        IsActive == true ? true : false;
        $('#demoedit').modal('show');

    });


    //Deactivate User then Display Pop-up Window on Screen
    $("#tblUserList").on('click', '#BtnDeactive', function () {
        var currentRow = $(this).closest("tr");
        var HFLoginID = $("#txt_Login").val(currentRow.find("td:eq(1)").text().trim())
        $("#txt_Login").hide();
        $('#demoDeactive').modal('show');
    });

    //show Column and list user data 
    //In Action Column bind two Button as Edit and Deactivate Button
    //jQuery("#tblUserList").jqGrid({

    //    url: GetUserList,
    //    datatype: "json",
    //    height: 'auto',
    //    width: 900,
    //    autowidth: true,
    //    sortname: 'LoginName',
    //    shrinkToFit: true,
    //    rowList: [10, 20, 30],
    //    sortorder: "desc",

    //    colNames: ['Name', 'Email ID', 'Role', 'Created by', 'Action', 'ID', 'ContactNumber', 'Status', 'hdnIsActive', 'User Code', 'Rolecode'],
    //    colModel: [
    //                 { name: 'LoginName', index: 'Name', sortable: true, title: false, align: 'Center' },
    //                  { name: 'LoginID', index: 'LoginID/EmailID', sortable: false },
    //                   { name: 'RoleName', index: 'Role', sortable: true, editable: true },
    //                     { name: 'Createdby', index: 'Created by', sortable: true, editable: true },
    //                      { name: 'act', index: 'act', sortable: false, align: 'Center' },
    //                       { name: 'ID', index: 'ID', sortable: true, hidden: true, title: false },
    //                { name: 'ContactNumber', index: 'ContactNumber', hidden: true, sortable: true, editable: true },
    //                 { name: 'IsActive', index: 'Status', sortable: true, editable: true, hidden: true },
    //               { name: 'hdnIsActive', index: 'hdnIsActive', sortable: true, hidden: true, editable: true },
    //             { name: 'UserCode', index: 'User Code', sortable: true, editable: true, hidden: true },
    //              { name: 'RoleID', index: 'Rolecode', sortable: true, editable: true, hidden: true },



    //    ],

    //    pager: '#pager',
    //    pgbuttons: true,
    //    viewrecords: true,
    //    caption: "user detail",
    //    gridcomplete: function () {
    //        jquery('#txtsearch').keyup(function () {
    //            

    //            searchcolumn = jquery("#tbluserlist").jqgrid('getcol', 'loginname', true)

    //            
    //            var searchstring = jquery(this).val().tolowercase()
    //            jquery.each(searchcolumn, function () {
    //                if (this.value.tolowercase().indexof(searchstring) == -1) {
    //                    jquery('#' + this.id).hide()
    //                } else {
    //                    jquery('#' + this.id).show()
    //                }
    //            })
    //        })

          
    //        var ids = jquery("#tbluserlist").jqgrid('getdataids');
    //        for (var i = 0; i < ids.length; i++) {
    //            var cl = ids[i];
    //            be = "<img id='btnedit' src='../content/images/edit.png' title='Edit' style='height:20px;width:20px;cursor: pointer;margin-right:10px; class='img-responsive'/><img id='btndeactive' src='../content/images/shape-26.png' title='Delete' style='height:20px;width:20px;cursor: pointer;margin-right:10px; class='img-responsive'/>"
    //            be = "<input style='height:22px;width:20px;' type='button' id='btnedit' value='e'  />"
    //            se = "<input style='height:22px;width:20px;' type='button' id='btndeactive'value='d'   />";
    //            jquery("#tbluserlist").jqgrid('setrowdata', ids[i], { act: be });
    //        }
    //    },
    //});



    //Added by Prabir(Start)
    $.ajax({
        type: "GET",
        url: GetUserList,
        dataType: "json",
        success: OnSuccessOfMaster,
        error: function (result) {
            alert("Error");
        }
    });

    function OnSuccessOfMaster(data) {
        colNames = ['Name', 'Login ID', 'Role', 'Created by', 'Action', 'ID', 'ContactNumber', 'Status', 'hdnIsActive', 'User Code', 'Rolecode'],
        colModel = [
                         { name: 'LoginName', width: 180, index: 'Name', sortable: true, title: false, align: 'Center' },
                          { name: 'LoginID', index: 'LoginID/EmailID', sortable: true, width: 325 },
                           { name: 'RoleName', index: 'Role', sortable: true, editable: true, width: 180 },
                             { name: 'Createdby', index: 'Created by', sortable: true, editable: true, width: 180 },
                              { name: 'act', index: 'act', sortable: false, align: 'Center', width: 180 },
                               { name: 'ID', index: 'ID', sortable: true, hidden: true, title: false },
                        { name: 'ContactNumber', index: 'ContactNumber', hidden: true, sortable: true, editable: true },
                         { name: 'IsActive', index: 'Status', sortable: true, editable: true, hidden: true },
                       { name: 'hdnIsActive', index: 'hdnIsActive', sortable: true, hidden: true, editable: true },
                     { name: 'UserCode', index: 'User Code', sortable: true, editable: true, hidden: true },
                      { name: 'RoleID', index: 'Rolecode', sortable: true, editable: true, hidden: true },



        ],

        CallJQGrid('#tblUserList', data, colNames, colModel,'','',true);
    }

    //Get JQgrid
    function CallJQGrid(id, objData, colName, colData, sortCol1Name, sortCol2Name, IsPagingEnable, IsSrNoEnable, IshideHorzitalBorder, sorttype, Isautowidth, loadCompleteFN) {
        var rowNum = 0;
        var idForPager = '';
        IsPagingEnable = IsObjectUndefinedOrNull(IsPagingEnable) ? false : true;
        IsSrNoEnable = IsObjectUndefinedOrNull(IsSrNoEnable) ? false : true;
        IshideHorzitalBorder = IsObjectUndefinedOrNull(IshideHorzitalBorder) ? false : true;
        sorttype = IsObjectUndefinedOrNull(sorttype) ? 'string' : sorttype;
        Isautowidth = IsObjectUndefinedOrNull(Isautowidth) ? true : Isautowidth;
        if (IsPagingEnable) {
            idForPager = "pager_" + id.replace('#', '');
            $(id).after('<div id="' + idForPager + '"></div>');
            rowNum = 10;
            $('#' + idForPager).show();
        }
        else {
            rowNum = objData.length;
            $('#' + idForPager).show();
        }
        if (IshideHorzitalBorder) $('.ui-jqgrid-labels > tr.ui-row-ltr > td').css('border-bottom-color', 'transparent')
        jQuery(id).jqGrid({
            height: "auto",
            datatype: "local",
            data: objData,
            gridview: true,
            sortname: sortCol1Name,
            sorttype: sorttype,
            colNames: colName,
            colModel: colData,
            viewrecords: true,
            //autowidth: true,
            //autowidth: Isautowidth,
            height: 'auto',
            //width: 900,
            //autowidth: true,
            sortname: 'LoginName',
            shrinkToFit: false,
            rowList: [10, 20, 30],
            //sortorder: "desc",
            cmTemplate: { title: false },
            rownumbers: IsSrNoEnable,
            rowNum: rowNum,
            rowList: [10, 20, 30],
            pager: '#' + idForPager,
            emptyrecords: "No records to display",
            loadComplete: loadCompleteFN,
            pgbuttons: true,
            viewrecords: true,
            caption: "User Detail",
            gridComplete: function () {
                var ids = jQuery("#tblUserList").jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];
                    be = "<img id='BtnEdit' src='../Content/images/edit.png' title='Edit' style='height:20px;width:20px;cursor: pointer;margin-right:10px; class='img-responsive'/><img id='BtnDeactive' src='../Content/images/shape-26-old.png' title='OFF' style='height:20px;width:20px;cursor: pointer;margin-right:10px; class='img-responsive'/>"
                    //be = "<input style='height:22px;width:20px;' type='button' id='BtnEdit' value='E'  />"
                    //se = "<input style='height:22px;width:20px;' type='button' id='BtnDeactive'value='D'   />";
                    jQuery("#tblUserList").jqGrid('setRowData', ids[i], { act: be });
                }
            }
        });
        jQuery(id).jqGrid('clearGridData');
        jQuery(id).jqGrid('setGridParam', { data: objData });
        jQuery(id).trigger('reloadGrid');
    }

    //------------------To Call Search
    $(document).on("input", "[id='txtSearch']", function () { CallJQGrid_SmartSearch('tblUserList', $(this).val()) }); //Smart search for postion grid

    //---------------To Load Search
    //Smart search in JQGrid
    function CallJQGrid_SmartSearch(jqGridID, searchText, colName) {
        try {
            $grid = $('#' + jqGridID);
            var postData = $grid.jqGrid("getGridParam", "postData"),
                                colModel = $grid.jqGrid("getGridParam", "colModel"),
                                rules = [],
                                l = colModel.length,
                                i,
                                cm;
            for (i = 0; i < l; i++) {
                cm = colModel[i];
                if (cm.search !== false && (cm.stype === undefined || cm.stype === "text")) {
                    rules.push({
                        field: IsObjectUndefinedOrNull(colName) ? cm.name : colName,
                        op: "cn",
                        data: searchText
                    });
                }
            }
            postData.filters = JSON.stringify({
                groupOp: "OR",
                rules: rules
            });
            $grid.jqGrid("setGridParam", { search: true });
            $grid.trigger("reloadGrid", [{ page: 1, current: true }]);
        }
        catch (ex) {
            console.log(ex);
            //errorFunction(ex);
            BV_Common.AlertPopUp("No record found", 'green');
            return false;
        }
    }
    function IsObjectUndefinedOrNull(obj) {
        try {
            if (typeof obj === 'undefined' || obj == undefined || obj == null) {
                return true;
            }
            else {
                return false;
            }
        }
        catch (err) {
            return true;
        }
    }
    //(End)


  
    //when Click on Add User Button then Display Popup Window
    $("#btnShow").click(function () {
        $('#demoModal').modal('show');
        $('#lblErrLoginId').hide();
        $('#lblErrLoginName').hide();
        $('#lblErrPassword').hide();
        $('#lblErrLoginConPw').hide();
        $('#lblErrLoginRole').hide();
        $('#lblErrContactNo').hide();
        $('#lblErrLoginAddUser').hide();
        $('#lblErrCorrectContactNo').hide();
        $('#lblErrLoginMatchPw').hide();
        $('#lblErrLoginPwLength').hide();
        $('#lblErrLoginIdLenth').hide();
        $('#lblErrLoginIdNumeric').hide();
        BindDropDown($("#ddlUserType"), lstUserType, '-Select User Type-');
        BindDropDown($("#ddlRole"), lstRole, '-Select Role-');
    });
    //To hide validation after input.
   
        $("#txtLoginId").keyup(function () {

            $('#lblErrLoginId').hide();

            $('#lblErrLoginIdLenth').hide();
            $('#lblErrLoginInvalidId').hide();
            $('#lblErrLoginIdNumeric').hide();
            $('#loginid_exits').text("");
        });
        $("#txtLoginName").keyup(function () {
            $('#lblErrLoginName').hide();
        });
        $("#txtPassword").keyup(function () {
            $('#lblErrPassword').hide();
        });
        $("#txtconfirmPassword").keyup(function () {
            $('#lblErrLoginConPw').hide();
        });
        $("#txtContactNumber").keyup(function () {
            $('#lblErrContactNo').hide();
        });
        $("#ddlUserType").change(function () {
            $('#lblErrLoginAddUser').hide();
        });
        $("#ddlRole").change(function () {
            $('#lblErrLoginRole').hide();
        });
    

        $("#txt_LoginName").keyup(function () {
            $('#lblErrEditLoginName').hide();
        });

        $("#txt_ContactNumber").keyup(function () {
            $('#lblErrEditContactNum').hide();
            $('#lblErrEditCorrectContactNum').hide();
        });
    //Email Validation Function
    function validateEmail(LoginID) {
       
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            //var address = document.getElementById[email].value;
            if (reg.test(LoginID) == false) {
                //alert('Invalid Email Address');
                $('#lblErrLoginInvalidId').show();
                $('#txtLoginId').val("");
                return (false);
            }                
    }

    $(document).on('change', '#txtLoginId', function (evt) {
        var IsWebTerminalUser = $('#ddlUserType').val();
        var LoginID = $('#txtLoginId').val();

        if (IsWebTerminalUser == 'W') {
      
            if(validateEmail(LoginID))
            {
               
            }
           
        }

        if (IsWebTerminalUser == 'T') {
           
            if (!LoginIdNumeric(LoginID,evt)) {
                $('#lblErrLoginIdNumeric').show();
            }
        }

        //Added by Prabir(Start)
        $.post(IsLoginIdExist, { LoginID: LoginID }, function (data) {
            if (data == "1") {
                $("#txtLoginId").val("");
                $('#loginid_exits').text("Login ID already exist").css('color', 'red', 'font-weight', '700', 'text-transform' , 'uppercase');
                error = true;
            }
            else {
                $('#loginid_exits').text("");
                error = false;
            }
        });
        //(End)

    });
    //Allow Nemric value in login id
    
    //$(document).on('keypress', '#txtLoginId', function (evt) {
    function LoginIdNumeric(LoginID,evt) {
        
        var IsWebTerminalUser = $('#ddlUserType').val();
        //var LoginID = $('#txtLoginId').val();
        if (IsWebTerminalUser == 'T') {

            //if ((evt.which > 31 && evt.which < 48) ||
            //  (evt.which > 58 && evt.which < 65) ||
            //  (evt.which > 64 && evt.which < 90) ||
            //  (evt.which > 90 && evt.which < 127)
            //  )
            if (LoginID.match(/^\d+$/)) {
             //   alert("mumeric");
                return true;
            }
            else {
                //alert('allow only numeric');
                $('#lblErrLoginIdNumeric').show();
                $('#txtLoginId').val("");
                return false;
            }

           
        }
    }
        //});
    
   
    $('#BtnCancel').click(function () {
        var IsWebTerminalUser = $('#ddlUserType').val(0);
        var LoginID = $('#txtLoginId').val("");
        var LoginName = $('#txtLoginName').val("");
        var Password = $('#txtPassword').val("");
        var confirmPassword = $('#txtconfirmPassword').val("");
        var RoleID = $('#ddlRole').val(0);
        var ContactNumber = $('#txtContactNumber').val("");
        $('#loginid_exits').text("");//Added by Prabir biswas
    });

    //  allow numeric value in contact number 
    
    $(document).on('keypress', '#txtContactNumber', function (evt) {
              
        var LoginID = $('#txtContactNumber').val();
               if ((evt.which > 31 && evt.which < 48) ||
              (evt.which > 58 && evt.which < 65) ||
              (evt.which > 64 && evt.which < 90) ||
              (evt.which > 90 && evt.which < 127)
              ) {

                alert('allow only numeric');
                $('#txtContactNumber').val("");
                return false;
            }
      //  }

    });
    // allow numeric on update contact number
    $(document).on('keypress', '#txt_ContactNumber', function (evt) {
        
        var LoginID = $('#txt_ContactNumber').val();
        if ((evt.which > 31 && evt.which < 48) ||
       (evt.which > 58 && evt.which < 65) ||
       (evt.which > 64 && evt.which < 90) ||
       (evt.which > 90 && evt.which < 127)
       ) {

            alert('allow only numeric');
           
            return false;
        }
        //  }

    });
    

    //Save Data When We Enter Data into Popup Window
    $(function () {
        $('#btnSave').click(function () {
            
            
            
            var IsWebTerminalUser = $('#ddlUserType').val();
            var LoginID = $('#txtLoginId').val();
            var LoginName = $('#txtLoginName').val();
            var Password = $('#txtPassword').val();
            var confirmPassword = $('#txtconfirmPassword').val();
            var RoleID = $('#ddlRole').val();
            var ContactNumber = $('#txtContactNumber').val();
            $('#loginid_exits').text("");//Added by Prabir
            var errormsg = false;
            if (IsWebTerminalUser == "T") {
                if (LoginID != "") {
                    if (LoginID.length > 5) {
                        $('#lblErrLoginIdLenth').show();
                        // errormsg = errormsg + "Login ID should have atleast should have 5 digits.\n";
                        errormsg = true;
                    }
                }

            }           
            if (IsWebTerminalUser == 0) {
                $('#lblErrLoginAddUser').show();
                errormsg = true;
            }
            if (LoginID == "") {
                
                //errormsg = errormsg + "Please Enter Login ID\n";
              
                $('#lblErrLoginId').show();
                errormsg = true;
                
            }
            //else if ((IsWebTerminalUser == "T") && LoginID.length > 5) {
            //    $('#lblErrLoginIdLenth').show();
            //    // errormsg = errormsg + "Login ID should have atleast should have 5 digits.\n";
            //    errormsg = true;
            //}
            if (LoginName == "") {
                //errormsg = errormsg + "Please Enter Login Name\n";
                $('#lblErrLoginName').show();
                errormsg = true;
            }
        
            if (Password == "") {
                $('#lblErrPassword').show();
                errormsg = true;
                //errormsg = errormsg + "Please Enter Password\n";
            }
            else if (Password.length < 6) {
                // errormsg = errormsg + "Password length should be more than 6.\n";
                $('#lblErrLoginPwLength').show();
                errormsg = true;
            }

            if (confirmPassword == "") {
                // errormsg = errormsg + "Please Enter Confirm Password\n";
                $('#lblErrLoginConPw').show();
                errormsg = true;
            }
            if (confirmPassword != "") {
                if (Password != confirmPassword) {

                    $('#lblErrLoginMatchPw').show();
                    errormsg = true;
                }
            }

            if (ContactNumber == "") {
                // errormsg = errormsg + "Please Enter Contact Number\n";
                $('#lblErrContactNo').show();
                errormsg = true;

            }
            else if (ContactNumber != "" && ContactNumber.length > 10) {
                $('#lblErrCorrectContactNo').show();
                errormsg = true;
            }
         
            if (RoleID == 0) {
                // errormsg = errormsg + "Please Enter RoleID\n";
                $('#lblErrLoginRole').show();
                errormsg = true;
            }


            //if (errormsg != "") {
            //    alert(errormsg);
            //    return false;
            //}
            if (errormsg == true) {
                return false;
            }
            if (IsWebTerminalUser != '', LoginID != '' && LoginName != '' && Password != '' && RoleID != '' && ContactNumber != '') {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: AddUserData,
                    data: JSON.stringify({ IsWebTerminalUser: IsWebTerminalUser, LoginID: LoginID, LoginName: LoginName, Password: Password, RoleID: RoleID, ContactNumber: ContactNumber }),
                    dataType: "json",
                    success: function (data) {
                        alert("Data Save Successfully");
                        location.reload();

                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
            }

        })
    });
    
 
  
    //UpData on current rows
    $(function () {
        $('#BtnUpdate').click(function () {
            

            var IsWebTerminalUpdateUser = $('#IsWebTerminalUpdateUser').val();
            var LoginID = $('#txt_LoginId').val();
            var LoginName = $('#txt_LoginName').val();
            var RoleName = $('#RoleName').val();
            var ContactNumber = $('#txt_ContactNumber').val();
            var IsActive = $($('#IsActive')).is(":checked");
            var errormsg = false;
            if (LoginName == "") {
                //errormsg = errormsg + "Please Enter Login Name\n";
                $('#lblErrEditLoginName').show();
                errormsg = true;
            }

            if (ContactNumber == "") {
              
                $('#lblErrEditContactNum').show();
                errormsg = true;
            } else if (ContactNumber.length > 10)
            {
                $('#lblErrEditCorrectContactNum').show();
                errormsg = true;
            }
            if (RoleName == 0)
            {
                $('#lblErrEditRoleName').show();
                errormsg = true;
            }
            if (errormsg == true) {
                return false;
            }
            //if (errormsg != "") {
            //    alert(errormsg);
            //    return false;
            //}

            if (IsWebTerminalUpdateUser != '' && LoginID != '' && LoginName != '' && RoleName != '' && ContactNumber != '') {

                $.ajax({

                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: updatedetails,
                    data: JSON.stringify({ LoginID: LoginID, LoginName: LoginName, ContactNumber: ContactNumber, IsActive: IsActive, RoleName: RoleName, IsWebTerminalUser: IsWebTerminalUpdateUser }),
                    dataType: "json",
                    success: function (data) {
                        alert("Data Updated Successfully.")
                       // $('#lblDataUpdate').show();
                        // $('#dvDataUpdate').delay(5000).fadeOut();
                        
                        location.reload();
                    },
                    error: function (result) {

                    }
                });
            }
            else {
                alert('Please enter all the fields')
                return false;
            }
        })
    });

    
})

//When We click On Deactive Button Then Deactivate User From List 
$(function () {
    $('#Btn_Deactive').click(function () {

        var LoginID = $('#txt_Login').val();
        $.ajax({

            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: DeactiveUser,
            data: JSON.stringify({ LoginID: LoginID }),
            dataType: "json",
            success: function (data) {
                location.reload();
            },
            error: function (result) {
            }

        });
    });
});
//$(function () {
//    $('#btnSearch').click(function () {
    
//        
//        var LoginID = $('#txtSearch').val();
//        $.ajax({
//           // type: "Json",
//            contentType: "application/json; charset=utf-8",
//            url: GetSearchData,
//            data: JSON.stringify({ LoginID: LoginID }),
//            dataType: "json",          
//            success: function (data) {
                         
//            },
//            error: function (result) {
//            }

//        });
//    });
//});

function GetUserMasterDLL() {
    $.ajax({
        async: false,
        url: GetUserMasterDLLURL,
        type: 'POST',
        dataType: 'json',
        data: '',
        success: GetDropDownValue,
        error: errorFunction,
    })
}

function GetDropDownValue(data) {
    //var data = $.parseJSON(data);
    lstUserType = data.Table;
    lstRole = data.Table1;
}

function BindDropDown(objControl, jsonToBind, SelectMsg, ValueToBeSelected) {
    if (jsonToBind == '')
        objControl.append($("<option></option>").val(0).html('--No Data--'));
    // if (!Convert.IsObjectNullOrEmpty(jsonToBind)) {
    objControl.empty();
    if (SelectMsg != '' && SelectMsg != undefined) {
        objControl.append($("<option></option>").val(0).html(SelectMsg));
    }
    $.each(jsonToBind, function (key, item) {
        objControl.append($("<option></option>").val(item.code).html(item.value));
    });
    if (ValueToBeSelected != '' && ValueToBeSelected != undefined) {
        objControl.val(ValueToBeSelected).attr("selected", "selected");
    }
    //}
}
function errorFunction(err) {
    console.log(err);
}