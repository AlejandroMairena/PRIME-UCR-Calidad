using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class Assignment
    {
        [Parameter] public IncidentDetailsModel Incident { get; set; }
        [Parameter] public EventCallback<AssignmentModel> OnSave { get; set; }
        [Inject] public IAssignmentService AssignmentService { get; set; }

        private IEnumerable<string> Specialists
        {
            get => _model.TeamMembers.Select(tm => tm.Cédula);
            set =>
                _model.TeamMembers =
                    (from member in _specialists
                        join id in value on member.Cédula equals id
                        select member)
                    .ToList();
        }
        
        // dropdown menu values 
        private List<UnidadDeTransporte> _transportUnits;
        private List<EspecialistaTécnicoMédico> _specialists;
        private List<CoordinadorTécnicoMédico> _coordinators;
        
        private string _statusMessage = "";
        private AssignmentModel _model = new AssignmentModel();
        private bool _isLoading = true;
        private bool _saveButtonEnabled;
        private EditContext _context;
        
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
            _transportUnits =
                (await AssignmentService.GetAllTransportUnitsByMode(Incident.Mode))
                .ToList();

            _coordinators =
                (await AssignmentService.GetCoordinatorsAsync())
                .ToList();

            _specialists =
                (await AssignmentService.GetSpecialistsAsync())
                .ToList();
            
            _statusMessage = "";
            _isLoading = false;
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues();
        }
    }
}
