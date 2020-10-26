using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Repositories.MedicalRecords;
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
        private readonly ITransportUnitRepository _transportUnitRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;


        public IncidentService(
            IIncidentRepository incidentRepository,
            IModesRepository modesRepository,
            IIncidentStateRepository statesRepository,
            ILocationRepository locationRepository,
            ITransportUnitRepository transportUnitRepository,
            IMedicalRecordRepository medicalRecordRepository)
        {
            _incidentRepository = incidentRepository;
            _modesRepository = modesRepository;
            _statesRepository = statesRepository;
            _locationRepository = locationRepository;
            _transportUnitRepository = transportUnitRepository;
            _medicalRecordRepository = medicalRecordRepository;
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
            if (model.EstimatedDateOfTransfer == null)
            {
                throw new ArgumentNullException("model.EstimatedDateOfTransfer");
            }
            
            var entity = new Incidente
            {
                TipoModalidad = model.Mode.Tipo,
                CedulaAdmin = person.Cédula,
                Cita = new Cita()
            };

            entity.Cita.FechaHoraEstimada = (DateTime)model.EstimatedDateOfTransfer;
            
            // insert before adding state to get auto generated code from DB
            await _incidentRepository.InsertAsync(entity);
            
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
            var transportUnit = await _transportUnitRepository.GetTransporUnitByIncidentIdAsync(incident.Codigo);
            if (incident != null)
            {
                var state = await _statesRepository.GetCurrentStateByIncidentId(incident.Codigo);
                var medicalRecord = 
                    incident.Cita.IdExpediente != null ?
                    await _medicalRecordRepository.GetByKeyAsync((int)incident.Cita.IdExpediente)
                    : null;
                var model = new IncidentDetailsModel
                {
                    Code = incident.Codigo,
                    Mode = incident.TipoModalidad,
                    CurrentState = state.Nombre,
                    Completed = incident.IsCompleted(),
                    Modifiable = incident.IsModifiable(state),
                    RegistrationDate = incident.Cita.FechaHoraCreacion,
                    EstimatedDateOfTransfer = incident.Cita.FechaHoraEstimada,
                    AdminId = incident.CedulaAdmin,
                    Origin = incident.Origen,
                    Destination = incident.Destino,
                    AppointmentId = incident.CodigoCita,
                    TransportUnitId = transportUnit?.Matricula,
                    TransportUnit = transportUnit,
                    MedicalRecord = medicalRecord
                };
                
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

            // update destination
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
            
            if (model.TransportUnit != null)
            {
                if (incident.MatriculaTrans == null || incident.MatriculaTrans != model.TransportUnit.Matricula)
                {
                    model.TransportUnit = await _transportUnitRepository.GetByKeyAsync(model.TransportUnit.Matricula);
                    incident.MatriculaTrans = model.TransportUnit.Matricula;                   
                    modified = true;
                }
            }

            if (modified)
                await _incidentRepository.UpdateAsync(incident);

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
            }
            
            return await GetIncidentDetailsAsync(incident.Codigo);
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