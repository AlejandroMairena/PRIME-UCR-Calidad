using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Implementations.Dashboard;
using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Attributes;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Permissions.Dashboard
{
    public class SecureDashboardService : IDashboardService
    {
        private readonly IPrimeSecurityService primeSecurityService;

        private readonly DashboardService dashboardService;

        public SecureDashboardService(IDashboardRepository dashboardRep,
            IIncidentRepository _incidentRepository,
            IDistrictRepository _districtRepository,
            ICountryRepository _countryRepository,
            IMedicalCenterRepository _medicalCenterRepository,
            IPrimeSecurityService _primeSecurityService)
        {
            primeSecurityService = _primeSecurityService;
            dashboardService = new DashboardService(dashboardRep, _incidentRepository, _districtRepository, _countryRepository, _medicalCenterRepository);
        }

        public async Task<int> GetIncidentCounterAsync(string modality)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(new[] { AuthorizationPermissions.CanSeeIncidentsInfoOnDashboard });
            return await dashboardService.GetIncidentCounterAsync(modality);
        }
        
        public async Task<List<Distrito>> GetAllDistrictsAsync()
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(new[] { AuthorizationPermissions.CanSeeIncidentsInfoOnDashboard });
            return await dashboardService.GetAllDistrictsAsync();
        }

        public async Task<List<Incidente>> GetAllIncidentsAsync()
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(new[] { AuthorizationPermissions.CanSeeIncidentsInfoOnDashboard });
            return await dashboardService.GetAllIncidentsAsync();
        }

        public async Task<List<Incidente>> GetFilteredIncidentsList(FilterModel Value)
        {
            await primeSecurityService.CheckIfIsAuthorizedAsync(new[] { AuthorizationPermissions.CanSeeIncidentsInfoOnDashboard });
            return await dashboardService.GetFilteredIncidentsList(Value);
        }
    }
}
