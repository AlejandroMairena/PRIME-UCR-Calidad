using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Application.Implementations.Incidents
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IModesRepository _modesRepository;
        private readonly IIncidentStateRepository _statesRepository;

        public IncidentService(
            IIncidentRepository incidentRepository,
            IModesRepository modesRepository,
            IIncidentStateRepository statesRepository)
        {
            _incidentRepository = incidentRepository;
            _modesRepository = modesRepository;
            _statesRepository = statesRepository;
        }

        public async Task<Incidente> GetIncidentAsync(string id)
        {
            return await _incidentRepository.GetByKeyAsync(id);
        }

        public async Task<IEnumerable<Modalidad>> GetTransportModesAsync()
        {
            return await _modesRepository.GetAllAsync();
        }

        public async Task<Incidente> CreateIncident(IncidentModel model)
        {
            var date = DateTime.Now;
            var entity = new Incidente
            {
                Codigo = Guid.NewGuid().ToString(),
                FechaHoraRegistro = DateTime.Now,
                FechaHoraEstimada = model.EstimatedDateOfTransfer,
                TipoModalidad = model.Mode.Tipo
            };
            
            var state = new EstadoIncidente
            {
                NombreEstado = IncidentStates.InCreationProcess.Nombre,
                CodigoIncidente = entity.Codigo,
                FechaModificado = DateTime.Now
            };

            await _incidentRepository.InsertAsync(entity);
            await _statesRepository.AddState(state);
            
            return entity;
        }
    }
}