using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Implementations
{
    /**
     * Class used to manage the authorization of the users. 
     */
    public class PrimeAuthorizationService : IPrimeAuthorizationService
    {
        /**
         * Method used to handle the authorization of an user to the aplication.
         */
        public string HavePolicy(int policy, List<Pertenece> UsersProfiles, List<Perfil> ProfilesAndPermissions)
        {

            var permissionsList = new List<Permiso>();
            foreach(var profile in UsersProfiles)
            {
                var permissionsOfProfile = ProfilesAndPermissions.Find(p => p.NombrePerfil == profile.IDPerfil).PerfilesYPermisos;
                foreach(var permission in permissionsOfProfile)
                {
                    permissionsList.Add(permission.Permiso);
                }
            }
            switch (policy)
            {
                case (int)AuthorizationPolicies.CanManageUsers:
                    return CanManageUsers(permissionsList);
            }
            return "false";
        }

        private string CanManageUsers(List<Permiso> permissionsList)
        {
            return permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanDoAnything)
                    || permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanManageUsers) ? "true" : "false";
        }
    }
}
