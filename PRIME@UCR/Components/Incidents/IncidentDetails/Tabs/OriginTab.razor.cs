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
        // Selected options
        private OriginType _selectedOriginType = OriginType.Household;

        private CountryType _selectedCountryType = CountryType.CostaRica;

        // Lists of options
        private readonly List<Tuple<OriginType, string>> _dropdownValuesOrigin = new List<Tuple<OriginType, string>>
        {
            Tuple.Create(OriginType.Household, "Domicilio"),
            Tuple.Create(OriginType.International, "Internacional"),
            Tuple.Create(OriginType.MedicalCenter, "Centro médico")
        };

        private readonly List<Tuple<CountryType, string>> _dropdownValuesCountries = new List<Tuple<CountryType, string>>
        {
            Tuple.Create(CountryType.CostaRica, "Costa Rica"),
            Tuple.Create(CountryType.UnitedStates, "Estados Unidos"),
            Tuple.Create(CountryType.Panama, "Panamá")
        };
        
        void OnChangeOrigin(OriginType originType)
        {
            _selectedOriginType = originType;
        }
        
        void OnChangeCountry(CountryType countryType)
        {
            _selectedCountryType = countryType;
        }
    }
    
}