using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class ProfilesComponent
    {
        [Inject]
        public IProfilesService profilesService { get; set; }

        [Inject]
        public IPermissionsService permissionsService { get; set; }

        [Inject]
        public IUserService userService { get; set; }

        public List<Perfil> ListProfiles { get; set; }

        [Parameter]
        public ProfileModel Value { get; set; }

        [Parameter]
        public EventCallback<ProfileModel> ValueChanged { get; set; }

        public Perfil selectedProfile { get; set; }

        public int counterPermissions;
       
        protected override async Task OnInitializedAsync()
        {
            ListProfiles = (await profilesService.GetPerfiles()).ToList();
            counterPermissions = (await permissionsService.GetPermisos()).ToList().Count;
        }
        
        private async Task updateOtherTables(Perfil newPerfil) 
        {
            selectedProfile = newPerfil;
            Value.ProfileName = newPerfil.NombrePerfil;
            var ListProfiles = await profilesService.GetPerfilesWithDetailsAsync();
            var profile = (ListProfiles.Find(p => p.NombrePerfil == Value.ProfileName));
            Value.PermissionsList.Clear();
            foreach (var permission in profile.PerfilesYPermisos)
            {
                Value.PermissionsList.Add(permission.Permiso);
            }
            Value.UserLists.Clear();
            foreach(var user in profile.UsuariosYPerfiles)
            {
                Value.UserLists.Add(user.Usuario);
            }

            Value.CheckedPermissions = new List<bool>();
            var permisssionsList = (await permissionsService.GetPermisos()).ToList();
            foreach (var permission in permisssionsList)
            {
                var permissionChecked = Value.PermissionsList.Find(p => permission.IDPermiso == p.IDPermiso) == null ? false : true;
                Value.CheckedPermissions.Add(permissionChecked);
            }

            Value.CheckedUsers = new List<Tuple<string, bool>>();
            var usersList = (await userService.GetUsuarios()).ToList();
            foreach(var user in usersList)
            {
                var userChecked = Value.UserLists.Find(p => p.Id == user.Id) == null ? false : true;
                Value.CheckedUsers.Add(new Tuple<string, bool>(user.Id, userChecked));
            }
            await ValueChanged.InvokeAsync(Value);
        }

    }
}
