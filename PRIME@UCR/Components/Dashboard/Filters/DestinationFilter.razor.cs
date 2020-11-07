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
        [Parameter] public FilterModel Value { get; set; }
        [Parameter] public EventCallback<FilterModel> ValueChanged { get; set; }

        private List<CentroMedico> _medicalCenters;
        private bool _isLoading = true;

        async Task OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            Value.MedicalCenterDestination.MedicalCenter = medicalCenter;
            await ValueChanged.InvokeAsync(Value);
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

        protected override async Task OnInitializedAsync()
        {
            await LoadExistingValues();
        }
    }
}