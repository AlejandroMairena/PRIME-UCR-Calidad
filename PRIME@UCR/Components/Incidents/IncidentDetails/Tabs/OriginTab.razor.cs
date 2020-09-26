using System;
using System.Collections.Generic;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    enum OriginType
    {
        Household,
        International,
        MedicalCenter
    }
    
    public partial class OriginTab
    {
        private readonly List<Tuple<OriginType, string>> _dropdownValues = new List<Tuple<OriginType, string>>
        {
            Tuple.Create(OriginType.Household, "Domicilio"),
            Tuple.Create(OriginType.International, "Internacional"),
            Tuple.Create(OriginType.MedicalCenter, "Centro médico")
        };

        private OriginType _selectedOriginType = OriginType.Household;
        
        void OnChange(OriginType originType)
        {
            _selectedOriginType = originType;
        }
    }
    
}