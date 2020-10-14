using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Components.Incidents.LocationPickers;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    // Enum with the options for available origin types
    enum OriginType
    {
        Household,
        International,
        MedicalCenter
    }

    public partial class OriginTab
    {
        // Selected options
        private Tuple<OriginType, string> _selectedOriginType;
        private OriginModel _model = new OriginModel();
        private HouseholdModel _householdModel = new HouseholdModel();
        private string _statusMessage = "";

        [Parameter]
        public Ubicacion Origin { get; set; }

        [Parameter]
        public EventCallback<OriginModel> OnSave { get; set; }

        private InternationalPicker _intlPicker;
        private HouseholdPicker _householdPicker;
        private MedicalCenterPicker _medicalCenterPicker;
        
        // Lists of options
        private readonly List<Tuple<OriginType, string>> _dropdownValuesOrigin = new List<Tuple<OriginType, string>>
        {
            Tuple.Create(OriginType.Household, "Domicilio"),
            Tuple.Create(OriginType.International, "Internacional"),
            Tuple.Create(OriginType.MedicalCenter, "Centro médico")
        };

        private void OnOriginTypeChange(Tuple<OriginType, string> type)
        {
            _statusMessage = "";
            _selectedOriginType = type;
        }

        private void OnOriginChange(Ubicacion origin)
        {
            _model.Origin = origin;
        }

        private void OnHouseholdChange(HouseholdModel household)
        {
            if (household != null)
            {
                _model.Origin = new Domicilio
                {
                    Direccion = household.Address,
                    DistritoId = household.District.Id,
                    Longitud = household.Longitude,
                    Latitud = household.Latitude
                };
            }
            else
                _model.Origin = null;
        }

        private async Task Save()
        {
            _statusMessage = "Se guardaron los cambios exitosamente.";
            await OnSave.InvokeAsync(_model);
        }

        private async Task LoadExistingValues(bool isFirstRender)
        {
            if (Origin is Domicilio)
            {
                _selectedOriginType = _dropdownValuesOrigin[0];
                if (_householdPicker != null && !isFirstRender)
                    await _householdPicker.LoadExistingValues();
            }
            else if (Origin is Internacional)
            {
                _selectedOriginType = _dropdownValuesOrigin[1];
                if (_intlPicker != null && !isFirstRender)
                    await _intlPicker.LoadExistingValues();
            }
            else if (Origin is CentroUbicacion)
            {
                _selectedOriginType = _dropdownValuesOrigin[2];
                if (_medicalCenterPicker != null && !isFirstRender)
                    await _medicalCenterPicker.LoadExistingValues();
            }
            else
                _selectedOriginType = _dropdownValuesOrigin[0]; // default
            
            _model.Origin = Origin;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues(true);
        }
    }
    
}