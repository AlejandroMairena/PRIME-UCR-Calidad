using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Reflection;
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
        public bool HavePermission(int permission, List<Permiso> permissionsList)
        {
            return permissionsList.Exists(p => p.IDPermiso == permission);
        }
    }
}
