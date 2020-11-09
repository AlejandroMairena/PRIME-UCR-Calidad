using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.IncidentsCounter
{

    public partial class TotalIncidentsCounterComponent
    {
        [Inject]
        public IDashboardService DashboardService
        {
            get;
            set;
        }

        public int incidentsCounter;

        protected override async Task OnInitializedAsync()
        {
            incidentsCounter = await DashboardService.GetIncidentCounterAsync(String.Empty);
        }
    }
}

