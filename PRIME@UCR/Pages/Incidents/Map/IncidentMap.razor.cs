using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.StateManagement.Interfaces.Incidents;
using Radzen;
using Radzen.Blazor;

namespace PRIME_UCR.Pages.Incidents.Map
{
    public partial class IncidentMap : IDisposable
    {
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IState<MapState> StateStore { get; set; }
        [Inject] private IDispatcher Dispatcher { get; set; }

        private EventHandler<MapState> StateChangedHandler => (s, e) => InvokeAsync(StateHasChanged);

        private const string kIncidentDetailsUrl = "incidents";


        protected override void OnInitialized()
        {
            StateStore.StateChanged += StateChangedHandler;
            Dispatcher.Dispatch(new LoadGpsDataWithFilterAction());
        }

        private void OnMarkerClick(RadzenGoogleMapMarker marker)
        {
            NavigationManager.NavigateTo($"{kIncidentDetailsUrl}/{marker.Title}");
        }

        private void OnFilterChange(Modalidad filter)
        {
            Dispatcher.Dispatch(new LoadGpsDataWithFilterAction
            {
                UnitTypeFilter = filter
            });
        }

        public void Dispose()
        {
            StateStore.StateChanged -= StateChangedHandler;
        }
    }
}
