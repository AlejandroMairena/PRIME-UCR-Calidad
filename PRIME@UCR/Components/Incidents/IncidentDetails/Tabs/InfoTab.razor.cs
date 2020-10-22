using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class InfoTab
    {
        [Parameter] public IncidentDetailsModel DetailsModel { get; set; }
        [Inject] public IPersonService PersonService { get; set; }

        private String _register;

        protected override async Task OnInitializedAsync()
        {
            Persona person = await PersonService.GetPersonByIdAsync(DetailsModel.AdminId);
            _register = person.Nombre+" "+person.PrimerApellido+" "+person.SegundoApellido;
        }
    }
}