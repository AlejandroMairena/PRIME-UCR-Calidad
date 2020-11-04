using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    public interface IPrimeSecurityService
    {
        Task<bool> isAuthorizedAsync(AuthorizationPolicies authorizationPolicy);
    }
}
