﻿using System;
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

namespace PRIME_UCR.Components.Dashboard.Filters.LocationFilter
{
    public partial class MedicalCenterFilter
    {
        [Inject] public ILocationService LocationService { get; set; }
        [Parameter] public MedicalCenterLocationModel Value { get; set; }
        [Parameter] public bool IsOrigin { get; set; }
        [Parameter] public EventCallback<MedicalCenterLocationModel> OnSave { get; set; }
        [Parameter] public EventCallback OnDiscard { get; set; }
        [Parameter] public bool IsFirst { get; set; }
        public string DoctorForLabel => IsOrigin ? "Médico en origen" : "Médico en destino";

        private List<CentroMedico> _medicalCenters;
        private EditContext _context;
        private bool _isLoading = true;

        async Task OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            Value.MedicalCenter = medicalCenter;
        }

        private async Task LoadMedicalCenters(bool firstRender)
        {
            _medicalCenters =
                (await LocationService.GetAllMedicalCentersAsync())
                .ToList();

            if (!firstRender)
                Value.MedicalCenter = null;
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
                Value = new MedicalCenterLocationModel { IsOrigin = IsOrigin };

            await LoadExistingValues();

            _context = new EditContext(Value);
            _context.OnFieldChanged += HandleFieldChanged;
        }

        // used to toggle submit button disabled attribute
        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            StateHasChanged();
        }

        public void Dispose()
        {
            _context.OnFieldChanged -= HandleFieldChanged;
        }
    }
}