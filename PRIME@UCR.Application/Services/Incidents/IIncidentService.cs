﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.Dtos;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Application.Services.Incidents
{
    public interface IIncidentService
    {
        Task<Incidente> GetIncidentAsync(string code);
        Task<IEnumerable<Modalidad>> GetTransportModesAsync();
        Task<Incidente> CreateIncidentAsync(IncidentModel model, Persona person);
        Task<IncidentDetailsModel> GetIncidentDetailsAsync(string code);
        Task<IncidentDetailsModel> UpdateIncidentDetailsAsync(IncidentDetailsModel model);
        Task<IEnumerable<Incidente>> GetAllAsync();
    }
}