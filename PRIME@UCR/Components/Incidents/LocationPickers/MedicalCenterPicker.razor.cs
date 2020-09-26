using System.Collections.Generic;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class MedicalCenterPicker
    {
        enum MedicalCenterType
        {
            Cima,
            Mexico
        }

        //Selected type
        private MedicalCenterType _selectedMedicalCenterType = MedicalCenterType.Cima;

        private readonly List<(MedicalCenterType, string)> _dropdownValuesMedicalCenter = new List<(MedicalCenterType, string)>
        {
            (MedicalCenterType.Cima, "CIMA"),
            (MedicalCenterType.Mexico, "México")
        };

        void OnChangeMedicalCenter(MedicalCenterType medicalType)
        {
            _selectedMedicalCenterType = medicalType;
        }

    }
}