using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Services.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Components.Incidents.LocationPickers
{
    public partial class MedicalCenterPicker
    {
        [Inject]
        public ILocationService LocationService { get; set; }
        
        [Parameter]
        public Ubicacion Value { get; set; }

        [Parameter]
        public String LocationContext { get; set; } 
        
        [Parameter]
        public EventCallback<Ubicacion> ValueChanged { get; set; }
        
        private CentroMedico _selectedMedicalCenter;

        private Médico _selectedDoctor;

        private String doctorForLabel;

        private int _bedNumber;

        private List<CentroMedico> _values;

        private List<Médico> _doctors;

        async Task Callback()
        {
            var location = new CentroUbicacion
            {
                CentroMedicoId = _selectedMedicalCenter.Id,
                NumeroCama = _bedNumber
            };
            await ValueChanged.InvokeAsync(location);
        }

        async Task OnChangeMedicalCenter(CentroMedico medicalCenter)
        {
            _selectedMedicalCenter = medicalCenter;

            await Callback();
        }
        async Task OnChangeDoctor(Médico doctor)
        {
            _selectedDoctor = doctor;

            await Callback();
        }

        protected override async Task OnInitializedAsync()
        {
            _values =
                (await LocationService.GetAllMedicalCentersAsync())
                .ToList();
            _doctors = new List<Médico>() { new Médico(), new Médico() }; //Hardcoded to show 2 options on display
            doctorForLabel = "Médico de " + LocationContext;

            if (Value is CentroUbicacion location)
            {
                _selectedMedicalCenter = _values.First(mc => mc.Id == location.CentroMedicoId);
                _bedNumber = location.NumeroCama;
            }
            else
            {
                _selectedMedicalCenter = _values.First();
                await Callback();
            }

        }
    }
}