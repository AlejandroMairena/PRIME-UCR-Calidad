using ChartJs.Blazor.ChartJS.BarChart;
using ChartJs.Blazor.ChartJS.BarChart.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes.Ticks;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Wrappers;
using ChartJs.Blazor.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentVSTransportTypeChartComponent
    {
        BarConfig _config;
        ChartJsBarChart _barChartJs;

        protected override void OnInitialized()
        {
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

            _config.Data.Labels.AddRange(new[] { "Aereo", "Maritimo" });

            var barSet = new BarDataset<DoubleWrapper>
            {
                //Label = "My double dataset",
                BackgroundColor = new[] { "#242968" , "#123123" },
                BorderWidth = 0,
                HoverBackgroundColor = "#f06384",
                HoverBorderColor = "#f06384",
                HoverBorderWidth = 1,
                BorderColor = "#ffffff",
            };

           


            barSet.AddRange(new double[] { 8,5 }.Wrap());
            _config.Data.Datasets.Add(barSet);
        }
    }
}
