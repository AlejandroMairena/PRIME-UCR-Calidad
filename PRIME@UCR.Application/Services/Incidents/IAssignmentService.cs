using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface IAssignmentService
    {
        Task<IEnumerable<UnidadDeTransporte>> GetAllTransportUnitsByMode(string mode);
        Task<IEnumerable<CoordinadorTécnicoMédico>> GetCoordinatorsAsync();
        Task<IEnumerable<EspecialistaTécnicoMédico>> GetSpecialistsAsync();
    }
}