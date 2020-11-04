using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly IPrimeSecurityService primeSecurityService;

        private readonly ISqlDataProvider _db; 

        public PermisoRepository(ISqlDataProvider dataProvider,
            IPrimeSecurityService _primeSecurityService)
        {
            _db = dataProvider;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<List<Permiso>> GetAllAsync()
        {
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageUsers)))
            {
                return await _db.Permissions.ToListAsync();
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }
    }
}
