using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    [AuthorizationType(typeof(NumeroTelefonicoRepositoryAuthorization))]
    public partial class NumeroTelefonicoRepositoryPermissions
    {
    }

    public abstract class NumeroTelefonicoRepositoryAuthorization
    {
        public abstract Task AddPhoneNumberAsync(NúmeroTeléfono phoneNumber);
    }
}
