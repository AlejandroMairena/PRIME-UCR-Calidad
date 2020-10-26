using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Implementations.Incidents
{
    public class AssignmentService : IAssignmentService
    {
        private readonly ITransportUnitRepository _transportUnitRepository;
        private readonly ICoordinadorTécnicoMédicoRepository _coordinatorRepo;
        private readonly IEspecialistaTécnicoMédicoRepository _specialistRepo;

        public AssignmentService(ITransportUnitRepository transportUnitRepository,
            ICoordinadorTécnicoMédicoRepository coordinatorRepo,
            IEspecialistaTécnicoMédicoRepository specialistRepo)
        {
            _transportUnitRepository = transportUnitRepository;
            _coordinatorRepo = coordinatorRepo;
            _specialistRepo = specialistRepo;
        }

        public async Task<IEnumerable<UnidadDeTransporte>> GetAllTransportUnitsByMode(string mode)
        {
            return await _transportUnitRepository.GetAllTransporUnitsByMode(mode);
        }
        
        public async Task<IEnumerable<CoordinadorTécnicoMédico>> GetCoordinatorsAsync()
        {
            return await _coordinatorRepo.GetAllAsync();
        }
        
        public async Task<IEnumerable<EspecialistaTécnicoMédico>> GetSpecialistsAsync()
        {
            return await _specialistRepo.GetAllAsync();
        }
    }
}