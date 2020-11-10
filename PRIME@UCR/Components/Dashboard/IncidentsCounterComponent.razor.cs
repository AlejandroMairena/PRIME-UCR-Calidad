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

        public IncidentsCounterModel incidentsCounter;

        protected override async Task OnInitializedAsync()
        {
            incidentsCounter = new IncidentsCounterModel();
            incidentsCounter.totalIncidentsCounter = await DashboardService.GetIncidentCounterAsync(String.Empty);
            incidentsCounter.maritimeIncidents = await DashboardService.GetIncidentCounterAsync("Marítimo");
            incidentsCounter.airIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Aéreo");

            incidentsCounter.assignedIncidentsCounter += 3;
        }
    }
}
