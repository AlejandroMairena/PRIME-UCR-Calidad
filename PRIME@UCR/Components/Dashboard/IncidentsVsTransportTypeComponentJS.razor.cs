using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard
{
    public partial class IncidentsVsTransportTypeComponentJS
    {
        private int eventQuantity { get; set; }

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
            eventQuantity = incidentsData.Count();

            await JS.InvokeVoidAsync("CreateColumnChart", (object)incidentsData);
        }
    }
}
