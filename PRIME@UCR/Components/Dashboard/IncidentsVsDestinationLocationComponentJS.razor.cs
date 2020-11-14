using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentsVsDestinationLocationComponentJS
    {
        private int eventQuantity { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        public IDashboardService _dashboardService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await GenerateIncidentsVsDestinationLocationComponentJS();
        }

        private async Task GenerateIncidentsVsDestinationLocationComponentJS()
        {
            var incidentsData = await _dashboardService.GetAllIncidentsAsync();

            eventQuantity = incidentsData.Count();

            var districtData = await _dashboardService.GetAllDistrictsAsync();

            var incidentsPerDestination = incidentsData.GroupBy(i => i.IdDestino);

            var results = new List<String>();

            foreach (var incidents in incidentsPerDestination)
            {
                var labelName = "No Asignado";
                var district = districtData.Where((district) => district.Id == incidents.ToList().First().IdDestino);
                if (district.Any())
                {
                    labelName = district.First().Nombre;
                }
                results.Add(labelName);
                results.Add(incidents.ToList().Count().ToString());
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsDestinationLocationComponentJS", (object)results);
        }
    }
}
