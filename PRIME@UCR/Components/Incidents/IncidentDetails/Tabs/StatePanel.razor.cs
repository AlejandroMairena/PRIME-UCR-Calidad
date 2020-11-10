﻿using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models.Incidents;
using Radzen;
using RepoDb.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class StatePanel
    {

        public string nextState;

        public List<string> PendingTasks = new List<string>();

        [Parameter]
        public IncidentDetailsModel Incident { get; set; }

        [Inject]
        public IIncidentService IncidentService { get; set; }


        private int currentStateIndex;
        MatButton Button2;
        BaseMatMenu Menu2;
        public List<Tuple<string, string>> IncidentStatesList = new List<Tuple<string, string>> {
            Tuple.Create(IncidentStates.InCreationProcess.Nombre ,"Creando"),
            Tuple.Create(IncidentStates.Created.Nombre,"Creado"),
            Tuple.Create(IncidentStates.Rejected.Nombre, "Rechazado"),
            Tuple.Create(IncidentStates.Approved.Nombre, "Aceptado"),
            Tuple.Create(IncidentStates.Assigned.Nombre, "Asignado"),
            Tuple.Create(IncidentStates.Preparing.Nombre, "Preparación"),
            Tuple.Create(IncidentStates.InOriginRoute.Nombre, "Hacia origen"),
            Tuple.Create(IncidentStates.PatientInOrigin.Nombre, "Colecta"),
            Tuple.Create(IncidentStates.InRoute.Nombre, "Traslado"),
            Tuple.Create(IncidentStates.Delivered.Nombre, "Entregado"),
            Tuple.Create(IncidentStates.Reactivated.Nombre, "Reactivado"),
            Tuple.Create(IncidentStates.Done.Nombre, "Finalizado")
        };

        protected override async Task OnInitializedAsync()
        {
            currentStateIndex = IncidentStatesList.FindIndex(i => i.Item1 == Incident.CurrentState);
            nextState = (await IncidentService.GetNextIncidentState(Incident.Code)).ToString();
            PendingTasks.Add("Prueba1, ");
            PendingTasks.Add("Test ");
        }

        public string setStateColor(int index)
        {
            string className = "";
            if (index < currentStateIndex)
            {
                className = "bg-primary";
            } else if(index > currentStateIndex)
            {
                className = "bg-secondary";
            }
            else
            {
                className = "bg-current-state";
            }
            return className;
        }

        public void OnClick(MouseEventArgs e)
        {
            this.Menu2.OpenAsync(Button2.Ref);
        }

        public void FormatPendingTasks()
        {
            for(int index = 0; index < PendingTasks.Count; ++index)
            {

            }
        }
    }
}

