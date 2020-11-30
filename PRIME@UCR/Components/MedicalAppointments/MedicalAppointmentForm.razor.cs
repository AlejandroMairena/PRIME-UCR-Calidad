using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Appointments;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.MedicalAppointments
{
    public partial class MedicalAppointmentForm
    {
        [Parameter] public Expediente RecordModel { get; set; }

        private List<Médico> DoctorList;

        private List<CentroMedico> MedicalCenterList;

        private EditContext _contextDoctor;

        private Médico selectedDoctor;

        private CentroMedico selectedMedicalCenter;

        private DateTime date;

        protected override async Task OnInitializedAsync()
        {
            DoctorList = (await DoctorService.GetAllDoctorsAsync()).ToList();
            MedicalCenterList = (await LocationService.GetAllMedicalCentersAsync()).ToList();
            _contextDoctor = new EditContext(DoctorList);
        }

        private async Task MakeAppointment()
        {
            Cita citaNueva = new Cita() {
                FechaHoraCreacion = DateTime.Now,
                FechaHoraEstimada = date,
                IdExpediente = RecordModel.Id
            };
            var cita = await AppointmentService.InsertAppointmentAsync(citaNueva);
            CitaMedica citaMedicaNueva = new CitaMedica()
            {
                CitaId = citaNueva.Id,
                EstadoId = 7,
                CentroMedicoId = selectedMedicalCenter.Id,
                ExpedienteId = RecordModel.Id,
                CedMedicoAsignado = selectedDoctor.Cédula
            };
            await AppointmentService.InsertMedicalAppointmentAsync(citaMedicaNueva);
        }

    }
}
