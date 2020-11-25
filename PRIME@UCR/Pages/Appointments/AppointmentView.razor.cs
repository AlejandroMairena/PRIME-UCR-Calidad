﻿using Microsoft.AspNetCore.Components;
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

        public bool drug_selector_active { get; set; } = true;

        public bool prescription_description_not_done { get; set; } = false;



        protected override void OnInitialized()
        {
            medicalprescrip = new List<PoseeReceta>();
            //await get_prescriptions();
            base.OnInitialized();
        }


        protected override async Task OnInitializedAsync()
        {
            await get_prescriptions();
        }


        private async Task updateChanges(bool action) {

            prescription_description_not_done = false;
            drug_selector_active = true;
            await get_prescriptions(); 
        }

        private async Task get_prescriptions()
        {
            IEnumerable<PoseeReceta> records = await appointment_service.GetPrescriptionsByAppointmentId(Convert.ToInt32(id));
            medicalprescrip = records.ToList();
            StateHasChanged();
        }

        private async Task updatelist(bool f) {
            //tengo que esperar a que se agregue la descripción. 
            drug_selector_active = false; 
            await get_prescriptions();
            StateHasChanged();
        }
    }
}
