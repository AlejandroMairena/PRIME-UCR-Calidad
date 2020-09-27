using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class ProvinceRepository : GenericRepository<Provincia, string>, IProvinceRepository
    {
        public ProvinceRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<IEnumerable<Provincia>> GetProvincesByCountryNameAsync(string countryName)
        {
            return await _db.Provinces
                .AsNoTracking()
                .Where(p => p.NombrePais == countryName)
                .ToListAsync();
        }
    }
}