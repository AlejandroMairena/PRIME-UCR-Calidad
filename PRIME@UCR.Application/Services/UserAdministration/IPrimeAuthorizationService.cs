using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    /*
     * Interface used to manage all the policies of the autorization.
     */
    public interface IPrimeAuthorizationService
    {
        bool HavePermission(int permission, List<Permiso> permissionsList);
    }
}
