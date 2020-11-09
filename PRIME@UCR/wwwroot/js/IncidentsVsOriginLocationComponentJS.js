﻿function CreateIncidentsVsOriginLocationComponentJS(results) {
    console.log(results);
    am4core.ready(function () {


        var chartData = [];

        for (var i = 0; i < results.length; i+=2) {
            var origin = results[i];
            var quantity = results[i+1];

            console.log(origin, quantity);

            chartData.push({
                "origin": origin,
                "quantity": quantity
            });
        }


        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        var chart = am4core.create("IncidentsVsOriginLocationComponentJS", am4charts.XYChart);
        chart.padding(40, 40, 40, 40);

        //Change language 
        chart.language.locale = am4lang_es_ES;

        var categoryAxis = chart.yAxes.push(new am4charts.CategoryAxis());
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.dataFields.category = "origin";
        categoryAxis.renderer.minGridDistance = 1;
        categoryAxis.renderer.inversed = true;
        categoryAxis.renderer.grid.template.disabled = true;

        var valueAxis = chart.xAxes.push(new am4charts.ValueAxis());
        valueAxis.min = 0;

        var series = chart.series.push(new am4charts.ColumnSeries());
        series.dataFields.categoryY = "origin";
        series.dataFields.valueX = "quantity";
        series.tooltipText = "{valueX.value}"
        series.columns.template.strokeOpacity = 0;
        series.columns.template.column.cornerRadiusBottomRight = 5;
        series.columns.template.column.cornerRadiusTopRight = 5;

        var labelBullet = series.bullets.push(new am4charts.LabelBullet())
        labelBullet.label.horizontalCenter = "left";
        labelBullet.label.dx = 10;
        labelBullet.label.text = "{values.valueX.workingValue.formatNumber('#.0as')}";
        labelBullet.locationX = 1;

        // as by default columns of the same series are of the same color, we add adapter which takes colors from chart.colors color set
        series.columns.template.adapter.add("fill", function (fill, target) {
            return chart.colors.getIndex(target.dataItem.index);
        });

        categoryAxis.sortBySeries = series;
        chart.data = chartData;


    }); // end am4core.ready()
}