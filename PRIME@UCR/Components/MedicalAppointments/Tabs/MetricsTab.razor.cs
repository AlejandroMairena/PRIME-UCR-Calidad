using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.MedicalAppointments.Tabs
{
    public partial class MetricsTab
    { 
        [Parameter] public int AppointmentId { get; set; }

        private EditContext MetricContext;

        private MetricsApp MetricsForm { get; set; }

        private MetricasCitaMedica Metrics;

        private bool metrics_saved { get; set; } = false;

        private bool metrics_updated { get; set; } = false; 

        protected override async Task OnInitializedAsync() {
            MetricsForm = new MetricsApp();
            MetricContext = new EditContext(MetricsForm);
            Metrics = await appointment_service.GetMetricsMedAppointmentByAppId(AppointmentId);
            if (Metrics != null)
            {
                MetricsForm.Altura = Metrics.Altura.ToString();
                MetricsForm.Peso = Metrics.Peso.ToString();
                MetricsForm.Presion = Metrics.Presion.ToString();
            }
        
        }


        public async Task saveMetricData() {
            metrics_saved = false;
            metrics_updated = false; 

            Metrics = await appointment_service.GetMetricsMedAppointmentByAppId(AppointmentId);

            if (Metrics != null)
            {

                Metrics.Altura = MetricsForm.Altura;
                Metrics.Peso = MetricsForm.Peso;
                Metrics.Presion = MetricsForm.Presion;

                await appointment_service.UpdateMetrics(Metrics);
                metrics_updated = true; 
            }
            else
            {
                Metrics = new MetricasCitaMedica()
                {
                    Altura = MetricsForm.Altura,
                    Peso = MetricsForm.Peso,
                    Presion = MetricsForm.Presion,
                    CitaId = AppointmentId
                }; 
                await appointment_service.InsertMetrics(Metrics);
                metrics_saved = true;
            }
        }
    }
}




/*
 * 
 * INSERT INTO Cita(FechaHoraCreacion, FechaHoraEstimada, IdExpediente)
VALUES(GETDATE(), GETDATE(), 18)

INSERT INTO CitaMedica(ExpedienteId, CedMedicoAsignado, CentroMedicoId, EstadoId, CitaId)
VALUES(18, 22222222, 2, 7, 45)

 * 
 * */
