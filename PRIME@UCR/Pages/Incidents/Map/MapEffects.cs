using System;
using System.Threading;
using System.Threading.Tasks;
using Fluxor;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Pages.Incidents.Map
{
    public class MapEffects : IDisposable
    {
        private const int PeriodInMs = (int)(5 * 60 * 1e3); // 5 minutes in milliseconds
        private readonly IGpsDataService _service;
        private IDispatcher _dispatcher;
        private Timer _timer;
        private Modalidad _filter;

        public MapEffects(IGpsDataService service)
        {
            _service = service;
        }

        [EffectMethod]
        public Task HandleLoadGpsDataWithFilterAction(LoadGpsDataWithFilterAction action, IDispatcher dispatcher)
        {
            _filter = action.UnitTypeFilter;
            _dispatcher ??= dispatcher;
            RefreshGpsData();

            return Task.CompletedTask;
        }

        [EffectMethod]
        public async Task HandleLoadGpsDataAction(LoadGpsDataAction action, IDispatcher dispatcher)
        {
            if (_timer == null)
            {
                _timer = new Timer(state => RefreshGpsData(), null, PeriodInMs, PeriodInMs);
            }

            dispatcher.Dispatch(new LoadingGpsDataAction());

            var gpsData =
                _filter is null
                ? await _service.GetAllGpsDataAsync()
                : await _service.GetGpsDataByUnitTypeAsync(_filter);

            var filters = await _service.GetGpsDataFiltersAsync();

            dispatcher.Dispatch(new LoadGpsDataSuccessfulAction
            {
                GpsData = gpsData,
                Filters = filters,
                Filter = _filter
            });
        }

        private void RefreshGpsData()
        {
            _dispatcher.Dispatch(new LoadGpsDataAction());
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
