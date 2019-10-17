var logs = ""
$(document).ready(function () {

    $('#tbl_UploadList').dataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',

        "ajax": {
            "url": GetUploadLogs,
            "type": "POST",
            "datatype": "json",
            "async": false
        },
        "columns": [
            { "data": "companyName", "name": "CompanyName", "autoWidth": true }, //index 2   
            { "data": "excelFileName", "name": "ExcelFileName", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },  
            {
                "data": "createdDate", "name": "CreatedDate", "autoWidth": true, "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate().toString() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },
            {
                "data": function (data, type, row) {
                    debugger;
                    //logs = data.logs;
                    return '<a href="#" onclick="GetLogs(' + data.id + ')" class="dowte">Download Log</a>';
                }
            }

        ],

    });

    oTable1 = $('#tbl_UploadList').DataTable();

    $('.searchTerm').on('keyup', function () {
        oTable1.columns(2).search($('#txt_Upload').val().trim());
        oTable1.draw();
    });
});

function GetLogs(id) {
    url = "../AnchorCompany/GetLogs?ID=" + id;
    window.location.href = url;
}