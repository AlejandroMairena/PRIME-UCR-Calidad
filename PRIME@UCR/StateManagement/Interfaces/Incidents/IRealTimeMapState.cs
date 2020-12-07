using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.StateManagement.Interfaces.Incidents
{
    public interface IRealTimeMapState
    {
        Modalidad UnitTypeFilter { get; }
        List<IncidentGpsData> GpsData { get; }
        void SetUnitTypeFilter(Modalidad unitType);
        event EventHandler<List<IncidentGpsData>> ReceivedNewGpsData;
        Task StartGpsMonitoring();
    }
}
