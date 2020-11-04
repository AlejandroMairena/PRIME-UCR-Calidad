using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class FuncionarioRepository : IFuncionarioRepository
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
            if ((await primeSecurityService.isAuthorizedAsync(AuthorizationPolicies.CanManageMedicalRecords)))
            {
                return await _db.Functionaries.ToListAsync();

            }
            else
            {
                throw new NotAuthorizedException();
            }
        }
    }
}
