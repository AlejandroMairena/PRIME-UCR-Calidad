/**
 * Funciton that creates the Appointment VS medical center component
 * Params: result
 * [results] only contains an array the the data to include in the graph
 * 
 * NO SENSITIVE DATA SHOULD BE TREATED IN THIS CODE
 */

function CreateAppointmentsVsMedicalRecordsComponentWeightJS(results) {
    am4core.ready(function () {
        /*

        var chartData = [];

        for (var i = 0; i < results.length; i += 2) {
            var destination = results[i];
            var quantity = results[i + 1];


            chartData.push({
                "destination": destination,
                "quantity": quantity
            });
        }

*/

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        var chart = am4core.create("AppointmentsVsMedicalRecordsComponent", am4charts.XYChart);

        // Add data
        chart.data = [{
            "date": new Date(2020, 3, 21),
            "value": 60
        }, {
                "date": new Date(2020, 4, 22),
            "value": 63
        }, {
                "date": new Date(2020, 5, 23),
            "value": 62
        }, {
                "date": new Date(2020, 6, 24),
            "value": 60
        }, {
                "date": new Date(2020, 7, 25),
            "value": 61
        }, {
                "date": new Date(2020, 8, 26),
            "value": 62,
            "disabled": false
        }];



        // Create axes
        var dateAxis = chart.xAxes.push(new am4charts.DateAxis());

        dateAxis.title.text = "Fecha de la Cita";
        dateAxis.title.align = "center";
        dateAxis.title.fontWeight = 600;
        dateAxis.renderer.grid.template.location = 0.5;
        dateAxis.dateFormatter.inputDateFormat = "yyyy-MM-dd";
        dateAxis.renderer.minGridDistance = 40;
        dateAxis.tooltipDateFormat = "MMM dd, yyyy";
        dateAxis.dateFormats.setKey("day", "dd");
        // Create value axis
        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

        valueAxis.title.text = "Peso del Paciente";
        valueAxis.title.align = "center";
        valueAxis.title.fontWeight = 600;

        //Graph Scale
        valueAxis.min = 0;
        valueAxis.max = 100;
        valueAxis.maxPrecision = 0;

        // Create series
        var lineSeries = chart.series.push(new am4charts.LineSeries());

        lineSeries.dataFields.valueY = "value";
        lineSeries.dataFields.dateX = "date";
        lineSeries.name = "Sales";
        lineSeries.strokeWidth = 3;
        lineSeries.strokeDasharray = "5,4";

        // Add simple bullet
        var bullet = lineSeries.bullets.push(new am4charts.CircleBullet());
        bullet.disabled = true;
        bullet.propertyFields.disabled = "disabled";

        var secondCircle = bullet.createChild(am4core.Circle);
        secondCircle.radius = 6;
        secondCircle.fill = chart.colors.getIndex(8);


        bullet.events.on("inited", function (event) {
            animateBullet(event.target.circle);
        })


        //Animate final bullet
        function animateBullet(bullet) {
            var animation = bullet.animate([{ property: "scale", from: 1, to: 5 }, { property: "opacity", from: 1, to: 0 }], 1000, am4core.ease.circleOut);
            animation.events.on("animationended", function (event) {
                animateBullet(event.target.object);
            })
        }


    }); // end am4core.ready()
}