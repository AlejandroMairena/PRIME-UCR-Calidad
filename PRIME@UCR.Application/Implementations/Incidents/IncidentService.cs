using System;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Implementations.Incidents
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _repository;

        public IncidentService(IIncidentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Incidente> GetIncidentAsync(string id)
        {
            return await _repository.GetByKeyAsync(id);
        }
    }
}