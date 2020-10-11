using Microsoft.AspNetCore.Components;
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

        public List<Permiso> ListPermissions { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ListPermissions = (await permissionsService.GetPermisos()).ToList();
        }

    }
}
