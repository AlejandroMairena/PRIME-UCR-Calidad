using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class PatientTab
    {
        [Parameter]
        public Expediente Expediente { get; set; }

        [Parameter]
        public EventCallback<PatientModel> OnSave { get; set; }

        private PatientModel _model = new PatientModel();

        private void OnIDChange(string Id)
        {
            _model.CedPaciente = Id; 
        }

        private async Task Save()
        {
            await OnSave.InvokeAsync(_model);
        }

        protected override void OnInitialized()
        {
           _model.Expediente = Expediente;
         }

    }
}