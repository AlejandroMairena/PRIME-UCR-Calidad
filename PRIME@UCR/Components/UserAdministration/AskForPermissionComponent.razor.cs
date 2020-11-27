using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.UserAdministration
{
    public partial class AskForPermissionComponent
    {
        [Inject]
        public IPermissionsService permissionsService { get; set; }

        [Inject]
        public IProfilesService profilesService { get; set; }

        [Inject]
        public IUserService userService { get; set; }

        [Inject]
        public UserManager<Usuario> UserManager { get; set; }


        [CascadingParameter]
        private Task<AuthenticationState> _authenticationState { get; set; }

        [Parameter]
        public AskForPermissionModel AskForPermissionModel { get; set; }

        [Parameter]
        public EventCallback<AskForPermissionModel> AskForPermissionModelChanged { get; set; }

        [Parameter]
        public bool IsBusy { get; set; }

        [Parameter]
        public EventCallback<bool> IsBusyChanged { get; set; }

        public List<Permiso> ListPermissions { get; set; }

        public List<Permiso> NotAssignedPermissions { get; set; }

        public List<Permiso> AssignedPermissions { get; set; }

        public List<Perfil> ListProfiles { get; set; }

        /**
         * Function: Assigns, a new list of permissions to the attribute ListPermissions once IsInitialized  is set to true.
         */
        protected override void OnInitialized()
        {
            ListPermissions = new List<Permiso>();
            AssignedPermissions = new List<Permiso>();
            NotAssignedPermissions = new List<Permiso>();
            ListProfiles = new List<Perfil>();

        }

        /**
         * Function: Assigns, a new list of permissions to the attribute ListPermissions based on the permissions that are
         * placed on the database.
         */
        protected override async Task OnInitializedAsync()
        {
            var userEmail = (await _authenticationState).User.Identity.Name;
            AskForPermissionModel.User = await UserManager.FindByEmailAsync(userEmail);
            ListPermissions = (await permissionsService.GetPermisos()).ToList();
            ListProfiles = (await profilesService.GetPerfilesWithDetailsAsync()).ToList();

            foreach (var profile in AskForPermissionModel.User.UsuariosYPerfiles)
            {
                var currProfile = ListProfiles.Find(p => profile.Perfil.NombrePerfil == p.NombrePerfil);
                foreach (var permission in currProfile.PerfilesYPermisos)
                {
                    if(AssignedPermissions.Find(p => permission.IDPermiso == p.IDPermiso) != null ? false : true)
                    {
                        AssignedPermissions.Add(permission.Permiso);
                    }
                }
            }

            foreach (var permission in ListPermissions)
            {
                var permissionAssigned = AssignedPermissions.Find(p => permission.IDPermiso == p.IDPermiso) == null ? false : true;
                if(permissionAssigned == false)
                {
                    NotAssignedPermissions.Add(permission);
                }
            }
        }

        //Update the list of permissions that are requested
        protected void update_list(int idPermission, ChangeEventArgs e)
        {
            var permission = NotAssignedPermissions.Find(p => idPermission == p.IDPermiso);
            if(permission != null)
            {
                if ((bool)e.Value)
                {
                    AskForPermissionModel.PermissionsList.Add(permission.DescripciónPermiso);
                }
                else
                {
                    AskForPermissionModel.PermissionsList.Remove(permission.DescripciónPermiso);
                }
            }
        }


    }
}
