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
        private readonly ILocationRepository _locationRepository;

        public IncidentService(
            IIncidentRepository incidentRepository,
            IModesRepository modesRepository,
            IIncidentStateRepository statesRepository,
            ILocationRepository locationRepository)
        {
            _incidentRepository = incidentRepository;
            _modesRepository = modesRepository;
            _statesRepository = statesRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Incidente> GetIncidentAsync(int id)
        {
            return await _incidentRepository.GetByKeyAsync(id);
        }

        public async Task<IEnumerable<Modalidad>> GetTransportModesAsync()
        {
            return await _modesRepository.GetAllAsync();
        }

        public async Task<Incidente> CreateIncident(IncidentModel model)
        {
            var entity = new Incidente
            {
                FechaHoraRegistro = DateTime.Now,
                FechaHoraEstimada = model.EstimatedDateOfTransfer,
                TipoModalidad = model.Mode.Tipo
            };
            
            // insert before adding state to get auto generated code from DB
            await _incidentRepository.InsertAsync(entity);
            
            var state = new EstadoIncidente
            {
                NombreEstado = IncidentStates.InCreationProcess.Nombre,
                IncidenteId = entity.Id,
                FechaModificado = DateTime.Now,
                Activo = true
            };

            await _statesRepository.AddState(state);
            
            return entity;
        }
        public async Task<IncidentDetailsModel> GetIncidentDetailsAsync(string code)
        {
            var incident = await _incidentRepository.GetWithDetailsAsync(code);
            if (incident != null)
            {
                var state = await _statesRepository.GetCurrentStateByIncidentId(incident.Id);
                var model = new IncidentDetailsModel(
                    incident.Id,
                    incident.Codigo,
                    incident.TipoModalidad,
                    state.Nombre,
                    incident.IsCompleted(),
                    incident.IsModifiable(state),
                    incident.FechaHoraRegistro,
                    incident.FechaHoraEstimada
                );
                model.Origin = incident.Origen;
                model.Destination = incident.Destino;
                
                return model;
            }

            return null;
        }

        public async Task<IncidentDetailsModel> UpdateIncidentDetails(IncidentDetailsModel model)
        {
            var incident = await _incidentRepository.GetByKeyAsync(model.Id);
            bool modified = false;
            // update origin
            if (model.Origin != null)
            {
                if (incident.IdOrigen == null || incident.IdOrigen != model.Origin.Id)
                {
                    var origin = await _locationRepository.InsertAsync(model.Origin);
                    incident.IdOrigen = origin.Id;
                    incident.Origen = origin;
                    modified = true;
                }
            }

            if (model.Destination != null)
            {
                if (incident.IdDestino == null || incident.IdDestino != model.Destination.Id)
                {
                    var destination = await _locationRepository.InsertAsync(model.Destination);
                    incident.IdDestino = destination.Id;
                    incident.Destino = destination;
                    modified = true;
                }
            }

            if (modified)
                await _incidentRepository.UpdateAsync(incident);

            var state = model.CurrentState;
            
            if (!model.Completed && incident.IsCompleted()) // if it was just completed but wasn't previously
            {
                var incidentState = new EstadoIncidente
                {
                    IncidenteId = incident.Id,
                    NombreEstado = IncidentStates.Created.Nombre,
                    Activo = true,
                    FechaModificado = DateTime.Now
                };
                await _statesRepository.AddState(incidentState);
                state = incidentState.NombreEstado;
            }

            var outputModel = new IncidentDetailsModel(
                incident.Id,
                incident.Codigo,
                incident.TipoModalidad,
                state,
                incident.IsCompleted(),
                model.Modifiable,
                incident.FechaHoraRegistro,
                incident.FechaHoraEstimada
            );
            outputModel.Origin = incident.Origen;
            outputModel.Destination = incident.Destino;
            
            return outputModel;
        }
    }
}