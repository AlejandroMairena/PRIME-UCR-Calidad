using System;
using System.ComponentModel;

namespace PRIME_UCR.Components.MedicalRecords
{
    public enum DetailsTab
    {
        [Description("Informacion general")]
        Info,
        [Description("Citas anteriores")]
        Appointments
    }
}