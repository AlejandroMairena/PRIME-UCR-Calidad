using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.StateManagement.Interfaces.Incidents;
using Radzen;
using Radzen.Blazor;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class IncidentMap : IDisposable
    {
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private IGpsDataService GpsDataService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IRealTimeMapState RealTimeMapState { get; set; }

        const int kDefaultZooom = 10;
        const string kIncidentDetailsUrl = "incidents";
        private readonly GoogleMapPosition kDefaultCenter = new()
        {
            Lat = 9.927069,
            Lng = -83.188547
        };

        private bool _isLoading = true;
        private GoogleMapPosition _center;

        protected override async Task OnInitializedAsync()
        {
            RealTimeMapState.ReceivedNewGpsData += HandleNewGpsData;
            await RealTimeMapState.StartGpsMonitoring();
            _isLoading = false;
        }

        private void HandleNewGpsData(object sender, List<IncidentGpsData> incidentGpsData)
        {
            CalculateMapCenter();
            RefreshPage();
        }

        private void RefreshPage()
        {
            // necessary to guarantee that rendering is executed on UI thread
            // https://blazor-university.com/components/multi-threaded-rendering/invokeasync/
            InvokeAsync(StateHasChanged);
        }

        private void CalculateMapCenter()
        {
            if (RealTimeMapState.GpsData.Count > 0)
            {
                var centerLat = RealTimeMapState.GpsData.Average(i => i.CurrentLatitude);
                var centerLong = RealTimeMapState.GpsData.Average(i => i.CurrentLongitude);
                _center = new GoogleMapPosition
                {
                    Lat = centerLat,
                    Lng = centerLong
                };
            }
            else
            {
                _center = kDefaultCenter;
            }
        }

        private void OnMarkerClick(RadzenGoogleMapMarker marker)
        {
            NavigationManager.NavigateTo($"{kIncidentDetailsUrl}/{marker.Title}");
        }

        public void Dispose()
        {
            RealTimeMapState.ReceivedNewGpsData -= HandleNewGpsData;
        }
    }
}
