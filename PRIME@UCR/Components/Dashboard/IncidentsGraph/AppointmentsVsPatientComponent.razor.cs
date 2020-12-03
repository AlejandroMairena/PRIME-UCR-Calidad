using Blazored.Modal;
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
    public partial class AppointmentsVsPatientComponent
    {
        [Parameter] public DashboardDataModel Data { get; set; }
        [Parameter] public EventCallback<DashboardDataModel> DataChanged { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }

        [Parameter] public bool ZoomActive { get; set; }


        private int patientQuantity { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        IModalService Modal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GenerateCreateAppointmentVsPatientComponent();
        }


        protected override async Task OnParametersSetAsync()
        {
            await GenerateCreateAppointmentVsPatientComponent();
        }

        /*
         *  GenerateCreateAppointmentVsPatientComponent()
         *  
         *  Method that generates the AppointmentVsPatient Graph 
         *  based on the incoming list from the dashboard service in which 
         *  data is already filtered 
         *  
         */
        private async Task GenerateCreateAppointmentVsPatientComponent()
        {
            //TODO: Use Cita Data to create this
            //var incidentsData = Data.filteredIncidentsData;

            //eventQuantity = incidentsData.Count();

            var medicalCenters = Data.medicalCenters;

            /*var incidentsPerDestination = incidentsData.GroupBy(i => {
                if (i.Destino != null)
                {
                    var cu = i.Destino as CentroUbicacion;
                    return cu.CentroMedicoId;
                }
                else
                {
                    return 0;
                }
            });*/

            patientQuantity = 29;
            var results = new List<String>();
            var appointments = new List<int>() { 2, 3, 3, 3, 3 };
            var i = 0;
            foreach (var medicalCenter in medicalCenters)
            {
                /*
                var labelName = "No Asignado";
                if (incidents.Key != 0)
                {
                    var medicalCenter = medicalCenters.Where((medCenter) => incidents.ToList().First().Destino is CentroUbicacion cu
                                                                                        && cu.CentroMedicoId == medCenter.Id);
                    if (medicalCenter.Any())
                    {
                        labelName = medicalCenter.First().Nombre;
                    }
                }*/
                results.Add(medicalCenter.Nombre);
                results.Add(appointments[i].ToString());
                i++;
                //results.Add(incidents.ToList().Count().ToString());
            }

            await JS.InvokeVoidAsync("CreateAppointmentVsPatientComponentJS", (object)results);
        }

        void ShowModal()
        {
            var modalOptions = new ModalOptions()
            {
                Class = "graph-zoom-modal blazored-modal"
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(AppointmentsVsPatientComponent.Data), Data);
            parameters.Add(nameof(AppointmentsVsPatientComponent.ZoomActive), true);
            Modal.Show<AppointmentsVsPatientComponent>("Citas por Paciente", parameters, modalOptions);
        }
    }
}
