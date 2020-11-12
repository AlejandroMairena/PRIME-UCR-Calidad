using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.Dashboard
{
    public interface IDashboardRepository : IGenericRepository<Incidente, string>
    {
        Task<List<Incidente>> GetAllIncidentsAsync();

        Task<List<Distrito>> GetAllDistrictsAsync();
    }
}
