using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Domain.Models;
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

             var incidentes = new List<List<Incidente>> { };

            foreach (var incident in incidentsPerDay)
            {
                incidentes.Add( incident.ToList());
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsTimeChartComponent", (object)incidentes);
        }
    }
}
