using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Implementations.Incidents
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IMedicalCenterRepository _medicalCenterRepository;


        public IncidentService(IIncidentRepository incidentRepository, IMedicalCenterRepository medicalCenterRepository)
        {
            _incidentRepository = incidentRepository;
            _medicalCenterRepository = medicalCenterRepository;
        }

        public async Task<Incidente> GetIncidentAsync(string id)
        {
            return await _incidentRepository.GetByKeyAsync(id);
        }

        public async Task<IEnumerable<CentroMedico>> GetAllMedicalCentersAsync()
        {
            return await _medicalCenterRepository.GetAllAsync();
        }
    }
}