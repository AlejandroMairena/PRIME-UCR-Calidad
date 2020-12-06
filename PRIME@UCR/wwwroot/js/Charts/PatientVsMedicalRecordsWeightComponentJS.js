/**
 * Funciton that creates the Appointment VS medical center component
 * Params: result
 * [results] only contains an array the the data to include in the graph
 * 
 * NO SENSITIVE DATA SHOULD BE TREATED IN THIS CODE
 */

function CreateAppointmentsVsMedicalRecordsWeightComponentJS(results) {
    am4core.ready(function () {
        
        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        // Create chart instance
        var chart = am4core.create("AppointmentsVsMedicalRecordsWeightComponent", am4charts.XYChart);

        //

        // Increase contrast by taking evey second color
        chart.colors.step = 2;

        // Create axes
        var dateAxis = chart.xAxes.push(new am4charts.DateAxis());
        dateAxis.renderer.minGridDistance = 50;

        // Create series
        function createAxisAndSeries(field, name, opposite) {
            var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
            if (chart.yAxes.indexOf(valueAxis) != 0) {
                valueAxis.syncWithAxis = chart.yAxes.getIndex(0);
            }

            var series = chart.series.push(new am4charts.LineSeries());
            series.dataFields.valueY = field;
            series.dataFields.dateX = "date";
            series.strokeWidth = 2;
            series.yAxis = valueAxis;
            series.name = name;
            series.tooltipText = "{name}: [bold]{valueY}[/]";
            series.tensionX = 0.8;
            series.showOnInit = true;

            var interfaceColors = new am4core.InterfaceColorSet();

            var bullet = series.bullets.push(new am4charts.CircleBullet());
            bullet.circle.stroke = interfaceColors.getFor("background");
            bullet.circle.strokeWidth = 2;

            valueAxis.renderer.line.strokeOpacity = 1;
            valueAxis.renderer.line.strokeWidth = 2;
            valueAxis.renderer.line.stroke = series.stroke;
            valueAxis.renderer.labels.template.fill = series.stroke;
            valueAxis.renderer.opposite = opposite;
        }

        

        for (var i = 0; i < results.length; i += 2) {
            var chartData = [];
            for (var j = 0; j < results[i+1].length; j += 2) {
                var date = results[i+1][j];
                var value = results[i+1][j + 1];
                chartData.push({
                    "date": date,
                    "value": value
                });
            }

            chart.data = chartData;
            createAxisAndSeries("value", results[i][0] + " Weight", false);
           
        }


        // Add legend
        chart.legend = new am4charts.Legend();

        // Add cursor
        chart.cursor = new am4charts.XYCursor();

    }); // end am4core.ready()
}