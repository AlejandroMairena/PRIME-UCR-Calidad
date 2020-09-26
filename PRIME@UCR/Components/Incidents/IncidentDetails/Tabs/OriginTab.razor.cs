using System;
using System.Collections.Generic;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    //Enum with the options for selects
    enum OriginType
    {
        Household,
        International,
        MedicalCenter
    }

    enum CountryType {
        CostaRica, 
        UnitedStates,
        Panama
    }

    public partial class OriginTab
    {
        private readonly List<Tuple<OriginType, string>> _dropdownValues = new List<Tuple<OriginType, string>>
        // Selected options
        private OriginType _selectedOriginType = OriginType.Household;

        private CountryType _selectedCountryType = CountryType.CostaRica;

        // Lists of options
        private readonly List<(OriginType, string)> _dropdownValuesOrigin = new List<(OriginType, string)>
        {
            Tuple.Create(OriginType.Household, "Domicilio"),
            Tuple.Create(OriginType.International, "Internacional"),
            Tuple.Create(OriginType.MedicalCenter, "Centro médico")
        };

        private readonly List<(CountryType, string)> _dropdownValuesCountries = new List<(CountryType, string)>
        {
            (CountryType.CostaRica, "Costa Rica"),
            (CountryType.UnitedStates, "Estados Unidos"),
            (CountryType.Panama, "Panamá")
        };
        
        void OnChange(OriginType originType)
        {
            _selectedOriginType = originType;
        }
    }
    
}