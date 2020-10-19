﻿using System.Threading.Tasks;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Application.Repositories.Incidents
{
    public interface IIncidentRepository : IGenericRepository<Incidente, string>
    {
        Task<Incidente> GetWithDetailsAsync(string code);
    }
}