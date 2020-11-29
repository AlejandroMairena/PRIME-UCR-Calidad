using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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


        protected override async Task OnInitializedAsync() {
            MetricsForm = new MetricsApp(); 
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
            Metrics = await appointment_service.GetMetricsMedAppointmentByAppId(AppointmentId);

            if (Metrics != null)
            {
                //se tiene que actualizar. 
            }
            else {

                //falta insertar tmbn en la tabla Metricas. 

                Metrics.Altura = Convert.ToDouble(MetricsForm.Altura);
                Metrics.Peso = Convert.ToDouble(MetricsForm.Peso);
                Metrics.Presion = Convert.ToDouble(MetricsForm.Presion);
                Metrics.CitaId = AppointmentId;

                await appointment_service.InsertMetrics(Metrics);
                metrics_saved = true; 
            }

        }


    }
}
