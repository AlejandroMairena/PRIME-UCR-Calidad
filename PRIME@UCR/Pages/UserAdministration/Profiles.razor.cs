﻿using System;
using System.Collections.Generic;
using System.Linq;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.UserAdministration;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Pages.UserAdministration
{
    public partial class Profiles
    {
        [Inject]
        public IProfilesService profilesService { get; set; }

        public ProfileModel profile;

        protected override async Task OnInitializedAsync()
        {
            profile = new ProfileModel();
            profile.ProfileName = "Administrador";            
            profile.CheckedPermissions = new List<bool>();
            for (int i = 0; i < 22; ++i) 
            {
                if (i < 2)
                {
                    profile.CheckedPermissions.Add(true);
                }
                else 
                {
                    profile.CheckedPermissions.Add(false);
                }
            }
            profile.CheckedUsers = new List<Tuple<string,bool>>();
            var profilesList =  await profilesService.GetPerfilesWithDetailsAsync();
            var user = profilesList[0].UsuariosYPerfiles;
            profile.CheckedUsers.Add(new Tuple<string, bool>(user[0].IDUsuario, true));
            profile.PermissionsList = new List<Permiso>();
            profile.UserLists = new List<Usuario>();
        }

    }
}
