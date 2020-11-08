using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    public abstract class NumeroTelefonicoRepositoryAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanCreateUsers })]
        public abstract Task AddPhoneNumberAsync(NúmeroTeléfono phoneNumber);
    }
}
