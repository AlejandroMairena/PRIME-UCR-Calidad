using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Models.Incidents;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class HouseholdPicker
    {
        [Inject]
        public IIncidentService IncidentService { get; set; }

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

        T LoadFirst<T>(IEnumerable<Tuple<T, string>> values)
        {
            var tuple = values.FirstOrDefault();
            if (tuple != null)
                return tuple.Item1;
            
            throw new ArgumentException("List was empty or first tuple was null");
        }

        List<Tuple<T, string>> LoadAsTupleList<T>(IEnumerable<T> values, string displayProperty)
        {
            return values
                .Select(val =>
                {
                    var t = typeof(T);
                    return Tuple.Create(val, t.GetProperty(displayProperty).GetValue(val) as string);
                    // possible null reference, intentionally unchecked
                })
                .ToList(); 
        }
        
        async Task LoadCountries()
        {
            _countries = (await IncidentService.GetAllCountriesAsync())
                .Select(c => Tuple.Create(c, c.Nombre))
                .ToList();
            _selectedCountry = LoadFirst(_countries);

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
            var provinces =
                await IncidentService.GetProvincesByCountryAsync(_selectedCountry);
            _provinces = LoadAsTupleList(provinces, "Nombre");
            
            _selectedProvince = LoadFirst(_provinces);
            
            await LoadCantons();
            await LoadDistricts();
        }

        async Task OnChangeProvince(Provincia province)
        {
            _selectedProvince = province;

            await LoadCantons();
        }

        async Task LoadCantons()
        {
            var cantons =
                await IncidentService.GetCantonsByProvinceAsync(_selectedProvince);
            _cantons = LoadAsTupleList(cantons, "Nombre");
            
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
                await IncidentService.GetDistrictsByCantonAsync(_selectedCanton);
            _districts = LoadAsTupleList(districts, "Nombre");
        }

        void OnChangeDistrict(Distrito district)
        {
            _selectedDistrict = district;
        }

        async Task InitializeComponent()
        {
            await LoadCountries();
            await LoadProvinces();
            await LoadCantons();
            await LoadDistricts();
        }
    }
}
