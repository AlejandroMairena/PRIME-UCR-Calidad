using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<List<Incidente>> GetAllIncidentsAsync();

        Task<List<Distrito>> GetAllDistrictsAsync();
    }
}
