using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.MedicalRecords;
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
        public EventCallback<IncidentsCounterModel> incidentsCounterChanged;

        public AppointmentFilterModel AppointmentFilter = new AppointmentFilterModel();
        public EventCallback<AppointmentFilterModel> AppointmentValueChanged;

        public DashboardDataModel DashboardData = new DashboardDataModel();
        public EventCallback<DashboardDataModel> DashboardDataChanged;

        public string _selectedFilter;
        public EventCallback<string> _selectedFilterChanged;

        public bool AreIncidentsGraphSelected = true;


        [Inject]
        public IMedicalRecordService MedicalRecordService { get; set; }

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

            incidentsCounter.totalIncidentsCounter = await DashboardService.GetIncidentCounterAsync(String.Empty, "Día");
            incidentsCounter.maritimeIncidents = await DashboardService.GetIncidentCounterAsync("Marítimo", "Día");
            incidentsCounter.airIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Aéreo", "Día");
            incidentsCounter.groundIncidentsCounter = await DashboardService.GetIncidentCounterAsync("Terrestre", "Día");
            
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

        private void UpdateAppointmentFilteredIncidentsData()
        {
            /*Acá va la funcionalidad para refrescar los datos de los gráficos de citas.*/
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
        private async Task ClearAppointmentFilters()
        {
            DashboardData.isReadyToShowGraphs = false;
            StateHasChanged();

            AppointmentFilter = new AppointmentFilterModel();
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
            DashboardData.patients = (await MedicalRecordService.GetPatients()).ToList();
            DashboardData.filteredIncidentsData = DashboardData.incidentsData;

            DashboardData.isReadyToShowFilters = true; // Always after loading all filters data
        }

    }
}
