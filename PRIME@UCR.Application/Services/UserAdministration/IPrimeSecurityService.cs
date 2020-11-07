using PRIME_UCR.Application.DTOs.UserAdministration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Constants;

namespace PRIME_UCR.Application.Services.UserAdministration
{
    public interface IPrimeSecurityService
    {
        Task CheckIfIsAuthorizedAsync(MethodBase method);
    }
}
