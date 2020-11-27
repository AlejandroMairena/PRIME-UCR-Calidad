using Microsoft.AspNetCore.Components;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.Services.Multimedia;
using PRIME_UCR.Components.MedicalAppointments.Tabs;

namespace PRIME_UCR.Pages.Appointments
{
    public partial class MCAppointmentView
    {
        //[Parameter] public CitaMedica Medappointment {get; set;}

        [Parameter] public string med_appointment_id { get; set; }

       
    }
}
