using Microsoft.AspNetCore.Components;
using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.Appointments
{
    public partial class AppointmentView
    {
        [Parameter] public string id { get; set; }

        public List<PoseeReceta> medicalprescrip { get; set; }

        protected override async Task OnInitializedAsync()
        {
            medicalprescrip = new List<PoseeReceta>(); 
            await get_prescriptions();
        }


        private async Task get_prescriptions()
        {
            IEnumerable<PoseeReceta> records = await appointment_service.GetPrescriptionsByAppointmentId(Convert.ToInt32(id));
            medicalprescrip = records.ToList();

        }

        private async Task updatelist(bool f) {
            StateHasChanged();
            await get_prescriptions(); 
        }
    }
}
