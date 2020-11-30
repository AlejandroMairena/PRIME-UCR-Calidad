using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Appointments
{
    public class MetricsRepository : GenericRepository<Metricas, int>, IMetricsRepository
    {

        public MetricsRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {

        }


        public async Task<Metricas> GetMetricsByAppId(int id) {
            return await _db.Metrics.FirstOrDefaultAsync(p => p.CitaId == id);
        }


    }
}
