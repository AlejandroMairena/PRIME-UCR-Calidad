using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Components.Incidents.IncidentDetails.Tabs;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class IncidentDetails
    {
        const DetailsTab DefaultTab = DetailsTab.Info;
        
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Active { get; set; }

        private readonly IEnumerable<Tuple<DetailsTab, string>> _tabs = new List<Tuple<DetailsTab, string>>
        {
            Tuple.Create(DetailsTab.Info, "Información"),
            Tuple.Create(DetailsTab.Origin, "Origen"),
            Tuple.Create(DetailsTab.Destination, "Destino"),
            // Tuple.Create(DetailsTab.Patient, "Paciente")
        };
        
        private DetailsTab _activeTab;

        private Incidente _incident;

        private void OnTabSet(DetailsTab tab)
        {
            _activeTab = tab;
        }

        async Task SetIncident()
        {
            _incident = await IncidentService.GetIncidentAsync(Id);
        }
    }
}