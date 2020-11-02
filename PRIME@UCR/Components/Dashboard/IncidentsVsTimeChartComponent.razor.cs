using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartJs.Blazor.Charts;
using ChartJs.Blazor.ChartJS.Common;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.LineChart;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes.Ticks;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.Util;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentsVsTimeChartComponent
    {
        LineConfig _lineConfig;
        ChartJsLineChart _lineChartJs;
       
        private LineDataset<Point> _incidentsPerDaySet;

        protected override void OnInitialized()
        {
            _lineConfig = new LineConfig
            {
                Options = new LineOptions
                {
                    Responsive = true,
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = "Incidentes en el tiempo"
                    },
                    Legend = new Legend
                    {
                        Display = false,
                    },
                    Tooltips = new Tooltips
                    {
                        Mode = InteractionMode.Nearest,
                        Intersect = false
                    },
                    Scales = new Scales
                    {
                        xAxes = new List<CartesianAxis>
                        {
                            new LinearCartesianAxis
                            {
                                ScaleLabel = new ScaleLabel
                                {
                                    LabelString = "Tiempo"
                                },
                                GridLines = new GridLines
                                {
                                    Display = false
                                },
                                Ticks = new LinearCartesianTicks
                                {
                                    SuggestedMin = 0
                                }
                            }
                        },
                        yAxes = new List<CartesianAxis>
                        {
                            new LinearCartesianAxis
                            {
                                ScaleLabel = new ScaleLabel
                                {
                                    LabelString = "Incidentes"
                                }
                            }
                        }
                    },//finaliza Scales
                    Hover = new LineOptionsHover
                    {
                        Intersect = true,
                        Mode = InteractionMode.Y
                    }//finaliza Hover

                }//finaliza lineOptions
            };//finaliza lineConfig
            _incidentsPerDaySet = new LineDataset<Point>
            {
                BackgroundColor = ColorUtil.ColorString(255, 255, 255, 1.0),
                BorderColor = ColorUtil.ColorString(0, 0, 255, 1.0),
                Label = "Incidentes por día",
                Fill = false,
                PointBackgroundColor = ColorUtil.ColorString(0, 0, 255, 1.0),
                BorderWidth = 1,
                PointRadius = 3,
                PointBorderWidth = 1,
                SteppedLine = SteppedLine.False,
            };

            for(var i = 0; i < 10; i++)
            {
                _incidentsPerDaySet.Add(new Point(i*2, i+2));
                //_tempPerDaySet.AddRange(weatherForecasts.Select(p => new Point(p.Date.Day, p.TemperatureF)));
            }
            _lineConfig.Data.Datasets.Add(_incidentsPerDaySet);

             
        }//finaliza metodo
    }
}
