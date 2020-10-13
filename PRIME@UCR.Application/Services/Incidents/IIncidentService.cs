using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface IIncidentService
    {
        Task<Incidente> GetIncidentAsync(string code);
        Task<IEnumerable<Modalidad>> GetTransportModesAsync();
        Task<Incidente> CreateIncident(IncidentModel model);
        Task<IncidentDetailsModel> GetIncidentDetailsAsync(string code);
        Task<IncidentDetailsModel> UpdateIncidentDetails(IncidentDetailsModel model);
    }
}