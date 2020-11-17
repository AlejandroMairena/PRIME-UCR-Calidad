using PRIME_UCR.Application.DTOs.UserAdministration;
using PRIME_UCR.Application.Exceptions.UserAdministration;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.Permissions.UserAdministration;
using RepoDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public partial class NumeroTelefonoRepository : INumeroTelefonoRepository
    {

        private readonly ISqlDataProvider _db;
        private readonly IPrimeSecurityService primeSecurityService;

        public NumeroTelefonoRepository(ISqlDataProvider dataProvider,
            IPrimeSecurityService _primeSecurityService)
        {
            _db = dataProvider;
            primeSecurityService = _primeSecurityService;
        }

        public async Task<int> AddPhoneNumberAsync(NúmeroTeléfono phoneNumber)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            
            await _db.PhoneNumbers.AddAsync(phoneNumber);
            var returnValue = await _db.SaveChangesAsync();
            return returnValue;
        }
    }

    [MetadataType(typeof(NumeroTelefonicoRepositoryAuthorization))]
    public partial class NumeroTelefonoRepository
    {
    }
}
