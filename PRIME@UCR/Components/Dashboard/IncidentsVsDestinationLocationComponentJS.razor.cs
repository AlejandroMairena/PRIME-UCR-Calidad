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

            var incidentsPerOrigin = incidentsData.GroupBy(i => i.IdDestino);

            var results = new List<String>();

            foreach (var incidents in incidentsPerOrigin)
            {
                Debug.WriteLine(incidents.ToList()[0].IdDestino.ToString());
                var labelName = incidents.ToList()[0].IdDestino.ToString();
                if (labelName == "" || labelName == null)
                {
                    labelName = "No Asignado";
                }
                results.Add(labelName);
                results.Add(incidents.ToList().Count().ToString());
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsDestinationLocationComponentJS", (object)results);
        }
    }
}
