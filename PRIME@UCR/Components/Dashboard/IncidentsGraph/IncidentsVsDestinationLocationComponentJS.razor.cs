﻿using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.IncidentsGraph
{
    public partial class IncidentsVsDestinationLocationComponentJS
    {
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        [Parameter] public bool  ZoomActive { get; set; }


        private int eventQuantity { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        public IDashboardService _dashboardService { get; set; }

        [Inject]
        IModalService Modal { get; set; }

        [Inject]
        public ILocationService _locationService { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            await GenerateIncidentsVsDestinationLocationComponentJS();
        }


        private async Task GenerateIncidentsVsDestinationLocationComponentJS()
        {
            var incidentsData = await _dashboardService.GetFilteredIncidentsList(Value);

            eventQuantity = incidentsData.Count();

            var medicalCenters = await _locationService.GetAllMedicalCentersAsync();

            var incidentsPerDestination = incidentsData.GroupBy(i => { 
                if(i.Destino != null)
                {
                    var cu = i.Destino as CentroUbicacion;
                    return cu.CentroMedicoId;
                } else
                {
                    return 0;
                }
            });

            var results = new List<String>();

            foreach (var incidents in incidentsPerDestination)
            {
                var labelName = "No Asignado";
                if(incidents.Key != 0)
                {
                    var medicalCenter = medicalCenters.Where((medCenter) => incidents.ToList().First().Destino is CentroUbicacion cu 
                                                                                        && cu.CentroMedicoId == medCenter.Id);
                    if (medicalCenter.Any())
                    {
                        labelName = medicalCenter.First().Nombre;
                    }
                }
                results.Add(labelName);
                results.Add(incidents.ToList().Count().ToString());
            }

            await JS.InvokeVoidAsync("CreateIncidentsVsDestinationLocationComponentJS", (object)results);
        }

        void ShowModal()
        {
            var modalOptions = new ModalOptions()
            {
                Class = "graph-zoom-modal blazored-modal"
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(IncidentsVsDestinationLocationComponentJS.Value), Value);
            parameters.Add(nameof(IncidentsVsDestinationLocationComponentJS.ZoomActive), true);
            Modal.Show<IncidentsVsDestinationLocationComponentJS>("Incidentes VS Ubicacion de Destino", parameters, modalOptions);
        }
    }
}