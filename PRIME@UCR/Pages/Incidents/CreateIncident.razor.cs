using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class CreateIncident
    {
        [Inject]
        public IIncidentService IncidentService { get; set; }
        
        [Inject]
        private NavigationManager NavManager { get; set; }
        
        private IncidentModel _model = new IncidentModel();
        private List<Modalidad> _modes;

        private const string DetailsUrl = "/incidents";


        void Redirect(string id)
        {
            NavManager.NavigateTo($"{DetailsUrl}/{id}");
        }
            

        async Task Create()
        {
            var result = await IncidentService.CreateIncident(_model);
            Redirect(result.Codigo);
        }

        protected override async Task OnInitializedAsync()
        {
            _modes =
                (await IncidentService.GetTransportModesAsync())
                .ToList();
            _model.Mode = _modes.First();
        }
    }
}