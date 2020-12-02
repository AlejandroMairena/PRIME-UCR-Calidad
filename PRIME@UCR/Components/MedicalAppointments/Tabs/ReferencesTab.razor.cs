using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models.Appointments;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.MedicalAppointments.Tabs
{

    public partial class ReferencesTab
    {
        private EditContext _context;

        public List<EspecialidadMedica> EspecialidadesMedicas;

        public EspecialidadMedica selected_specialty { get; set; }

        public bool already_selected { get; set; } = false; 

        public List<Persona> Doctors { get; set; }

        public Persona selected_doctor { get; set; }

        public DateTime date { get; set; }

        public bool data_inserted { get; set; } = false; 


        protected override async Task OnInitializedAsync() {

            EspecialidadesMedicas = (await appointment_service.GetMedicalSpecialtiesAsync()).ToList();
            _context = new EditContext(EspecialidadesMedicas); 


        }


        public string get_msgname() {
            return "Seleccione el médico (" + selected_specialty.Nombre + ")"; 
        }

        public async Task OnChangeSpecialty(EspecialidadMedica specialty) {
            Doctors = null;
            already_selected = true;
            selected_specialty = specialty; 
            Doctors = (await appointment_service.GetDoctorsBySpecialtyNameAsync(specialty.Nombre)).ToList();
        }


        public void scheduleAppo() { 
        
        
        }

    }
}
