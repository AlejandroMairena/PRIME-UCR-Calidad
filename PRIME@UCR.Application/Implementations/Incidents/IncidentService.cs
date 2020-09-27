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

        public IncidentService(IIncidentRepository incidentRepository, IMedicalCenterRepository medicalCenterRepository, ICountryRepository countryRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<Incidente> GetIncidentAsync(string id)
        {
            return await _incidentRepository.GetByKeyAsync(id);
        }

    }
}