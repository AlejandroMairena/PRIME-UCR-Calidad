using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Infrastructure.Repositories.External
{
    public class GpsDataRepository : IGpsDataRepository
    {
        private readonly IIncidentRepository _incidentRepository;

        public GpsDataRepository(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        private const double kMinLatitude = 9.176097;
        private const double kMaxLatitude = 9.927069;
        private const double kMinLongitude = -84.052347;
        private const double kMaxLongitude = -82.974313;

        public async Task<IEnumerable<IncidentGpsData>> GetAllGpsDataAsync()
        {
            var incidents = await _incidentRepository.GetOngoingIncidentsAsync();
            return incidents.Select(i => new IncidentGpsData
            {
                OngoingIncident =  i,
                IncidentCode = i.Incident.Codigo,
                CurrentLatitude = RandomLatitude(),
                CurrentLongitude = RandomLongitude(),
            });
        }

        public async Task<IEnumerable<IncidentGpsData>> GetGpsDataByUnitTypeAsync(Modalidad unitType)
        {
            if (unitType == null) throw new ArgumentNullException(nameof(unitType));

            var incidents = (await _incidentRepository.GetOngoingIncidentsAsync())
                .Where(i => i.UnitType.Tipo == unitType.Tipo);
            return incidents.Select(i => new IncidentGpsData
            {
                OngoingIncident =  i,
                IncidentCode = i.Incident.Codigo,
                CurrentLatitude = RandomLatitude(),
                CurrentLongitude = RandomLongitude(),
            });
        }

        public Task<IEnumerable<Modalidad>> GetGpsDataFiltersAsync()
        {
            return Task.FromResult(new List<Modalidad>
            {
                new Modalidad { Tipo = "Aéreo" },
                new Modalidad { Tipo = "Marítimo" },
                new Modalidad { Tipo = "Terrestre" },
            }.AsEnumerable());
        }

        private double RandomLongitude()
        {
            var rand = new Random();
            return rand.NextDouble() * (kMaxLongitude - kMinLongitude) + kMinLongitude;
        }


        private double RandomLatitude()
        {
            var rand = new Random();
            return rand.NextDouble() * (kMaxLatitude - kMinLatitude) + kMinLatitude;
        }
    }
}
