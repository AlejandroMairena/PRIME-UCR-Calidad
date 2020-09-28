using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Internal;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class HouseholdPicker
    {
        [Inject]
        public ILocationService LocationService { get; set; }
        
        [Parameter]
        public Ubicacion Value { get; set; }
        
        [Parameter]
        public EventCallback<Ubicacion> ValueChanged { get; set; }

        private List<Provincia> _provinces;
        private List<Canton> _cantons;
        private List<Distrito> _districts;

        private Provincia _selectedProvince;
        private Canton _selectedCanton;
        private Distrito _selectedDistrict;
        private string _address;
        private double _longitude;
        private double _latitude;

        // Check if everything has been loaded
        bool IsLoading()
        {
            return _provinces == null || _selectedProvince == null ||
                   _cantons == null || _selectedCanton == null ||
                   _districts == null || _selectedDistrict == null;
        }

        async Task LoadProvinces()
        {
            // get options
            _provinces =
                (await LocationService.GetProvincesByCountryNameAsync(Pais.DefaultCountry))
                .ToList();

            // Could throw invalid operation exception if list is empty,
            // but this should only happen if there are countries in our DB with no provinces registered,
            // which shouldn't happen.
            _selectedProvince = _provinces.First();
        }

        async Task Callback()
        {
            var household = new Domicilio
            {
                DistritoId = _selectedDistrict.Id,
                Direccion = _address,
                Longitud = _longitude,
                Latitud = _latitude
            };
            await ValueChanged.InvokeAsync(household);
        }

        async Task OnChangeProvince(Provincia province)
        {
            _selectedProvince = province;
            await LoadCantons();
            await LoadDistricts();
            await Callback();
        }

        async Task LoadCantons()
        {
            _cantons =
                (await LocationService.GetCantonsByProvinceNameAsync(_selectedProvince.Nombre))
                .ToList();

            _selectedCanton = _cantons.First();
        }

        async Task OnChangeCanton(Canton canton)
        {
            _selectedCanton = canton;
            await LoadDistricts();
            await Callback();
        }

        async Task LoadDistricts()
        {
            _districts =
                (await LocationService.GetDistrictsByCantonIdAsync(_selectedCanton.Id))
                .ToList();

            _selectedDistrict = _districts.First();
        }

        async Task OnChangeDistrict(Distrito district)
        {
            _selectedDistrict = district;
            await Callback();
        }

        private async Task LoadExistingValues()
        {
            if (Value is Domicilio household)
            {
                var location = await LocationService.GetLocationByDistrictId(household.DistritoId);
                await LoadProvinces();
                _selectedProvince = location.Province;
                await LoadCantons();
                _selectedCanton = location.Canton;
                await LoadDistricts();
                _selectedDistrict = location.District;
                _address = household.Direccion;
                _longitude = household.Longitud;
                _latitude = household.Latitud;
            }
            else
            {
                await LoadProvinces();
                await LoadCantons();
                await LoadDistricts();
                await Callback();
            }
        }
        
        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues();
        }
    }
}
