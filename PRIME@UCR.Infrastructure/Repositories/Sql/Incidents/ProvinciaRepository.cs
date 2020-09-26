using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class ProvinciaRepository : GenericRepository<Provincia, string>, IProvinciaRepository
    {
        public ProvinciaRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<Provincia> GetByKeyWithPaisAsync(string id)
        {
            return await _db.Set<Provincia>()
                .Include(p => p.Pais)
                .FirstOrDefaultAsync(p => p.Nombre == id);
        }
    }
}