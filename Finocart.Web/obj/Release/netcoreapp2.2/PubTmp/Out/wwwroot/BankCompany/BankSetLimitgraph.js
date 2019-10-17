window.onload = function () {

    debugger;
    GetSetLimitforAnchorCompany();
    var ddlAnchorCompList = document.getElementById('SetLimitforAnchor');
    ddlAnchorCompList.addEventListener("change", function () {
        GenerateClashFlowChart($("#SetLimitforAnchor").val());
    });


}

function GenerateClashFlowChart(AnchorCompID) {
    debugger;
    $.ajax({
        url: '../BankCompany/GetAnchorSetLimitData',
        dataType: 'json',
        type: 'Post',
        async: true,
        data: {
            AnchorCompID: AnchorCompID,

        },
        success: function (result) {
            GenerateClashFlowChartData(result);
        }
    });
}

function GenerateClashFlowChartData(result) {
    debugger;
    $("#chart-container").insertFusionCharts({
        type: "line",
        width: "100%",
        height: "300",
        dataFormat: "json",
        dataSource: {
            chart: {
                caption: "Cash Flow",
                yaxisname: "Amount",
                //rotatelabels: "1",
                setadaptiveymin: "1",
                theme: "fusion",
                dateformat: "mm/dd/yyyy",
                numberscalevalue: "1000,1000,1000",
                numberprefix: "INR ",
                numvisibleplot: "3",
            },
            data: result

        }
    });
}

function GetSetLimitforAnchorCompany() {
    debugger;
    $.ajax({
        url: '../BankCompany/GetSetLimitForAnchorComp',
        dataType: 'json',
        type: 'Post',
        async: true,

        success: function (result) {
            $("#SetLimitforAnchor").html('');
            $.each(result, function (x, y) {
                $("#SetLimitforAnchor").append("<option value=\"" + y.anchor_id + "\">" + y.anchor_Name + "</option>")
            });
            GenerateClashFlowChart($("#SetLimitforAnchor").val());
        }
    });

}

