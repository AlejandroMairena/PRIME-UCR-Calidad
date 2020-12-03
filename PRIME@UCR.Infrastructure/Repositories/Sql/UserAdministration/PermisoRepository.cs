using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.Permissions.UserAdministration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    internal class PermisoRepository : IPermisoRepository
    {
        private readonly ISqlDataProvider _db; 

        public PermisoRepository(ISqlDataProvider dataProvider)
        {
            _db = dataProvider;
        }

        public async Task<List<Permiso>> GetAllAsync()
        {
            return await _db.Permissions.ToListAsync();
        }
    }
}
