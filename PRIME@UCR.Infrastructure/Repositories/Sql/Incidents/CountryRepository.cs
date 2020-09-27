using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class CountryRepository : GenericRepository<Pais, string>, ICountryRepository
    {
        public CountryRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        // public new Task<IEnumerable<Pais>> GetAllAsync()
        // {
        //     IEnumerable<Pais> result = new List<Pais>
        //     {
        //         new Pais { Nombre = "Costa Rica" },
        //         new Pais { Nombre = "Panamá" }
        //     };
        //     return Task.FromResult(result);
        // }
    }
}
