using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Permissions.Dashboard;
using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.Dashboard
{
    public partial class DashboardService : IDashboardService
    {
        public readonly IDashboardRepository dashboardRepository;
        public readonly IPrimeSecurityService primeSecurity;

        public DashboardService(IDashboardRepository dashboardRep, IPrimeSecurityService primeSecurityService)
        {
            dashboardRepository = dashboardRep;
            primeSecurity = primeSecurityService;
        }

        public async Task<int> GetIncidentCounterAsync(string modality)
        {
            await primeSecurity.CheckIfIsAuthorizedAsync(this.GetType());
            return await dashboardRepository.GetIncidentsCounterAsync(modality);
        }

        public async Task<List<Incidente>> GetAllIncidentsAsync()
        {
            return await dashboardRepository.GetAllIncidentsAsync();
        }

        public async Task<List<Distrito>> GetAllDistrictsAsync()
        {
            return await dashboardRepository.GetAllDistrictsAsync();
        }
    }

    [MetadataType(typeof(DashboardServicePermissions))]
    public partial class DashboardService
    {

    }
}
