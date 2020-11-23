using BlazorTable;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.Appointments
{
    public partial class PrescriptionListComponent
    {
        [Parameter] public string AppointmentId { get; set; }

        [Parameter] public List<PoseeReceta> Medpres { get; set; }
        public List<PoseeReceta> medicalprescrip { get; set; }

        public ITable<PoseeReceta> appointprescripModel { get; set; }

        public string prescription_text_area { get; set; }


        protected override async Task OnInitializedAsync()
        {
            //await get_prescriptions();
        }


        public async Task set_prescription_dosis(int idPrescription, int idAppointment)
        {
            await appointment_service.UpdatePrescriptionDosis(idPrescription, idAppointment, prescription_text_area);
            get_prescriptions();
            prescription_text_area = ""; 
        }


        private async Task get_prescriptions()
        {
            IEnumerable<PoseeReceta> records = await appointment_service.GetPrescriptionsByAppointmentId(Convert.ToInt32(AppointmentId));
            Medpres = records.ToList();
            StateHasChanged(); 

        }

    }
}
