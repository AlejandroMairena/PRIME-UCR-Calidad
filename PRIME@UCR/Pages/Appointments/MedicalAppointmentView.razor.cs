using Microsoft.AspNetCore.Components;
using PRIME_UCR.Components.MedicalAppointments;
using PRIME_UCR.Components.MedicalRecords.Constants;
using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages.Appointments
{
    public partial class MedicalAppointmentView
    {
        [Parameter] public string id { get; set; }

        private readonly List<Tuple<MADetailsTab, string>> _tabs = new List<Tuple<MADetailsTab, string>>();

        const MADetailsTab DefaultTab = MADetailsTab.Recetas;

        private MADetailsTab _activeTab = DefaultTab;

        protected bool exists = true;

        public CitaMedica appointment { get; set; }

        public RecordSummary Summary;



        private void FillTabStates()
        {
            _tabs.Clear();
            var tabValues = Enum.GetValues(typeof(MADetailsTab)).Cast<MADetailsTab>();
            foreach (var tab in tabValues)
            {
                switch (tab)
                {
                    case MADetailsTab.Recetas:
                        _tabs.Add(new Tuple<MADetailsTab, string>(MADetailsTab.Recetas, ""));
                        break;
                    case MADetailsTab.Multimedia:
                        _tabs.Add(new Tuple<MADetailsTab, string>(MADetailsTab.Multimedia, ""));
                        break;
                }
            }
        }



        protected override async Task OnInitializedAsync()
        {
            appointment = await appointment_service.GetMedicalAppointmentByKeyAsync(Convert.ToInt32(id)); 
            //Summary = new RecordSummary();
            //Summary.LoadValues(appointment);

            if (appointment == null)
                exists = false;
            else
                FillTabStates();

        }


    }
}
