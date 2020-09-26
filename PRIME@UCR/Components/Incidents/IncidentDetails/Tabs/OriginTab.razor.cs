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
        private readonly List<(OriginType, string)> _dropdownValues = new List<(OriginType, string)>
        {
            (OriginType.Household, "Domicilio"),
            (OriginType.International, "Internacional"),
            (OriginType.MedicalCenter, "Centro médico")
        };

        private OriginType _selectedOriginType = OriginType.Household;
        
        void OnChange(OriginType originType)
        {
            _selectedOriginType = originType;
        }
    }
    
}