window.onload = function () {

    var dataPoints = []; var labelAngle = 0;
    drawChart();
    var chart = new CanvasJS.Chart("Content_Line_Chart",
        {
            zoomEnabled: true,
            theme: "dark1",
            axisX: {
                title: 'Duration',
                titleFontColor: 'White',
                lineColor: 'White',
                labelFontColor: "white",
                labelFontSize: 12,
                labelAngle: labelAngle,
                titleTextStyle:
                    {
                        italic: false,
                        color: '#00BBF1',
                        fontSize: '20'
                    },
                textStyle:
                    {
                        color: '#f5f5f5'
                    },
                valueFormatString: "DD MMM YYYY",

            },
            axisY: {
                title: 'Amount (INR)',
                titleFontColor: 'White',
                lineColor: 'White',
                labelFontSize: 12,
                minimum: 0,

                labelFontColor: "white",

            },
            toolTip: {
                shared: true,
                contentFormatter: function (e) {
                    var str = "";
                    for (var i = 0; i < e.entries.length; i++) {
                        debugger;
                        var formateddate = $.datepicker.formatDate("dd-M-yy", e.entries[i].dataPoint.x);
                        
                        var temp = "<strong style='color: #34A853;'>" + formateddate + "</strong><br/> <span style='color: #34A853;'>Anchor Name:</span> <strong>" + e.entries[i].dataPoint.z + "</strong><br/> <span style='color: #34A853;'>Available Limits:</span> <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
                        str = str.concat(temp);
                    }
                    return (str);
                }
            },
            colors: ['#3386C6', '#34A853', '#ff6600', '#FBBC05'],
            backgroundColor: '#454545',
            data: [{
                type: "line",
                name: "Amount",
                dataPoints: dataPoints
            }]
        });
    
    if (dataPoints != "") {
        chart.render();
    }
    else {
        $("#Content_Line_Chart").html("No data available");
    }

    function drawChart() {
        var month = $("#selThreeMonths").val()
        $.ajax({
            type: "Post",
            dataType: "json",
            url: "../BankCompany/GetGraphDetails?month=" + month,
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                
                if (data != null && data != "") {
                    for (var i = 0; i < data.length; i++) {
                        dataPoints.push({
                            x: new Date(data[i].createdDate),
                            y: data[i].available_Limits,
                            z: data[i].anchor_Name
                        });
                    }
                    if (data.length > 2) {
                        labelAngle = -20
                    }
                    else {
                        labelAngle = 0
                    }
                }
                else {
                    $("#Content_Line_Chart").html("No data available");
                }
                return true;
            },
            error: function () {
                alert("Error occured!!")
            }
        });
        return false;

    }

    var chartType = document.getElementById('selThreeMonths');
    chartType.addEventListener("change", function () {
        dataPoints = [];
        drawChart();
        chart.options.data[0].dataPoints = dataPoints;
        if (dataPoints.length > 2) {
            chart.options.axisX.labelAngle = -30
        }
        else {
            chart.options.axisX.labelAngle = 0
        }
        chart.options.data[0].dataPoints = dataPoints;
        chart.render();
    });

}

