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
    public partial class DestinationFilter
    {
        [Inject] public ILocationService LocationService { get; set; }
        [Parameter] public bool IsOrigin { get; set; }
        [Parameter] public EventCallback<MedicalCenterLocationModel> OnSave { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }
        [Parameter] public bool IsFirst { get; set; }
        public string DoctorForLabel => IsOrigin ? "Médico en origen" : "Médico en destino";
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }

        private List<CentroMedico> _medicalCenters;
        private bool _isLoading = true;

        async Task OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            Value.MedicalCenterDestination.MedicalCenter = medicalCenter;
        }

        private async Task LoadMedicalCenters(bool firstRender)
        {
            _medicalCenters =
                (await LocationService.GetAllMedicalCentersAsync())
                .ToList();
            if (!firstRender)
                Value.MedicalCenterDestination.MedicalCenter = null;
        }


        private async Task LoadExistingValues()
        {
            _isLoading = true;
            StateHasChanged();
            await LoadMedicalCenters(true);
            _isLoading = false;
        }

        private async Task Discard()
        {
            await OnDiscard.InvokeAsync(null);
            if (!IsFirst && !OnDiscard.HasDelegate)
                await LoadExistingValues();
        }

        protected override async Task OnInitializedAsync()
        {
            if (IsFirst)
                Value.MedicalCenterDestination = new MedicalCenterLocationModel { IsOrigin = IsOrigin };
            await LoadExistingValues();

        }

        // used to toggle submit button disabled attribute
        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            StateHasChanged();
        }
    }
}