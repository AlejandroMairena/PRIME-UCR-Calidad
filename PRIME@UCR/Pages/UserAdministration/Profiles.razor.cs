using System;
using System.Collections.Generic;
using System.Linq;
using PRIME_UCR.Application.DTOs.UserAdministration;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.UserAdministration
{
    public partial class Profiles
    {
        public ProfileModel profile;

        protected override void OnInitialized()
        {
            profile = new ProfileModel();
            profile.ProfileName = "Administrador";
        }
    }
}
