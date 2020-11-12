using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class InfoTab
    {
        [Parameter] public IncidentDetailsModel DetailsModel { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [CascadingParameter] public Task<AuthenticationState> AuthState { get; set; }
        [Inject] public IPersonService PersonService { get; set; }
        [Inject] public IIncidentService IncidentService { get; set; }
        [Inject] public IUserService UserService { get; set; }
        
        private Persona _creator;
        private Persona _currentUser;
        
        protected override async Task OnInitializedAsync()
        {
            
            var emailUser = (await AuthState).User.Identity.Name;
            _currentUser = await UserService.getPersonWithDetailstAsync(emailUser);
            
            _creator = await PersonService.GetPersonByIdAsync(DetailsModel.AdminId);
        }

        private async Task Approve()
        {
            await IncidentService
                .ApproveIncidentAsync(DetailsModel.Code, _currentUser.Cédula);
            await OnSave.InvokeAsync(null);
        }

        private async Task Reject() {
            await IncidentService
                .RejectIncidentAsync(DetailsModel.Code, _currentUser.Cédula);
            await OnSave.InvokeAsync(null);
        }
        
    }
}