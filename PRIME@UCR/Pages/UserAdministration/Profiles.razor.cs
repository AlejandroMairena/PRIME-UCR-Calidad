using System;
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

        protected override void OnInitialized()
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
            profile.CheckedUsers = new List<bool>();
            profile.PermissionsList = new List<Permiso>();
            profile.UserLists = new List<Persona>();
        }

    }
}
