using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class DestinationTab
    {
        [Parameter]
        public Ubicacion Destination { get; set; }
        
        [Parameter]
        public EventCallback<DestinationModel> OnSave { get; set; }
        
        private DestinationModel _model = new DestinationModel();

        private void OnDestinationChange(Ubicacion destination)
        {
            _model.Destination = destination;
        }

        private async Task Save()
        {
            await OnSave.InvokeAsync(_model);
        }

        protected override void OnInitialized()
        {
            _model.Destination = Destination;
        }
    }
}
