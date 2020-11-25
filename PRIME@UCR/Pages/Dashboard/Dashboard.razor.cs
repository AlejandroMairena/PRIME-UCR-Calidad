using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Dashboard.IncidentsGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.Dashboard
{
    public partial class Dashboard
    {
        public FilterModel Value = new FilterModel();

        public IncidentsCounterModel incidentsCounter = new IncidentsCounterModel();

        public DashboardDataModel DashboardData = new DashboardDataModel();

        [Inject]
        public ILocationService LocationService { get; set; }

        [Inject]
        public IIncidentService IncidentService { get; set; }

        [Inject]
        public IDashboardService DashboardService { get; set; }

        [Inject]
        public IStateService StateService { get; set; }

        //FILTER COMPONENT
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }


        protected override async Task OnInitializedAsync()
        {
            DashboardData.medicalCenters = (await LocationService.GetAllMedicalCentersAsync()).ToList();
            DashboardData.countries = (await LocationService.GetAllCountriesAsync()).ToList();
            DashboardData.incidentsData = (await DashboardService.GetAllIncidentsAsync()).ToList();
            DashboardData.districts = (await DashboardService.GetAllDistrictsAsync());
            DashboardData.states = (await StateService.GetAllStates()).ToList();
            DashboardData.modalities = (await IncidentService.GetTransportModesAsync()).ToList();

            DashboardData.isReadyToShowFilters = true; // Always after loading all filters data

            incidentsCounter.totalIncidentsCounter = await DashboardService.GetIncidentCounterAsync(String.Empty);
            incidentsCounter.maritimeIncidents = await DashboardService.GetIncidentCounterAsync("Marítimo");
            incidentsCounter.airIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Aéreo");
            incidentsCounter.groundIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Terrestre");

            incidentsCounter.isReadyToShowCounters = true; // Always after loading all incidents counter data
        }

        private async Task ClearFilters()
        {
            Value = new FilterModel();
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
