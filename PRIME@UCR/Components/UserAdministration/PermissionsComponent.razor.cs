using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class PermissionsComponent
    {
        
        [Inject]
        public IPermissionsService permissionsService { get; set; }

        [Inject]
        public IUserService userService { get; set; }

        public List<Permiso> ListPermissions { get; set; }

        private Persona person;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ListPermissions = (await permissionsService.GetPermisos()).ToList();
            var emailUser = (await authenticationState).User.Identity.Name;
            person = await userService.getPersonWithDetailstAsync(emailUser);
        }

    }
}
