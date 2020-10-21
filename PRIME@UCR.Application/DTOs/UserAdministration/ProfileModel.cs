using System;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    public class ProfileModel
    {
        public string ProfileName { get; set; }

        public List<bool> CheckedUsers { get; set; }

        public List<bool> CheckedPermissions { get; set; }

        public List<Persona> UserLists { get; set; }

        public List<Permiso> PermissionsList { get; set; }
    }
}
