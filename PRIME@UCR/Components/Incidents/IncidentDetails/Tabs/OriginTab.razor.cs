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

    public partial class OriginTab
    {
        // Selected options
        private OriginType _selectedOriginType = OriginType.Household;

        // Lists of options
        private readonly List<Tuple<OriginType, string>> _dropdownValuesOrigin = new List<Tuple<OriginType, string>>
        {
            Tuple.Create(OriginType.Household, "Domicilio"),
            Tuple.Create(OriginType.International, "Internacional"),
            Tuple.Create(OriginType.MedicalCenter, "Centro médico")
        };
        
        void OnChangeOrigin(OriginType originType)
        {
            _selectedOriginType = originType;
        }
        
    }
    
}