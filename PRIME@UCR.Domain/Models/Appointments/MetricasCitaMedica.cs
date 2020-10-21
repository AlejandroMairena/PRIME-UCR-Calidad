using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.Appointments
{
    public class MetricasCitaMedica: Metricas
    {
        public double Presion { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
    }
}
