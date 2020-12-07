using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.StateManagement.Interfaces.Incidents;

namespace PRIME_UCR.StateManagement.Implementations.Incidents
{
    public class RealTimeMapState : IRealTimeMapState, IDisposable
    {
        private const int PeriodInMs = (int)(5 * 60 * 1e3); // 5 minutes in milliseconds
        public Modalidad UnitTypeFilter => _unitTypeFilter;
        public List<IncidentGpsData> GpsData => _gpsData;

        public event EventHandler<List<IncidentGpsData>> ReceivedNewGpsData;

        private Timer _timer;
        private Modalidad _unitTypeFilter;
        private List<IncidentGpsData> _gpsData = new();
        private readonly IGpsDataService _gpsDataService;

        public RealTimeMapState(IGpsDataService gpsDataService)
        {
            _gpsDataService = gpsDataService;
        }

        public async Task StartGpsMonitoring()
        {
            // refresh gps data periodically
            await GetGpsData();
            _timer = new Timer(async _ => await RefreshGpsData(), null, 0, PeriodInMs);
        }

        public void SetUnitTypeFilter(Modalidad unitType)
        {
            _unitTypeFilter = unitType;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async Task RefreshGpsData()
        {
            await GetGpsData();
            ReceivedNewGpsData?.Invoke(this, _gpsData);
        }

        private async Task GetGpsData()
        {
            // _gpsData = (await _gpsDataService.GetGpsDataByUnitTypeAsync(_unitTypeFilter)).ToList();
            _gpsData = (await _gpsDataService.GetAllGpsDataAsync()).ToList();
        }

    }
}
