using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class EspecialistaTécnicoMédicoRepository : GenericRepository<EspecialistaTécnicoMédico, string>, IEspecialistaTécnicoMédicoRepository
    {
        public EspecialistaTécnicoMédicoRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }
    }
}
