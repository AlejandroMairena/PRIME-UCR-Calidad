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
        public bool HavePolicy(int policy, List<Permiso> permissionsList)
        {
            switch (policy)
            {
                case (int)AuthorizationPolicies.CanManageUsers:
                    return CanManageUsers(permissionsList);
                case (int)AuthorizationPolicies.CanCreateCheckList:
                    return CanCreateChecklist(permissionsList);
            }
            return false;
        }

        private bool CanManageUsers(List<Permiso> permissionsList)
        {
            return permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanDoAnything)
                    || permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanManageUsers);
        }

        private bool CanCreateChecklist(List<Permiso> permissionsList)
        {
            return permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanCreateCheckList)
                    || permissionsList.Exists(p => p.IDPermiso == (int)AuthorizationPermissions.CanDoAnything);
        }
    }
}
