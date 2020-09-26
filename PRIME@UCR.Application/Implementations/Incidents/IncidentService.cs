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
        private readonly ICountryRepository _countryRepository;

        public IncidentService(IIncidentRepository incidentRepository, IMedicalCenterRepository medicalCenterRepository, ICountryRepository countryRepository)
        {
            _incidentRepository = incidentRepository;
            _medicalCenterRepository = medicalCenterRepository;
            _countryRepository = countryRepository;
        }

        public async Task<Incidente> GetIncidentAsync(string id)
        {
            return await _incidentRepository.GetByKeyAsync(id);
        }

        public async Task<IEnumerable<CentroMedico>> GetAllMedicalCentersAsync()
        {
            return await _medicalCenterRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Pais>> GetAllCountriesAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        public Task<IEnumerable<Provincia>> GetAllProvincesAsync()
        {
            IEnumerable<Provincia> prov = new List<Provincia>
            {
                new Provincia{ Nombre = "San Jose" },
                new Provincia{ Nombre = "Alajuela" }
            };
            return Task.FromResult(prov);
        }
    }
}