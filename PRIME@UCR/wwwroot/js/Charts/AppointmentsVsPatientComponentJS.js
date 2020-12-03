/**
 * Funciton that creates the Appointment VS patientcomponent
 * Params: result
 * [results] only contains an array the the data to include in the graph
 * 
 * NO SENSITIVE DATA SHOULD BE TREATED IN THIS CODE
 */

function CreateAppointmentVsPatientComponentJS(results) {
    am4core.ready(function () {


        var chartData = [];

        for (var i = 0; i < results.length; i += 2) {
            var destination = results[i];
            var quantity = results[i + 1];


            chartData.push({
                "destination": destination,
                "quantity": quantity
            });
        }

        //Chart Data
       // chart.data = chartData;

        // Themes
        am4core.useTheme(am4themes_animated);

        //Change language 
        //chart.language.locale = am4lang_es_ES;

        var chart = am4core.create("AppointmentVsPatientComponent", am4charts.XYChart);

        var data = [];
        var value = 12;

        var names = ["Raina",
            "Demarcus",
            "Carlo",
            "Jacinda",
            "Richie",
            "Antony",
            "Amada",
            "Idalia",
            "Janella",
            "Marla",
            "Curtis",
            "Shellie",
            "Meggan",
            "Nathanael",
            "Jannette",
            "Tyrell",
            "Sheena",
            "Maranda",
            "Briana",
            "Rosa",
            "Rosanne",
            "Herman",
            "Wayne",
            "Shamika",
            "Suk",
            "Clair",
            "Olivia",
            "Hans",
            "Glennie",
        ];

        for (var i = 0; i < names.length; i++) {
            value = Math.round(Math.random() * 12);
            data.push({ category: names[i], value: value });
        }

        chart.data = data;
        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.dataFields.category = "category";
        categoryAxis.renderer.minGridDistance = 15;
        categoryAxis.renderer.grid.template.location = 0.5;
        categoryAxis.renderer.grid.template.strokeDasharray = "1,3";
        categoryAxis.renderer.labels.template.rotation = -90;
        categoryAxis.renderer.labels.template.horizontalCenter = "left";
        categoryAxis.renderer.labels.template.location = 0.5;

        categoryAxis.renderer.labels.template.adapter.add("dx", function (dx, target) {
            return -target.maxRight / 2;
        })

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.tooltip.disabled = true;
        valueAxis.renderer.ticks.template.disabled = true;
        valueAxis.renderer.axisFills.template.disabled = true;

        var series = chart.series.push(new am4charts.ColumnSeries());
        series.dataFields.categoryX = "category";
        series.dataFields.valueY = "value";
        series.tooltipText = "{valueY.value}";
        series.sequencedInterpolation = true;
        series.fillOpacity = 0;
        series.strokeOpacity = 1;
        series.strokeDashArray = "1,3";
        series.columns.template.width = 0.01;
        series.tooltip.pointerOrientation = "horizontal";

        var bullet = series.bullets.create(am4charts.CircleBullet);

        chart.cursor = new am4charts.XYCursor();

        chart.scrollbarX = new am4core.Scrollbar();
        chart.scrollbarY = new am4core.Scrollbar();

    }); // end am4core.ready()
}