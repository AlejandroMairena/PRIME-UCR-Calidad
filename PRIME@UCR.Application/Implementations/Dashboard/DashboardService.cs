using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Permissions.Dashboard;
using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public async  Task<List<Incidente>> GetFilteredIncidentsList(FilterModel Value)
        {
            var filteredList = await GetAllIncidentsAsync();
           
            //InitialDate
            if (Value.InitialDateFilter.HasValue)
            {
                var selectedDate = Value.InitialDateFilter.Value;
                filteredList = filteredList.Where((incident) => DateTime.Compare(selectedDate, incident.Cita.FechaHoraEstimada) < 0).ToList();
            }

            //Final Date
            if (Value.FinalDateFilter.HasValue)
            {
                var selectedDate = Value.FinalDateFilter.Value;
                filteredList = filteredList.Where((incident) => DateTime.Compare(selectedDate, incident.Cita.FechaHoraEstimada) > 0).ToList();
            }

            //Modality
            if (Value.ModalityFilter != null)
            {
                var modality = Value.ModalityFilter.Tipo;
                filteredList = filteredList.Where((incident) => modality == incident.Modalidad).ToList();
            }

            //Origin
            if(Value.HouseholdOriginFilter.District != null)
            {
                var disctrictID = Value.HouseholdOriginFilter.District.Id;
                filteredList = filteredList.Where((incident) => disctrictID == incident.IdOrigen).ToList();
            }

            /*Destination
            if (Value.HouseholdOriginFilter.District != null)
            {
                var disctrictID = Value.HouseholdOriginFilter.District.Id;
                filteredList = filteredList.Where((incident) => disctrictID == incident.IdOrigen).ToList();
            }*/

            /*
            if (Value.HouseholdOriginFilter != null)
            {
                var disctrictID = Value.MedicalCenterDestination.i;
                filteredList = filteredList.Where((incident) => disctrictID == incident.Origen.Id).ToList();
            }*/

            return filteredList;
        }
    }

    [MetadataType(typeof(DashboardServicePermissions))]
    public partial class DashboardService
    {

    }
}
