using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Repositories.Incidents
{
    public interface IIncidentRepository : IGenericRepository<Incidente, string>
    {
        Task<Incidente> GetWithDetailsAsync(string code);
        Task<Incidente> GetIncidentByDateCodeAsync(int id);
        Task<IEnumerable<IncidentListModel>> GetIncidentListModelsAsync();
        Task<Médico> GetAssignedOriginDoctor(string code);
        Task<Médico> GetAssignedDestinationDoctor(string code);
        Task<IEnumerable<OngoingIncident>> GetOngoingIncidentsAsync();
    }
}
