window.onload = function () {

    //var dataPoints = []; var labelAngle = 0;
    //drawChart();
    //var chart = new CanvasJS.Chart("Content_Line_Chart",
    //    {
    //        zoomEnabled: true,
    //        theme: "dark1",
    //        axisX: {
    //            //intervalType: "day",
    //            //interval: 4,
    //            title: 'Duration',
    //            titleFontColor: 'White',
    //            lineColor: 'White',
    //            labelFontColor: "white",
    //            labelFontSize: 12,
    //            labelWrap: true,
    //            labelAngle: labelAngle,
    //            titleTextStyle:
    //            {
    //                italic: false,
    //                color: '#00BBF1',
    //                fontSize: '20'
    //            },
    //            textStyle:
    //            {
    //                color: '#f5f5f5'
    //            },
    //            valueFormatString: "DD MMM YYYY",

    //        },
    //        axisY: {
    //            title: 'Amount (INR)',
    //            titleFontColor: 'White',
    //            lineColor: 'White',
    //            labelFontSize: 12,
    //            minimum: 0,
        
    //            labelFontColor: "white",

    //        },
    //        toolTip: {
    //            shared: true
    //        },
    //        colors: ['#3386C6', '#34A853', '#ff6600', '#FBBC05'],
    //        backgroundColor: '#454545',
    //        data: [{
    //            type: "line",
    //            name: "Amount",
    //            dataPoints: dataPoints
    //        }]
    //    });
    //if (dataPoints != "") {
    //    chart.render();
    //}
    //else {
    //    $("#Content_Line_Chart").html("No data available");
    //}

   
    //function drawChart() {
    //    var month = $("#selThreeMonths").val()
    //    $.ajax({
    //        type: "Post",
    //        dataType: "json",
    //        url: "../AnchorCompDashboard/GetGraphDetails?month=" + month,
    //        contentType: 'application/json; charset=utf-8',
    //        async: false,
    //        success: function (data) {
    //            if (data != null && data != "") {
    //                for (var i = 0; i < data.length; i++) {
    //                    dataPoints.push({
    //                        x: new Date(data[i].paymentDate),
    //                        y: data[i].approvedAmt
    //                    });
    //                }
    //                if (data.length > 2) {
    //                    labelAngle = -20
    //                }
    //                else {
    //                    labelAngle = 0
    //                }
    //            }
    //            else {
    //                $("#Content_Line_Chart").html("No data available");
    //            }
    //            return true;
    //        },
    //        error: function () {
    //            alert("Error occured!!")
    //        }
    //    });
    //    return false;

    //}

    //var chartType = document.getElementById('selThreeMonths');
    //chartType.addEventListener("change", function () {
    //    dataPoints = [];
    //    drawChart();
    //    chart.options.data[0].dataPoints = dataPoints;
    //    if (dataPoints.length > 2) {
    //        chart.options.axisX.labelAngle = -30
    //    }
    //    else {
    //        chart.options.axisX.labelAngle = 0
    //    }
    //    chart.options.data[0].dataPoints = dataPoints;
    //    chart.render();
    //});
    debugger;
    var graph1 = document.getElementById('anchorPayableGraph');
    graph1.addEventListener("change", function () {
        GetVendorsForAnchor();
    });
    GetVendorsForAnchor();
    var ddlVendor = document.getElementById('anchorPayableGraphVendors');
    ddlVendor.addEventListener("change", function () {
        GetChartClashFlowChartUpcomingData($("#anchorPayableGraphVendors").val());
    });

    
}

function GetChartClashFlowChartUpcomingData(vendorID) {
    debugger;
    $.ajax({
        url: '../AnchorCompDashboard/GetUpcomingPayableData',
        dataType: 'json',
        type: 'Post',
        async: true,
        data: {
            DataTypeId: $("#anchorPayableGraph").val(),
            vendorId: vendorID
        },
        success: function (result) {
            GenerateClashFlowChart(result);
        }
    });
}

function GenerateClashFlowChart(result) {
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

function GetVendorsForAnchor() {
    debugger;
    $.ajax({
        url: '../AnchorCompDashboard/GetVendorsForAnchor',
        dataType: 'json',
        type: 'Post',
        async: true,
        data: {
            DataTypeId: $("#anchorPayableGraph").val()
        },
        success: function (result) {
            $("#anchorPayableGraphVendors").html('');
            $.each(result, function (x, y) {
                $("#anchorPayableGraphVendors").append("<option value=\"" + y.vendorID + "\">" + y.vendorName+"</option>")
            });
            GetChartClashFlowChartUpcomingData($("#anchorPayableGraphVendors").val());
        }
    });

}

