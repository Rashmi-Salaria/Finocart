//================================================================Declarations=========================================================//
//================================================================Declarations=========================================================//
$(document).ready(function () {

    $('#tbl_UserList').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": GetUserList,
            "type": "POST",
            "datatype": "json",

        },
        "columns": [
            { "data": "userID", "name": "UserID", "autoWidth": true, "visible": false }, //index 1
            { "data": "name", "name": "Name", "autoWidth": true }, //index 2
            { "data": "email", "name": "Email", "autoWidth": true }, //index 3
            { "data": "mobile", "name": "Mobile", "autoWidth": true }, //index 4
            { "data": "designation", "name": "Designation", "autoWidth": true }, //index 5
            { "data": "rolesAccess", "name": "RolesAccess", "autoWidth": true }, //index 6
            { "data": "lookupValue", "name": "LookupValue", "autoWidth": true }, //index 8
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + (month > 9 ? month: "0" + month)+ "/" + date.getFullYear();
                } }, //index 9
            {
                "data": function (data, type, row) {
                    return "<a href='../User/EditUserPage/" + btoa(data.userID) + "'' class='actions-ico'><img src='../Content/images/edit.png' title='Edit' class='img-responsive' /><a  onclick='deletemember(" + data.userID + ")'  class='actions-ico'><img src='../Content/images/delete.png' title='Delete' class='img-responsive' /></a>";
                }
            }

        ],
    });  

    oTable = $('#tbl_UserList').DataTable();
    $('#btnFilter').click(function () {
        oTable.columns(2).search($('#txt_UserName').val().trim());
        oTable.columns(6).search($('#ddl_Status').val().trim());
        oTable.draw();
    });

    $('.searchTerm').on('keyup', function () {
        debugger
        oTable.columns(2).search($('#txt_UserName').val().trim());
        oTable.columns(6).search($('#txt_RoleAccess').val().trim());
        oTable.draw();
    });

    //$('.selectstatus').change(function () {
    //    debugger
    //    oTable.columns(6).search($('#ddl_Status').val().trim());
    //    oTable.draw();
    //});
    //$('.searchrole').on('keyup', function () {
        
    //    oTable.draw();
    //});

    $('#ExportInvoiceCSV').click(function () {
        debugger;
        var UserName = $('#txt_UserName').val().trim();
        var UserRole = $('#txt_RoleAccess').val().trim();
        url = "../User/ExportUsersData?UserName=" + UserName + "&RoleName=" + UserRole;
        window.location.href = url;
    });

    $('#btnAddNew').click(function () {
        url = AddUser;
        window.location.href = url;
    });
});

function DeleteUserPage(UserId) {

    if (confirm("Are you sure you want to delete this User page?")) {

        var url = Delete_CMSPage;
        $.post(url, { ID: UserId }, function (data) {
            if (data == "Deleted") {
                alert("User Page deleted successfully");
                oTable = $('#PagesDataTable').DataTable();
                oTable.draw();
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



function deletemember(id1) {

    var r = confirm("Are you sure you want to delete");
    if (r == true) {
        $.ajax({
            url: "../User/DeleteUserPage?id=" + id1 + "",
            dataType: 'script',
            cache: false,
            contentType: false,
            processData: false,
            data: id1,
            type: 'post'
        })
        //alert("Success");
        window.location.reload();
    }

}

