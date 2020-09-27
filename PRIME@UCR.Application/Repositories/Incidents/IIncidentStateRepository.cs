﻿using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Application.Repositories.Incidents
{
    public interface IIncidentStateRepository
    {
        Task AddState(EstadoIncidente incidentState);
    }
}