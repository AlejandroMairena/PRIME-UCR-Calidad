using ChartJs.Blazor.ChartJS.BarChart;
using ChartJs.Blazor.ChartJS.BarChart.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes.Ticks;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Wrappers;
using ChartJs.Blazor.Charts;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentsVsTransportTypeChartComponent
    {
        BarConfig _config;
        ChartJsBarChart _barChartJs;

        [Inject]
        public IDashboardService _dashboardService { get; set; }

        private  BarDataset<DoubleWrapper> _incidentsPerTransport;

        protected override async Task OnInitializedAsync()
        {

            /*
            * Bar Chart configuration Title and axis range
            */
            _config = new BarConfig
            {
                Options = new BarOptions
                {
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = "Incidentes por transporte"
                    },
                    Scales = new BarScales
                    {
                        XAxes = new List<CartesianAxis>
                    {
                        new BarCategoryAxis
                        {
                            BarPercentage = 0.5,
                            BarThickness = BarThickness.Flex
                        }
                    },
                        YAxes = new List<CartesianAxis>
                    {
                        new BarLinearCartesianAxis
                        {
                            Ticks = new LinearCartesianTicks
                            {
                                BeginAtZero = true
                            }
                        }
                    }
                    },
                    Responsive = true
                }
            };


            /*
            * Bar Chart Labels
            */
            _config.Data.Labels.AddRange(new[] { "Aéreo", "Terrestre", "Marítimo" });

            _incidentsPerTransport = new BarDataset<DoubleWrapper>
            {
                BackgroundColor = new[] { "#242968", "#123123", "#242968" },
                BorderWidth = 0,
                HoverBackgroundColor = "#f06384",
                HoverBorderColor = "#f06384",
                HoverBorderWidth = 1,
                BorderColor = "#ffffff",
            };

            /*
            * Method to get the incidents data 
            * variables to count the # of incidents
            */
            var incidentsData = await _dashboardService.GetAllIncidentsAsync();

            int aerialIncidents = 0;
            int maritimeIncidents = 0;
            int landIncidents = 0;

            foreach (var incident in incidentsData)
            {
                switch (incident.Modalidad)
                {
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
            }


            _incidentsPerTransport.AddRange(new double[] {aerialIncidents, landIncidents, maritimeIncidents,}.Wrap());
            _config.Data.Datasets.Add(_incidentsPerTransport);
        }
    }
}
