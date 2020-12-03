using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Implementations.Incidents
{
    public class GpsDataService : IGpsDataService
    {
        private readonly IGpsDataRepository _repository;

        public GpsDataService(IGpsDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<IncidentGpsData>> GetAllGpsDataAsync()
        {
            return await _repository.GetAllGpsDataAsync();
        }

        public async Task<IEnumerable<IncidentGpsData>> GetGpsDataByUnitTypeAsync(Modalidad unitType)
        {
            if (unitType == null) throw new ArgumentNullException(nameof(unitType));
            return await _repository.GetGpsDataByUnitTypeAsync(unitType);
        }
    }
}
