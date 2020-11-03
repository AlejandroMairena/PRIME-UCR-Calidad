using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
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
        protected readonly IAuthorizationService AuthorizationService;

        protected readonly AuthenticationStateProvider AuthenticationStateProvider;

        private readonly ISqlDataProvider _db; 

        public PermisoRepository(ISqlDataProvider dataProvider,
            IAuthorizationService _authorizationService,
            AuthenticationStateProvider _authenticationStateProvider)
        {
            AuthorizationService = _authorizationService;
            AuthenticationStateProvider = _authenticationStateProvider;
            _db = dataProvider;
        }

        public async Task<List<Permiso>> GetAllAsync()
        {
            var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            if((await AuthorizationService.AuthorizeAsync(user, AuthorizationPolicies.CanManageUsers.ToString())).Succeeded)
            {
                return await _db.Permissions.ToListAsync();
            }
            return null;
        }
    }
}
