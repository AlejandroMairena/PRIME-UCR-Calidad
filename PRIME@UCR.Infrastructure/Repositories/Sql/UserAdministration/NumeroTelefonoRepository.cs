using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class NumeroTelefonoRepository : INumeroTelefonoRepository
    {

        private readonly ISqlDataProvider _db;

        public NumeroTelefonoRepository(ISqlDataProvider db)
        {
            _db = db;
        }

        public async Task AddPhoneNumberAsync(NúmeroTeléfono phoneNumber)
        {
            await _db.PhoneNumbers.AddAsync(phoneNumber);
            await _db.SaveChangesAsync();
        }
    }
}
