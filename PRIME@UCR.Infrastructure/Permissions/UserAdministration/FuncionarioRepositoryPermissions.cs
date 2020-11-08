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
    [AuthorizationType(typeof(FuncionarioRepositoryAuthorization))]
    public partial class FuncionarioRepository
    {
    }

    public abstract class FuncionarioRepositoryAuthorization
    {
        [RequirePermissions(new[] { AuthorizationPermissions.CanAssignIncidents })]
        public abstract Task<List<Funcionario>> GetAllAsync();


    }
}

