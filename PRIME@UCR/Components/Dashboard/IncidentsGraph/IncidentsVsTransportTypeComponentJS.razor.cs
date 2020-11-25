﻿using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PRIME_UCR.Application.DTOs.Dashboard;
using PRIME_UCR.Application.Services.Dashboard;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Dashboard.IncidentsGraph
{
    public partial class IncidentsVsTransportTypeComponentJS
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
        IModalService Modal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await GenerateColumnChart();
        }

        private async Task GenerateColumnChart()
        {
            var incidentsData = await _dashboardService.GetFilteredIncidentsList(Value);
            eventQuantity = incidentsData.Count();

            var incidentsPerModality = incidentsData.GroupBy(i => i.Modalidad);

            var results = new List<String>();

            foreach (var incidents in incidentsPerModality)
            {
                var modalityName = incidents.First().Modalidad;
                results.Add(modalityName);
                results.Add(incidents.ToList().Count().ToString());
            }



            await JS.InvokeVoidAsync("CreateColumnChart", (object)results);
        }


        void ShowModal()
        {
            var modalOptions = new ModalOptions()
            {
                Class = "graph-zoom-modal blazored-modal"
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(IncidentsVsTransportTypeComponentJS.Value), Value);
            parameters.Add(nameof(IncidentsVsTransportTypeComponentJS.ZoomActive), true);
            Modal.Show<IncidentsVsTransportTypeComponentJS>("Incidente vs Modalidad de Transporte", parameters, modalOptions);
        }
    }
}
