using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
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
        private readonly IAssignemntRepository _assignmentRepo;
        private readonly IIncidentRepository _incidentRepository;

        public AssignmentService(ITransportUnitRepository transportUnitRepository,
            ICoordinadorTécnicoMédicoRepository coordinatorRepo,
            IEspecialistaTécnicoMédicoRepository specialistRepo,
            IAssignemntRepository assignmentRepo,
            IIncidentRepository incidentRepository)
        {
            _transportUnitRepository = transportUnitRepository;
            _coordinatorRepo = coordinatorRepo;
            _specialistRepo = specialistRepo;
            _assignmentRepo = assignmentRepo;
            _incidentRepository = incidentRepository;
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

        public async Task<AssignmentModel> GetAssignmentsByIncidentIdAsync(string code)
        {
            var incident = await _incidentRepository.GetByKeyAsync(code);
            if (incident == null)
            {
                throw new ArgumentException("Invalid incidnet code");
            }

            var coordinator = await _coordinatorRepo.GetByKeyAsync(incident.CedulaTecnicoCoordinador);
            var transportUnit = await _transportUnitRepository.GetByKeyAsync(incident.MatriculaTrans);
            var specialists = await _assignmentRepo.GetAssignmentsByIncidentIdAsync(code);
            
            return new AssignmentModel
            {
                TransportUnit = transportUnit,
                Coordinator = coordinator,
                TeamMembers = specialists.ToList()
            };
        }

        public async Task AssignToIncidentAsync(string code, AssignmentModel model)
        {
            var incident = await _incidentRepository.GetByKeyAsync(code);
            incident.CedulaTecnicoCoordinador = model.Coordinator?.Cédula;
            incident.MatriculaTrans = model.TransportUnit?.Matricula;

            await _incidentRepository.UpdateAsync(incident);
            await _assignmentRepo.ClearTeamMembers(code);
            if (model.TeamMembers.Count > 0)
                await _assignmentRepo.AssignToIncident(code, model.TeamMembers);
        }
    }
}