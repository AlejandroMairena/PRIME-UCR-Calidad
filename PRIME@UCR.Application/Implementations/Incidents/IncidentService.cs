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

        public Task<IEnumerable<Provincia>> GetProvincesByCountryAsync(Pais country)
        {

            IEnumerable<Provincia> prov;
            if (country.Nombre == "Costa Rica")
            {
                prov = new List<Provincia>
                {
                    new Provincia{ Nombre = "San Jose" },
                    new Provincia{ Nombre = "Alajuela" }
                };
            }
            else
            {
                prov = new List<Provincia>
                {
                    new Provincia{ Nombre = "Texas" },
                    new Provincia{ Nombre = "California" }
                };
            }
            return Task.FromResult(prov);
        }

        public Task<IEnumerable<Canton>> GetCantonsByProvinceAsync(Provincia province)
        {
            IEnumerable<Canton> cantons = new List<Canton>
            {
                new Canton { Nombre = "Montes de Oca" },
                new Canton { Nombre = "Escazú" }
            };
            
            return Task.FromResult(cantons);
        }

        public Task<IEnumerable<Distrito>> GetDistrictsByCantonAsync(Canton canton)
        {
            IEnumerable<Distrito> districts = new List<Distrito>
            {
                new Distrito { Nombre = "Pavas" },
                new Distrito { Nombre = "La Trinidad" }
            };
            
            return Task.FromResult(districts);
        }
    }
}