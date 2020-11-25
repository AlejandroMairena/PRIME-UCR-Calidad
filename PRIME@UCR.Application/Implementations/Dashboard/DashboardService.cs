using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Permissions.Dashboard;
using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Application.Repositories.Incidents;
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
    internal class DashboardService : IDashboardService
    {
        public readonly IDashboardRepository dashboardRepository;
        public readonly IIncidentRepository incidentRepository;
        public readonly IDistrictRepository districtRepository;
        public readonly ICountryRepository countryRepository;
        public readonly IMedicalCenterRepository medicalCenterRepository;

        public DashboardService(IDashboardRepository dashboardRep, 
            IIncidentRepository _incidentRepository,
            IDistrictRepository _districtRepository,
            ICountryRepository _countryRepository,
            IMedicalCenterRepository _medicalCenterRepository)
        {
            dashboardRepository = dashboardRep;
            incidentRepository = _incidentRepository;
            districtRepository = _districtRepository;
            countryRepository = _countryRepository;
            medicalCenterRepository = _medicalCenterRepository;
        }

        public async Task<int> GetIncidentCounterAsync(string modality)
        {
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
            var AllDistricts = await dashboardRepository.GetAllDistrictsAsync();
            var AllCountries = (await countryRepository.GetAllAsync()).ToList();
            var AllMedicalCenters = (await medicalCenterRepository.GetAllAsync()).ToList();
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

            // Origin
            if(Value.OriginType != null)
            {
                if(Value.OriginType == "Internacional")
                {
                    if(Value.InternationalOriginFilter.Country != null)
                    {
                        var country = Value.InternationalOriginFilter.Country.Nombre;
                        filteredList = filteredList.Where((incident) => AllCountries.Exists(c => incident.Origen is Internacional i
                                                                                            && c.Nombre == country
                                                                                            && i.NombrePais == c.Nombre)).ToList();   
                    } else
                    {
                        filteredList = filteredList.Where((incident) => incident.Origen is Internacional i).ToList();
                    }
                } else if(Value.OriginType == "Centro médico")
                {
                    if(Value.MedicalCenterOriginFilter.MedicalCenter != null)
                    {
                        var medicalCenterName = Value.MedicalCenterOriginFilter.MedicalCenter.Nombre;
                        filteredList = filteredList.Where((incident) => AllMedicalCenters.Exists(m => incident.Origen is CentroUbicacion c
                                                                                            && m.Nombre == medicalCenterName
                                                                                            && c.CentroMedicoId == m.Id)).ToList();
                    }
                    else
                    {
                        filteredList = filteredList.Where((incident) => incident.Origen is CentroUbicacion c).ToList();
                    }
                } else
                {
                    if(Value.HouseholdOriginFilter.Province != null)
                    {
                        var provinceName = Value.HouseholdOriginFilter.Province.Nombre;
                        filteredList = filteredList.Where(i => AllDistricts.Exists(d => i.Origen is Domicilio dom 
                                                                                        && d.Canton.NombreProvincia == provinceName 
                                                                                        && dom.DistritoId == d.Id)).ToList();

                        
                        if(Value.HouseholdOriginFilter.Canton != null)
                        {
                            var cantonName = Value.HouseholdOriginFilter.Canton.Nombre;
                            filteredList = filteredList.Where(i => AllDistricts.Exists(d => i.Origen is Domicilio dom
                                                                                       && d.Canton.Nombre == cantonName
                                                                                       && dom.DistritoId == d.Id)).ToList();

                            if(Value.HouseholdOriginFilter.District != null)
                            {
                                var districtName = Value.HouseholdOriginFilter.District.Nombre;

                                filteredList = filteredList.Where(i => AllDistricts.Exists(d => i.Origen is Domicilio dom
                                                                                       && d.Nombre == districtName
                                                                                       && dom.DistritoId == d.Id)).ToList();
                            }
                        }
                    } else
                    {
                        filteredList = filteredList.Where((incident) => incident.Origen is Domicilio d).ToList();
                    }
                }
            }

            //Destination
            if (Value.MedicalCenterDestination.MedicalCenter != null)
            {
                var medicalCenterName = Value.MedicalCenterDestination.MedicalCenter.Nombre;
                filteredList = filteredList.Where((incident) => AllMedicalCenters.Exists(m => incident.Destino is CentroUbicacion c
                                                                                            && m.Nombre == medicalCenterName
                                                                                            && c.CentroMedicoId == m.Id)).ToList();
            }

            // State
            if (Value.StateFilter != null && Value.StateFilter.Nombre != String.Empty)
            {
                var state = Value.StateFilter.Nombre;
                filteredList = filteredList.Where((incident) => incident.EstadoIncidentes.Exists(i => i.NombreEstado == state)).ToList();
            }

            return filteredList;
        }
    }
}
