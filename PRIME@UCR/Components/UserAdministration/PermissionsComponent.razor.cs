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
using Microsoft.AspNetCore.Http;

namespace PRIME_UCR.Components.UserAdministration
{
    /**
     * Partial class used to manage the logic part of the PermissionsComponent.
     */
    public partial class PermissionsComponent
    {

        [Inject]
        public IPermissionsService permissionsService { get; set; }

        [Inject]
        public IPermiteService permiteService { get; set; }

        [Inject]
        public AuthenticationStateProvider authenticationStateProvider { get; set; }

        public List<Permiso> ListPermissions { get; set; }

        [Parameter]
        public ProfileModel Value { get; set; }

        [Parameter]
        public EventCallback<ProfileModel> ValueChanged { get; set; }

        /**
         * Function: Assigns, a new list of permissions to the attribute ListPermissions once IsInitialized  is set to true.
         */
        protected override void OnInitialized()
        {
            ListPermissions = new List<Permiso>();
        }

        /**
         * Function: Assigns, a new list of permissions to the attribute ListPermissions based on the permissions that are
         * placed on the database.
         */
        protected override async Task OnInitializedAsync()
        {
            ListPermissions = (await permissionsService.GetPermisos()).ToList();
        }

        /**
         * Function: Used to update the permissions asigned to each profile so, the permissions chosen by the user on the
         * front end, are changed at the database level. 
         * 
         * Requires: The permission id, which corresponds to a attribute that uniquely identifies a permission, and an argument
         * that indicates that there's an event happening.
         */
        protected async Task update_profile(int idPermission, ChangeEventArgs e)
        {
            if(Value.PermissionsList != null)
            {
                var Permission = (ListPermissions.Find(p => p.IDPermiso == idPermission));
                if ((bool)e.Value)
                {
                    Value.PermissionsList.Add(Permission);
                    await permiteService.InsertPermissionAsync(Value.ProfileName, idPermission);
                    Value.StatusMessage = "El permiso \"" + Permission.DescripciónPermiso + "\" fue agregado al perfil " + Value.ProfileName + ". Para que los usuarios afectados puedan notar los cambios, deberán reiniciar su sesión.";
                    Value.StatusMessageType = "success";
                }
                else 
                {
                    Value.PermissionsList.Remove(Permission);
                    await permiteService.DeletePermissionAsync(Value.ProfileName,idPermission);
                    Value.StatusMessage = "El permiso \"" + Permission.DescripciónPermiso + "\" fue removido del perfil " + Value.ProfileName + ". Para que los usuarios afectados puedan notar los cambios, deberán reiniciar su sesión.";
                    Value.StatusMessageType = "warning";
                }
                await authenticationStateProvider.GetAuthenticationStateAsync();
                Value.CheckedPermissions[idPermission-1] = (bool)e.Value;
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}