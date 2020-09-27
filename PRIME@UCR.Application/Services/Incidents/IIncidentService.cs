using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface IIncidentService
    {
        Task<Incidente> GetIncidentAsync(string id);

    }
}