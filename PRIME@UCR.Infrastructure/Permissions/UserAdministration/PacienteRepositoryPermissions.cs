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
    public abstract class PacienteRepositoryAuthorization
    {
        [RequirePermissions(new AuthorizationPermissions[] {})]
        public abstract Task<Paciente> InsertPatientOnlyAsync(Paciente entity);

        [RequirePermissions(new AuthorizationPermissions[] {})]
        public abstract Task<Paciente> GetByKeyAsync(string key);

        [RequirePermissions(new AuthorizationPermissions[] {})]
        public abstract Task<IEnumerable<Paciente>> GetAllAsync();

        [RequirePermissions(new AuthorizationPermissions[] {})]
        public abstract Task<IEnumerable<Paciente>> GetByConditionAsync(Expression<Func<Paciente, bool>> expression);

        [RequirePermissions(new AuthorizationPermissions[] {})]
        public abstract Task<Paciente> InsertAsync(Paciente model);

        [RequirePermissions(new AuthorizationPermissions[] {})]
        public abstract Task DeleteAsync(string key);

        [RequirePermissions(new AuthorizationPermissions[] {})]
        public abstract Task UpdateAsync(Paciente model);

    }
}
