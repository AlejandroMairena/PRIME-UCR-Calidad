using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Components.Incidents.IncidentDetails.Tabs;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.IncidentDetails
{
    public partial class ActiveTab
    {
        [Parameter]
        public DetailsTab Active { get; set; }    
        
        [Parameter]
        public IncidentDetailsModel Incident { get; set; }    
        
        [Parameter]
        public EventCallback<IncidentDetailsModel> OnSave { get; set; }

        private async Task SaveDestination(DestinationModel model)
        {
            Incident.Destination = model.Destination;
            await OnSave.InvokeAsync(Incident);
        }

        private async Task SaveOrigin(OriginModel model)
        {
            Incident.Origin = model.Origin;
            await OnSave.InvokeAsync(Incident);
        }

        private async Task SaveAssignments(AssignmentModel model)
        {
            Incident.TransportUnit = model.TransportUnit;
            await OnSave.InvokeAsync(Incident);
        }
        private async Task SavePatient(PatientModel model)
       {
            Incident.Expediente = model.Expediente;
            await OnSave.InvokeAsync(Incident);
       }
    }
}