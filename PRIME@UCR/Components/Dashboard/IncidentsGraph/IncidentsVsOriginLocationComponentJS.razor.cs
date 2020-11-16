using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.IncidentsGraph
{
    public partial  class IncidentsVsOriginLocationComponentJS
    {
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        [Parameter] public bool ZoomActive { get; set; }

        private int eventQuantity { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        public IDashboardService _dashboardService { get; set; }

        [Inject]
        IModalService Modal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await GenerateIncidentsVsOriginLocationComponentJS();
        }

        private async Task GenerateIncidentsVsOriginLocationComponentJS()
        {
            var incidentsData = await _dashboardService.GetFilteredIncidentsList(Value);

            eventQuantity = incidentsData.Count();

            var districtData = await _dashboardService.GetAllDistrictsAsync();

            var incidentsPerOrigin = incidentsData.GroupBy(i => i.IdOrigen);

            var results = new List<String>();


            foreach (var incidents in incidentsPerOrigin)
            {
                var labelName = "No Asignado";
                var district = districtData.Where((district) => district.Id == incidents.ToList().First().IdOrigen);
                if (district.Any())
                {
                    labelName = district.First().Nombre;
                }
                results.Add(labelName);
                results.Add(incidents.ToList().Count().ToString());
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsOriginLocationComponentJS", (object)results);
        }

        void ShowModal()
        {
            var modalOptions = new ModalOptions()
            {
                Class = "graph-zoom-modal blazored-modal"
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(IncidentsVsOriginLocationComponentJS.Value), Value);
            parameters.Add(nameof(IncidentsVsOriginLocationComponentJS.ZoomActive), true);
            Modal.Show<IncidentsVsOriginLocationComponentJS>("Incidentes VS Ubicacion de Origen", parameters, modalOptions);
        }
    }
}
