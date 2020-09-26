using System.Threading.Tasks;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Repositories.Incidents
{
    public interface IProvinciaRepository : IGenericRepository<Provincia, string>
    {
        Task<Provincia> GetByKeyWithPaisAsync(string id);
    }
}