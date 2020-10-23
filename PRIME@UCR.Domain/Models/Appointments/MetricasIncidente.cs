using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Domain.Models.Appointments
{
    public class MetricasIncidente: Metricas
    {
        public double CircToraxica { get; set; }
        public double CircAbdominal { get; set; }
        public double Talla { get; set; }
    }
}
