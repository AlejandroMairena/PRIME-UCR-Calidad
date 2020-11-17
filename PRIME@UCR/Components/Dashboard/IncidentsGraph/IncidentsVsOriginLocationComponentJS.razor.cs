﻿using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.IncidentsGraph
{
    public partial  class IncidentsVsOriginLocationComponentJS
    {
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        [Parameter] public bool ZoomActive { get; set; }

        private int eventQuantity { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        public IDashboardService _dashboardService { get; set; }

        [Inject]
        public ILocationService _locationService { get; set; }

        [Inject]
        IModalService Modal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await GenerateIncidentsVsOriginLocationComponentJS();
        }

        private async Task GenerateIncidentsVsOriginLocationComponentJS()
        {
            var countriesData = await _locationService.GetAllCountriesAsync();
            var provincesData = await _locationService.GetProvincesByCountryNameAsync(Pais.DefaultCountry);
            var cantonData = new List<Canton>();
            var districtData = new List<Distrito>();
            if(Value.HouseholdOriginFilter.Province != null)
            {
                cantonData = (await _locationService.GetCantonsByProvinceNameAsync(Value.HouseholdOriginFilter.Province.Nombre)).ToList();
            }
            if (Value.HouseholdOriginFilter.Canton != null)
            {
                districtData = (await _locationService.GetDistrictsByCantonIdAsync(Value.HouseholdOriginFilter.Canton.Id)).ToList();
            }

            var incidentsData = await _dashboardService.GetFilteredIncidentsList(Value);
            var medicalCenters = await _locationService.GetAllMedicalCentersAsync();
            var districtsData = await _dashboardService.GetAllDistrictsAsync();

            var AllIncidentsData = await _dashboardService.GetAllIncidentsAsync();

            eventQuantity = incidentsData.Count();

            var incidentsPerOrigin = new List<IGrouping<string, Incidente>>();
            
            if (Value.OriginType == "Internacional")
            {
                incidentsPerOrigin = incidentsData.GroupBy(i => {
                    if (i.Origen != null)
                    {
                        var inter = i.Origen as Internacional;
                        return inter.NombrePais;
                    }
                    else
                    {
                        return null;
                    }
                }).ToList();
            } else if (Value.OriginType == "Centro médico")
            {
                incidentsPerOrigin = incidentsData.GroupBy(i => {
                    if (i.Origen != null)
                    {
                        var centroUbicacion = i.Origen as CentroUbicacion;
                        return medicalCenters.FirstOrDefault(mc => mc.Id == centroUbicacion.CentroMedicoId)?.Nombre;
                    }
                    else
                    {
                        return null;
                    }
                }).ToList();
            } else if (Value.OriginType == "Domicilio")
            {
                if(Value.HouseholdOriginFilter.District != null)
                {
                    incidentsPerOrigin = incidentsData.GroupBy(i => {
                        if (i.Origen != null)
                        {
                            var domicilio = i.Origen as Domicilio;
                            return districtsData.Find(district => domicilio?.DistritoId == district.Id)?.Nombre;
                        }
                        else
                        {
                            return null;
                        }
                    }).ToList();
                } else if (Value.HouseholdOriginFilter.Canton != null)
                {
                    incidentsPerOrigin = incidentsData.GroupBy(i => {
                        if (i.Origen != null)
                        {
                            var domicilio = i.Origen as Domicilio;
                            return districtsData.Find(district => domicilio?.DistritoId == district.Id)?.Nombre;
                        }
                        else
                        {
                            return null;
                        }
                    }).ToList();
                } else if (Value.HouseholdOriginFilter.Province != null)
                {
                    incidentsPerOrigin = incidentsData.GroupBy(i => {
                        if (i.Origen != null)
                        {
                            var domicilio = i.Origen as Domicilio;
                            return districtsData.Find(district => domicilio?.DistritoId == district.Id)?.Canton.Nombre;
                        }
                        else
                        {
                            return null;
                        }
                    }).ToList();
                } else
                {
                    incidentsPerOrigin = incidentsData.GroupBy(i => {
                        if (i.Origen != null)
                        {
                            var domicilio = i.Origen as Domicilio;
                            return districtsData.Find(district => domicilio?.DistritoId == district.Id)?.Canton.NombreProvincia;
                        }
                        else
                        {
                            return null;
                        }
                    }).ToList();
                }
            } else
            {
                incidentsPerOrigin = incidentsData.GroupBy(i => {
                    if(i.Origen != null)
                    {
                        return i.Origen?.GetType().Name;    
                    } else
                    {
                        return null;
                    }
                }).ToList();
            }


            var results = new List<String>();


            foreach (var incidents in incidentsPerOrigin)
            {
                var labelName = "No Asignado";
                if(incidents.Key != null)
                {
                    labelName = incidents.Key;
                }
                results.Add(labelName);
                results.Add(incidents.ToList().Count().ToString());
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsOriginLocationComponentJS", (object)results);
        }

        void ShowModal()
        {
            var modalOptions = new ModalOptions()
            {
                Class = "graph-zoom-modal blazored-modal"
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(IncidentsVsOriginLocationComponentJS.Value), Value);
            parameters.Add(nameof(IncidentsVsOriginLocationComponentJS.ZoomActive), true);
            Modal.Show<IncidentsVsOriginLocationComponentJS>("Incidentes VS Ubicacion de Origen", parameters, modalOptions);
        }
    }
}