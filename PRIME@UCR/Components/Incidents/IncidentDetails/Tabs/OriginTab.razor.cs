using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Components.Controls;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    //Enum with the options for selects
    enum OriginType
    {
        Household,
        International,
        MedicalCenter
    }

    public partial class OriginTab
    {
        // Selected options
        private Tuple<OriginType, string> _selectedOriginType = Tuple.Create(OriginType.Household, "Domicilio");
        private OriginModel _model = new OriginModel();
        private string _errorMessage = "";

        [Parameter]
        public Ubicacion Origin { get; set; }

        [Parameter]
        public EventCallback<OriginModel> OnSave { get; set; }
        
        // Lists of options
        private readonly List<Tuple<OriginType, string>> _dropdownValuesOrigin = new List<Tuple<OriginType, string>>
        {
            Tuple.Create(OriginType.Household, "Domicilio"),
            Tuple.Create(OriginType.International, "Internacional"),
            Tuple.Create(OriginType.MedicalCenter, "Centro médico")
        };

        private void OnOriginTypeChange(Tuple<OriginType, string> type)
        {
            _errorMessage = "";
            _selectedOriginType = type;
        }

        private void OnOriginChange(Ubicacion origin)
        {
            _model.Origin = origin;
        }
        
        private async Task Save()
        {
            if (_model.Origin is Domicilio household &&
                (String.IsNullOrEmpty(household.Direccion) || household.Direccion.Length > 150))
            {
                _errorMessage = "La dirección es obligatoria y no debe exceder 150 caracteres.";
                return;
            }

            _errorMessage = "";
            await OnSave.InvokeAsync(_model);
        }

        private void LoadExistingValues()
        {
            if (Origin is Domicilio)
                _selectedOriginType = _dropdownValuesOrigin[0];
            else if (Origin is Internacional)
                _selectedOriginType = _dropdownValuesOrigin[1];
            else if (Origin is CentroUbicacion)
                _selectedOriginType = _dropdownValuesOrigin[2];
            
            _model.Origin = Origin;
        }

        protected override void OnInitialized()
        {
            LoadExistingValues();
        }
    }
    
}