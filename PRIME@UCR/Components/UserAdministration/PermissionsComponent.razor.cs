using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.UserAdministration;

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

        private List<Permiso> ListPermissionsPerProfile { get; set; }

        [Parameter]
        public ProfileModel Value { get; set; }

        [Parameter]
        public EventCallback<ProfileModel> ValueChanged { get; set; }

        protected override void OnInitialized()
        {
            ListPermissions = new List<Permiso>();
            ListPermissionsPerProfile = new List<Permiso>();
        }

        protected override async Task OnInitializedAsync()
        {
            ListPermissions = (await permissionsService.GetPermisos()).ToList();
        }

        protected override async Task OnParametersSetAsync()
        {
            var ListProfiles = await profileService.GetPerfilesWithDetailsAsync();
            var profile = (ListProfiles.Find(p => p.NombrePerfil == Value.ProfileName));
            ListPermissionsPerProfile.Clear();
            foreach(var permission in profile.PerfilesYPermisos)
            {
                ListPermissionsPerProfile.Add(permission.Permiso);
            }
            Value.PermissionsList = ListPermissionsPerProfile;
        }

        private bool check(int idPermission)
        {
            if(ListPermissionsPerProfile.Count != 0)
            {
                return (ListPermissionsPerProfile.Find(p => p.IDPermiso == idPermission) == null) ? false : true;
            }
            return false;
        }

        protected async Task update_profile(int idPermission)
        {
            if(Value.PermissionsList != null)
            {
                var hasPermission = (Value.PermissionsList.Find(p => p.IDPermiso == idPermission) != null);
                if (hasPermission)
                {
                    await permiteService.DeletePermissionAsync(Value.ProfileName,idPermission);
                }
                else 
                {
                    // agregar permiso a la relacion                
                }
            }
        }
    }
}
