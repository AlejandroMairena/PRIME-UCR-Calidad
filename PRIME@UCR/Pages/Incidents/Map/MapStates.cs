using System.Collections.Generic;
using Fluxor;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents.Map
{
    public abstract record MapState
    {
    }

    public record LoadingMapState : MapState
    {
    }

    public record LoadedMapState : MapState
    {
        public IEnumerable<IncidentGpsData> GpsData { get; init; }
        public IEnumerable<Modalidad> AvailableFilters { get; init; }
        public Modalidad Filter { get; init; }
        public GpsPoint Center { get; init; }
    }

    public record GpsPoint
    {
        public double Longitude { get; init; }
        public double Latitude { get; init; }
    }

    public class MapFeature : Feature<MapState>
    {
        public override string GetName() => nameof(MapState);
        protected override MapState GetInitialState() => new LoadingMapState();
    }
}
