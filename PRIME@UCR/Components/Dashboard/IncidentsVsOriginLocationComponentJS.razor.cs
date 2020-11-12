using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial  class IncidentsVsOriginLocationComponentJS
    {
        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        public IDashboardService _dashboardService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await GenerateIncidentsVsOriginLocationComponentJS();
        }

        private async Task GenerateIncidentsVsOriginLocationComponentJS()
        {
            var incidentsData = await _dashboardService.GetAllIncidentsAsync();

            var districtData = await _dashboardService.GetAllDistrictsAsync();

            var incidentsPerOrigin = incidentsData.GroupBy(i => i.IdOrigen);

            var results = new List<String>();

            foreach (var incidents in incidentsPerOrigin)
            {
                var originName = districtData.Where((district) => district.Id == incidents.ToList().First().IdOrigen).First().Nombre;
                results.Add(originName);
                results.Add(incidents.ToList().Count().ToString());
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsOriginLocationComponentJS", (object)results);
        }
    }
}
