using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface IIncidentService
    {
        Task<Incidente> GetIncidentAsync(string code);
        Task<Estado> GetIncidentStateByIdAsync(string code);
        Task<IEnumerable<Modalidad>> GetTransportModesAsync();
        Task<Incidente> CreateIncidentAsync(IncidentModel model, Persona person);
        Task<IncidentDetailsModel> GetIncidentDetailsAsync(string code);
        Task<IncidentDetailsModel> UpdateIncidentDetailsAsync(IncidentDetailsModel model);
        Task<IEnumerable<Incidente>> GetAllAsync();
        Task<IEnumerable<IncidentListModel>> GetIncidentListModelsAsync();
        public Task ApproveIncidentAsync(string code, string reviewerId);
        public Task RejectIncidentAsync(string code, string reviewerId);
        public Task<string> GetNextIncidentState(string code);
        public Task<List<Tuple<string, string>>> GetPendingTasksAsync(IncidentDetailsModel model, string nexState);
        public List<Tuple<string, string>> GetCreatedStatePendingTasks(IncidentDetailsModel model);
        public Task<List<Tuple<string, string>>> GetAssignedStatePendingTasks(IncidentDetailsModel model);
        public List<Tuple<string, string>> GetApprovedStatePendingTasks(IncidentDetailsModel model);
        public Task ChangeState(string code, string nextState);
    }
}