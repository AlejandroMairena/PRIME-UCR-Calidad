using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models;

namespace PRIME_UCR.Components.Incidents.IncidentDetails.Tabs
{
    public partial class DestinationTab
    {
        [Inject] private ILocationService LocationService { get; set; }
        [Inject] private IDoctorService DoctorService { get; set; }
        [Parameter] public Ubicacion Destination { get; set; }
        [Parameter] public EventCallback<DestinationModel> OnSave { get; set; }
        
        private DestinationModel _model = new DestinationModel();
        private string _statusMessage = "";
        private MedicalCenterLocationModel _medicalCenterModel = new MedicalCenterLocationModel();
        private bool _isLoading = false;

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
            _statusMessage = "Se guardaron los cambios exitosamente.";
            await OnSave.InvokeAsync(_model);
        }

        private async Task LoadExistingValues()
        {
            _isLoading = true;
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
            _statusMessage = "";
            _isLoading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues();
        }
    }
}
