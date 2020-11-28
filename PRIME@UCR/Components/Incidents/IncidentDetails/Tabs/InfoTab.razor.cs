using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class InfoTab
    {
        [Parameter] public IncidentDetailsModel DetailsModel { get; set; }
        [Parameter] public LastChangeModel LastChange { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public Persona CurrentUser { get; set; }
        [CascadingParameter] public Task<AuthenticationState> AuthState { get; set; }
        [Inject] public IPersonService PersonService { get; set; }
        [Inject] public IIncidentService IncidentService { get; set; }
        [Inject] public IUserService UserService { get; set; }
        
        private Persona _creator;
        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            _creator = await PersonService.GetPersonByIdAsync(DetailsModel.AdminId);
            _isLoading = false;
        }
        
    }
}