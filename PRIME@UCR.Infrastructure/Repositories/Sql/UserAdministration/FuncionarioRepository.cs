﻿using Microsoft.EntityFrameworkCore;
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public partial class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly IPrimeSecurityService primeSecurityService;

        private readonly ISqlDataProvider _db;
        public FuncionarioRepository(ISqlDataProvider dataProvider, IPrimeSecurityService _primeSecurityService) 
        {
            _db = dataProvider;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<List<Funcionario>> GetAllAsync()
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _db.Functionaries.ToListAsync();

        }
    }

    [MetadataType(typeof(FuncionarioRepositoryAuthorization))]
    public partial class FuncionarioRepository
    {
    }
}
