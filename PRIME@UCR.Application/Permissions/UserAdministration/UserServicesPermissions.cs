using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{

    [AuthorizationType(typeof(UserServiceAuthorization))]
    public partial class UserService
    {
    }
    
    public abstract class UserServiceAuthorization
    {
        [RequirePermissions(new[]{ AuthorizationPermissions.CanAccessDashboard })] 
        public abstract Task<Persona> getPersonWithDetailstAsync(string email);
    }
}
