using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Application.DTOs.MedicalRecords;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Appointments;

namespace PRIME_UCR.Components.MedicalAppointments.Tabs
{
    public partial class AppointmentGeneralInfo
    {
        [Parameter] public CitaMedica Appointment { get; set; }

        public Médico doctor { get; set; }

        public EstadoCitaMedica current_state { get; set; }


        protected override async Task OnInitializedAsync()
        {




        }

    }
}
