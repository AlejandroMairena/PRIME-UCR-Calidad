using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface IGpsDataService
    {
        // Gets all ongoing incidents with gps data
        Task<IEnumerable<IncidentGpsData>> GetAllGpsDataAsync();
        // Gets all ongoing incidents with gps data filtered by unit type
        Task<IEnumerable<IncidentGpsData>> GetGpsDataByUnitTypeAsync(Modalidad unitType);
        Task<IEnumerable<Modalidad>> GetGpsDataFiltersAsync();
    }
}
