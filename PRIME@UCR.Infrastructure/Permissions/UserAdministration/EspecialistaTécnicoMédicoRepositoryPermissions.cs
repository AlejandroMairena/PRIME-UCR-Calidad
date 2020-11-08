using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Permissions.UserAdministration
{
    [AuthorizationType(typeof(EspecialistaTécnicoMédicoRepositoryAuthorization))]
    public partial class EspecialistaTécnicoMédicoRepository
    {
    }

    public abstract class EspecialistaTécnicoMédicoRepositoryAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<IEnumerable<EspecialistaTécnicoMédico>> GetAllAsync();


    }
}

