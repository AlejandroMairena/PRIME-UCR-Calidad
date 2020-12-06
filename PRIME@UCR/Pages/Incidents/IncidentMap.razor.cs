using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using Radzen;
using Radzen.Blazor;

namespace PRIME_UCR.Pages.Incidents
{
    public partial class IncidentMap
    {
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private IGpsDataService GpsDataService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        const int kDefaultZooom = 10;
        const string kIncidentDetailsUrl = "incidents";
        private readonly GoogleMapPosition kDefaultCenter = new GoogleMapPosition
        {
            Lat = 9.927069,
            Lng = -83.188547
        };

        private bool _isLoading = true;
        private List<IncidentGpsData> _data;
        private GoogleMapPosition _center;

        protected override async Task OnInitializedAsync()
        {
            _data =
                (await GpsDataService.GetAllGpsDataAsync())
                .ToList();

            if (_data.Count > 0)
            {
                var centerLat = _data.Average(i => i.CurrentLatitude);
                var centerLong = _data.Average(i => i.CurrentLongitude);
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

            _isLoading = false;
        }

        private void OnMarkerClick(RadzenGoogleMapMarker marker)
        {
            NavigationManager.NavigateTo($"{kIncidentDetailsUrl}/{marker.Title}");
        }
    }
}
