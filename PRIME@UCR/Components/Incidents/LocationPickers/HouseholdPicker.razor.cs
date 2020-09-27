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
        public EventCallback<Ubicacion> OnChange { get; set; }

        private List<Tuple<Pais, string>> _countries;
        private List<Tuple<Provincia, string>> _provinces;
        private List<Tuple<Canton, string>> _cantons;
        private List<Tuple<Distrito, string>> _districts;

        private Pais _selectedCountry;
        private Provincia _selectedProvince;
        private Canton _selectedCanton;
        private Distrito _selectedDistrict;

        bool IsLoading()
        {
            return _countries == null ||
                   _provinces == null ||
                   _cantons == null ||
                   _districts == null;
        }

        async Task LoadCountries()
        {
            var countries =
                (await LocationService.GetAllCountriesAsync())
                .ToList();
            _countries = DropDownUtilities.LoadAsTupleList(countries, "Nombre");

            // Load first value
            _selectedCountry = countries.First(); 
            
            // cascaded behavior
            await LoadProvinces();
            await LoadCantons();
            await LoadDistricts();
        }
        
        async Task OnChangeCountry(Pais country)
        {
            _selectedCountry = country;

            await LoadProvinces();
        }

        async Task LoadProvinces()
        {
            // get options
            var provinces =
                (await LocationService.GetProvincesByCountryNameAsync(_selectedCountry.Nombre))
                .ToList();
            _provinces = DropDownUtilities.LoadAsTupleList(provinces, "Nombre");

            // Could throw invalid operation exception if list is empty,
            // but this should only happen if there are countries in our DB with no provinces registered,
            // which shouldn't happen.
            _selectedProvince = provinces.First();
            
            await LoadCantons();
            await LoadDistricts();
        }

        async Task Callback()
        {
            var household = new Domicilio
            {
                DistritoId = _selectedDistrict.Id
            };
            await OnChange.InvokeAsync(household);
        }

        async Task OnChangeProvince(Provincia province)
        {
            _selectedProvince = province;

            await LoadCantons();
        }

        async Task LoadCantons()
        {
            var cantons =
                (await LocationService.GetCantonsByProvinceNameAsync(_selectedProvince.Nombre))
                .ToList();
            _cantons = DropDownUtilities.LoadAsTupleList(cantons, "Nombre");

            _selectedCanton = cantons.First();
            
            await LoadDistricts();
        }

        async Task OnChangeCanton(Canton canton)
        {
            _selectedCanton = canton;

            await LoadDistricts();
        }

        async Task LoadDistricts()
        {
            var districts =
                (await LocationService.GetDistrictsByCantonIdAsync(_selectedCanton.Id))
                .ToList();
            _districts = DropDownUtilities.LoadAsTupleList(districts, "Nombre");

            _selectedDistrict = districts.First();
            await Callback();
        }

        async Task OnChangeDistrict(Distrito district)
        {
            _selectedDistrict = district;
            await Callback();
        }
        
        protected override async Task OnInitializedAsync()
        {
            await LoadCountries();
            await LoadProvinces();
            await LoadCantons();
            await LoadDistricts();
        }
    }
}
