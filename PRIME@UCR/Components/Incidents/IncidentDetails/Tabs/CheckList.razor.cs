using MatBlazor;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class CheckList
    {
        [Parameter] public IncidentDetailsModel Incident { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }

        MatTheme AddButtonTheme = new MatTheme()
        {
            Primary = "white",
            Secondary = "#095290"
        };

        void Redirect()
        {
            NavManager.NavigateTo($"/checklist/{Incident.Code}");
        }
    }
}
