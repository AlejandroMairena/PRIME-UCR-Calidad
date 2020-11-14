﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.IncidentsGraph
{
    public partial class IncidentsVsTimeChartComponentJS
    {
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        private int eventQuantity { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        public IDashboardService _dashboardService { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            await GenerateColumnChart();
        }

        private async Task GenerateColumnChart()
        {
            var incidentsData =  await _dashboardService.GetFilteredIncidentsList(Value);

            eventQuantity = incidentsData.Count();

            var incidentsPerDay = incidentsData.GroupBy(i => i.Cita.FechaHoraEstimada.DayOfYear);

             var incidentes = new List<List<Incidente>> { };

            foreach (var incident in incidentsPerDay)
            {
                incidentes.Add( incident.ToList());
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsTimeChartComponent", (object)incidentes);
        }
    }
}
