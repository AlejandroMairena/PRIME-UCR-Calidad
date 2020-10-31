using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.MedicalRecords;
using PRIME_UCR.Components.Incidents.IncidentDetails.Tabs;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Components.MedicalRecords
{
    public partial class ActiveTab
    {
        [Parameter]
        public DetailsTab Active { get; set; }    
        
        [Parameter]
        public RecordViewModel Expediente { get; set; }

        [Parameter]
        public EventCallback<IncidentDetailsModel> OnSave { get; set; }

    }
}