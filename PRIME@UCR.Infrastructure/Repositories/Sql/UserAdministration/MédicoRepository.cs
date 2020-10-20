using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class MédicoRepository : GenericRepository<Médico, string>, IMédicoRepository
    {
        public MédicoRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }
    }
}
