using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PRIME_UCR.Application.Dtos.Incidents;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Application.DTOs.Dashboard;

namespace PRIME_UCR.Components.Dashboard.Filters
{
    public partial class HospitalFilter
    {
        [Parameter] public AppointmentFilterModel Value { get; set; }
        [Parameter] public EventCallback<AppointmentFilterModel> ValueChanged { get; set; }
        [Parameter] public DashboardDataModel Data { get; set; }
        [Parameter] public EventCallback<DashboardDataModel> DataChanged { get; set; }

        private List<CentroMedico> _medicalCenters;
        private bool _isLoading = true;
        private bool _changesMade = false;

        private void OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            if (medicalCenter == Value._selectedHospital.MedicalCenter)
            {
                _changesMade = false;
            }
            else
            {
                _changesMade = true;
            }
            Value._selectedHospital.MedicalCenter = medicalCenter;
        }

        private void LoadExistingValues()
        {
            _isLoading = true;
            StateHasChanged();
            _medicalCenters = Data.medicalCenters;
            _isLoading = false;
        }

        protected override void OnInitialized()
        {
            LoadExistingValues();
        }

        private void Discard()
        {
            _changesMade = false;
            Value._selectedHospital.MedicalCenter = Value.Hospital.MedicalCenter;
        }

        private async Task Save()
        {
            StateHasChanged();
            Value.Hospital.MedicalCenter = Value._selectedHospital.MedicalCenter;
            if (Value.Hospital.MedicalCenter != null)
            {
                Value.ButtonEnabled = true;
            }
            else
            {
                Value.ButtonEnabled = false;
            }
            _changesMade = false;
            await ValueChanged.InvokeAsync(Value);
        }

    }
}