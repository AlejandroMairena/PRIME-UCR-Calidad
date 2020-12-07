#nullable enable
using System.Collections.Generic;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents.Map
{
    public abstract record MapAction
    {
    }

    public record LoadingGpsDataAction : MapAction
    {
    }

    public record LoadGpsDataAction : MapAction
    {
    }

    public record LoadGpsDataWithFilterAction : MapAction
    {
        public Modalidad? UnitTypeFilter { get; init; }
    }

    public record LoadGpsDataSuccessfulAction : MapAction
    {
        public IEnumerable<IncidentGpsData> GpsData { get; init; }
        public IEnumerable<Modalidad> Filters { get; init; }
        public Modalidad? Filter { get; set; }
    }
}
