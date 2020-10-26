using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class Assignment
    {
        [Parameter] public string Mode { get; set; }
        [Parameter] public EventCallback<AssignmentModel> OnSave { get; set; }
        [Parameter] public UnidadDeTransporte TransportUnit { get; set; }

        private string _statusMessage = "";
        private AssignmentModel _model = new AssignmentModel();
        private bool _isLoading { get; set; }
        private async Task Save()
        {
            _statusMessage = "Se guardaron los cambios exitosamente.";
            await OnSave.InvokeAsync(_model);
        }
        private async Task OnTransportUnitSave(AssignmentModel assignmentModel)
        {
            _model.TransportUnit = new UnidadDeTransporte
            {
                Matricula = assignmentModel.TransportUnit.Matricula,
                Estado = assignmentModel.TransportUnit.Estado,
                Modalidad = assignmentModel.TransportUnit.Modalidad,
                ModalidadTrasporte = assignmentModel.TransportUnit.ModalidadTrasporte
            };
            await Save();
        }

        private async Task LoadExistingValues()
        {
            _isLoading = true;
            StateHasChanged();
            if (TransportUnit != null)
            {
                _model.TransportUnit = TransportUnit;
            }
            _statusMessage = "";
            _isLoading = false;
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues();
        }
    }
}
