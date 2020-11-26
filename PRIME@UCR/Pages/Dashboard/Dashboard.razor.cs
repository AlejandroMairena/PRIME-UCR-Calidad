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
        public EventCallback<FilterModel> ValueChanged;

        public IncidentsCounterModel incidentsCounter = new IncidentsCounterModel();

        public DashboardDataModel DashboardData = new DashboardDataModel();
        public EventCallback<DashboardDataModel> DashboardDataChanged;

        [Inject]
        public ILocationService LocationService { get; set; }

        [Inject]
        public IIncidentService IncidentService { get; set; }

        [Inject]
        public IDashboardService DashboardService { get; set; }

        [Inject]
        public IStateService StateService { get; set; }

        //FILTER COMPONENT
        [Parameter] public EventCallback OnDiscard { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await InitializeDashboardData();

            incidentsCounter.totalIncidentsCounter = await DashboardService.GetIncidentCounterAsync(String.Empty);
            incidentsCounter.maritimeIncidents = await DashboardService.GetIncidentCounterAsync("Marítimo");
            incidentsCounter.airIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Aéreo");
            incidentsCounter.groundIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Terrestre");

            incidentsCounter.isReadyToShowCounters = true; // Always after loading all incidents counter data
            DashboardData.isReadyToShowGraphs = true;
        }

        private async Task UpdateFilteredIncidentsData()
        {
            DashboardData.isReadyToShowGraphs = false;
            StateHasChanged();
            DashboardData.filteredIncidentsData = await DashboardService.GetFilteredIncidentsList(Value);
            DashboardData.isReadyToShowGraphs = true;
            StateHasChanged();
        }

        private async Task ClearFilters()
        {
            DashboardData.isReadyToShowGraphs = false;
            StateHasChanged();

            Value = new FilterModel();
            DashboardData = new DashboardDataModel();
            await InitializeDashboardData();
            await DashboardDataChanged.InvokeAsync(DashboardData);
            await ValueChanged.InvokeAsync(Value);
            DashboardData.isReadyToShowGraphs = true;
            StateHasChanged();
        }

        private async Task InitializeDashboardData()
        {
            DashboardData.medicalCenters = (await LocationService.GetAllMedicalCentersAsync()).ToList();
            DashboardData.countries = (await LocationService.GetAllCountriesAsync()).ToList();
            DashboardData.incidentsData = (await DashboardService.GetAllIncidentsAsync()).ToList();
            DashboardData.districts = (await DashboardService.GetAllDistrictsAsync());
            DashboardData.states = (await StateService.GetAllStates()).ToList();
            DashboardData.modalities = (await IncidentService.GetTransportModesAsync()).ToList();
            DashboardData.filteredIncidentsData = DashboardData.incidentsData;

            DashboardData.isReadyToShowFilters = true; // Always after loading all filters data
        }

    }
}
