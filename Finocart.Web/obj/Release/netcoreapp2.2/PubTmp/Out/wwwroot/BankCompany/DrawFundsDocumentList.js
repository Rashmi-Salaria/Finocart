$(document).ready(function () {
    if ($('#divsuccess').html() == 'success') {
        alert("Document Upload Successfully.");
        //location.href = url;
        $('#divsuccess').html('');
    }
});

$('#btnUpload').click(function () {
    var files = $("#myfile_0").get(0).files;
    var fileData = new FormData();

    for (var i = 0; i < files.length; i++) {
        fileData.append("fileInput", files[i]);
    }
    var files1 = $("#myfile_1").get(0).files;
    var fileData1 = new FormData();

    for (var i = 0; i < files1.length; i++) {
        fileData1.append("fileInput", files1[i]);
    }
    var file1 = $('#myfile_0').val();
    var file2 = $('#myfile_1').val();
    var file3 = $('#myfile_2').val();
    var file4 = $('#myfile_3').val();
    var isvalid;
    
    if (file1 == null || file1 == '' || file1 == undefined) {
        $('#error_0').html('Please upload Company PAN');
        isvalid = false;
    }
    if (file2 == null || file2 == '' || file2 == undefined) {
        $('#error_1').html('Please upload Company TAN');
        isvalid = false;
    }
    if (file3 == null || file3 == '' || file3 == undefined) {
        $('#error_2').html('Please upload Aadhar');
        isvalid = false;
    }
    if (file4 == null || file4 == '' || file4 == undefined) {
        $('#error_3').html('Please upload PAN and Aadhar of Person.');
        isvalid = false;
    }

    if (isvalid == false) {
        return false;
    }

    $(".uploadfileclass").each(function () {
        var filename = $(this).val();
    });
    
    //$('.uploadfileclass').each(function () {
    //    var id = this.id;
    //    var DynamicID = id.split('myfile_')[1];
    //    var itemvalue = $('#myfile_' + DynamicID + '').val();
    //    if (itemvalue == null || itemvalue == '' || itemvalue == undefined) {

    //        alert('please enter file');
    //        return false;
    //    }
    //});

    var filedata = $('input[type="file"]');
    var BankName = $("#BankName").val();
    var data = new FormData();
    //for (var i = 0; i < filedata.length; i++) {

    //    var file = $("#" + i).get(0);
    //    var documenttype = $("#" + i).attr("data-name");
    //    var files = file.files;

    //    if (files == undefined || files.length === 0) {
    //        //console.log('no file');
    //        alert("please upload " + documenttype);;
    //    }
    //    else {
            
    //        data.append('AnchorDocument', $('#myfile_0')[0].files[0]);
    //        data.append('BankName', BankName);
    //    }
    //}

    //$.ajax({
    //    url: "/BankCompany/UploadDocument/",
    //    type: "POST",
    //    dataType: "json",
    //    data: data,
    //    contentType: false,
    //    processData: false,
    //    success: function (data) {
    //        debugger;
    //        if (data.bResult == true) {
    //            Common.CustomSuccessNotify("Anchor document uploaded");
    //            location.reload();
    //        }
    //        else {
    //            Common.CustomErrorNotify(data.ReturnMessage);
    //            location.reload();
    //        }
    //        alert(data);
    //        $("#myfile").val('');
    //    },
        //error: function () {
        //    alert("There was error uploading files!");
        //}
    //});
});