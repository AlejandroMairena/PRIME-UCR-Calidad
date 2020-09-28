using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Components.Incidents.IncidentDetails.Tabs;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class IncidentDetails
    {
        const DetailsTab DefaultTab = DetailsTab.Info;
        
        [Parameter]
        public string Id { get; set; }

        protected bool exists = true;
        
        private readonly IEnumerable<Tuple<DetailsTab, string>> _tabs = new List<Tuple<DetailsTab, string>>
        {
            Tuple.Create(DetailsTab.Info, "Información"),
            Tuple.Create(DetailsTab.Origin, "Origen"),
            Tuple.Create(DetailsTab.Destination, "Destino"),
            // Tuple.Create(DetailsTab.Patient, "Paciente")
        };

        private DetailsTab _activeTab = DefaultTab;
        private IncidentDetailsModel _incidentModel;

        protected override async Task OnInitializedAsync()
        {
            _incidentModel = await IncidentService.GetIncidentDetailsAsync(Id);
            if (_incidentModel == null)
                exists = false;
        }

        private async Task Save(IncidentDetailsModel model)
        {
            _incidentModel = await IncidentService.UpdateIncidentDetails(model);
        }
    }
}