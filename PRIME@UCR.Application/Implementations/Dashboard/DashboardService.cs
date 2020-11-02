using Microsoft.EntityFrameworkCore.Metadata;
using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.Dashboard
{
    public class DashboardService : IDashboardService
    {
        protected readonly IDashboardRepository _idashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepo)
        {
            _idashboardRepository = dashboardRepo;
        }

        public async Task<List<Incidente>> GetAllIncidentsAsync()
        {
            return await _idashboardRepository.GetAllIncidentsAsync();
        }
    }
}
