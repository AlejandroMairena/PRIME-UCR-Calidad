using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.Appointments
{
    public interface IMetricsRepository : IGenericRepository<Metricas, int>
    {
        Task<Metricas> GetMetricsByAppId(int id); 

    }
}
