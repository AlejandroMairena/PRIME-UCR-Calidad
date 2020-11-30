using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentsCounterComponent
    {
        [Inject]
        public IDashboardService DashboardService { get; set; }
        [Parameter]
        public bool Value { get; set; }
        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }
        public IncidentsCounterModel incidentsCounter;
        private readonly List<string> _filters = new List<string> { "Día", "Semana", "Mes", "Año" };
        private string _selectedFilter = "Día";

        protected override async Task OnInitializedAsync()
        {
            incidentsCounter = new IncidentsCounterModel();
            incidentsCounter.totalIncidentsCounter = await DashboardService.GetIncidentCounterAsync(String.Empty, "Día");
            incidentsCounter.maritimeIncidents = await DashboardService.GetIncidentCounterAsync("Marítimo", "Día");
            incidentsCounter.airIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Aéreo", "Día");
            incidentsCounter.groundIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Terrestre", "Día");
            Value = true;
            await ValueChanged.InvokeAsync(Value);

            incidentsCounter.isReadyToShowCounters = true;
        }

        private async Task OnFilterChange(string filter)
        {
            incidentsCounter.isReadyToShowCounters = false;
            _selectedFilter = filter;
            incidentsCounter.totalIncidentsCounter = await DashboardService.GetIncidentCounterAsync(String.Empty, filter);
            incidentsCounter.maritimeIncidents = await DashboardService.GetIncidentCounterAsync("Marítimo", filter);
            incidentsCounter.airIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Aéreo", filter);
            incidentsCounter.groundIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Terrestre", filter);
            incidentsCounter.isReadyToShowCounters = true;
        }
    }
}
