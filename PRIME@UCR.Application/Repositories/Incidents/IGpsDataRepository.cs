using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Repositories.Incidents
{
    public interface IGpsDataRepository
    {
        Task<IEnumerable<IncidentGpsData>> GetAllGpsDataAsync();
        Task<IEnumerable<IncidentGpsData>> GetGpsDataByUnitTypeAsync(Modalidad unitType);
    }
}
