using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTable;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Implementations.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Incidents.IncidentDetails.Tabs;
using PRIME_UCR.Domain.Models;


namespace PRIME_UCR.Pages.Incidents
{
    public partial class IncidentList
    {
        private const string CreateIncidentUrl = "/incidents/create";

        [Inject]
        private NavigationManager NavManager { get; set; }

        private ITable<Incidente> Table;

        private List<Incidente> incidentsList;

        [Inject]
        private IIncidentService IncidentService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            incidentsList = (await IncidentService.GetAllAsync()).ToList();

        }


        void Redirect()
        {
            NavManager.NavigateTo($"{CreateIncidentUrl}");
        }
      
    }

}




