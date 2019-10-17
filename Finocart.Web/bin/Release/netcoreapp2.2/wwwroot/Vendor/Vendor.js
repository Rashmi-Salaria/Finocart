$(document).ready(function () {
    $("#selUpDoc").change(function () {
        $("#lblDocTypeError").hide();
    })
    $('#Vendoruploaddocuments').dataTable({

        "processing": true,
        "serverSide": true,
        "searching": true,
        "dom": '"ltipr"',
        "ajax": {
            "url": "/Vendor/GetVendorDocList",
            "type": "POST",
            "datatype": "json",

        },
        "columns": [
            { "data": "docID", "name": "DocID", "autoWidth": true }, //index 1
            { "data": "documentType", "name": "DocumentType", "autoWidth": true }, //index 2
            { "data": "fileName", "name": "FileName", "autoWidth": true }, //index 3

            { "data": "Status", "defaultContent": "<i>Uploaded</i>" },
            {
                "data": function (data, type, row) {
                    
                    return "<a href='../Vendor/DeleteVendorDoc/" + data.docID + "' class='actions-ico'><img src='~/Content/images/edit.png' title='Edit' class='img-responsive' /></a>";
                }
            }

        ],
    });
  
});


