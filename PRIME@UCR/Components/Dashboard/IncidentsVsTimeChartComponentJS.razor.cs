using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentsVsTimeChartComponentJS
    {
        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        public IDashboardService _dashboardService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await GenerateColumnChart();
        }

        private async Task GenerateColumnChart()
        {
            var incidentsData = await _dashboardService.GetAllIncidentsAsync();

            var incidentsPerDay = incidentsData.GroupBy(i => i.Cita.FechaHoraCreacion.DayOfYear);

             var incidentes = new List<Point> { };

            foreach (var incident in incidentsPerDay)
            {
                Debug.WriteLine(new Point(incident.Key, incident.ToList().Count()));
                incidentes.Add(new Point(incident.Key, incident.ToList().Count()));
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsTimeChartComponent", (object)incidentes);
        }
    }
}
