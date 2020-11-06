using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Parameter] public Ubicacion Origin { get; set; }
        [Parameter] public EventCallback<OriginModel> OnSave { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }

        [Parameter] public FilterModel Value { get; set; }

        // Selected options
        private Tuple<OriginType, string> _selectedOriginType;

        // Lists of options
        private readonly List<Tuple<OriginType, string>> _dropdownValuesOrigin = new List<Tuple<OriginType, string>>
        {
            Tuple.Create(OriginType.SinFiltro, EnumUtils.GetDescription(OriginType.SinFiltro)),
            Tuple.Create(OriginType.Household, EnumUtils.GetDescription(OriginType.Household)),
            Tuple.Create(OriginType.International, EnumUtils.GetDescription(OriginType.International)),
            Tuple.Create(OriginType.MedicalCenter, EnumUtils.GetDescription(OriginType.MedicalCenter))
        };

        private void OnOriginTypeChange(Tuple<OriginType, string> type)
        {
            _selectedOriginType = type;
        }

        private void OnOriginChange(Ubicacion origin)
        {
            Value.OriginFilter.Origin = origin;
        }

        private bool _isLoading;

        private async Task OnHouseholdSave(HouseholdModel household)
        {
            if (household.Longitude != null && household.Latitude != null)
            {
                Value.OriginFilter.Origin = new Domicilio
                {
                    Direccion = household.Address,
                    DistritoId = household.District.Id,
                    Longitud = (double)household.Longitude,
                    Latitud = (double)household.Latitude
                };
            }
            else
            {
                throw new ApplicationException("Household picker shouldn't return null longitude or latitude");
            }

            Value.HouseholdOriginFilter = household;
            await Save();
        }

        private async Task OnInternationalSave(InternationalModel international)
        {
            Value.OriginFilter.Origin = new Internacional
            {
                NombrePais = international.Country.Nombre
            };

            Value.InternationalOriginFilter = international;
            await Save();
        }

        private async Task OnMedicalCenterSave(MedicalCenterLocationModel medicalCenter)
        {
            Value.OriginFilter.Origin = new CentroUbicacion
            {
                CedulaMedico = medicalCenter.Doctor.Cédula,
                CentroMedicoId = medicalCenter.MedicalCenter.Id,
                NumeroCama = medicalCenter.BedNumber
            };

            Value.MedicalCenterOriginFilter = medicalCenter;
            await Save();
        }

        private async Task Save()
        {
            await OnSave.InvokeAsync(Value.OriginFilter);
        }

        private async Task LoadExistingValues()
        {
            _isLoading = true;
            StateHasChanged();
            switch (Origin)
            {
                case Domicilio d:
                    {
                        _selectedOriginType = _dropdownValuesOrigin[0];
                        var location = await LocationService.GetLocationByDistrictId(d.DistritoId);
                        Value.HouseholdOriginFilter = new HouseholdModel
                        {
                            Province = location.Province,
                            Canton = location.Canton,
                            District = location.District,
                            Address = d.Direccion,
                            Longitude = d.Longitud,
                            Latitude = d.Latitud
                        };
                        break;
                    }
                case Internacional i:
                    _selectedOriginType = _dropdownValuesOrigin[1];
                    Value.InternationalOriginFilter = new InternationalModel
                    {
                        Country = await LocationService.GetCountryByName(i.NombrePais)
                    };
                    break;
                case CentroUbicacion mc:
                    _selectedOriginType = _dropdownValuesOrigin[2];
                    var doctor = await DoctorService.GetDoctorByIdAsync(mc.CedulaMedico);
                    var medicalCenter = await LocationService.GetMedicalCenterById(mc.CentroMedicoId);
                    Value.MedicalCenterOriginFilter = new MedicalCenterLocationModel
                    {
                        IsOrigin = true,
                        BedNumber = mc.NumeroCama,
                        Doctor = doctor,
                        MedicalCenter = medicalCenter
                    };
                    break;
                default:
                    _selectedOriginType = _dropdownValuesOrigin[0];
                    break;
            }
            Value.OriginFilter.Origin = Origin;
            _isLoading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues();
        }
    }

}