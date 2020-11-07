using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.UserAdministration
{
    [AuthorizationType(typeof(NumeroTelefonoServiceAuthorization))]
    public partial class NumeroTelefonoService
    {
    }

    public abstract class NumeroTelefonoServiceAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers, AuthorizationPermissions.CanModifyUsers })]
        public abstract Task AddNewPhoneNumberAsync(string idUser, string phoneNumber);
    }
}
