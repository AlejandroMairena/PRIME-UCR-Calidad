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
using PRIME_UCR.Application.Services.Incidents;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.Services.Dashboard;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentsVsTimeChartComponent
    {
        LineConfig _lineConfig;
        ChartJsLineChart _lineChartJs;

        [Inject]
        public IDashboardService _dashboardService { get; set; }
       
        private LineDataset<Point> _incidentsPerDaySet;

        protected override async Task OnInitializedAsync()
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
 //                                   SuggestedMin = 0
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
                                },
                                Ticks = new LinearCartesianTicks
                                {
                                    SuggestedMin = 0,
                                    StepSize = 1
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

            var incidentsData = await _dashboardService.GetAllIncidentsAsync();

            var incidentsPerDay = incidentsData.GroupBy(i => i.Cita.FechaHoraCreacion.DayOfYear);

            foreach (var incident in incidentsPerDay)
            {
                _incidentsPerDaySet.Add(new Point(incident.Key, incident.ToList().Count()));
            }

            _lineConfig.Data.Datasets.Add(_incidentsPerDaySet);

             
        }//finaliza metodo
    }
}
