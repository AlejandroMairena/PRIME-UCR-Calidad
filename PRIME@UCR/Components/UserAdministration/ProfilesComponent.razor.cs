using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class ProfilesComponent
    {
        [Inject]
        public IProfilesService profilesService { get; set; }

        public List<Perfil> ListProfiles { get; set; }

        public Perfil selectedProfile { get; set; }

        [Parameter]
        public ProfileModel Value { get; set; }

        [Parameter]
        public EventCallback<ProfileModel> ValueChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ListProfiles = (await profilesService.GetPerfiles()).ToList();
        }

        
        private async Task updateOtherTables(Perfil newPerfil) 
        {
            selectedProfile = newPerfil;
            if (newPerfil != null)
            {
                //actualizar las tablas
            }
            Value.ProfileName = newPerfil.NombrePerfil;
            await ValueChanged.InvokeAsync(Value);
        }

    }
}
