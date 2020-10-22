﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;

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

        public async Task<Incidente> GetIncidentAsync(string code)
        {
            return await _incidentRepository.GetByKeyAsync(code);
        }

        public async Task<IEnumerable<Modalidad>> GetTransportModesAsync()
        {
            return await _modesRepository.GetAllAsync();
        }

        public async Task<Incidente> CreateIncidentAsync(IncidentModel model, Persona person)
        {
            var entity = new Incidente
            {
                TipoModalidad = model.Mode.Tipo,
                CedulaAdmin = person.Cédula
            };
            
            // insert before adding state to get auto generated code from DB
            await _incidentRepository.InsertAsync(entity, model.EstimatedDateOfTransfer);
            
            var state = new EstadoIncidente
            {
                NombreEstado = IncidentStates.InCreationProcess.Nombre,
                CodigoIncidente = entity.Codigo,
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
                var state = await _statesRepository.GetCurrentStateByIncidentId(incident.Codigo);
                var model = new IncidentDetailsModel(
                    incident.Codigo,
                    incident.TipoModalidad,
                    state.Nombre,
                    incident.IsCompleted(),
                    incident.IsModifiable(state),
                    incident.Cita.FechaHoraCreacion,
                    incident.Cita.FechaHoraEstimada,
                    incident.CedulaAdmin
                );
                model.Origin = incident.Origen;
                model.Destination = incident.Destino;
                
                return model;
            }

            return null;
        }

        public async Task<IncidentDetailsModel> UpdateIncidentDetailsAsync(IncidentDetailsModel model)
        {
            var incident = await _incidentRepository.GetByKeyAsync(model.Code);
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
                    CodigoIncidente = incident.Codigo,
                    NombreEstado = IncidentStates.Created.Nombre,
                    Activo = true,
                    FechaModificado = DateTime.Now
                };
                await _statesRepository.AddState(incidentState);
                state = incidentState.NombreEstado;
            }

            var outputModel = new IncidentDetailsModel(
                incident.Codigo,
                incident.TipoModalidad,
                state,
                incident.IsCompleted(),
                model.Modifiable,
                incident.Cita.FechaHoraCreacion,
                incident.Cita.FechaHoraEstimada,
                incident.CedulaAdmin
            );
            outputModel.Origin = incident.Origen;
            outputModel.Destination = incident.Destino;
            
            return outputModel;
        }

        public async Task<IEnumerable<Incidente>> GetAllAsync()
        {
            return await _incidentRepository.GetAllAsync();
        }

        public async Task<IEnumerable<IncidentListModel>> GetIncidentListModelsAsync()
        {
            return await _incidentRepository.GetIncidentListModelsAsync();
        }
    }
}