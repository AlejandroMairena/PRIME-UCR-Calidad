function CreateColumnChart(incidentsData) {


    console.log(incidentsData);
    console.log(incidentsData[1]);

    am4core.ready(function () {
        // Themes begin
        am4core.useTheme(am4themes_frozen);
        am4core.useTheme(am4themes_animated);
        // Themes end

        // Create chart instance
        var chart = am4core.create("chartdiv", am4charts.XYChart3D);

        var dataArray = [];

        var aerialIncidents = 0;
        var maritimeIncidents = 0;
        var landIncidents = 0;


        incidentsData.forEach( (incident) => {
            switch (incident.modalidad) {
                case "Aéreo":
                    aerialIncidents += 1;
                    break;
                case "Terrestre":
                    landIncidents += 1;
                    break;
                case "Marítimo":
                    maritimeIncidents += 1;
                    break;
                default:
                    break;
            }
        });

        chart.data = [
            {
                "incidentType": "Aereos",
                "quantity": aerialIncidents,
            },
            {
                "incidentType": "Terrestres",
                "quantity": landIncidents,
            },
            {
                "incidentType": "Maritimos",
                "quantity": maritimeIncidents,
            },
        ];



        // Create axes
        let categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "incidentType";
        categoryAxis.renderer.labels.template.rotation = 270;
        categoryAxis.renderer.labels.template.hideOversized = false;
        categoryAxis.renderer.minGridDistance = 20;
        categoryAxis.renderer.labels.template.horizontalCenter = "right";
        categoryAxis.renderer.labels.template.verticalCenter = "middle";
        categoryAxis.tooltip.label.rotation = 270;
        categoryAxis.tooltip.label.horizontalCenter = "right";
        categoryAxis.tooltip.label.verticalCenter = "middle";

        let valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.title.text = "Tipo de transporte";
        valueAxis.title.fontWeight = "bold";

        // Create series
        var series = chart.series.push(new am4charts.ColumnSeries3D());
        series.dataFields.valueY = "quantity";
        series.dataFields.categoryX = "incidentType";
        series.name = "Cantidad";
        series.tooltipText = "{categoryX}: [bold]{valueY}[/]";
        series.columns.template.fillOpacity = .8;

        var columnTemplate = series.columns.template;
        columnTemplate.strokeWidth = 2;
        columnTemplate.strokeOpacity = 1;
        columnTemplate.stroke = am4core.color("#FFFFFF");

        columnTemplate.adapter.add("fill", function (fill, target) {
            return chart.colors.getIndex(target.dataItem.index);
        })

        columnTemplate.adapter.add("stroke", function (stroke, target) {
            return chart.colors.getIndex(target.dataItem.index);
        })

        chart.cursor = new am4charts.XYCursor();
        chart.cursor.lineX.strokeOpacity = 0;
        chart.cursor.lineY.strokeOpacity = 0;

    }); // end am4core.ready()
}