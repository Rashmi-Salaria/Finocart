//google.load("visualization", "1", { packages: ["corechart"] });  
//google.setOnLoadCallback(drawChart);
//function drawChart() {
//    var month = $("#selThreeMonths").val()
//    $.ajax({
//        type: "Post",
//        dataType: "json",
//        url: "../Vendor/GetGraphDetails?month=" + month,
//        contentType: 'application/json; charset=utf-8',
//        async: false,
//        success: function (data) {
//            if (data != null && data != "") {
//                ContentLineDashboardChart(data);
//            }
//            else
//            {
//                $("#Content_Line_Chart").html("No data available");
//            }
//                return true;
//        },
//        error: function () {
//            alert("Error occured!!")
//        }
//    });


//    return false;
//}
//function ContentLineDashboardChart(data) {
//    //$("#Content_Line_Chart").show();
//    var dataArray = new google.visualization.DataTable();

//    dataArray.addColumn('date', 'Time of Day');
//    dataArray.addColumn('number', 'Amount');
//    $.each(data, function (i, item) {
//        dataArray.addRows([
//            [new Date(item.invoiceApprovalDate), item.approvedAmt],
//        ]);
//    });

//    var options = {
//        title: "",
//        width: '0px',
//        height: '0px',
//        //"id":"qdwdw",
//        //"class":"abc",
//        titleTextStyle: { color: '#FFFFFF' },
//        pointSize: 5,
//        //legend: { position: 'none' },
//        legend:
//        {
//            position: 'top',
//            textStyle:
//            {
//                color: '#f5f5f5'
//            }

//        },
//        colors: ['#3386C6', '#34A853', '#ff6600', '#FBBC05'],
//        backgroundColor: '#454545',
//        hAxis:
//        {
//            title: 'Duration',
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
//            viewWindow: {
//                //min: new Date(2019, 4, 1),
//                //max: new Date(2019, 7, 1)
//                min: new Date(data[0].invoiceApprovalDate),
//                max: new Date(data[data.length - 1].invoiceApprovalDate)
//            },
//            gridlines: {
//                count: -1,
//                color: 'transparent',
//                units: {
//                    days: { format: ['MMM dd'] },
//                }
//            },
//        },
//        vAxis:
//        {
//            baselineColor: '#f5f5f5',
//            title: 'Amount (INR)',
//            titleTextStyle:
//            {
//                color: '#00BBF1',
//                italic: false,
//                fontSize: '20'

//            },
//            textStyle:
//            {
//                color: '#f5f5f5'
//            },
//        },

//    };

//    var chart = new google.visualization.LineChart(
//        document.getElementById('Content_Line_Chart'));

//    chart.draw(dataArray, options);
//    return false;
//}



//window.onload = function () {
//    drawChart();
//    var options = {
//        animationEnabled: true,
//        theme: "light2",
//        title: {
//            text: "Actual vs Projected Sales"
//        },
//        axisX: {
//            valueFormatString: "DD MMM"
//        },
//        axisY: {
//            title: "Number of Sales",
//            suffix: "K",
//            minimum: 30
//        },
//        toolTip: {
//            shared: true
//        },
//        legend: {
//            cursor: "pointer",
//            verticalAlign: "bottom",
//            horizontalAlign: "left",
//            dockInsidePlotArea: true,
//            itemclick: toogleDataSeries
//        },

//        data: [{
//            type: "line",
//            showInLegend: true,
//            name: "Projected Sales",
//            markerType: "square",
//            xValueFormatString: "DD MMM, YYYY",
//            color: "#F08080",
//            yValueFormatString: "#,##0K",
//            dataPoints: [
//                { x: new Date(2017, 10, 1), y: 63 },
//                { x: new Date(2017, 10, 2), y: 69 },
//                { x: new Date(2017, 10, 3), y: 65 },
//                { x: new Date(2017, 10, 4), y: 70 },
//                { x: new Date(2017, 10, 5), y: 71 },
//                { x: new Date(2017, 10, 6), y: 65 },
//                { x: new Date(2017, 10, 7), y: 73 },
//                { x: new Date(2017, 10, 8), y: 96 },
//                { x: new Date(2017, 10, 9), y: 84 },
//                { x: new Date(2017, 10, 10), y: 85 },
//                { x: new Date(2017, 10, 11), y: 86 },
//                { x: new Date(2017, 10, 12), y: 94 },
//                { x: new Date(2017, 10, 13), y: 97 },
//                { x: new Date(2017, 10, 14), y: 86 },
//                { x: new Date(2017, 10, 15), y: 89 }
//            ]
//        },
//        {
//            type: "line",
//            showInLegend: true,
//            name: "Actual Sales",
//            lineDashType: "dash",
//            yValueFormatString: "#,##0K",
//            dataPoints: [
//                { x: new Date(2017, 10, 1), y: 60 },
//                { x: new Date(2017, 10, 2), y: 57 },
//                { x: new Date(2017, 10, 3), y: 51 },
//                { x: new Date(2017, 10, 4), y: 56 },
//                { x: new Date(2017, 10, 5), y: 54 },
//                { x: new Date(2017, 10, 6), y: 55 },
//                { x: new Date(2017, 10, 7), y: 54 },
//                { x: new Date(2017, 10, 8), y: 69 },
//                { x: new Date(2017, 10, 9), y: 65 },
//                { x: new Date(2017, 10, 10), y: 66 },
//                { x: new Date(2017, 10, 11), y: 63 },
//                { x: new Date(2017, 10, 12), y: 67 },
//                { x: new Date(2017, 10, 13), y: 66 },
//                { x: new Date(2017, 10, 14), y: 56 },
//                { x: new Date(2017, 10, 15), y: 64 }
//            ]
//        }]
//    };
//    function drawChart() {
//    var month = $("#selThreeMonths").val()
//    $.ajax({
//        type: "Post",
//        dataType: "json",
//        url: "../Vendor/GetGraphDetails?month=" + month,
//        contentType: 'application/json; charset=utf-8',
//        async: false,
//        success: function (data) {
//            if (data != null && data != "") {
//                ContentLineDashboardChart(data);
//            }
//            else
//            {
//                $("#Content_Line_Chart").html("No data available");
//            }
//                return true;
//        },
//        error: function () {
//            alert("Error occured!!")
//        }
//    });


//    return false;
//}
//function ContentLineDashboardChart(data) {
//    //$("#Content_Line_Chart").show();
//    var dataArray = new google.visualization.DataTable();

//    dataArray.addColumn('date', 'Time of Day');
//    dataArray.addColumn('number', 'Amount');
//    $.each(data, function (i, item) {
//        dataArray.addRows([
//            [new Date(item.invoiceApprovalDate), item.approvedAmt],
//        ]);
//    });

//    var options = {
//        title: "",
//        width: '0px',
//        height: '0px',
//        //"id":"qdwdw",
//        //"class":"abc",
//        titleTextStyle: { color: '#FFFFFF' },
//        pointSize: 5,
//        //legend: { position: 'none' },
//        legend:
//        {
//            position: 'top',
//            textStyle:
//            {
//                color: '#f5f5f5'
//            }

//        },
//        colors: ['#3386C6', '#34A853', '#ff6600', '#FBBC05'],
//        backgroundColor: '#454545',
//        hAxis:
//        {
//            title: 'Duration',
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
//            viewWindow: {
//                //min: new Date(2019, 4, 1),
//                //max: new Date(2019, 7, 1)
//                min: new Date(data[0].invoiceApprovalDate),
//                max: new Date(data[data.length - 1].invoiceApprovalDate)
//            },
//            gridlines: {
//                count: -1,
//                color: 'transparent',
//                units: {
//                    days: { format: ['MMM dd'] },
//                }
//            },
//        },
//        vAxis:
//        {
//            baselineColor: '#f5f5f5',
//            title: 'Amount (INR)',
//            titleTextStyle:
//            {
//                color: '#00BBF1',
//                italic: false,
//                fontSize: '20'

//            },
//            textStyle:
//            {
//                color: '#f5f5f5'
//            },
//        },

//    };
//    $("#Content_Line_Chart").CanvasJSChart(options);

//    function toogleDataSeries(e) {
//        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
//            e.dataSeries.visible = false;
//        } else {
//            e.dataSeries.visible = true;
//        }
//        e.chart.render();
//    }

//    }

window.onload = function () {

    //var dataPoints = []; var labelAngle = 0;
    //drawChart();
    ////ContentLineDashboardChart()
    ////function ContentLineDashboardChart() {
    //var chart = new CanvasJS.Chart("Content_Line_Chart",
    //    {
    //        //function ContentLineDashboardChart() {
    //        //options = {
    //        //animationEnabled: true,
    //        zoomEnabled: true,
    //        theme: "dark1",
    //        //legend:
    //        //{
    //        //    position: 'top',
    //        //    textStyle:
    //        //    {
    //        //        color: '#f5f5f5'
    //        //    }

    //        //},
    //        axisX: {
    //            intervalType: "day",
    //            interval: 1,
    //            title: 'Duration',
    //            titleFontColor: 'White',
    //            lineColor: 'White',
    //            labelFontColor: "white",
    //            labelFontSize: 12,
    //            //labelMaxWidth: 50,
    //            //labelWrap: true,
    //            labelAngle: labelAngle,
    //            ////labelAngle: 50,
    //            //labelAutoFit: true,
    //            tickColor: "White",
    //            //titleTextStyle:
    //            //{
    //            //    italic: false,
    //            //    color: '#00BBF1',
    //            //    fontSize: '20'
    //            //},
    //            //textStyle:
    //            //{
    //            //    color: '#f5f5f5'
    //            //},
    //            labelFormatter: function (e) {
    //                return CanvasJS.formatDate(e.value, "DD MMM YYYY");
    //            },
    //            //valueFormatString: "DD MMM YYYY",

    //        },
    //        axisY: {
    //            //borderColor: "white",
    //            //    //baselineColor: '#f5f5f5',
                
    //            title: 'Amount (INR)',
    //            titleFontColor: 'White',
    //            lineColor: 'White',
    //            labelFontSize: 12,
    //            minimum: 0,
    //            tickColor: "White",
    //            //    titleTextStyle:
    //            //{
    //            //    italic: false,
    //            //    color: '#00BBF1',
    //            //    fontSize: '20'
    //            //},
    //            //textStyle:
    //            //{
    //            //    color: '#f5f5f5'
    //            //    },
    //            //    ticks: {
    //            //        beginAtZero: false,
    //            //        max: 100,
    //            //        min: 0,
    //            //        stepSize: 10
    //            //    },
    //            labelFontColor: "white",
    //            //suffix: "K",
    //            //minimum: 30
    //            //valueFormatString: "#,##0.##" 
    //        },
    //        toolTip: {
    //            shared: true
    //        },
    //        //legend: {
    //        //    cursor: "pointer",
    //        //    //verticalAlign: "bottom",
    //        //    //horizontalAlign: "left",
    //        //    //dockInsidePlotArea: true,
    //        //    position: 'top',
    //        //    textStyle:
    //        //    {
    //        //        color: '#f5f5f5'
    //        //    }
    //        //    //itemclick: MonthChange
    //        //},
    //        colors: ['#3386C6', '#34A853', '#ff6600', '#FBBC05'],
    //        backgroundColor: '#454545',
    //        data: [{
    //            type: "line",
    //            name: "Amount",
    //            backgroundColor: 'red',
    //            //borderColor: "#fffff",
    //            //yValueFormatString: "$#,###.##",
    //            dataPoints: dataPoints
    //            //        [  { x: 10, y: 71 },
    //            //        { x: 20, y: 55 },
    //            //    { x: 30, y: 50 },
    //            //    { x: 40, y: 65 },
    //            //    { x: 50, y: 68 },
    //            //    { x: 60, y: 28 },
    //            //    { x: 70, y: 34 },
    //            //    { x: 80, y: 14 },
    //            //    { x: 90, y: 23 }
    //            //]   
    //        }]
    //        //}

    //        //    $("#Content_Line_Chart").CanvasJSChart(options);
    //        //}
    //    });
    //if (dataPoints != "") {
    //    chart.render();
    //}
    //else {
    //    //$("#Content_Line_Chart").html("No data available");
    //}
    ////}



    ////var options = {
    ////    animationEnabled: true,
    ////    theme: "light2",
    ////    title: {
    ////        text: "Actual vs Projected Sales"
    ////    },
    ////    axisX: {
    ////        valueFormatString: "DD MMM"
    ////    },
    ////    axisY: {
    ////        title: "Number of Sales",
    ////        suffix: "K",
    ////        minimum: 30
    ////    },
    ////    toolTip: {
    ////        shared: true
    ////    },
    ////    legend: {
    ////        cursor: "pointer",
    ////        verticalAlign: "bottom",
    ////        horizontalAlign: "left",
    ////        dockInsidePlotArea: true,
    ////    },
    ////    data: [{
    ////        type: "line",
    ////        showInLegend: true,
    ////        name: "Projected Sales",
    ////        markerType: "square",
    ////        xValueFormatString: "DD MMM, YYYY",
    ////        color: "#F08080",
    ////        yValueFormatString: "#,##0K",
    ////        dataPoints:
    ////dataPoints

    ////            [
    ////            { x: new Date(2017, 10, 1), y: 63 },
    ////            { x: new Date(2017, 10, 2), y: 69 },
    ////            { x: new Date(2017, 10, 3), y: 65 },
    ////            { x: new Date(2017, 10, 4), y: 70 },
    ////            { x: new Date(2017, 10, 5), y: 71 },
    ////            { x: new Date(2017, 10, 6), y: 65 },
    ////            { x: new Date(2017, 10, 7), y: 73 },
    ////            { x: new Date(2017, 10, 8), y: 96 },
    ////            { x: new Date(2017, 10, 9), y: 84 },
    ////            { x: new Date(2017, 10, 10), y: 85 },
    ////            { x: new Date(2017, 10, 11), y: 86 },
    ////            { x: new Date(2017, 10, 12), y: 94 },
    ////            { x: new Date(2017, 10, 13), y: 97 },
    ////            { x: new Date(2017, 10, 14), y: 86 },
    ////            { x: new Date(2017, 10, 15), y: 89 }
    ////        ]           

    ////    }]

    ////};


    //function drawChart() {
    //    var month = $("#selThreeMonths").val()
    //    //$("#Content_Line_Chart").html('');
    //    $.ajax({
    //        type: "Post",
    //        dataType: "json",
    //        url: "../Vendor/GetGraphDetails?month=" + month,
    //        contentType: 'application/json; charset=utf-8',
    //        async: false,
    //        success: function (data) {
    //            if (data != null && data != "") {
    //                //ContentLineDashboardChart(data);
    //                for (var i = 0; i < data.length; i++) {
    //                    dataPoints.push({
    //                        x: new Date(data[i].paymentDate),
    //                        y: data[i].approvedAmt
    //                    });
    //                }
    //                //ContentLineDashboardChart();
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


    //    //return false;

    //}



    ////$.getJSON(drawChart);
    ////function MonthChange(objYear) {
    ////    var month = $(objYear).val();
    ////    if (month != undefined && month != null && month != 0) {
    ////        drawChart();
    ////    }
    ////}
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
    //    chart.render();
    //});
    GetAnchorCompanies();
    var graph1 = document.getElementById('ddlGraphType');
    graph1.addEventListener("change", function () {
        GetAnchorCompanies();
    });
   
    var ddlAnchrorCompany = document.getElementById('ddlAnchrorCompany');
    ddlAnchrorCompany.addEventListener("change", function () {
        GetReceivableReceivedData($("#ddlAnchrorCompany").val());
    });

}


function GetReceivableReceivedData(AnchorCompId) {
    $.ajax({
        url: '../VendorDashboard/GetUpcomingPayableData',
        dataType: 'json',
        type: 'Post',
        async: true,
        data: {
            DataTypeId: $("#ddlGraphType").val(),
            anchorCompId: AnchorCompId
        },
        success: function (result) {
            GenerateCashFlowChart(result);
        }
    });
}

function GenerateCashFlowChart(result) {
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

function GetAnchorCompanies() {
    $.ajax({
        url: '../VendorDashboard/GetAnchorCompanyForVendor',
        dataType: 'json',
        type: 'Post',
        async: true,
        data: {
            DataTypeId: $("#ddlGraphType").val()
        },
        success: function (result) {
            $("#ddlAnchrorCompany").html('');
            $.each(result, function (x, y) {
                $("#ddlAnchrorCompany").append("<option value=\"" + y.anchorCompanyId + "\">" + y.companyName + "</option>")
            });
            GetReceivableReceivedData($("#ddlAnchrorCompany").val());
        }
    });

}

