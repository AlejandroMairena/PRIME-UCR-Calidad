using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class CantonRepository : GenericRepository<Canton, int>, ICantonRepository
    {
        public CantonRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<IEnumerable<Canton>> GetCantonsByProvinceNameAsync(string provinceName)
        {
            return await _db.Cantons
                .AsNoTracking()
                .Where(c => c.NombreProvincia == provinceName)
                .ToListAsync();
        }
    }
}