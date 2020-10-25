using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.UserAdministration;
using Microsoft.AspNetCore.Components.Web;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class PermissionsComponent
    {

        [Inject]
        public IPermissionsService permissionsService { get; set; }

        [Inject]
        public IProfilesService profileService { get; set; }

        [Inject]
        public IPermiteService permiteService { get; set; }

        public List<Permiso> ListPermissions { get; set; }


        [Parameter]
        public ProfileModel Value { get; set; }

        [Parameter]
        public EventCallback<ProfileModel> ValueChanged { get; set; }

        protected override void OnInitialized()
        {
            ListPermissions = new List<Permiso>();
        }

        protected override async Task OnInitializedAsync()
        {
            ListPermissions = (await permissionsService.GetPermisos()).ToList();
        }

        protected async Task update_profile(int idPermission, ChangeEventArgs e)
        {
            if(Value.PermissionsList != null)
            {
                var Permission = (ListPermissions.Find(p => p.IDPermiso == idPermission));
                if ((bool)e.Value)
                {
                    Value.PermissionsList.Add(Permission);
                    await permiteService.InsertPermissionAsync(Value.ProfileName, idPermission);
                    Value.StatusMessage = "El permiso \"" + Permission.DescripciónPermiso + "\" fue agregado al perfil " + Value.ProfileName;
                }
                else 
                {
                    Value.PermissionsList.Remove(Permission);
                    await permiteService.DeletePermissionAsync(Value.ProfileName,idPermission);
                    Value.StatusMessage = "El permiso \"" + Permission.DescripciónPermiso + "\" fue removido del perfil " + Value.ProfileName;
                }
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}