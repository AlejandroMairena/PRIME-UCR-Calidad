using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Components.Incidents.IncidentDetails.Constants;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class DestinationTab
    {
        [Inject] private ILocationService LocationService { get; set; }
        [Inject] private IDoctorService DoctorService { get; set; }
        [Parameter] public IncidentDetailsModel Incident { get; set; }
        public Ubicacion Destination { get; set; }
        [Parameter] public EventCallback<DestinationModel> OnSave { get; set; }
        [Parameter] public string StatusMessage { get; set; }
        [Parameter] public string StatusClass { get; set; }
        [CascadingParameter] public Action ClearStatusMessageCallback { get; set; }
        
        private DestinationModel _model = new DestinationModel();
        private MedicalCenterLocationModel _medicalCenterModel = new MedicalCenterLocationModel();
        private bool _isLoading = false;
        // Info for Incident summary that is shown at top of the page
        public IncidentSummary Summary = new IncidentSummary();

        private async Task OnMedicalCenterSave(MedicalCenterLocationModel medicalCenter)
        {
            _model.Destination = new CentroUbicacion
            {
                CedulaMedico = medicalCenter.Doctor.Cédula,
                CentroMedicoId = medicalCenter.MedicalCenter.Id,
                NumeroCama = medicalCenter.BedNumber
            };

            _medicalCenterModel = medicalCenter;
            await Save();
        }

        private async Task Save()
        {
            await OnSave.InvokeAsync(_model);
        }

        private async Task LoadExistingValues()
        {
            _isLoading = true;
            Destination = Incident.Destination;
            StateHasChanged();
            if (Destination is CentroUbicacion mc)
            {
                var doctor = await DoctorService.GetDoctorByIdAsync(mc.CedulaMedico);
                var medicalCenter = await LocationService.GetMedicalCenterById(mc.CentroMedicoId);
                _medicalCenterModel = new MedicalCenterLocationModel
                {
                    IsOrigin = false,
                    BedNumber = mc.NumeroCama,
                    Doctor = doctor,
                    MedicalCenter = medicalCenter
                };
            }
            
            _model.Destination = Destination;
            ClearStatusMessageCallback();
            _isLoading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            Summary.LoadValues(Incident);
            await LoadExistingValues();
        }
    }
}
