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
        public string HavePermission(int permission, List<Pertenece> UsersProfiles, List<Perfil> ProfilesAndPermissions)
        {
            foreach (var profile in UsersProfiles)
            {
                if ( (ProfilesAndPermissions.Find(p => p.NombrePerfil == profile.IDPerfil)).
                        PerfilesYPermisos.Find(p => p.IDPermiso == permission) != null )
                {
                    return "true";
                }
            }
            return "false";
        }
    }
}
