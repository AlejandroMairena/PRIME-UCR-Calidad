using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Repositories.Incidents
{
    public interface IIncidentRepository : IGenericRepository<Incidente, string>
    {
        Task<Incidente> GetWithDetailsAsync(string code);

        Task<IEnumerable<IncidentListModel>> GetIncidentListModelsAsync();
    }
}