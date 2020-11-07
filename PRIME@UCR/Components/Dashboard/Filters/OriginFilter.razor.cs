using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Components.Incidents.LocationPickers;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Dashboard.Filters
{
    // Enum with the options for available origin types
    enum OriginType
    {
        [Description("Sin filtro")]
        SinFiltro,
        [Description("Domicilio")]
        Household,
        [Description("Internacional")]
        International,
        [Description("Centro médico")]
        MedicalCenter
    }

    public partial class OriginFilter
    {

        [Inject] private ILocationService LocationService { get; set; }
        [Inject] private IDoctorService DoctorService { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public FilterModel Value { get; set; }

        // Origin needed attributes

        private Tuple<OriginType, string> _selectedOriginType;
        private readonly List<Tuple<OriginType, string>> _dropdownValuesOrigin = new List<Tuple<OriginType, string>>
        {
            Tuple.Create(OriginType.SinFiltro, EnumUtils.GetDescription(OriginType.SinFiltro)),
            Tuple.Create(OriginType.Household, EnumUtils.GetDescription(OriginType.Household)),
            Tuple.Create(OriginType.International, EnumUtils.GetDescription(OriginType.International)),
            Tuple.Create(OriginType.MedicalCenter, EnumUtils.GetDescription(OriginType.MedicalCenter))
        };

        // Household needed attributes

        private List<Provincia> _provinces;
        private List<Canton> _cantons;
        private List<Distrito> _districts;

        // International needed attributes

        private List<Pais> _countries;

        // Medical centers needed attributes

        private List<CentroMedico> _medicalCenters;

        private bool _isLoading;

        private void OnOriginTypeChange(Tuple<OriginType, string> type)
        {
            _selectedOriginType = type;
            Value.OriginType = type.Item2;
            if (Value.OriginType == "Domicilio")
            {
                Value.InternationalOriginFilter.Country = null;
                Value.MedicalCenterOriginFilter.MedicalCenter = null;
            }
            else if (Value.OriginType == "Internacional")
            {
                Value.HouseholdOriginFilter.District = null;
                Value.HouseholdOriginFilter.Canton = null;
                Value.HouseholdOriginFilter.Province = null;
                Value.MedicalCenterOriginFilter.MedicalCenter = null;
            }
            else 
            {
                Value.HouseholdOriginFilter.District = null;
                Value.HouseholdOriginFilter.Canton = null;
                Value.HouseholdOriginFilter.Province = null;
                Value.InternationalOriginFilter.Country = null;
            }
        }

        private async Task LoadExistingValues()
        {
            _isLoading = true;
            StateHasChanged();
            
            _selectedOriginType = _dropdownValuesOrigin[0];
            //Initialize household attributes
            
            await LoadProvinces(true);
            await LoadCantons(true);
            await LoadDistricts(true);

            //Initialize international attributes

            await LoadCountries(true);

            //Initialize medical center attributes
            await LoadMedicalCenters(true);

            _isLoading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues();
        }

        // Household needed methods

        async Task LoadProvinces(bool firstLoad)
        {
            // get options
            _provinces =
                (await LocationService.GetProvincesByCountryNameAsync(Pais.DefaultCountry))
                .ToList();

            if (!firstLoad)
                Value.HouseholdOriginFilter.Province = null;
        }

        async Task OnChangeProvince(Provincia province)
        {
            Value.HouseholdOriginFilter.Province = province;
            await LoadCantons(false);
            await LoadDistricts(false);
        }

        async Task LoadCantons(bool firstLoad)
        {
            if (Value.HouseholdOriginFilter.Province != null)
                _cantons =
                    (await LocationService.GetCantonsByProvinceNameAsync(Value.HouseholdOriginFilter.Province.Nombre))
                    .ToList();
            else
                _cantons = new List<Canton>();

            if (!firstLoad)
                Value.HouseholdOriginFilter.Canton = null;
        }

        async Task OnChangeCanton(Canton canton)
        {
            Value.HouseholdOriginFilter.Canton = canton;
            await LoadDistricts(false);
        }

        async Task LoadDistricts(bool firstLoad)
        {
            if (Value.HouseholdOriginFilter.Canton != null)
                _districts =
                    (await LocationService.GetDistrictsByCantonIdAsync(Value.HouseholdOriginFilter.Canton.Id))
                    .ToList();
            else
                _districts = new List<Distrito>();

            if (!firstLoad)
                Value.HouseholdOriginFilter.District = null;
        }

        // Needed international methods 

        private async Task LoadCountries(bool firstRender) 
        {
            _countries = (await LocationService.GetAllCountriesAsync())
                .Where(c => c.Nombre != Pais.DefaultCountry)
                .ToList();
            if (!firstRender)
                Value.InternationalOriginFilter.Country = null;

        }

        // Needed medical centers methods
        async Task OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            Value.MedicalCenterOriginFilter.MedicalCenter = medicalCenter;
        }

        private async Task LoadMedicalCenters(bool firstRender)
        {
            _medicalCenters =
                (await LocationService.GetAllMedicalCentersAsync())
                .ToList();

            if (!firstRender)
                Value.MedicalCenterOriginFilter.MedicalCenter = null;
        }

    }

}