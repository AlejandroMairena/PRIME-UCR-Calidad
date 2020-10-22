using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTable;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
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

        private ITable<IncidentListModel> Table;

        private List<IncidentListModel> incidentsList;

        [Inject]
        private IIncidentService IncidentService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            incidentsList = (await IncidentService.GetIncidentListModelsAsync()).ToList();

        }

        MatTheme AddButtonTheme = new MatTheme()
        {
            Primary = "white",
            Secondary = "#095290"
        };


        void Redirect()
        {
            NavManager.NavigateTo($"{CreateIncidentUrl}");
        }
      
    }

}




